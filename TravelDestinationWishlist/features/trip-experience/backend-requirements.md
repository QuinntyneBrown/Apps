# Backend Requirements - Trip Experience

## API Endpoints
- PUT /api/trips/{id}/start - Start trip (TripStarted)
- POST /api/trips/{id}/attractions - Log attraction (AttractionVisited)
- POST /api/trips/{id}/experiences - Log experience (LocalExperienceEnjoyed)
- POST /api/trips/{id}/accommodations/review - Review stay (AccommodationReviewed)

## Models
```csharp
public class AttractionVisit {
    public Guid Id;
    public Guid TripId;
    public string AttractionName;
    public DateTime VisitDate;
    public decimal EntranceCost;
    public int Rating;
    public string Highlights;
}
```
