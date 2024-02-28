namespace api_sylvainbreton.Services.Interfaces
{
    using api_sylvainbreton.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IServiceResult<IEnumerable<Category>>> GetAllCategoriesAsync();
        Task<IServiceResult<Category>> GetCategoryByIdAsync(int id);
        Task<IServiceResult<Category>> CreateCategoryAsync(Category category);
        Task<IServiceResult<Category>> UpdateCategoryAsync(int id, Category category);
        Task<IServiceResult<Category>> DeleteCategoryAsync(int id);
    }
}
