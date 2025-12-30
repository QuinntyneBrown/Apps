using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.Vehicles;

public record GetVehicleByIdQuery : IRequest<VehicleDto?>
{
    public Guid VehicleId { get; init; }
}

public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleDto?>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<GetVehicleByIdQueryHandler> _logger;

    public GetVehicleByIdQueryHandler(
        IVehicleValueTrackerContext context,
        ILogger<GetVehicleByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<VehicleDto?> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vehicle {VehicleId}", request.VehicleId);

        var vehicle = await _context.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.VehicleId == request.VehicleId, cancellationToken);

        if (vehicle == null)
        {
            _logger.LogWarning("Vehicle {VehicleId} not found", request.VehicleId);
            return null;
        }

        return vehicle.ToDto();
    }
}
