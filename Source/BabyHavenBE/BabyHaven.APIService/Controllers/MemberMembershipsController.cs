using BabyHaven.Common.DTOs.PromotionDTOs;
using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.MemberMembershipDTOs;

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

        // GET api/<MemberMembershipsController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(Guid id)
        {
            return await _membershipService.GetByMemberId(id);
        }

        // POST api/<MemberMembershipsController>
        [HttpPost]
        public async Task<IServiceResult> Post(MemberMembershipCreateDto memberMembershipCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _membershipService.Create(memberMembershipCreateDto);
        }

        // PUT api/<MemberMembershipsController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(MemberMembershipUpdateDto memberMembershipUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _membershipService.Update(memberMembershipUpdateDto);
        }

        // DELETE api/<MemberMembershipsController>/5/3
        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(Guid id)
        {
            return await _membershipService.DeleteById(id);
        }

        private bool MemberMembershipExists(Guid id)
        {
            return _membershipService.GetById(id) != null;
        }
    }
}
