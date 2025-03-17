using BabyHaven.Common;
using BabyHaven.Common.DTOs.ChildMilestoneDTOs;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildMilestoneController : ControllerBase
    {
        private readonly IChildMilestoneService _childMilestoneService;

        public ChildMilestoneController(IChildMilestoneService childMilestoneService)
        {
            _childMilestoneService = childMilestoneService 
                ?? throw new ArgumentNullException(nameof(childMilestoneService));
        }

        /// <summary>
        /// Gets all child milestones.
        /// </summary>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _childMilestoneService.GetAll();

            return StatusCode(result.Status, result);
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<ChildMilestoneViewAllDto>> GetForOData()
        {
            return await _childMilestoneService.GetQueryable();
        }

        /// <summary>
        /// Gets a child milestone by child ID and milestone ID.
        /// </summary>
        /// <param name="childId">The ID of the child.</param>
        /// <param name="milestoneId">The ID of the milestone.</param>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet("{childId}/{milestoneId}")]
        public async Task<IActionResult> GetById(Guid childId, int milestoneId)
        {
            var result = await _childMilestoneService.GetById(childId, milestoneId);

            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Creates a new child milestone.
        /// </summary>
        /// <param name="childId">The ID of the child.</param>
        /// <param name="dto">The child milestone creation DTO.</param>
        /// <returns>The result of the creation operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChildMilestoneCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _childMilestoneService.Create(dto);

            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Updates an existing child milestone.
        /// </summary>
        /// <param name="childId">The ID of the child.</param>
        /// <param name="milestoneId">The ID of the milestone.</param>
        /// <param name="dto">The child milestone update DTO.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("{childId}/{milestoneId}")]
        public async Task<IActionResult> Update(Guid childId, int milestoneId, [FromBody] ChildMilestoneUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _childMilestoneService.Update(dto);

            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Deletes a child milestone by child ID and milestone ID.
        /// </summary>
        /// <param name="childId">The ID of the child.</param>
        /// <param name="milestoneId">The ID of the milestone.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("{childId}/{milestoneId}")]
        public async Task<IActionResult> Delete(Guid childId, int milestoneId)
        {
            var result = await _childMilestoneService.Delete(childId, milestoneId);

            return StatusCode(result.Status, result);
        }
    }
}
