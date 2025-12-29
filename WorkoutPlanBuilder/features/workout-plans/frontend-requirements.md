# Workout Plans - Frontend Requirements

## Overview
The Workout Plans frontend provides an intuitive interface for users to create, manage, and execute structured workout routines. The interface emphasizes ease of use during workout sessions and comprehensive plan management.

## Pages/Views

### 1. Workout Plans List View
**Route:** `/workout-plans`

**Purpose:** Display all workout plans for the current user with filtering and status indicators.

**Components:**
- WorkoutPlansList (container)
- WorkoutPlanCard (presentational)
- StatusFilter (presentational)
- SearchBar (presentational)
- CreatePlanButton (presentational)

**Features:**
- Grid/List view toggle
- Filter by status (All, Draft, Active, Completed, Archived)
- Search by plan name
- Sort by date created, name, duration
- Quick actions: Start, Edit, Delete, Clone
- Status badges with color coding
- Progress indicators for active plans
- Create new plan button (prominent)

**State Management:**
```typescript
interface WorkoutPlansState {
  plans: WorkoutPlanSummary[];
  loading: boolean;
  error: string | null;
  filter: {
    status: PlanStatus | 'all';
    searchTerm: string;
    sortBy: 'name' | 'createdDate' | 'duration';
    sortOrder: 'asc' | 'desc';
  };
  viewMode: 'grid' | 'list';
}

interface WorkoutPlanSummary {
  id: string;
  name: string;
  description: string;
  status: PlanStatus;
  durationWeeks: number;
  totalWorkouts: number;
  createdDate: Date;
  startedDate?: Date;
  completedDate?: Date;
  completionPercentage: number;
}

enum PlanStatus {
  Draft = 'draft',
  Active = 'active',
  Completed = 'completed',
  Archived = 'archived'
}
```

**API Integration:**
- GET `/api/workout-plans` with query params
- DELETE `/api/workout-plans/{id}`

### 2. Create/Edit Workout Plan View
**Route:** `/workout-plans/create` or `/workout-plans/{id}/edit`

**Purpose:** Multi-step form for creating or editing a workout plan.

**Components:**
- WorkoutPlanForm (container)
- PlanDetailsStep (form section)
- WorkoutsStep (form section)
- WorkoutBuilder (sub-component)
- ExerciseSelector (modal)
- ReviewStep (summary)

**Form Steps:**
1. **Plan Details**
   - Plan name (required)
   - Description
   - Duration (weeks)
   - Public/Private toggle

2. **Add Workouts**
   - Add multiple workouts
   - Each workout has:
     - Name
     - Day of week (optional)
     - Exercise list with drag-drop ordering
     - Target sets/reps/weight for each exercise
     - Rest periods

3. **Review & Save**
   - Summary of plan
   - Estimated total duration
   - Save as draft or activate

**State Management:**
```typescript
interface WorkoutPlanFormState {
  currentStep: number;
  planDetails: {
    name: string;
    description: string;
    durationWeeks: number;
    isPublic: boolean;
  };
  workouts: WorkoutForm[];
  validationErrors: Record<string, string>;
  saving: boolean;
}

interface WorkoutForm {
  id: string; // temporary ID for form
  name: string;
  dayOfWeek?: DayOfWeek;
  orderIndex: number;
  exercises: WorkoutExerciseForm[];
}

interface WorkoutExerciseForm {
  exerciseId: string;
  exerciseName: string;
  orderIndex: number;
  targetSets: number;
  targetReps: string;
  targetWeight?: number;
  weightUnit?: 'pounds' | 'kilograms';
  restSeconds: number;
  notes: string;
}
```

**Features:**
- Form validation with real-time feedback
- Auto-save to local storage
- Exercise search and selection
- Drag-and-drop exercise ordering
- Quick exercise substitution
- Duplicate workout within plan
- Calculate total plan duration
- Preview mode

**API Integration:**
- POST `/api/workout-plans`
- PUT `/api/workout-plans/{id}`
- GET `/api/exercises` (for exercise selector)

### 3. Workout Plan Detail View
**Route:** `/workout-plans/{id}`

**Purpose:** Display complete plan details with options to start, edit, or manage the plan.

