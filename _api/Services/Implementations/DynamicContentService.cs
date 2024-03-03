namespace api_sylvainbreton.Services.Implementations

{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Exceptions;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services.Implementations.Helpers;
    using api_sylvainbreton.Services.Interfaces;
    using api_sylvainbreton.Services.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DynamicContentService(SylvainBretonDbContext context, ILogger<DynamicContentService> logger) : IDynamicContentService
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<DynamicContentService> _logger = logger;

        public async Task<IServiceResult<IEnumerable<DynamicContent>>> GetAllDynamicContentsAsync()
        {
            try
            {
                var dynamicContents = await _context.DynamicContents.AsNoTracking().ToListAsync();
                return new ServiceResult<IEnumerable<DynamicContent>>(true, dynamicContents, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all dynamic contents");
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }

        public async Task<IServiceResult<DynamicContent>> GetDynamicContentByIdAsync(int id)
        {
            try
            {
                var dynamicContent = await _context.DynamicContents.FindAsync(id);
                if (dynamicContent == null)
                {
                    return new ServiceResult<DynamicContent>(false, null, "Dynamic content not found");
                }

                return new ServiceResult<DynamicContent>(true, dynamicContent, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dynamic content with ID {Id}", id);
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }

        public async Task<IServiceResult<DynamicContent>> CreateDynamicContentAsync(DynamicContent dynamicContent)
        {
            try
            {
                _context.DynamicContents.Add(dynamicContent);
                await _context.SaveChangesAsync();

                return new ServiceResult<DynamicContent>(true, dynamicContent, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating dynamic content");
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }

        public async Task<IServiceResult<DynamicContent>> UpdateDynamicContentAsync(int id, DynamicContent dynamicContent)
        {
            if (id != dynamicContent.ContentID)
            {
                return new ServiceResult<DynamicContent>(false, null, "ID mismatch");
            }

            _context.Entry(dynamicContent).State = EntityState.Modified;

            try
            {
                if (!await DynamicContentHelper.DynamicContentExists(_context, id))
                {
                    return new ServiceResult<DynamicContent>(false, null, "Dynamic content not found");
                }

                await _context.SaveChangesAsync();
                return new ServiceResult<DynamicContent>(true, dynamicContent, null);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Error updating dynamic content with ID {Id}", id);
                throw new InternalServerErrorException("An error occurred while updating. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating dynamic content with ID {Id}", id);
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }

        public async Task<IServiceResult<DynamicContent>> DeleteDynamicContentAsync(int id)
        {
            try
            {
                var dynamicContent = await _context.DynamicContents.FindAsync(id);
                if (dynamicContent == null)
                {
                    return new ServiceResult<DynamicContent>(false, null, "Dynamic content not found");
                }

                _context.DynamicContents.Remove(dynamicContent);
                await _context.SaveChangesAsync();

                return new ServiceResult<DynamicContent>(true, dynamicContent, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting dynamic content with ID {Id}", id);
                throw new InternalServerErrorException("An error occurred while processing your request. Please try again later.");
            }
        }
    }
}
