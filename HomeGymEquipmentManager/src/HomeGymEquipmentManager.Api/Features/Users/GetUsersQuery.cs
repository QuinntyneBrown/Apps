using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeGymEquipmentManager.Api.Features.Users;

public record GetUsersQuery : IRequest<IEnumerable<UserDto>> { }

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IHomeGymEquipmentManagerContext _context;
    private readonly ILogger<GetUsersQueryHandler> _logger;

    public GetUsersQueryHandler(IHomeGymEquipmentManagerContext context, ILogger<GetUsersQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all users");
        var users = await _context.Users.Include(u => u.UserRoles).AsNoTracking().OrderBy(u => u.UserName).ToListAsync(cancellationToken);
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return users.Select(u => u.ToDto(roles));
    }
}
