using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Households;

public record GetHouseholdByIdQuery : IRequest<HouseholdDto?>
{
    public Guid HouseholdId { get; init; }
}

public class GetHouseholdByIdQueryHandler : IRequestHandler<GetHouseholdByIdQuery, HouseholdDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetHouseholdByIdQueryHandler> _logger;

    public GetHouseholdByIdQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetHouseholdByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HouseholdDto?> Handle(GetHouseholdByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting household {HouseholdId}", request.HouseholdId);

        var household = await _context.Households
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.HouseholdId == request.HouseholdId, cancellationToken);

        if (household == null)
        {
            _logger.LogWarning("Household {HouseholdId} not found", request.HouseholdId);
            return null;
        }

        return household.ToDto();
    }
}
