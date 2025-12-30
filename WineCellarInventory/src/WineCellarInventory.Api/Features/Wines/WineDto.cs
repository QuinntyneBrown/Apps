using WineCellarInventory.Core;

namespace WineCellarInventory.Api.Features.Wines;

public record WineDto
{
    public Guid WineId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public WineType WineType { get; init; }
    public Region Region { get; init; }
    public int? Vintage { get; init; }
    public string? Producer { get; init; }
    public decimal? PurchasePrice { get; init; }
    public int BottleCount { get; init; }
    public string? StorageLocation { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class WineExtensions
{
    public static WineDto ToDto(this Wine wine)
    {
        return new WineDto
        {
            WineId = wine.WineId,
            UserId = wine.UserId,
            Name = wine.Name,
            WineType = wine.WineType,
            Region = wine.Region,
            Vintage = wine.Vintage,
            Producer = wine.Producer,
            PurchasePrice = wine.PurchasePrice,
            BottleCount = wine.BottleCount,
            StorageLocation = wine.StorageLocation,
            Notes = wine.Notes,
            CreatedAt = wine.CreatedAt,
        };
    }
}
