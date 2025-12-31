namespace ContactManagementApp.Api.Features.FollowUps;

public record FollowUpDto
{
    public Guid FollowUpId { get; init; }
    public Guid UserId { get; init; }
    public Guid ContactId { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletedDate { get; init; }
    public string Priority { get; init; } = "Medium";
    public string? Notes { get; init; }
    public bool IsOverdue { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
