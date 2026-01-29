using Tasks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Api.Features;

public record GetTasksQuery : IRequest<IEnumerable<TaskDto>>;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
{
    private readonly ITasksDbContext _context;

    public GetTasksQueryHandler(ITasksDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _context.Tasks.ToListAsync(cancellationToken);
        return tasks.Select(t => t.ToDto());
    }
}
