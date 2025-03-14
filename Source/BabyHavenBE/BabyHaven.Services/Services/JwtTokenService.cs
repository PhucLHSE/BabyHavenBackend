using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using BabyHaven.Repositories.Models;
using BabyHaven.Services.IServices;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _config;

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJSONPaymentToken(Transaction transaction)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            new Claim[]
            {
            new Claim("Status", transaction.PaymentStatus)
            },
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public string GenerateJSONWebToken(UserAccount userAccount)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var profilePicture = Convert.ToBase64String(userAccount.ProfilePicture ?? new byte[0]);

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            new Claim[]
            {
                new(ClaimTypes.Name, userAccount.Name),
                new(ClaimTypes.NameIdentifier, userAccount.UserId.ToString()),
                new(ClaimTypes.Role, userAccount.RoleId.ToString()),
                new(ClaimTypes.Email, userAccount.Email),
                new("ProfileImage", profilePicture),
                new("IsVerified", userAccount.IsVerified.ToString()) // Use a custom claim type for IsVerified
            },
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
