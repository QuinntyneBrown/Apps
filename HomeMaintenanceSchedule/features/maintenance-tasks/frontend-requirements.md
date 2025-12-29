# Maintenance Tasks - Frontend Requirements

## Overview
The Maintenance Tasks frontend provides an intuitive interface for homeowners to create, manage, schedule, and track home maintenance tasks with calendar integration, reminders, and visual progress tracking.

## User Interface Components

### 1. Task Dashboard
**Purpose**: Primary view showing task overview and quick actions

**Components**:
- **Summary Cards**:
  - Upcoming Tasks (next 7 days) - Yellow accent
  - Overdue Tasks - Red accent with count badge
  - Completed This Month - Green accent with percentage
  - Total Estimated Costs - Blue accent with currency

- **Quick Actions Bar**:
  - "+ New Task" button (primary CTA)
  - Filter dropdown (All, By Category, By Priority, By Status)
  - Sort dropdown (Due Date, Priority, Created Date)
  - View toggle (List/Calendar/Kanban)

- **Task List**:
  - Compact card view with key information
  - Color-coded priority indicators
  - Status badges
  - Quick complete checkbox
  - Action menu (Edit, Postpone, Cancel, Delete)

**Responsive Design**:
- Mobile: Stacked cards, bottom navigation
- Tablet: 2-column grid
- Desktop: 3-column grid with sidebar

### 2. Task Creation/Edit Form
**Purpose**: Create new or edit existing maintenance tasks

**Form Fields**:
```
Task Information Section:
- Title * (text input, max 200 chars, character counter)
- Description (textarea, max 2000 chars, rich text editor)
- Category * (dropdown: HVAC, Plumbing, Electrical, etc.)
- Priority * (radio buttons: Low, Medium, High, Critical with color coding)

Scheduling Section:
- Scheduled Date * (date/time picker)
- Due Date * (date/time picker, must be >= scheduled date)
- Recurrence (dropdown: None, Daily, Weekly, Monthly, Quarterly, etc.)
- Recurrence Interval (number input, only shown if recurrence != None)

Assignment Section:
- Property * (dropdown, multi-property support)
- Assign to Provider (dropdown, optional, searchable)

Cost Section:
- Estimated Cost (currency input with $ prefix)
- Notes (textarea for cost justification)

Attachments:
- Photo Upload (drag & drop area, max 5 photos, 10MB each)
- File type validation (JPG, PNG, PDF)
- Preview thumbnails with remove option
```

**Validation**:
- Real-time field validation with error messages
- Highlight required fields
- Date logic validation (due date >= scheduled date)
- Form submit disabled until valid

**UX Features**:
- Auto-save draft to localStorage
- Template selection for common tasks
- Duplicate task feature
- Cancel with confirmation dialog if changes exist

### 3. Calendar View
**Purpose**: Visualize tasks on a calendar for scheduling

**Features**:
- Month/Week/Day view toggle
- Color-coded task blocks by priority
- Drag & drop to reschedule
- Click to view task details (modal)
- Quick complete from calendar
- Today indicator
- Overdue tasks highlighted in red
- Filter by category/priority/property

**Integration**:
- Export to Google Calendar
- Export to Outlook Calendar
- iCal format download

### 4. Task Details View
**Purpose**: Complete task information and history

**Sections**:
- **Header**:
  - Title with priority badge
  - Status indicator
  - Action buttons (Edit, Complete, Postpone, Cancel, Delete)

- **Task Information**:
  - Description
  - Category and Priority
  - Scheduled Date, Due Date
  - Recurrence pattern
  - Assigned Provider (with contact link)
  - Property

- **Cost Summary**:
  - Estimated: $X.XX
  - Actual: $X.XX (after completion)
  - Variance: +/- X%

- **Photos**:
  - Before/After gallery
  - Lightbox view
  - Download option

- **Notes & History**:
  - Completion notes
  - Postponement history
  - Edit history with timestamps
  - Activity timeline

- **Related Tasks**:
  - Previous instances (for recurring tasks)
  - Next scheduled instance

