using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipPackagesController : ControllerBase
    {
        private readonly IMembershipPackageService _membershipPackageService;

        public MembershipPackagesController(IMembershipPackageService membershipPackageService)
            => _membershipPackageService = membershipPackageService;

        // GET: api/<MembershipPackagesController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _membershipPackageService.GetAll();
        }
    }
}
