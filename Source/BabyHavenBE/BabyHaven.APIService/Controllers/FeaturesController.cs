﻿using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.FeatureDTOs;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
            => _featureService = featureService;


        // GET: api/<FeaturesController>
        [HttpGet]
        public async Task<IServiceResult> GetAll()
        {
            return await _featureService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<FeatureViewAllDto>> GetForOData()
        {
            return await _featureService.GetQueryable();
        }

        // GET api/<FeaturesController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> GetById(int id)
        {
            return await _featureService.GetById(id);
        }

        // POST api/<FeaturesController>
        [HttpPost]
        public async Task<IServiceResult> Create(FeatureCreateDto featureCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _featureService.Create(featureCreateDto);
        }

        // PUT api/<FeaturesController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Update(FeatureUpdateDto featureUpdateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed", 
                    ModelState);
            }

            return await _featureService.Update(featureUpdateDto);
        }

        // DELETE api/<FeaturesController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> DeleteById(int id)
        {
            return await _featureService.DeleteById(id);
        }

        private bool FeatureExists(int id)
        {
            return _featureService.GetById(id) != null;
        }
    }
}
