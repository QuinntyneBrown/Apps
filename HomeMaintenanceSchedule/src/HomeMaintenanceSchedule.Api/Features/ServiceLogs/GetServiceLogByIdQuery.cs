using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.ServiceLogs;

public record GetServiceLogByIdQuery : IRequest<ServiceLogDto?>
{
    public Guid ServiceLogId { get; init; }
}

public class GetServiceLogByIdQueryHandler : IRequestHandler<GetServiceLogByIdQuery, ServiceLogDto?>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<GetServiceLogByIdQueryHandler> _logger;

    public GetServiceLogByIdQueryHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<GetServiceLogByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ServiceLogDto?> Handle(GetServiceLogByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting service log {ServiceLogId}", request.ServiceLogId);

        var log = await _context.ServiceLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.ServiceLogId == request.ServiceLogId, cancellationToken);

        return log?.ToDto();
    }
}
