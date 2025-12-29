# Handicap Management - Frontend Requirements

## Pages

### Handicap Dashboard (`/handicap`)
- **Hero Display**: Current handicap (large)
- **Trend Indicator**: Up/down arrow with change amount
- **Trend Chart**: Handicap over time (line graph)
- **Rounds Used**: List of 8 rounds in calculation
- **Requirements**: Progress to official handicap (need 20 rounds)
- **Achievement Badge**: Single-digit milestone if applicable

### Handicap History (`/handicap/history`)
- Timeline of handicap changes
- Each entry: date, handicap, change from previous
- Filter by date range
- Export handicap certification

## UI Components

### HandicapDisplay
```typescript
interface HandicapDisplay {
  currentIndex: number;
  previousIndex: number;
  trend: 'improving' | 'stable' | 'increasing';
  change: number;
  lastUpdated: Date;
  roundsUsed: number;
  roundsNeeded: number;
}
```

### HandicapTrendChart
- Line chart showing handicap evolution
- Highlight milestones (single-digit achievement)
- Annotations for significant changes

### RoundsForHandicap
- List of 8 rounds used in calculation
- Each showing: date, course, score, differential
- Highlight best rounds
