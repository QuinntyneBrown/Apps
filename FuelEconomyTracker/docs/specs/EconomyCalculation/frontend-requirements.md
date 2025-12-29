# Economy Calculation - Frontend Requirements

## Pages/Views

### 1. Economy Dashboard
**Route**: `/vehicles/{vehicleId}/economy`

**UI Components**:
- **Hero Section**: Current Average MPG
  - Large MPG number (48px font)
  - Trend indicator (↑ improving, → stable, ↓ declining)
  - Comparison to personal best (+X MPG away)
  - Comparison to EPA rating

- **Statistics Cards**:
  - Last 7 Days Average
  - Last 30 Days Average
  - Lifetime Average
  - Personal Best (with date)

- **MPG Trend Chart**:
  - Line graph showing MPG over time
  - Selectable timeframes (Week, Month, 3M, Year, All)
  - EPA rating reference line
  - Personal best marker
  - Interactive tooltips with details

- **Recent Calculations List**:
  - Last 5-10 MPG calculations
  - Each showing: Date, MPG, Miles, Gallons
  - Color-coded badges (above/below average)

- **Quick Actions**:
  - "Add Fill-Up" button
  - "Set MPG Goal" button
  - "View All History" link

**Interactions**:
- Tap chart data points for details
- Switch time periods with smooth transitions
- Pull to refresh latest data
- Celebrate personal bests with confetti animation

### 2. MPG History Page
**Route**: `/vehicles/{vehicleId}/economy/history`

**UI Components**:
- **Filter Bar**:
  - Time period selector
  - Trip type filter
  - Sort options

- **Detailed Chart**:
  - More comprehensive than dashboard
  - Multiple series (MPG, Running Average, EPA)
  - Annotations for significant events
  - Zoom and pan capabilities

- **Statistics Table**:
  - Date, MPG, Miles, Gallons, Trip Type
  - Sortable columns
  - Highlight personal bests
  - Flag outliers

- **Insights Panel**:
  - Best performing month/season
  - Worst performing conditions
  - Improvement trends
  - Goal progress

### 3. Set MPG Goal Modal
**Trigger**: Click "Set MPG Goal" button

**UI Components**:
- **Goal Configuration**:
  - Target MPG slider/input
  - Current baseline (auto-filled)
  - Timeframe selector (30/60/90 days)
  - Motivation text input

- **Visual Preview**:
  - Chart showing current vs target
  - Estimated savings if achieved
  - Required improvement percentage

- **Action Buttons**:
  - "Set Goal"
  - "Cancel"

## UI Components Library

### MPGBadge Component
```typescript
interface MPGBadgeProps {
  mpg: number;
  average: number;
  size?: 'sm' | 'md' | 'lg';
  showDifference?: boolean;
}

// Visual: Circular badge with MPG value
// Green if above average, yellow if at, red if below
// Shows +/- difference if enabled
```

### EconomyTrendChart Component
```typescript
interface EconomyTrendChartProps {
  data: EconomyDataPoint[];
  timeRange: TimeRange;
  showEPA?: boolean;
  showPersonalBest?: boolean;
  interactive?: boolean;
}

// Uses Chart.js or Recharts
// Responsive design
// Touch-friendly on mobile
```

### PersonalBestBanner Component
```typescript
interface PersonalBestBannerProps {
  mpg: number;
  date: Date;
  beatPreviousBy: number;
}

// Celebratory design with icon
// Prominent display
// Share button included
```

## State Management

```typescript
interface EconomyState {
  stats: {
    currentAverage: number;
    last30DaysAverage: number;
    lifetimeAverage: number;
    personalBest: PersonalBest;
    epaComparison: EPAComparison;
    trend: 'improving' | 'declining' | 'stable';
  };
  history: EconomyCalculation[];
  chartData: ChartDataPoint[];
  filters: {
    timeRange: TimeRange;
    tripType?: string;
  };
  goal?: EconomyGoal;
  loading: boolean;
  error?: string;
}
```

## API Integration

```typescript
class EconomyService {
  async getEconomyStats(vehicleId: string): Promise<EconomyStats> {
    // GET /api/vehicles/{vehicleId}/economy-stats
  }

  async getEconomyHistory(
    vehicleId: string,
    period: Period,
    granularity: Granularity
  ): Promise<EconomyHistory> {
    // GET /api/vehicles/{vehicleId}/economy-history
  }

  async setEconomyGoal(goal: EconomyGoal): Promise<void> {
    // POST /api/economy-goals
  }
}
```

## Responsive Design

**Mobile**:
- Stack cards vertically
- Simplified chart with fewer data points
- Bottom sheet for filters
- Swipeable chart time periods

**Tablet**:
- Two-column card layout
- Side panel for filters
- Enhanced chart with more details

**Desktop**:
- Three-column layout
- Full-featured chart with all controls
- Sidebar for additional stats

## Animations & Interactions

- Confetti animation on personal best achievement
- Smooth chart transitions when changing timeframes
- Number counter animation for MPG values
- Skeleton loading for charts
- Success toast on goal setting

## Accessibility

- Chart data available in table format
- Keyboard controls for chart navigation
- Screen reader descriptions for trend indicators
- High contrast mode for charts
- Alternative text for all visualizations
