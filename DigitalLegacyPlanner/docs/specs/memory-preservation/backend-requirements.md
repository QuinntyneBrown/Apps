# Memory Preservation - Backend Requirements

## API Endpoints

#### POST /api/memories/preservation-plan
Create memory preservation plan
- **Request Body**: `{ contentToPreserve, storageMethod, familyAccess }`
- **Events**: `MemoryPreservationPlanCreated`

#### POST /api/memories/stories
Record personal stories
- **Request Body**: `{ narrativeContent, audience, deliveryTiming }`
- **Events**: `StoriesRecorded`

## Data Models

```csharp
public class MemoryPreservationPlan
{
    public Guid Id { get; set; }
    public List<string> ContentToPreserve { get; set; }
    public string StorageMethod { get; set; }
    public List<Guid> FamilyMembersWithAccess { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class PersonalStory
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string NarrativeContent { get; set; }
    public List<Guid> IntendedAudience { get; set; }
    public DeliveryTiming Timing { get; set; }
}
```
