# Medication Management - Frontend Requirements

## Overview
The Medication Management frontend provides an intuitive interface for pet owners to track medications, receive reminders, and manage their pets' medication schedules.

## User Stories

### As a Pet Owner
1. I want to add a new medication for my pet so that I can track when to give it
2. I want to see all medications my pet is currently taking so I can manage them easily
3. I want to mark a medication dose as given so the system knows it was administered
4. I want to receive reminders before medication is due so I don't forget
5. I want to see my pet's medication schedule for the day/week so I can plan ahead
6. I want to be notified when medication supply is running low so I can order refills
7. I want to view medication history to track adherence and share with vet
8. I want to set up multiple pets' medications in one place
9. I want to mark a dose as missed with a reason
10. I want to discontinue a medication when no longer needed

### As a Veterinarian
1. I want to prescribe medications for my patients electronically
2. I want to view patient medication adherence before appointments
3. I want to be notified of adverse reactions or issues

## Pages/Views

### 1. Medication Dashboard
**Route:** `/pets/{petId}/medications`

**Purpose:** Main hub for viewing and managing all medications for a pet

**Components:**
- Active Medications List
- Today's Schedule Widget
- Refill Alerts Banner
- Quick Add Medication Button
- Adherence Summary Card
- Medication Calendar View Toggle

**Features:**
- Filter by status (Active, Discontinued, All)
- Search medications by name
- Sort by next due time, name, or date added
- Quick action buttons (Give Dose, Mark Missed, View Details)
- Visual indicators for upcoming doses (color-coded)
- Badge notifications for missed doses

**Data Displayed:**
- Medication name and icon/image
- Dosage and route
- Next dose time (countdown if < 2 hours)
- Current supply and days remaining
- Adherence percentage (last 30 days)
- Quick status indicators (✓ On track, ⚠ Low supply, ❌ Missed doses)

### 2. Add/Edit Medication
**Route:** `/pets/{petId}/medications/new` or `/pets/{petId}/medications/{id}/edit`

**Purpose:** Form to add new medication or edit existing

**Form Fields:**
- Medication Name (autocomplete from common medications)
- Medication Type (Tablet, Liquid, Injectable, Topical, etc.)
- Dosage Amount (with unit selector: mg, ml, tablets, etc.)
- Administration Route (Oral, Topical, Injectable, etc.)
- Frequency:
  - Simple: Once daily, Twice daily, Three times daily, etc.
  - Custom: Every X hours/days
  - Specific times: Time picker for exact schedule
- Start Date (date picker)
- End Date (optional, for courses with defined duration)
- Duration in Days (alternative to end date)
- Instructions/Notes (rich text area)
- Prescribing Veterinarian (dropdown)
- Initial Supply Quantity
- Refill Threshold (when to alert)
- Refill Source (Pharmacy name/contact)
- Photo Upload (medication bottle/packaging)

**Validation:**
- All required fields marked with *
- Real-time validation feedback
- Dosage format validation
- Date logic validation (end > start)
- Supply quantity must be positive
- Frequency must result in valid schedule

**UX Features:**
- Medication templates for common prescriptions
- Copy from previous medication
- Smart defaults based on medication type
- Preview of generated schedule
- Save as draft functionality

### 3. Medication Details
**Route:** `/pets/{petId}/medications/{id}`

**Purpose:** Comprehensive view of single medication

**Sections:**

#### Overview Card
- Medication name with photo
- Active status badge
- Prescription details (vet, date prescribed)
- Dosage and route
- Frequency description
- Start and end dates
- Special instructions

#### Supply Tracker
- Visual gauge of current supply
- Days remaining calculation
- Last refill date
- Next refill needed estimate
- Refill history list
- Quick refill button

#### Schedule Section
- Calendar view of doses
- List view option
- Color coding: Green (given), Red (missed), Yellow (upcoming), Gray (scheduled)
- Filter by date range
- Export schedule option

#### Administration History
- Paginated table of all administrations
- Columns: Date/Time, Scheduled, Actual, Given By, Notes
- Filter and search
- Adherence statistics
- Visual adherence chart (calendar heatmap)

#### Actions
- Record Dose button
- Mark Missed button
- Add Refill button
- Edit Medication button
- Discontinue Medication button (with confirmation)

