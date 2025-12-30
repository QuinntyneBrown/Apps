using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.MaintenanceTasks;

public record GetMaintenanceTaskByIdQuery : IRequest<MaintenanceTaskDto?>
{
    public Guid MaintenanceTaskId { get; init; }
}

public class GetMaintenanceTaskByIdQueryHandler : IRequestHandler<GetMaintenanceTaskByIdQuery, MaintenanceTaskDto?>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<GetMaintenanceTaskByIdQueryHandler> _logger;

    public GetMaintenanceTaskByIdQueryHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<GetMaintenanceTaskByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MaintenanceTaskDto?> Handle(GetMaintenanceTaskByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting maintenance task {MaintenanceTaskId}", request.MaintenanceTaskId);

        var task = await _context.MaintenanceTasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.MaintenanceTaskId == request.MaintenanceTaskId, cancellationToken);

        return task?.ToDto();
    }
}
