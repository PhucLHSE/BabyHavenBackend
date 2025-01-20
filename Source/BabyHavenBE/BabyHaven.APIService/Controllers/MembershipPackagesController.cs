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

        // GET api/<MembershipPackagesController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _membershipPackageService.GetById(id);
        }

        // POST api/<MembershipPackagesController>
        [HttpPost]
        public async Task<IServiceResult> Post(MembershipPackage membershipPackage)
        {
            return await _membershipPackageService.Save(membershipPackage);
        }
    }
}
