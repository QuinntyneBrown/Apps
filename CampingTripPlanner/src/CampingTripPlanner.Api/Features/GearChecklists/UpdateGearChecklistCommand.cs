using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.GearChecklists;

public record UpdateGearChecklistCommand : IRequest<GearChecklistDto?>
{
    public Guid GearChecklistId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public bool IsPacked { get; init; }
    public int Quantity { get; init; }
    public string? Notes { get; init; }
}

public class UpdateGearChecklistCommandHandler : IRequestHandler<UpdateGearChecklistCommand, GearChecklistDto?>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<UpdateGearChecklistCommandHandler> _logger;

    public UpdateGearChecklistCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<UpdateGearChecklistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GearChecklistDto?> Handle(UpdateGearChecklistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating gear checklist {GearChecklistId}", request.GearChecklistId);

        var gearChecklist = await _context.GearChecklists
            .FirstOrDefaultAsync(g => g.GearChecklistId == request.GearChecklistId, cancellationToken);

        if (gearChecklist == null)
        {
            _logger.LogWarning("Gear checklist {GearChecklistId} not found", request.GearChecklistId);
            return null;
        }

        gearChecklist.ItemName = request.ItemName;
        gearChecklist.IsPacked = request.IsPacked;
        gearChecklist.Quantity = request.Quantity;
        gearChecklist.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated gear checklist {GearChecklistId}", request.GearChecklistId);

        return gearChecklist.ToDto();
    }
}
