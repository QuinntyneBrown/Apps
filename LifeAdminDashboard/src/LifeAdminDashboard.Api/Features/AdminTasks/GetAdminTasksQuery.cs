using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.AdminTasks;

public record GetAdminTasksQuery : IRequest<IEnumerable<AdminTaskDto>>
{
    public Guid? UserId { get; init; }
    public TaskCategory? Category { get; init; }
    public TaskPriority? Priority { get; init; }
    public bool? IsCompleted { get; init; }
    public bool? IsOverdue { get; init; }
}

public class GetAdminTasksQueryHandler : IRequestHandler<GetAdminTasksQuery, IEnumerable<AdminTaskDto>>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<GetAdminTasksQueryHandler> _logger;

    public GetAdminTasksQueryHandler(
        ILifeAdminDashboardContext context,
        ILogger<GetAdminTasksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<AdminTaskDto>> Handle(GetAdminTasksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting admin tasks for user {UserId}", request.UserId);

        var query = _context.Tasks.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(t => t.Category == request.Category.Value);
        }

        if (request.Priority.HasValue)
        {
            query = query.Where(t => t.Priority == request.Priority.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == request.IsCompleted.Value);
        }

        var tasks = await query
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .ToListAsync(cancellationToken);

        if (request.IsOverdue == true)
        {
            tasks = tasks.Where(t => t.IsOverdue()).ToList();
        }

        return tasks.Select(t => t.ToDto());
    }
}
