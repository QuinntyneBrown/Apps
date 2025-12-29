# Frontend Requirements - Habit Correlation

## Components

### HabitLogForm
- Habit type selector (caffeine, exercise, alcohol, screen time, meals)
- Time picker for when habit occurred
- Intensity/quantity inputs (varies by habit type)
- Quick log buttons for common habits

### CorrelationInsights
- List of identified correlations
- Strength indicator (weak, moderate, strong)
- Impact visualization (positive/negative on sleep)
- Confidence level
- Recommendations based on correlations

### CaffeineTracker
- Log caffeine intake (type, amount, time)
- Optimal cutoff time display
- Daily caffeine timeline
- Impact on sleep quality chart

### ExerciseOptimizer
- Log exercise (type, duration, time, intensity)
- Optimal exercise window display
- Exercise vs sleep quality correlation
- Recommendations for timing

### HabitImpactChart
- Scatter plot: habit timing vs sleep quality
- Trendline showing correlation
- Best/worst scenarios highlighted

## Pages

### Habits Dashboard (/habits)
- Quick log panel
- Recent habits
- Top correlations
- Insights and recommendations

### Correlation Analysis (/habits/correlations)
- Detailed correlation reports
- Habit impact charts
- Comparison tools

## Interactions
- Quick log habits throughout day
- View correlation insights
- Adjust habits based on recommendations
- Track improvement after habit changes
