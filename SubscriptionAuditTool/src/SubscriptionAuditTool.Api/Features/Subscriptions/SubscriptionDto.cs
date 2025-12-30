using SubscriptionAuditTool.Core;

namespace SubscriptionAuditTool.Api.Features.Subscriptions;

public record SubscriptionDto
{
    public Guid SubscriptionId { get; init; }
    public string ServiceName { get; init; } = string.Empty;
    public decimal Cost { get; init; }
    public BillingCycle BillingCycle { get; init; }
    public DateTime NextBillingDate { get; init; }
    public SubscriptionStatus Status { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? CancellationDate { get; init; }
    public Guid? CategoryId { get; init; }
    public string? Notes { get; init; }
    public decimal AnnualCost { get; init; }
    public string? CategoryName { get; init; }
}

public static class SubscriptionExtensions
{
    public static SubscriptionDto ToDto(this Subscription subscription)
    {
        return new SubscriptionDto
        {
            SubscriptionId = subscription.SubscriptionId,
            ServiceName = subscription.ServiceName,
            Cost = subscription.Cost,
            BillingCycle = subscription.BillingCycle,
            NextBillingDate = subscription.NextBillingDate,
            Status = subscription.Status,
            StartDate = subscription.StartDate,
            CancellationDate = subscription.CancellationDate,
            CategoryId = subscription.CategoryId,
            Notes = subscription.Notes,
            AnnualCost = subscription.CalculateAnnualCost(),
            CategoryName = subscription.Category?.Name,
        };
    }
}
