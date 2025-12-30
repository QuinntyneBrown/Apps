using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.UserAggregate;
using FamilyCalendarEventPlanner.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Users;

public record CreateUserCommand : IRequest<UserDto>
{
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public List<Guid>? RoleIds { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        IPasswordHasher passwordHasher,
        ILogger<CreateUserCommandHandler> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating user with username: {UserName}", request.UserName);

        // Check for existing username
        var existingByUsername = await _context.Users
            .AnyAsync(u => u.UserName == request.UserName, cancellationToken);
        if (existingByUsername)
        {
            throw new InvalidOperationException($"Username '{request.UserName}' is already taken.");
        }

        // Check for existing email
        var existingByEmail = await _context.Users
            .AnyAsync(u => u.Email == request.Email, cancellationToken);
        if (existingByEmail)
        {
            throw new InvalidOperationException($"Email '{request.Email}' is already registered.");
        }

        var (hashedPassword, salt) = _passwordHasher.HashPassword(request.Password);

        var user = new User(
            userName: request.UserName,
            email: request.Email,
            hashedPassword: hashedPassword,
            salt: salt);

        // Add roles if specified
        if (request.RoleIds != null && request.RoleIds.Any())
        {
            var roles = await _context.Roles
                .Where(r => request.RoleIds.Contains(r.RoleId))
                .ToListAsync(cancellationToken);

            foreach (var role in roles)
            {
                user.AddRole(role);
            }
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created user {UserId} with username: {UserName}", user.UserId, request.UserName);

        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(allRoles);
    }
}
