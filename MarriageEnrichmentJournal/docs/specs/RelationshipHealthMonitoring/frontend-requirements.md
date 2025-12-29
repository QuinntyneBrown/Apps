# Frontend Requirements - Relationship Health Monitoring

## Pages/Views

### Relationship Health Dashboard
**Route**: `/health/dashboard`

**Components**:
- HealthScoreGauge
- TrendChart
- QuickMoodLog
- MetricsSummary
- PatternCards
- ActionableSuggestions

**Features**:
- Visual health score (0-100)
- Interactive trend chart (selectable time periods)
- Quick mood logging widget
- Key metrics summary (gratitude, wins, streaks)
- Identified positive patterns
- Suggested actions/interventions

### Mood Logging Page
**Route**: `/health/mood-log`

**Components**:
- MoodRatingSlider (1-10)
- FactorsSelector
- NotesTextarea
- MoodHistoryList

**Features**:
- Easy mood rating (1-10 scale with emoji)
- Select contributing factors (predefined + custom)
- Optional notes
- View mood history
- Quick log from anywhere (modal)

### Trends Analysis Page
**Route**: `/health/trends`

**Components**:
- TrendLineChart
- PeriodSelector (7d, 30d, 90d, all)
- FactorBreakdown
- BothPartnersComparison

**Features**:
- Visual trend line over time
- Compare both partners' moods
- See correlation with events
- Identify positive and negative factors
- Export trend data

### Patterns Library
**Route**: `/health/patterns`

**Components**:
- PatternCard
- StrengthIndicator
- ExamplesTimeline

**Features**:
- Display identified positive patterns
- Show pattern frequency and strength
- View specific examples of pattern
- Celebrate pattern milestones

## Components

### HealthScoreGauge
**Display**:
- Circular gauge (0-100)
- Color-coded (red <50, yellow 50-75, green >75)
- Trend arrow (up/down/stable)
- Score change from last week

### MoodRatingSlider
**Features**:
- Visual slider 1-10
- Emoji faces for each level
- Current selection highlighted
- Haptic feedback on mobile

### TrendChart
**Type**: Line chart
**Data**: Mood ratings over time
**Features**:
- Zoom and pan
- Both partners on same chart (different colors)
- Hover to see details
- Mark significant events

### PatternCard
**Display**:
- Pattern name/type
- Frequency badge
- Examples count
- "View Details" button
- Celebration indicator

### QuickMoodLog
**Component**: Compact mood logger
**Features**:
- Mini rating selector
- Quick submit
- Expand for full form

## State Management

### healthSlice
```typescript
{
  dashboard: {
    healthScore: number,
    metrics: {},
    loading: boolean
  },
  moodLogs: {
    items: [],
    recent: [],
    loading: boolean
  },
  trends: {
    direction: string,
    dataPoints: [],
    period: string,
    loading: boolean
  },
  patterns: {
    items: [],
    loading: boolean
  },
  suggestions: {
    interventions: [],
    priority: string
  }
}
```

## Real-time Features
- Live dashboard updates when spouse logs mood
- Notification if concerning trend detected
- Celebrate when positive pattern identified

## Data Visualization

### Charts Required
- Health score gauge (D3.js or Chart.js)
- Mood trend line chart
- Contributing factors breakdown (bar chart)
- Pattern frequency heatmap

## Responsive Design
- Mobile: Stacked widgets, swipeable charts
- Desktop: Grid layout, side-by-side comparisons
- Touch-optimized sliders and selectors

## Accessibility
- Screen reader descriptions for charts
- Keyboard navigation for all controls
- Color-blind friendly chart colors
- Alternative text data tables for charts
