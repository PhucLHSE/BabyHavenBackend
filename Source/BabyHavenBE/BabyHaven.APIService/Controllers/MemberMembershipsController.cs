using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberMembershipsController : ControllerBase
    {
        private readonly IMemberMembershipService _membershipService;

        public MemberMembershipsController(IMemberMembershipService membershipService)
            => _membershipService = membershipService;

        // GET: api/<MemberMembershipsController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _membershipService.GetAll();
        }
    }
}