### 4. Record Administration
**Route:** Modal/Drawer on medication details or dashboard

**Purpose:** Quick interface to record that medication was given

**Fields:**
- Medication name (pre-filled, read-only)
- Scheduled time (pre-filled)
- Actual time (defaults to now, editable)
- Given by (defaults to current user)
- Dosage confirmation (pre-filled, editable if varied)
- Notes (optional)
- Photo of pet taking medication (optional)

**UX Features:**
- One-tap "Give Now" for exact scheduled dose
- Quick dismiss after submission
- Success confirmation with next dose time
- Undo option for 30 seconds after submission

### 5. Today's Schedule
**Route:** `/schedule/today` or `/pets/{petId}/schedule/today`

**Purpose:** Day-at-a-glance view of all medication doses

**Features:**
- Timeline view by hour
- Grouped by pet (if multiple)
- Status indicators for each dose
- Quick action buttons
- Filter by pet
- Print-friendly view

**Display:**
- Current time indicator on timeline
- Past doses (gray with status)
- Current/upcoming doses (highlighted)
- Color-coded by medication or status
- Dose details on tap/click

**Actions:**
- Mark as given (single tap)
- Mark as missed (with reason)
- Snooze reminder (15 min, 30 min, 1 hour)
- View medication details

### 6. Weekly Calendar View
**Route:** `/pets/{petId}/schedule/week`

**Purpose:** Week-at-a-glance medication schedule

**Features:**
- 7-day calendar grid
- Color-coded medications
- Time slots for each day
- Visual adherence indicators
- Navigation: Previous/Next week, Jump to date
- Export to calendar apps

**Interactions:**
- Click dose to view details
- Click day to see daily detail
- Drag to reschedule (with confirmation)
- Filter medications to show/hide

### 7. Refill Management
**Route:** `/pets/{petId}/medications/refills`

**Purpose:** Manage medication refills and low supply alerts

**Sections:**

#### Refills Needed (Priority)
- Cards for each medication needing refill
- Priority badges (Critical < 3 days, High < 7 days, Medium < 14 days)
- Current quantity and days remaining
- Refill source information
- Quick order buttons
- Dismiss/Snooze options

#### Refill History
- Table of past refills
- Columns: Date, Medication, Quantity, Source, Cost
- Filter by medication and date range
- Total cost calculations

**Features:**
- One-click order through partnered pharmacies
- Add refill manually
- Set custom refill thresholds
- Notifications preferences for refills

### 8. Adherence Reports
**Route:** `/pets/{petId}/medications/adherence`

**Purpose:** Visualize and analyze medication adherence

**Visualizations:**
- Overall adherence percentage (large display)
- Trend chart over time (line/bar chart)
- Calendar heatmap (green = given, red = missed)
- Per-medication breakdown (table and charts)
- Time-of-day adherence pattern
- Day-of-week adherence pattern

**Filters:**
- Date range selector
- Medication selector
- Comparison mode (multiple medications)

**Export Options:**
- PDF report for veterinarian
- CSV data export
- Email report to self or vet

**Insights:**
- Adherence rate with trend indicator
- Most missed doses (by time, day, medication)
- Longest streak of perfect adherence
- Suggestions for improvement

### 9. Medication History
**Route:** `/pets/{petId}/medications/history`

**Purpose:** Complete record of all medications (active and discontinued)

**Features:**
- Tabbed view: Active | Discontinued | All
- Search and filter
- Sort options
- Expandable rows for details
- Timeline view option

**Information Displayed:**
- Medication name
- Date prescribed and discontinued
- Total duration
- Adherence rate
- Reason for discontinuation
- Prescribing vet
- Total doses given vs scheduled

### 10. Notifications Center
**Route:** `/notifications` or sidebar panel

**Purpose:** Central location for all medication-related notifications

**Notification Types:**
- Upcoming dose reminders
- Missed dose alerts
- Low supply warnings
- Refill confirmations
- Medication interactions (if detected)

**Features:**
- Filter by type and pet
- Mark as read/unread
- Quick actions from notification
- Notification preferences link
- Clear all option

**Settings:**
- Enable/disable notification types
- Reminder timing (15, 30, 60 min before)
- Notification channels (push, email, SMS)
- Quiet hours
- Pet-specific settings

## Components

### Reusable UI Components

