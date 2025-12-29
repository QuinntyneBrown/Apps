# Reporting - Frontend Requirements

## Overview
User interface components for viewing weekly reports, monthly insights, productivity trends, and exporting data for personal analysis.

---

## Pages

### Reports Dashboard Page (`/reports`)
**Purpose**: Main reporting interface showing overview and recent reports

**Components**:
- Dashboard Summary Cards
- Report Period Selector
- Weekly Reports Grid
- Monthly Insights Grid
- Trends Chart
- Export Controls

**States**:
- Loading (fetching reports)
- Empty (no reports available)
- Populated (displaying reports)
- Generating (report being created)

---

### Weekly Report Detail Page (`/reports/weekly/{reportId}`)
**Purpose**: Detailed view of a specific weekly report

**Components**:
- Week Summary Header
- Daily Breakdown Chart
- Session Statistics Cards
- Focus Score Trends
- Productivity Insights
- Export Button

---

### Monthly Insight Detail Page (`/reports/monthly/{insightId}`)
**Purpose**: Detailed view of monthly productivity insights

**Components**:
- Month Summary Header
- Weekly Comparison Chart
- Productivity Heat Map
- Top Distractions List
- Key Insights Cards
- Recommendations Panel
- Export Button

---

### Analytics Trends Page (`/reports/trends`)
**Purpose**: Long-term trend analysis and comparisons

**Components**:
- Time Range Selector (3/6/12 months)
- Multi-Line Trend Chart
- Metric Selector
- Comparison Toggle (month-over-month, year-over-year)
- Statistical Summary

---

## Components

### DashboardSummaryCard
```typescript
interface DashboardSummaryCardProps {
  title: string;
  value: string | number;
  subtitle?: string;
  trend?: {
    direction: 'up' | 'down' | 'stable';
    percentage: number;
  };
  icon?: ReactNode;
}
```

**Visual Requirements**:
- Card with subtle shadow
- Large value display
- Trend indicator with color (green=up, red=down, gray=stable)
- Icon in corner
- Smooth hover animation

---

### WeeklyReportCard
```typescript
interface WeeklyReportCardProps {
  report: WeeklyReport;
  onClick: () => void;
}

interface WeeklyReport {
  reportId: string;
  weekStartDate: string;
  weekEndDate: string;
  totalSessions: number;
  completedSessions: number;
  totalFocusMinutes: number;
  averageFocusScore: number;
}
```

**Display**:
- Week date range (e.g., "Dec 18 - Dec 24, 2024")
- Total sessions count
- Focus time (formatted: "10h 30m")
- Average focus score with visual indicator
- Completion rate percentage
- Quick stats grid
- Click to view details

---

### MonthlyInsightCard
```typescript
interface MonthlyInsightCardProps {
  insight: MonthlyInsight;
  onClick: () => void;
}

interface MonthlyInsight {
  insightId: string;
  month: number;
  year: number;
  totalSessions: number;
  totalFocusHours: number;
  averageFocusScore: number;
  weeklyTrend: 'improving' | 'stable' | 'declining';
  keyInsights: string[];
}
```

**Display**:
- Month and year (e.g., "December 2024")
- Total focus hours
- Average focus score
- Trend badge
- Top 2 key insights preview
- Click to view full report

---

### DailyBreakdownChart
```typescript
interface DailyBreakdownChartProps {
  data: DailyBreakdown[];
  metric: 'sessions' | 'minutes' | 'score';
}

interface DailyBreakdown {
  date: string;
  sessionCount: number;
  focusMinutes: number;
  averageScore: number;
}
```

**Visual Requirements**:
- Bar chart for sessions/minutes
- Line chart for score
- 7-day view (Monday - Sunday)
- Tooltips on hover
- Color-coded by performance
- Responsive to container size

---

### ProductivityHeatMap
```typescript
interface ProductivityHeatMapProps {
  month: number;
  year: number;
  data: HeatMapData[];
}

interface HeatMapData {
  date: string;
  focusMinutes: number;
  sessionCount: number;
}
```

**Visual Requirements**:
- Calendar grid layout
- Color intensity based on focus minutes
- Hover shows detailed stats
- Legend for color scale
- Empty days shown in gray

---

### TrendsChart
```typescript
interface TrendsChartProps {
  data: TrendData[];
  metrics: MetricConfig[];
  timeRange: '3m' | '6m' | '12m';
}

interface TrendData {
  period: string;
  focusMinutes: number;
  sessionCount: number;
  averageScore: number;
  completionRate: number;
}
```

**Visual Requirements**:
- Multi-line chart
- Toggle metrics on/off
- Zoom and pan controls
- Comparison overlay option
- Export chart as image

---

### InsightsList
```typescript
interface InsightsListProps {
  insights: Insight[];
  maxItems?: number;
}

interface Insight {
  id: string;
  text: string;
  category: 'productivity' | 'focus' | 'distraction' | 'improvement';
  priority: number;
}
```

**Display**:
- Categorized insights
- Priority sorting
- Icon per category
- Expandable details
- Action buttons (e.g., "Set Goal", "View Details")

---

### ExportReportModal
```typescript
interface ExportReportModalProps {
  isOpen: boolean;
  onClose: () => void;
  onExport: (config: ExportConfig) => void;
}

interface ExportConfig {
  reportType: 'weekly' | 'monthly' | 'custom';
  format: 'csv' | 'pdf' | 'json';
  startDate?: string;
  endDate?: string;
  includeCharts: boolean;
}
```

