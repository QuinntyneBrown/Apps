# Backend Requirements - Organization and Timeline

## API Endpoints
- GET /api/goals/categories/{category}/progress - Category progress (CategoryProgressCalculated)
- POST /api/goals/{id}/deadline/extend - Extend deadline (DeadlineExtended)
- GET /api/goals/timeline - Get timeline view (DeadlineApproaching, DeadlineMissed)
- GET /api/achievements - Get personal records (StreakAchieved, PersonalRecordSet)

## Domain Models
```csharp
public class CategoryProgress {
    public string CategoryName { get; set; }
    public decimal TotalTarget { get; set; }
    public decimal TotalSaved { get; set; }
    public int GoalCount { get; set; }
}
```
