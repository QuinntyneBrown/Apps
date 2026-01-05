using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FriendGroupEventCoordinator.Core.Models.UserAggregate;
using FriendGroupEventCoordinator.Core.Models.UserAggregate.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FriendGroupEventCoordinator.Core.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly int _expiresInHours;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _expiresInHours = configuration.GetValue<int>("Jwt:ExpiresInHours", 24);
    }

    public string GenerateToken(User user, IEnumerable<Role> roles)
    {
        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT Key is not configured.");
        var issuer = _configuration["Jwt:Issuer"]
            ?? "FriendGroupEventCoordinator";
        var audience = _configuration["Jwt:Audience"]
            ?? "FriendGroupEventCoordinator.Api";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: GetTokenExpiration(),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public DateTime GetTokenExpiration()
    {
        return DateTime.UtcNow.AddHours(_expiresInHours);
    }
}
