using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;

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

        // GET api/<ConsultationResponsesController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> GetById(int id)
        {
            return await _consultationResponseService.GetById(id);
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