**Components:**
- WorkoutPlanDetail (container)
- PlanHeader (info section)
- WorkoutList (expandable list)
- WorkoutCard (individual workout)
- ExerciseList (exercise details)
- ActionButtons (start, edit, delete, clone)
- ProgressSection (for active plans)

**Features:**
- Full plan overview
- Expandable workout sections
- Exercise details with images
- Start plan button (if not started)
- Edit button (if draft or active)
- Clone plan option
- Share plan (if public)
- Print-friendly view
- Progress tracking (for active plans)
- Session history (if any sessions logged)

**State Management:**
```typescript
interface WorkoutPlanDetailState {
  plan: WorkoutPlanDetail | null;
  loading: boolean;
  error: string | null;
  expandedWorkouts: Set<string>;
  showDeleteConfirm: boolean;
}

interface WorkoutPlanDetail {
  id: string;
  name: string;
  description: string;
  userId: string;
  createdDate: Date;
  modifiedDate?: Date;
  status: PlanStatus;
  durationWeeks: number;
  isPublic: boolean;
  startedDate?: Date;
  completedDate?: Date;
  workouts: WorkoutDetail[];
  totalWorkouts: number;
  estimatedTotalDuration: number; // minutes
}

interface WorkoutDetail {
  id: string;
  name: string;
  dayOfWeek?: DayOfWeek;
  orderIndex: number;
  estimatedDuration: number; // minutes
  exercises: WorkoutExerciseDetail[];
}

interface WorkoutExerciseDetail {
  id: string;
  exerciseId: string;
  exerciseName: string;
  orderIndex: number;
  targetSets: number;
  targetReps: string;
  targetWeight?: number;
  weightUnit?: string;
  restSeconds: number;
  notes: string;
}
```

**API Integration:**
- GET `/api/workout-plans/{id}`
- POST `/api/workout-plans/{id}/start`
- POST `/api/workout-plans/{id}/complete`
- DELETE `/api/workout-plans/{id}`

### 4. Public Plans Browser
**Route:** `/workout-plans/browse`

**Purpose:** Browse and discover public workout plans shared by other users.

**Components:**
- PublicPlansBrowser (container)
- PlanCard (presentational)
- FilterSidebar (filters)
- SearchBar (search)

**Features:**
- Search public plans
- Filter by duration, difficulty, type
- Preview plan details
- Clone plan to own library
- Rating/review system (future)
- Popular plans section
- Category browsing

**State Management:**
```typescript
interface PublicPlansState {
  plans: WorkoutPlanSummary[];
  loading: boolean;
  error: string | null;
  searchTerm: string;
  filters: {
    minDuration?: number;
    maxDuration?: number;
  };
  pagination: {
    currentPage: number;
    pageSize: number;
    totalCount: number;
  };
}
```

**API Integration:**
- GET `/api/workout-plans/public`

## Reusable Components

### WorkoutPlanCard
**Props:**
```typescript
interface WorkoutPlanCardProps {
  plan: WorkoutPlanSummary;
  onStart: (id: string) => void;
  onEdit: (id: string) => void;
  onDelete: (id: string) => void;
  onClone: (id: string) => void;
  showActions?: boolean;
}
```

**Features:**
- Displays plan summary info
- Status badge
- Progress bar (for active plans)
- Quick action buttons
- Click to view details

### ExerciseSelector
**Props:**
```typescript
interface ExerciseSelectorProps {
  isOpen: boolean;
  onClose: () => void;
  onSelect: (exercise: Exercise) => void;
  selectedExercises: string[]; // IDs already in workout
}
```

**Features:**
- Modal dialog
- Search exercises
- Filter by muscle group, equipment
- Exercise preview with image
- Multi-select option
- Recently used exercises

### WorkoutBuilder
**Props:**
```typescript
interface WorkoutBuilderProps {
  workout: WorkoutForm;
  onChange: (workout: WorkoutForm) => void;
  onRemove: () => void;
}
```

**Features:**
- Add/remove exercises
- Drag-drop reordering
- Set targets for each exercise
- Duplicate exercises
- Calculate workout duration

## State Management

### Global State (Redux/Zustand)
```typescript
// Store structure
{
  workoutPlans: {
    list: WorkoutPlanSummary[];
    current: WorkoutPlanDetail | null;
    loading: boolean;
    error: string | null;
  },
  ui: {
    viewMode: 'grid' | 'list';
    filters: FilterState;
    modals: {
      exerciseSelector: boolean;
      deleteConfirm: boolean;
    };
  }
}
```

