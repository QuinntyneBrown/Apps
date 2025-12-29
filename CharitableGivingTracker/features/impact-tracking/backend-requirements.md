# Impact Tracking - Backend Requirements

## Overview
Tracks giving goals, milestones, and charitable impact measurement.

## Domain Model

### GivingGoal Aggregate
- **GoalId**: Unique identifier (Guid)
- **UserId**: User ID (Guid)
- **TargetAmount**: Goal amount (decimal)
- **TaxYear**: Goal year (int)
- **CurrentAmount**: Amount donated so far (decimal)
- **IsReached**: Goal reached status (bool)
- **ReachedDate**: When goal achieved (DateTime, nullable)

## Commands

### SetAnnualGivingGoalCommand
- Creates new giving goal for year
- Raises **AnnualGivingGoalSet** event

### TrackGoalProgressCommand
- Updates CurrentAmount
- Checks if goal reached
- Raises **GivingGoalReached** if achieved

## Domain Events

### AnnualGivingGoalSet
```csharp
public class AnnualGivingGoalSet : DomainEvent
{
    public Guid GoalId { get; set; }
    public decimal TargetAmount { get; set; }
    public int TaxYear { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### GivingGoalReached
```csharp
public class GivingGoalReached : DomainEvent
{
    public Guid GoalId { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal ActualAmount { get; set; }
    public DateTime AchievementDate { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/impact/goals
- Sets annual giving goal

### GET /api/impact/goals/{year}
- Gets goal for year with progress

### GET /api/impact/milestones
- Gets achieved milestones
