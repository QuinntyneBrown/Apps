# Medication Management - Frontend Requirements

## Overview
The Medication Management frontend provides an intuitive interface for users to add, view, edit, and manage their medications, set dosing schedules, and monitor drug interactions.

## User Stories

### As a User, I want to:
1. Add a new medication with all relevant details
2. View all my medications in an organized list
3. Edit medication details when prescriptions change
4. Set up dosing schedules for each medication
5. Pause medications temporarily per doctor's orders
6. Discontinue medications when treatment ends
7. Resume paused medications
8. See warnings about drug interactions
9. Search for specific medications quickly
10. View medication history and timeline
11. Categorize medications (prescription, OTC, supplements)
12. Add photos of pills for easy identification
13. Track which medications I'm currently taking
14. See medication details including prescriber and purpose

## Pages/Views

### 1. Medications Dashboard
**Route**: `/medications`

**Purpose**: Main landing page showing overview of all medications

**Components**:
- Medications summary cards (Total meds, Active, Paused, Upcoming refills)
- Quick action buttons (Add Medication, Check Interactions, View Calendar)
- Active medications list with dose times
- Interaction warnings banner
- Medication timeline
- Search and filters

**Features**:
- Filter by status (All, Active, Paused, Discontinued)
- Filter by category (Prescription, OTC, Supplement, Vitamin)
- Filter by form (Tablet, Capsule, Liquid, etc.)
- Sort by medication name, start date, prescriber
- Search by medication name, generic name, prescriber
- Color-coded medication cards by category
- Quick actions (Edit, Pause, Take Dose, View Details)

### 2. Add Medication Form
**Route**: `/medications/new`

**Purpose**: Add a new medication to the system

**Form Sections**:

**Basic Information**:
- Medication Name (required, text input, max 200 chars)
- Generic Name (optional, text input, max 200 chars)
- Dosage (required, text input, e.g., "500mg")
- Form (required, dropdown: Tablet, Capsule, Liquid, Injection, Topical, Inhaler, Patch, Drops, Other)
- Category (required, dropdown: Prescription, OTC, Supplement, Vitamin)

**Prescription Details**:
- Prescriber (optional, text input, max 200 chars)
- Prescription Number (optional, text input, max 100 chars)
- Pharmacy (optional, text input, max 200 chars)

**Schedule & Timing**:
- Start Date (required, date picker)
- End Date (optional, date picker, for limited duration treatments)
- Purpose/Condition (optional, textarea, max 500 chars)
- Special Instructions (optional, textarea, max 1000 chars)

**Appearance & Identification**:
- Pill Color (optional, color selector or text)
- Pill Shape (optional, text input)
- Upload Photo (optional, image upload, max 5MB)

**Additional Details**:
- With Food (toggle, must be taken with food)
- Side Effects to Watch (optional, textarea, max 1000 chars)
- Notes (optional, textarea, max 2000 chars)

**Validation**:
- Real-time field validation
- Display error messages inline
- Highlight invalid fields
- Check for duplicate medications
- Disable submit until all required fields are valid

**Actions**:
- Save button (creates medication and goes to schedule setup)
- Save & Add Another button (creates medication and clears form)
- Cancel button (returns to dashboard with confirmation if form is dirty)

**Success Flow**:
- Show success toast notification
- Prompt to set up dosing schedule
- Run automatic drug interaction check
- Display interaction warnings if found

### 3. Set Dosing Schedule
**Route**: `/medications/{medicationId}/schedule`

**Purpose**: Configure when and how often to take medication

**Form Fields**:
- Frequency (required, dropdown: As Needed, Daily, Every X Days, Weekly, Custom)
- Times of Day (required, time picker, multiple times)
  - Morning (7:00 AM default)
  - Afternoon (12:00 PM default)
  - Evening (6:00 PM default)
  - Night (10:00 PM default)
  - Custom time option
- Days of Week (for weekly frequency, multi-select)
- Days Interval (for "Every X Days" frequency, number input)
- With Food Requirement (toggle)
- Max Doses Per Day (optional, number input, safety limit)
- Minimum Interval Between Doses (optional, hours, decimal)

