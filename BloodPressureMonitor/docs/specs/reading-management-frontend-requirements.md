# Frontend Requirements - Reading Management

## Overview
The Reading Management frontend provides users with intuitive interfaces to record, view, edit, and manage their blood pressure readings.

## User Interface Components

### 1. Quick Reading Entry Form

**Location:** Dashboard, accessible via prominent "Record Reading" button

**Fields:**
- Systolic (number input, mmHg)
  - Placeholder: "e.g., 120"
  - Validation: 50-250, required
  - Real-time validation feedback
- Diastolic (number input, mmHg)
  - Placeholder: "e.g., 80"
  - Validation: 30-150, required
  - Real-time validation feedback
- Pulse (number input, bpm)
  - Placeholder: "e.g., 72"
  - Validation: 30-250, optional
- Measurement Time (datetime picker)
  - Default: current time
  - Cannot be future date
- Arm Used (dropdown)
  - Options: Left, Right
  - Default: user's last selection or "Left"

**Advanced Options (Expandable):**
- Context (dropdown)
  - Options: Resting, Post-exercise, Stressed, Medication Check, Other
  - Default: Resting
- Posture (dropdown)
  - Options: Sitting, Standing, Lying Down
- Time Since Activity (number input, minutes)
- Protocol Checklist (expandable)
  - Checkboxes: Rested 5+ minutes, Arm supported, Back supported, Feet flat, No talking
  - "I followed proper measurement protocol" summary checkbox

**Actions:**
- Save Reading (primary button)
- Cancel (secondary button)
- Save & Add Another (tertiary button)

**Behavior:**
- Auto-focus on systolic field when form opens
- Tab order: Systolic → Diastolic → Pulse → Measurement Time
- Show success message with reading summary after save
- Clear form after successful save (if Save & Add Another)
- Show validation errors inline below each field
- Disable Save button until required fields valid

**Validation Feedback:**
- Green checkmark for valid values
- Red error message for invalid values
- Warning icon if value is unusual but technically valid
- Immediate validation on blur, re-validate on change

### 2. Multiple Readings Session

**Location:** Accessible from Quick Entry Form ("Record Multiple Readings" link)

**Layout:**
- Header: "Multiple Readings Session"
- Subtext: "Take 2-3 readings, 1-2 minutes apart for best accuracy"
- Reading entry cards (stacked vertically)
  - Reading 1, Reading 2, Reading 3 (max 5)
  - Each card has Systolic, Diastolic, Pulse fields
  - Add Reading button (up to 5 readings)
  - Remove Reading button (X icon on each card)
- Shared fields (apply to all readings):
  - Measurement Time
  - Context
  - Arm Used
- Calculated Average Display (updates live):
  - Average Systolic / Diastolic
  - Pulse average
  - Variance indicators (green if low, yellow if moderate, red if high)
- Save Session button

**Behavior:**
- Start with 2 reading cards visible
- Calculate average automatically as values entered
- Show variance warnings if readings differ significantly
- Save all individual readings plus averaged result

### 3. Reading History List

**Location:** Main readings page

**Display Format:**
- Card-based list (responsive grid on larger screens)
- Sort options: Newest First (default), Oldest First
- Filter options:
  - Date range picker
  - Context filter (checkboxes: Resting, Post-exercise, etc.)
  - Value range filters (Systolic, Diastolic ranges)

**Each Reading Card Shows:**
- Primary info (large):
  - Systolic / Diastolic (e.g., "120/80")
  - Color-coded by BP category (normal=green, elevated=yellow, high=orange/red)
- Secondary info (smaller):
  - Pulse (if recorded): "♥ 72 bpm"
  - Date and time (formatted: "Dec 29, 2025 8:30 AM")
  - Context icon and label
  - Arm used icon (L/R)
- Actions (on hover or touch):
  - View Details (eye icon)
  - Edit (pencil icon) - only if within 24 hours
  - Delete (trash icon)
  - Add to Report (plus icon)

**Pagination:**
- Show 20 readings per page
- Load more button or infinite scroll
- "Showing X-Y of Z readings"

**Empty State:**
- Friendly illustration
- "No readings yet"
- "Record your first blood pressure reading to start tracking"
- Prominent "Record Reading" button

### 4. Reading Detail View

**Location:** Modal or separate page accessed from reading card

**Display Sections:**

**Main Reading Info:**
- Large BP display: "120 / 80 mmHg"
- BP category badge (Normal, Elevated, High Stage 1, etc.)
- Pulse display: "72 bpm" with rhythm indicator
- Date and time

**Measurement Details:**
- Arm used
- Posture
- Context
- Time since activity
- Protocol followed indicator (checkmark if yes)

**Quality Indicators:**
- Measurement quality score (if calculated)
- Variance from recent average
- Any warnings or flags

**Context Timeline:**
- Recent readings before/after this one (mini chart)
- Medication timing (if tracked)
- Lifestyle factors logged around same time

**Actions:**
- Edit Reading (if within 24 hours)
- Delete Reading
- Add Note
- Share Reading
- Close/Back

### 5. Device Sync Interface

**Location:** Settings or Main Menu

**Display:**
- Connected Devices list
  - Device name and model
  - Last sync time
  - Battery level (if available)
  - Connection status indicator
- Add Device button (opens pairing flow)

**Sync Flow:**
1. Select device or "Sync All"
2. Show syncing progress (spinner + "Syncing X readings...")
3. Show results:
   - "Imported X new readings"
   - "Skipped Y duplicates"
   - List of imported readings (expandable)
4. Option to view imported readings

**Error Handling:**
- Clear error messages if sync fails
- Retry button
- "View Sync Log" for troubleshooting

## User Flows

