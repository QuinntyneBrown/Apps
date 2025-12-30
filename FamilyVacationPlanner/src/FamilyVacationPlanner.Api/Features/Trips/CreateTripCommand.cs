using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.Trips;

public record CreateTripCommand : IRequest<TripDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Destination { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDto>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<CreateTripCommandHandler> _logger;

    public CreateTripCommandHandler(
        IFamilyVacationPlannerContext context,
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
            Destination = request.Destination,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
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
