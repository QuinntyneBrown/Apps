# Job Listings - Frontend Requirements

## Overview
The Job Listings UI provides an intuitive interface for job seekers to discover, organize, and track job opportunities.

## Pages/Views

### 1. Job Listings Dashboard
**Route:** `/job-listings`

**Purpose:** Main hub for viewing and managing all job listings.

**Components:**
- Header with search bar and filters
- Status tabs (All, Discovered, Saved, Archived)
- Job listings grid/list view toggle
- Pagination controls
- Quick stats summary

**Features:**
- View all job listings with status indicators
- Quick filter by status, priority, category
- Search across title, company, description
- Sort by discovered date, deadline, priority, company
- Bulk actions (archive selected, delete selected)
- Export to CSV/PDF

### 2. Add Job Listing Form
**Route:** `/job-listings/new`

**Purpose:** Manual entry of a new job listing.

**Form Fields:**
- Job Title* (text input)
- Company* (text input with autocomplete from previous companies)
- Location* (text input with suggestions)
- Source URL* (URL input with validation)
- Description* (rich text editor/textarea)
- Employment Type (dropdown: Full-time, Part-time, Contract, Temporary, Internship)
- Experience Level (dropdown: Entry, Mid, Senior, Lead, Executive)
- Salary Range (optional):
  - Min (number input)
  - Max (number input)
  - Currency (dropdown: USD, EUR, GBP, CAD, etc.)
  - Period (dropdown: Hourly, Daily, Weekly, Monthly, Annual)
- Tags (multi-select with create new option)

**Actions:**
- Save as Discovered (primary button)
- Save and Mark as Saved (secondary button)
- Cancel

**Validation:**
- Real-time validation for required fields
- URL format validation
- Salary min must be <= max
- Show character counts for text fields

### 3. Job Listing Detail View
**Route:** `/job-listings/{id}`

**Purpose:** View and edit complete details of a job listing.

**Sections:**
- Header (title, company, status badge, quick actions)
- Job Details Panel:
  - Location
  - Employment type
  - Experience level
  - Salary range
  - Tags
  - Source URL (clickable link)
  - Discovered date
- Description Panel (full formatted description)
- User Notes Panel (editable)
- Metadata Panel:
  - Priority (editable)
  - Category (editable)
  - Deadline (editable with date picker)
  - Status history timeline
- Related Items:
  - Linked applications (if any)
  - Linked networking contacts
  - Interview records

**Actions:**
- Edit details (inline editing or modal)
- Save listing (if discovered)
- Archive listing (if discovered/saved)
- Delete listing (with confirmation)
- Share listing (email, copy link)
- Add to application
- Print/Export to PDF

### 4. Browser Extension Quick Add
**Purpose:** Capture job listings from any website.

**UI:**
- Popup overlay when extension icon clicked
- Auto-populated fields from page content:
  - Title (from page title/h1)
  - Company (detected from page)
  - URL (current page)
  - Description (extracted from page)
- Editable fields before saving
- One-click "Save" or "Save & Mark Important"

## Components

### JobListingCard
**Purpose:** Display job listing in grid/list view.

**Props:**
- `jobListing` - Job listing data object
- `viewMode` - "grid" or "list"
- `onSave` - Callback for save action
- `onArchive` - Callback for archive action
- `onSelect` - Callback for selection (bulk actions)

**Display:**
- Job title (heading)
- Company name with icon/logo
- Location with icon
- Status badge
- Priority indicator (color-coded)
- Deadline (if set, with urgency indicator)
- Tags (first 3-5, with "+N more")
- Salary range (if available)
- Discovery date (relative time)
- Quick action buttons (Save, Archive, View)

**States:**
- Default
- Hover (show additional actions)
- Selected (for bulk operations)
- Expired (if deadline passed)

### SearchAndFilterBar
**Purpose:** Global search and filtering for job listings.

