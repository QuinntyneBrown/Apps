# Seasonal Analysis - Backend Requirements

## API Endpoints

### GET /api/vehicles/{vehicleId}/seasonal-patterns
**Response**: Seasonal MPG patterns, weather correlations

### POST /api/weather-impacts
**Request**: Weather conditions, MPG observation
**Domain Events**: `WeatherImpactRecorded`

### POST /api/fuel-blend-assessments
**Request**: Ethanol percentage, MPG result
**Domain Events**: `EthanolBlendImpactAssessed`

## Domain Models

```csharp
public class SeasonalPattern : Entity
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Season Season { get; private set; }
    public decimal AverageMPG { get; private set; }
    public decimal VarianceFromAnnual { get; private set; }
    public List<string> ContributingFactors { get; private set; }
    public decimal PatternStrength { get; private set; }
}

public class WeatherImpact : Entity
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public DateTime Date { get; private set; }
    public WeatherConditions Conditions { get; private set; }
    public decimal Temperature { get; private set; }
    public decimal MPGImpact { get; private set; }
}
```

## Business Logic
- Detect seasonal patterns (summer/winter efficiency)
- Correlate weather with MPG changes
- Account for winter fuel blends
- Adjust expectations based on season
- Compare ethanol blend impacts

## Database Schema

### seasonal_patterns
- id, vehicle_id, season, average_mpg
- variance_from_annual, contributing_factors, pattern_strength

### weather_impacts
- id, vehicle_id, date, weather_conditions, temperature
- mpg_impact, driving_adjustments, severity
