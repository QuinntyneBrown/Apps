# Frontend Requirements - Goal Management

## Pages/Views
### 1. Goals Dashboard (`/goals`)
- GoalsGrid (active, completed, abandoned tabs)
- CreateGoalButton
- GoalCard with progress, deadline, priority

### 2. Create/Edit Goal Modal
- Goal name, target amount, deadline, category, priority inputs
- Impact preview (monthly contribution needed)

## UI Components
- GoalCard: Shows name, progress bar, target vs current, deadline countdown
- GoalStatusBadge: Visual indicator (active/achieved/abandoned)
- PrioritySelector: 1-5 stars

## State Management
```typescript
interface GoalState {
  goals: Goal[];
  activeGoals: Goal[];
  completedGoals: Goal[];
  abandonedGoals: Goal[];
}
```
