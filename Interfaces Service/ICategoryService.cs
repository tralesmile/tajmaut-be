using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Service;

namespace tajmautAPI.Interfaces_Service
{
    public interface ICategoryService
    {
        Task<ServiceResponse<CategoryRESPONSE>> CreateCategory(CategoryREQUEST request);
        Task<ServiceResponse<List<CategoryRESPONSE>>> GetAllCategories();
        Task<ServiceResponse<CategoryRESPONSE>> DeleteCategory(int categorieId);  
        Task<ServiceResponse<CategoryRESPONSE>> UpdateCategory(CategoryREQUEST request,int catId);
    }
}
