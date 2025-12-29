# Break Management - Frontend Requirements

## Overview
User interface components for managing breaks between focus sessions including break timers, activity logging, and break history.

---

## Pages

### Break Timer Page (`/break`)
**Purpose**: Active break timer interface

**Components**:
- Break Timer Display (circular countdown)
- Break Type Indicator
- Activity Suggestions
- Quick Activity Logger
- Extend Break Button
- End Break Button

**States**:
- Active (break in progress)
- Extended (break duration exceeded)
- Suggested Activities Visible
- Activity Logged Confirmation

---

### Break History Page (`/breaks/history`)
**Purpose**: View past breaks and effectiveness

**Components**:
- Date Range Filter
- Break Type Filter
- Break Cards List
- Effectiveness Rating Display
- Activity Summary
- Pagination Controls
- Export Button

---

### Break Settings Page (`/breaks/settings`)
**Purpose**: Configure break preferences and patterns

**Components**:
- Break Pattern Selector (Pomodoro, Custom)
- Duration Preferences
- Reminder Settings
- Activity Preferences
- Notification Toggles

---

## Components

### BreakTimerDisplay
```typescript
interface BreakTimerDisplayProps {
  remainingSeconds: number;
  totalSeconds: number;
  breakType: 'short' | 'long' | 'custom';
  wasExtended: boolean;
}
```

**Visual Requirements**:
- Circular timer (200px diameter)
- Progress ring showing elapsed time
- MM:SS format display
- Color scheme: blue (short), green (long), purple (custom)
- Pulse animation when time nearly up

---

### BreakActivityLogger
```typescript
interface BreakActivityLoggerProps {
  breakId: string;
  onActivityLogged: (activity: Activity) => void;
  suggestedActivities: string[];
}
```

**Fields**:
- Activity Type Selector (Quick buttons)
  - Walk
  - Coffee/Tea
  - Meditation
  - Snack
  - Stretch
  - Other
- Optional Notes Textarea
- Effectiveness Rating (1-5 stars)
- Log Activity Button

---

### BreakRecommendationBanner
```typescript
interface BreakRecommendationBannerProps {
  recommendedBreakType: 'short' | 'long';
  recommendedDuration: number;
  reason: string;
  onStartBreak: () => void;
  onSkipBreak: (reason?: string) => void;
}
```

**Display**:
- Recommendation message
- Duration badge
- Reason explanation
- Start Break button (prominent)
- Skip Break link (subtle)

---

### BreakCard
```typescript
interface BreakCardProps {
  break: Break;
  onClick: () => void;
}
```

**Display**:
- Date/Time
- Duration (planned vs actual)
- Break Type Badge
- Activities List (icons)
- Effectiveness Rating
- Extension Indicator (if extended)

---

### BreakExtensionModal
```typescript
interface BreakExtensionModalProps {
  currentBreak: Break;
  onExtend: (minutes: number, reason?: string) => void;
  onCancel: () => void;
}
```

**Fields**:
- Extension Duration Slider (1-15 minutes)
- Reason Input (optional)
- Extend Button
- Cancel Button

---

### BreakSkipDialog
```typescript
interface BreakSkipDialogProps {
  onSkip: (reason: string) => void;
  onCancel: () => void;
  consecutiveSkips: number;
}
```

**Fields**:
- Warning message (if consecutive skips high)
- Skip reason dropdown
  - In flow state
  - Not tired
  - Deadline pressure
  - Other
- Optional notes
- Confirm Skip Button
- Take Break Anyway Button

---

## User Flows

### Start Break Flow
1. Session completes
2. Break recommendation banner appears
3. User clicks "Start Break"
4. Break type selection (or use recommended)
5. Break timer begins
6. Activity suggestions displayed
7. Timer countdown starts

### Log Break Activity Flow
1. During active break, user clicks activity button
2. Activity logger expands
3. User selects activity type (quick buttons)
4. User optionally adds notes
5. User rates effectiveness (1-5 stars)
6. User clicks "Log Activity"
7. Confirmation message displayed
8. Activity added to break summary