### Actions
```typescript
// Action creators
loadWorkoutPlans(filters?: FilterParams)
loadWorkoutPlan(id: string)
createWorkoutPlan(plan: CreatePlanDto)
updateWorkoutPlan(id: string, plan: UpdatePlanDto)
deleteWorkoutPlan(id: string)
startWorkoutPlan(id: string)
completeWorkoutPlan(id: string)
clonWorkoutPlan(id: string)
```

## Validation Rules

### Plan Details
- Name: Required, 1-200 characters
- Description: Optional, max 2000 characters
- Duration: Required, 1-52 weeks
- Must have at least one workout

### Workout
- Name: Required, 1-200 characters
- Must have at least one exercise
- Day of week: Optional
- Order: Auto-assigned, sequential

### Exercise in Workout
- Exercise: Required (select from library)
- Sets: Required, 1-20
- Reps: Required, valid format ("8-12", "15", "AMRAP")
- Weight: Optional, positive number
- Rest: Required, 0-600 seconds

## Error Handling

### API Errors
- Network errors: Show retry button
- 404: Plan not found, redirect to list
- 403: Unauthorized, show message
- 400: Validation errors, highlight fields
- 500: Server error, show friendly message

### Form Errors
- Display inline validation errors
- Scroll to first error on submit
- Disable submit button when invalid
- Show summary of errors at top

## Loading States

- Skeleton loaders for plan cards
- Spinner for form submission
- Progress bar for plan operations
- Optimistic UI updates where possible

## Responsive Design

### Mobile (< 768px)
- Single column layout
- Simplified card view
- Bottom sheet for actions
- Collapsible filters
- Swipe gestures for actions

### Tablet (768px - 1024px)
- Two column grid
- Side drawer for filters
- Larger touch targets

### Desktop (> 1024px)
- Three+ column grid
- Side panel navigation
- Hover interactions
- Keyboard shortcuts

## Accessibility

- ARIA labels on all interactive elements
- Keyboard navigation support
- Focus indicators
- Screen reader announcements for actions
- Color contrast compliance (WCAG AA)
- Form field labels and descriptions

## Performance Optimizations

- Lazy load plan details
- Virtual scrolling for large lists
- Debounce search input
- Cache API responses
- Memoize expensive calculations
- Code splitting by route
- Image lazy loading

## User Experience Features

### Auto-save
- Save form progress to localStorage
- Restore on page reload
- Clear on successful submit
- Show save indicator

### Notifications
- Success messages for actions
- Error toasts for failures
- Loading indicators
- Confirmation dialogs for destructive actions

### Shortcuts
- Ctrl/Cmd + N: New plan
- Ctrl/Cmd + S: Save plan
- Esc: Close modals
- Enter: Submit forms

### Drag and Drop
- Reorder exercises in workout
- Reorder workouts in plan
- Visual feedback during drag
- Drop zones highlighted

## Analytics Events

Track user interactions:
- `plan_created`: When user creates a plan
- `plan_started`: When user starts a plan
- `plan_completed`: When user completes a plan
- `plan_deleted`: When user deletes a plan
- `plan_cloned`: When user clones a plan
- `plan_viewed`: When user views plan details
- `workout_added`: When workout added to plan
- `exercise_added`: When exercise added to workout

## Testing Requirements

### Unit Tests
- Component rendering
- Event handlers
- State updates
- Validation logic
- Utility functions

### Integration Tests
- Form submission flows
- API integration
- State management
- Routing

### E2E Tests
- Create plan flow
- Edit plan flow
- Start/complete plan
- Delete plan
- Browse public plans

## Dependencies

```json
{
  "react": "^18.0.0",
  "react-router-dom": "^6.0.0",
  "axios": "^1.0.0",
  "@tanstack/react-query": "^5.0.0",
  "zustand": "^4.0.0",
  "react-hook-form": "^7.0.0",
  "zod": "^3.0.0",
  "@dnd-kit/core": "^6.0.0",
  "@dnd-kit/sortable": "^7.0.0",
  "date-fns": "^2.0.0",
  "lucide-react": "^0.200.0"
}
```
