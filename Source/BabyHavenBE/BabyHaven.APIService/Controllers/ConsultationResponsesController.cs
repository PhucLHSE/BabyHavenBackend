using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.ConsultationResponseDTOs;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationResponsesController : ControllerBase
    {
        private readonly IConsultationResponseService _consultationResponseService;

        public ConsultationResponsesController(IConsultationResponseService consultationResponseService)
            => _consultationResponseService = consultationResponseService;

        // GET: api/<ConsultationResponsesController>
        [HttpGet]
        public async Task<IServiceResult> GetAll()
        {
            return await _consultationResponseService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<ConsultationResponseViewAllDto>> GetForOData()
        {
            return await _consultationResponseService.GetQueryable();
        }

        // GET api/<ConsultationResponsesController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> GetById(int id)
        {
            return await _consultationResponseService.GetById(id);
        }

        [HttpGet("member/{id}")]
        public async Task<IServiceResult> GetByMemberId(Guid id)
        {
            return await _consultationResponseService.GetByMemberId(id);
        }

        // POST api/<ConsultationRequestsController>
        [HttpPost]
        public async Task<IServiceResult> Create(ConsultationResponseCreateDto consultationResponseCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _consultationResponseService.Create(consultationResponseCreateDto);
        }

        // DELETE api/<ConsultationResponsesController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> DeleteById(int id)
        {
            return await _consultationResponseService.DeleteById(id);
        }

        private bool ConsultationResponseExists(int id)
        {
            return _consultationResponseService.GetById(id) != null;
        }
    }
}
