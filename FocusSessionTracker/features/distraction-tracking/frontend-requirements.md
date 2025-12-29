# Distraction Tracking - Frontend Requirements

## Overview
User interface components for logging, viewing, and analyzing distractions during focus sessions. Includes quick-log functionality, detailed tracking, pattern visualization, and resistance recording.

---

## Pages

### Distraction Logger Modal (`/session` - overlay)
**Purpose**: Quick distraction logging during active session

**Components**:
- Distraction Type Selector (Internal/External)
- Quick Source Buttons
- Impact Level Slider
- Optional Description Field
- Duration Timer (auto-start on open)
- Log Button

**Trigger**: Click "Log Distraction" button during session

---

### Distraction History Page (`/distractions`)
**Purpose**: View all logged distractions

**Components**:
- Date Range Filter
- Type Filter (Internal/External/All)
- Source Filter Dropdown
- Impact Level Filter
- Distraction Cards List
- Pagination Controls
- Export Button

---

### Distraction Analytics Page (`/distractions/analytics`)
**Purpose**: Visualize distraction patterns and trends

**Components**:
- Time Period Selector
- Distraction Frequency Chart
- Source Breakdown Pie Chart
- Peak Times Heatmap
- Internal vs External Comparison
- Resistance Success Rate Card
- Pattern Insights Panel

---

### Distraction Resistance Tracker (`/session` - sidebar)
**Purpose**: Quick-log successful distraction resistance

**Components**:
- Resistance Quick-Log Button
- Distraction Type Selector
- Strategy Dropdown
- Today's Resistance Count Badge

---

## Components

### QuickDistractionLogger
```typescript
interface QuickDistractionLoggerProps {
  sessionId: string;
  isOpen: boolean;
  onClose: () => void;
  onLog: (distraction: DistractionLog) => void;
}

interface DistractionLog {
  distractionType: 'internal' | 'external';
  source: string;
  impactLevel: number;
  duration: number;
  description?: string;
}
```

**Visual Requirements**:
- Modal overlay with semi-transparent backdrop
- Auto-focus on type selector
- Duration timer starts automatically
- Keyboard shortcuts: I (internal), E (external), 1-5 (impact)
- Quick-log mode: One-click source buttons

**Quick Sources**:
- Internal: Mind Wandering, Urge, Thought, Daydream
- External: Notification, Person, Noise, Email

---

### DistractionCard
```typescript
interface DistractionCardProps {
  distraction: Distraction;
  onClick: () => void;
  onEdit?: () => void;
  onDelete?: () => void;
}

interface Distraction {
  id: string;
  sessionId: string;
  distractionType: 'internal' | 'external';
  source: string;
  description?: string;
  duration: number;
  impactLevel: number;
  timestamp: Date;
}
```

**Display**:
- Type Badge (color-coded)
- Source Icon and Label
- Impact Level (1-5 dots)
- Duration
- Timestamp
- Description (expandable)
- Edit/Delete Actions

---

### ResistanceLogger
```typescript
interface ResistanceLoggerProps {
  sessionId: string;
  onLog: (resistance: ResistanceLog) => void;
}

interface ResistanceLog {
  distractionTypeResisted: string;
  resistanceStrategy: string;
  description?: string;
}
```

**Quick Strategies**:
- Deep Breath
- Noted for Later
- Blocked App
- 10-Second Rule
- Focus Reminder

---

### DistractionFrequencyChart
```typescript
interface DistractionFrequencyChartProps {
  data: DistractionDataPoint[];
  period: 'day' | 'week' | 'month';
}

interface DistractionDataPoint {
  date: Date;
  internalCount: number;
  externalCount: number;
}
```

