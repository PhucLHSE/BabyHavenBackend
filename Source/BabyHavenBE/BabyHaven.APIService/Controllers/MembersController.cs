using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.MemberDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
            => _memberService = memberService;

        // GET: api/<MembersController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _memberService.GetAll();
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(Guid id)
        {
            return await _memberService.GetById(id);
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(MemberUpdateDto memberUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _memberService.Update(memberUpdateDto);
        }
    }
}
