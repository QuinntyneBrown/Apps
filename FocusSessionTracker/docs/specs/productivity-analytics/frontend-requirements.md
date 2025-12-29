# Productivity Analytics - Frontend Requirements

## Overview
User interface for viewing productivity analytics, achievements, streaks, deep work progress, and performance trends.

---

## Pages

### Productivity Dashboard Page (`/analytics/productivity`)
**Purpose**: Main analytics overview showing key metrics and trends

**Components**:
- Metrics Overview Cards (sessions, focus time, streak, avg score)
- Focus Score Trend Chart
- Session Distribution Chart
- Weekly Progress Calendar
- Achievement Highlights
- Deep Work Progress Bar
- Quick Stats Summary
- Export Data Button

**Data Loading**:
- Fetch on mount with default 30-day period
- Real-time updates via WebSocket
- Loading skeletons during fetch

---

### Achievements Page (`/analytics/achievements`)
**Purpose**: Display all achievements and progress

**Components**:
- Achievement Category Tabs
- Achievement Grid
- Progress Indicators
- Filter Controls
- Search Bar
- Achievement Detail Modal

**Categories**:
- High Focus Sessions
- Streaks
- Deep Work
- Consistency
- Special Achievements

---

### Streak History Page (`/analytics/streaks`)
**Purpose**: View focus streak history and current streak status

**Components**:
- Current Streak Card (prominent)
- Streak Calendar Heatmap
- Milestone Progress Timeline
- Historical Streaks List
- Streak Recovery Tips
- Streak Statistics

---

### Deep Work Analytics (`/analytics/deep-work`)
**Purpose**: Track deep work accumulation and patterns

**Components**:
- Deep Work Time Counter
- Threshold Progress Bars
- Weekly Deep Work Chart
- Best Performing Days/Times
- Deep Work Session List
- Recommendations Panel

---

## Components

### MetricsOverview
```typescript
interface MetricsOverviewProps {
  totalSessions: number;
  totalFocusTime: number;
  currentStreak: number;
  averageFocusScore: number;
  dateRange: { start: Date; end: Date };
}
```

**Visual Requirements**:
- 4 cards in responsive grid
- Large number display with icon
- Trend indicator (up/down arrow with %)
- Color coding: green (positive), red (negative), gray (neutral)

---

### FocusScoreChart
```typescript
interface FocusScoreChartProps {
  dataPoints: Array<{ date: string; score: number }>;
  period: 'week' | 'month' | 'quarter';
  onPeriodChange: (period: string) => void;
}
```

**Chart Type**: Line chart with area fill
**Features**:
- Smoothed curve
- Target line at 80 (good focus threshold)
- Hover tooltips with date and score
- Zoom controls
- Period selector buttons

---

### AchievementCard
```typescript
interface AchievementCardProps {
  achievement: {
    id: string;
    name: string;
    description: string;
    category: string;
    achieved: boolean;
    achievedAt?: Date;
    progress?: number;
    icon: string;
  };
  onClick: () => void;
}
```

**Visual States**:
- Achieved: Full color with gold border, checkmark badge
- In Progress: Partial color with progress ring
- Locked: Grayscale with lock icon
- Glow animation for recently achieved

---

### StreakCalendar
```typescript
interface StreakCalendarProps {
  dates: Array<{ date: string; completed: boolean; sessionCount: number }>;
  currentStreak: number;
  startDate: Date;
}
```

**Display**:
- GitHub-style contribution heatmap
- Color intensity based on session count
- Hover shows date and session details
- Current streak highlighted with border
- Missing days marked in red

---

### DeepWorkProgress
```typescript
interface DeepWorkProgressProps {
  currentMinutes: number;
  nextThreshold: number;
  thresholdsReached: number[];
  weeklyGoal: number;
}
```

**Components**:
- Circular progress indicator
- Time remaining to next threshold
- List of completed thresholds with dates
- Weekly goal tracker
- Motivational messages

---

### TrendChart
```typescript
interface TrendChartProps {
  metric: 'focus-score' | 'session-count' | 'focus-time';
  data: ChartDataPoint[];
  groupBy: 'day' | 'week' | 'month';
  onMetricChange: (metric: string) => void;
}
```

**Features**:
- Multi-metric selector
- Comparison mode (current vs previous period)
- Statistical summary (mean, median, range)
- Export chart as image
- Responsive scaling

---

### AchievementDetailModal
```typescript
interface AchievementDetailModalProps {
  achievement: Achievement;
  isOpen: boolean;
  onClose: () => void;
}
```

**Content**:
- Achievement icon (large)
- Achievement name and description
- Date achieved (if completed)
- Requirements checklist
- Related stats
- Share button

---

### DateRangePicker
```typescript
interface DateRangePickerProps {
  startDate: Date;
  endDate: Date;
  presets: Array<{ label: string; days: number }>;
  onChange: (start: Date, end: Date) => void;
}
```

