using System;
using System.Threading.Tasks;
using BabyHaven.Common;
using BabyHaven.Common.DTOs.AIChatDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrowthRecordAnalysisController : ControllerBase
    {
        private readonly IGrowthAnalysisService _growthAnalysisService;

        public GrowthRecordAnalysisController(IGrowthAnalysisService growthAnalysisService)
        {
            _growthAnalysisService = growthAnalysisService 
                ?? throw new ArgumentNullException(nameof(growthAnalysisService));
        }

        [HttpPost("analyze-growth-record")]
        public async Task<IActionResult> AnalyzeGrowthRecord([FromBody] GrowthRecordAnalysisDto record)
        {

            if (record == null)
            {

                return BadRequest(new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Invalid input data"));
            }

            var result = await _growthAnalysisService.AnalyzeGrowthRecord(record);

            if (result.Status == Const.SUCCESS_READ_CODE)
            {

                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("chat")]
        public async Task<IActionResult> ChatWithAI([FromBody] GrowthRecordChatRequest request)
        {
            var result = await _growthAnalysisService.ChatWithAI(request.SessionId, request.UserMessage, request?.InitialRecord);
            if (result.Status == Const.SUCCESS_READ_CODE)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
