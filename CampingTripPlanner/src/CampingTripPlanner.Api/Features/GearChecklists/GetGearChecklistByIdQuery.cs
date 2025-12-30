using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.GearChecklists;

public record GetGearChecklistByIdQuery : IRequest<GearChecklistDto?>
{
    public Guid GearChecklistId { get; init; }
}

public class GetGearChecklistByIdQueryHandler : IRequestHandler<GetGearChecklistByIdQuery, GearChecklistDto?>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<GetGearChecklistByIdQueryHandler> _logger;

    public GetGearChecklistByIdQueryHandler(
        ICampingTripPlannerContext context,
        ILogger<GetGearChecklistByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GearChecklistDto?> Handle(GetGearChecklistByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gear checklist {GearChecklistId}", request.GearChecklistId);

        var gearChecklist = await _context.GearChecklists
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GearChecklistId == request.GearChecklistId, cancellationToken);

        if (gearChecklist == null)
        {
            _logger.LogWarning("Gear checklist {GearChecklistId} not found", request.GearChecklistId);
            return null;
        }

        return gearChecklist.ToDto();
    }
}
