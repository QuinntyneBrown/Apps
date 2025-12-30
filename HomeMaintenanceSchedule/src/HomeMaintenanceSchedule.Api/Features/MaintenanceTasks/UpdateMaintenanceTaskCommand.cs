using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.MaintenanceTasks;

public record UpdateMaintenanceTaskCommand : IRequest<MaintenanceTaskDto?>
{
    public Guid MaintenanceTaskId { get; init; }
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
}

public class UpdateMaintenanceTaskCommandHandler : IRequestHandler<UpdateMaintenanceTaskCommand, MaintenanceTaskDto?>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<UpdateMaintenanceTaskCommandHandler> _logger;

    public UpdateMaintenanceTaskCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<UpdateMaintenanceTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaintenanceTaskDto?> Handle(UpdateMaintenanceTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating maintenance task {MaintenanceTaskId}", request.MaintenanceTaskId);

        var task = await _context.MaintenanceTasks
            .FirstOrDefaultAsync(t => t.MaintenanceTaskId == request.MaintenanceTaskId, cancellationToken);

        if (task == null)
        {
            _logger.LogWarning("Maintenance task {MaintenanceTaskId} not found", request.MaintenanceTaskId);
            return null;
        }

        task.Name = request.Name;
        task.Description = request.Description;
        task.MaintenanceType = request.MaintenanceType;
        task.Status = request.Status;
        task.DueDate = request.DueDate;
        task.CompletedDate = request.CompletedDate;
        task.RecurrenceFrequencyDays = request.RecurrenceFrequencyDays;
        task.EstimatedCost = request.EstimatedCost;
        task.ActualCost = request.ActualCost;
        task.Priority = request.Priority;
        task.Location = request.Location;
        task.ContractorId = request.ContractorId;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated maintenance task {MaintenanceTaskId}", request.MaintenanceTaskId);

        return task.ToDto();
    }
}
