# Task Management - Frontend Requirements

## Overview

The Task Management frontend provides an intuitive interface for homeowners to schedule, track, complete, and manage maintenance tasks. The interface supports filtering, sorting, calendar views, and real-time notifications.

## User Interface Components

### 1. Task Dashboard

**Purpose**: Main landing page showing task overview and quick actions

**Components**:
- **Statistics Cards**
  - Total Active Tasks
  - Overdue Tasks (with red alert badge)
  - Tasks Due This Week
  - Completed This Month

- **Quick Filters**
  - All Tasks
  - Upcoming (Next 7 days)
  - Overdue
  - In Progress
  - Completed

- **Task List View**
  - Sortable columns (Title, Due Date, Priority, Status, Category)
  - Search bar with autocomplete
  - Bulk actions (Mark Complete, Postpone, Cancel)
  - Status indicators (color-coded badges)
  - Priority indicators (icons with colors)

- **Calendar View Toggle**
  - Month view
  - Week view
  - Day view
  - Task indicators on calendar dates

**Wireframe Elements**:
```
+----------------------------------------------------------+
|  [Logo] Home Maintenance Schedule          [User Menu]  |
+----------------------------------------------------------+
| Dashboard | Tasks | Providers | Seasonal | Appliances   |
+----------------------------------------------------------+
|                                                          |
| +-------------+ +-------------+ +-------------+          |
| | Active: 12  | | Overdue: 3  | | This Week:5 |          |
| +-------------+ +-------------+ +-------------+          |
|                                                          |
| [All] [Upcoming] [Overdue] [In Progress] [Completed]    |
|                                                          |
| Search: [_______________________] [+ New Task]           |
|                                                          |
| +--------------------------------------------------+     |
| | Title          | Due Date  | Priority | Status  |     |
| +--------------------------------------------------+     |
| | HVAC Filter    | Jan 15    | Medium  | Scheduled|     |
| | Clean Gutters  | Jan 12    | High    | Overdue  |     |
| | Check Smoke... | Jan 20    | High    | Scheduled|     |
| +--------------------------------------------------+     |
+----------------------------------------------------------+
```

### 2. Task Creation Form

**Purpose**: Create or schedule new maintenance tasks

**Form Fields**:

- **Basic Information**
  - Title (required, text input)
  - Description (textarea, rich text optional)
  - Category (dropdown: HVAC, Plumbing, Electrical, etc.)
  - Priority (radio buttons: Low, Medium, High, Critical)

- **Scheduling**
  - Scheduled Date (date picker)
  - Due Date (date picker with time)
  - Recurrence Pattern (dropdown: None, Daily, Weekly, Monthly, Quarterly, Annually)
  - Recurrence Interval (number input, shown if pattern != None)

- **Assignment**
  - Assign to Service Provider (dropdown, optional)
  - Property (dropdown if multiple properties)

- **Cost**
  - Estimated Cost (currency input, optional)

- **Attachments**
  - Upload Photos (drag & drop zone)
  - Add Notes (textarea)

**Validation**:
- Title: Required, max 200 characters
- Due Date: Must be >= Scheduled Date
- Recurrence Interval: Required if pattern is not None

**User Actions**:
- Save Task (raises TaskScheduled event)
- Save as Template
- Cancel

### 3. Task Detail View

**Purpose**: Display comprehensive information about a specific task

**Sections**:

- **Header**
  - Task Title
  - Status Badge
  - Priority Indicator
  - Quick Actions (Edit, Complete, Postpone, Cancel)

- **Details Tab**
  - Description
  - Category
  - Scheduled Date
  - Due Date
  - Created Date
  - Recurrence Information
  - Assigned Provider (with link to provider profile)
  - Property Information

- **Costs Tab**
  - Estimated Cost
  - Actual Cost (if completed)
  - Cost Variance
  - Historical cost chart (for recurring tasks)

- **Photos Tab**
  - Before/After photo gallery
  - Upload new photos
  - Photo thumbnails with lightbox

- **Notes Tab**
  - Completion notes
  - General notes
  - Add new note with timestamp

- **History Tab**
  - Event timeline (TaskScheduled, TaskPostponed, TaskCompleted, etc.)
  - Timestamp and user information
  - Previous values for changed fields

**Responsive Design**:
- Mobile: Tabs collapse to accordion
- Desktop: Side-by-side layout

### 4. Task Completion Modal

**Purpose**: Mark task as completed with details

