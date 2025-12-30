using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.GearChecklists;

public record DeleteGearChecklistCommand : IRequest<bool>
{
    public Guid GearChecklistId { get; init; }
}

public class DeleteGearChecklistCommandHandler : IRequestHandler<DeleteGearChecklistCommand, bool>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<DeleteGearChecklistCommandHandler> _logger;

    public DeleteGearChecklistCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<DeleteGearChecklistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteGearChecklistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting gear checklist {GearChecklistId}", request.GearChecklistId);

        var gearChecklist = await _context.GearChecklists
            .FirstOrDefaultAsync(g => g.GearChecklistId == request.GearChecklistId, cancellationToken);

        if (gearChecklist == null)
        {
            _logger.LogWarning("Gear checklist {GearChecklistId} not found", request.GearChecklistId);
            return false;
        }

        _context.GearChecklists.Remove(gearChecklist);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted gear checklist {GearChecklistId}", request.GearChecklistId);

        return true;
    }
}
