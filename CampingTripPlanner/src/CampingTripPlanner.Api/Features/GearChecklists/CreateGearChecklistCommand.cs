using CampingTripPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.GearChecklists;

public record CreateGearChecklistCommand : IRequest<GearChecklistDto>
{
    public Guid UserId { get; init; }
    public Guid TripId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public int Quantity { get; init; } = 1;
    public string? Notes { get; init; }
}

public class CreateGearChecklistCommandHandler : IRequestHandler<CreateGearChecklistCommand, GearChecklistDto>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<CreateGearChecklistCommandHandler> _logger;

    public CreateGearChecklistCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<CreateGearChecklistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GearChecklistDto> Handle(CreateGearChecklistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating gear checklist for user {UserId}, trip: {TripId}",
            request.UserId,
            request.TripId);

        var gearChecklist = new GearChecklist
        {
            GearChecklistId = Guid.NewGuid(),
            UserId = request.UserId,
            TripId = request.TripId,
            ItemName = request.ItemName,
            IsPacked = false,
            Quantity = request.Quantity,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.GearChecklists.Add(gearChecklist);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created gear checklist {GearChecklistId} for user {UserId}",
            gearChecklist.GearChecklistId,
            request.UserId);

        return gearChecklist.ToDto();
    }
}
