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

        [HttpPost("clear-chat")]
        public IActionResult ClearChat([FromBody] ClearChatRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.SessionId))
            {
                return BadRequest(new ServiceResult(Const.ERROR_EXCEPTION, "Invalid request. SessionId is required."));
            }

            try
            {
                _growthAnalysisService.ClearChatHistory(request.SessionId);
                return Ok(new ServiceResult(Const.SUCCESS_CREATE_CODE, "Chat history cleared successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResult(Const.ERROR_EXCEPTION, $"Error clearing chat history: {ex.Message}"));
            }
        }
    }
}
