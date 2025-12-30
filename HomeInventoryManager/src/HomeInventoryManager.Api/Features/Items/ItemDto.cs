using HomeInventoryManager.Core;

namespace HomeInventoryManager.Api.Features.Items;

public record ItemDto
{
    public Guid ItemId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Category Category { get; init; }
    public Room Room { get; init; }
    public string? Brand { get; init; }
    public string? ModelNumber { get; init; }
    public string? SerialNumber { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public decimal? PurchasePrice { get; init; }
    public decimal? CurrentValue { get; init; }
    public int Quantity { get; init; }
    public string? PhotoUrl { get; init; }
    public string? ReceiptUrl { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}

public static class ItemExtensions
{
    public static ItemDto ToDto(this Item item)
    {
        return new ItemDto
        {
            ItemId = item.ItemId,
            UserId = item.UserId,
            Name = item.Name,
            Description = item.Description,
            Category = item.Category,
            Room = item.Room,
            Brand = item.Brand,
            ModelNumber = item.ModelNumber,
            SerialNumber = item.SerialNumber,
            PurchaseDate = item.PurchaseDate,
            PurchasePrice = item.PurchasePrice,
            CurrentValue = item.CurrentValue,
            Quantity = item.Quantity,
            PhotoUrl = item.PhotoUrl,
            ReceiptUrl = item.ReceiptUrl,
            Notes = item.Notes,
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
        };
    }
}
