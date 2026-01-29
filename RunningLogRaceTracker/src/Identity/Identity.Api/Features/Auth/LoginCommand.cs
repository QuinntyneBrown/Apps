using Identity.Api.Controllers;
using Identity.Core;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Auth;

public record LoginCommand : IRequest<LoginResponse?>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse?>
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

    public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null) return null;

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password, user.Salt))
            return null;

        var roles = await _context.UserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Join(_context.Roles, ur => ur.RoleId, r => r.RoleId, (ur, r) => r.Name)
            .ToListAsync(cancellationToken);

        var token = _jwtTokenService.GenerateToken(user, roles);

        return new LoginResponse(token, user.UserId, user.UserName);
    }
}
