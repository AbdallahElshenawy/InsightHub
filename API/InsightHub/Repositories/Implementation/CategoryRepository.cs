using InsightHub.Data;
using InsightHub.Models;
using InsightHub.Repositories.Interface;

namespace InsightHub.Repositories.Implementation
{
    public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
    {
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
