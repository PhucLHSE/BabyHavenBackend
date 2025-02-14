using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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
    }
}
