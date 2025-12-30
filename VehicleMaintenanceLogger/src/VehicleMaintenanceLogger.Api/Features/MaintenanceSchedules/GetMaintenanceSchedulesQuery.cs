using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.MaintenanceSchedules;

public record GetMaintenanceSchedulesQuery : IRequest<IEnumerable<MaintenanceScheduleDto>>
{
    public Guid? VehicleId { get; init; }
    public ServiceType? ServiceType { get; init; }
    public bool? IsActive { get; init; }
    public bool? IsDue { get; init; }
    public decimal? CurrentMileage { get; init; }
}

public class GetMaintenanceSchedulesQueryHandler : IRequestHandler<GetMaintenanceSchedulesQuery, IEnumerable<MaintenanceScheduleDto>>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<GetMaintenanceSchedulesQueryHandler> _logger;

    public GetMaintenanceSchedulesQueryHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<GetMaintenanceSchedulesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MaintenanceScheduleDto>> Handle(GetMaintenanceSchedulesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting maintenance schedules for vehicle {VehicleId}", request.VehicleId);

        var query = _context.MaintenanceSchedules.AsNoTracking();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(m => m.VehicleId == request.VehicleId.Value);
        }

        if (request.ServiceType.HasValue)
        {
            query = query.Where(m => m.ServiceType == request.ServiceType.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(m => m.IsActive == request.IsActive.Value);
        }

        var schedules = await query
            .OrderBy(m => m.NextServiceDate)
            .ThenBy(m => m.NextServiceMileage)
            .ToListAsync(cancellationToken);

        // Filter by IsDue if requested
        if (request.IsDue.HasValue && request.IsDue.Value)
        {
            var currentDate = DateTime.UtcNow;
            var currentMileage = request.CurrentMileage ?? 0;

            schedules = schedules
                .Where(s => s.IsDue(currentMileage, currentDate))
                .ToList();
        }

        return schedules.Select(m => m.ToDto());
    }
}
