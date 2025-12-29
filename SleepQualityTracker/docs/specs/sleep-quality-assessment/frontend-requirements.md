# Frontend Requirements - Sleep Quality Assessment

## Components

### QualityScoreDisplay
- Large numerical score (0-100)
- Color-coded circle/gauge (red < 40, yellow 40-60, green 60-85, blue > 85)
- Score breakdown by factor (duration, efficiency, stages, consistency)
- Comparison to personal average

### QualityFactorsBreakdown
- Visual breakdown of contributing factors
- Each factor shows score and weight
- Tooltips explaining each factor
- Actionable insights for improvement

### QualityTrendChart
- Line chart showing quality over time
- Selectable time range (7 days, 30 days, 90 days, year)
- Moving average trendline
- Highlight exceptional and poor nights
- Annotations for significant events

### TrendSummary
- Average quality for period
- Trend direction indicator (↑↓→)
- Percentage change from previous period
- Best and worst nights in period
- Insights text

### QualityInsights
- Personalized recommendations
- Patterns identified
- Factors to improve
- Positive reinforcement for good trends

## Pages

### Quality Dashboard (/quality)
- Current quality score
- Trend chart
- Factor breakdown
- Recent insights

### Quality History (/quality/history)
- Historical quality scores
- Detailed trend analysis
- Comparison charts

## Interactions
- View quality immediately after logging sleep
- Drill into factor details
- Compare periods
- Export quality report

## Visualizations
- Gauge chart for score
- Stacked bar for factors
- Line chart for trends
- Heatmap calendar for monthly view