### 5. Task Completion Modal
**Purpose**: Quick completion workflow

**Form Fields**:
```
- Completed Date * (date/time picker, default: now)
- Actual Cost (currency input)
- Completion Notes (textarea, optional)
- Before/After Photos (upload, optional)
- Rate Provider (if assigned, 1-5 stars)
- Mark as Successful (checkbox)
```

**Submit Actions**:
- "Complete Task" button (primary)
- "Complete & Create Next" (for non-recurring tasks)
- "Cancel" button

### 6. Filters & Search
**Purpose**: Find and filter tasks efficiently

**Filter Options**:
- **Status**: All, Scheduled, In Progress, Completed, Overdue, Cancelled
- **Priority**: All, Low, Medium, High, Critical
- **Category**: All, HVAC, Plumbing, Electrical, etc.
- **Date Range**: Custom date picker, Presets (This Week, This Month, This Quarter)
- **Property**: Dropdown (multi-select for property managers)
- **Assigned Provider**: Dropdown

**Search**:
- Text search in title, description, notes
- Auto-suggest results as typing
- Search history
- Advanced search modal for complex queries

**Filter State**:
- Persist filters in URL query params
- Save custom filter presets
- "Clear All Filters" button

### 7. Kanban Board View
**Purpose**: Visual task workflow management

**Columns**:
- Scheduled
- In Progress
- Completed (last 30 days)
- Overdue (highlighted)

**Features**:
- Drag & drop between columns (updates status)
- Card shows: Title, Due Date, Priority badge, Category icon
- Column count badges
- Collapse/Expand columns
- Filter/Search applies to board

### 8. Task Statistics & Reports
**Purpose**: Analyze maintenance patterns and costs

**Visualizations**:
- **Completion Rate Chart**: Line chart over time
- **Cost Analysis**: Bar chart by month, category breakdown pie chart
- **Tasks by Category**: Donut chart
- **Upcoming vs Overdue**: Gauge chart
- **Average Time to Complete**: Metric card
- **Most Common Tasks**: Top 5 list

**Export Options**:
- Download as PDF report
- Export data to CSV
- Email report

### 9. Notifications & Reminders
**Purpose**: Keep users informed of task deadlines

**Notification Types**:
- Task due in 7 days (Info)
- Task due in 3 days (Warning)
- Task due tomorrow (Warning)
- Task due today (Alert)
- Task overdue (Critical Alert)
- Recurring task created (Info)

**Display**:
- Toast notifications (bottom-right)
- Notification bell icon with badge count
- Notification center panel
- Email notifications (opt-in)
- SMS notifications (opt-in, premium feature)

**Settings**:
- Notification preferences panel
- Toggle notification types
- Set quiet hours
- Snooze option (1 hour, 1 day, 1 week)

## State Management

### Redux Store Structure
```javascript
{
  tasks: {
    items: [],
    selectedTask: null,
    loading: false,
    error: null,
    filters: {
      status: 'All',
      priority: 'All',
      category: 'All',
      dateRange: null,
      propertyId: null,
      providerId: null
    },
    pagination: {
      page: 1,
      pageSize: 20,
      totalCount: 0
    },
    statistics: {
      totalTasks: 0,
      completedTasks: 0,
      overdueTasks: 0,
      upcomingTasks: 0,
      completionRate: 0,
      totalCost: 0
    }
  }
}
```

### Actions
- `fetchTasks()`, `fetchTaskById(id)`
- `createTask(data)`, `updateTask(id, data)`
- `completeTask(id, data)`, `postponeTask(id, data)`, `cancelTask(id, data)`
- `deleteTask(id)`
- `setFilter(filterType, value)`, `clearFilters()`
- `fetchStatistics()`

## API Integration

### Axios Service
```javascript
class TaskService {
  async getTasks(filters, pagination) { }
  async getTaskById(id) { }
  async createTask(data) { }
  async updateTask(id, data) { }
  async completeTask(id, data) { }
  async postponeTask(id, data) { }
  async cancelTask(id, data) { }
  async deleteTask(id) { }
  async getUpcomingTasks(days) { }
  async getOverdueTasks() { }
  async getStatistics(dateRange) { }
}
```

