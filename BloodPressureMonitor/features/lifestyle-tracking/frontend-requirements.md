# Frontend Requirements - Lifestyle Tracking

## UI Components

### 1. Lifestyle Dashboard

**Quick Log Cards:**
- Sodium Intake (mg counter with presets)
- Exercise (type, duration, intensity)
- Stress Level (1-10 slider with emoji indicators)
- Sleep Hours (number input)
- Alcohol (servings counter)
- Caffeine (cups/servings counter)

**Daily Summary:**
- Today's logged factors
- Color-coded indicators (green/yellow/red based on recommendations)
- Quick stats vs targets

### 2. Sodium Tracker

**Input Methods:**
- Manual entry (mg)
- Meal-based entry with common foods database
- Photo + AI estimation (future)
- Barcode scanner (future)

**Display:**
- Daily total vs 2300mg target
- Progress bar (green if under, red if over)
- Historical chart (7/30 day view)
- Top sodium sources (pie chart)

### 3. Exercise Logger

**Form:**
- Exercise type (dropdown with icons):
  - Cardio, Strength, Yoga, Walking, Other
- Duration (slider or input)
- Intensity: Light / Moderate / Vigorous
- Notes

**Pre-Exercise BP Prompt:**
- "Take BP reading before exercise?"
- "Take BP reading after exercise (30 min)?"

### 4. Stress Tracker

**Input:**
- Stress level slider (1-10) with emojis
  - 1-3: 😊 Calm
  - 4-6: 😐 Moderate
  - 7-10: 😰 High
- Stress triggers (multi-select tags):
  - Work, Family, Financial, Health, Traffic, Other
- Notes (textarea)

**Stress BP Correlation Display:**
- Chart showing stress level vs BP
- Scatter plot with trend line
- "Your BP increases by ~8 mmHg on high stress days"

### 5. Correlation Insights

**Display Cards:**
- Factor name and icon
- Correlation strength (visual gauge)
- Impact statement:
  - "High sodium linked to +8 mmHg systolic"
  - "Exercise associated with -6 mmHg average"
  - "Stress strongly correlated with elevated BP"
- Recommendations
- "Learn More" expandable section

**Correlation Matrix:**
- Heatmap showing all factor correlations
- Click cell to see details

### 6. Lifestyle Impact Chart

**Multi-Line Chart:**
- BP readings (line)
- Lifestyle factors (bar overlays or separate lines)
- Synchronized X-axis (time)
- Toggle visibility of each factor
- Identify patterns visually

## User Flows

### Flow 1: Log Daily Lifestyle Factors
1. User opens app in evening
2. Dashboard shows "Log Today's Factors"
3. User taps "Sodium"
4. User enters 2800mg
5. Warning: "Above recommended limit"
6. User taps "Exercise"
7. User selects "Cardio" 30 min "Moderate"
8. User taps "Stress"
9. User sets slider to 6
10. User selects trigger "Work"
11. Summary shows all factors logged
12. User sees correlation insights update

### Flow 2: Discover Sodium Correlation
1. User views Insights page
2. Sodium correlation card shows "Strong" (0.72)
3. User taps to expand
4. Chart shows sodium vs BP over 30 days
5. High sodium days have elevated BP
6. Recommendation: "Reduce to <2300mg"
7. User taps "Track Sodium Better"
8. Sodium tracker opens with targets set

## Analytics Events

- `lifestyle_factor_logged`
- `correlation_viewed`
- `recommendation_followed`
- `exercise_logged`
- `stress_logged`
