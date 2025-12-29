# Frontend Requirements - Medication Tracking

## User Interface Components

### 1. Medication List

**Display:**
- Active medications cards
  - Medication name and dosage
  - Schedule (time and frequency)
  - Next dose time
  - Adherence percentage (last 30 days)
  - "Log Dose" quick button
- "Add Medication" button
- Inactive/past medications (collapsible)

### 2. Add Medication Form

**Fields:**
- Medication name (text input with autocomplete)
- Dosage (text input, e.g., "10mg")
- Frequency (dropdown):
  - Once daily
  - Twice daily
  - Three times daily
  - As needed
  - Custom schedule
- Time(s) of day (time pickers)
- Start date (date picker, default today)
- Reminder toggle
- Notes (textarea)

### 3. Log Dose Interface

**Quick Log:**
- Click "Log Dose" on medication card
- Confirm time taken (default: now)
- Option to log BP reading at same time
- One-tap confirmation

**Missed Dose:**
- Mark as missed
- Reschedule option
- Skip this dose

### 4. Medication Effectiveness Dashboard

**Display:**
- Before/After comparison cards
  - Average BP before medication
  - Average BP after medication
  - Improvement arrows and values
- Effectiveness score (0-10)
- Timeline chart showing BP over medication period
  - Vertical line marking medication start
  - Shaded regions for before/after
- Statistical confidence indicator

### 5. Timing Correlation View

**Chart:**
- X-axis: Hours since medication
- Y-axis: Average BP
- Shows optimal measurement window
- Peak effectiveness period highlighted

**Insights:**
- "Your BP is lowest 2-4 hours after medication"
- "Best time to measure: 3 hours after dose"

### 6. Adherence Tracker

**Display:**
- Calendar view with dose status:
  - Green checkmark: Taken on time
  - Yellow: Taken late
  - Red X: Missed
  - Grey: Not scheduled
- Adherence percentage (month and all-time)
- Streak counter (consecutive days)
- Reminders settings

## User Flows

### Flow 1: Add Medication
1. User clicks "Add Medication"
2. Form appears
3. User types medication name (autocomplete suggests)
4. User enters dosage "10mg"
5. User selects "Once daily"
6. User sets time "8:00 AM"
7. User enables reminder
8. User clicks "Add"
9. Medication added to list
10. First reminder scheduled

### Flow 2: Log Dose
1. Reminder notification appears at 8:00 AM
2. User clicks notification
3. "Log Dose" confirmation appears
4. User confirms
5. Dose marked as taken
6. Optional: "Take BP reading now?" prompt
7. User records BP reading
8. Reading tagged with medication timing

### Flow 3: View Effectiveness
1. User navigates to medication card
2. User clicks "View Effectiveness"
3. Effectiveness dashboard loads
4. Before/after comparison shows improvement
5. User views detailed chart
6. User reads insights
7. User exports for doctor visit

## Analytics Events

- `medication_added`
- `dose_logged`
- `dose_missed`
- `effectiveness_viewed`
- `reminder_sent`
- `reminder_interacted`
