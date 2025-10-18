using InsightHub.Dtos;
using InsightHub.Models;
using InsightHub.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryRepository categoryRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestDto requestDto)
        {
            var category = new Category
            {
                Name = requestDto.Name,
                UrlHandle = requestDto.UrlHandle
            };

            await categoryRepository.CreateAsync(category);

            // Domain model to DTO
            var response = new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }
    }
}