**Form Fields**:
- Completion Date (date picker, defaults to today)
- Actual Cost (currency input)
- Duration (time input: hours and minutes)
- Completion Notes (textarea, optional)
- Upload Photos (drag & drop)
- Rate Provider (if assigned, 1-5 stars)

**User Actions**:
- Complete Task (raises TaskCompleted event)
- Cancel

**Success Message**:
"Task completed successfully! [View Details] [Go to Dashboard]"

### 5. Task Postpone Modal

**Purpose**: Reschedule task to a later date

**Form Fields**:
- Current Due Date (read-only, highlighted)
- New Due Date (date picker with time)
- Reason for Postponement (textarea, required)
- Notify Assigned Provider (checkbox, if applicable)

**Validation**:
- New Date: Must be in the future
- Reason: Required, min 10 characters

**Warning Messages**:
- If postponed > 3 times: "This task has been postponed multiple times. Consider cancelling or reassigning."

**User Actions**:
- Postpone Task (raises TaskPostponed event)
- Cancel

### 6. Task Cancellation Modal

**Purpose**: Cancel task with reason

**Form Fields**:
- Cancellation Reason (dropdown + textarea)
  - No longer needed
  - Completed by other means
  - Duplicate task
  - Other (specify)
- Cancel Future Recurrences (checkbox, if recurring)
- Notify Assigned Provider (checkbox, if applicable)

**Warning**:
"Are you sure you want to cancel this task? This action cannot be undone."

**User Actions**:
- Confirm Cancellation (raises TaskCancelled event)
- Keep Task

### 7. Calendar View

**Purpose**: Visual timeline of scheduled tasks

**Features**:
- Month/Week/Day view toggle
- Color-coded tasks by priority or category
- Drag & drop to reschedule
- Click date to create new task
- Click task to view details
- Filter by category, priority, or provider
- Legend for colors

**Calendar Interactions**:
- Hover: Show task tooltip (title, time, provider)
- Click: Open task detail view
- Drag: Reschedule task (confirms before saving)
- Right-click: Context menu (Edit, Complete, Cancel)

### 8. Overdue Tasks Alert

**Purpose**: Prominently display overdue tasks requiring attention

**Display**:
- Alert banner at top of dashboard (red background)
- Count of overdue tasks
- "View Overdue Tasks" button
- Auto-refresh every 5 minutes

**Overdue Task List**:
- Sorted by priority, then days overdue
- Days overdue badge (e.g., "3 days overdue")
- Quick complete button
- Quick postpone button

### 9. Recurring Task Management

**Purpose**: Manage recurring task series

**Features**:
- View all instances in a series
- Edit series or single instance
- Cancel series or future instances
- View completion history
- Modify recurrence pattern

**Edit Options Modal**:
- "Edit this task only"
- "Edit this and future tasks"
- "Edit all tasks in series"

## State Management

### Redux Store Structure

```typescript
interface TaskState {
  tasks: {
    byId: { [id: string]: Task };
    allIds: string[];
  };
  filters: {
    status: TaskStatus[];
    priority: TaskPriority[];
    category: TaskCategory[];
    dateRange: { start: Date; end: Date };
    searchQuery: string;
  };
  sort: {
    field: string;
    direction: 'asc' | 'desc';
  };
  ui: {
    isLoading: boolean;
    error: string | null;
    selectedTaskId: string | null;
    viewMode: 'list' | 'calendar';
  };
  pagination: {
    currentPage: number;
    pageSize: number;
    totalCount: number;
  };
}
```

### Actions

```typescript
// Async Actions (Thunks)
- fetchTasks(filters)
- fetchTaskById(id)
- createTask(taskData)
- updateTask(id, taskData)
- completeTask(id, completionData)
- postponeTask(id, newDueDate, reason)
- cancelTask(id, reason, cancelFutureRecurrences)
- deleteTask(id)

// Sync Actions
- setFilters(filters)
- setSort(field, direction)
- setViewMode(mode)
- selectTask(id)
- clearSelectedTask()
```

## API Integration

### Service Layer

```typescript
class TaskService {
  async getTasks(filters: TaskFilters): Promise<TaskList>;
  async getTaskById(id: string): Promise<Task>;
  async createTask(task: CreateTaskRequest): Promise<Task>;
  async updateTask(id: string, task: UpdateTaskRequest): Promise<Task>;
  async completeTask(id: string, data: CompleteTaskRequest): Promise<void>;
  async postponeTask(id: string, data: PostponeTaskRequest): Promise<void>;
  async cancelTask(id: string, data: CancelTaskRequest): Promise<void>;
  async getOverdueTasks(): Promise<Task[]>;
  async getUpcomingTasks(days: number): Promise<Task[]>;
}
```

