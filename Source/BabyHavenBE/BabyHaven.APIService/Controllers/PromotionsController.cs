﻿using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.PromotionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService)
            => _promotionService = promotionService;

        // GET: api/<PromotionsController>
        [HttpGet]
        public async Task<IServiceResult> GetAll()
        {
            return await _promotionService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<PromotionViewAllDto>> GetForOData()
        {
            return await _promotionService.GetQueryable();
        }

        // GET api/<PromotionsController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> GetById(Guid id)
        {
            return await _promotionService.GetById(id);
        }

        // POST api/<PromotionsController>
        [HttpPost]
        public async Task<IServiceResult> Create(PromotionCreateDto promotionCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed", 
                    ModelState);
            }

            return await _promotionService.Create(promotionCreateDto);
        }

        // PUT api/<PromotionsController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Update(PromotionUpdateDto promotionUpdateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed", 
                    ModelState);
            }

            return await _promotionService.Update(promotionUpdateDto);
        }

        // DELETE api/<PromotionsController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> DeleteById(Guid id)
        {
            return await _promotionService.DeleteById(id);
        }

        private bool PromotionExists(Guid id)
        {
            return _promotionService.GetById(id) != null;
        }
    }
}