### Error Handling
- Network errors: Show retry toast
- Validation errors: Display field-specific errors
- 403 Forbidden: Redirect to login
- 404 Not Found: Show "Task not found" message
- 500 Server Error: Show generic error with support link

### Loading States
- Skeleton loaders for list items
- Spinner for full-page loads
- Disabled buttons with spinner during submission
- Optimistic updates for quick actions

## Forms & Validation

### React Hook Form
```javascript
const taskFormSchema = yup.object({
  title: yup.string().required().max(200),
  description: yup.string().max(2000),
  category: yup.string().required().oneOf(TaskCategories),
  priority: yup.string().required().oneOf(TaskPriorities),
  scheduledDate: yup.date().required().min(new Date()),
  dueDate: yup.date().required().min(yup.ref('scheduledDate')),
  recurrencePattern: yup.string().required(),
  recurrenceInterval: yup.number().when('recurrencePattern', {
    is: (val) => val !== 'None',
    then: yup.number().required().min(1)
  }),
  estimatedCost: yup.number().min(0),
  propertyId: yup.string().required(),
  assignedProviderId: yup.string().nullable()
});
```

## Accessibility (WCAG 2.1 AA)

- Semantic HTML elements
- ARIA labels for interactive elements
- Keyboard navigation support (Tab, Enter, Escape, Arrow keys)
- Focus indicators
- Color contrast ratio >= 4.5:1
- Screen reader announcements for dynamic content
- Skip navigation links
- Alt text for images

## Performance Optimization

- React.memo for list items
- Virtualized lists (react-window) for 100+ tasks
- Lazy load task details
- Debounced search input (300ms)
- Image lazy loading and optimization
- Code splitting by route
- Service Worker for offline caching

## Responsive Breakpoints

- Mobile: < 640px
- Tablet: 640px - 1024px
- Desktop: > 1024px
- Large Desktop: > 1440px

## Theming & Styling

### Color Palette
```css
/* Priority Colors */
--priority-low: #6B7280;      /* Gray */
--priority-medium: #F59E0B;   /* Amber */
--priority-high: #EF4444;     /* Red */
--priority-critical: #DC2626; /* Dark Red */

/* Status Colors */
--status-scheduled: #3B82F6;  /* Blue */
--status-inprogress: #8B5CF6; /* Purple */
--status-completed: #10B981;  /* Green */
--status-overdue: #EF4444;    /* Red */
--status-cancelled: #6B7280;  /* Gray */

/* UI Colors */
--primary: #2563EB;
--secondary: #64748B;
--success: #10B981;
--warning: #F59E0B;
--error: #EF4444;
--info: #3B82F6;
```

### Typography
- Headings: Inter font, Bold (600-700)
- Body: Inter font, Regular (400)
- Monospace (costs, dates): Roboto Mono

## Testing Requirements

### Unit Tests (Jest + React Testing Library)
- Component rendering
- User interactions (clicks, form submissions)
- Form validation
- State updates
- Utility functions

### Integration Tests
- Complete task creation flow
- Filter and search functionality
- Task completion workflow
- Calendar navigation

### E2E Tests (Cypress)
- User can create a task
- User can complete a task
- User can filter tasks
- User receives overdue notification

## Browser Support

- Chrome/Edge: Last 2 versions
- Firefox: Last 2 versions
- Safari: Last 2 versions
- Mobile Safari: iOS 13+
- Mobile Chrome: Android 8+

## Third-Party Libraries

- **UI Components**: Material-UI or Tailwind CSS + Headless UI
- **Forms**: React Hook Form + Yup validation
- **Date Handling**: date-fns or Day.js
- **Charts**: Recharts or Chart.js
- **Calendar**: FullCalendar or React Big Calendar
- **Drag & Drop**: react-beautiful-dnd
- **Notifications**: react-toastify
- **File Upload**: react-dropzone
- **Icons**: Heroicons or Material Icons

---

**Version**: 1.0
**Last Updated**: 2025-12-29