### HTTP Client Configuration

```typescript
// Base URL
const API_BASE_URL = process.env.REACT_APP_API_URL;

// Axios interceptors
- Request: Add JWT token to headers
- Response: Handle 401 (refresh token), 403 (unauthorized), 500 (error)
```

## Real-Time Updates

### SignalR Integration

```typescript
// Connect to task hub
const taskHub = new HubConnectionBuilder()
  .withUrl(`${API_BASE_URL}/hubs/tasks`)
  .build();

// Subscribe to events
taskHub.on('TaskScheduled', (task) => {
  dispatch(addTask(task));
  showNotification('New task scheduled', 'success');
});

taskHub.on('TaskCompleted', (taskId, completionDate) => {
  dispatch(updateTaskStatus(taskId, 'Completed'));
  showNotification('Task completed!', 'success');
});

taskHub.on('TaskOverdue', (taskId) => {
  dispatch(updateTaskStatus(taskId, 'Overdue'));
  showNotification('Task is overdue!', 'warning');
});

taskHub.on('TaskPostponed', (taskId, newDueDate) => {
  dispatch(updateTaskDueDate(taskId, newDueDate));
  showNotification('Task postponed', 'info');
});

taskHub.on('TaskCancelled', (taskId) => {
  dispatch(removeTask(taskId));
  showNotification('Task cancelled', 'info');
});
```

## Notifications

### Toast Notifications

**Success Messages**:
- "Task created successfully!"
- "Task completed! Great job!"
- "Task postponed to [new date]"

**Warning Messages**:
- "Task is overdue by [X] days"
- "You have [X] overdue tasks"
- "This task has been postponed [X] times"

**Error Messages**:
- "Failed to create task. Please try again."
- "Failed to complete task. Please check your connection."

**Configuration**:
- Position: Top-right
- Duration: 5 seconds (success/info), 10 seconds (warning), until dismissed (error)
- Auto-dismiss: Yes (except errors)

### Push Notifications

**Browser Notifications** (with user permission):
- Task due in 1 hour
- Task is now overdue
- Recurring task instance created

## Form Validation

### Client-Side Validation

```typescript
const taskValidationSchema = Yup.object({
  title: Yup.string()
    .required('Title is required')
    .max(200, 'Title must be less than 200 characters'),
  description: Yup.string()
    .max(5000, 'Description must be less than 5000 characters'),
  scheduledDate: Yup.date()
    .required('Scheduled date is required'),
  dueDate: Yup.date()
    .required('Due date is required')
    .min(Yup.ref('scheduledDate'), 'Due date must be after scheduled date'),
  priority: Yup.string()
    .required('Priority is required')
    .oneOf(['Low', 'Medium', 'High', 'Critical']),
  category: Yup.string()
    .required('Category is required'),
  estimatedCost: Yup.number()
    .min(0, 'Cost cannot be negative')
    .nullable(),
  recurrenceInterval: Yup.number()
    .when('recurrencePattern', {
      is: (val) => val !== 'None',
      then: Yup.number().required('Interval is required for recurring tasks').min(1),
    }),
});
```

### Error Display

- Inline validation errors (below field)
- Error summary at top of form
- Field highlighting (red border)
- Error icon next to invalid fields

## Accessibility

### WCAG 2.1 Level AA Compliance

**Keyboard Navigation**:
- Tab through all interactive elements
- Enter to activate buttons
- Escape to close modals
- Arrow keys for date picker navigation

**Screen Reader Support**:
- Semantic HTML (header, nav, main, article)
- ARIA labels for icons and status badges
- ARIA live regions for notifications
- Alt text for all images

**Visual Accessibility**:
- Color contrast ratio >= 4.5:1
- Focus indicators (visible outline)
- Text size adjustable
- No information conveyed by color alone

**Form Accessibility**:
- Labels associated with inputs
- Required field indicators
- Error messages announced by screen readers
- Field validation on blur and submit

## Responsive Design

### Breakpoints

```css
/* Mobile */
@media (max-width: 767px) {
  - Single column layout
  - Stacked statistics cards
  - Hamburger menu
  - Full-width forms
  - Bottom navigation bar
}

/* Tablet */
@media (min-width: 768px) and (max-width: 1023px) {
  - Two column layout
  - Side navigation
  - Grid statistics cards (2x2)
}

/* Desktop */
@media (min-width: 1024px) {
  - Multi-column layout
  - Full sidebar navigation
  - Grid statistics cards (1x4)
  - Side-by-side form layout
}
```

