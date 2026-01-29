namespace Messaging.Contracts;

public record InvoiceCreatedEvent
{
    public Guid InvoiceId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public Guid ClientId { get; init; }
    public Guid? ProjectId { get; init; }
    public string InvoiceNumber { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
    public string Currency { get; init; } = "USD";
    public DateTime InvoiceDate { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime CreatedAt { get; init; }
}
