# Activities - Backend Requirements

## API Endpoints
- GET /api/activities - Get activity library
- POST /api/activities - Create custom activity
- PUT /api/activities/{id} - Update activity
- GET /api/activities/categories - Get categories

## Domain Events
- ActivityCreated
- ActivityCompleted
- ActivitySkipped
- ActivityModified

## Data Models
```csharp
public class Activity {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ActivityCategory Category { get; set; }
    public int SuggestedDurationMinutes { get; set; }
    public string IconName { get; set; }
    public List<string> Benefits { get; set; }
}

public enum ActivityCategory {
    Wellness, Exercise, Nutrition,
    Productivity, SelfCare, Mindfulness
}
```
