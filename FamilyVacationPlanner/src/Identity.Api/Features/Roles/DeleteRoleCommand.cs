using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Roles;

public record DeleteRoleCommand : IRequest<bool>
{
    public Guid RoleId { get; init; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IIdentityDbContext _context;

    public DeleteRoleCommandHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);

        if (role == null) return false;

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
