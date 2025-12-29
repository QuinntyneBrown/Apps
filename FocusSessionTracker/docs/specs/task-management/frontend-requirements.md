# Task Management - Frontend Requirements

## Overview
User interface components for creating, managing, and tracking tasks across multiple focus sessions, with progress visualization and completion workflows.

---

## Pages

### Task List Page (`/tasks`)
**Purpose**: Main task management dashboard

**Components**:
- Task List with filters and sorting
- Quick Add Task Button
- Status Filter Tabs (All, Active, Completed)
- Priority Filter Chips
- Search Bar
- Progress Summary Card

**States**:
- Empty (no tasks created)
- Loading (fetching tasks)
- Populated (tasks displayed)
- Filtered (subset of tasks)

---

### Task Detail Page (`/tasks/:id`)
**Purpose**: View and manage individual task

**Components**:
- Task Header (title, status, priority)
- Progress Bar with percentage
- Session History Timeline
- Quick Actions (Edit, Archive, Assign to Session)
- Progress Notes List
- Time Tracking Summary
- Related Project Link

---

### Create/Edit Task Page (`/tasks/new` or `/tasks/:id/edit`)
**Purpose**: Create new task or edit existing

**Components**:
- Task Form
- Title Input
- Description Textarea
- Priority Selector
- Estimated Sessions Input
- Due Date Picker
- Tag Manager
- Project Selector
- Save/Cancel Buttons

---

## Components

### TaskCard
```typescript
interface TaskCardProps {
  task: Task;
  onSelect: (taskId: string) => void;
  onAssignToSession?: (taskId: string) => void;
  onEdit?: (taskId: string) => void;
  compact?: boolean;
}
```

**Display**:
- Task title (truncated if long)
- Status badge
- Priority indicator (color-coded)
- Progress bar with percentage
- Sessions completed / estimated
- Due date (if set, with warning if near)
- Tags (first 3, with +N more)
- Quick action buttons

**Visual Requirements**:
- Card with rounded corners, shadow on hover
- Color-coded left border for priority (red/yellow/green)
- Smooth progress bar animation
- Responsive layout (stacks on mobile)

---

### TaskForm
```typescript
interface TaskFormProps {
  task?: Task;
  onSave: (taskData: TaskData) => void;
  onCancel: () => void;
  projects: Project[];
}
```

**Fields**:
- Title (required, max 200 chars, counter shown)
- Description (optional, max 2000 chars, rich text)
- Priority selector (radio buttons: High/Medium/Low)
- Estimated sessions (number input, 1-100)
- Due date (date picker, must be future)
- Tags (multi-input with autocomplete)
- Project (dropdown, optional)

**Validation**:
- Title required
- Due date must be in future
- Estimated sessions 1-100 if provided
- Real-time validation feedback

---

### ProgressBar
```typescript
interface ProgressBarProps {
  percentage: number;
  showLabel?: boolean;
  size?: 'small' | 'medium' | 'large';
  animated?: boolean;
}
```

**Visual Requirements**:
- Smooth gradient fill (0-100%)
- Color changes: 0-33% (red), 34-66% (yellow), 67-100% (green)
- Percentage label centered (if showLabel=true)
- Animation on percentage change
- Sizes: small (8px), medium (12px), large (20px)

---

### SessionTimeline
```typescript
interface SessionTimelineProps {
  taskId: string;
  sessions: TaskSession[];
  onSessionClick: (sessionId: string) => void;
}
```

**Display**:
- Vertical timeline of all sessions
- Each node shows:
  - Date/time
  - Duration
  - Progress made during session
  - Session quality rating
  - Expandable progress notes
- Visual connector lines
- "Current progress" indicator at bottom

---

### TaskStatusBadge
```typescript
interface TaskStatusBadgeProps {
  status: 'not_started' | 'in_progress' | 'completed' | 'archived';
  size?: 'small' | 'medium';
}
```

**Visual Requirements**:
- not_started: Gray background, "Not Started"
- in_progress: Blue background, "In Progress"
- completed: Green background, "Completed"
- archived: Dark gray, "Archived"
- Rounded pill shape
- Icon + text

---

### TaskFilters
```typescript
interface TaskFiltersProps {
  filters: TaskFilters;
  onChange: (filters: TaskFilters) => void;
  onReset: () => void;
}
```

**Controls**:
- Status tabs (All/Active/Completed/Archived)
- Priority checkboxes (High/Medium/Low)
- Project dropdown
- Tag filter (multi-select)
- Date range picker (created/due)
- Sort dropdown (Priority/Due Date/Progress/Created)
- Clear all filters button

---

### AssignTaskModal
```typescript
interface AssignTaskModalProps {
  task: Task;
  currentSession: Session | null;
  onAssign: (taskId: string, sessionId: string) => void;
  onCancel: () => void;
}
```

**Display**:
- Task summary (title, current progress)
- Current session info (if active)
- Assign to current session button
- Start new session with task button
- Cancel button

---

### TaskCompletionModal
```typescript
interface TaskCompletionModalProps {
  task: Task;
  onConfirm: (notes: string) => void;
  onCancel: () => void;
}
```

**Display**:
- Congratulations message
- Task summary (title, total time, sessions)
- Completion notes textarea (optional)
- Confetti animation
- Complete button
- View task button

---

## User Flows

### Create Task Flow
1. User clicks "New Task" button
2. Create Task page loads
3. User fills in title (required)
4. User optionally adds:
   - Description
   - Priority (defaults to medium)
   - Estimated sessions
   - Due date
   - Tags
   - Project
5. User clicks "Save"
6. Validation runs
7. Task created via API
8. Success message shown
9. Redirect to task detail page

