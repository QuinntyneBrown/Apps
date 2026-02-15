using Tasks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Api.Features;

public record GetTaskByIdQuery(Guid PriorityTaskId) : IRequest<TaskDto?>;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto?>
{
    private readonly ITasksDbContext _context;

    public GetTaskByIdQueryHandler(ITasksDbContext context)
    {
        _context = context;
    }

    public async Task<TaskDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.PriorityTaskId == request.PriorityTaskId, cancellationToken);
        return task?.ToDto();
    }
}
