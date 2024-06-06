using AutoMapper;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models.Photos;
using PhotoSharingApi.Services.Interfaces;
using System.Text.Json;

namespace PhotoSharingApi.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IMapper _mapper;

        private readonly IPhotoRepository _photoRepository;
        private readonly IPhotoCategoryRepository _photoCategoryRepository;
        private readonly IPhotoAlbumRepository _photoAlbumRepository;

        public PhotoService(
            IMapper mapper, 
            IPhotoRepository photoRepository, 
            IPhotoCategoryRepository photoCategoryRepository, 
            IPhotoAlbumRepository photoAlbumRepository)
        {
            _mapper = mapper;
            _photoRepository = photoRepository;
            _photoCategoryRepository = photoCategoryRepository;
            _photoAlbumRepository = photoAlbumRepository;
        }

        public async Task Add(int authorID, AddPhotoRequestModel photo, IFormFile photoFile)
        {
            Photo addedPhoto = new Photo
            {
                user_id = authorID,
                posted_at = DateTime.Now,
                path = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName),
                geolocation = JsonSerializer.Serialize(photo.Geolocation),
                title = photo.Title,
                description = photo.Description
            };

            string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles\\Photos\\");

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            string newPhotoFilePath = Path.Combine(targetDirectory, addedPhoto.path);

            using (var fileStream = new FileStream(newPhotoFilePath, FileMode.Create))
            {
                await photoFile.CopyToAsync(fileStream);
            }

            await _photoRepository.Add(addedPhoto);

            foreach (var category in photo.Categories)
            {
                await _photoCategoryRepository.Add(new PhotoCategory
                {
                    photo_id = addedPhoto.photo_id,
                    category_id = category
                });
            }

            await _photoAlbumRepository.Add(new PhotoAlbum
            {
                photo_id = addedPhoto.photo_id,
                album_id = photo.Album
            });
        }

        public List<PhotoModel> GetAll(int categoryId = 0)
        {
            List<PhotoModel> results = new List<PhotoModel>();

            if (categoryId == 0)
            {
                results = _mapper.Map<List<PhotoModel>>(_photoRepository.GetAll());
            }
            else
            {
                var categoryPhotos = _photoCategoryRepository.GetPhotosByCategory(categoryId).Select(item => item.Photo);
                results = _mapper.Map<List<PhotoModel>>(categoryPhotos);

            }

            return results.OrderByDescending(item => item.Id).ToList();
        }

        public PhotoModel GetPhotoById(int photoId)
        {
            var result = _mapper.Map<PhotoModel>(_photoRepository.GetById(photoId));
            return result;
        }

        public List<PhotoModel> GetPhotoByTitle(string titlePart)
        {
            var results = _mapper.Map<List<PhotoModel>>(_photoRepository.GetByTitle(titlePart));
            return results;
        }

        public async Task Delete(int photoId)
        {
            var photo = _photoRepository.GetAll().Where(item => item.photo_id == photoId).FirstOrDefault();

            if (photo != null)
            {
                string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles\\Photos\\");

                string photoFilePath = Path.Combine(targetDirectory, photo.path);

                if (File.Exists(photoFilePath))
                    File.Delete(photoFilePath);

                await _photoRepository.Delete(item => item.photo_id == photoId);
            }
        }
    }
}
