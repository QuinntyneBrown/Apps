# Backend Requirements - Trip Planning

## API Endpoints
- POST /api/trips - Plan trip (TripPlanned)
- POST /api/trips/{id}/book - Book trip (TripBooked)
- POST /api/trips/{id}/itinerary - Create itinerary (ItineraryCreated)
- PUT /api/trips/{id}/reschedule - Reschedule (TripRescheduled)
- DELETE /api/trips/{id} - Cancel (TripCancelled)

## Models
```csharp
public class Trip {
    public Guid Id;
    public List<Guid> DestinationIds;
    public DateTime StartDate;
    public DateTime EndDate;
    public List<string> Companions;
    public decimal BudgetEstimate;
    public TripStatus Status;
}
```
