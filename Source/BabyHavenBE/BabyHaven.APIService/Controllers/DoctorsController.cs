using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.DoctorDTOs;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {

            _doctorService = doctorService;
        }

        // GET: api/<DoctorController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _doctorService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<DoctorViewAllDto>> GetForOData()
        {
            return await _doctorService.GetQueryable();
        }

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _doctorService.GetById(id);
        }

        [HttpGet("doctor/{userId}")]
        public async Task<IServiceResult> GetByUserId(Guid userId)
        {
            return await _doctorService.GetByUserId(userId);
        }

        // POST api/<DoctorController>/5
        [HttpPost]
        public async Task<IServiceResult> Create([FromBody] DoctorCreateDto dto)
        {
            if (!ModelState.IsValid)
                return new ServiceResult { Status = Const.ERROR_VALIDATION_CODE, 
                    Message = "Invalid model state." };

            return await _doctorService.Create(dto);
        }


        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(DoctorUpdateDto userDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed", 
                    ModelState);
            }

            return await _doctorService.Update(userDto);
        }

        // DELETE api/<DoctorController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(int id)
        {
            return await _doctorService.DeleteById(id);
        }

        private bool DoctorExists(int id)
        {
            return _doctorService.GetById(id) != null;
        }
    }
}
