using VehicleMaintenanceLogger.Core;

namespace VehicleMaintenanceLogger.Api.Features.MaintenanceSchedules;

public record MaintenanceScheduleDto
{
    public Guid MaintenanceScheduleId { get; init; }
    public Guid VehicleId { get; init; }
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

public static class MaintenanceScheduleExtensions
{
    public static MaintenanceScheduleDto ToDto(this MaintenanceSchedule schedule)
    {
        return new MaintenanceScheduleDto
        {
            MaintenanceScheduleId = schedule.MaintenanceScheduleId,
            VehicleId = schedule.VehicleId,
            ServiceType = schedule.ServiceType,
            Description = schedule.Description,
            MileageInterval = schedule.MileageInterval,
            MonthsInterval = schedule.MonthsInterval,
            LastServiceMileage = schedule.LastServiceMileage,
            LastServiceDate = schedule.LastServiceDate,
            NextServiceMileage = schedule.NextServiceMileage,
            NextServiceDate = schedule.NextServiceDate,
            IsActive = schedule.IsActive,
            Notes = schedule.Notes,
        };
    }
}
