using Tasks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Api.Features;

public record DeleteTaskCommand(Guid PriorityTaskId) : IRequest<bool>;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly ITasksDbContext _context;
    private readonly ILogger<DeleteTaskCommandHandler> _logger;

    public DeleteTaskCommandHandler(ITasksDbContext context, ILogger<DeleteTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.PriorityTaskId == request.PriorityTaskId, cancellationToken);

        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Task deleted: {TaskId}", request.PriorityTaskId);

        return true;
    }
}
