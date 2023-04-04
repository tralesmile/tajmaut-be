using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Models.ModelsRESPONSE;
using tajmautAPI.Services.Implementations;

namespace tajmautAPI.Services.Interfaces
{
    /// <summary>
    /// Service interface for managing event categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="request">The category data.</param>
        /// <returns>A response containing the newly created category.</returns>
        Task<ServiceResponse<CategoryRESPONSE>> CreateCategory(CategoryREQUEST request);

        /// <summary>
        /// Gets all existing categories.
        /// </summary>
        /// <returns>A response containing a list of all categories.</returns>
        Task<ServiceResponse<List<CategoryRESPONSE>>> GetAllCategories();

        /// <summary>
        /// Deletes an existing category.
        /// </summary>
        /// <param name="categoryId">The ID of the category to delete.</param>
        /// <returns>A response containing the deleted category.</returns>
        Task<ServiceResponse<CategoryRESPONSE>> DeleteCategory(int categoryId);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="request">The updated category data.</param>
        /// <param name="categoryId">The ID of the category to update.</param>
        /// <returns>A response containing the updated category.</returns>
        Task<ServiceResponse<CategoryRESPONSE>> UpdateCategory(CategoryREQUEST request, int categoryId);
    }
}
