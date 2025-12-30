using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.MaintenanceTasks;

public record GetMaintenanceTasksQuery : IRequest<IEnumerable<MaintenanceTaskDto>>
{
    public Guid? UserId { get; init; }
    public MaintenanceType? MaintenanceType { get; init; }
    public TaskStatus? Status { get; init; }
    public Guid? ContractorId { get; init; }
}

public class GetMaintenanceTasksQueryHandler : IRequestHandler<GetMaintenanceTasksQuery, IEnumerable<MaintenanceTaskDto>>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<GetMaintenanceTasksQueryHandler> _logger;

    public GetMaintenanceTasksQueryHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<GetMaintenanceTasksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MaintenanceTaskDto>> Handle(GetMaintenanceTasksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting maintenance tasks for user {UserId}", request.UserId);

        var query = _context.MaintenanceTasks.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.MaintenanceType.HasValue)
        {
            query = query.Where(t => t.MaintenanceType == request.MaintenanceType.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(t => t.Status == request.Status.Value);
        }

        if (request.ContractorId.HasValue)
        {
            query = query.Where(t => t.ContractorId == request.ContractorId.Value);
        }

        var tasks = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return tasks.Select(t => t.ToDto());
    }
}
