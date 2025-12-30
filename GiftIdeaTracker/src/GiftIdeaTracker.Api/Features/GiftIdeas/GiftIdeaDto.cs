using GiftIdeaTracker.Core;

namespace GiftIdeaTracker.Api.Features.GiftIdeas;

public record GiftIdeaDto
{
    public Guid GiftIdeaId { get; init; }
    public Guid UserId { get; init; }
    public Guid? RecipientId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Occasion Occasion { get; init; }
    public decimal? EstimatedPrice { get; init; }
    public bool IsPurchased { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class GiftIdeaExtensions
{
    public static GiftIdeaDto ToDto(this GiftIdea giftIdea)
    {
        return new GiftIdeaDto
        {
            GiftIdeaId = giftIdea.GiftIdeaId,
            UserId = giftIdea.UserId,
            RecipientId = giftIdea.RecipientId,
            Name = giftIdea.Name,
            Occasion = giftIdea.Occasion,
            EstimatedPrice = giftIdea.EstimatedPrice,
            IsPurchased = giftIdea.IsPurchased,
            CreatedAt = giftIdea.CreatedAt,
        };
    }
}
