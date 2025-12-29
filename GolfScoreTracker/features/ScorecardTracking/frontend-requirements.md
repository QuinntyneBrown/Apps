# Scorecard Tracking - Frontend Requirements

## Pages

### Scorecard Entry (`/rounds/{id}/scorecard`)
- Grid layout: Holes 1-18 (or 1-9)
- Each hole shows: Hole #, Par, Score input, Putts (optional)
- Color-coded scores:
  - Eagle: Dark green
  - Birdie: Light green
  - Par: White
  - Bogey: Yellow
  - Double+: Red
- Running total displayed
- Quick entry mode: tap hole, enter score
- Detailed mode: expand for putts, fairways, greens

### Hole Detail Modal
- Hole number and par
- Score selector (large buttons 2-10+)
- Putts counter
- Fairway hit toggle
- GIR toggle
- Penalty strokes counter
- Notes field
- Save/Cancel

## UI Components

### ScorecardGrid
- Responsive table layout
- Color-coded cells based on score
- Tap cell to edit
- Total row at bottom
- Front 9/Back 9 subtotals

### HoleScoreCell
```typescript
interface HoleScoreCell {
  holeNumber: number;
  par: number;
  score?: number;
  putts?: number;
  scoreToPar: number;
  status: 'empty' | 'birdie' | 'par' | 'bogey' | 'double+' | 'eagle';
}
```

### QuickScoreEntry
- Number pad style for fast entry
- Common scores prominent (par-2 to par+3)
- Submit and next hole flow

## Interactions
- Tap hole to open detailed entry
- Swipe between holes
- Auto-save on entry
- Undo last entry
- Copy score from partner (if group play)
