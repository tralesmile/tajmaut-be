using AutoMapper;
using tajmautAPI.Exceptions;
using tajmautAPI.Interfaces;
using tajmautAPI.Interfaces_Service;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;

namespace tajmautAPI.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo,IMapper mapper)
        {
            _repo= repo;
            _mapper= mapper;
        }

        public async Task<ServiceResponse<CategoryRESPONSE>> CreateCategory(CategoryREQUEST request)
        {
            ServiceResponse<CategoryRESPONSE> result = new();
            try
            {
                var getCategory = await _repo.CreateCategory(request);
                result.Data = _mapper.Map<CategoryRESPONSE>(getCategory);
            }
            catch (CustomError ex)
            {
                result.isError= true;
                result.errorMessage= ex.ErrorMessage;
                result.statusCode= ex.StatusCode;
            }

            return result;
        }

        public async Task<ServiceResponse<CategoryRESPONSE>> DeleteCategory(int categoryId)
        {
            ServiceResponse<CategoryRESPONSE> result = new();
            try
            {
                var category = await _repo.GetCategoryById(categoryId);

                await _repo.DeleteCategory(category);

                result.Data = _mapper.Map<CategoryRESPONSE>(category);

            }
            catch (CustomError ex)
            {
                result.isError= true;
                result.errorMessage= ex.ErrorMessage;
                result.statusCode= ex.StatusCode;
            }

            return result;
        }

        public async Task<ServiceResponse<List<CategoryRESPONSE>>> GetAllCategories()
        {
            ServiceResponse<List<CategoryRESPONSE>> result = new();
            try
            {
                var categories = await _repo.GetAllCategories();
                result.Data = _mapper.Map<List<CategoryRESPONSE>>(categories);
            }
            catch (CustomError ex)
            {
                result.isError= true;
                result.errorMessage= ex.ErrorMessage;
                result.statusCode= ex.StatusCode;
            }

            return result;
        }

        public async Task<ServiceResponse<CategoryRESPONSE>> UpdateCategory(CategoryREQUEST request, int catId)
        {
            ServiceResponse<CategoryRESPONSE> result = new();
            try
            {
                var category = await _repo.GetCategoryById(catId);
                var categoryUpdate = await _repo.UpdateCategory(category,request);
                result.Data = _mapper.Map<CategoryRESPONSE>(categoryUpdate);
            }
            catch(CustomError ex)
            {
                result.isError= true;
                result.errorMessage= ex.ErrorMessage;
                result.statusCode= ex.StatusCode;
            }

            return result;
        }
    }
}
