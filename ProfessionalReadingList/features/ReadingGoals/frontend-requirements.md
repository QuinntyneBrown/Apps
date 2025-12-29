# Frontend Requirements - Reading Goals

## Key Components

### Goal Setting
- Goal type selector
- Target metric input
- Time frame picker
- Progress tracking toggle

### Goals Dashboard
- Active goals list
- Progress bars
- Achievement badges
- Milestone celebrations

### Reading Streaks
- Streak counter
- Calendar heatmap
- Streak notifications

## State Management
```typescript
interface GoalsState {
  activeGoals: ReadingGoal[];
  milestones: Milestone[];
  currentStreak: number;
  longestStreak: number;
}
```
