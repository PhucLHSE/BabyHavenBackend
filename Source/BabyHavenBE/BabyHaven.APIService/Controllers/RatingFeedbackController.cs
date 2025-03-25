using BabyHaven.Common;
using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using BabyHaven.Common.DTOs.RatingFeedbackDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingFeedbackController : ControllerBase
    {
        private readonly IRatingFeedbackService _ratingFeedbackService;

        public RatingFeedbackController(IRatingFeedbackService ratingFeedbackService)
            => _ratingFeedbackService = ratingFeedbackService;

        // GET: api/<RatingFeedbackController>
        [HttpGet]
        public async Task<IServiceResult> GetAll()
        {
            return await _ratingFeedbackService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<RatingFeedbackViewAllDto>> GetForOData()
        {
            return await _ratingFeedbackService.GetQueryable();
        }

        // GET api/<RatingFeedbackController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> GetById(int id)
        {
            return await _ratingFeedbackService.GetById(id);
        }

        // POST api/<RatingFeedbackController>
        [HttpPost]
        public async Task<IServiceResult> Post([FromBody] RatingFeedbackCreateDto dto)
        {
            return await _ratingFeedbackService.Create(dto);
        }

        // PUT api/<RatingFeedbackController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(int id, [FromBody] RatingFeedbackUpdateDto dto)
        {
            if (id != dto.FeedbackId)
            {
                return new ServiceResult(Const.FAIL_UPDATE_CODE, "Feedback ID mismatch.");
            }
            return await _ratingFeedbackService.Update(dto);
        }

        // DELETE api/<RatingFeedbackController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> DeleteById(int id)
        {
            return await _ratingFeedbackService.DeleteById(id);
        }

        private bool RatingFeedbackExists(int id)
        {
            return _ratingFeedbackService.GetById(id) != null;
        }
    }
}
