using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.ConsultationRequestDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationRequestsController : ControllerBase
    {
        private readonly IConsultationRequestService _consultationRequestService;

        public ConsultationRequestsController(IConsultationRequestService consultationRequestService)
            => _consultationRequestService = consultationRequestService;

        // GET: api/<ConsultationRequestsController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _consultationRequestService.GetAll();
        }

        // GET api/<ConsultationRequestsController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _consultationRequestService.GetById(id);
        }

        // POST api/<ConsultationRequestsController>
        [HttpPost]
        public async Task<IServiceResult> Post(ConsultationRequestCreateDto consultationRequestCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _consultationRequestService.Create(consultationRequestCreateDto);
        }
    }
}