**Fields**:
- Report Type Selector
- Date Range Picker
- Format Selection (Radio buttons)
- Include Charts Toggle
- Export Button

---

### GenerateReportButton
```typescript
interface GenerateReportButtonProps {
  reportType: 'weekly' | 'monthly';
  onGenerate: (config: GenerateConfig) => void;
  disabled?: boolean;
}

interface GenerateConfig {
  reportType: string;
  weekStartDate?: string;
  month?: number;
  year?: number;
}
```

**Behavior**:
- Opens configuration modal
- Shows loading state during generation
- Success notification on completion
- Error handling with retry

---

## User Flows

### View Weekly Reports Flow
1. User navigates to Reports Dashboard
2. Weekly reports section displays recent reports
3. User can filter by date range
4. User clicks on a weekly report card
5. Detail page opens with full breakdown
6. Charts and stats displayed
7. User can export report

### Generate Monthly Insight Flow
1. User clicks "Generate Monthly Insight"
2. Modal opens with month/year selector
3. User selects month and year
4. User clicks "Generate"
5. System shows loading indicator
6. Report generates in background
7. Success notification appears
8. New insight appears in list
9. User can click to view details

### Export Reports Flow
1. User clicks "Export" button
2. Export modal opens
3. User selects report type and date range
4. User chooses format (CSV/PDF/JSON)
5. User toggles chart inclusion
6. User clicks "Export"
7. File downloads automatically
8. Success notification shown

### View Trends Flow
1. User navigates to Trends page
2. Default 6-month view loads
3. Charts display with selected metrics
4. User can toggle time range
5. User can add/remove metrics
6. User can compare periods
7. User can export chart image

---

## State Management

### Reports Store
```typescript
interface ReportsState {
  weeklyReports: WeeklyReport[];
  monthlyInsights: MonthlyInsight[];
  dashboardData: DashboardData | null;
  selectedReport: WeeklyReport | MonthlyInsight | null;
  trendsData: TrendData[];
  isLoading: boolean;
  isGenerating: boolean;
  error: string | null;
}
```

### Actions
- `fetchWeeklyReports(startDate?: string, endDate?: string)`
- `fetchMonthlyInsights(year?: number)`
- `fetchDashboardData()`
- `fetchReportDetails(reportId: string, type: 'weekly' | 'monthly')`
- `generateWeeklyReport(config: GenerateConfig)`
- `generateMonthlyInsight(config: GenerateConfig)`
- `exportReport(config: ExportConfig)`
- `fetchTrends(timeRange: string)`

---

## Validation Rules

| Field | Rule |
|-------|------|
| Week Start Date | Must be a Monday |
| Week End Date | Must be a Sunday |
| Month | 1-12 |
| Year | Current year or earlier |
| Export Date Range | Maximum 6 months |
| Time Range | Must be valid period |

---

## Accessibility Requirements

- All charts have text alternatives
- Keyboard navigation for all controls
- Screen reader announcements for data updates
- High contrast mode for charts
- Focus indicators on interactive elements
- ARIA labels on all data visualizations
- Table view alternative for charts

---

## Responsive Design

| Breakpoint | Layout | Charts |
|------------|--------|--------|
| Mobile (<768px) | Single column, stacked cards | Simplified charts, horizontal scroll |
| Tablet (768-1024px) | 2-column grid | Medium detail charts |
| Desktop (>1024px) | 3-column grid, side panels | Full detail charts with interactions |

---

## Data Visualization Guidelines

### Color Palette
- **Excellent (90-100)**: #22c55e (Green)
- **Good (70-89)**: #3b82f6 (Blue)
- **Average (50-69)**: #eab308 (Yellow)
- **Poor (<50)**: #ef4444 (Red)
- **Neutral**: #64748b (Gray)

### Chart Types
- **Session Count**: Bar chart
- **Focus Score**: Line chart with area fill
- **Productivity Heat Map**: Calendar-style heat map
- **Trends**: Multi-line chart
- **Completion Rate**: Donut chart
- **Daily Breakdown**: Combined bar and line chart

### Animation
- Chart data: 800ms ease-out
- Card hover: 200ms ease-in-out
- Page transitions: 300ms ease-in-out
- Loading states: Skeleton screens

---

## Performance Requirements

1. **Initial Load**: Dashboard data loads in <2 seconds
2. **Chart Rendering**: Charts render in <500ms
3. **Report Generation**: Background job, notify when complete
4. **Export**: Stream large exports, show progress
5. **Pagination**: Load 10 reports per page
6. **Caching**: Cache dashboard data for 1 hour
7. **Lazy Loading**: Load chart libraries on demand

---

## Error Handling

- **No Data**: Show empty state with "Generate Report" CTA
- **Generation Failed**: Show error message with retry button
- **Export Failed**: Show error notification, suggest smaller range
- **Network Error**: Show offline indicator, retry on reconnect
- **Invalid Date Range**: Inline validation with helpful message

---

## Notification Messages

- **Report Generated**: "Your weekly report is ready to view!"
- **Insight Created**: "Monthly insights for December 2024 are now available"
- **Export Complete**: "Your report has been downloaded"
- **Generation In Progress**: "Generating report... This may take a moment"
- **Error**: "Unable to generate report. Please try again"

---

## Loading States

- **Dashboard**: Skeleton cards for summary
- **Report List**: Skeleton cards in grid
- **Chart Loading**: Shimmer placeholder
- **Export**: Progress bar with percentage
- **Generation**: Spinner with status text
