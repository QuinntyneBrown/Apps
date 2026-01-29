using ServiceRecords.Core.Models;

namespace ServiceRecords.Api.Features;

public record ServiceRecordDto
{
    public Guid ServiceRecordId { get; init; }
    public Guid ApplianceId { get; init; }
    public DateTime ServiceDate { get; init; }
    public string? ServiceProvider { get; init; }
    public string? Description { get; init; }
    public decimal? Cost { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ServiceRecordExtensions
{
    public static ServiceRecordDto ToDto(this ServiceRecord sr) => new()
    {
        ServiceRecordId = sr.ServiceRecordId,
        ApplianceId = sr.ApplianceId,
        ServiceDate = sr.ServiceDate,
        ServiceProvider = sr.ServiceProvider,
        Description = sr.Description,
        Cost = sr.Cost,
        CreatedAt = sr.CreatedAt
    };
}
