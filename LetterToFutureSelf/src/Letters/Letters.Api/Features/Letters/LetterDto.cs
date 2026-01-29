using Letters.Core.Models;

namespace Letters.Api.Features.Letters;

public record LetterDto
{
    public Guid LetterId { get; init; }
    public Guid UserId { get; init; }
    public string Subject { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime WrittenDate { get; init; }
    public DateTime ScheduledDeliveryDate { get; init; }
    public DateTime? ActualDeliveryDate { get; init; }
    public DeliveryStatus DeliveryStatus { get; init; }
    public bool HasBeenRead { get; init; }
    public DateTime? ReadDate { get; init; }
    public DateTime CreatedAt { get; init; }
}
