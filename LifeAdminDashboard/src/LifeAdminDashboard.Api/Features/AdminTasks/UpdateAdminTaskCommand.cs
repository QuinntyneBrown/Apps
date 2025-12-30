using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.AdminTasks;

public record UpdateAdminTaskCommand : IRequest<AdminTaskDto?>
{
    public Guid AdminTaskId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TaskCategory Category { get; init; }
    public TaskPriority Priority { get; init; }
    public DateTime? DueDate { get; init; }
    public bool IsRecurring { get; init; }
    public string? RecurrencePattern { get; init; }
    public string? Notes { get; init; }
}

public class UpdateAdminTaskCommandHandler : IRequestHandler<UpdateAdminTaskCommand, AdminTaskDto?>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<UpdateAdminTaskCommandHandler> _logger;

    public UpdateAdminTaskCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<UpdateAdminTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AdminTaskDto?> Handle(UpdateAdminTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating admin task {AdminTaskId}", request.AdminTaskId);

        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.AdminTaskId == request.AdminTaskId, cancellationToken);

        if (task == null)
        {
            _logger.LogWarning("Admin task {AdminTaskId} not found", request.AdminTaskId);
            return null;
        }

        task.Title = request.Title;
        task.Description = request.Description;
        task.Category = request.Category;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        task.IsRecurring = request.IsRecurring;
        task.RecurrencePattern = request.RecurrencePattern;
        task.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated admin task {AdminTaskId}", request.AdminTaskId);

        return task.ToDto();
    }
}
