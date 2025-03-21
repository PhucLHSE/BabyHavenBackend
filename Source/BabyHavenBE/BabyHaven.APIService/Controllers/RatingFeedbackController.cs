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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RatingFeedbackController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RatingFeedbackController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RatingFeedbackController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
