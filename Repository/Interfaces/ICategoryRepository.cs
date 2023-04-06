using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;

namespace TajmautMK.Repository.Interfaces
{
    /// <summary>
    /// Interface for accessing and modifying category data in the database.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Creates a new category in the database.
        /// </summary>
        /// <param name="request">The category data to be created.</param>
        /// <returns>The created category.</returns>
        Task<CategoryEvent> CreateCategory(CategoryREQUEST request);

        /// <summary>
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        Task<List<CategoryEvent>> GetAllCategories();

        /// <summary>
        /// Gets a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the specified ID.</returns>
        Task<CategoryEvent> GetCategoryById(int id);

        /// <summary>
        /// Deletes a category from the database.
        /// </summary>
        /// <param name="cat">The category to delete.</param>
        /// <returns>True if the category was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteCategory(CategoryEvent cat);

        /// <summary>
        /// Updates an existing category in the database.
        /// </summary>
        /// <param name="category">The category to update.</param>
        /// <param name="request">The updated category data.</param>
        /// <returns>The updated category.</returns>
        Task<CategoryEvent> UpdateCategory(CategoryEvent category, CategoryREQUEST request);
    }
}
