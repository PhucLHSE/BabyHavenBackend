﻿using BabyHaven.Common.DTOs.PromotionDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.TransactionDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
            => _transactionService = transactionService;

        // GET: api/<TransactionsController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _transactionService.GetAll();
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(Guid id)
        {
            return await _transactionService.GetById(id);
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public async Task<IServiceResult> Post(TransactionCreateDto transactionCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _transactionService.Create(transactionCreateDto);
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(Guid id)
        {
            return await _transactionService.DeleteById(id);
        }

        private bool TransactionExists(Guid id)
        {
            return _transactionService.GetById(id) != null;
        }
    }
}
