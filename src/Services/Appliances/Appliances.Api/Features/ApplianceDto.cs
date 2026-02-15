using Appliances.Core.Models;

namespace Appliances.Api.Features;

public record ApplianceDto
{
    public Guid ApplianceId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public ApplianceType ApplianceType { get; init; }
    public string? Brand { get; init; }
    public string? ModelNumber { get; init; }
    public string? SerialNumber { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public decimal? PurchasePrice { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ApplianceExtensions
{
    public static ApplianceDto ToDto(this Appliance appliance) => new()
    {
        ApplianceId = appliance.ApplianceId,
        UserId = appliance.UserId,
        Name = appliance.Name,
        ApplianceType = appliance.ApplianceType,
        Brand = appliance.Brand,
        ModelNumber = appliance.ModelNumber,
        SerialNumber = appliance.SerialNumber,
        PurchaseDate = appliance.PurchaseDate,
        PurchasePrice = appliance.PurchasePrice,
        CreatedAt = appliance.CreatedAt
    };
}
