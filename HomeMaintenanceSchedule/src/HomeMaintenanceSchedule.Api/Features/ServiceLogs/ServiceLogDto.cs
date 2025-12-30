using HomeMaintenanceSchedule.Core;

namespace HomeMaintenanceSchedule.Api.Features.ServiceLogs;

public record ServiceLogDto
{
    public Guid ServiceLogId { get; init; }
    public Guid MaintenanceTaskId { get; init; }
    public DateTime ServiceDate { get; init; }
    public string Description { get; init; } = string.Empty;
    public Guid? ContractorId { get; init; }
    public decimal? Cost { get; init; }
    public string? Notes { get; init; }
    public string? PartsUsed { get; init; }
    public decimal? LaborHours { get; init; }
    public DateTime? WarrantyExpiresAt { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ServiceLogExtensions
{
    public static ServiceLogDto ToDto(this ServiceLog log)
    {
        return new ServiceLogDto
        {
            ServiceLogId = log.ServiceLogId,
            MaintenanceTaskId = log.MaintenanceTaskId,
            ServiceDate = log.ServiceDate,
            Description = log.Description,
            ContractorId = log.ContractorId,
            Cost = log.Cost,
            Notes = log.Notes,
            PartsUsed = log.PartsUsed,
            LaborHours = log.LaborHours,
            WarrantyExpiresAt = log.WarrantyExpiresAt,
            CreatedAt = log.CreatedAt,
        };
    }
}
