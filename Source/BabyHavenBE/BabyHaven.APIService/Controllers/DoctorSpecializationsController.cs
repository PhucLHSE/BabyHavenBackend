using BabyHaven.Common;
using BabyHaven.Common.DTOs.DoctorSpecializationDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSpecializationsController : ControllerBase
    {
        private readonly IDoctorSpecializationService _doctorSpecializationService;

        public DoctorSpecializationsController(IDoctorSpecializationService doctorSpecializationService)
            => _doctorSpecializationService = doctorSpecializationService;


        // GET: api/<DoctorSpecializationsController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _doctorSpecializationService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<DoctorSpecializationViewAllDto>> GetForOData()
        {
            return await _doctorSpecializationService.GetQueryable();
        }

        // GET api/<DoctorSpecializationsController>/5/3
        [HttpGet("{doctorSpecializationId}")]
        public async Task<IServiceResult> Get(int doctorSpecializationId)
        {
            return await _doctorSpecializationService.GetById(doctorSpecializationId);
        }

        // POST api/<DoctorSpecializationsController>
        [HttpPost]
        public async Task<IServiceResult> Post(DoctorSpecializationCreateDto doctorSpecializationCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _doctorSpecializationService.Create(doctorSpecializationCreateDto);
        }

        // PUT api/<DoctorSpecializationsController>/5/3
        [HttpPut("{doctorSpecializationId}")]
        public async Task<IServiceResult> Put(DoctorSpecializationUpdateDto doctorSpecializationUpdateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _doctorSpecializationService.Update(doctorSpecializationUpdateDto);
        }

        // DELETE api/<DoctorSpecializationsController>/5/3
        [HttpDelete("{doctorSpecializationId}")]
        public async Task<IServiceResult> Delete(int doctorSpecializationId)
        {
            return await _doctorSpecializationService.DeleteById(doctorSpecializationId);
        }

        private bool DoctorSpecializationExists(int doctorSpecializationId)
        {
            return _doctorSpecializationService.GetById(doctorSpecializationId) != null;
        }
    }
}
