# Goal Management - Frontend Requirements

## Overview
User interface components for managing focus goals including daily and weekly targets, progress tracking, achievement history, and streak visualization.

---

## Pages

### Goal Dashboard Page (`/goals`)
**Purpose**: Main goal management interface

**Components**:
- Current Day Goal Card
- Current Week Goal Card
- Quick Goal Setup
- Achievement Stats Overview
- Streak Display
- Recent Achievements List

**States**:
- No goals set
- Goals active with progress
- Goals achieved
- Goals failed

---

### Goal History Page (`/goals/history`)
**Purpose**: View past goals and achievements

**Components**:
- Date Range Filter
- Goal Type Filter (Daily/Weekly)
- Status Filter Chips
- Achievement Cards Grid
- Statistics Summary
- Export Button

---

### Goal Setup Page (`/goals/setup`)
**Purpose**: Create and configure new goals

**Components**:
- Goal Type Selector
- Target Session Input
- Target Minutes Input
- Date/Week Picker
- Goal Templates
- Preview Card
- Save Button

---

## Components

### CurrentGoalCard
```typescript
interface CurrentGoalCardProps {
  goal: Goal;
  onUpdate: (goalId: string, updates: Partial<Goal>) => void;
  onDelete: (goalId: string) => void;
}
```

**Visual Requirements**:
- Large progress ring showing completion %
- Target vs. Current display
- Sessions and Minutes breakdown
- Status badge
- Edit/Delete actions
- Achievement celebration animation when completed

---

### GoalSetupModal
```typescript
interface GoalSetupModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (config: GoalConfig) => void;
  goalType: 'daily' | 'weekly';
  existingGoal?: Goal;
}
```

**Fields**:
- Goal Type Toggle (Daily/Weekly)
- Target Sessions Input (number)
- Target Minutes Input (number)
- Date Picker (for daily) or Week Picker (for weekly)
- Template Selector (Beginner/Intermediate/Advanced)
- Save Button

**Templates**:
- Beginner Daily: 2 sessions, 50 minutes
- Intermediate Daily: 4 sessions, 100 minutes
- Advanced Daily: 6 sessions, 150 minutes
- Beginner Weekly: 10 sessions, 250 minutes
- Intermediate Weekly: 20 sessions, 500 minutes
- Advanced Weekly: 30 sessions, 750 minutes

---

### ProgressRing
```typescript
interface ProgressRingProps {
  current: number;
  target: number;
  size: 'small' | 'medium' | 'large';
  color: string;
  label: string;
}
```

**Visual Requirements**:
- SVG circle with animated stroke
- Percentage text in center
- Current/Target subtext
- Color changes: red (<50%), yellow (50-80%), green (>80%)
- Sparkle effect when 100% reached

---

### StreakDisplay
```typescript
interface StreakDisplayProps {
  currentStreak: number;
  longestStreak: number;
  goalType: 'daily' | 'weekly';
}
```

**Display**:
- Flame icon with streak count
- Current streak (large, prominent)
- Longest streak (smaller, reference)
- Streak type indicator
- Milestone badges (7, 14, 30, 60, 90 days)

---

### AchievementCard
```typescript
interface AchievementCardProps {
  achievement: GoalAchievement;
  onClick: () => void;
}
```

**Display**:
- Achievement date
- Goal type badge
- Target vs. Actual comparison
- Completion rate percentage
- Bonus points earned
- Trophy/Star icon
- Celebration confetti on hover

---

### GoalStatsPanel
```typescript
interface GoalStatsPanelProps {
  stats: GoalStatistics;
  timeRange: 'week' | 'month' | 'year' | 'all';
  onTimeRangeChange: (range: string) => void;
}
```

**Metrics**:
- Total goals set
- Total achieved
- Achievement rate %
- Current streak
- Longest streak
- Average progress
- Daily vs. Weekly breakdown
- Trend chart

---

### GoalTemplateSelector
```typescript
interface GoalTemplateSelectorProps {
  onSelect: (template: GoalTemplate) => void;
  goalType: 'daily' | 'weekly';
}
```

**Templates Display**:
- Template cards with difficulty indicator
- Sessions and minutes preview
- Recommended for badge
- Custom option

---

## User Flows

### Set Daily Goal Flow
1. User clicks "Set Daily Goal" button
2. Goal Setup Modal opens with 'daily' selected
3. User selects template or custom values
4. User sets target sessions and minutes
5. System validates inputs
6. User clicks "Create Goal"
7. Goal created and displayed on dashboard
8. Success message shown

### Set Weekly Goal Flow
1. User clicks "Set Weekly Goal" button
2. Goal Setup Modal opens with 'weekly' selected
3. System shows current week date range
4. User selects template or custom values
5. User confirms week and targets
6. User clicks "Create Goal"
7. Goal created and displayed on dashboard
8. Success message shown

