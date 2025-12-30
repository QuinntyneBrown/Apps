using NeighborhoodSocialNetwork.Core;

namespace NeighborhoodSocialNetwork.Api.Features.Messages;

public record MessageDto
{
    public Guid MessageId { get; init; }
    public Guid SenderNeighborId { get; init; }
    public Guid RecipientNeighborId { get; init; }
    public string Subject { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public bool IsRead { get; init; }
    public DateTime? ReadAt { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class MessageExtensions
{
    public static MessageDto ToDto(this Message message)
    {
        return new MessageDto
        {
            MessageId = message.MessageId,
            SenderNeighborId = message.SenderNeighborId,
            RecipientNeighborId = message.RecipientNeighborId,
            Subject = message.Subject,
            Content = message.Content,
            IsRead = message.IsRead,
            ReadAt = message.ReadAt,
            CreatedAt = message.CreatedAt,
        };
    }
}
