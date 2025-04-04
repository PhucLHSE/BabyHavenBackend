﻿using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.ConsultationRequestDTOs;
using Microsoft.AspNetCore.OData.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationRequestsController : ControllerBase
    {
        private readonly IConsultationRequestService _consultationRequestService;

        public ConsultationRequestsController(IConsultationRequestService consultationRequestService)
            => _consultationRequestService = consultationRequestService;

        // GET: api/<ConsultationRequestsController>
        [HttpGet]
        public async Task<IServiceResult> GetAll()
        {
            return await _consultationRequestService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<ConsultationRequestViewAllDto>> GetForOData()
        {
            return await _consultationRequestService.GetQueryable();
        }

        // GET api/<ConsultationRequestsController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> GetById(int id)
        {
            return await _consultationRequestService.GetById(id);
        }

        // POST api/<ConsultationRequestsController>
        [HttpPost]
        public async Task<IServiceResult> Create(ConsultationRequestCreateDto consultationRequestCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed", 
                    ModelState);
            }

            return await _consultationRequestService.Create(consultationRequestCreateDto);
        }

        [HttpPut("{requestId}/{status}")]
        public async Task<IServiceResult> UpdateStatus(int requestId, string status)
        {
            return await _consultationRequestService.UpdateStatus(requestId, status);
        }


        // DELETE api/<ConsultationRequestsController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> DeleteById(int id)
        {
            return await _consultationRequestService.DeleteById(id);
        }

        private bool ConsultationRequestExists(int id)
        {
            return _consultationRequestService.GetById(id) != null;
        }
    }
}
