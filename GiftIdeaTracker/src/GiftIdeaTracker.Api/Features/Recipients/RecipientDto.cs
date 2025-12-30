using GiftIdeaTracker.Core;

namespace GiftIdeaTracker.Api.Features.Recipients;

public record RecipientDto
{
    public Guid RecipientId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Relationship { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class RecipientExtensions
{
    public static RecipientDto ToDto(this Recipient recipient)
    {
        return new RecipientDto
        {
            RecipientId = recipient.RecipientId,
            UserId = recipient.UserId,
            Name = recipient.Name,
            Relationship = recipient.Relationship,
            CreatedAt = recipient.CreatedAt,
        };
    }
}
