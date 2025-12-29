# Applications - Frontend Requirements

## Overview
The Applications UI provides a comprehensive interface for job seekers to manage their job applications from start to finish, tracking status changes and maintaining detailed records.

## Pages/Views

### 1. Applications Dashboard
**Route:** `/applications`

**Purpose:** Main hub for viewing and managing all job applications.

**Components:**
- Header with search and filters
- Status pipeline view (Kanban-style)
- List/grid view toggle
- Application cards
- Statistics summary

**Features:**
- View applications in Kanban board by status
- Filter by status, date range, company
- Sort by submission date, status, company
- Quick status updates via drag-and-drop
- View application timeline
- Export applications to CSV/PDF

### 2. Start Application Flow
**Route:** `/applications/new`

**Purpose:** Begin a new job application from a saved listing.

**Form Fields:**
- Job Listing Selection* (dropdown/search)
- Target Submission Date (date picker)
- Initial Notes (textarea)

**Actions:**
- Start Application (creates draft)
- Cancel

### 3. Submit Application Form
**Route:** `/applications/{id}/submit`

**Purpose:** Record application submission details.

**Form Fields:**
- Submission Method* (dropdown: Online Portal, Email, In Person, Recruiter, Referral)
- Confirmation Number (text input)
- Documents Submitted (multi-select checklist):
  - Resume
  - Cover Letter
  - Portfolio
  - References
  - Transcripts
  - Other (custom)
- Cover Letter Used (checkbox)
- Submission Notes (textarea)
- Submitted Date* (date-time picker, defaults to now)

**Actions:**
- Submit Application (marks as submitted)
- Save as Draft
- Cancel

### 4. Application Detail View
**Route:** `/applications/{id}`

**Purpose:** View and manage complete application details.

**Sections:**
- Header (job title, company, status badge, quick actions)
- Job Details Panel (linked job listing info)
- Application Timeline (visual status progression)
- Status History (detailed log of all updates)
- Documents Submitted
- Notes and Next Steps
- Related Items (interviews, offers)

**Actions:**
- Update Status
- Add Note
- Withdraw Application
- Mark as Rejected
- Delete Application (draft only)
- View Job Listing
- Schedule Interview
- Print/Export

### 5. Update Status Dialog
**Purpose:** Record a status change for the application.

**Fields:**
- New Status* (dropdown of valid next statuses)
- Updated By (company contact name)
- Status Notes (textarea)
- Next Steps (textarea)
- Date of Update (date-time picker, defaults to now)

**Actions:**
- Update Status
- Cancel

### 6. Application Pipeline View
**Purpose:** Kanban-style visualization of application progress.

**Columns:**
- Draft
- Submitted
- Under Review
- Phone Screen
- Technical Assessment
- Interviewing
- Background Check
- Offer Extended
- Rejected
- Withdrawn

**Features:**
- Drag and drop cards between columns
- Count badges on each column
- Filter and search across all columns
- Collapse/expand columns

## Components

### ApplicationCard
**Purpose:** Display application summary in grid/Kanban view.

**Props:**
- `application` - Application data object
- `viewMode` - "grid", "list", or "kanban"
- `onStatusChange` - Callback for status updates
- `onWithdraw` - Callback for withdrawal
- `draggable` - Enable drag-and-drop

**Display:**
- Job title and company
- Status badge (color-coded)
- Submission date
- Days since submission
- Current stage indicator
- Quick action buttons

**States:**
- Default
- Hover (show actions)
- Dragging
- Rejected (grayed out)
- Withdrawn (grayed out)

### StatusPipeline
**Purpose:** Visual timeline of application status.

**Display:**
- Horizontal or vertical timeline
- Completed steps (filled circles)
- Current step (highlighted)
- Future steps (empty circles)
- Date stamps for each completed step

### StatusBadge
**Purpose:** Visual indicator of application status.

**Statuses:**
- Draft (gray)
- Submitted (blue)
- Under Review (purple)
- Phone Screen (cyan)
- Technical Assessment (orange)
- Interviewing (yellow)
- Background Check (pink)
- Offer Extended (green)
- Rejected (red)
- Withdrawn (dark gray)

### ApplicationTimeline
**Purpose:** Chronological view of all status changes.

**Display:**
- Vertical timeline with dates
- Status change events
- Notes and updates
- Contact names
- Next steps

