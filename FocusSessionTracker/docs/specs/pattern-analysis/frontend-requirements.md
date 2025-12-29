# Pattern Analysis - Frontend Requirements

## Overview
User interface components for viewing productivity patterns, insights, distraction analysis, and personalized recommendations based on focus session history.

---

## Pages

### Pattern Insights Dashboard (`/insights`)
**Purpose**: Main analytics dashboard showing all detected patterns

**Components**:
- Overview Stats Card
- Optimal Session Length Card
- Peak Focus Times Card
- Distraction Patterns Card
- Productivity Trends Graph
- Actionable Insights List
- Pattern History Timeline

**States**:
- Loading (analyzing data)
- Insufficient Data (needs more sessions)
- Data Available (showing patterns)
- Error (analysis failed)

---

### Optimal Session Length Analysis (`/insights/session-length`)
**Purpose**: Detailed view of optimal session duration analysis

**Components**:
- Duration Recommendation Card
- Success Rate by Duration Chart
- Sample Size Indicator
- Confidence Score Meter
- Session Type Breakdown
- Historical Duration Performance

---

### Peak Focus Times (`/insights/peak-times`)
**Purpose**: Visualization of user's most productive time windows

**Components**:
- Daily Heatmap (hour-by-hour focus scores)
- Weekly Pattern Grid
- Time Window Cards (peak periods)
- Focus Score by Time Chart
- Session Timing Recommendations
- Calendar Integration Button

---

### Distraction Analysis (`/insights/distractions`)
**Purpose**: Analysis of distraction patterns and mitigation strategies

**Components**:
- Distraction Frequency Chart
- Top Distractors List
- Distraction Timeline
- Impact Level Indicators
- Mitigation Suggestions Cards
- Pattern Resolution Tracker

---

### Productivity Trends (`/insights/trends`)
**Purpose**: Long-term productivity trend visualization

**Components**:
- Trend Direction Indicator
- Focus Score Timeline Graph
- Completion Rate Chart
- Contributing Factors List
- Milestone Markers
- Trend Comparison (week/month/quarter)

---

## Components

### PatternCard
```typescript
interface PatternCardProps {
  pattern: Pattern;
  showDetails?: boolean;
  onViewDetails: () => void;
}

interface Pattern {
  id: string;
  type: 'optimal-length' | 'peak-time' | 'distraction' | 'trend';
  title: string;
  confidence: number;
  detectedAt: Date;
  data: any;
  recommendation?: string;
}
```

**Visual Requirements**:
- Card with colored accent based on pattern type
- Confidence score badge (0-100%)
- Icon representing pattern type
- Expandable details section
- Call-to-action button for applicable patterns

---

### OptimalLengthWidget
```typescript
interface OptimalLengthWidgetProps {
  optimalDuration: number;
  confidence: number;
  successRates: DurationSuccessRate[];
  onApplyRecommendation: () => void;
}

interface DurationSuccessRate {
  duration: number;
  successRate: number;
  sampleSize: number;
}
```

**Visual Requirements**:
- Large display of optimal duration
- Bar chart showing success rates
- Apply button to set as default
- Confidence indicator (ring/progress bar)
- Sample size transparency indicator

---

### PeakTimesHeatmap
```typescript
interface PeakTimesHeatmapProps {
  timeWindows: TimeWindow[];
  selectedDay?: string;
  onTimeSlotClick: (hour: number) => void;
}

interface TimeWindow {
  startHour: number;
  endHour: number;
  focusScore: number;
  sessionCount: number;
}
```

**Visual Requirements**:
- 24-hour grid with color intensity by focus score
- Day-of-week selector
- Hover tooltip with details
- Peak windows highlighted
- Color scale legend (low to high focus)

---

### DistractionPatternList
```typescript
interface DistractionPatternListProps {
  patterns: DistractionPattern[];
  onDismissPattern: (id: string) => void;
  onApplyMitigation: (id: string) => void;
}

interface DistractionPattern {
  id: string;
  type: string;
  source: string;
  frequency: number;
  impact: 'low' | 'medium' | 'high';
  timing: string[];
  mitigation: string;
}
```

**Visual Requirements**:
- List items with impact level color coding
- Frequency badge
- Timing chips (when it typically occurs)
- Expandable mitigation suggestion
- Mark as resolved button

---

### TrendGraph
```typescript
interface TrendGraphProps {
  trend: ProductivityTrend;
  metric: 'focus-score' | 'completion-rate' | 'session-count';
  period: 'week' | 'month' | 'quarter';
  onPeriodChange: (period: string) => void;
}

interface ProductivityTrend {
  direction: 'upward' | 'downward' | 'stable';
  magnitude: 'slight' | 'moderate' | 'significant';
  dataPoints: TrendDataPoint[];
  factors: string[];
}
```

**Visual Requirements**:
- Line chart with trend line
- Direction indicator (arrow up/down/neutral)
- Magnitude badge
- Period selector tabs
- Contributing factors list below
- Milestone annotations on graph

---

### InsightCard
```typescript
interface InsightCardProps {
  insight: Insight;
  onDismiss: () => void;
  onTakeAction: () => void;
}

interface Insight {
  id: string;
  type: string;
  priority: 'low' | 'medium' | 'high';
  title: string;
  description: string;
  actionable: boolean;
  actionText?: string;
  createdAt: Date;
}
```

**Visual Requirements**:
- Priority color coding (red/yellow/blue)
- Icon based on insight type
- Clear title and description
- Action button (if actionable)
- Dismiss option
- Timestamp

---

### ConfidenceIndicator
```typescript
interface ConfidenceIndicatorProps {
  score: number; // 0-100
  size?: 'small' | 'medium' | 'large';
  showLabel?: boolean;
}
```

