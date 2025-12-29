# Frontend Requirements - Reading Goals

## Pages/Views

### 1. Goals Dashboard (`/goals`)
- ActiveGoalsGrid
- GoalProgressCards
- CreateGoalButton
- MilestonesTimeline
- ReadingStreakWidget

### 2. Create Goal Modal
- GoalTypeSelector (Books/Pages/Genre)
- TargetNumberInput
- TimeframeSelector (Month/Quarter/Year)
- GenreConstraints (optional multi-select)
- DateRangePicker

### 3. Challenges Page (`/challenges`)
- AvailableChallengesList
- JoinedChallengesList
- ChallengeProgressCards
- ChallengeLeaderboard

### 4. Achievements Page (`/achievements`)
- MilestoneBadges grid
- AchievementTimeline
- StatisticsComparison
- ShareButton

## UI Components

### GoalProgressCard
- Goal type icon
- Progress bar with percentage
- Target vs Current display
- Days remaining
- On-track indicator
- Required pace calculator

### MilestoneBadge
- Badge icon with tier color
- Milestone description
- Achievement date
- Share button

### ReadingStreakWidget
- Current streak flame icon
- Streak count
- Longest streak record
- Calendar heatmap
- Motivational message

## State Management
```typescript
interface GoalsState {
  activeGoals: ReadingGoal[];
  completedGoals: ReadingGoal[];
  challenges: ReadingChallenge[];
  milestones: Milestone[];
  streak: ReadingStreak;
}
```

## Actions
- createGoal(goal)
- updateGoalProgress(goalId)
- joinChallenge(challengeId)
- fetchMilestones()
- fetchStreak()