#### MedicationCard
Displays summary of a medication with quick actions.

**Props:**
- medication (object)
- showActions (boolean)
- compact (boolean)

**Features:**
- Visual supply indicator
- Next dose countdown
- Status badges
- Quick action menu
- Click to expand details

#### DoseStatusBadge
Shows status of a medication dose.

**Props:**
- status (enum: Pending, Administered, Missed, Skipped)
- size (small, medium, large)

**Variants:**
- ✓ Administered (green)
- ❌ Missed (red)
- ⏰ Pending (yellow)
- ⊘ Skipped (gray)

#### ScheduleTimeline
Timeline view of doses for a day.

**Props:**
- doses (array)
- date (date)
- onDoseClick (function)
- onGiveDose (function)

**Features:**
- Hour markers
- Current time indicator
- Grouped medications
- Interactive dose markers

#### AdherenceChart
Visual representation of adherence.

**Props:**
- data (object)
- chartType (line, bar, calendar, pie)
- period (7, 30, 90 days)

**Features:**
- Responsive design
- Tooltips with details
- Color-coded data
- Export image option

#### SupplyGauge
Visual indicator of medication supply level.

**Props:**
- currentQuantity (number)
- threshold (number)
- maxQuantity (number)

**Features:**
- Color-coded (green > threshold, yellow near, red below)
- Days remaining calculation
- Animated fill
- Warning icon when low

#### FrequencySelector
Custom input for selecting medication frequency.

**Props:**
- value (object)
- onChange (function)

**Options:**
- Preset frequencies (Once daily, Twice daily, etc.)
- Custom interval (every X hours/days)
- Specific times (time pickers)
- Days of week selector
- As needed (PRN) option

#### RefillAlert
Banner or card alerting user to low supply.

**Props:**
- medication (object)
- daysRemaining (number)
- priority (string)
- onDismiss (function)
- onOrder (function)

**Features:**
- Priority color coding
- Countdown display
- Action buttons
- Dismiss/snooze options

## State Management

### Global State (Context/Redux)

#### Medications State
```javascript
{
  byId: {
    [medicationId]: {
      id,
      petId,
      name,
      dosage,
      frequency,
      startDate,
      endDate,
      status,
      supply: { current, threshold },
      schedule: [],
      lastAdministered,
      nextDue,
      adherenceRate
    }
  },
  allIds: [],
  loading: false,
  error: null,
  filters: {
    status: 'active',
    search: '',
    sortBy: 'nextDue'
  }
}
```

#### Schedule State
```javascript
{
  byDate: {
    [date]: [
      {
        id,
        medicationId,
        scheduledTime,
        status,
        actualTime,
        notes
      }
    ]
  },
  todaySchedule: [],
  upcomingCount: 0,
  missedCount: 0,
  loading: false
}
```

#### Notifications State
```javascript
{
  notifications: [
    {
      id,
      type,
      medicationId,
      petId,
      message,
      timestamp,
      read,
      priority
    }
  ],
  unreadCount: 0,
  preferences: {
    pushEnabled: true,
    emailEnabled: true,
    smsEnabled: false,
    reminderMinutes: 30,
    quietHoursStart: '22:00',
    quietHoursEnd: '07:00'
  }
}
```

### Local State (Component Level)
- Form state for add/edit medication
- Modal/drawer open/close states
- Filter and search input values
- Pagination state for tables
- Expanded/collapsed card states

## API Integration

### API Service Layer
Create medication service with methods:
- `getMedications(petId, filters)`
- `getMedicationById(id)`
- `createMedication(data)`
- `updateMedication(id, data)`
- `deleteMedication(id)`
- `discontinueMedication(id, data)`
- `recordAdministration(medicationId, data)`
- `markMissed(medicationId, data)`
- `getSchedule(petId, startDate, endDate)`
- `getTodaySchedule(petId)`
- `getAdherence(petId, period)`
- `getRefillsNeeded(userId)`
- `recordRefill(medicationId, data)`

### Real-time Updates
- WebSocket connection for live notifications
- Server-sent events for schedule updates
- Optimistic UI updates with rollback on error
- Background sync for offline changes

## Responsive Design

### Mobile (< 768px)
- Single column layout
- Bottom navigation bar
- Swipe gestures for actions
- Full-screen modals
- Simplified charts
- Collapsible sections
- Large touch targets (min 44x44px)

