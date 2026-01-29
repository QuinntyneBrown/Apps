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
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(IIdentityDbContext context, ILogger<UpdateUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null) return null;

        user.UpdateProfile(request.UserName, request.Email);
        await _context.SaveChangesAsync(cancellationToken);

        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);

        _logger.LogInformation("User updated: {UserId}", user.UserId);

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
