using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.ServiceRecords;

public record GetServiceRecordByIdQuery : IRequest<ServiceRecordDto?>
{
    public Guid ServiceRecordId { get; init; }
}

public class GetServiceRecordByIdQueryHandler : IRequestHandler<GetServiceRecordByIdQuery, ServiceRecordDto?>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<GetServiceRecordByIdQueryHandler> _logger;

    public GetServiceRecordByIdQueryHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<GetServiceRecordByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ServiceRecordDto?> Handle(GetServiceRecordByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting service record {ServiceRecordId}", request.ServiceRecordId);

        var serviceRecord = await _context.ServiceRecords
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ServiceRecordId == request.ServiceRecordId, cancellationToken);

        if (serviceRecord == null)
        {
            _logger.LogWarning("Service record {ServiceRecordId} not found", request.ServiceRecordId);
            return null;
        }

        return serviceRecord.ToDto();
    }
}
