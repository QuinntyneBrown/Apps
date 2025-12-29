# Frontend Requirements - Goal Management

## User Interface Components

### 1. Goal Dashboard Card

**Location:** Main dashboard, prominent position

**Display:**
- Current active goal
- Progress ring/bar (percentage in goal range)
- Current streak (days)
- Readings in range: "45/56 (80%)"
- Time to deadline: "45 days remaining"
- Quick stats: Last 7 days in goal

**Visual Elements:**
- Circular progress indicator (colored by achievement %)
  - 0-50%: Red
  - 51-75%: Orange
  - 76-90%: Yellow
  - 91-100%: Green
- Streak flame icon with number
- Mini sparkline chart of recent readings

**Actions:**
- "View Details" button
- "Record Reading" quick action

### 2. Set Goal Interface

**Location:** Modal or dedicated page

**Form Fields:**
- Target BP Range
  - Systolic range: Min/Max sliders (110-140)
  - Diastolic range: Min/Max sliders (70-90)
  - Visual range indicator showing category
- Goal Type (dropdown)
  - Maintain healthy BP
  - Lower BP
  - Achieve doctor's target
- Deadline (date picker, optional)
- "This goal was set by my doctor" checkbox

**Preset Templates:**
- "Normal BP" (120/80 ± 10)
- "Prehypertension Management" (130/85 ± 10)
- "Custom Range"

**Visual Feedback:**
- Show BP category for selected range
- Display how current average compares to goal
- Estimated time to achieve (based on current trend)

**Actions:**
- "Set Goal" (primary button)
- "Save as Draft"
- "Cancel"

### 3. Goal Progress Page

**Sections:**

**Progress Overview:**
- Large progress percentage (circular gauge)
- Achievement metrics cards:
  - Readings in range
  - Current streak
  - Longest streak
  - Days remaining
- Achievement timeline

**Progress Chart:**
- Line chart of readings over time
- Shaded goal range area
- Markers for in-range vs out-of-range readings
- Streak visualization (highlighted periods)

**Milestones:**
- Achievement badges:
  - First goal reached
  - 7-day streak
  - 30-day streak
  - 80% achievement
  - Goal completed
- Progress towards next milestone

**Insights:**
- "You're 5 readings away from 80% achievement!"
- "Your morning readings are mostly in goal range"
- "Keep up your current trend to reach your goal by March"

### 4. Goal History

**Display:**
- List of past goals (achieved, expired, archived)
- Each card shows:
  - Goal range
  - Achievement percentage
  - Status badge
  - Date range
- Filter by status
- Sort by date

### 5. Achievement Celebration

**Triggered:** When goal reached or milestone achieved

**Modal Display:**
- Confetti animation
- Large checkmark icon
- Congratulatory message
  - "Goal Reached! 🎉"
  - "You've maintained BP in goal range!"
- Achievement summary
- "Share Achievement" button
- "Set New Goal" button

## User Flows

### Flow 1: Set New Goal
1. User clicks "Set Goal" on dashboard
2. Goal form appears
3. User selects "Achieve doctor's target"
4. User adjusts systolic slider to 110-120
5. User adjusts diastolic slider to 70-80
6. Visual shows "Normal BP range"
7. User sets deadline to 6 months out
8. User checks "Set by doctor"
9. User clicks "Set Goal"
10. Confirmation appears
11. Goal dashboard updates with new goal

### Flow 2: Track Progress
1. User navigates to Goals page
2. Progress overview loads
3. User sees 75% achievement
4. User views progress chart
5. User identifies out-of-range readings
6. User clicks on out-of-range day
7. Reading details appear with context
8. User adds note for future reference

### Flow 3: Celebrate Achievement
1. User records reading in goal range
2. System detects 30-day streak milestone
3. Celebration modal appears with confetti
4. "30-Day Streak Achieved!" message
5. User views achievement details
6. User shares to social media
7. Achievement added to profile

## Responsive Design

- Mobile: Stacked layout, simplified charts
- Desktop: Side-by-side progress metrics
- Touch-friendly sliders and date pickers

## Accessibility

- Progress announced to screen readers
- Keyboard-accessible sliders
- High contrast mode for progress indicators
- Text alternatives for visual celebrations

## Analytics Events

- `goal_created` - New goal set
- `goal_achieved` - Goal reached
- `milestone_reached` - Streak or % milestone
- `goal_viewed` - User views progress
- `goal_updated` - User modifies goal
