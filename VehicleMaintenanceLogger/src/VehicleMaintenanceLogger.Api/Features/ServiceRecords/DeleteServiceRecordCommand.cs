using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.ServiceRecords;

public record DeleteServiceRecordCommand : IRequest<bool>
{
    public Guid ServiceRecordId { get; init; }
}

public class DeleteServiceRecordCommandHandler : IRequestHandler<DeleteServiceRecordCommand, bool>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<DeleteServiceRecordCommandHandler> _logger;

    public DeleteServiceRecordCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<DeleteServiceRecordCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteServiceRecordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting service record {ServiceRecordId}", request.ServiceRecordId);

        var serviceRecord = await _context.ServiceRecords
            .FirstOrDefaultAsync(s => s.ServiceRecordId == request.ServiceRecordId, cancellationToken);

        if (serviceRecord == null)
        {
            _logger.LogWarning("Service record {ServiceRecordId} not found", request.ServiceRecordId);
            return false;
        }

        _context.ServiceRecords.Remove(serviceRecord);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted service record {ServiceRecordId}", request.ServiceRecordId);

        return true;
    }
}
