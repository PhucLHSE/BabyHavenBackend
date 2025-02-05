using BabyHaven.Common;
using BabyHaven.Common.DTOs.UserAccountDTOs;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using BabyHaven.Services.Services;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IConfiguration _config;
        private readonly IUserAccountService _userAccountsService;
        private readonly IJwtTokenService _jwtTokenService;
        public UserAccountsController(IConfiguration config, IUserAccountService userAccountsService, IJwtTokenService jwtTokenService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginReqeust request)
        {
            var user = await _userAccountsService.Authenticate(request.Email, request.Password);

            if (user == null)
                return Unauthorized();

            var token = _jwtTokenService.GenerateJSONWebToken(user);

            return Ok(token);
        }

        public sealed record LoginReqeust(string Email, string Password);
    }
}
