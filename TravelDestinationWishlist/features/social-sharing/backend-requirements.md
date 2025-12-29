# Backend Requirements - Social and Sharing

## API Endpoints
- POST /api/recommendations - Receive recommendation (TripRecommendationReceived)
- POST /api/share - Share story (TravelStoryShared)
- POST /api/trips/{id}/invite - Invite buddy (TravelBuddyInvited)

## Models
```csharp
public class TravelRecommendation {
    public Guid Id;
    public string Destination;
    public string Recommender;
    public string Reason;
    public bool AddedToWishlist;
}
```
