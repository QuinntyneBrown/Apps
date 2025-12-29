# Trip Planning - Backend Requirements

## Domain Events
- CampingTripPlanned, CampsiteResearched, CampsiteReserved, TripItineraryCreated, TripCancelled

## API Endpoints
- `POST /api/trips` - Create trip
- `GET /api/trips` - List trips
- `POST /api/trips/{id}/itinerary` - Create itinerary
- `POST /api/trips/{id}/reservation` - Book campsite

## Data Models
```csharp
Trip {
    id: Guid
    destination: string
    plannedDates: DateRange
    tripType: TripType
    groupSize: int
    difficulty: Difficulty
}
```
