# Project Management - Frontend Requirements

## Overview
User interface for creating, viewing, and managing classic car restoration projects.

## Pages/Views

### 1. Projects Dashboard
**Route**: `/projects`

**Purpose**: Display overview of all restoration projects

**Components**:
- Project cards/list with thumbnail images
- Filter by status (All, In Progress, Completed, Abandoned)
- Sort by date, name, completion percentage
- Search by vehicle make, model, VIN
- Quick stats: Total projects, Active projects, Completion rate
- "New Project" button

**Data Display**:
- Vehicle make/model/year
- Project thumbnail image
- Status badge
- Progress percentage
- Budget status (on track, over budget)
- Days since start
- Target completion date

**Interactions**:
- Click card to view project details
- Quick actions menu: Edit, Archive, Share
- Filter and sort controls
- Search with auto-complete

### 2. Create Project
**Route**: `/projects/new`

**Purpose**: Start a new restoration project

**Form Fields**:
- Vehicle Information:
  - Make (text input, required)
  - Model (text input, required)
  - Year (number input, required)
  - VIN (text input, 17 chars, required)
  - Color (text input)
- Acquisition Details:
  - Acquisition date (date picker, required)
  - Purchase price (currency input, required)
  - Purchase location (text input)
  - Condition at start (textarea)
- Project Planning:
  - Restoration scope (textarea)
  - Target completion (date picker)
  - Initial photos (file upload, multiple)

**Validation**:
- Real-time validation on field blur
- VIN format validation
- Year range validation
- Required field indicators
- Duplicate VIN check

**Actions**:
- Save as draft
- Create project
- Cancel and discard
- Upload photos

### 3. Project Details
**Route**: `/projects/{id}`

**Purpose**: View comprehensive project information

**Sections**:
- **Header**:
  - Vehicle name and image
  - Status badge
  - Progress bar
  - Action menu (Edit, Complete, Abandon, Share)

- **Overview Tab**:
  - Vehicle specifications
  - Acquisition details
  - Project scope summary
  - Key metrics (time, cost, completion %)

- **Timeline Tab**:
  - Visual timeline of milestones
  - Gantt chart view option
  - Critical path indicators
  - Milestone cards with status

- **Photos Tab**:
  - Before photos gallery
  - Progress photos organized by date
  - After photos (when completed)
  - Before/after comparison slider

- **Statistics Tab**:
  - Budget vs actual costs chart
  - Time spent breakdown
  - Parts installed count
  - Work sessions summary

**Interactions**:
- Edit project details inline
- Add/edit milestones
- Upload photos with drag-and-drop
- View full-screen photo gallery
- Export project report (PDF)
- Share project link

### 4. Define Scope
**Route**: `/projects/{id}/scope`

**Purpose**: Set restoration objectives and parameters

**Form Fields**:
- Restoration type (radio buttons):
  - Concours (show quality)
  - Restomod (modern upgrades)
  - Driver (reliable daily driver)
- Systems to restore (checkboxes):
  - Engine/Drivetrain
  - Suspension/Brakes
  - Electrical
  - Interior
  - Exterior/Paint
  - Trim/Chrome
- Originality level (slider):
  - Fully original
  - Period correct
  - Modernized
  - Custom
- Budget ceiling (currency input)
- Quality targets (textarea)

**Validation**:
- At least one system must be selected
- Budget must exceed purchase price
- Quality targets required for concours

**Actions**:
- Save scope
- Update scope
- Reset to defaults
- Generate material estimate

### 5. Milestones Manager
**Route**: `/projects/{id}/milestones`

**Purpose**: Plan and track project phases

**Components**:
- Milestone list with status indicators
- Progress tracking per milestone
- Time and cost summaries
- "Add Milestone" button
- Timeline visualization

**Milestone Card**:
- Name and description
- Target date
- Status (Pending, In Progress, Completed)
- Associated photos
- Time spent
- Costs incurred
- Quality rating (when complete)
- Action buttons (Edit, Complete, Delete)

**Add/Edit Milestone Modal**:
- Name (text input)
- Description (textarea)
- Target date (date picker)
- Planned tasks (list builder)
- Photo upload
- Save/Cancel buttons

**Interactions**:
- Drag-and-drop to reorder milestones
- Mark as complete with completion form
- Add quality notes and photos on completion
- Delete pending milestones
- Expand/collapse milestone details

### 6. Complete Project
**Route**: `/projects/{id}/complete`

**Purpose**: Finalize restoration and document completion

**Form Sections**:
- Completion date (date picker, default today)
- Final condition assessment (textarea)
- Quality rating (star rating, 1-5)
- Before/after photo selection
- Total time summary (calculated)
- Total cost summary (calculated)
- Reveal event details (optional):
  - Event name
  - Event date
  - Attendees
- Lessons learned (textarea)

**Validation**:
- All milestones must be complete or cancelled
- Before and after photos required
- Quality rating required
- Completion date cannot be before start

**Actions**:
- Mark as complete
- Generate completion report
- Share completion announcement
- Cancel and return

## Components Library

### ProjectCard
- Thumbnail image
- Vehicle info display
- Status badge
- Progress indicator
- Quick actions menu

### MilestoneTimeline
- Visual timeline component
- Milestone markers
- Date labels
- Status indicators
- Interactive tooltips

### BeforeAfterComparison
- Side-by-side image display
- Slider comparison
- Fullscreen mode
- Caption display

### ProgressIndicator
- Circular progress
- Linear progress bar
- Percentage display
- Color coding (on track, at risk, over)

### BudgetMeter
- Spent vs budgeted visualization
- Warning indicators
- Breakdown by category
- Trend indicator

## State Management

### Project State
```javascript
{
  projects: [],
  currentProject: null,
  loading: false,
  error: null,
  filters: {
    status: 'all',
    search: '',
    sortBy: 'date'
  }
}
```

### Actions
- `fetchProjects()` - Load project list
- `fetchProjectById(id)` - Load single project
- `createProject(data)` - Create new project
- `updateProject(id, data)` - Update project
- `deleteProject(id)` - Delete project
- `completeProject(id, data)` - Mark complete
- `abandonProject(id, data)` - Mark abandoned
- `setFilter(filter)` - Update filters

## Responsive Design

### Mobile (< 768px)
- Single column layout
- Stacked cards
- Bottom navigation
- Swipe gestures for galleries
- Collapsible sections

### Tablet (768px - 1024px)
- Two column grid
- Side drawer navigation
- Touch-optimized controls
- Responsive tables

### Desktop (> 1024px)
- Multi-column layouts
- Sidebar navigation
- Hover interactions
- Full-width tables
- Keyboard shortcuts

## Accessibility Requirements
- ARIA labels for all interactive elements
- Keyboard navigation support
- Screen reader compatibility
- High contrast mode support
- Focus indicators
- Alt text for all images
- Form field labels and hints

## Performance Requirements
- Initial page load < 2s
- Project list render < 500ms
- Smooth animations (60fps)
- Lazy load images
- Infinite scroll for large lists
- Debounced search input
- Optimistic UI updates

## Offline Support
- Cache project list
- Queue project updates
- Sync when online
- Offline indicator
- Conflict resolution

## Error Handling
- Validation errors inline
- Network errors with retry
- Not found pages
- Generic error boundaries
- Toast notifications for actions
- Form submission errors

## Testing Requirements
- Unit tests for components
- Integration tests for flows
- E2E tests for critical paths
- Accessibility tests
- Visual regression tests
- Performance tests
