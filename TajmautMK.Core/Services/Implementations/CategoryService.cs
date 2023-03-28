using AutoMapper;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Repositories.Interfaces;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //create category
        public async Task<ServiceResponse<CategoryRESPONSE>> CreateCategory(CategoryREQUEST request)
        {
            ServiceResponse<CategoryRESPONSE> result = new();
            try
            {
                //create category
                var getCategory = await _repo.CreateCategory(request);

                result.Data = _mapper.Map<CategoryRESPONSE>(getCategory);
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.errorMessage = ex.ErrorMessage;
                result.statusCode = ex.StatusCode;
            }

            return result;
        }

        //delete category
        public async Task<ServiceResponse<CategoryRESPONSE>> DeleteCategory(int categoryId)
        {
            ServiceResponse<CategoryRESPONSE> result = new();
            try
            {
                //get category by id
                var category = await _repo.GetCategoryById(categoryId);

                //delete category
                await _repo.DeleteCategory(category);

                result.Data = _mapper.Map<CategoryRESPONSE>(category);

            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.errorMessage = ex.ErrorMessage;
                result.statusCode = ex.StatusCode;
            }

            return result;
        }

        //get all categories
        public async Task<ServiceResponse<List<CategoryRESPONSE>>> GetAllCategories()
        {
            ServiceResponse<List<CategoryRESPONSE>> result = new();
            try
            {
                //get all
                var categories = await _repo.GetAllCategories();

                result.Data = _mapper.Map<List<CategoryRESPONSE>>(categories);
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.errorMessage = ex.ErrorMessage;
                result.statusCode = ex.StatusCode;
            }

            return result;
        }

        //update category
        public async Task<ServiceResponse<CategoryRESPONSE>> UpdateCategory(CategoryREQUEST request, int catId)
        {
            ServiceResponse<CategoryRESPONSE> result = new();
            try
            {
                //get category by id
                var category = await _repo.GetCategoryById(catId);

                //update
                var categoryUpdate = await _repo.UpdateCategory(category, request);

                result.Data = _mapper.Map<CategoryRESPONSE>(categoryUpdate);
            }
            catch (CustomError ex)
            {
                result.isError = true;
                result.errorMessage = ex.ErrorMessage;
                result.statusCode = ex.StatusCode;
            }

            return result;
        }
    }
}
