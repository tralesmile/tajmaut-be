using Microsoft.EntityFrameworkCore;
using tajmautAPI.Middlewares.Exceptions;
using tajmautAPI.Models.EntityClasses;
using tajmautAPI.Models.ModelsREQUEST;
using tajmautAPI.Repositories.Interfaces;
using tajmautAPI.Services.Interfaces;

namespace tajmautAPI.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly tajmautDataContext _ctx;
        private readonly IHelperValidationClassService _helper;

        public CategoryRepository(IHelperValidationClassService helper, tajmautDataContext ctx)
        {
            _ctx = ctx;
            _helper = helper;
        }

        public async Task<CategoryEvent> CreateCategory(CategoryREQUEST request)
        {
            var currentUserID = _helper.GetMe();
            var categoryEvent = new CategoryEvent
            {
                Name = request.Name,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CreatedBy = currentUserID,
                ModifiedBy = currentUserID,
            };

            _ctx.CategoryEvents.Add(categoryEvent);
            await _ctx.SaveChangesAsync();

            return categoryEvent;

        }

        public async Task<bool> DeleteCategory(CategoryEvent cat)
        {
            _ctx.CategoryEvents.Remove(cat);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryEvent>> GetAllCategories()
        {
            var categories = await _ctx.CategoryEvents.ToListAsync();

            if (categories.Count() > 0)
            {
                return categories;
            }

            throw new CustomError(404, $"No categories found");
        }

        public async Task<CategoryEvent> GetCategoryById(int id)
        {
            var category = await _ctx.CategoryEvents.FirstOrDefaultAsync(x => x.CategoryEventId == id);

            if (category != null)
                return category;

            throw new CustomError(404, $"Category not found");
        }

        //update category and save to db
        public async Task<CategoryEvent> UpdateCategory(CategoryEvent category, CategoryREQUEST request)
        {
            category.Name = request.Name;

            await _ctx.SaveChangesAsync();
            return category;
        }
    }
}
