using Identity.Core;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Auth;

public record LoginCommand : IRequest<LoginResult?>
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public record LoginResult
{
    public string Token { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
    public Guid UserId { get; init; }
    public string Username { get; init; } = string.Empty;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult?>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(
        IIdentityDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResult?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

        if (user == null)
            return null;

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password, user.Salt))
            return null;

        var userRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken);

        var roles = await _context.Roles
            .Where(r => userRoleIds.Contains(r.RoleId))
            .ToListAsync(cancellationToken);

        var token = _jwtTokenService.GenerateToken(user, roles);

        return new LoginResult
        {
            Token = token,
            ExpiresAt = _jwtTokenService.GetTokenExpiration(),
            UserId = user.UserId,
            Username = user.UserName
        };
    }
}
