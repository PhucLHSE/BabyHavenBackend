using BabyHaven.Common.DTOs.UserAccountDTOs;
using BabyHaven.Common;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Mappers;

namespace BabyHaven.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserAccountService _userAccountService;
        private readonly IJwtTokenService _jwtTokenService;

        public GoogleAuthController(IConfiguration config, IUserAccountService userAccountService, IJwtTokenService jwtTokenService)
        {
            _config = config;
            _userAccountService = userAccountService;
            _jwtTokenService = jwtTokenService;
        }

        // 1. Trả về URL để Frontend tự redirect đến Google OAuth hoặc redirect trực tiếp nếu từ browser
        [HttpGet("signin-google")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "GoogleAuth", null, Request.Scheme);

            // Kiểm tra nếu yêu cầu đến từ browser
            if (HttpContext.Request.Headers["Accept"].ToString().Contains("text/html"))
            {
                var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
                return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            }

            // Trả về URL cho frontend
            return Ok(new { url = redirectUrl });
        }

        // 2. Xử lý phản hồi từ Google sau khi xác thực thành công
        [HttpGet("signin-google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return BadRequest("Google authentication failed!");

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            });

            // Kiểm tra nếu user đã tồn tại hoặc tạo tài khoản mới
            var loginGoogleDto = new LoginGoogleDto
            {
                Email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                ProfilePictureUrl = claims?.FirstOrDefault(c => c.Type == "picture")?.Value,
                RoleId = 3
            };

            var serviceResult = await _userAccountService.AuthenticateWithGoogle(loginGoogleDto);

            if (serviceResult.Status != Const.SUCCESS_LOGIN_CODE)
            {
                return Unauthorized(serviceResult);
            }

            var user = serviceResult.Data as UserAccount;
            if (user == null)
            {
                return Unauthorized();
            }
            
            var token = _jwtTokenService.GenerateJSONWebToken(user);
            var userDto = user.MapToUserAccountViewAllDto();
            
            return Ok(new { token, userDto });
        }

        [HttpPost("signout-google")]
        public async Task<IActionResult> SignOutGoogle()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Google Sign-Out successful" });
        }
    }
}
