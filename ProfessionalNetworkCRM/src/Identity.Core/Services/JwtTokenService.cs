using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Core.Models.UserAggregate;
using Identity.Core.Models.UserAggregate.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Core.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    public JwtTokenService(IConfiguration configuration) { _configuration = configuration; }
    public string GenerateToken(User user, IEnumerable<Role> roles)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], expires: GetTokenExpiration(), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public DateTime GetTokenExpiration() => DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpirationInMinutes"] ?? "60"));
}