**Features:**
- Search input with debounced API calls
- Filter dropdowns:
  - Status (multi-select)
  - Priority (multi-select)
  - Category (multi-select from user's categories)
  - Tags (multi-select with autocomplete)
  - Employment Type (multi-select)
  - Experience Level (multi-select)
  - Salary Range (min/max inputs)
- Active filters display (chips with remove option)
- Clear all filters button
- Save filter preset option

### PrioritySelector
**Purpose:** Select and display priority level.

**Options:**
- High (red indicator)
- Medium (yellow indicator)
- Low (green indicator)

**Display:** Color-coded badges or dropdown

### StatusBadge
**Purpose:** Visual indicator of listing status.

**Statuses:**
- Discovered (blue)
- Saved (green)
- Archived (gray)

### DeadlineIndicator
**Purpose:** Show application deadline with urgency.

**Logic:**
- Red: Deadline within 3 days
- Orange: Deadline within 7 days
- Yellow: Deadline within 14 days
- Green: Deadline more than 14 days away
- Gray: No deadline set

### CategoryManager
**Purpose:** Create and manage job listing categories.

**Features:**
- View all categories
- Create new category
- Rename category
- Delete category (with reassignment of listings)
- Assign color to category

### JobListingStats
**Purpose:** Display statistics dashboard.

**Metrics:**
- Total listings
- By status (pie chart)
- By priority (bar chart)
- By category (list with counts)
- Upcoming deadlines (list)
- Recent discoveries (timeline)
- Top companies (list)
- Most common tags (tag cloud)

### BulkActionsBar
**Purpose:** Perform actions on multiple selected listings.

**Actions:**
- Archive selected
- Delete selected
- Change priority
- Assign to category
- Export selected

## State Management

### Redux/State Slices

**jobListingsSlice:**
```typescript
{
  listings: JobListing[],
  selectedListingId: string | null,
  filters: {
    status: string[],
    priority: string[],
    categories: string[],
    tags: string[],
    search: string,
    employmentType: string[],
    experienceLevel: string[]
  },
  sorting: {
    field: 'discoveredAt' | 'deadline' | 'priority' | 'company',
    direction: 'asc' | 'desc'
  },
  pagination: {
    pageNumber: number,
    pageSize: number,
    totalCount: number
  },
  viewMode: 'grid' | 'list',
  selectedIds: string[],
  loading: boolean,
  error: string | null,
  stats: JobListingStats
}
```

**Actions:**
- `fetchJobListings` - Load listings with filters
- `fetchJobListingById` - Load single listing
- `createJobListing` - Create new listing
- `updateJobListing` - Update listing details
- `saveJobListing` - Save a discovered listing
- `archiveJobListing` - Archive a listing
- `deleteJobListing` - Delete a listing
- `setFilters` - Update filter criteria
- `setSorting` - Update sort order
- `setViewMode` - Toggle grid/list view
- `selectListing` - Add to bulk selection
- `clearSelection` - Clear bulk selection
- `fetchStats` - Load statistics

## Routing

```
/job-listings                  - Dashboard (list all)
/job-listings/new              - Add new listing
/job-listings/:id              - View/edit listing details
/job-listings/stats            - Statistics dashboard
/job-listings/categories       - Manage categories
```

## Forms and Validation

### Add/Edit Job Listing Form

**Validation Rules:**
- Title: Required, 1-500 characters
- Company: Required, 1-200 characters
- Location: Required, 1-200 characters
- Source URL: Required, valid URL format
- Description: Required, 1-10,000 characters
- Salary Min <= Salary Max
- Deadline must be future date
- Tags: Max 50, each max 100 characters

**Error Messages:**
- "Job title is required"
- "Please enter a valid URL"
- "Minimum salary cannot exceed maximum salary"
- "Deadline must be in the future"

### Save Job Listing Form

**Fields:**
- Priority* (required)
- Notes (optional, max 2000 chars)
- Category (optional, select or create)
- Deadline (optional, date picker)

**Validation:**
- Priority must be selected
- Character count for notes

## UI/UX Requirements

### Responsive Design
- Mobile: Single column, stacked cards
- Tablet: 2-column grid
- Desktop: 3-column grid or detailed list view
- Filters collapse into drawer on mobile

### Accessibility
- ARIA labels for all interactive elements
- Keyboard navigation support
- Screen reader announcements for actions
- Focus management for modals
- Color contrast ratios meet WCAG AA standards
- Skip links for navigation

### Performance
- Virtual scrolling for large lists (>100 items)
- Lazy loading of job descriptions
- Image optimization for company logos
- Debounced search (300ms)
- Optimistic UI updates

### Interactions

**Drag and Drop:**
- Drag listings between status columns
- Drag to reorder within priority levels

**Keyboard Shortcuts:**
- `N` - New job listing
- `S` - Focus search
- `/` - Toggle filters
- `Ctrl/Cmd + A` - Select all visible
- `Delete` - Archive selected
- `Arrow keys` - Navigate cards
- `Enter` - Open selected listing
- `Esc` - Close modals/clear selection

**Animations:**
- Smooth transitions between views
- Card hover effects
- Slide-in for side panels
- Fade for modals
- Progress indicators for async actions

## Data Display

### Job Listing Card (Grid View)
```
┌─────────────────────────────────┐
│ [Priority]    [Status Badge]    │
│                                  │
│ Job Title                        │
│ Company Name | Location          │
│                                  │
│ [Tag] [Tag] [Tag] +2            │
│                                  │
│ $120k - $180k/yr                │
│                                  │
│ Discovered 2 days ago           │
│ Deadline: Jan 15 [!]            │
│                                  │
│ [View] [Save] [Archive]         │
└─────────────────────────────────┘
```

### Job Listing Row (List View)
```
[☐] [Priority] | Job Title | Company | Location | Status | [Tags...] | Deadline | [Actions ⋮]
```

### Filters Panel
```
Search: [_______________] [🔍]

Status:
☑ Discovered (45)
☑ Saved (82)
☐ Archived (23)

Priority:
☐ High (15)
☑ Medium (38)
☑ Low (29)

Category:
☐ Dream Jobs (12)
☐ Backend Roles (34)
☐ Remote Only (28)

Tags:
[Select tags...▾]

[Clear All] [Apply Filters]
```

## Notifications

**Toast Messages:**
- "Job listing saved successfully"
- "Job listing archived"
- "Job listing deleted"
- "Failed to load job listings. Please try again."
- "3 listings exported to CSV"
- "Deadline approaching for [Job Title] at [Company]"

**Email Notifications:**
- Daily digest of upcoming deadlines
- Weekly summary of discovered listings
- Reminders for listings with approaching deadlines

## Integration Points

### Browser Extension
- Auto-detect job listing information
- One-click save from any website
- Badge showing number of saved listings

### Email Integration
- Parse job alert emails
- Extract and create listings automatically

### Calendar Integration
- Add deadlines to calendar
- Sync with Google Calendar, Outlook

### Export Options
- CSV export (all fields)
- PDF export (formatted listing details)
- Shareable link (with privacy controls)

## Error Handling

### Error States
- Empty state (no listings yet)
- No results found (after filtering)
- Network error (with retry button)
- Loading error (failed to fetch)
- Permission denied
- Listing not found (404)

### Error Messages
- User-friendly language
- Actionable suggestions
- Retry options where applicable
- Contact support for critical errors

## Testing Requirements

### Unit Tests
- Component rendering
- Action creators
- Reducers
- Validation functions
- Utility functions

### Integration Tests
- Form submission flows
- Filter and search functionality
- Bulk operations
- Navigation flows

### E2E Tests
- Create job listing
- Save and archive listing
- Search and filter
- Complete user journey from discovery to application

## Browser Support
- Chrome/Edge (latest 2 versions)
- Firefox (latest 2 versions)
- Safari (latest 2 versions)
- Mobile Safari (iOS 14+)
- Chrome Mobile (Android 10+)

## Localization
- Date formatting based on user locale
- Currency formatting
- Number formatting
- RTL support for appropriate languages
- Translatable UI strings
