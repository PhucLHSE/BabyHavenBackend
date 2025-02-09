using BabyHaven.Common;
using BabyHaven.Common.DTOs.DoctorSpecializationDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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

        // GET api/<DoctorSpecializationsController>/5/3
        [HttpGet("{specializationId}/{doctorId}")]
        public async Task<IServiceResult> Get(int specializationId, int doctorId)
        {
            return await _doctorSpecializationService.GetById(specializationId, doctorId);
        }

        // POST api/<DoctorSpecializationsController>
        [HttpPost]
        public async Task<IServiceResult> Post(DoctorSpecializationCreateDto doctorSpecializationCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _doctorSpecializationService.Create(doctorSpecializationCreateDto);
        }

        // PUT api/<DoctorSpecializationsController>/5/3
        [HttpPut("{specializationId}/{doctorId}")]
        public async Task<IServiceResult> Put(DoctorSpecializationUpdateDto doctorSpecializationUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _doctorSpecializationService.Update(doctorSpecializationUpdateDto);
        }

        // DELETE api/<DoctorSpecializationsController>/5/3
        [HttpDelete("{specializationId}/{doctorId}")]
        public async Task<IServiceResult> Delete(int specializationId, int doctorId)
        {
            return await _doctorSpecializationService.DeleteById(specializationId, doctorId);
        }

        private bool DoctorSpecializationExists(int packageId, int featuredId)
        {
            return _doctorSpecializationService.GetById(packageId, featuredId) != null;
        }
    }
}
