using BabyHaven.Common.DTOs.FeatureDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.PackageFeatureDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageFeaturesController : ControllerBase
    {
        private readonly IPackageFeatureService _packageFeatureService;

        public PackageFeaturesController(IPackageFeatureService packageFeatureService)
            => _packageFeatureService = packageFeatureService;


        // GET: api/<PackageFeaturesController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _packageFeatureService.GetAll();
        }

        // GET api/<PackageFeaturesController>/5/3
        [HttpGet("{PackageId}/{FeatureId}")]
        public async Task<IServiceResult> Get(int PackageId, int FeatureId)
        {
            return await _packageFeatureService.GetById(PackageId, FeatureId);
        }

        // POST api/<PackageFeaturesController>
        [HttpPost]
        public async Task<IServiceResult> Post(PackageFeatureCreateDto packageFeatureCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _packageFeatureService.Create(packageFeatureCreateDto);
        }
    }
}
