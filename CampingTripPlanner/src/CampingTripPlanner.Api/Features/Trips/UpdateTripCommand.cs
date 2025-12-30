using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Trips;

public record UpdateTripCommand : IRequest<TripDto?>
{
    public Guid TripId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid? CampsiteId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int NumberOfPeople { get; init; }
    public string? Notes { get; init; }
}

public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, TripDto?>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<UpdateTripCommandHandler> _logger;

    public UpdateTripCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<UpdateTripCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TripDto?> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating trip {TripId}", request.TripId);

        var trip = await _context.Trips
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            _logger.LogWarning("Trip {TripId} not found", request.TripId);
            return null;
        }

        trip.Name = request.Name;
        trip.CampsiteId = request.CampsiteId;
        trip.StartDate = request.StartDate;
        trip.EndDate = request.EndDate;
        trip.NumberOfPeople = request.NumberOfPeople;
        trip.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated trip {TripId}", request.TripId);

        return trip.ToDto();
    }
}
