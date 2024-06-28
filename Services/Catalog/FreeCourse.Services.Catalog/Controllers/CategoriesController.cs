using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController(ICategoryService _categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var categories = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id){
            var category = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto){
            var response = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInstance(response);
        }
    }
}