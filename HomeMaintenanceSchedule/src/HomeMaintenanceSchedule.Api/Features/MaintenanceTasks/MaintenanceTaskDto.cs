using HomeMaintenanceSchedule.Core;

namespace HomeMaintenanceSchedule.Api.Features.MaintenanceTasks;

public record MaintenanceTaskDto
{
    public Guid MaintenanceTaskId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public MaintenanceType MaintenanceType { get; init; }
    public TaskStatus Status { get; init; }
    public DateTime? DueDate { get; init; }
    public DateTime? CompletedDate { get; init; }
    public int? RecurrenceFrequencyDays { get; init; }
    public decimal? EstimatedCost { get; init; }
    public decimal? ActualCost { get; init; }
    public int Priority { get; init; }
    public string? Location { get; init; }
    public Guid? ContractorId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}

public static class MaintenanceTaskExtensions
{
    public static MaintenanceTaskDto ToDto(this MaintenanceTask task)
    {
        return new MaintenanceTaskDto
        {
            MaintenanceTaskId = task.MaintenanceTaskId,
            UserId = task.UserId,
            Name = task.Name,
            Description = task.Description,
            MaintenanceType = task.MaintenanceType,
            Status = task.Status,
            DueDate = task.DueDate,
            CompletedDate = task.CompletedDate,
            RecurrenceFrequencyDays = task.RecurrenceFrequencyDays,
            EstimatedCost = task.EstimatedCost,
            ActualCost = task.ActualCost,
            Priority = task.Priority,
            Location = task.Location,
            ContractorId = task.ContractorId,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
        };
    }
}