### DocumentsChecklist
**Purpose:** Track submitted documents.

**Display:**
- Checkbox list of standard documents
- Custom documents (user-added)
- Submit dates for each document
- File attachments (optional)

### WithdrawApplicationDialog
**Purpose:** Confirm and record application withdrawal.

**Fields:**
- Reason* (dropdown: Accepted another offer, Not interested, etc.)
- Notes (textarea)
- Withdrawal Date (date picker, defaults to now)

**Actions:**
- Confirm Withdrawal
- Cancel

### RejectApplicationDialog
**Purpose:** Record application rejection details.

**Fields:**
- Rejection Reason (dropdown)
- Company Feedback (textarea)
- Eligible for Reapplication (checkbox)
- Earliest Reapplication Date (date picker, if eligible)
- Notes (textarea)
- Rejection Date (date picker, defaults to now)

**Actions:**
- Confirm Rejection
- Cancel

### ApplicationStats
**Purpose:** Display application statistics and metrics.

**Metrics:**
- Total applications
- By status (bar chart)
- Success rate (percentage)
- Average time to response
- Response rate
- Most common rejection reasons
- Applications by month (trend chart)

### BulkActionsBar
**Purpose:** Perform actions on multiple applications.

**Actions:**
- Update status (batch)
- Export selected
- Delete selected (draft only)

## State Management

### Redux/State Slices

**applicationsSlice:**
```typescript
{
  applications: Application[],
  selectedApplicationId: string | null,
  filters: {
    status: string[],
    dateFrom: Date | null,
    dateTo: Date | null,
    companies: string[],
    search: string
  },
  sorting: {
    field: 'submittedAt' | 'startedAt' | 'company' | 'status',
    direction: 'asc' | 'desc'
  },
  viewMode: 'grid' | 'list' | 'kanban',
  selectedIds: string[],
  loading: boolean,
  error: string | null,
  stats: ApplicationStats
}
```

**Actions:**
- `fetchApplications` - Load applications with filters
- `fetchApplicationById` - Load single application
- `startApplication` - Create new application
- `submitApplication` - Submit application
- `updateApplicationStatus` - Update status
- `rejectApplication` - Mark as rejected
- `withdrawApplication` - Withdraw application
- `deleteApplication` - Delete draft application
- `setFilters` - Update filter criteria
- `setViewMode` - Toggle view mode
- `fetchStats` - Load statistics

## Routing

```
/applications                          - Dashboard (Kanban or list)
/applications/new                      - Start new application
/applications/:id                      - View/edit application details
/applications/:id/submit               - Submit application form
/applications/:id/update-status        - Update status dialog
/applications/stats                    - Statistics dashboard
```

## Forms and Validation

### Start Application Form

**Validation Rules:**
- Job Listing: Required, must be a saved listing
- Target Submission Date: Must be in the future
- Notes: Max 2000 characters

**Error Messages:**
- "Please select a job listing"
- "Target date must be in the future"

### Submit Application Form

**Validation Rules:**
- Submission Method: Required
- Confirmation Number: Max 200 characters
- Documents: At least one document required
- Notes: Max 2000 characters
- Submitted Date: Cannot be in the future

**Error Messages:**
- "Please select a submission method"
- "Please select at least one submitted document"
- "Submission date cannot be in the future"

### Update Status Form

**Validation Rules:**
- Status: Required, must be valid transition from current status
- Updated By: Max 200 characters
- Notes: Max 2000 characters
- Next Steps: Max 500 characters

**Error Messages:**
- "Invalid status transition"
- "Please select a valid status"

## UI/UX Requirements

### Responsive Design
- Mobile: Single column, stacked cards, horizontal scroll for Kanban
- Tablet: 2-column grid, condensed Kanban
- Desktop: Full Kanban board or multi-column grid

### Accessibility
- ARIA labels for status badges and buttons
- Keyboard navigation for Kanban board
- Screen reader announcements for status changes
- Focus management for dialogs
- High contrast mode support

### Performance
- Virtual scrolling for large application lists
- Lazy loading of application details
- Debounced search (300ms)
- Optimistic UI updates
- Skeleton loaders during fetch

### Interactions

**Drag and Drop:**
- Drag applications between status columns
- Visual feedback during drag
- Confirm on drop if significant status change
- Undo option for accidental moves

