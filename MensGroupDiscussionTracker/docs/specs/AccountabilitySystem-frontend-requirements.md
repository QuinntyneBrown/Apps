# Frontend Requirements - Accountability System

## Pages
- **My Goals** (`/accountability/goals`): Personal accountability goals
- **Check-In** (`/accountability/check-in`): Report progress on goals
- **Partner Dashboard** (`/accountability/partners`): View partner's goals and provide support

## Components
- **GoalCard**: Display goal with progress bar and check-in history
- **CheckInForm**: Report progress, challenges, victories
- **PartnerGoalsList**: View and support partner's goals
- **ProgressTimeline**: Visual timeline of check-ins

## State Management
```typescript
{
  goals: {
    mine: Goal[],
    partnersGoals: Goal[]
  },
  checkIns: CheckIn[],
  partners: Partner[]
}
```

## Key Features
- Check-in reminders
- Progress visualization
- Partner encouragement/comments
- Goal completion celebration