### Flow 1: Quick Reading Entry
1. User clicks "Record Reading" button
2. Quick entry form appears (modal or inline)
3. User enters systolic, diastolic, pulse
4. User optionally expands advanced options
5. User clicks "Save Reading"
6. System validates input
7. System saves reading
8. Success message appears with reading summary
9. Form closes or clears for next entry

### Flow 2: Multiple Readings Session
1. User clicks "Record Multiple Readings"
2. Session interface appears
3. User takes first reading, enters values
4. User waits 1-2 minutes
5. User takes second reading, enters values
6. System shows calculated average
7. User optionally takes third reading
8. User clicks "Save Session"
9. System saves all readings and average
10. Success message shows averaged result

### Flow 3: View and Edit Reading
1. User views reading history
2. User clicks on a reading card
3. Detail view opens
4. User clicks "Edit" (if within 24 hours)
5. Form appears with pre-filled values
6. User modifies values
7. User clicks "Save Changes"
8. System validates and updates
9. Success message appears
10. Detail view refreshes with updated data

### Flow 4: Device Sync
1. User navigates to device sync
2. User selects device or "Sync All"
3. System connects to device
4. System retrieves readings
5. System checks for duplicates
6. System imports new readings
7. Progress indicator shows status
8. Results summary appears
9. User can view imported readings

## Responsive Design

### Mobile (< 768px)
- Full-width entry form
- Stacked form fields (one per row)
- Touch-friendly buttons (min 44px height)
- Simplified reading cards (vertical layout)
- Bottom sheet for detail views
- Sticky "Record Reading" FAB (floating action button)

### Tablet (768px - 1024px)
- Two-column layout for reading history
- Side panel for detail views
- Larger touch targets
- More reading cards visible

### Desktop (> 1024px)
- Three-column layout for reading history
- Modal for entry forms
- Hover states on interactive elements
- Keyboard shortcuts (e.g., Ctrl+N for new reading)
- More data visible per screen

## Accessibility

- ARIA labels on all form fields
- Keyboard navigation support (Tab, Enter, Esc)
- Focus indicators visible
- Error messages announced to screen readers
- Color not the only indicator (use icons + text)
- High contrast mode support
- Font size adjustable
- Skip to main content link

## Data Visualization

### Reading Trend Chart (on history page)
- Line chart showing BP over time
- Dual Y-axis (systolic and diastolic)
- Pulse overlay (optional toggle)
- Shaded regions for BP categories (normal, elevated, high)
- Interactive tooltips on data points
- Zoom and pan controls
- Time range selector (7 days, 30 days, 90 days, All)

### Quick Stats Cards
- Average BP (current period)
- Readings this week
- Percentage in goal range
- Current streak (consecutive days with readings)

## Notifications and Feedback

### Success Messages
- "Reading recorded successfully"
- "Session saved with average: 120/80 mmHg"
- "Reading updated"
- "Reading deleted"

### Warning Messages
- "This reading is significantly different from your recent average. Consider retaking."
- "You haven't taken a reading today. Would you like to record one now?"

### Error Messages
- "Systolic must be greater than diastolic"
- "Please enter a value between 50 and 250"
- "This reading cannot be edited (more than 24 hours old)"
- "Sync failed. Please check your device connection."

### Loading States
- Skeleton screens for reading history
- Spinner for save operations
- Progress bar for device sync

## Offline Support

- Allow reading entry when offline
- Queue readings for sync when connection restored
- Visual indicator of offline status
- Cached recent readings viewable offline
- Sync pending indicator on queued readings

## State Management

### Local State (Component-level)
- Form input values
- Validation states
- UI toggles (expanded/collapsed sections)

### Global State (App-level)
- Current user readings list
- Selected reading for detail view
- Sync status
- Device connection status
- Filter and sort preferences

### Persistent State (Local Storage)
- Last used form defaults (arm, context)
- User preferences (units, date format)
- Offline reading queue

## Performance Optimization

- Virtualized scrolling for long reading lists (render only visible items)
- Lazy load reading details
- Debounce search/filter inputs
- Cache API responses (5 minutes)
- Optimistic UI updates (show success immediately, rollback on error)
- Image optimization for device icons
- Code splitting by route

## Validation Rules

### Client-Side Validation
- Required fields present
- Numeric ranges valid
- Systolic > Diastolic
- Date not in future
- All rules from backend requirements

### Real-Time Validation
- Validate on blur
- Re-validate on change after first validation
- Show errors immediately
- Show success indicators for valid input

## UI Components Library

### Reusable Components
- `BPInput` - Specialized number input for BP values
- `ReadingCard` - Display reading summary
- `BPChart` - Blood pressure trend visualization
- `BPCategoryBadge` - Color-coded category indicator
- `ReadingForm` - Reusable form for entry/edit
- `DateRangePicker` - Date range selection
- `DeviceSyncStatus` - Device connection indicator

### Design System
- Primary color: Health blue (#2196F3)
- Success: Green (#4CAF50)
- Warning: Orange (#FF9800)
- Danger: Red (#F44336)
- Normal BP: Green
- Elevated BP: Yellow
- High BP: Orange/Red
- Typography: System font stack for readability
- Spacing: 8px base unit
- Border radius: 8px for cards, 4px for inputs

## Analytics Events to Track

- `reading_recorded` - User records a reading
- `reading_edited` - User edits a reading
- `reading_deleted` - User deletes a reading
- `multiple_readings_session` - User completes multi-reading session
- `device_sync_initiated` - User starts device sync
- `device_sync_completed` - Sync completes successfully
- `reading_detail_viewed` - User views reading details
- `chart_interacted` - User interacts with trend chart
- `form_abandoned` - User starts but doesn't complete entry
