using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record GetUserByIdQuery : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IIdentityDbContext _context;

    public GetUserByIdQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null) return null;

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