**Visual Elements**:
- Calendar preview showing scheduled doses
- Timeline visualization of daily dose schedule
- Conflict warnings for overlapping medications
- Recommendation for optimal spacing

**Actions**:
- Save Schedule button
- Skip for Now button (can set up later)
- Back button (return to medication form)

**Success**:
- Show success toast
- Redirect to medication details
- Display next dose reminder time
- Activate reminders

### 4. Edit Medication Form
**Route**: `/medications/{medicationId}/edit`

**Purpose**: Modify existing medication details

**Features**:
- Pre-populate form with existing data
- Same validation as Add Medication form
- Show medication history timeline
- Display audit trail (created, last updated)
- Warning if schedule will be affected by changes

**Additional Actions**:
- Pause Medication button (opens pause dialog)
- Discontinue Medication button (opens discontinue dialog)
- Delete button (opens confirmation dialog)

### 5. Medication Details View
**Route**: `/medications/{medicationId}`

**Purpose**: View complete medication information and history

**Sections**:

**Medication Information Card**:
- Name, dosage, form
- Prescriber, prescription number
- Start date, end date
- Status badge (Active, Paused, Discontinued)
- Pill photo if available
- Purpose and instructions
- Color and shape for identification

**Dosing Schedule**:
- Frequency description
- Times of day
- Visual schedule calendar
- Next dose time countdown
- Edit Schedule button

**Adherence Summary**:
- Current adherence rate for this medication
- Doses taken this week/month
- Missed doses count
- Adherence trend graph
- Perfect adherence streak

**Refill Information**:
- Doses remaining
- Days of supply left
- Next refill date
- Refill history
- Order Refill button

**Side Effects Reported**:
- List of side effects logged
- Severity and frequency
- Recent side effect alerts
- Report Side Effect button

**Interaction Warnings**:
- Drug interactions affecting this medication
- Severity indicators
- Clinical significance
- Recommendations

**Activity Log**:
- All changes to this medication
- Doses taken/missed
- Pauses and resumes
- Schedule changes
- User and timestamp for each event

**Actions**:
- Edit Medication button
- Edit Schedule button
- Pause/Resume toggle
- Discontinue button
- Delete button
- Take Dose Now button
- Order Refill button
- Share with Doctor button
- Print/Export button

### 6. Pause Medication Dialog
**Component**: Modal dialog

**Purpose**: Temporarily suspend medication

**Fields**:
- Pause Reason (required, dropdown or text)
  - Doctor's orders
  - Side effects
  - Illness/infection
  - Surgery preparation
  - Other (specify)
- Expected Resume Date (optional, date picker)
- Notes (optional, textarea)

**Actions**:
- Confirm Pause button
- Cancel button

**Effects**:
- Medication marked as paused
- Reminders suspended
- Adherence calculations adjusted
- Resume reminder scheduled if date provided

### 7. Discontinue Medication Dialog
**Component**: Modal dialog

**Purpose**: Stop taking medication permanently

**Fields**:
- Discontinuation Date (required, date picker, defaults to today)
- Discontinuation Reason (required, dropdown or text)
  - Treatment completed
  - Prescription expired
  - Switched to alternative
  - Side effects too severe
  - No longer needed
  - Doctor's orders
  - Other (specify)
- Duration Taken (calculated automatically)
- Final Notes (optional, textarea)

**Confirmation**:
- Warning that all reminders will be cancelled
- Confirmation checkbox: "I understand this medication will be discontinued"

**Actions**:
- Confirm Discontinue button
- Cancel button

**Effects**:
- Medication marked as discontinued
- All reminders cancelled
- Moved to discontinued medications list
- Historical data preserved

### 8. Drug Interactions View
**Route**: `/medications/interactions`

**Purpose**: View all drug interactions for user's medications

**Features**:
- Interaction warnings grouped by severity:
  - Critical (red): Dangerous, avoid combination
  - Moderate (yellow): Caution advised, monitor closely
  - Minor (blue): Minor interaction, generally safe
- Each interaction shows:
  - Medications involved
  - Interaction type
  - Clinical significance
  - Recommendations
  - What to watch for
- Filter by severity
- Sort by severity or medication name
- Print/Export for doctor
- Share with Healthcare Provider button

