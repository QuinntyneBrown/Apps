# Trip Tracking - Backend Requirements

## API Endpoints

### POST /api/trips/start
**Request Body**:
```json
{
  "vehicleId": "uuid",
  "startTime": "datetime",
  "startingOdometer": "integer",
  "tripPurpose": "enum (Commute, Business, Leisure, Other)",
  "routePlanned": "string (optional)",
  "weatherConditions": "string (optional)"
}
```
**Domain Events**: `TripStarted`

### POST /api/trips/{id}/complete
**Request Body**:
```json
{
  "endTime": "datetime",
  "endingOdometer": "integer",
  "tripDuration": "integer (minutes)",
  "averageSpeed": "decimal (optional)",
  "tripMPGEstimate": "decimal (optional)",
  "drivingConditions": "string",
  "notes": "string (optional)"
}
```
**Domain Events**: `TripCompleted`, `LongDistanceTripLogged` (if distance > threshold), `CityDrivingSessionTracked` (if city driving)

### GET /api/trips
**Query Parameters**: vehicleId, startDate, endDate, purpose, page, pageSize

**Response**: Paginated trip list with summaries

## Domain Models

```csharp
public class Trip : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public int StartingOdometer { get; private set; }
    public int? EndingOdometer { get; private set; }
    public int? MilesDriven { get; private set; }
    public TripPurpose Purpose { get; private set; }
    public string RoutePlanned { get; private set; }
    public WeatherConditions Weather { get; private set; }
    public DrivingConditions Conditions { get; private set; }
    public decimal? EstimatedMPG { get; private set; }
    public TripStatus Status { get; private set; }

    public void Complete(int endingOdometer, DateTime endTime, DrivingConditions conditions)
    {
        EndTime = endTime;
        EndingOdometer = endingOdometer;
        MilesDriven = endingOdometer - StartingOdometer;
        Status = TripStatus.Completed;
        Conditions = conditions;

        RaiseDomainEvent(new TripCompleted(...));

        if (MilesDriven > 200)
            RaiseDomainEvent(new LongDistanceTripLogged(...));
    }
}
```

## Business Logic
- Trips cannot end before they start
- Ending odometer must be > starting odometer
- Long distance trip threshold: 200+ miles
- City driving: >70% city indicator
- Auto-categorize based on time/route patterns
- Estimate MPG based on historical data for similar trips

## Database Schema

### trips
- id (uuid, PK)
- user_id (uuid, FK)
- vehicle_id (uuid, FK)
- start_time (timestamp)
- end_time (timestamp, nullable)
- starting_odometer (integer)
- ending_odometer (integer, nullable)
- miles_driven (integer, nullable)
- trip_purpose (varchar)
- route_planned (text, nullable)
- weather_conditions (varchar, nullable)
- driving_conditions (varchar, nullable)
- estimated_mpg (decimal, nullable)
- highway_percentage (integer, nullable)
- status (varchar)
- notes (text, nullable)
- created_at (timestamp)

**Indexes**:
- idx_trips_vehicle_date (vehicle_id, start_time DESC)
- idx_trips_purpose (trip_purpose)
- idx_trips_status (status)