**Presets**:
- Last 7 Days
- Last 30 Days
- Last Quarter
- Last Year
- Custom Range

---

## User Flows

### View Dashboard Flow
1. User navigates to Analytics Dashboard
2. System loads default 30-day metrics
3. Cards display with animation
4. Charts render with smooth transitions
5. User can change date range
6. Metrics update dynamically

### Achievement Unlock Flow
1. User completes qualifying session
2. Achievement notification appears
3. Confetti animation plays
4. Achievement card unlocks with glow effect
5. User can click to view details
6. Option to share achievement

### Streak Milestone Flow
1. User completes session maintaining streak
2. Milestone reached notification
3. Streak card updates with celebration animation
4. Milestone badge appears
5. Encouragement message displays
6. Next milestone shown

### Deep Work Threshold Flow
1. Deep work time accumulates
2. Progress bar updates in real-time
3. Threshold reached triggers celebration
4. Achievement unlocked
5. Statistics update
6. Next threshold displayed

---

## State Management

### Analytics Store
```typescript
interface AnalyticsState {
  dashboard: {
    metrics: ProductivityMetrics | null;
    trends: TrendData[];
    loading: boolean;
    error: string | null;
  };
  achievements: {
    items: Achievement[];
    filters: AchievementFilters;
    loading: boolean;
  };
  streaks: {
    current: Streak | null;
    history: Streak[];
    calendar: CalendarData[];
    loading: boolean;
  };
  deepWork: {
    currentMinutes: number;
    thresholds: Threshold[];
    weeklyData: DeepWorkData[];
    loading: boolean;
  };
  dateRange: {
    start: Date;
    end: Date;
  };
}
```

### Actions
- `fetchDashboardMetrics(startDate: Date, endDate: Date)`
- `fetchAchievements(filters: AchievementFilters)`
- `fetchStreakData()`
- `fetchDeepWorkStats(period: string)`
- `setDateRange(start: Date, end: Date)`
- `exportAnalytics(format: string)`
- `handleAchievementUnlocked(achievement: Achievement)`

---

## Validation Rules

| Field | Rule |
|-------|------|
| Date Range | Max 365 days span |
| Custom Date | Cannot be future date |
| Export Format | csv, json, or pdf only |
| Chart Data Points | Max 365 points per chart |

---

## Accessibility Requirements

- All charts have accessible data tables
- Achievement status announced to screen readers
- Keyboard navigation for all interactive elements
- Color blindness-friendly palette
- High contrast mode support
- Focus indicators on all controls
- ARIA labels on all metrics

---

## Responsive Design

| Breakpoint | Layout | Chart Size |
|------------|--------|------------|
| Mobile (<768px) | Single column, stacked cards | Full width, reduced height |
| Tablet (768-1024px) | 2 column grid | 50% width |
| Desktop (>1024px) | 3-4 column grid, side panels | Optimized for large screens |

---

## Performance Optimization

1. **Lazy Loading**: Charts load on viewport intersection
2. **Debouncing**: Date range changes debounced 500ms
3. **Memoization**: Chart data memoized to prevent re-renders
4. **Virtual Scrolling**: Achievement list uses virtual scrolling
5. **Code Splitting**: Analytics routes split into separate bundles

---

## Animations

### Achievement Unlock
- Fade in from center
- Scale up animation (0.8 to 1)
- Glow pulse effect
- Confetti particles
- Duration: 1.5 seconds

### Milestone Reached
- Bounce animation
- Color flash (gold)
- Star burst effect
- Sound effect (optional)
- Duration: 2 seconds

### Chart Transitions
- Smooth line drawing (stroke-dasharray animation)
- Fade in area fills
- Stagger bar animations
- Duration: 800ms

---

## Real-time Updates

### WebSocket Events
- `achievement-unlocked`: New achievement notification
- `streak-milestone`: Streak milestone reached
- `threshold-reached`: Deep work threshold met
- `metrics-updated`: Daily metrics recalculated

### Update Strategy
- Optimistic updates for immediate feedback
- Background sync every 5 minutes
- Force refresh on visibility change
- Conflict resolution: server wins

---

## Error Handling

1. **Network Errors**: Show retry button with offline message
2. **Data Load Failures**: Display error state with refresh option
3. **Export Failures**: Toast notification with error details
4. **Invalid Date Range**: Inline validation message
5. **Missing Data**: Show empty state with helpful message

---

## Empty States

### No Data Available
- Illustration of analytics chart
- "No data yet" message
- "Complete your first session to see analytics"
- Call-to-action button

### No Achievements Unlocked
- Locked achievement illustration
- "Start your journey" message
- Show closest unlockable achievements
- Motivational quote

### Broken Streak
- Broken chain illustration
- "Streak ended" message with empathy
- "Start a new streak today" encouragement
- Display previous best streak
