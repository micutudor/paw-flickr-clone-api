using AutoMapper;
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
        }
    }
}