**Actions**:
- View Medication Details (for each involved medication)
- Contact Doctor button
- Dismiss warning (with confirmation)
- Mark as Reviewed

### 9. Medication List View
**Route**: `/medications/list`

**Purpose**: Detailed table view with advanced features

**Table Columns**:
- Medication Name
- Dosage
- Form
- Schedule (times/day)
- Next Dose
- Status
- Adherence Rate
- Actions (view, edit, pause, discontinue)

**Features**:
- Column sorting
- Column visibility toggle
- Advanced filters panel
- Multi-select for bulk actions
- Row expansion for quick details
- Inline status updates
- Pagination (25, 50, 100 items per page)
- Export to CSV/PDF

**Bulk Actions**:
- Check selected for interactions
- Export selected to PDF
- Print medication list
- Share with provider

### 10. Medication Calendar View
**Route**: `/medications/calendar`

**Purpose**: Visualize medication schedule on calendar

**Features**:
- Monthly calendar view
- Each medication shown at scheduled times
- Color-coded by medication or category
- Dose status indicators (taken, missed, upcoming)
- Click dose to mark as taken
- Hover for medication details
- Filter by medication
- Legend showing color codes
- Month/Year navigation
- Day view for detailed daily schedule

### 11. Medication Timeline
**Component**: Reusable widget

**Purpose**: Visual history of medications over time

**Features**:
- Horizontal timeline
- Medications shown as bars with start/end dates
- Active medications highlighted
- Paused medications shown with pause icon
- Discontinued medications grayed out
- Hover for details
- Click to view medication
- Zoom and scroll controls
- Filter by date range

## UI Components

### MedicationCard
- Displays medication summary
- Shows name, dosage, next dose time
- Status badge (Active, Paused, Discontinued)
- Category color coding
- Pill image thumbnail
- Quick action buttons (Take Dose, Edit, View)
- Interaction warning indicator
- Responsive design

### MedicationForm
- Reusable form for add/edit
- Built-in validation with error messages
- Auto-save draft (localStorage)
- Field help text and tooltips
- Required field indicators
- Progressive disclosure (advanced options)

### ScheduleVisualizer
- Visual representation of dosing schedule
- 24-hour timeline
- Dose time markers
- With food indicators
- Conflict warnings
- Interactive editing

### InteractionWarning
- Alert component for drug interactions
- Severity color coding (red, yellow, blue)
- Expandable details
- Action buttons
- Dismiss functionality

### AdherenceIndicator
- Visual adherence percentage
- Color-coded (green >90%, yellow 70-90%, red <70%)
- Trend arrow (improving/declining)
- Tooltip with details

### StatusBadge
- Medication status indicator
- Color-coded (green: Active, yellow: Paused, gray: Discontinued)
- Icon representation
- Tooltip with status details

### DoseTimeChip
- Displays scheduled dose time
- Morning/Afternoon/Evening/Night label
- Actual time
- With food indicator icon
- Removable for editing

## User Interactions

### Add Medication Flow
1. User clicks "Add Medication" button
2. Form appears with basic information section
3. User fills in required fields (name, dosage, form, category, start date)
4. Real-time validation provides feedback
5. User completes optional fields
6. User uploads pill photo (optional)
7. User clicks "Save"
8. API call creates medication
9. Automatic drug interaction check runs
10. If interactions found, warning displayed
11. User prompted to set up dosing schedule
12. User configures schedule (or skips)
13. Success notification appears
14. Redirected to medication details
15. First reminder scheduled

### Edit Medication Flow
1. User clicks "Edit" on medication card
2. Edit form opens with pre-filled data
3. User modifies fields
4. Real-time validation provides feedback
5. Warning shown if schedule affected
6. User clicks "Save"
7. API call updates medication
8. Interaction check runs if medications changed
9. Success notification appears
10. Updated information displayed

### Pause Medication Flow
1. User clicks "Pause" button
2. Pause dialog appears
3. User selects pause reason
4. User optionally sets expected resume date
5. User clicks "Confirm Pause"
6. API call pauses medication
7. Success notification appears
8. Medication marked as paused
9. Reminders suspended
10. Resume reminder scheduled if date set

