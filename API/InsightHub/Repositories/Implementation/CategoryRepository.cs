using InsightHub.Data;
using InsightHub.Models;
using InsightHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory is null)
            {
                return null;
            }

            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            // Query
            var categories = dbContext.Categories.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(query))
            {
                categories = categories.Where(x => x.Name.Contains(query));
            }


            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                bool isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);

                categories = sortBy.ToLower() switch
                {
                    "name" => isAsc ? categories.OrderBy(x => x.Name)
                                    : categories.OrderByDescending(x => x.Name),
                    "url" => isAsc ? categories.OrderBy(x => x.UrlHandle)
                                   : categories.OrderByDescending(x => x.UrlHandle),
                    _ => categories
                };
            }

            var skipResults = (pageNumber - 1) * pageSize;
            categories = categories.Skip(skipResults).Take(pageSize);

            return await categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCount()
        {
            return await dbContext.Categories.CountAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }

            return null;
        }
    }
}