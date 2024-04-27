using AutoMapper;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;
using PhotoSharingApi.Models.Albums;
using PhotoSharingApi.Models.Photos;
using System.Text.Json;

namespace PhotoSharingApi.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.category_id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));

            CreateMap<Photo, PhotoModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.photo_id))
                .ForMember(dest => dest.PostedBy, opt => opt.MapFrom(src => src.User.username))
                .ForMember(dest => dest.PostedOn, opt => opt.MapFrom(src => src.posted_at))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.path))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Geolocation, opt => opt.MapFrom(src => JsonSerializer.Deserialize<PhotoGeolocationModel>(src.geolocation, JsonSerializerOptions.Default)))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.PhotoCategories.Select(item => new CategoryModel { Id = item.Category.category_id, Name = item.Category.name })));

            CreateMap<Album, UserAlbumsResponseModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.album_id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.PhotosCount, opt => opt.MapFrom(src => src.PhotoAlbums.Count));
        }
    }
}