### Mobile-Specific Features

- Swipe to complete/postpone task
- Pull to refresh task list
- Bottom sheet for task details
- Floating action button for new task
- Touch-friendly tap targets (min 44px)

## Performance Optimization

### Code Splitting

```typescript
// Lazy load components
const TaskCalendar = lazy(() => import('./components/TaskCalendar'));
const TaskDetail = lazy(() => import('./components/TaskDetail'));
```

### Memoization

```typescript
// Memoize expensive computations
const filteredTasks = useMemo(() => {
  return tasks.filter(task => matchesFilters(task, filters));
}, [tasks, filters]);

// Memoize components
const TaskListItem = React.memo(({ task }) => {
  // Component implementation
});
```

### Virtual Scrolling

- Implement virtual scrolling for large task lists (>100 items)
- Use react-window or react-virtualized
- Render only visible items + buffer

### Caching

- Cache task list with React Query (stale time: 5 minutes)
- Invalidate cache on mutations
- Background refetch on focus

## Error Handling

### Error Boundaries

```typescript
class TaskErrorBoundary extends React.Component {
  componentDidCatch(error, errorInfo) {
    logErrorToService(error, errorInfo);
  }

  render() {
    if (this.state.hasError) {
      return <ErrorFallback />;
    }
    return this.props.children;
  }
}
```

### Retry Logic

- Auto-retry failed requests (max 3 attempts)
- Exponential backoff
- Manual retry button for failures

### Offline Support

- Show offline indicator
- Queue mutations when offline
- Sync when connection restored
- Optimistic updates with rollback

## Testing Requirements

### Unit Tests
- Component rendering
- Event handlers
- Form validation
- Redux reducers and actions
- Service layer methods

### Integration Tests
- User workflows (create, complete, postpone task)
- Form submission
- API integration
- Real-time updates

### E2E Tests (Cypress/Playwright)
- Complete user journey: login -> create task -> complete task
- Calendar view interactions
- Filter and search functionality
- Mobile responsive behavior

## UI Component Library

### Recommended Libraries

- **UI Framework**: Material-UI or Ant Design
- **Forms**: React Hook Form + Yup
- **Date Picker**: react-datepicker or @mui/x-date-pickers
- **Notifications**: react-toastify
- **Calendar**: FullCalendar or react-big-calendar
- **Drag & Drop**: react-beautiful-dnd
- **Charts**: Recharts or Chart.js

## Design System

### Colors

```css
/* Primary Colors */
--primary-main: #1976d2;
--primary-light: #42a5f5;
--primary-dark: #1565c0;

/* Status Colors */
--status-scheduled: #2196f3;
--status-inprogress: #ff9800;
--status-completed: #4caf50;
--status-overdue: #f44336;
--status-cancelled: #9e9e9e;

/* Priority Colors */
--priority-low: #4caf50;
--priority-medium: #ff9800;
--priority-high: #f44336;
--priority-critical: #9c27b0;
```

### Typography

```css
/* Font Family */
--font-family: 'Roboto', 'Helvetica', 'Arial', sans-serif;

/* Font Sizes */
--text-xs: 0.75rem;
--text-sm: 0.875rem;
--text-base: 1rem;
--text-lg: 1.125rem;
--text-xl: 1.25rem;
--text-2xl: 1.5rem;
```

### Spacing

```css
/* Spacing Scale */
--space-1: 0.25rem;
--space-2: 0.5rem;
--space-3: 0.75rem;
--space-4: 1rem;
--space-6: 1.5rem;
--space-8: 2rem;
```

## Analytics Tracking

### Events to Track

```typescript
// Track user interactions
analytics.track('Task Created', {
  category: task.category,
  priority: task.priority,
  isRecurring: task.recurrencePattern !== 'None',
});

analytics.track('Task Completed', {
  taskId: task.id,
  daysToComplete: calculateDays(task.scheduledDate, task.completedDate),
  completedOnTime: task.completedDate <= task.dueDate,
});

analytics.track('Task Postponed', {
  taskId: task.id,
  postponementCount: task.postponementCount,
  daysPosponedBy: calculateDays(task.originalDueDate, task.newDueDate),
});
```

## Documentation

- Component Storybook
- User guide with screenshots
- Developer README
- API integration guide
- Deployment checklist
