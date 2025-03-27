using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BmiPercentileController : ControllerBase
    {
        private readonly IBmiPercentileService _bmiPercentileService;

        public BmiPercentileController(IBmiPercentileService bmiPercentileService)
        {
            _bmiPercentileService = bmiPercentileService
                ?? throw new ArgumentNullException(nameof(bmiPercentileService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bmiPercentileService.GetAll();
            return StatusCode(result.Status, result);
        }

        [HttpGet("{age}/{gender}")]
        public async Task<IActionResult> GetByAgeAndGender(int age, string gender)
        {
            var result = await _bmiPercentileService.GetByAgeAndGender(age, gender);
            return StatusCode(result.Status, result);
        }

    }
}
