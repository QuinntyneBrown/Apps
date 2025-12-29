# Subscription Management - Backend Requirements

## API Endpoints

#### POST /api/subscriptions
Inventory paid subscription
- **Request Body**: `{ serviceName, cost, paymentMethod, billingCycle, cancellationInstructions }`
- **Events**: `PaidSubscriptionInventoried`

#### POST /api/subscriptions/{id}/cancellation-plan
Plan subscription cancellation
- **Request Body**: `{ cancellationPriority, cancellationMethod, contactInfo }`
- **Events**: `SubscriptionCancellationPlanned`

## Data Models

```csharp
public class PaidSubscription
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public decimal MonthlyCost { get; set; }
    public string PaymentMethod { get; set; }
    public BillingCycle BillingCycle { get; set; }
    public string CancellationInstructions { get; set; }
    public Priority CancellationPriority { get; set; }
}
```
