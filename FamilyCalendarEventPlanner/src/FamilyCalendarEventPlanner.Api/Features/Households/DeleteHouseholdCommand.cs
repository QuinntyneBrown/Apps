using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Households;

public record DeleteHouseholdCommand : IRequest<bool>
{
    public Guid HouseholdId { get; init; }
}

public class DeleteHouseholdCommandHandler : IRequestHandler<DeleteHouseholdCommand, bool>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<DeleteHouseholdCommandHandler> _logger;

    public DeleteHouseholdCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<DeleteHouseholdCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteHouseholdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting household {HouseholdId}", request.HouseholdId);

        var household = await _context.Households
            .FirstOrDefaultAsync(h => h.HouseholdId == request.HouseholdId, cancellationToken);

        if (household == null)
        {
            _logger.LogWarning("Household {HouseholdId} not found", request.HouseholdId);
            return false;
        }

        _context.Households.Remove(household);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted household {HouseholdId}", request.HouseholdId);

        return true;
    }
}
