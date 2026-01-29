using Identity.Core;
using Identity.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Auth;

public record LoginCommand : IRequest<LoginResponse?>
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Guid TenantId { get; init; }
}

public record LoginResponse
{
    public string Token { get; init; } = string.Empty;
    public DateTime Expiration { get; init; }
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
}

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
            .FirstOrDefaultAsync(u => u.UserName == request.UserName && u.TenantId == request.TenantId, cancellationToken);

        if (user == null)
        {
            _logger.LogWarning("Login failed: User not found - {UserName}", request.UserName);
            return null;
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password, user.Salt))
        {
            _logger.LogWarning("Login failed: Invalid password - {UserName}", request.UserName);
            return null;
        }

        var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
        var roles = await _context.Roles.Where(r => roleIds.Contains(r.RoleId)).ToListAsync(cancellationToken);

        var token = _jwtTokenService.GenerateToken(user, roles);
        var expiration = _jwtTokenService.GetTokenExpiration();

        _logger.LogInformation("Login successful: {UserName}", request.UserName);

        return new LoginResponse
        {
            Token = token,
            Expiration = expiration,
            UserId = user.UserId,
            UserName = user.UserName
        };
    }
}
