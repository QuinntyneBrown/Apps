using WeeklyReviewSystem.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WeeklyReviewSystem.Api.Features.Roles;

public record GetRolesQuery : IRequest<IEnumerable<RoleDto>> { }

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleDto>>
{
    private readonly IWeeklyReviewSystemContext _context;
    private readonly ILogger<GetRolesQueryHandler> _logger;

    public GetRolesQueryHandler(IWeeklyReviewSystemContext context, ILogger<GetRolesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all roles");
        var roles = await _context.Roles.AsNoTracking().OrderBy(r => r.Name).ToListAsync(cancellationToken);
        return roles.Select(r => r.ToDto());
    }
}
