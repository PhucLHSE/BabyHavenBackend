﻿using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.PackagePromotionDTOs;

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
        public async Task<IServiceResult> Get()
        {
            return await _packagePromotionsService.GetAll();
        }

        // GET api/<PackagePromotionsController>/5/3
        [HttpGet("{packageId}/{promotionId}")]
        public async Task<IServiceResult> Get(int packageId, Guid promotionId)
        {
            return await _packagePromotionsService.GetById(packageId, promotionId);
        }

        // POST api/<PackagePromotionsController>
        [HttpPost]
        public async Task<IServiceResult> Post(PackagePromotionCreateDto packagePromotionCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed",
                    ModelState);
            }

            return await _packagePromotionsService.Create(packagePromotionCreateDto);
        }
    }
}