### Tablet (768px - 1024px)
- Two column layout where appropriate
- Side drawer for details
- Grid view for medication cards
- Expanded charts
- Floating action button

### Desktop (> 1024px)
- Multi-column layout
- Sidebar navigation
- Modal dialogs for forms
- Full data tables
- Dashboard with widgets
- Keyboard shortcuts

## Accessibility

### WCAG 2.1 AA Compliance
- Semantic HTML structure
- ARIA labels and roles
- Keyboard navigation support
- Focus indicators
- Screen reader optimization
- Alt text for images
- Sufficient color contrast (4.5:1)
- Resizable text up to 200%
- Skip navigation links

### Specific Features
- Time announcements for doses
- Confirmation dialogs for critical actions
- Error messages with clear instructions
- Form field error associations
- Loading state announcements
- Success/failure notifications read aloud

## Performance Optimization

### Loading Strategies
- Lazy load components
- Code splitting by route
- Image lazy loading and optimization
- Pagination for large lists
- Virtual scrolling for long schedules

### Caching
- Cache medication data locally
- Service worker for offline access
- Cache API responses
- Optimistic updates
- Stale-while-revalidate strategy

### Bundle Optimization
- Tree shaking
- Minimize dependencies
- Dynamic imports
- Compress assets
- CDN for static resources

**Target Metrics:**
- Initial load < 3 seconds
- Time to interactive < 5 seconds
- Lighthouse score > 90
- 60 FPS animations

## Error Handling

### User-Facing Errors
- Validation errors inline on forms
- Toast notifications for actions
- Error boundaries for component failures
- Retry mechanisms for failed requests
- Offline mode messaging
- Clear error messages with next steps

### Network Errors
- Retry logic with exponential backoff
- Queue actions when offline
- Sync when connection restored
- Show connection status indicator

### Form Validation
- Real-time field validation
- Submit button disabled until valid
- Clear error messages
- Highlight invalid fields
- Scroll to first error

## Security

### Input Validation
- Sanitize all user inputs
- Client-side validation (UX)
- Server-side validation (security)
- XSS prevention
- SQL injection prevention

### Authentication & Authorization
- JWT token management
- Automatic token refresh
- Logout on token expiration
- Role-based UI rendering
- Secure storage of credentials

### Data Protection
- HTTPS only
- Sensitive data encryption
- No medication details in URLs
- Clear session data on logout
- Comply with HIPAA/privacy regulations

## Internationalization (i18n)

### Language Support
- English (default)
- Spanish
- French
- German
- Extensible for additional languages

### Localization Features
- Translated UI text
- Localized date/time formats
- Unit conversion (metric/imperial)
- Currency formatting
- RTL layout support

## Analytics & Monitoring

### User Analytics
- Page views and routes
- Feature usage tracking
- User flows and funnels
- Medication completion rates
- Error frequency

### Events to Track
- Medication added
- Dose recorded
- Dose missed
- Refill needed alert triggered
- Notification clicked
- Report generated
- Medication discontinued

### Performance Monitoring
- Page load times
- API response times
- Error rates
- Crash reports
- User session recordings (optional, with consent)

## Testing Requirements

### Unit Tests
- Component rendering
- State management logic
- Utility functions
- Form validation
- Data transformations
- Coverage target: > 80%

### Integration Tests
- API service integration
- Form submission flows
- Navigation flows
- State updates
- Notification handling

### E2E Tests
- Critical user journeys:
  - Add medication and record doses
  - View schedule and adherence
  - Manage refills
  - Discontinue medication
- Cross-browser testing
- Mobile device testing

### Accessibility Tests
- Automated a11y testing (axe, Pa11y)
- Screen reader testing
- Keyboard navigation testing
- Color contrast validation

## Browser Support

### Desktop
- Chrome (last 2 versions)
- Firefox (last 2 versions)
- Safari (last 2 versions)
- Edge (last 2 versions)

### Mobile
- iOS Safari (last 2 versions)
- Chrome Mobile (last 2 versions)
- Samsung Internet (latest)

### Progressive Enhancement
- Core functionality without JavaScript
- Enhanced features with JavaScript
- Graceful degradation for older browsers
- Polyfills for missing features
