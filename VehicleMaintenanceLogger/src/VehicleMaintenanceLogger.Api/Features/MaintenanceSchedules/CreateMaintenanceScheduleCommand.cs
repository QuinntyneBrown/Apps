using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.MaintenanceSchedules;

public record CreateMaintenanceScheduleCommand : IRequest<MaintenanceScheduleDto>
{
    public Guid VehicleId { get; init; }
    public ServiceType ServiceType { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal? MileageInterval { get; init; }
    public int? MonthsInterval { get; init; }
    public decimal? LastServiceMileage { get; init; }
    public DateTime? LastServiceDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateMaintenanceScheduleCommandHandler : IRequestHandler<CreateMaintenanceScheduleCommand, MaintenanceScheduleDto>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<CreateMaintenanceScheduleCommandHandler> _logger;

    public CreateMaintenanceScheduleCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<CreateMaintenanceScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaintenanceScheduleDto> Handle(CreateMaintenanceScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating maintenance schedule for vehicle {VehicleId}, service type: {ServiceType}",
            request.VehicleId,
            request.ServiceType);

        var schedule = new MaintenanceSchedule
        {
            MaintenanceScheduleId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            ServiceType = request.ServiceType,
            Description = request.Description,
            MileageInterval = request.MileageInterval,
            MonthsInterval = request.MonthsInterval,
            LastServiceMileage = request.LastServiceMileage,
            LastServiceDate = request.LastServiceDate,
            Notes = request.Notes,
            IsActive = true,
        };

        // Calculate next service dates if intervals are provided
        if (request.LastServiceMileage.HasValue && request.MileageInterval.HasValue)
        {
            schedule.NextServiceMileage = request.LastServiceMileage.Value + request.MileageInterval.Value;
        }

        if (request.LastServiceDate.HasValue && request.MonthsInterval.HasValue)
        {
            schedule.NextServiceDate = request.LastServiceDate.Value.AddMonths(request.MonthsInterval.Value);
        }

        _context.MaintenanceSchedules.Add(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created maintenance schedule {MaintenanceScheduleId} for vehicle {VehicleId}",
            schedule.MaintenanceScheduleId,
            request.VehicleId);

        return schedule.ToDto();
    }
}
