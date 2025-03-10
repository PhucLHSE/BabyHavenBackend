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
        public async Task<IServiceResult> GetAll()
        {
            return await _packageFeatureService.GetAll();
        }

        // GET api/<PackageFeaturesController>/5/3
        [HttpGet("{packageId}/{featureId}")]
        public async Task<IServiceResult> GetByIds(int packageId, int featureId)
        {
            return await _packageFeatureService.GetById(packageId, featureId);
        }

        // POST api/<PackageFeaturesController>
        [HttpPost]
        public async Task<IServiceResult> Create(PackageFeatureCreateDto packageFeatureCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _packageFeatureService.Create(packageFeatureCreateDto);
        }

        // PUT api/<PackageFeaturesController>/5/3
        [HttpPut("{packageId}/{featureId}")]
        public async Task<IServiceResult> Update(PackageFeatureUpdateDto packageFeatureUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _packageFeatureService.Update(packageFeatureUpdateDto);
        }

        // DELETE api/<FeaturesController>/5/3
        [HttpDelete("{packageId}/{featuredId}")]
        public async Task<IServiceResult> DeleteByIds(int packageId, int featuredId)
        {
            return await _packageFeatureService.DeleteById(packageId, featuredId);
        }

        private bool PackageFeatureExists(int packageId, int featuredId)
        {
            return _packageFeatureService.GetById(packageId, featuredId) != null;
        }
    }
}
