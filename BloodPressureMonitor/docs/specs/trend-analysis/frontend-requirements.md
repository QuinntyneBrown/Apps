# Frontend Requirements - Trend Analysis

## Overview
Visual displays and interactive charts showing blood pressure trends, patterns, and predictions.

## User Interface Components

### 1. Trend Dashboard

**Location:** Main dashboard tab or dedicated "Trends" page

**Components:**
- Time range selector (7d, 30d, 90d, 6m, 1y, All)
- Trend summary cards
- Main trend chart
- Trend insights panel
- Volatility indicator

**Trend Summary Cards:**
- Average BP (large display: "125/82")
  - Trend arrow (↑ rising, ↓ lowering, → stable)
  - Change from previous period: "+3/-1 mmHg"
- Current Trend
  - Direction badge (Rising/Lowering/Stable)
  - Rate of change: "+2.5 mmHg/month"
- Volatility Score
  - Visual gauge (Low/Moderate/High)
  - Color-coded: Green/Yellow/Red
- Data Quality
  - Number of readings in period
  - Measurement consistency score

### 2. Interactive Trend Chart

**Chart Type:** Line chart with dual Y-axis

**Features:**
- Systolic line (red/orange)
- Diastolic line (blue)
- Pulse line (optional, toggle on/off)
- Shaded regions for BP categories:
  - Normal (green shade)
  - Elevated (yellow shade)
  - High Stage 1 (orange shade)
  - High Stage 2 (red shade)
- Trend line overlay (dotted line showing overall direction)
- Data points clickable to view reading details
- Zoom and pan controls
- Crosshair with values on hover

**Interactive Elements:**
- Click data point → Show reading detail tooltip
- Drag to zoom into time range
- Double-click to reset zoom
- Toggle visibility of systolic/diastolic/pulse
- Switch between line/area/scatter chart types

**Tooltip on Hover:**
```
Dec 29, 2025 8:30 AM
BP: 125/82 mmHg
Pulse: 73 bpm
Context: Resting
```

### 3. Trend Insights Panel

**Location:** Below or beside trend chart

**Insights Displayed:**
- Trend Detection:
  - "Your blood pressure has been rising over the past month"
  - "Average increase: +2.5 mmHg systolic per month"
- Pattern Recognition:
  - "Your morning readings are typically 5 mmHg higher than evening"
  - "Readings taken after exercise show 10 mmHg increase"
- Volatility Analysis:
  - "Your readings show moderate variability"
  - "Consider measuring at more consistent times"
- Predictions:
  - "If this trend continues, your BP may reach 135/88 by Feb 2026"
  - "Confidence: 75%"

**Insight Cards:**
- Icon indicating type (trend, pattern, prediction, concern)
- Title
- Description
- "Learn more" expandable section
- Dismiss button

### 4. Comparison View

**Purpose:** Compare different time periods

**Layout:**
- Split view with two time periods
- Period selectors for each side
- Side-by-side statistics
- Difference indicators

**Display:**
```
┌─────────────────┬─────────────────┐
│  Dec 1-15       │  Dec 16-29      │
│  Avg: 128/84    │  Avg: 122/80    │
│  ↓ -6/-4        │                 │
└─────────────────┴─────────────────┘
```

### 5. Volatility Analyzer

**Location:** Dedicated section or expandable card

**Display:**
- Volatility score gauge (0-10)
- Explanation: "Your readings vary by ±12 mmHg on average"
- Contributing factors:
  - Measurement time consistency
  - Context variability
  - Technique consistency
- Recommendations:
  - "Take readings at the same time each day"
  - "Ensure proper measurement technique"
- Historical volatility chart (volatility over time)

### 6. Prediction View

**Location:** Expandable section in Trends page

**Display:**
- Current trend visualization
- Projected values table:
  ```
  30 days: 128/84 mmHg (85% confidence)
  60 days: 131/86 mmHg (70% confidence)
  90 days: 134/88 mmHg (60% confidence)
  ```
- Confidence indicator for each prediction
- Disclaimer: "Predictions based on current trend. Results may vary."
- Scenario modeling:
  - "If you maintain goal range: 120/80 by March"
  - "If trend continues: 135/88 by March"

## User Flows

### Flow 1: View Trend Analysis
1. User navigates to Trends page
2. Default 30-day view loads
3. Trend chart renders with data
4. Trend summary cards populate
5. Insights panel shows detected trends
6. User changes time range to 90 days
7. Chart and insights update
8. User hovers over data point
9. Tooltip displays reading details
10. User clicks data point
11. Reading detail modal opens

### Flow 2: Compare Time Periods
1. User clicks "Compare" button
2. Comparison view activates
3. User selects first period: Nov 2025
4. User selects second period: Dec 2025
5. Side-by-side statistics display
6. Difference indicators show improvement
7. User views comparison chart
8. User exports comparison as PDF

### Flow 3: Analyze Volatility
1. User notices "High Volatility" indicator
2. User clicks volatility score
3. Volatility analyzer expands
4. Chart shows volatility over time
5. Recommendations displayed
6. User views contributing factors
7. User clicks "Learn More"
8. Educational content on measurement consistency displays

## Data Visualization

### Chart Library
- Use Chart.js or D3.js for flexibility
- Support responsive resizing
- Smooth animations for data updates
- Accessible color palette

### Color Scheme
- Systolic: #E74C3C (red-orange)
- Diastolic: #3498DB (blue)
- Pulse: #9B59B6 (purple)
- Trend line: #34495E (dark gray, dashed)
- Background shading: Semi-transparent category colors

### Chart Performance
- Virtualize data points for large datasets (>500 points)
- Aggregate data for long time ranges
- Lazy load historical data
- Cache rendered charts

## Responsive Design

### Mobile
- Simplified chart (hide minor gridlines)
- Swipe to scroll through time
- Tap data point for details
- Collapsible insights panel
- Stack summary cards vertically

### Tablet/Desktop
- Full-featured chart with all controls
- Side-by-side layout for chart and insights
- Hover interactions
- Keyboard shortcuts for zoom/pan

## Accessibility

- Chart data available in table format
- Screen reader announces trend direction
- Keyboard navigation for chart interactions
- High contrast mode
- Text descriptions of visual trends

## State Management

### Global State
- Selected time range
- Chart data (cached)
- Trend analysis results
- User preferences (chart type, visible series)

### URL State
- Encode time range in URL
- Deep linking to specific trend views
- Shareable links

## Analytics Events

- `trend_viewed` - User views trends page
- `time_range_changed` - User changes analysis period
- `trend_chart_interacted` - User zooms/pans chart
- `comparison_viewed` - User compares periods
- `prediction_viewed` - User views future predictions
- `insight_expanded` - User reads more about insight
