using AutoMapper;
using Microsoft.OpenApi.Writers;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;
using PhotoSharingApi.Models.Albums;
using PhotoSharingApi.Models.Comments;
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
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(dest => dest.IsModerator, opt => opt.MapFrom(src => src.is_moderator));

            CreateMap<Comment, CommentModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.comment_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.PhotoId, opt => opt.MapFrom(src => src.photo_id))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.comment))
                .ForMember(dest => dest.CommentedAt, opt => opt.MapFrom(src => src.commented_at))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status));

        }
    }
}
