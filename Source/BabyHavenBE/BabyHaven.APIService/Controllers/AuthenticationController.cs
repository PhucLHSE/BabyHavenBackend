using Azure.Core;
using BabyHaven.Common;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.Base;
using BabyHaven.Services.IServices;
using Microsoft.AspNetCore.Identity.Data;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserAccountService _userAccountsService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAuthService _authService;

        public AuthenticationController(IConfiguration config, IUserAccountService userAccountsService, IJwtTokenService jwtTokenService, IAuthService authService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
            _jwtTokenService = jwtTokenService;
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IServiceResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userAccountsService.Authenticate(request.Email, request.Password);

            if (user == null)
                return new ServiceResult(Const.ERROR_VALIDATION_CODE, "Unauthorized");

            var token = _jwtTokenService.GenerateJSONWebToken(user);

            return new ServiceResult(Const.SUCCESS_READ_CODE, "Login successful", token);
        }

        //private string GenerateJSONWebToken(UserAccount userAccount)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"]
        //            , _config["Jwt:Audience"]
        //            , new Claim[]
        //            {
        //        new(ClaimTypes.Name, userAccount.Email),
        //        //new(ClaimTypes.Email, systemUserAccount.Email),
        //        new(ClaimTypes.Role, userAccount.RoleId.ToString()),
        //            },
        //            expires: DateTime.Now.AddMinutes(120),
        //            signingCredentials: credentials
        //        );

        //    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        //    return tokenString;
        //}

        public sealed record LoginRequest(string Email, string Password);

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Check if the email already exists
            var existingUser = await _userAccountsService.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "Email already exists." });
            }
            var otpSent = await _authService.SendOtpAsync(request.Email);
            if (!otpSent)
            {
                return BadRequest(new { message = "Failed to send OTP." });
            }

            return Ok(new { message = "OTP sent. Please verify your email." });
        }

        [HttpPost("VerifyRegistrationOtp")]
        public async Task<IActionResult> VerifyRegistrationOtp([FromBody] VerifyOtpRequest request, string otp)
        {

            var isValid = await _authService.VerifyOtpAsync(request.Email, otp);
            if (!isValid)
            {
                return BadRequest(new { message = "Invalid OTP." });
            }

            // Map request to a UserAccount model
            var userAccount = new UserAccount
            {
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                Gender = request.Gender,
                DateOfBirth = DateOnly.Parse(request.DateOfBirth),
                Address = request.Address,
                Password = request.Password, // Ideally, you should hash the password
                RegistrationDate = DateTime.UtcNow,
                Status = "Active",
                IsVerified = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RoleId = 1
            };

            // Save to the database
            var result = await _userAccountsService.CreateAsync(userAccount);
            if (result)
            {
                return Ok(new { message = "User registered successfully." });
            }

            return BadRequest(new { message = "Failed to register user." });
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _userAccountsService.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound(new { message = "Email not found." });
            }

            var otpSent = await _authService.SendResetPasswordOtpAsync(request.Email);
            if (!otpSent)
            {
                return BadRequest(new { message = "Failed to send OTP." });
            }

            return Ok(new { message = "OTP sent to email." });
        }

        [HttpPost("VerifyResetPasswordOtp")]
        public async Task<IActionResult> VerifyResetPasswordOtp([FromBody] VerifyOtpForPasswordRequest request)
        {
            var (isValid, resetToken) = await _authService.VerifyResetPasswordOtpWithTokenAsync(request.Email, request.Otp);
            if (!isValid)
            {
                return BadRequest(new { message = "Invalid OTP." });
            }
            return Ok(new { message = "OTP verified successfully.", ResetToken = resetToken });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordWithTokenRequest request)
        {
            var result = await _authService.ResetPasswordWithTokenAsync(request.ResetToken, request.NewPassword);
            if (result)
            {
                return Ok(new { message = "Password reset successfully." });
            }
            return BadRequest(new { message = "Invalid or expired reset token." });
        }

        //[HttpPost("VerifyOtpAndResetPassword")]
        //public async Task<IActionResult> VerifyOtpAndResetPassword([FromBody] ResetPasswordWithOtpRequest request)
        //{
        //    var isValid = await _authService.SendResetPasswordOtpAsync(request.Email);
        //    if (!isValid)
        //    {
        //        return BadRequest(new { message = "Invalid OTP." });
        //    }

        //    var result = await _authService.ResetPasswordAsync(request.Email, request.Otp, request.NewPassword);
        //    if (result)
        //    {
        //        return Ok(new { message = "Password reset successfully." });
        //    }

        //    return BadRequest(new { message = "Failed to reset password." });
        //}

        // DTO for register request
        public sealed record RegisterRequest(
            string Email
        );
        public sealed record VerifyOtpRequest(
            string Email,
            string Username,
            string PhoneNumber,
            string Name,
            string Gender,
            string DateOfBirth,
            string Address,
            string Password
        );
        public sealed record ResetPasswordRequest(
            string Email
        );
        public sealed record ResetPasswordWithOtpRequest(
            string Email,
            string NewPassword
        );
        public sealed record VerifyOtpForPasswordRequest(
            string Email,
            string Otp
        );
        public sealed record ResetPasswordWithTokenRequest(
            string ResetToken,
            string NewPassword
        );
    }
}
