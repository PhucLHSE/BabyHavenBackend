using BabyHaven.Common;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
using BabyHaven.Common.DTOs.MemberDTOs;
using Microsoft.AspNetCore.OData.Query;

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
        public async Task<IServiceResult> GetAll()
        {
            return await _memberService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<MemberViewAllDto>> GetForOData()
        {
            return await _memberService.GetQueryable();
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> GetById(Guid id)
        {
            return await _memberService.GetById(id);
        }

        // GET: api/<MembersController>/5
        [HttpGet("member/{id}")]
        public async Task<IServiceResult> GetByUserId(Guid id)
        {
            return await _memberService.GetByUserId(id);
        }

        // POST api/<MembersController>
        [HttpPost]
        public async Task<IServiceResult> Post(MemberCreateDto memberCreateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _memberService.Create(memberCreateDto);
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Update(MemberUpdateDto memberUpdateDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.ERROR_VALIDATION_CODE, 
                    "Validation failed", 
                    ModelState);
            }

            return await _memberService.Update(memberUpdateDto);
        }

        // DELETE api/<MembersController>/5/3
        [HttpDelete("{id}")]
        public async Task<IServiceResult> DeleteById(Guid id)
        {
            return await _memberService.DeleteById(id);
        }

        private bool MemberExists(Guid id)
        {
            return _memberService.GetById(id) != null;
        }
    }
}
