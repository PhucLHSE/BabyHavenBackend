using BabyHaven.Common;
using BabyHaven.Common.DTOs.GrowthRecordDTOs;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BabyHaven.APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrowthRecordController : ControllerBase
    {
        private readonly IGrowthRecordService _growthRecordService;

        public GrowthRecordController(IGrowthRecordService growthRecordService)
        {
            _growthRecordService = growthRecordService ?? throw new ArgumentNullException(nameof(growthRecordService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrowthRecord([FromBody] GrowthRecordRequiredDto dto)
        {
            var result = await _growthRecordService.CreateGrowthRecordRequired(dto);
            if (result.Status == Const.SUCCESS_CREATE_CODE)
            {
                return Ok(result);
            }
            return StatusCode(result.Status, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(GrowthRecordUpdateDto dto)
        {
            var result = await _growthRecordService.UpdateGrowthRecord(dto);
            if (result.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return Ok(result);
            }
            return StatusCode(result.Status, result);
        }

        [HttpDelete("{recordId}")]
        public async Task<IActionResult> DeleteGrowthRecord(int recordId)
        {
            var result = await _growthRecordService.DeleteGrowthRecord(recordId);
            if (result.Status == Const.SUCCESS_DELETE_CODE)
            {
                return Ok(result);
            }
            return StatusCode(result.Status, result);
        }

        [HttpGet("child/{childId}")]
        public async Task<IActionResult> GetAllGrowthRecordsByChild(Guid childId)
        {
            var result = await _growthRecordService.GetAllGrowthRecordsByChild(childId);
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                return Ok(result);
            }
            return StatusCode(result.Status, result);
        }

        [HttpGet("{recordId}/child/{childId}")]
        public async Task<IActionResult> GetGrowthRecordById(int recordId, Guid childId)
        {
            var result = await _growthRecordService.GetGrowthRecordById(recordId, childId);
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                return Ok(result);
            }
            return StatusCode(result.Status, result);
        }

        [HttpGet("child/{childId}/date-range")]
        public async Task<IActionResult> GetRecordsByDateRange(Guid childId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _growthRecordService.GetRecordsByDateRangeAsync(childId, startDate, endDate);
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                return Ok(result);
            }
            return StatusCode(result.Status, result);
        }
    }
}
