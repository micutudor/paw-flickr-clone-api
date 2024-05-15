using AutoMapper;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using PhotoSharingApi.Models;
using PhotoSharingApi.Services.Interfaces;

namespace PhotoSharingApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository) 
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public List<CategoryModel> GetAll() => _mapper.Map<List<CategoryModel>>(_categoryRepository.GetAll());
    }
}
