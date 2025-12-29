# Dose Tracking - Frontend Requirements

## Overview
User interface for logging doses, viewing dose history, and managing medication adherence.

## Key Pages

### 1. Take Dose Screen
**Route**: `/doses/take/{doseId}`

**Features**:
- Large "Take Dose" confirmation button
- Medication details display
- Scheduled vs actual time
- With food reminder if required
- Notes input field
- Quick log functionality

### 2. Dose History View
**Route**: `/doses/history`

**Features**:
- Calendar view of doses (taken/missed/upcoming)
- Filter by medication
- Filter by date range
- Color-coded status (green: taken, red: missed, gray: upcoming)
- Adherence percentage display
- Export to PDF

### 3. Missed Doses Alert
**Component**: Modal/Banner

**Features**:
- List of missed doses
- Make-up dose suggestions
- Quick log option
- Snooze/Dismiss actions

## UI Components

### DoseButton
- Large, touch-friendly confirmation button
- Shows medication name and time
- Disabled if too early (double-dose prevention)
- Success animation on confirm

### DoseCalendar
- Month/week view
- Color-coded dose indicators
- Click to view details
- Streak visualization

### AdherenceChart
- Line graph showing adherence over time
- Goal indicator (typically 80-90%)
- Trend analysis
- Weekly/monthly toggle

## User Flows

### Log Dose Flow
1. User receives reminder or opens app
2. Clicks "Take Dose" on medication card
3. Confirms actual time (defaults to now)
4. Optionally adds notes
5. Clicks "Confirm Taken"
6. Double-dose check runs
7. Success notification shown
8. Adherence updated
9. Next dose countdown starts

### View History Flow
1. User navigates to dose history
2. Calendar displays with color-coded doses
3. User selects date range or medication filter
4. Details shown for selected period
5. Adherence stats calculated and displayed

## State Management

```typescript
interface DoseState {
  upcomingDoses: Dose[];
  doseHistory: Dose[];
  missedDoses: Dose[];
  loading: boolean;
  error: string | null;
}

interface Dose {
  doseId: string;
  medicationId: string;
  medicationName: string;
  scheduledTime: Date;
  actualTime?: Date;
  status: 'Scheduled' | 'Taken' | 'Missed' | 'Delayed';
  notes?: string;
}
```

## Testing
- E2E: Complete dose logging flow
- E2E: Double-dose prevention
- Unit: Adherence calculations
- Integration: API dose logging
