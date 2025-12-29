# Run Logging - Frontend

## Pages

### Active Run (`/runs/active`)
- Live stats: Distance, Time, Current Pace
- Map with GPS route
- Audio cues at mile markers
- Pause/Resume/Stop buttons
- Heart rate display (if connected)

### Run History (`/runs`)
- Calendar view and list view toggle
- Each run: date, distance, pace, time
- Color-coded by pace (fast/average/slow)
- Filter by workout type
- Weekly/monthly summary cards

### Run Details (`/runs/{id}`)
- Route map with elevation profile
- Stats breakdown
- Weather conditions
- Splits per mile
- Edit/Delete options

## UI Components

```typescript
interface ActiveRunDisplay {
  distance: number;
  elapsedTime: string;
  currentPace: string;
  averagePace: string;
  heartRate?: number;
}

interface RunCard {
  id: string;
  date: Date;
  distance: number;
  duration: string;
  pace: string;
  type: WorkoutType;
}
```
