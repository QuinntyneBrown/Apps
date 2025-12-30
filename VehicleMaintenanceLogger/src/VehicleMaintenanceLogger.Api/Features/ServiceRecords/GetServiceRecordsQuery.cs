using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.ServiceRecords;

public record GetServiceRecordsQuery : IRequest<IEnumerable<ServiceRecordDto>>
{
    public Guid? VehicleId { get; init; }
    public ServiceType? ServiceType { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public decimal? MinCost { get; init; }
    public decimal? MaxCost { get; init; }
}

public class GetServiceRecordsQueryHandler : IRequestHandler<GetServiceRecordsQuery, IEnumerable<ServiceRecordDto>>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<GetServiceRecordsQueryHandler> _logger;

    public GetServiceRecordsQueryHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<GetServiceRecordsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ServiceRecordDto>> Handle(GetServiceRecordsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting service records for vehicle {VehicleId}", request.VehicleId);

        var query = _context.ServiceRecords.AsNoTracking();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(s => s.VehicleId == request.VehicleId.Value);
        }

        if (request.ServiceType.HasValue)
        {
            query = query.Where(s => s.ServiceType == request.ServiceType.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(s => s.ServiceDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(s => s.ServiceDate <= request.EndDate.Value);
        }

        if (request.MinCost.HasValue)
        {
            query = query.Where(s => s.Cost >= request.MinCost.Value);
        }

        if (request.MaxCost.HasValue)
        {
            query = query.Where(s => s.Cost <= request.MaxCost.Value);
        }

        var serviceRecords = await query
            .OrderByDescending(s => s.ServiceDate)
            .ToListAsync(cancellationToken);

        return serviceRecords.Select(s => s.ToDto());
    }
}
