namespace api_sylvainbreton.Services.Implementations.Helpers
{
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public static class DynamicContentHelper
    {
        public static async Task<bool> DynamicContentExists(SylvainBretonDbContext context, int id)
        {
            return await context.DynamicContents.AnyAsync(e => e.ContentID == id);
        }
    }
}
