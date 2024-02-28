namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class DynamicContentsController(IDynamicContentService dynamicContentService) : ControllerBase
    {
        private readonly IDynamicContentService _dynamicContentService = dynamicContentService;

        // GET: api/DynamicContents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DynamicContent>>> GetDynamicContents()
        {
            var serviceResult = await _dynamicContentService.GetAllDynamicContentsAsync();

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return Ok(serviceResult.Data);
        }

        // GET: api/DynamicContents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DynamicContent>> GetDynamicContent(int id)
        {
            var serviceResult = await _dynamicContentService.GetDynamicContentByIdAsync(id);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return Ok(serviceResult.Data);
        }

        // POST: api/DynamicContents
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DynamicContent>> PostDynamicContent([FromBody] DynamicContent DynamicContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await _dynamicContentService.CreateDynamicContentAsync(DynamicContent);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetDynamicContent), new { id = serviceResult.Data.ContentID }, serviceResult.Data);
        }

        // PUT: api/DynamicContents/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDynamicContent(int id, [FromBody] DynamicContent DynamicContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await _dynamicContentService.UpdateDynamicContentAsync(id, DynamicContent);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }

        // DELETE: api/DynamicContents/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDynamicContent(int id)
        {
            var serviceResult = await _dynamicContentService.DeleteDynamicContentAsync(id);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }
    }
}
