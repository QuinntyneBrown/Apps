using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.AdminTasks;

public record CreateAdminTaskCommand : IRequest<AdminTaskDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TaskCategory Category { get; init; }
    public TaskPriority Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public bool IsRecurring { get; init; }
    public string? RecurrencePattern { get; init; }
    public string? Notes { get; init; }
}

public class CreateAdminTaskCommandHandler : IRequestHandler<CreateAdminTaskCommand, AdminTaskDto>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<CreateAdminTaskCommandHandler> _logger;

    public CreateAdminTaskCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<CreateAdminTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AdminTaskDto> Handle(CreateAdminTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating admin task for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var task = new AdminTask
        {
            AdminTaskId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Priority = request.Priority,
            DueDate = request.DueDate,
            IsCompleted = false,
            IsRecurring = request.IsRecurring,
            RecurrencePattern = request.RecurrencePattern,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created admin task {AdminTaskId} for user {UserId}",
            task.AdminTaskId,
            request.UserId);

        return task.ToDto();
    }
}
