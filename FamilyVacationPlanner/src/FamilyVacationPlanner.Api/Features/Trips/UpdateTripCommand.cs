using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Trips;

public record UpdateTripCommand : IRequest<TripDto?>
{
    public Guid TripId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Destination { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, TripDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<UpdateTripCommandHandler> _logger;

    public UpdateTripCommandHandler(
        IFamilyVacationPlannerContext context,
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
        trip.Destination = request.Destination;
        trip.StartDate = request.StartDate;
        trip.EndDate = request.EndDate;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated trip {TripId}", request.TripId);

        return trip.ToDto();
    }
}
