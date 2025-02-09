using BabyHaven.Common.DTOs.AlertDTOS;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
        }

        /// <summary>
        /// Gets all alerts.
        /// </summary>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _alertService.GetAll();
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Gets an alert by ID.
        /// </summary>
        /// <param name="alertId">The ID of the alert to retrieve.</param>
        /// <returns>The result of the retrieval operation.</returns>
        [HttpGet("{alertId}")]
        public async Task<IActionResult> GetById(int alertId)
        {
            var result = await _alertService.GetById(alertId);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Creates a new alert.
        /// </summary>
        /// <param name="dto">The alert creation DTO.</param>
        /// <returns>The result of the creation operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlertCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _alertService.Create(dto);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Updates an existing alert.
        /// </summary>
        /// <param name="dto">The alert update DTO.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AlertUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _alertService.Update(dto);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Deletes an alert by ID.
        /// </summary>
        /// <param name="alertId">The ID of the alert to delete.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("{alertId}")]
        public async Task<IActionResult> Delete(int alertId)
        {
            var result = await _alertService.Delete(alertId);
            return StatusCode(result.Status, result);
        }
    }
}
