namespace Goals.Api.Features;

public record GoalDto
{
    public Guid GoalId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal TargetAmount { get; init; }
    public decimal CurrentAmount { get; init; }
    public DateTime TargetDate { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
