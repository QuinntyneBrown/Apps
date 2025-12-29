# Frontend Requirements - Networking Goals

## User Interface Components

### Goals Dashboard
**Route**: `/goals`

**Display**:
- Active goals with progress bars
- Achievement badges
- Goal history
- Suggested new goals

### Set Goal Form
**Fields**:
- Goal type
- Target number
- Target date
- Motivation/why

### Progress Tracker
**Widgets**:
- Daily/weekly activity tracking
- Progress toward goals
- Streak counter
- Encouragement messages

## State Management

```typescript
interface GoalsState {
  activeGoals: NetworkingGoal[];
  completedGoals: NetworkingGoal[];
  progress: Map<string, number>;
  streaks: {
    current: number;
    longest: number;
  };
}
```
