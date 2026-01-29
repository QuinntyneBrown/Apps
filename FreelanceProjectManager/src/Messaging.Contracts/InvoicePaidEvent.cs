namespace Messaging.Contracts;

public record InvoicePaidEvent
{
    public Guid InvoiceId { get; init; }
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public Guid ClientId { get; init; }
    public decimal TotalAmount { get; init; }
    public string Currency { get; init; } = "USD";
    public DateTime PaidAt { get; init; }
}
