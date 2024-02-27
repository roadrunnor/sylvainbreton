namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(SylvainBretonDbContext context, ILogger<CategoriesController> logger, ICategoryService categoryService) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<CategoriesController> _logger = logger;
        private readonly ICategoryService _categoryService = categoryService;

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var serviceResult = await _categoryService.GetAllCategoriesAsync();

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return Ok(serviceResult.Data);
        }


        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var serviceResult = await _categoryService.GetCategoryByIdAsync(id);

            if (!serviceResult.Success)
            {
                if (serviceResult.StatusCode == 404)
                {
                    return NotFound("The specified category was not found.");
                }

                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return Ok(serviceResult.Data);
        }


        // POST: api/Categories
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await _categoryService.CreateCategoryAsync(category);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetCategory), new { id = serviceResult.Data.CategoryID }, serviceResult.Data);
        }

        // PUT: api/Categories/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryID)
            {
                return BadRequest("The ID does not match.");
            }

            var serviceResult = await _categoryService.UpdateCategoryAsync(id, category);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }

        // DELETE: api/Categories/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var serviceResult = await _categoryService.DeleteCategoryAsync(id);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }
    }
}
