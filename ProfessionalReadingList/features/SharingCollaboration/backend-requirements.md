# Backend Requirements - Sharing and Collaboration

## Domain Events
- ReadingRecommendationCreated
- ReadingListShared
- DiscussionStarted

## Key API Endpoints
- POST /api/recommendations - Create recommendation
- POST /api/reading-lists/share - Share list
- POST /api/discussions - Start discussion
- GET /api/recommendations/received - Get recommendations

## Data Models
```csharp
public class Recommendation : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid MaterialId { get; private set; }
    public Guid RecommenderId { get; private set; }
    public List<Guid> RecipientIds { get; private set; }
    public string Reason { get; private set; }
}
```
