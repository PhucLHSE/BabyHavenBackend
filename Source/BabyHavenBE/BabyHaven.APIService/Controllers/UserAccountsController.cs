using BabyHaven.Common;
using BabyHaven.Common.DTOs.MembershipPackageDTOs;
using BabyHaven.Common.DTOs.UserAccountDTOs;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly IUserAccountService _userAccountsService;

        public UserAccountsController(IUserAccountService userAccountsService)
        {

            _userAccountsService = userAccountsService;
        }

        // GET: api/<UserAccountsController>
        [HttpGet]
        public async Task<IServiceResult> Get()
        {
            return await _userAccountsService.GetAll();
        }

        [HttpGet("odata")]
        [EnableQuery]
        public async Task<IQueryable<UserAccountViewAllDto>> GetForOData()
        {
            return await _userAccountsService.GetQueryable();
        }

        // GET api/<UserAccountsController>/5
        [HttpGet("{id}")]
        public async Task<IServiceResult> Get(Guid id)
        {
            return await _userAccountsService.GetById(id);
        }

        // POST api/<UserAccountsController>
        [HttpPost]
        public async Task<IServiceResult> Post(UserAccountCreateDto userDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.
                    ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _userAccountsService.Create(userDto);
        }

        // PUT api/<UserAccountsController>/5
        [HttpPut("{id}")]
        public async Task<IServiceResult> Put(UserAccountUpdateDto userDto)
        {
            if (!ModelState.IsValid)
            {

                return new ServiceResult(Const.
                    ERROR_VALIDATION_CODE,
                    "Validation failed",
                    ModelState);
            }

            return await _userAccountsService.Update(userDto);
        }

        // DELETE api/<UserAccountsController>/5
        [HttpDelete("{id}")]
        public async Task<IServiceResult> Delete(Guid id)
        {
            return await _userAccountsService.DeleteById(id);
        }

        private bool UserAccountExists(Guid id)
        {
            return _userAccountsService.GetById(id) != null;
        }
    }
}
