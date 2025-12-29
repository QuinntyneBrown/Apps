# Trip Management - Backend Requirements

## Domain Events
- **TripPlanned**: Fishing trip scheduled
- **TripStarted**: Trip commenced
- **TripCompleted**: Trip ended
- **TripCancelled**: Trip cancelled

## API Endpoints
- `POST /api/trips` - Create trip
- `GET /api/trips` - List trips
- `POST /api/trips/{id}/start` - Start trip
- `POST /api/trips/{id}/complete` - End trip
- `DELETE /api/trips/{id}` - Cancel trip

## Data Models
```csharp
Trip {
    id: Guid
    userId: Guid
    plannedDate: DateTime
    actualStartTime: DateTime?
    endTime: DateTime?
    locationId: Guid
    targetSpecies: string[]
    weatherConditions: WeatherData
    waterTemp: decimal?
    status: TripStatus
}
```

## Business Rules
- Cannot start trip before planned date
- Trip duration must be > 0
- Weather conditions required for completed trips
- Total catches must match catch log count
