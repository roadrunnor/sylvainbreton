namespace api_sylvainbreton.Services.Interfaces
{
    using api_sylvainbreton.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDynamicContentService
    {
        Task<IServiceResult<IEnumerable<DynamicContent>>> GetAllDynamicContentsAsync();
        Task<IServiceResult<DynamicContent>> GetDynamicContentByIdAsync(int id);
        Task<IServiceResult<DynamicContent>> CreateDynamicContentAsync(DynamicContent dynamicContent);
        Task<IServiceResult<DynamicContent>> UpdateDynamicContentAsync(int id, DynamicContent dynamicContent);
        Task<IServiceResult<DynamicContent>> DeleteDynamicContentAsync(int id);
    }
}
