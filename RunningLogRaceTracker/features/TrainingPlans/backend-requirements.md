# Training Plans - Backend

## API Endpoints

### POST /api/training-plans
Create/select training plan for race

### GET /api/training-plans/{id}/schedule
Get weekly workout schedule

### POST /api/training-plans/{id}/workouts/{workoutId}/complete
Mark workout as completed
Domain Events: WorkoutCompleted, TrainingPlanCompleted

## Domain Model

```csharp
public class TrainingPlan : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public RaceDistance GoalRace { get; private set; }
    public int WeeklyMileageTarget { get; private set; }
    public List<Workout> Workouts { get; private set; }
    public decimal AdherencePercentage { get; private set; }

    public void CompleteWorkout(Workout workout)
    {
        workout.MarkComplete();
        RecalculateAdherence();
        RaiseDomainEvent(new WorkoutCompleted(...));
    }
}
```

## Database Schema
### training_plans
- id, user_id, name, goal_race, start_date
- weekly_mileage, adherence_percentage, status

### workouts
- id, training_plan_id, week_number, day_number
- workout_type, target_distance, target_pace
- completed, completion_date
