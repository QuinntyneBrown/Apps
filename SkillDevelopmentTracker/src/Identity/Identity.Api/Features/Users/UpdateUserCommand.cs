using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record UpdateUserCommand(Guid UserId, Guid TenantId, string? UserName, string? Email) : IRequest<UserDto?>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IIdentityDbContext _context;

    public UpdateUserCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserId == request.UserId && u.TenantId == request.TenantId, cancellationToken);

        if (user == null) return null;

        user.UpdateProfile(request.UserName, request.Email);
        await _context.SaveChangesAsync(cancellationToken);

        var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
        var roles = await _context.Roles.Where(r => roleIds.Contains(r.RoleId)).Select(r => r.Name).ToListAsync(cancellationToken);

        return new UserDto(user.UserId, user.TenantId, user.UserName, user.Email, roles);
    }
}
