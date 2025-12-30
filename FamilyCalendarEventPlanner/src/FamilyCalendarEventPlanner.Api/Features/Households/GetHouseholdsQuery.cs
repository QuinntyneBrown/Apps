using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Households;

public record GetHouseholdsQuery : IRequest<IEnumerable<HouseholdDto>>
{
}

public class GetHouseholdsQueryHandler : IRequestHandler<GetHouseholdsQuery, IEnumerable<HouseholdDto>>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetHouseholdsQueryHandler> _logger;

    public GetHouseholdsQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetHouseholdsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<HouseholdDto>> Handle(GetHouseholdsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all households");

        var households = await _context.Households
            .AsNoTracking()
            .OrderBy(h => h.Name)
            .ToListAsync(cancellationToken);

        return households.Select(h => h.ToDto());
    }
}
