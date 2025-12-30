using GiftIdeaTracker.Core;

namespace GiftIdeaTracker.Api.Features.Purchases;

public record PurchaseDto
{
    public Guid PurchaseId { get; init; }
    public Guid GiftIdeaId { get; init; }
    public DateTime PurchaseDate { get; init; }
    public decimal ActualPrice { get; init; }
    public string? Store { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class PurchaseExtensions
{
    public static PurchaseDto ToDto(this Purchase purchase)
    {
        return new PurchaseDto
        {
            PurchaseId = purchase.PurchaseId,
            GiftIdeaId = purchase.GiftIdeaId,
            PurchaseDate = purchase.PurchaseDate,
            ActualPrice = purchase.ActualPrice,
            Store = purchase.Store,
            CreatedAt = purchase.CreatedAt,
        };
    }
}
