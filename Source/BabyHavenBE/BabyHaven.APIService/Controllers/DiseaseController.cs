using BabyHaven.Common;
using BabyHaven.Common.DTOs.DiseaseDTOs;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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
                    return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
                }
                return await _diseaseService.Save(diseaseCreateDto);
            }
        }
}