### Extend Break Flow
1. Break timer approaching zero
2. User clicks "Extend Break"
3. Extension modal opens
4. User selects additional minutes (slider)
5. User optionally adds reason
6. User clicks "Extend"
7. Timer updated with new duration
8. Extension tracked

### Skip Break Flow
1. Break recommendation appears
2. User clicks "Skip Break"
3. Skip confirmation dialog appears
4. If 3+ consecutive skips, warning shown
5. User selects skip reason from dropdown
6. User confirms skip
7. Skip recorded
8. Return to session view

### Complete Break Flow
1. Timer reaches zero (or user clicks End)
2. Optional: prompt for final activity log
3. Break marked complete
4. Summary displayed (duration, activities)
5. "Ready to Focus" button appears
6. On click, return to session timer

---

## State Management

### Break Store
```typescript
interface BreakState {
  currentBreak: Break | null;
  remainingSeconds: number;
  status: 'idle' | 'active' | 'extended';
  activities: BreakActivity[];
  recommendation: BreakRecommendation | null;
  consecutiveSkips: number;
  breakStreak: number;
}
```

### Actions
- `startBreak(config: BreakConfig)`
- `extendBreak(minutes: number, reason?: string)`
- `logActivity(activity: BreakActivity)`
- `completeBreak()`
- `skipBreak(reason: string)`
- `loadRecommendation()`
- `tickTimer()`

---

## Validation Rules

| Field | Rule |
|-------|------|
| Break Duration | 1-60 minutes |
| Extension Duration | 1-15 minutes per extension |
| Effectiveness Rating | 1-5 (optional) |
| Activity Notes | Max 1000 characters |
| Skip Reason | Required if skipping |

---

## Accessibility Requirements

- Timer updates announced to screen readers every minute
- All buttons have clear ARIA labels
- Keyboard shortcuts:
  - Space (pause/resume break timer)
  - E (extend break)
  - A (log activity)
  - Escape (cancel modals)
- High contrast mode support
- Focus indicators on all interactive elements
- Screen reader announces break recommendations

---

## Responsive Design

| Breakpoint | Timer Size | Layout |
|------------|------------|--------|
| Mobile (<768px) | 180px | Single column, stacked controls |
| Tablet (768-1024px) | 220px | Two column |
| Desktop (>1024px) | 260px | Centered with side activity panel |

---

## Notification Requirements

1. **Break Reminder**: Show after session completion
2. **Break Almost Over**: Alert at 1 minute remaining
3. **Break Completed**: Notification when timer ends
4. **Burnout Warning**: Alert after 5 consecutive skips
5. **Break Streak**: Celebrate daily break streak milestones

---

## Visual Design

### Break Type Colors
- **Short Break**: Light Blue (`#60a5fa`)
- **Long Break**: Green (`#4ade80`)
- **Custom Break**: Purple (`#a78bfa`)
- **Extended Break**: Orange (`#fb923c`)

### Activity Icons
- Walk: 🚶
- Coffee: ☕
- Meditation: 🧘
- Snack: 🍎
- Stretch: 🤸
- Water: 💧

### States
- **Active Break**: Pulsing blue ring
- **Extended**: Orange border on timer
- **Near Completion**: Faster pulse animation
- **Skipped**: Gray with strikethrough

---

## Gamification Elements

1. **Break Streak Badge**: Show consecutive days with healthy breaks
2. **Activity Diversity**: Reward trying different break activities
3. **Effectiveness Insights**: Show most effective break types
4. **Hydration Tracker**: Track water intake during breaks
5. **Movement Goals**: Track physical activity during breaks

---

## Integration with Session Timer

- Automatic break recommendation after session completion
- Session context shown during break (what you were working on)
- Return to previous task after break
- Break history linked to session history
- Combined focus time + break time statistics
