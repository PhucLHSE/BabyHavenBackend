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
                new("ProfileImage", profilePicture)
            },
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
