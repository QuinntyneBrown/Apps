using VehicleMaintenanceLogger.Core;

namespace VehicleMaintenanceLogger.Api.Features.ServiceRecords;

public record ServiceRecordDto
{
    public Guid ServiceRecordId { get; init; }
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

public static class ServiceRecordExtensions
{
    public static ServiceRecordDto ToDto(this ServiceRecord serviceRecord)
    {
        return new ServiceRecordDto
        {
            ServiceRecordId = serviceRecord.ServiceRecordId,
            VehicleId = serviceRecord.VehicleId,
            ServiceType = serviceRecord.ServiceType,
            ServiceDate = serviceRecord.ServiceDate,
            MileageAtService = serviceRecord.MileageAtService,
            Cost = serviceRecord.Cost,
            ServiceProvider = serviceRecord.ServiceProvider,
            Description = serviceRecord.Description,
            Notes = serviceRecord.Notes,
            PartsReplaced = serviceRecord.PartsReplaced,
            InvoiceNumber = serviceRecord.InvoiceNumber,
            WarrantyExpirationDate = serviceRecord.WarrantyExpirationDate,
        };
    }
}
