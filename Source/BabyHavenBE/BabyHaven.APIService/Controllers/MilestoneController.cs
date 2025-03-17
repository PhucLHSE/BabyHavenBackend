using BabyHaven.Common;
using BabyHaven.Common.DTOs.MilestoneDTOS;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BabyHaven.APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MilestoneController : ControllerBase
    {
        private readonly IMilestoneService _milestoneService;

        public MilestoneController(IMilestoneService milestoneService)
        {
            _milestoneService = milestoneService 
                ?? throw new ArgumentNullException(nameof(milestoneService));
        }

        /// <summary>
        /// Gets all milestones.
        /// </summary>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _milestoneService.GetAll();

            return StatusCode(result.Status, result);
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<MilestoneViewAllDto>> GetForOData()
        {
            return await _milestoneService.GetQueryable();
        }

        /// <summary>
        /// Gets a milestone by ID.
        /// </summary>
        /// <param name="id">The ID of the milestone to retrieve.</param>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _milestoneService.GetById(id);

            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Creates a new milestone.
        /// </summary>
        /// <param name="dto">The milestone creation DTO.</param>
        /// <returns>The result of the creation operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MilestoneCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _milestoneService.Create(dto);

            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Updates an existing milestone.
        /// </summary>
        /// <param name="id">The ID of the milestone to update.</param>
        /// <param name="dto">The milestone update DTO.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MilestoneUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _milestoneService.Update(dto);

            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Deletes a milestone by ID.
        /// </summary>
        /// <param name="id">The ID of the milestone to delete.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _milestoneService.Delete(id);

            return StatusCode(result.Status, result);
        }
    }
}
