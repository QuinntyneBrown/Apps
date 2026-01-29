namespace Messaging.Contracts.Events;

public sealed record LoanCreatedEvent : IntegrationEvent
{
    public required Guid LoanId { get; init; }
    public required string Name { get; init; }
    public required string LoanType { get; init; }
    public required decimal RequestedAmount { get; init; }
}