### Discontinue Medication Flow
1. User clicks "Discontinue" button
2. Discontinue dialog appears with warning
3. User selects discontinuation reason
4. User enters discontinuation date
5. Duration taken calculated and shown
6. User checks confirmation checkbox
7. User clicks "Confirm Discontinue"
8. API call discontinues medication
9. Success notification appears
10. All reminders cancelled
11. Medication moved to discontinued list
12. Historical data preserved

### Set Schedule Flow
1. User accesses schedule setup (from add or edit)
2. User selects frequency
3. User adds times of day (multiple)
4. Visual timeline shows schedule
5. User sets with food requirement
6. User sets max doses (optional)
7. Calendar preview shows upcoming doses
8. Conflict warnings displayed if overlap
9. User clicks "Save Schedule"
10. API call creates schedule
11. Reminders activated
12. Success notification appears
13. Next dose countdown displayed

## State Management

### Medication State
```typescript
interface MedicationState {
  medications: Medication[];
  selectedMedication: Medication | null;
  interactions: DrugInteraction[];
  loading: boolean;
  error: string | null;
  filters: MedicationFilters;
  sortConfig: SortConfig;
}
```

### Medication Model
```typescript
interface Medication {
  medicationId: string;
  userId: string;
  medicationName: string;
  genericName?: string;
  dosage: string;
  form: MedicationForm;
  prescriber?: string;
  prescriptionNumber?: string;
  startDate: Date;
  endDate?: Date;
  isActive: boolean;
  isPaused: boolean;
  pauseReason?: string;
  pauseStartDate?: Date;
  expectedResumeDate?: Date;
  discontinuedDate?: Date;
  discontinuationReason?: string;
  purpose?: string;
  instructions?: string;
  withFood: boolean;
  category: MedicationCategory;
  color?: string;
  shape?: string;
  imageUrl?: string;
  notes?: string;
  schedule?: MedicationSchedule;
  createdAt: Date;
  updatedAt: Date;
}

interface MedicationSchedule {
  scheduleId: string;
  medicationId: string;
  frequency: ScheduleFrequency;
  timesOfDay: string[]; // TimeSpan strings
  daysOfWeek?: number[]; // DayOfWeek enum values
  withFoodRequirement: boolean;
  maxDosesPerDay?: number;
  minimumInterval?: number; // Hours
}

enum MedicationForm {
  Tablet = 'Tablet',
  Capsule = 'Capsule',
  Liquid = 'Liquid',
  Injection = 'Injection',
  Topical = 'Topical',
  Inhaler = 'Inhaler',
  Patch = 'Patch',
  Drops = 'Drops',
  Other = 'Other'
}

enum MedicationCategory {
  Prescription = 'Prescription',
  OTC = 'OTC',
  Supplement = 'Supplement',
  Vitamin = 'Vitamin'
}

enum ScheduleFrequency {
  AsNeeded = 'AsNeeded',
  Daily = 'Daily',
  EveryXDays = 'EveryXDays',
  Weekly = 'Weekly',
  Custom = 'Custom'
}

interface DrugInteraction {
  medicationIds: string[];
  medicationNames: string[];
  interactionType: string;
  severity: 'Critical' | 'Moderate' | 'Minor';
  clinicalSignificance: string;
  recommendations: string;
}
```

### Actions
- `loadMedications()`: Fetch all medications for user
- `loadMedication(medicationId)`: Fetch single medication
- `createMedication(medication)`: Create new medication
- `updateMedication(medicationId, updates)`: Update medication
- `pauseMedication(medicationId, reason, resumeDate)`: Pause medication
- `resumeMedication(medicationId)`: Resume paused medication
- `discontinueMedication(medicationId, reason)`: Discontinue medication
- `deleteMedication(medicationId)`: Delete medication
- `setSchedule(medicationId, schedule)`: Set dosing schedule
- `checkInteractions()`: Check for drug interactions
- `searchMedications(query)`: Search medications
- `filterMedications(filters)`: Apply filters

## API Integration

