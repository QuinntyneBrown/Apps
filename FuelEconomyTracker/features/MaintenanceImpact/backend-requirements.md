# Maintenance Impact - Backend Requirements

## API Endpoints

### POST /api/maintenance-events
Create maintenance record and track MPG impact

### GET /api/vehicles/{vehicleId}/maintenance-impact
Analyze correlation between maintenance and economy

### POST /api/tire-pressure-checks
**Domain Events**: `TireInflationCorrected`

### POST /api/air-filter-replacements
**Domain Events**: `AirFilterReplaced`

## Domain Models

```csharp
public class MaintenanceEvent : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public string ServiceType { get; private set; }
    public DateTime ServiceDate { get; private set; }
    public int OdometerReading { get; private set; }
    public decimal Cost { get; private set; }
    public decimal? MPGBefore { get; private set; }
    public decimal? MPGAfter { get; private set; }
    public decimal? ImprovementPercentage { get; private set; }
    public decimal? ROI { get; private set; }

    public void CalculateImpact(decimal currentMPG, decimal previousAverageMPG)
    {
        MPGAfter = currentMPG;
        MPGBefore = previousAverageMPG;
        ImprovementPercentage = ((MPGAfter - MPGBefore) / MPGBefore) * 100;
        RaiseDomainEvent(new MaintenanceImpactedEconomy(...));
    }
}
```

## Business Logic
- Track MPG 2 weeks before and after maintenance
- Calculate ROI based on fuel savings
- Recommend maintenance based on MPG decline
- Correlate specific services with economy improvements

## Database Schema

### maintenance_events
- id, vehicle_id, service_type, service_date, odometer_reading
- cost, mpg_before, mpg_after, improvement_percentage, roi
- notes, next_service_due
