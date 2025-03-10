using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.SpecializationDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationsController(ISpecializationService specializationService)
            => _specializationService = specializationService;
        // GET: api/<SpecializationController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _specializationService.GetAll();
        }

        // GET api/<SpecializationController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _specializationService.GetById(id);
        }

        // POST api/<SpecializationController>
        [HttpPost]
        public async Task<IServiceResult> Post(SpecializationCreateDto specializationCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.
                    ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _specializationService.Create(specializationCreateDto);
        }

        // PUT api/<SpecializationController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(SpecializationUpdateDto specializationUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.
                    ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _specializationService.Update(specializationUpdateDto);
        }

        // DELETE api/<SpecializationController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(int id)
        {
            return await _specializationService.DeleteById(id);
        }

        private bool SpecializationExists(int id)
        {
            return _specializationService.GetById(id) != null;
        }
    }
}
