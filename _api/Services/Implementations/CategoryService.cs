namespace api_sylvainbreton.Services.Implementations
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Exceptions;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services.Interfaces;
    using api_sylvainbreton.Services.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService(SylvainBretonDbContext context, ILogger<CategoryService> logger) : ICategoryService
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<CategoryService> _logger = logger;

        public async Task<IServiceResult<IEnumerable<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.AsNoTracking().ToListAsync();
                return new ServiceResult<IEnumerable<Category>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all categories.");
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }

        public async Task<IServiceResult<Category>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryID == id);
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {Id} not found.", id);
                    return new ServiceResult<Category>($"Category with ID {id} not found.", 404);
                }

                return new ServiceResult<Category>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the category with ID {Id}.", id);
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }

        public async Task<IServiceResult<Category>> CreateCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return new ServiceResult<Category>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new category.");
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }

        public async Task<IServiceResult<Category>> UpdateCategoryAsync(int id, Category category)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(id);
                if (existingCategory == null)
                {
                    _logger.LogWarning("Category with ID {Id} not found.", id);

                    return new ServiceResult<Category>($"Category with ID {id} not found.", 404);
                }

                _context.Entry(existingCategory).CurrentValues.SetValues(category);
                await _context.SaveChangesAsync();
                return new ServiceResult<Category>(existingCategory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "A concurrency error occurred while updating the category with ID {Id}.", id);
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the category with ID {Id}.", id);
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }

        public async Task<IServiceResult<Category>> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                {
                    _logger.LogWarning("Category with ID {Id} not found.", id);
                    return new ServiceResult<Category>(string.Format("Category with ID {0} not found.", id), 404);
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return new ServiceResult<Category>(true, null, "The category has been successfully deleted.", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the category with ID {Id}.", id);
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }
    }
}