**Visual Requirements**:
- Circular progress indicator
- Color gradation (red<60, yellow 60-80, green>80)
- Percentage display in center
- Optional label below
- Tooltip explaining confidence

---

### PatternTimeline
```typescript
interface PatternTimelineProps {
  patterns: Pattern[];
  dateRange: DateRange;
  onPatternClick: (id: string) => void;
}
```

**Visual Requirements**:
- Vertical timeline with date markers
- Pattern cards along timeline
- Type-based icons and colors
- Scroll to load more
- Date range selector

---

## User Flows

### View Insights Flow
1. User navigates to Insights Dashboard
2. System checks for sufficient data
3. If insufficient: Show "Need more data" message with progress
4. If sufficient: Display detected patterns
5. User can filter by pattern type
6. User clicks pattern card to view details
7. Detailed view opens with recommendations

### Apply Optimal Length Flow
1. User views optimal session length card
2. System displays recommended duration with confidence
3. User clicks "Apply Recommendation"
4. Confirmation modal shows impact on current settings
5. User confirms
6. Default session duration updated
7. Success message displayed
8. Future session suggestions use new default

### Schedule Peak Time Flow
1. User views peak focus times
2. System highlights optimal time windows
3. User clicks "Schedule Session"
4. Calendar/scheduler opens with recommended time
5. User confirms session booking
6. Notification set for session time
7. Pattern used to optimize future scheduling

### Resolve Distraction Pattern Flow
1. User views distraction patterns
2. System shows top distractors with mitigation
3. User clicks mitigation suggestion
4. Action modal explains steps to resolve
5. User enables recommended solution (e.g., focus mode)
6. Pattern marked as "mitigation applied"
7. Future tracking monitors effectiveness
8. If successful, pattern auto-resolves after 14 days

---

## State Management

### Pattern Analytics Store
```typescript
interface PatternAnalyticsState {
  patterns: Pattern[];
  optimalLength: OptimalLength | null;
  peakTimes: PeakTime[];
  distractions: DistractionPattern[];
  trends: ProductivityTrend[];
  insights: Insight[];
  isLoading: boolean;
  error: string | null;
  lastAnalyzed: Date | null;
  sufficientData: boolean;
}
```

### Actions
- `fetchPatterns(userId: string, filters?: PatternFilters)`
- `fetchOptimalLength(userId: string)`
- `fetchPeakTimes(userId: string)`
- `fetchDistractionPatterns(userId: string)`
- `fetchProductivityTrends(userId: string, period: string)`
- `fetchInsights(userId: string)`
- `triggerAnalysis(userId: string, type: string)`
- `dismissInsight(insightId: string)`
- `applyRecommendation(patternId: string, action: string)`
- `markPatternResolved(patternId: string)`

---

## Validation Rules

| Field | Rule |
|-------|------|
| Confidence Threshold | Display only patterns with >60% confidence |
| Date Range | Max 1 year historical analysis |
| Sample Size Display | Show warning if <20 sessions |
| Trend Period | week (7 days), month (30 days), quarter (90 days) |

---

## Accessibility Requirements

- All charts have accessible data tables
- Color-blind friendly palette for heatmaps
- Screen reader announcements for new insights
- Keyboard navigation for all interactive elements
- Focus indicators on all clickable items
- ARIA labels for chart elements
- Text alternatives for visual patterns

---

## Responsive Design

| Breakpoint | Layout | Adjustments |
|------------|--------|-------------|
| Mobile (<768px) | Single column | Stacked cards, simplified charts |
| Tablet (768-1024px) | Two column grid | Side-by-side pattern cards |
| Desktop (>1024px) | Three column grid | Full dashboard view, large charts |

---

## Data Refresh Strategy

- Patterns auto-refresh every 5 minutes while viewing
- Manual refresh button available
- Real-time updates via WebSocket for new patterns
- Background sync when app in foreground
- Cache patterns locally for offline viewing
- Show "last updated" timestamp

---

## Empty States

### Insufficient Data
```
+------------------------------------------+
|        Not Enough Data Yet               |
|                                          |
|     [Chart Icon with Question Mark]     |
|                                          |
|  We need at least 20 completed sessions  |
|  to identify meaningful patterns.        |
|                                          |
|  Progress: [####--------] 8/20           |
|                                          |
|     [Start a Session]                    |
+------------------------------------------+
```

### No Patterns Detected
```
+------------------------------------------+
|     No Patterns Detected Yet             |
|                                          |
|         [Magnifying Glass Icon]          |
|                                          |
|  Keep focusing! Patterns will emerge     |
|  as you complete more sessions.          |
|                                          |
|  Check back in a few days.               |
+------------------------------------------+
```

---

## Loading States

- Skeleton screens for pattern cards
- Shimmer effect on charts while loading
- Progress bar for analysis jobs
- Loading spinner for individual components
- Optimistic updates for user actions

---

## Error Handling

- Network errors: Show retry button
- Analysis errors: Suggest manual trigger
- Insufficient permissions: Show upgrade prompt
- Stale data: Show refresh prompt
- Invalid patterns: Filter out silently

---

## Animations

- Pattern cards fade in on load
- Trend graphs animate drawing
- Confidence rings animate fill
- Insight cards slide in from right
- Success states: Confetti or checkmark pulse
- Smooth transitions between time periods

---

## Export Options

- Export insights as PDF report
- Download pattern data as CSV
- Share specific patterns via link
- Email weekly insights digest
- Calendar export for peak times

---

## Personalization

- Remember preferred chart types
- Save custom date ranges
- Pin favorite insights
- Customize dashboard layout
- Set insight notification preferences
- Theme patterns by focus area
