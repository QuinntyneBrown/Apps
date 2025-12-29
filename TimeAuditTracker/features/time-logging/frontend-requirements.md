# Frontend Requirements - Time Logging

## User Interface Components

### Quick Time Entry Form
**Purpose:** Allow rapid logging of time entries
**Components:**
- Activity type text input with autocomplete
- Start time picker (date + time)
- End time picker (date + time)
- Duration display (auto-calculated)
- Category dropdown with icons
- Location input (optional)
- Energy level slider (1-10)
- Submit button

**Behavior:**
- Auto-calculate duration as times are entered
- Highlight overlaps with existing entries
- Save draft entries locally before submission
- Support keyboard shortcuts (Ctrl+Enter to submit)

### Timeline View
**Purpose:** Visualize time entries chronologically
**Components:**
- Scrollable timeline with hourly markers
- Color-coded activity blocks
- Gaps indicator for unlogged time
- Day/week/month view toggles
- Zoom controls

**Behavior:**
- Show entries as proportional blocks
- Enable drag-to-adjust entry times
- Click to edit entry details
- Highlight automatic vs manual entries with icons

### Bulk Entry Mode
**Purpose:** Log an entire day or period retrospectively
**Components:**
- Date range selector
- Multi-row entry grid
- Quick templates for common activities
- Batch save button
- Progress indicator

**Behavior:**
- Pre-fill common activities
- Validate total time equals 24 hours (for full day)
- Show completeness percentage
- Enable copy from previous day

### Edit Time Entry Modal
**Purpose:** Modify existing time entries
**Components:**
- All fields from quick entry form
- Adjustment reason text area
- Original values display
- Save/Cancel buttons
- Delete button

**Behavior:**
- Show before/after comparison
- Require reason for significant changes
- Confirm before deletion
- Update timeline immediately

## User Flows

### Flow 1: Log New Time Entry
1. User clicks "Add Entry" button
2. Quick entry form appears
3. User fills activity type, times, category
4. Duration auto-calculates
5. User adjusts energy level
6. User clicks "Save"
7. System validates and saves
8. Timeline updates with new entry
9. Success notification appears

### Flow 2: Edit Existing Entry
1. User clicks entry in timeline
2. Edit modal opens with current values
3. User modifies fields
4. User enters adjustment reason
5. User clicks "Save Changes"
6. System records adjustment
7. Timeline updates
8. Confirmation message shown

### Flow 3: Bulk Log Day
1. User selects "Bulk Entry" mode
2. Date picker opens
3. User selects date to log
4. Grid appears with time slots
5. User fills activities for each slot
6. System shows completeness (e.g., 18/24 hours)
7. User clicks "Save All"
8. System processes batch
9. Summary confirmation shown

### Flow 4: Review Automatic Entries
1. System detects activity automatically
2. Entry appears in "Pending Review" queue
3. User opens review panel
4. User sees detected activity with confidence
5. User confirms or adjusts categorization
6. User clicks "Approve" or "Edit & Approve"
7. Entry moves to main timeline

## UI States

### Loading States
- Timeline loading: Show skeleton blocks
- Form submission: Disable form, show spinner
- Bulk save: Show progress bar with count

### Error States
- Overlap detected: Show warning banner with details
- Validation failed: Highlight invalid fields in red
- Save failed: Show retry button with error message

### Empty States
- No entries: Show welcome message with "Log First Entry" CTA
- Unlogged time: Show friendly reminder with quick-fill option

## Responsive Design
- Mobile: Stack form fields vertically, use native time pickers
- Tablet: Two-column form layout, condensed timeline
- Desktop: Full timeline view with sidebar form

## Accessibility
- Keyboard navigation for all forms
- ARIA labels for all inputs
- Screen reader announcements for timeline updates
- High contrast mode support
- Focus indicators for keyboard users

## Performance
- Lazy load timeline entries (load 7 days at a time)
- Debounce autocomplete searches (300ms)
- Optimistic UI updates for immediate feedback
- Cache recent entries in localStorage

## Validation Rules
- Start time must be before end time
- Duration must be > 0
- Energy level must be 1-10
- Category is required
- Cannot create entries in future (except scheduled)
