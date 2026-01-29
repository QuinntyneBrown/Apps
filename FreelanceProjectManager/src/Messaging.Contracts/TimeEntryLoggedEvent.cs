namespace Messaging.Contracts;

public record TimeEntryLoggedEvent
{
    public Guid TimeEntryId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }
    public DateTime WorkDate { get; init; }
    public decimal Hours { get; init; }
    public bool IsBillable { get; init; }
    public DateTime CreatedAt { get; init; }
}
