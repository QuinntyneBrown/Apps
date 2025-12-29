# Goals & Achievements - Frontend Requirements

## Pages

### Goals Dashboard (`/goals`)
- **Active Goals**: List with progress bars
- **Set New Goal** button
- **Completed Goals**: Archive
- **Suggested Goals**: Based on performance

### Set Goal Modal
- Goal type selector (Break 90, Reach Handicap, etc.)
- Target value input
- Deadline picker
- Motivation text
- Progress forecast

### Achievements Gallery (`/achievements`)
- Grid of achievement cards
- Unlocked achievements highlighted
- Locked achievements grayed out
- Tap to view details
- Share achievement button

### Achievement Detail Modal
- Achievement icon (large)
- Name and description
- Unlock date
- Associated round (if applicable)
- Share to social media

## UI Components

### GoalCard
```typescript
interface GoalCard {
  goalId: string;
  type: string;
  target: number;
  current: number;
  progress: number; // percentage
  deadline: Date;
  onTrack: boolean;
}
```

### GoalProgressBar
- Visual progress indicator
- Percentage complete
- Days remaining
- On track indicator

### AchievementBadge
- Icon with badge design
- Unlock animation
- Shine/glow effect for new
- Tap for details
