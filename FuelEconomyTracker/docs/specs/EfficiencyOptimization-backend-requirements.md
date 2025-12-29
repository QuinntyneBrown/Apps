# Efficiency Optimization - Backend Requirements

## API Endpoints

### POST /api/driving-analysis/analyze-habits
**Request**: Vehicle ID, analysis period
**Response**: Driving habit insights, efficiency impact
**Domain Events**: `DrivingHabitAnalyzed`

### POST /api/eco-goals
**Request**: Target MPG, timeframe, motivation
**Domain Events**: `EcoDrivingGoalSet`

### POST /api/eco-tips/{id}/apply
**Request**: Tip ID, application date
**Domain Events**: `EcoDrivingTipApplied`

### POST /api/routes/optimize
**Request**: Origin, destination, current route
**Response**: Optimized route suggestions
**Domain Events**: `RouteOptimizationPerformed`

## Domain Models

```csharp
public class DrivingHabit : Entity
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public DateTime AnalysisDate { get; private set; }
    public List<string> AggressiveDrivingIndicators { get; private set; }
    public SpeedPattern SpeedPatterns { get; private set; }
    public AccelerationPattern AccelerationHabits { get; private set; }
    public decimal EfficiencyImpact { get; private set; }
    public List<string> ImprovementSuggestions { get; private set; }
}

public class EcoDrivingGoal : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public decimal TargetMPG { get; private set; }
    public decimal CurrentBaseline { get; private set; }
    public DateTime Deadline { get; private set; }
    public decimal Progress { get; private set; }
}

public class EcoTip : Entity
{
    public Guid Id { get; private set; }
    public string Technique { get; private set; }
    public string Description { get; private set; }
    public decimal ExpectedImpact { get; private set; }
    public List<TipApplication> Applications { get; private set; }
}
```

## Business Logic
- Analyze acceleration patterns for aggressiveness
- Detect excessive idling
- Identify sub-optimal speed ranges
- Calculate potential MPG improvement from tips
- Track effectiveness of applied techniques
- Generate personalized recommendations

## Database Schema

### driving_habits
- id, vehicle_id, analysis_date, aggressive_indicators
- speed_patterns, acceleration_patterns, efficiency_impact
- improvement_suggestions

### eco_goals
- id, vehicle_id, target_mpg, baseline, deadline
- progress, status, motivation

### eco_tips
- id, technique, description, expected_impact, category

### tip_applications
- id, eco_tip_id, vehicle_id, application_date
- observed_impact, effectiveness_rating, continued_use
