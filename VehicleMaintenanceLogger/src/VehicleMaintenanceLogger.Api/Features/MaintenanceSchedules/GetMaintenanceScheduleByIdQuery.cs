using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.MaintenanceSchedules;

public record GetMaintenanceScheduleByIdQuery : IRequest<MaintenanceScheduleDto?>
{
    public Guid MaintenanceScheduleId { get; init; }
}

public class GetMaintenanceScheduleByIdQueryHandler : IRequestHandler<GetMaintenanceScheduleByIdQuery, MaintenanceScheduleDto?>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<GetMaintenanceScheduleByIdQueryHandler> _logger;

    public GetMaintenanceScheduleByIdQueryHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<GetMaintenanceScheduleByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaintenanceScheduleDto?> Handle(GetMaintenanceScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting maintenance schedule {MaintenanceScheduleId}", request.MaintenanceScheduleId);

        var schedule = await _context.MaintenanceSchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MaintenanceScheduleId == request.MaintenanceScheduleId, cancellationToken);

        if (schedule == null)
        {
            _logger.LogWarning("Maintenance schedule {MaintenanceScheduleId} not found", request.MaintenanceScheduleId);
            return null;
        }

        return schedule.ToDto();
    }
}