### Track Goal Progress Flow
1. User completes a focus session
2. System automatically updates goal progress
3. Progress ring animates to new percentage
4. If sessions target met: Sessions indicator turns green
5. If minutes target met: Minutes indicator turns green
6. If both met: Achievement celebration triggered
7. Notification sent to user

### Goal Achievement Flow
1. Goal targets reached
2. Confetti animation plays
3. Achievement modal appears
4. Display achievement details
5. Show bonus points earned
6. Update streak count
7. Offer to share achievement
8. Return to dashboard with updated stats

### View Goal History Flow
1. User navigates to Goal History page
2. System loads achievements
3. User filters by date/type/status
4. User clicks on achievement card
5. Detail modal shows full stats
6. User can export history as CSV

---

## State Management

### Goal Store
```typescript
interface GoalState {
  currentDailyGoal: Goal | null;
  currentWeeklyGoal: Goal | null;
  goals: Goal[];
  achievements: GoalAchievement[];
  stats: GoalStatistics;
  streaks: {
    daily: StreakInfo;
    weekly: StreakInfo;
  };
  loading: boolean;
  error: string | null;
}
```

### Actions
- `setDailyGoal(config: DailyGoalConfig)`
- `setWeeklyGoal(config: WeeklyGoalConfig)`
- `updateGoal(goalId: string, updates: Partial<Goal>)`
- `deleteGoal(goalId: string)`
- `fetchGoals(filters: GoalFilters)`
- `fetchAchievements(filters: AchievementFilters)`
- `fetchStats(timeRange: string)`
- `checkGoalProgress(goalId: string)`

---

## Validation Rules

| Field | Rule |
|-------|------|
| Target Sessions (Daily) | 1-20 |
| Target Sessions (Weekly) | 1-100 |
| Target Minutes (Daily) | 15-480 |
| Target Minutes (Weekly) | 100-2400 |
| Goal Date | Cannot be in the past |
| Week Range | Must be exactly 7 days |

---

## Accessibility Requirements

- All progress rings have ARIA labels with percentage
- Keyboard navigation for all interactive elements
- Screen reader announcements for goal achievements
- High contrast mode for progress indicators
- Focus indicators on all buttons and inputs
- Alternative text for all icons and graphics
- Keyboard shortcut: 'G' to open goal setup

---

## Responsive Design

| Breakpoint | Layout | Progress Ring |
|------------|--------|---------------|
| Mobile (<768px) | Single column, stacked cards | 150px |
| Tablet (768-1024px) | Two column grid | 200px |
| Desktop (>1024px) | Three column grid | 250px |

---

## Animations

### Progress Update
- Smooth counter animation (500ms)
- Progress ring stroke animation (800ms)
- Color transition on threshold change (300ms)

### Goal Achievement
- Confetti burst from progress ring
- Scale pulse animation (3 cycles)
- Trophy icon bounce
- Success sound effect (optional)

### Streak Milestone
- Flame icon flicker animation
- Badge appearance with pop effect
- Particle effects around streak count

---

## Notifications

### Goal Achievement
```
"Congratulations! You've achieved your [daily/weekly] goal!"
Type: Success
Action: View Details
```

### Goal Progress Milestone
```
"Great progress! You're [percentage]% toward your goal."
Type: Info
Trigger: 25%, 50%, 75%
```

### Goal Reminder
```
"You have [X] sessions left to reach your daily goal!"
Type: Info
Trigger: 6 PM if goal not met
```

### Streak Milestone
```
"Amazing! You're on a [X]-day streak!"
Type: Celebration
Trigger: 7, 14, 30, 60, 90 days
```

### Goal Failure Warning
```
"Your [daily/weekly] goal is ending soon. Current progress: [X]%"
Type: Warning
Trigger: 2 hours before period end
```

---

## Color Scheme

### Status Colors
- Active: Blue (#3b82f6)
- Achieved: Green (#22c55e)
- Failed: Red (#ef4444)
- Abandoned: Gray (#6b7280)

### Progress Colors
- Low (<50%): Red (#ef4444)
- Medium (50-80%): Yellow (#eab308)
- High (80-99%): Orange (#f59e0b)
- Complete (100%+): Green (#22c55e)

### Streak Colors
- 1-6 days: Orange (#f59e0b)
- 7-13 days: Blue (#3b82f6)
- 14-29 days: Purple (#8b5cf6)
- 30+ days: Gold (#fbbf24)

---

## Loading States

- Skeleton loaders for goal cards
- Progress ring with indeterminate animation
- Shimmer effect on stats
- Inline spinners for actions

---

## Error Handling

### Validation Errors
- Inline error messages below inputs
- Red border on invalid fields
- Clear error descriptions

### API Errors
- Toast notification for failed operations
- Retry button for network errors
- Fallback UI when data unavailable

### Empty States
- No goals set: Motivational message + CTA
- No achievements: Encouragement to start
- No history: Empty state illustration
