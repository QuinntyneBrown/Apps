# Round Management - Frontend Requirements

## Pages

### Start Round (`/rounds/start`)
- Course selector with search
- Tee box selection (Championship, Blue, White, Red)
- Playing partners selector
- Weather auto-detect with override
- Start Round button

### Active Round (`/rounds/{id}/active`)
- Current hole indicator (Hole 5 of 18)
- Scorecard grid (all holes visible)
- Current score vs par
- Quick score entry for current hole
- Navigation: Previous/Next Hole
- Pause/Resume/Abandon actions

### Round Summary (`/rounds/{id}`)
- Total score (large display)
- Score to par badge
- Hole-by-hole breakdown table
- Statistics summary (fairways, greens, putts)
- Edit score button
- Share round button

### Round History (`/rounds`)
- Filterable list by course, date
- Each round shows: date, course, score, par differential
- Personal best badge
- Search and sort options

## UI Components

```typescript
interface RoundCard {
  roundId: string;
  course: string;
  date: Date;
  score: number;
  par: number;
  isPersonalBest: boolean;
}

interface ScorecardView {
  holes: HoleScore[];
  currentHole: number;
  totalScore: number;
  parTotal: number;
}
```

## State Management

```typescript
interface RoundState {
  activeRound?: ActiveRound;
  roundHistory: Round[];
  courses: Course[];
  loading: boolean;
}
```
