﻿using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.PackageFeatureDTOs;
using BabyHaven.Common.DTOs.VNPayDTOS.PackagePromotionDTOs;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagePromotionsController : ControllerBase
    {
        private readonly IPackagePromotionService _packagePromotionsService;

        public PackagePromotionsController(IPackagePromotionService promotionsService)
            => _packagePromotionsService = promotionsService;

        // GET: api/<PackagePromotionsController>
        [HttpGet]
        public async Task<IServiceResult> GetAll()
        {
            return await _packagePromotionsService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<PackagePromotionViewAllDto>> GetForOData()
        {
            return await _packagePromotionsService.GetQueryable();
        }

        // GET api/<PackagePromotionsController>/5/3
        [HttpGet("{packageId}/{promotionId}")]
        public async Task<IServiceResult> GetByIds(int packageId, Guid promotionId)
        {
            return await _packagePromotionsService.GetById(packageId, promotionId);
        }

        // POST api/<PackagePromotionsController>
        [HttpPost]
        public async Task<IServiceResult> Create(PackagePromotionCreateDto packagePromotionCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed",
                    ModelState);
            }

            return await _packagePromotionsService.Create(packagePromotionCreateDto);
        }

        // PUT api/<PackagePromotionsController>/5/3
        [HttpPut("{packageId}/{promotionId}")]
        public async Task<IServiceResult> Update(PackagePromotionUpdateDto packagePromotionUpdateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed", 
                    ModelState);
            }

            return await _packagePromotionsService.Update(packagePromotionUpdateDto);
        }

        // DELETE api/<PackagePromotionsController>/5/3
        [HttpDelete("{packageId}/{promotionId}")]
        public async Task<IServiceResult> DeleteByIds(int packageId, Guid promotionId)
        {
            return await _packagePromotionsService.DeleteById(packageId, promotionId);
        }

        private bool PackagePromotionExists(int packageId, Guid promotionId)
        {
            return _packagePromotionsService.GetById(packageId, promotionId) != null;
        }
    }
}
