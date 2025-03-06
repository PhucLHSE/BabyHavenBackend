using BabyHaven.Common;
using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

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

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<MembershipPackageViewAllDto>> GetForOData()
        {
            return await _membershipPackageService.GetQueryable();
        }

        // GET api/<MembershipPackagesController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(int id)
        {
            return await _membershipPackageService.GetById(id);
        }

        // POST api/<MembershipPackagesController>
        [HttpPost]
        public async Task<IServiceResult> Post(MembershipPackageCreateDto membershipPackageCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _membershipPackageService.Create(membershipPackageCreateDto);
        }

        // PUT api/<MembershipPackagesController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(MembershipPackageUpdateDto membershipPackageUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Validation failed", ModelState);
            }

            return await _membershipPackageService.Update(membershipPackageUpdateDto);
        }

        // DELETE api/<MembershipPackagesController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(int id)
        {
            return await _membershipPackageService.DeleteById(id);
        }

        private bool MembershipPackageExists(int id)
        {
            return _membershipPackageService.GetById(id) != null;
        }
    }
}
