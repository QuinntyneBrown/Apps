using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Models.HouseholdAggregate.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Households;

public record UpdateHouseholdCommand : IRequest<HouseholdDto?>
{
    public Guid HouseholdId { get; init; }
    public string? Name { get; init; }
    public string? Street { get; init; }
    public string? City { get; init; }
    public CanadianProvince? Province { get; init; }
    public string? PostalCode { get; init; }
}

public class UpdateHouseholdCommandHandler : IRequestHandler<UpdateHouseholdCommand, HouseholdDto?>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<UpdateHouseholdCommandHandler> _logger;

    public UpdateHouseholdCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<UpdateHouseholdCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HouseholdDto?> Handle(UpdateHouseholdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating household {HouseholdId}", request.HouseholdId);

        var household = await _context.Households
            .FirstOrDefaultAsync(h => h.HouseholdId == request.HouseholdId, cancellationToken);

        if (household == null)
        {
            _logger.LogWarning("Household {HouseholdId} not found", request.HouseholdId);
            return null;
        }

        household.Update(
            request.Name,
            request.Street,
            request.City,
            request.Province,
            request.PostalCode);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated household {HouseholdId}", request.HouseholdId);

        return household.ToDto();
    }
}
