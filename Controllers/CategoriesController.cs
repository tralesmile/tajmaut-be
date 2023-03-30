using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Services.Interfaces;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;

namespace tajmautAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //create category
        [HttpPost("CreateCategory"),Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateCategory(CategoryREQUEST request)
        {
            var result = await _categoryService.CreateCategory(request);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        [HttpGet("GetAllCategories"), AllowAnonymous]
        public async Task<ActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategories();

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok(result.Data);
        }

        [HttpDelete("DeleteCategory"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Category Deleted");
        }

        [HttpPut("UpdateCategory"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCategory(CategoryREQUEST request,int catId)
        {
            var result = await _categoryService.UpdateCategory(request,catId);

            //check if error exists
            if (result.isError)
            {
                return StatusCode((int)result.statusCode, result.errorMessage);
            }

            //if no error
            return Ok("Category Updated");
        }
    }
}
