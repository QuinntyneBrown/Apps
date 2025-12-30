using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Itineraries;

public record UpdateItineraryCommand : IRequest<ItineraryDto?>
{
    public Guid ItineraryId { get; init; }
    public DateTime Date { get; init; }
    public string? Activity { get; init; }
    public string? Location { get; init; }
}

public class UpdateItineraryCommandHandler : IRequestHandler<UpdateItineraryCommand, ItineraryDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<UpdateItineraryCommandHandler> _logger;

    public UpdateItineraryCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<UpdateItineraryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ItineraryDto?> Handle(UpdateItineraryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating itinerary {ItineraryId}", request.ItineraryId);

        var itinerary = await _context.Itineraries
            .FirstOrDefaultAsync(i => i.ItineraryId == request.ItineraryId, cancellationToken);

        if (itinerary == null)
        {
            _logger.LogWarning("Itinerary {ItineraryId} not found", request.ItineraryId);
            return null;
        }

        itinerary.Date = request.Date;
        itinerary.Activity = request.Activity;
        itinerary.Location = request.Location;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated itinerary {ItineraryId}", request.ItineraryId);

        return itinerary.ToDto();
    }
}