### Service Methods
```typescript
class MedicationService {
  async getMedications(params?: QueryParams): Promise<Medication[]>;
  async getMedication(medicationId: string): Promise<Medication>;
  async createMedication(medication: CreateMedicationRequest): Promise<Medication>;
  async updateMedication(medicationId: string, updates: UpdateMedicationRequest): Promise<Medication>;
  async pauseMedication(medicationId: string, request: PauseMedicationRequest): Promise<void>;
  async resumeMedication(medicationId: string): Promise<void>;
  async discontinueMedication(medicationId: string, request: DiscontinueMedicationRequest): Promise<void>;
  async deleteMedication(medicationId: string): Promise<void>;
  async setSchedule(medicationId: string, schedule: MedicationSchedule): Promise<MedicationSchedule>;
  async checkInteractions(): Promise<DrugInteraction[]>;
  async searchMedications(query: string): Promise<Medication[]>;
  async uploadPhoto(medicationId: string, photo: File): Promise<string>; // Returns imageUrl
}
```

### Error Handling
- Network errors: Show retry button with offline indicator
- Validation errors: Display field-level errors inline
- Authorization errors: Redirect to login
- Not found errors: Show "Medication not found" message
- Conflict errors: Show appropriate message (e.g., "Already paused")
- Server errors: Show friendly error message with support contact

## Responsive Design

### Desktop (>1024px)
- Multi-column layout
- Side-by-side medication list and details
- Expanded forms (3 columns)
- Full calendar view
- Interaction panel always visible

### Tablet (768px - 1024px)
- Two-column layout
- Stacked medication list
- Two-column forms
- Compact calendar view
- Collapsible interaction panel

### Mobile (<768px)
- Single column layout
- Card-based medication list
- One-column forms
- Accordion sections for long forms
- Bottom sheet for interactions
- Swipe actions on medication cards
- Mobile-optimized time picker
- Sticky action buttons

## Accessibility

- ARIA labels on all interactive elements
- Keyboard navigation support (Tab, Enter, Esc)
- Focus indicators clearly visible
- Screen reader announcements for status changes
- High contrast mode support
- Font size adjustments (user preference)
- Color-blind friendly palette (not relying on color alone)
- Alt text for pill images
- Form labels properly associated with inputs

## Performance Optimization

- Lazy load medication list (virtual scrolling for 100+ meds)
- Image lazy loading and compression
- Debounce search input (300ms)
- Cache medication data (5 minutes)
- Optimize re-renders with React.memo/useMemo
- Code splitting by route
- Minimize bundle size (tree shaking)
- Service worker for offline support
- Prefetch next likely actions

## Notifications

### Success Notifications
- "Medication added successfully"
- "Schedule saved successfully"
- "Medication paused"
- "Medication discontinued"
- "Medication resumed"
- "Medication updated"

### Warning Notifications
- "Drug interaction detected! Review interactions."
- "This medication is paused. Resume to receive reminders."
- "Deleting will remove all history for this medication."
- "Changes to schedule will affect existing reminders."

### Error Notifications
- "Failed to save medication. Please try again."
- "Unable to check drug interactions. Proceeding without check."
- "Failed to pause medication. Please try again."

### Info Notifications
- "Set up a dosing schedule to receive reminders"
- "No active medications found"
- "Your medication schedule has been updated"

## Analytics Events

- `medication_added`: Track medication creation
- `medication_updated`: Track modifications
- `medication_paused`: Track pause events
- `medication_discontinued`: Track discontinuation
- `medication_deleted`: Track deletions
- `schedule_set`: Track schedule creation
- `interaction_checked`: Track interaction checks
- `interaction_warning_shown`: Track interaction warnings
- `photo_uploaded`: Track photo uploads

## Testing Requirements

### Unit Tests
- Component rendering
- Form validation logic
- State management reducers
- Utility functions (date calculations, etc.)
- Interaction severity classification

### Integration Tests
- API integration with mock backend
- User flows (add, edit, pause, discontinue)
- Schedule setup and validation
- Interaction checking

### E2E Tests
- Complete add medication flow
- Complete edit medication flow
- Pause and resume flow
- Discontinue medication flow
- Schedule setup flow
- Drug interaction warning flow

### Accessibility Tests
- Screen reader compatibility
- Keyboard navigation
- Color contrast ratios
- ARIA labels and roles
- Focus management
