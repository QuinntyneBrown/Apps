using DailyJournalingApp.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Roles;

public record DeleteRoleCommand : IRequest<bool>
{
    public Guid RoleId { get; init; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<DeleteRoleCommandHandler> _logger;

    public DeleteRoleCommandHandler(IDailyJournalingAppContext context, ILogger<DeleteRoleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting role {RoleId}", request.RoleId);
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) return false;
        var userRoles = await _context.UserRoles.Where(ur => ur.RoleId == request.RoleId).ToListAsync(cancellationToken);
        foreach (var userRole in userRoles) _context.UserRoles.Remove(userRole);
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
