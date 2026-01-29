using Tasks.Core;
using Tasks.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Api.Features;

public record UpdateTaskCommand(
    Guid PriorityTaskId,
    string? Title,
    string? Description,
    Urgency? Urgency,
    Importance? Importance,
    DateTime? DueDate,
    Guid? CategoryId) : IRequest<TaskDto?>;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto?>
{
    private readonly ITasksDbContext _context;
    private readonly ILogger<UpdateTaskCommandHandler> _logger;

    public UpdateTaskCommandHandler(ITasksDbContext context, ILogger<UpdateTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaskDto?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.PriorityTaskId == request.PriorityTaskId, cancellationToken);

        if (task == null) return null;

        if (request.Title != null) task.Title = request.Title;
        if (request.Description != null) task.Description = request.Description;
        if (request.Urgency.HasValue) task.Urgency = request.Urgency.Value;
        if (request.Importance.HasValue) task.Importance = request.Importance.Value;
        if (request.DueDate.HasValue) task.DueDate = request.DueDate.Value;
        if (request.CategoryId.HasValue) task.CategoryId = request.CategoryId.Value;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Task updated: {TaskId}", task.PriorityTaskId);

        return task.ToDto();
    }
}
