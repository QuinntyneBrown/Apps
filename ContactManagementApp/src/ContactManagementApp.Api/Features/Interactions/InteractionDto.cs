namespace ContactManagementApp.Api.Features.Interactions;

public record InteractionDto
{
    public Guid InteractionId { get; init; }
    public Guid UserId { get; init; }
    public Guid ContactId { get; init; }
    public string InteractionType { get; init; } = string.Empty;
    public DateTime InteractionDate { get; init; }
    public string? Subject { get; init; }
    public string? Notes { get; init; }
    public string? Outcome { get; init; }
    public int? DurationMinutes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
