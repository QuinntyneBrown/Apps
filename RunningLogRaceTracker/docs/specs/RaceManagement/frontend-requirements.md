# Race Management - Frontend

## Pages

### Race Calendar (`/races`)
- Upcoming races with countdown
- Past races with results
- Add race button

### Race Details (`/races/{id}`)
- Race info (name, date, location, distance)
- Goal time setting
- Training plan link
- Countdown timer
- Post-race: results and analysis

## UI Components

### RaceCard
```typescript
interface RaceCard {
  name: string;
  date: Date;
  distance: string;
  goalTime?: string;
  finishTime?: string;
  daysUntil?: number;
}
```

### RaceCountdown
- Days/hours until race
- Training plan status
- Readiness indicator
