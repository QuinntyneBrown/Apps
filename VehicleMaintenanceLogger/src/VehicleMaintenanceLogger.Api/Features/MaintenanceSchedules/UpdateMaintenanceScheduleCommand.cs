using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.MaintenanceSchedules;

public record UpdateMaintenanceScheduleCommand : IRequest<MaintenanceScheduleDto?>
{
    public Guid MaintenanceScheduleId { get; init; }
    public ServiceType ServiceType { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal? MileageInterval { get; init; }
    public int? MonthsInterval { get; init; }
    public decimal? LastServiceMileage { get; init; }
    public DateTime? LastServiceDate { get; init; }
    public decimal? NextServiceMileage { get; init; }
    public DateTime? NextServiceDate { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
}

public class UpdateMaintenanceScheduleCommandHandler : IRequestHandler<UpdateMaintenanceScheduleCommand, MaintenanceScheduleDto?>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<UpdateMaintenanceScheduleCommandHandler> _logger;

    public UpdateMaintenanceScheduleCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<UpdateMaintenanceScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaintenanceScheduleDto?> Handle(UpdateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating maintenance schedule {MaintenanceScheduleId}", request.MaintenanceScheduleId);

        var schedule = await _context.MaintenanceSchedules
            .FirstOrDefaultAsync(m => m.MaintenanceScheduleId == request.MaintenanceScheduleId, cancellationToken);

        if (schedule == null)
        {
            _logger.LogWarning("Maintenance schedule {MaintenanceScheduleId} not found", request.MaintenanceScheduleId);
            return null;
        }

        schedule.ServiceType = request.ServiceType;
        schedule.Description = request.Description;
        schedule.MileageInterval = request.MileageInterval;
        schedule.MonthsInterval = request.MonthsInterval;
        schedule.LastServiceMileage = request.LastServiceMileage;
        schedule.LastServiceDate = request.LastServiceDate;
        schedule.NextServiceMileage = request.NextServiceMileage;
        schedule.NextServiceDate = request.NextServiceDate;
        schedule.IsActive = request.IsActive;
        schedule.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated maintenance schedule {MaintenanceScheduleId}", request.MaintenanceScheduleId);

        return schedule.ToDto();
    }
}
