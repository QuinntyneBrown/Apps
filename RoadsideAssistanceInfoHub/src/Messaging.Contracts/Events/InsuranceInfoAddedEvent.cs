namespace Messaging.Contracts.Events;

public sealed record InsuranceInfoAddedEvent : IntegrationEvent
{
    public required Guid InsuranceInfoId { get; init; }
    public required Guid VehicleId { get; init; }
    public required string InsuranceCompany { get; init; }
    public required string PolicyNumber { get; init; }
}
