using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.FamilyMembers;

public record GetFamilyMembersQuery : IRequest<IEnumerable<FamilyMemberDto>>
{
    public Guid? FamilyId { get; init; }
}

public class GetFamilyMembersQueryHandler : IRequestHandler<GetFamilyMembersQuery, IEnumerable<FamilyMemberDto>>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetFamilyMembersQueryHandler> _logger;

    public GetFamilyMembersQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetFamilyMembersQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<FamilyMemberDto>> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting family members for family {FamilyId}", request.FamilyId);

        var query = _context.FamilyMembers.AsNoTracking();

        if (request.FamilyId.HasValue)
        {
            query = query.Where(m => m.FamilyId == request.FamilyId.Value);
        }

        var members = await query
            .OrderBy(m => m.Name)
            .ToListAsync(cancellationToken);

        return members.Select(m => m.ToDto());
    }
}
