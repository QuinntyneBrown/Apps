using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.Vehicles;

public record GetVehiclesQuery : IRequest<IEnumerable<VehicleDto>>
{
    public string? Make { get; init; }
    public string? Model { get; init; }
    public int? Year { get; init; }
    public bool? IsCurrentlyOwned { get; init; }
}

public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IEnumerable<VehicleDto>>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<GetVehiclesQueryHandler> _logger;

    public GetVehiclesQueryHandler(
        IVehicleValueTrackerContext context,
        ILogger<GetVehiclesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<VehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vehicles");

        var query = _context.Vehicles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Make))
        {
            query = query.Where(v => v.Make.Contains(request.Make));
        }

        if (!string.IsNullOrWhiteSpace(request.Model))
        {
            query = query.Where(v => v.Model.Contains(request.Model));
        }

        if (request.Year.HasValue)
        {
            query = query.Where(v => v.Year == request.Year.Value);
        }

        if (request.IsCurrentlyOwned.HasValue)
        {
            query = query.Where(v => v.IsCurrentlyOwned == request.IsCurrentlyOwned.Value);
        }

        var vehicles = await query
            .OrderByDescending(v => v.Year)
            .ThenBy(v => v.Make)
            .ThenBy(v => v.Model)
            .ToListAsync(cancellationToken);

        return vehicles.Select(v => v.ToDto());
    }
}
