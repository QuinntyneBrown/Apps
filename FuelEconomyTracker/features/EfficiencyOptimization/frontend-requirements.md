# Efficiency Optimization - Frontend Requirements

## Pages

### Eco-Driving Dashboard (`/eco-driving`)
- **Current Goal Card**: Progress toward MPG goal
- **Driving Habits Analysis**: Scores for acceleration, speed, idling
- **Active Tips**: Tips currently being applied
- **Suggested Tips**: Personalized recommendations
- **Route Optimizer**: Quick access to route planning

### Driving Habits Report (`/eco-driving/habits`)
- **Habit Scores**: Visual meters for different aspects
- **Comparison to Ideal**: Where you stand vs optimal driving
- **Impact Assessment**: How habits affect MPG
- **Improvement Actions**: Specific things to try

### Eco-Tips Library (`/eco-driving/tips`)
- **Browse Tips**: Categorized by impact level
- **Filter**: By difficulty, expected savings
- **Tip Details**: Description, how-to, expected impact
- **Track Application**: Mark tips as "trying" or "adopted"
- **Effectiveness Tracker**: Log results of applied tips

### Route Optimizer (`/eco-driving/routes`)
- **Route Input**: Origin, destination
- **Current Route**: Default/usual route
- **Optimized Suggestions**: Alternative routes with estimated MPG
- **Compare Routes**: Side-by-side comparison
- **Save Optimal Routes**: For regular trips

### Goal Setting Modal
- Target MPG slider
- Baseline display (current average)
- Timeframe selector
- Estimated savings calculation
- Motivation note field

## UI Components

### HabitScoreMeter
- Radial progress indicator
- Color-coded (green = good, yellow = moderate, red = poor)
- Specific score (0-100)
- Improvement tip tooltip

### TipCard
- Tip title and category icon
- Expected MPG improvement badge
- Difficulty level
- "Try This" button
- Effectiveness ratings from other users

### GoalProgressTracker
- Current vs target MPG
- Progress percentage
- Days remaining
- Trend indicator
- Motivational messages

## State Management

```typescript
interface EcoOptimizationState {
  currentGoal?: EcoGoal;
  drivingHabits: {
    acceleration: HabitScore;
    speed: HabitScore;
    idling: HabitScore;
    overall: HabitScore;
  };
  tips: {
    suggested: EcoTip[];
    active: EcoTip[];
    completed: EcoTip[];
  };
  optimizedRoutes: Route[];
}
```

## Features
- Gamification with achievement badges
- Social comparison (anonymous aggregates)
- Weekly challenges for eco-driving
- Push notifications with tips
- Integration with navigation apps
