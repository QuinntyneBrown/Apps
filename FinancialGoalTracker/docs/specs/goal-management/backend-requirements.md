# Backend Requirements - Goal Management

## API Endpoints
- POST /api/goals - Create goal (GoalCreated event)
- PUT /api/goals/{id} - Update goal (GoalUpdated event)
- DELETE /api/goals/{id}/abandon - Abandon goal (GoalAbandoned event)
- PUT /api/goals/{id}/reactivate - Reactivate goal (GoalReactivated event)
- GET /api/goals - List goals (with filters: active/completed/abandoned)
- GET /api/goals/{id} - Get goal details
- PUT /api/goals/{id}/category - Categorize goal (GoalCategorized event)
- PUT /api/goals/{id}/priority - Change priority (PriorityChanged event)

## Domain Models
```csharp
public class Goal {
    public Guid Id { get; set; }
    public string GoalName { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime Deadline { get; set; }
    public string Category { get; set; }
    public int Priority { get; set; }
    public GoalStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
public enum GoalStatus { Active, Achieved, Abandoned }
```

## Business Rules
1. Target amount must be positive
2. Deadline must be in future
3. Goal auto-achieves when CurrentAmount >= TargetAmount
4. Abandoned goals can be reactivated