### Assign Task to Session Flow
1. User starts new focus session
2. Session setup modal shows task selector
3. User searches/filters tasks
4. User selects task from list
5. Task assigned to session
6. Session starts with task context
7. Timer shows task name
8. User can update progress during session

### Update Task Progress Flow
1. User completes focus session
2. Session completion modal appears
3. If task assigned:
   - Progress notes field shown
   - User adds notes about progress made
   - User optionally updates progress % slider
4. User clicks "Complete Session"
5. Task progress updated via API
6. Task card updates with new progress
7. If progress reaches 100%:
   - Task Completion Modal appears
   - Celebration animation plays

### Complete Multi-Session Task Flow
1. Task progress reaches 100% (manual or automatic)
2. Task Completion Modal appears
3. Shows task summary statistics
4. User adds optional completion notes
5. User clicks "Mark as Complete"
6. Task status changes to completed
7. Success notification shown
8. Optional: Prompt to archive task

### Filter Tasks Flow
1. User lands on Task List page
2. User clicks status tab (e.g., "Active")
3. List filters to show only active tasks
4. User clicks priority filter (e.g., "High")
5. List further filters to high priority active tasks
6. User searches by title
7. List shows matching tasks
8. User clicks "Clear filters"
9. All tasks shown again

---

## State Management

### Task Store
```typescript
interface TaskState {
  tasks: Task[];
  currentTask: Task | null;
  filters: TaskFilters;
  sortBy: SortOption;
  loading: boolean;
  error: string | null;
}

interface TaskFilters {
  status?: TaskStatus[];
  priority?: Priority[];
  projectId?: string;
  tags?: string[];
  searchQuery?: string;
  dateRange?: { start: Date; end: Date };
}
```

### Actions
- `createTask(taskData: TaskData)`
- `updateTask(taskId: string, taskData: Partial<TaskData>)`
- `deleteTask(taskId: string)`
- `assignTaskToSession(taskId: string, sessionId: string)`
- `updateTaskProgress(taskId: string, progress: ProgressUpdate)`
- `completeTask(taskId: string, notes?: string)`
- `archiveTask(taskId: string)`
- `fetchTasks(filters?: TaskFilters)`
- `fetchTaskById(taskId: string)`
- `setFilters(filters: TaskFilters)`
- `setSortBy(sortOption: SortOption)`

---

## Validation Rules

| Field | Rule | Error Message |
|-------|------|---------------|
| Title | 3-200 chars, required | "Title must be 3-200 characters" |
| Description | Max 2000 chars | "Description too long (max 2000)" |
| EstimatedSessions | 1-100 if provided | "Must be between 1 and 100" |
| DueDate | Future date if provided | "Due date must be in the future" |
| Priority | One of: high/medium/low | "Invalid priority" |
| Tags | Max 10, each 2-50 chars | "Max 10 tags, 2-50 chars each" |

---

## Accessibility Requirements

- All form inputs have labels
- Error messages announced to screen readers
- Keyboard navigation for all interactive elements
- Focus indicators visible
- ARIA labels for icon-only buttons
- Status changes announced
- Progress bar has aria-valuenow/valuemin/valuemax
- Color is not the only indicator (use icons + text)
- Skip links for keyboard users
- Sufficient color contrast (WCAG AA)

---

## Responsive Design

### Mobile (<768px)
- Stack task cards vertically
- Single column layout
- Simplified filters (drawer/modal)
- Floating action button for "New Task"
- Swipe actions on task cards (archive, edit)
- Bottom sheet for quick actions

### Tablet (768-1024px)
- Two column task grid
- Side filters panel (collapsible)
- Modal forms
- Larger touch targets

### Desktop (>1024px)
- Three column task grid
- Persistent filters sidebar
- Inline task editing
- Hover effects and tooltips
- Keyboard shortcuts displayed
- Multi-select support

---

## Keyboard Shortcuts

| Shortcut | Action |
|----------|--------|
| N | New task |
| / | Focus search |
| F | Toggle filters |
| 1-3 | Switch status tabs |
| Enter | Open selected task |
| E | Edit selected task |
| Del | Archive selected task |
| Esc | Close modal/cancel |
| Arrow keys | Navigate task list |

---

## Performance Optimization

- Virtual scrolling for long task lists (>50 items)
- Debounced search input (300ms)
- Pagination or infinite scroll
- Optimistic UI updates
- Cache task list data
- Lazy load task details
- Compress large descriptions
- Image optimization for project icons

---

## Animation & Transitions

- Task card appearance: fade + slide up (200ms)
- Progress bar fill: smooth ease-out (500ms)
- Status change: scale + color transition (300ms)
- Filter toggle: slide down (200ms)
- Modal open/close: fade + scale (250ms)
- Completion celebration: confetti burst + bounce
- Hover effects: transform translateY(-2px)
- Loading states: skeleton screens

---

## Error Handling

| Scenario | User Feedback |
|----------|--------------|
| Network error | Toast: "Connection lost. Changes saved locally." |
| Validation error | Inline field error messages |
| Task not found | Empty state with "Create task" CTA |
| Duplicate title | Warning: "Similar task exists. Continue?" |
| Save failed | Toast: "Save failed. Retry?" with retry button |
| Delete failed | Toast: "Delete failed. Try again." |

---

## Empty States

### No Tasks Created
```
[Icon: Checklist]
No tasks yet
Create your first task to get started
[Create Task Button]
```

### No Active Tasks
```
[Icon: Checkmark]
All caught up!
No active tasks. Great work!
[View Completed Tasks]
```

### No Search Results
```
[Icon: Search]
No tasks found
Try adjusting your filters or search
[Clear Filters Button]
```
