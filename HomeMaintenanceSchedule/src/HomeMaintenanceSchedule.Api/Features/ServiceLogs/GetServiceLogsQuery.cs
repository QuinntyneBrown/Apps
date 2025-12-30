using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.ServiceLogs;

public record GetServiceLogsQuery : IRequest<IEnumerable<ServiceLogDto>>
{
    public Guid? MaintenanceTaskId { get; init; }
    public Guid? ContractorId { get; init; }
}

public class GetServiceLogsQueryHandler : IRequestHandler<GetServiceLogsQuery, IEnumerable<ServiceLogDto>>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<GetServiceLogsQueryHandler> _logger;

    public GetServiceLogsQueryHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<GetServiceLogsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ServiceLogDto>> Handle(GetServiceLogsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting service logs");

        var query = _context.ServiceLogs.AsNoTracking();

        if (request.MaintenanceTaskId.HasValue)
        {
            query = query.Where(l => l.MaintenanceTaskId == request.MaintenanceTaskId.Value);
        }

        if (request.ContractorId.HasValue)
        {
            query = query.Where(l => l.ContractorId == request.ContractorId.Value);
        }

        var logs = await query
            .OrderByDescending(l => l.ServiceDate)
            .ToListAsync(cancellationToken);

        return logs.Select(l => l.ToDto());
    }
}
