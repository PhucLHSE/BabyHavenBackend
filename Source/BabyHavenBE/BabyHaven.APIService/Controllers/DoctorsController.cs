using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.DoctorDTOs;

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

        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _doctorService.GetById(id);
        }

        // PUT api/<DoctorController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(DoctorUpdateDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
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
