using Identity.Core;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Auth;

public record LoginCommand(string Username, string Password) : IRequest<LoginResult?>;

public record LoginResult(string Token, DateTime Expiration, Guid UserId, string Username);

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
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

        if (user == null)
            return null;

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password, user.Salt))
            return null;

        var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
        var roles = await _context.Roles
            .Where(r => roleIds.Contains(r.RoleId))
            .ToListAsync(cancellationToken);

        var token = _jwtTokenService.GenerateToken(user, roles);
        var expiration = _jwtTokenService.GetTokenExpiration();

        return new LoginResult(token, expiration, user.UserId, user.UserName);
    }
}
