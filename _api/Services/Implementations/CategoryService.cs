namespace api_sylvainbreton.Services.Implementations
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Exceptions;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService(SylvainBretonDbContext context, ILogger<CategoryService> logger) : ICategoryService
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<CategoryService> _logger = logger;

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.AsNoTracking().ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CategoryService: An error occurred while retrieving all categories.");
                throw new InternalServerErrorException("An error occurred while retrieving all categories.");
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryID == id);
            if (category == null)
            {
                _logger.LogWarning("CategoryService: Category with ID {Id} not found.", id);
                throw new NotFoundException($"Category with ID {id} not found.");
            }

            return category;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CategoryService: An error occurred while creating a new category.");
                throw new InternalServerErrorException("An error occurred while creating the category. Please try again later.");
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "CategoryService: A concurrency error occurred while updating the category with ID {Id}.", category.CategoryID);
                throw new InternalServerErrorException("CategoryService: A concurrency error occurred. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CategoryService: An error occurred while updating the category with ID {Id}.", category.CategoryID);
                throw new InternalServerErrorException("CategoryService: An error occurred while updating the category. Please try again later.");
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("CategoryService: Attempted to delete a category with ID {Id}, but it was not found.", id);
                    throw new NotFoundException($"CategoryService: Category with ID {id} not found.");
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CategoryService: An error occurred while deleting the category with ID {Id}.", id);
                throw new InternalServerErrorException("CategoryService: An error occurred while deleting the category. Please try again later.");
            }
        }
    }
}
