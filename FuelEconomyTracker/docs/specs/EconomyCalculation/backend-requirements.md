# Economy Calculation - Backend Requirements

## API Endpoints

### GET /api/vehicles/{vehicleId}/economy-stats
Get fuel economy statistics for a vehicle.

**Response**: 200 OK
```json
{
  "currentAverage": "decimal",
  "personalBest": {
    "mpg": "decimal",
    "date": "datetime",
    "tripDetails": "string"
  },
  "last30DaysAverage": "decimal",
  "lifetimeAverage": "decimal",
  "epaComparison": {
    "epaCity": "decimal",
    "epaHighway": "decimal",
    "epaCombined": "decimal",
    "actualVsEpa": "decimal"
  },
  "trend": "enum (improving, declining, stable)"
}
```

### GET /api/vehicles/{vehicleId}/economy-history
Get MPG calculations over time.

**Query Parameters**:
- `period` (enum: week, month, year, all)
- `granularity` (enum: daily, weekly, monthly)

**Response**: Time series data
```json
{
  "data": [
    {
      "date": "datetime",
      "mpg": "decimal",
      "milesDriven": "integer",
      "gallonsUsed": "decimal"
    }
  ]
}
```

### POST /api/economy-goals
Set fuel economy improvement goal.

**Request Body**:
```json
{
  "vehicleId": "string (uuid)",
  "targetMPG": "decimal",
  "baseline": "decimal",
  "timeframe": "integer (days)",
  "motivation": "string"
}
```

**Domain Events Triggered**:
- `EcoDrivingGoalSet`

## Domain Models

### EconomyCalculation
```csharp
public class EconomyCalculation : Entity
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Guid FuelPurchaseId { get; private set; }
    public decimal MPG { get; private set; }
    public int MilesDriven { get; private set; }
    public decimal GallonsUsed { get; private set; }
    public DateTime CalculationDate { get; private set; }
    public TripType TripType { get; private set; }
    public WeatherConditions Weather { get; private set; }
    public DrivingConditions DrivingConditions { get; private set; }

    public void Calculate(FuelPurchase current, FuelPurchase previous)
    {
        MilesDriven = current.OdometerReading - previous.OdometerReading;
        GallonsUsed = current.Gallons;
        MPG = (decimal)MilesDriven / GallonsUsed;

        RaiseDomainEvent(new FuelEconomyCalculated(...));
    }
}
```

### RunningAverage
```csharp
public class RunningAverage : Entity
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public decimal CurrentAverage { get; private set; }
    public decimal PreviousAverage { get; private set; }
    public DateTime UpdateDate { get; private set; }
    public TimePeriod Period { get; private set; }
    public TrendDirection Trend { get; private set; }
    public decimal EPAComparison { get; private set; }

    public void Update(EconomyCalculation newCalculation)
    {
        PreviousAverage = CurrentAverage;
        // Calculate new rolling average
        RaiseDomainEvent(new RunningAverageUpdated(...));
    }
}
```

## Business Logic

### MPG Calculation Algorithm
```csharp
public class MPGCalculator
{
    public decimal Calculate(FuelPurchase current, FuelPurchase previous)
    {
        // Skip partial fills
        if (current.IsPartialFill || previous.IsPartialFill)
            return null;

        var miles = current.OdometerReading - previous.OdometerReading;
        var gallons = current.Gallons;

        if (miles <= 0 || gallons <= 0)
            throw new InvalidCalculationException();

        var mpg = (decimal)miles / gallons;

        // Validate reasonable range
        if (mpg < 5 || mpg > 100)
            throw new SuspiciousMPGException(mpg);

        return Math.Round(mpg, 2);
    }
}
```

### Running Average Calculation
- Use rolling 30-day average for short-term trends
- Use all-time average for baseline
- Weight recent calculations more heavily (exponential moving average)
- Exclude outliers (>2 standard deviations from mean)

### Personal Best Detection
- Check if new MPG exceeds all previous calculations
- Must be based on valid data (minimum 50 miles driven)
- Compare against vehicle type expectations
- Trigger celebration/notification

### Decline Detection
- Compare current average to 30-day rolling average
- Flag if decline > 15%
- Check against EPA rating as baseline
- Trigger investigation alert

## Domain Events

### FuelEconomyCalculated
```csharp
public class FuelEconomyCalculated : DomainEvent
{
    public Guid CalculationId { get; set; }
    public Guid VehicleId { get; set; }
    public decimal MPG { get; set; }
    public int MilesDriven { get; set; }
    public decimal GallonsUsed { get; set; }
    public DateTime CalculationDate { get; set; }
    public string TripType { get; set; }
}
```

### RunningAverageUpdated
```csharp
public class RunningAverageUpdated : DomainEvent
{
    public Guid VehicleId { get; set; }
    public decimal CurrentAverage { get; set; }
    public decimal PreviousAverage { get; set; }
    public string TrendDirection { get; set; }
    public decimal EPAComparison { get; set; }
}
```

### PersonalBestMPGAchieved
```csharp
public class PersonalBestMPGAchieved : DomainEvent
{
    public Guid RecordId { get; set; }
    public Guid VehicleId { get; set; }
    public decimal MPGAchieved { get; set; }
    public DateTime DateAchieved { get; set; }
    public decimal BeatPreviousBy { get; set; }
    public string TripDetails { get; set; }
}
```

### FuelEconomyDeclined
```csharp
public class FuelEconomyDeclined : DomainEvent
{
    public Guid DeclineId { get; set; }
    public Guid VehicleId { get; set; }
    public decimal CurrentMPG { get; set; }
    public decimal ExpectedMPG { get; set; }
    public decimal DeclinePercentage { get; set; }
    public List<string> PotentialCauses { get; set; }
}
```

## Event Handlers

### When FuelEconomyCalculated
1. Update running averages (30-day, 90-day, lifetime)
2. Check for personal best
3. Check for decline conditions
4. Update economy trend indicators
5. Recalculate goal progress

### When PersonalBestMPGAchieved
1. Update vehicle personal best record
2. Send congratulatory notification
3. Log achievement for history
4. Update leaderboard if applicable
5. Suggest sharing on social media

### When FuelEconomyDeclined
1. Analyze potential causes (maintenance due, weather, driving habits)
2. Send alert to user
3. Recommend diagnostic actions
4. Schedule maintenance check reminder

## Database Schema

### economy_calculations
- id (uuid, PK)
- vehicle_id (uuid, FK)
- fuel_purchase_id (uuid, FK)
- mpg (decimal)
- miles_driven (integer)
- gallons_used (decimal)
- calculation_date (timestamp)
- trip_type (varchar, nullable)
- weather_conditions (varchar, nullable)
- driving_conditions (varchar, nullable)
- created_at (timestamp)

**Indexes**:
- idx_economy_vehicle_date (vehicle_id, calculation_date DESC)
- idx_economy_mpg (vehicle_id, mpg DESC)

### running_averages
- id (uuid, PK)
- vehicle_id (uuid, FK, unique)
- current_average_30day (decimal)
- current_average_90day (decimal)
- current_average_lifetime (decimal)
- previous_average_30day (decimal)
- trend_direction (varchar)
- epa_city (decimal, nullable)
- epa_highway (decimal, nullable)
- epa_combined (decimal, nullable)
- last_updated (timestamp)

### personal_bests
- id (uuid, PK)
- vehicle_id (uuid, FK)
- best_mpg (decimal)
- achievement_date (timestamp)
- trip_details (text, nullable)
- miles_driven (integer)
- conditions (text, nullable)
