using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
