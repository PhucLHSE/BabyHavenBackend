using BabyHaven.Repositories.Models;
using BabyHaven.Services.IServices;
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

        public AuthenticationController(IConfiguration config, IUserAccountService userAccountsService, IJwtTokenService jwtTokenService)
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

        public sealed record LoginReqeust(string Email, string Password);

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Check if the email already exists
            var existingUser = await _userAccountsService.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "Email already exists." });
            }

            // Map request to a UserAccount model
            var userAccount = new UserAccount
            {
                UserId = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                Password = request.Password, // Ideally, you should hash the password
                RegistrationDate = DateTime.UtcNow,
                Status = "Active",
                RoleId = request.RoleId,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Save to the database
            var result = await _userAccountsService.CreateAsync(userAccount);
            if (result)
            {
                return Ok(new { message = "User registered successfully." });
            }

            return BadRequest(new { message = "Failed to register user." });
        }

        // DTO for register request
        public sealed record RegisterRequest(
            string Username,
            string Email,
            string PhoneNumber,
            string Name,
            string Gender,
            DateOnly DateOfBirth,
            string Address,
            string Password,
            int RoleId
        );

    }
}
