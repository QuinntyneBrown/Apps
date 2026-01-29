using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record AddRoleToUserCommand : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
}

public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, UserDto?>
{
    private readonly IIdentityDbContext _context;
    private readonly ILogger<AddRoleToUserCommandHandler> _logger;

    public AddRoleToUserCommandHandler(IIdentityDbContext context, ILogger<AddRoleToUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null) return null;

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found");
        }

        user.AddRole(role);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Role {RoleId} added to user {UserId}", request.RoleId, request.UserId);

        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = user.UserRoles
                .Select(ur => allRoles.FirstOrDefault(r => r.RoleId == ur.RoleId))
                .Where(r => r != null)
                .Select(r => new RoleInfo { RoleId = r!.RoleId, Name = r.Name })
                .ToList()
        };
    }
}
