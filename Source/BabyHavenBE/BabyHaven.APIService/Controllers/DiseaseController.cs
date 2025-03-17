using BabyHaven.Common;
using BabyHaven.Common.DTOs.DiseaseDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly IDiseaseService _diseaseService;

        public DiseasesController(IDiseaseService diseaseService)
            => _diseaseService = diseaseService;

        // GET: api/<DiseaseController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _diseaseService.GetAll();
        }

        // GET api/<DiseaseController>/id
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _diseaseService.GetById(id);
        }

        // POST api/<DiseaseController>
        [HttpPost]
        public async Task<IServiceResult> Post(DiseaseCreateDto diseaseCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed",
                    ModelState);
            }
            return await _diseaseService.Create(diseaseCreateDto);
        }

        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(int id)
        {
            return await _diseaseService.DeleteById(id);
        }

        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(int id, DiseaseUpdateDto diseaseUpdateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed", 
                    ModelState);
            }
            return await _diseaseService.UpdateById(id, diseaseUpdateDto);
        }

        [HttpPut("predelete/{id}")]
        public async Task<IServiceResult> PreDelete(int id)
        {
            var result = await _diseaseService.PreDeleteById(id);

            return result;
        }

        [HttpPut("recover/{id}")]
        public async Task<IServiceResult> Recover(int id)
        {
            var result = await _diseaseService.RecoverById(id);

            return result;
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<DiseaseViewAllDto>> GetForOData()
        {
            return await _diseaseService.GetQueryable();
        }
    }
}
