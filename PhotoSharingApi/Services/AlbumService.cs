using AutoMapper;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models.Albums;
using PhotoSharingApi.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace PhotoSharingApi.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IMapper _mapper;

        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IMapper mapper, IAlbumRepository albumRepository) 
        { 
            _mapper = mapper;
            _albumRepository = albumRepository;
        }

        public void Create(CreateAlbumRequestModel album)
        {
            var newAlbum = new Album
            {
                user_id = album.OwnerId,
                name = album.Name,
                created_at = DateTime.Now
            };

            _albumRepository.Add(newAlbum);
        }

        public GetAlbumResponseModel Get(int albumId)
        {
           var album = _albumRepository.GetAlbumPhotos(albumId);

           if (album != null)
           {
                var result = new GetAlbumResponseModel
                {
                    Name = album.name,
                    Owner = album.User.username,
                    CreatedAt = album.created_at,
                    Photos = album.PhotoAlbums.Select(item => item.Photo).ToList()
                };

                return result;
           }

           return null;
        }

        public List<UserAlbumsResponseModel> GetUserAlbums(int userId) => _mapper.Map<List<UserAlbumsResponseModel>>(_albumRepository.GetAllUserAlbums(userId));
    }
}
