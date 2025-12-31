using WoodworkingProjectManager.Core;
using WoodworkingProjectManager.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Auth;

public record LoginCommand : IRequest<LoginResult?>
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public record LoginResult
{
    public string Token { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
    public UserInfo User { get; init; } = null!;
}

public record UserInfo
{
    public Guid UserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public List<string> Roles { get; init; } = new();
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult?>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IWoodworkingProjectManagerContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        ILogger<LoginCommandHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    public async Task<LoginResult?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for username: {Username}", request.Username);

        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

        if (user == null)
        {
            _logger.LogWarning("Login failed: User not found for username: {Username}", request.Username);
            return null;
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password, user.Salt))
        {
            _logger.LogWarning("Login failed: Invalid password for username: {Username}", request.Username);
            return null;
        }

        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        var userRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToHashSet();
        var userRoles = allRoles.Where(r => userRoleIds.Contains(r.RoleId)).ToList();

        var token = _jwtTokenService.GenerateToken(user, userRoles);
        var expiresAt = _jwtTokenService.GetTokenExpiration();

        _logger.LogInformation("Login successful for username: {Username}", request.Username);

        return new LoginResult
        {
            Token = token,
            ExpiresAt = expiresAt,
            User = new UserInfo
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles.Select(r => r.Name).ToList()
            }
        };
    }
}
