# Screening Management - Frontend Requirements

## Components

### ScreeningDashboard
- **Purpose**: Main view showing all user screenings
- **Elements**:
  - Screening status cards (upcoming, overdue, completed)
  - Quick action buttons (schedule, view details)
  - Filter by status, type, date range
  - Search functionality

### ScreeningScheduleForm
- **Purpose**: Form for scheduling new screenings
- **Elements**:
  - Screening type dropdown
  - Provider search/select
  - Date/time picker
  - Location input
  - Insurance information section
  - Preparation notes display

### ScreeningDetailView
- **Purpose**: Detailed view of individual screening
- **Elements**:
  - Status badge and timeline
  - Provider and location info
  - Preparation checklist
  - Action buttons (complete, cancel, reschedule)
  - Results section (if completed)
  - Related documents

### ScreeningResultsForm
- **Purpose**: Form for recording screening results
- **Elements**:
  - Result date picker
  - Summary text area
  - Abnormal findings toggle
  - Follow-up required toggle
  - Provider notes field
  - Document upload

### OverdueScreeningsAlert
- **Purpose**: Prominent display of overdue screenings
- **Elements**:
  - Warning styling (red/orange)
  - Days overdue count
  - Quick schedule button
  - Risk information tooltip

## State Management

```typescript
interface ScreeningState {
  screenings: Screening[];
  selectedScreening: Screening | null;
  overdueCount: number;
  upcomingCount: number;
  loading: boolean;
  error: string | null;
  filters: ScreeningFilters;
}

interface Screening {
  id: string;
  userId: string;
  screeningType: ScreeningType;
  provider: Provider | null;
  status: 'Scheduled' | 'Completed' | 'Cancelled';
  scheduledDate: Date | null;
  completedDate: Date | null;
  nextDueDate: Date | null;
  results: ScreeningResult | null;
}
```

## User Interactions

1. **Schedule Screening**: Form validation, provider lookup, calendar integration
2. **Complete Screening**: Confirmation dialog, result prompt
3. **Cancel Screening**: Reason required, rescheduling offer
4. **View Results**: Expandable section, document viewer
5. **Filter/Search**: Real-time filtering, saved filter presets

## Responsive Design

- Mobile: Card-based list, bottom sheet for details
- Tablet: Two-column layout, side panel for details
- Desktop: Full dashboard with data table and detail panel
