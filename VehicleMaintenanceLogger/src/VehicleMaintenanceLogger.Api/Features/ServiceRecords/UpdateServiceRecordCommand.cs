using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.ServiceRecords;

public record UpdateServiceRecordCommand : IRequest<ServiceRecordDto?>
{
    public Guid ServiceRecordId { get; init; }
    public ServiceType ServiceType { get; init; }
    public DateTime ServiceDate { get; init; }
    public decimal MileageAtService { get; init; }
    public decimal Cost { get; init; }
    public string? ServiceProvider { get; init; }
    public string Description { get; init; } = string.Empty;
    public string? Notes { get; init; }
    public List<string> PartsReplaced { get; init; } = new List<string>();
    public string? InvoiceNumber { get; init; }
    public DateTime? WarrantyExpirationDate { get; init; }
}

public class UpdateServiceRecordCommandHandler : IRequestHandler<UpdateServiceRecordCommand, ServiceRecordDto?>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<UpdateServiceRecordCommandHandler> _logger;

    public UpdateServiceRecordCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<UpdateServiceRecordCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ServiceRecordDto?> Handle(UpdateServiceRecordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating service record {ServiceRecordId}", request.ServiceRecordId);

        var serviceRecord = await _context.ServiceRecords
            .FirstOrDefaultAsync(s => s.ServiceRecordId == request.ServiceRecordId, cancellationToken);

        if (serviceRecord == null)
        {
            _logger.LogWarning("Service record {ServiceRecordId} not found", request.ServiceRecordId);
            return null;
        }

        serviceRecord.ServiceType = request.ServiceType;
        serviceRecord.ServiceDate = request.ServiceDate;
        serviceRecord.MileageAtService = request.MileageAtService;
        serviceRecord.Cost = request.Cost;
        serviceRecord.ServiceProvider = request.ServiceProvider;
        serviceRecord.Description = request.Description;
        serviceRecord.Notes = request.Notes;
        serviceRecord.PartsReplaced = request.PartsReplaced;
        serviceRecord.InvoiceNumber = request.InvoiceNumber;
        serviceRecord.WarrantyExpirationDate = request.WarrantyExpirationDate;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated service record {ServiceRecordId}", request.ServiceRecordId);

        return serviceRecord.ToDto();
    }
}
