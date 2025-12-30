using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Itineraries;

public record CreateItineraryCommand : IRequest<ItineraryDto>
{
    public Guid TripId { get; init; }
    public DateTime Date { get; init; }
    public string? Activity { get; init; }
    public string? Location { get; init; }
}

public class CreateItineraryCommandHandler : IRequestHandler<CreateItineraryCommand, ItineraryDto>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<CreateItineraryCommandHandler> _logger;

    public CreateItineraryCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<CreateItineraryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ItineraryDto> Handle(CreateItineraryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating itinerary for trip {TripId}, date: {Date}",
            request.TripId,
            request.Date);

        var itinerary = new Itinerary
        {
            ItineraryId = Guid.NewGuid(),
            TripId = request.TripId,
            Date = request.Date,
            Activity = request.Activity,
            Location = request.Location,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Itineraries.Add(itinerary);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created itinerary {ItineraryId} for trip {TripId}",
            itinerary.ItineraryId,
            request.TripId);

        return itinerary.ToDto();
    }
}
