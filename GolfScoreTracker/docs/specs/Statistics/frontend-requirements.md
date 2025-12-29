# Statistics - Frontend Requirements

## Pages

### Stats Dashboard (`/statistics`)
- **Period Selector**: 7d, 30d, 90d, Year, All
- **Scoring Average**: Large display with trend
- **Key Metrics Grid**:
  - Fairways Hit %
  - Greens in Regulation %
  - Average Putts/Round
  - Total Birdies
- **Performance Charts**:
  - Scoring trend over time
  - Stat breakdown radar chart
- **Strengths & Weaknesses**:
  - Green badges for strengths
  - Yellow/red for weaknesses
  - Practice recommendations

### Detailed Stats (`/statistics/detailed`)
- All statistics in table format
- Drill-down by course, time period
- Comparison to handicap peers
- Export to CSV

## UI Components

### StatCard
```typescript
interface StatCard {
  label: string;
  value: string;
  trend?: 'up' | 'down' | 'stable';
  percentile?: number;
  icon: string;
}
```

### RadarChart
- Multi-axis chart showing all key stats
- Overlay peer average
- Interactive tooltips

### StrengthsWeaknessesPanel
- List of identified areas
- Priority level
- Suggested drills/practice
