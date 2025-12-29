# Training Plans - Frontend

## Pages

### Training Plan Dashboard (`/training-plans`)
- Active plan summary
- Current week schedule
- Adherence progress bar
- Upcoming workouts
- Plan milestones

### Workout Calendar (`/training-plans/{id}/calendar`)
- Week-by-week view
- Each day shows workout type and target
- Completed workouts marked
- Tap day to view/log workout

## UI Components

### WeeklySchedule
- 7-day grid showing planned workouts
- Visual indicators: completed, skipped, upcoming
- Tap to view workout details

### WorkoutCard
```typescript
interface WorkoutCard {
  date: Date;
  type: 'Tempo' | 'Intervals' | 'Long Run' | 'Recovery' | 'Rest';
  targetDistance: number;
  targetPace: string;
  completed: boolean;
}
```