**Keyboard Shortcuts:**
- `N` - New application
- `S` - Submit current draft
- `U` - Update status
- `Ctrl/Cmd + Enter` - Quick submit form
- `Esc` - Close dialogs
- `Arrow keys` - Navigate Kanban columns

**Animations:**
- Smooth transitions between statuses
- Card animations on status change
- Timeline progression animations
- Modal slide-ins
- Toast notifications

## Data Display

### Application Card (Kanban View)
```
┌────────────────────────────┐
│ Senior Software Engineer   │
│ Tech Corp                  │
│                            │
│ [INTERVIEWING]             │
│                            │
│ Submitted: Dec 22          │
│ (6 days ago)               │
│                            │
│ Stage: 2nd Round Interview │
│                            │
│ [View Details →]           │
└────────────────────────────┘
```

### Application Card (Grid View)
```
┌─────────────────────────────────┐
│ [INTERVIEWING]        [⋮ Menu]  │
│                                  │
│ Senior Software Engineer         │
│ Tech Corp • San Francisco, CA    │
│                                  │
│ Started: Dec 20                  │
│ Submitted: Dec 22                │
│                                  │
│ Current Stage:                   │
│ Second round interview scheduled │
│                                  │
│ [Update Status] [View Details]  │
└─────────────────────────────────┘
```

### Status Timeline
```
● Interviewing - Dec 26, 2025
│ Updated by: Jane Smith
│ Second round scheduled for Jan 5
│ Next: Prepare technical questions
│
● Phone Screen - Dec 24, 2025
│ Completed successfully
│
● Under Review - Dec 23, 2025
│ Application received
│
● Submitted - Dec 22, 2025
│ Via online portal
│
○ Started - Dec 20, 2025
```

### Kanban Board Layout
```
┌──────────┬──────────┬──────────┬──────────┬──────────┐
│ DRAFT    │SUBMITTED │UNDER REV │PHONE SCR │INTERVIEW │
│   (3)    │   (8)    │  (12)    │   (4)    │   (5)    │
├──────────┼──────────┼──────────┼──────────┼──────────┤
│ [Card]   │ [Card]   │ [Card]   │ [Card]   │ [Card]   │
│ [Card]   │ [Card]   │ [Card]   │ [Card]   │ [Card]   │
│ [Card]   │ [Card]   │ [Card]   │ [Card]   │ [Card]   │
│          │ [Card]   │ [Card]   │ [Card]   │ [Card]   │
│          │ [+]      │ [+]      │          │ [Card]   │
└──────────┴──────────┴──────────┴──────────┴──────────┘
```

## Notifications

**Toast Messages:**
- "Application started successfully"
- "Application submitted"
- "Status updated to [Status]"
- "Application withdrawn"
- "Application marked as rejected"

**Email Notifications:**
- Reminder to submit draft applications
- Status change confirmations
- Weekly application summary

**Push Notifications:**
- Reminder for upcoming submission deadlines
- Status updates from company (if tracked)

## Integration Points

### Job Listings
- Link applications to job listings
- Create application from saved listing
- Show application status on listing card

### Interviews
- Create interview from application
- Link interviews to application
- Show interview count on application card

### Offers
- Link offers to applications
- Transition to offer when received
- Track offer acceptance/rejection

### Calendar
- Add submission deadlines to calendar
- Sync interview dates
- Reminder notifications

### Document Storage
- Upload and attach resumes, cover letters
- Version control for documents
- Quick access to submitted documents

## Error Handling

### Error States
- Empty state (no applications yet)
- No results found (after filtering)
- Network error (with retry)
- Invalid status transition
- Application not found (404)

### Error Messages
- User-friendly language
- Actionable suggestions
- Validation errors inline
- Toast for system errors

## Testing Requirements

### Unit Tests
- Component rendering
- Form validation
- Status transitions
- Action creators
- Reducers

### Integration Tests
- Application creation flow
- Status update flow
- Withdrawal flow
- Kanban drag-and-drop

### E2E Tests
- Complete application lifecycle
- Start to submission
- Status updates through to offer/rejection
- Multi-application management

## Browser Support
- Chrome/Edge (latest 2 versions)
- Firefox (latest 2 versions)
- Safari (latest 2 versions)
- Mobile browsers (iOS 14+, Android 10+)

## Localization
- Date/time formatting
- Status labels
- Number formatting
- Currency for offer amounts
- RTL support
