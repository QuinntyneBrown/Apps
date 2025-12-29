# Backend Requirements - Memory Documentation

## API Endpoints
- POST /api/trips/{id}/photos - Upload photo (TripPhotoUploaded)
- POST /api/trips/{id}/journal - Write entry (TravelJournalEntryWritten)
- POST /api/trips/{id}/interactions - Record interaction (MemorableInteractionRecorded)
- POST /api/trips/{id}/highlights - Mark highlight (TripHighlightMarked)

## Models
```csharp
public class TravelPhoto {
    public Guid Id;
    public Guid TripId;
    public string PhotoUrl;
    public string Location;
    public DateTime PhotoDate;
    public string Caption;
    public List<string> Tags;
}
```
