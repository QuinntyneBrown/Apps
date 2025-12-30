using CampingTripPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Trips;

public record CreateTripCommand : IRequest<TripDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid? CampsiteId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int NumberOfPeople { get; init; }
    public string? Notes { get; init; }
}

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDto>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<CreateTripCommandHandler> _logger;

    public CreateTripCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<CreateTripCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TripDto> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating trip for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            CampsiteId = request.CampsiteId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            NumberOfPeople = request.NumberOfPeople,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created trip {TripId} for user {UserId}",
            trip.TripId,
            request.UserId);

        return trip.ToDto();
    }
}
