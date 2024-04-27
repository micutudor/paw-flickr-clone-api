using AutoMapper;
using Microsoft.OpenApi.Writers;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.Models;

namespace PhotoSharingApi.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.category_id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));

            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.First_Name, opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest => dest.Last_Name, opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(dest => dest.Is_Moderator, opt => opt.MapFrom(src => src.is_moderator));

            CreateMap<Comment, CommentModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.comment_id))
                .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.Photo_Id, opt => opt.MapFrom(src => src.photo_id))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.comment))
                .ForMember(dest => dest.Commented_At, opt => opt.MapFrom(src => src.commented_at))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status));

        }
    }
}
