# Analytics - Frontend Requirements

## UI Components

### 1. Analytics Dashboard
- Overview cards (completion rate, active streaks, habits)
- Trends chart (line/bar charts)
- Quick insights carousel
- Time period selector
- Category breakdown pie chart

### 2. Weekly Report View
- Report card with key metrics
- Most productive day highlight
- Best performing category
- Recommended actions list
- Share/export buttons
- Previous reports navigation

### 3. Trends Visualization
- Interactive charts:
  - Completion rate over time (line chart)
  - Completions by day of week (bar chart)
  - Completions by category (pie chart)
  - Streak timeline
- Date range selector
- Metric selector dropdown
- Zoom/pan capabilities

### 4. Patterns & Insights
- Pattern cards with confidence scores
- Visual indicators (icons, colors)
- "Why this matters" explanations
- Action buttons ("Apply suggestion")
- Dismiss/save options

### 5. Success Factors Display
- Factor cards with correlation scores
- Visual strength indicators
- Detailed explanations
- Recommendations
- Compare factors view

### 6. Comparison View
- Side-by-side period comparison
- This week vs last week
- This month vs last month
- Year-over-year
- Highlight improvements/declines

## State Management
```typescript
interface AnalyticsState {
  weeklyReport: WeeklyReport;
  trends: TrendsData;
  patterns: Pattern[];
  successFactors: SuccessFactor[];
  insights: Insight[];
  selectedPeriod: DateRange;
}
```

## Charts & Visualizations
- Line charts for trends
- Bar charts for comparisons
- Pie charts for distributions
- Heatmaps for calendar views
- Gauge charts for scores
- Progress rings

## Interactivity
- Hover tooltips with details
- Click to drill down
- Swipe between periods (mobile)
- Filter by habit/category
- Export chart as image

## Mobile Optimizations
- Swipeable report cards
- Simplified chart views
- Vertical scroll for insights
- Touch-friendly interactions
- Responsive chart sizes

## Export Options
- PDF report
- CSV data export
- Chart images (PNG)
- Share to social media
- Email weekly summary
