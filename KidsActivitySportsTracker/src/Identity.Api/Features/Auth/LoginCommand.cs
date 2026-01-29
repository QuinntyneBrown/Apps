using Identity.Core;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Auth;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse?>;

public record LoginResponse(string Token, DateTime Expiration, Guid UserId, string UserName);

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse?>
{
    private readonly IIdentityDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IIdentityDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        ILogger<LoginCommandHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null)
        {
            _logger.LogWarning("Login failed: user not found for {Email}", request.Email);
            return null;
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password, user.Salt))
        {
            _logger.LogWarning("Login failed: invalid password for {Email}", request.Email);
            return null;
        }

        var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
        var roles = await _context.Roles
            .Where(r => roleIds.Contains(r.RoleId))
            .ToListAsync(cancellationToken);

        var token = _jwtTokenService.GenerateToken(user, roles);
        var expiration = _jwtTokenService.GetTokenExpiration();

        _logger.LogInformation("User {UserId} logged in successfully", user.UserId);

        return new LoginResponse(token, expiration, user.UserId, user.UserName);
    }
}
