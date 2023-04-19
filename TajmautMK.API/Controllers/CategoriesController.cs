using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TajmautMK.Core.Services.Interfaces;
using TajmautMK.Common.Models.ModelsREQUEST;

namespace TajmautMK.API.Controllers
{
    /// <summary>
    /// Controller for handling category related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor for creating an instance of CategoriesController.
        /// </summary>
        /// <param name="categoryService">The category service used for performing category related operations.</param>
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="request">The request object containing category details.</param>
        /// <returns>The created category object on success or an error message on failure.</returns>
        [HttpPost("CreateCategory"),Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateCategory(CategoryREQUEST request)
        {
            var result = await _categoryService.CreateCategory(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of category objects on success or an error message on failure.</returns>
        [HttpGet("GetAllCategories"), AllowAnonymous]
        public async Task<ActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategories();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to be deleted.</param>
        /// <returns>A success message on success or an error message on failure.</returns>
        [HttpDelete("DeleteCategory"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Category Deleted");
        }

        /// <summary>
        /// Updates a category by ID.
        /// </summary>
        /// <param name="request">The request object containing the updated category details.</param>
        /// <param name="catId">The ID of the category to be updated.</param>
        /// <returns>A success message on success or an error message on failure.</returns>
        [HttpPut("UpdateCategory"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCategory(CategoryREQUEST request,int catId)
        {
            var result = await _categoryService.UpdateCategory(request,catId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.ErrorMessage);
            }

            //if no error
            return Ok("Category Updated");
        }
    }
}
