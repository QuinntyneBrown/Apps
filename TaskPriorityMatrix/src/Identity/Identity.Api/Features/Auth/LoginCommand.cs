using Identity.Core;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Auth;

public record LoginCommand(string UserName, string Password, Guid TenantId) : IRequest<LoginResponse>;

public record LoginResponse(string Token, DateTime Expiration, Guid UserId, string UserName, string Email);

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
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

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == request.UserName && u.TenantId == request.TenantId, cancellationToken);

        if (user == null)
        {
            _logger.LogWarning("Login failed: user {UserName} not found", request.UserName);
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password, user.Salt))
        {
            _logger.LogWarning("Login failed: invalid password for user {UserName}", request.UserName);
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var userRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == user.UserId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken);

        var roles = await _context.Roles
            .Where(r => userRoleIds.Contains(r.RoleId))
            .ToListAsync(cancellationToken);

        var token = _jwtTokenService.GenerateToken(user, roles);
        var expiration = _jwtTokenService.GetTokenExpiration();

        _logger.LogInformation("User {UserName} logged in successfully", request.UserName);

        return new LoginResponse(token, expiration, user.UserId, user.UserName, user.Email);
    }
}
