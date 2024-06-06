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

        public async Task Create(int userID,string name)
        {
            var newAlbum = new Album
            {
                user_id = userID,
                name = name,
                created_at = DateTime.Now
            };

            await _albumRepository.Add(newAlbum);
        }

        public List<UserAlbumsResponseModel> GetUserAlbums(int userID) => _mapper.Map<List<UserAlbumsResponseModel>>(_albumRepository.GetAllUserAlbums(userID));
    }
}
