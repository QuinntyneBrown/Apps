using Itineraries.Core;
using Itineraries.Core.Models;
using MediatR;

namespace Itineraries.Api.Features;

public record CreateItineraryCommand : IRequest<ItineraryDto>
{
    public Guid TenantId { get; init; }
    public Guid TripId { get; init; }
    public DateTime Date { get; init; }
    public string? Activity { get; init; }
    public string? Location { get; init; }
}

public class CreateItineraryCommandHandler : IRequestHandler<CreateItineraryCommand, ItineraryDto>
{
    private readonly IItinerariesDbContext _context;

    public CreateItineraryCommandHandler(IItinerariesDbContext context)
    {
        _context = context;
    }

    public async Task<ItineraryDto> Handle(CreateItineraryCommand request, CancellationToken cancellationToken)
    {
        var itinerary = new Itinerary
        {
            ItineraryId = Guid.NewGuid(),
            TenantId = request.TenantId,
            TripId = request.TripId,
            Date = request.Date,
            Activity = request.Activity,
            Location = request.Location,
            CreatedAt = DateTime.UtcNow
        };

        _context.Itineraries.Add(itinerary);
        await _context.SaveChangesAsync(cancellationToken);

        return new ItineraryDto
        {
            ItineraryId = itinerary.ItineraryId,
            TripId = itinerary.TripId,
            Date = itinerary.Date,
            Activity = itinerary.Activity,
            Location = itinerary.Location,
            CreatedAt = itinerary.CreatedAt
        };
    }
}
