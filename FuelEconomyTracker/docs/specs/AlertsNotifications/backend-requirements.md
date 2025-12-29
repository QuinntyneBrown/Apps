# Alerts & Notifications - Backend Requirements

## API Endpoints

### GET /api/alerts/active
Get active alerts for user

### POST /api/alert-preferences
Configure notification preferences

### POST /api/fuel-price-alerts
**Request**: Location, price threshold
**Domain Events**: `FuelPriceSpikeDetected` (when triggered)

## Domain Models

```csharp
public class Alert : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public AlertType Type { get; private set; }
    public string Message { get; private set; }
    public AlertSeverity Severity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsRead { get; private set; }
    public Dictionary<string, object> Data { get; private set; }
}

public enum AlertType
{
    FuelPriceSpike,
    LowFuel,
    BudgetThreshold,
    MPGDecline,
    MaintenanceDue,
    MilestoneAchieved
}
```

## Business Logic
- Monitor fuel prices in user's area
- Alert when fuel level estimated low
- Notify at budget thresholds (75%, 90%, 100%)
- Alert on significant MPG decline
- Celebrate mileage milestones
- Remind of maintenance

## Domain Events

### FuelPriceSpikeDetected
```csharp
public class FuelPriceSpikeDetected : DomainEvent
{
    public string Location { get; set; }
    public decimal PriceIncrease { get; set; }
    public decimal NewPrice { get; set; }
    public string FillUpTimingSuggestion { get; set; }
}
```

### LowFuelWarning
```csharp
public class LowFuelWarning : DomainEvent
{
    public Guid VehicleId { get; set; }
    public int EstimatedRange { get; set; }
    public List<NearbyStation> NearbyStations { get; set; }
}
```

## Database Schema

### alerts
- id, user_id, vehicle_id, alert_type, message
- severity, created_at, is_read, data (json)

### alert_preferences
- id, user_id, alert_type, enabled, delivery_method
- threshold_values (json)
