using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FriendGroupEventCoordinator.Api.Features.Users;

public record AddRoleToUserCommand : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
}

public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, UserDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;
    private readonly ILogger<AddRoleToUserCommandHandler> _logger;

    public AddRoleToUserCommandHandler(IFriendGroupEventCoordinatorContext context, ILogger<AddRoleToUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding role {RoleId} to user {UserId}", request.RoleId, request.UserId);
        var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) throw new InvalidOperationException($"Role with ID '{request.RoleId}' not found.");
        user.AddRole(role);
        await _context.SaveChangesAsync(cancellationToken);
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(roles);
    }
}
