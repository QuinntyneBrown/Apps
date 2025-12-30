using VehicleMaintenanceLogger.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VehicleMaintenanceLogger.Api.Features.ServiceRecords;

public record CreateServiceRecordCommand : IRequest<ServiceRecordDto>
{
    public Guid VehicleId { get; init; }
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

public class CreateServiceRecordCommandHandler : IRequestHandler<CreateServiceRecordCommand, ServiceRecordDto>
{
    private readonly IVehicleMaintenanceLoggerContext _context;
    private readonly ILogger<CreateServiceRecordCommandHandler> _logger;

    public CreateServiceRecordCommandHandler(
        IVehicleMaintenanceLoggerContext context,
        ILogger<CreateServiceRecordCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ServiceRecordDto> Handle(CreateServiceRecordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating service record for vehicle {VehicleId}, service type: {ServiceType}",
            request.VehicleId,
            request.ServiceType);

        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            ServiceType = request.ServiceType,
            ServiceDate = request.ServiceDate,
            MileageAtService = request.MileageAtService,
            Cost = request.Cost,
            ServiceProvider = request.ServiceProvider,
            Description = request.Description,
            Notes = request.Notes,
            PartsReplaced = request.PartsReplaced,
            InvoiceNumber = request.InvoiceNumber,
            WarrantyExpirationDate = request.WarrantyExpirationDate,
        };

        _context.ServiceRecords.Add(serviceRecord);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created service record {ServiceRecordId} for vehicle {VehicleId}",
            serviceRecord.ServiceRecordId,
            request.VehicleId);

        return serviceRecord.ToDto();
    }
}