**Chart Type**: Line chart with two series
**Colors**:
- Internal: Blue (#4A90E2)
- External: Orange (#F5A623)

---

### SourceBreakdownChart
```typescript
interface SourceBreakdownChartProps {
  sources: SourceCount[];
}

interface SourceCount {
  source: string;
  count: number;
  percentage: number;
}
```

**Chart Type**: Pie/Donut chart
**Legend**: Shows source name, count, and percentage

---

### PeakTimesHeatmap
```typescript
interface PeakTimesHeatmapProps {
  heatmapData: HeatmapCell[];
}

interface HeatmapCell {
  hour: number; // 0-23
  dayOfWeek: number; // 0-6
  count: number;
}
```

**Visual**: 7x24 grid showing distraction frequency by day/hour
**Color Scale**: White (0) to Dark Red (max)

---

### PatternInsightCard
```typescript
interface PatternInsightCardProps {
  pattern: DistractionPattern;
}

interface DistractionPattern {
  id: string;
  patternType: string;
  description: string;
  frequency: number;
  severity: number;
  mitigationSuggestions: string[];
}
```

**Display**:
- Pattern Icon and Title
- Description
- Frequency Badge
- Severity Indicator
- Suggested Actions List
- Dismiss Button

---

## User Flows

### Log Distraction During Session Flow
1. User is in active focus session
2. Distraction occurs
3. User clicks "Log Distraction" button
4. Quick Logger modal opens
5. Duration timer starts automatically
6. User selects type (Internal/External)
7. User clicks source quick button (or types custom)
8. User adjusts impact slider (1-5)
9. User optionally adds description
10. User clicks "Log" button
11. Modal closes
12. Distraction count updates on session page
13. Focus score recalculates

### Log Distraction Resistance Flow
1. User feels urge to check phone/notification
2. User resists the urge
3. User clicks "Resisted!" button
4. Quick resistance logger appears
5. User selects what they resisted
6. User selects strategy used
7. User clicks "Log Resistance"
8. Resistance count increments
9. Positive feedback animation plays

### View Distraction History Flow
1. User navigates to Distractions page
2. Default view shows last 7 days
3. User applies filters (type, source, date range)
4. List updates with filtered results
5. User clicks on distraction card
6. Detail modal opens showing full information
7. User can edit notes or delete

### Analyze Distraction Patterns Flow
1. User navigates to Analytics page
2. Charts load with default period (last week)
3. User changes time period to month
4. All charts update with new data
5. User hovers over chart elements to see details
6. User sees pattern insights in sidebar
7. User clicks on suggested action
8. Action guidance modal opens

---

## State Management

### Distraction Store
```typescript
interface DistractionState {
  currentSessionDistractions: Distraction[];
  distractionHistory: Distraction[];
  resistances: Resistance[];
  patterns: DistractionPattern[];
  analytics: DistractionAnalytics;
  isLogging: boolean;
  filters: DistractionFilters;
}

interface DistractionFilters {
  dateRange: { start: Date; end: Date };
  distractionType?: 'internal' | 'external';
  source?: string;
  minImpact?: number;
  maxImpact?: number;
}
```

### Actions
- `logDistraction(sessionId: string, distraction: DistractionLog)`
- `logResistance(sessionId: string, resistance: ResistanceLog)`
- `fetchDistractions(filters: DistractionFilters)`
- `fetchAnalytics(period: string)`
- `updateDistraction(id: string, updates: Partial<Distraction>)`
- `deleteDistraction(id: string)`
- `dismissPattern(patternId: string)`

---

## Validation Rules

| Field | Rule |
|-------|------|
| DistractionType | Required (internal or external) |
| Source | Required, max 50 characters |
| Description | Optional, max 1000 characters |
| Duration | Auto-calculated, max 600 seconds |
| ImpactLevel | Required, 1-5 scale |
| ResistanceStrategy | Required for resistance logs |

---

## Accessibility Requirements

- All modals closable with Escape key
- Keyboard navigation for source selection (Tab, Arrow keys)
- Screen reader announces distraction logged
- ARIA labels on all interactive elements
- Focus trap in modal dialogs
- Color-blind friendly chart colors
- High contrast mode for impact indicators

---

## Responsive Design

| Breakpoint | Logger Modal | Charts |
|------------|--------------|--------|
| Mobile (<768px) | Full screen overlay | Single column, scrollable |
| Tablet (768-1024px) | Centered modal (80% width) | Two column grid |
| Desktop (>1024px) | Centered modal (600px) | Three column grid |

---

## Real-time Features

- WebSocket connection for live distraction count updates
- Auto-save draft distraction logs (recover if modal closes)
- Optimistic UI updates (instant feedback)
- Background sync for offline logging

---

## Performance Optimizations

- Lazy load analytics charts (load on page view)
- Virtual scrolling for long distraction lists (>100 items)
- Debounce filter inputs (300ms delay)
- Cache analytics data for selected period (5 minutes)
- Compress chart data for network transfer

---

## UI/UX Guidelines

### Quick Logging Principles
- **Speed First**: Must log distraction in < 10 seconds
- **Minimal Friction**: One-click options for common cases
- **Auto-timing**: Duration tracked automatically
- **Smart Defaults**: Pre-select most common sources

### Feedback & Motivation
- **Positive Reinforcement**: Celebrate resistance logging
- **Non-judgmental**: Neutral tone for distraction logging
- **Progress Indicators**: Show improvement over time
- **Actionable Insights**: Patterns lead to suggestions

### Color Coding
- Internal Distractions: Blue tones
- External Distractions: Orange tones
- Resistance: Green tones
- High Impact (4-5): Red indicators
- Low Impact (1-2): Yellow indicators

---

## Notification & Alerts

- Pattern detected alert (dismissible)
- High distraction session warning (>5 distractions)
- Resistance milestone celebrations (10, 25, 50, 100)
- Weekly distraction report notification

---

## Integration Points

- Session page: Embedded quick-log button
- Analytics dashboard: Distraction summary widget
- Reports: Include distraction metrics
- Goals: Set distraction reduction targets
