# Streak Management - Frontend Requirements

## Overview
The Streak Management UI visualizes streaks, celebrates milestones, shows recovery options, and motivates users through gamification.

## User Interface Components

### 1. Streak Display (Habit Card)
- Flame icon with number (🔥 15)
- Color-coded by streak length:
  - 1-6 days: Orange
  - 7-29 days: Red
  - 30-99 days: Purple
  - 100+ days: Gold
- Animated flame for active streaks
- Pulsing animation for milestone days

### 2. Streak Detail View
- Current streak prominently displayed
- Longest streak badge
- Progress bar to next milestone
- Freeze tokens available
- Streak calendar visualization
- Timeline of breaks and recoveries

### 3. Milestone Celebration Modal
- Confetti animation
- Badge display
- Milestone name and description
- Share buttons (social media)
- Encouraging message
- Next milestone preview

### 4. Streak Break Warning
- Alert before potential streak break
- Time remaining (countdown)
- Quick complete button
- Freeze token option
- Impact explanation

### 5. Streak Freeze Interface
- Available tokens display
- Use freeze button
- Freeze date selector
- Reason input (optional)
- Confirmation dialog

### 6. Streak Recovery Options
- Broken streak notification
- Recovery eligibility check
- Premium upgrade prompt (if needed)
- Confirm recovery button
- New streak start confirmation

### 7. Leaderboard
- Top streaks display
- User's position
- Friend vs global toggle
- Category filter
- Refresh button

## State Management

```typescript
interface StreakState {
  streaks: {[habitId: string]: Streak};
  milestones: Milestone[];
  leaderboard: LeaderboardEntry[];
  loading: boolean;
  celebrating: boolean;
}

interface Streak {
  id: string;
  habitId: string;
  currentStreak: number;
  longestStreak: number;
  startDate: string;
  lastCompletionDate?: string;
  status: 'active' | 'broken' | 'frozen';
  freezeTokens: number;
  nextMilestone: number;
  progressToMilestone: number;
}

interface Milestone {
  days: number;
  name: string;
  description: string;
  icon: string;
  achieved: boolean;
}
```

## Animations
- Flame flicker effect
- Confetti on milestone
- Streak number count-up
- Progress bar fill
- Badge unlock animation

## Notifications
- "3-day streak! Keep going!"
- "Milestone reached: One Week Wonder!"
- "Streak at risk! Complete by 11:59 PM"
- "Streak broken. Start fresh!"

## Accessibility
- Screen reader announces streak updates
- High contrast mode for flame colors
- Keyboard navigation for leaderboard
- Alternative text for badges

## Performance
- Cache streak data
- Optimize calendar rendering
- Lazy load leaderboard
- Efficient milestone calculations
