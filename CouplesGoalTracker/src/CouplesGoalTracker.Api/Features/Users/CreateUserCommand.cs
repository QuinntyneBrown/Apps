using CouplesGoalTracker.Core;
using CouplesGoalTracker.Core.Model.UserAggregate;
using CouplesGoalTracker.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CouplesGoalTracker.Api.Features.Users;

public record CreateUserCommand : IRequest<UserDto>
{
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public List<Guid>? RoleIds { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly ICouplesGoalTrackerContext _context;
    private readonly ITenantContext _tenantContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(ICouplesGoalTrackerContext context, ITenantContext tenantContext, IPasswordHasher passwordHasher, ILogger<CreateUserCommandHandler> logger)
    {
        _context = context;
        _tenantContext = tenantContext;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating user with username: {UserName}", request.UserName);
        if (await _context.Users.AnyAsync(u => u.UserName == request.UserName, cancellationToken))
            throw new InvalidOperationException($"Username '{request.UserName}' is already taken.");
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            throw new InvalidOperationException($"Email '{request.Email}' is already registered.");

        var (hashedPassword, salt) = _passwordHasher.HashPassword(request.Password);
        var tenantId = _tenantContext.TenantId != Guid.Empty ? _tenantContext.TenantId : Constants.DefaultTenantId;
        var user = new User(tenantId, request.UserName, request.Email, hashedPassword, salt);

        if (request.RoleIds != null && request.RoleIds.Any())
        {
            var roles = await _context.Roles.Where(r => request.RoleIds.Contains(r.RoleId)).ToListAsync(cancellationToken);
            foreach (var role in roles) user.AddRole(role);
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Created user {UserId}", user.UserId);
        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(allRoles);
    }
}
