using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record UpdateUserCommand : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
}

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
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null) return null;

        user.UpdateProfile(request.UserName, request.Email);
        await _context.SaveChangesAsync(cancellationToken);

        var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
        var roles = await _context.Roles.Where(r => roleIds.Contains(r.RoleId)).ToDictionaryAsync(r => r.RoleId, cancellationToken);

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = user.UserRoles
                .Where(ur => roles.ContainsKey(ur.RoleId))
                .Select(ur => new RoleInfo
                {
                    RoleId = ur.RoleId,
                    Name = roles[ur.RoleId].Name
                }).ToList()
        };
    }
}
