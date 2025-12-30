using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.MaintenanceTasks;

public record CreateMaintenanceTaskCommand : IRequest<MaintenanceTaskDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public MaintenanceType MaintenanceType { get; init; }
    public TaskStatus Status { get; init; }
    public DateTime? DueDate { get; init; }
    public int? RecurrenceFrequencyDays { get; init; }
    public decimal? EstimatedCost { get; init; }
    public int Priority { get; init; } = 3;
    public string? Location { get; init; }
    public Guid? ContractorId { get; init; }
}

public class CreateMaintenanceTaskCommandHandler : IRequestHandler<CreateMaintenanceTaskCommand, MaintenanceTaskDto>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<CreateMaintenanceTaskCommandHandler> _logger;

    public CreateMaintenanceTaskCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<CreateMaintenanceTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaintenanceTaskDto> Handle(CreateMaintenanceTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating maintenance task for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            MaintenanceType = request.MaintenanceType,
            Status = request.Status,
            DueDate = request.DueDate,
            RecurrenceFrequencyDays = request.RecurrenceFrequencyDays,
            EstimatedCost = request.EstimatedCost,
            Priority = request.Priority,
            Location = request.Location,
            ContractorId = request.ContractorId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _context.MaintenanceTasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created maintenance task {MaintenanceTaskId} for user {UserId}",
            task.MaintenanceTaskId,
            request.UserId);

        return task.ToDto();
    }
}
