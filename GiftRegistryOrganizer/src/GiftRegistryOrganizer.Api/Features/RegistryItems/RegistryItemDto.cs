using GiftRegistryOrganizer.Core;

namespace GiftRegistryOrganizer.Api.Features.RegistryItems;

public record RegistryItemDto
{
    public Guid RegistryItemId { get; init; }
    public Guid RegistryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public string? Url { get; init; }
    public int QuantityDesired { get; init; }
    public int QuantityReceived { get; init; }
    public Priority Priority { get; init; }
    public bool IsFulfilled { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class RegistryItemExtensions
{
    public static RegistryItemDto ToDto(this RegistryItem item)
    {
        return new RegistryItemDto
        {
            RegistryItemId = item.RegistryItemId,
            RegistryId = item.RegistryId,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            Url = item.Url,
            QuantityDesired = item.QuantityDesired,
            QuantityReceived = item.QuantityReceived,
            Priority = item.Priority,
            IsFulfilled = item.IsFulfilled,
            CreatedAt = item.CreatedAt,
        };
    }
}
