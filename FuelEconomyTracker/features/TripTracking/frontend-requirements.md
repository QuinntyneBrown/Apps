# Trip Tracking - Frontend Requirements

## Pages

### Trip Dashboard (`/trips`)
- Active trip indicator (if trip in progress)
- "Start Trip" button (prominent)
- Recent trips list with purpose icons
- Trip statistics summary (total miles this month, most common purpose)

### Start Trip Modal
- Vehicle selector
- Trip purpose selector (icons: 🏢 Commute, 💼 Business, 🏖️ Leisure)
- Current odometer input
- Optional route input
- Weather auto-detection with manual override
- "Start Tracking" button

### Active Trip View (`/trips/active`)
- Trip timer (elapsed time)
- Estimated miles (if GPS tracking enabled)
- Current fuel level indicator
- Quick note button
- "Complete Trip" button
- "Cancel Trip" button

### Complete Trip Modal
- Auto-filled ending odometer (GPS estimate + confirmation)
- Trip summary (duration, estimated miles)
- Driving conditions selector (highway %, traffic level)
- Estimated MPG display
- Notes field
- "Save Trip" button

### Trip History (`/trips/history`)
- Filterable by purpose, date range
- Each trip shows: date, purpose icon, miles, duration, MPG
- Expandable details
- Edit/Delete actions
- Export trips for period

## UI Components

```typescript
interface TripCard {
  id: string;
  startTime: Date;
  endTime: Date;
  miles: number;
  purpose: TripPurpose;
  mpg?: number;
  conditions: string;
}

interface ActiveTripDisplay {
  elapsedTime: string;
  estimatedMiles: number;
  startTime: Date;
  purpose: TripPurpose;
}
```

## State Management

```typescript
interface TripTrackingState {
  activeTrip?: ActiveTrip;
  recentTrips: Trip[];
  tripHistory: Trip[];
  filters: TripFilters;
  statistics: {
    monthlyMiles: number;
    avgTripLength: number;
    purposeBreakdown: Record<TripPurpose, number>;
  };
}
```

## Features

- GPS integration for automatic odometer estimation
- Background tracking with notifications
- Voice input for trip notes
- Route optimization suggestions
- Expense calculation for business trips
- Calendar integration for trip scheduling
