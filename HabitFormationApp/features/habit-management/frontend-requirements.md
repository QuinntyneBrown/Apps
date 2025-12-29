# Habit Management - Frontend Requirements

## Overview
The Habit Management UI provides an intuitive interface for users to create, edit, archive, and reactivate their habits.

## User Interface Components

### 1. Habit List View
**Purpose**: Display all active habits for the user

**Components:**
- Habit cards showing:
  - Habit name and icon
  - Category badge
  - Current streak
  - Frequency indicator
  - Quick complete button
  - Edit/Archive actions
- Filter bar (All, Category-specific)
- Sort options (Name, Created Date, Streak, Category)
- Search functionality
- "Create New Habit" button

**Behavior:**
- Real-time updates when habits are modified
- Smooth animations for state changes
- Empty state with onboarding for new users
- Loading skeletons during data fetch

### 2. Create Habit Form
**Purpose**: Allow users to create new habits

**Form Fields:**
- **Name** (required, text input, max 100 chars)
  - Real-time character counter
  - Auto-suggestions based on common habits
- **Description** (optional, textarea, max 500 chars)
  - Character counter
- **Category** (required, dropdown)
  - Predefined: Health, Fitness, Productivity, Learning, Wellness, Social, Finance
  - Custom option
- **Frequency** (required, radio buttons)
  - Daily
  - Weekly (with day selector)
  - Custom (advanced frequency builder)
- **Start Date** (required, date picker)
  - Default: today
  - Allow backdating up to 30 days
- **Target Duration** (optional, number input)
  - In days
  - Helper text: "Leave empty for ongoing habit"
- **Enable Reminders** (toggle switch)
  - If enabled, show reminder settings

**Validation:**
- Client-side validation with inline error messages
- Submit button disabled until form is valid
- Duplicate name warning

**Behavior:**
- Modal or side panel display
- Auto-save to draft every 30 seconds
- Cancel confirmation if form has changes
- Success toast notification on creation
- Redirect to habit detail or back to list

### 3. Edit Habit Form
**Purpose**: Modify existing habit details

**Similar to Create form but:**
- Pre-populated with current values
- Cannot change StartDate
- Warning message if changing frequency affects streak
- "Save Changes" button
- "Cancel" button with unsaved changes warning

### 4. Habit Detail View
**Purpose**: Show comprehensive information about a single habit

**Sections:**
- Header:
  - Habit name and category
  - Edit and Archive buttons
  - Back button
- Overview card:
  - Description
  - Frequency details
  - Start date
  - Target duration (if set)
  - Current streak
- Recent activity (last 7 days)
- Completion history calendar
- Statistics summary
- Related insights

### 5. Archive Confirmation Dialog
**Purpose**: Confirm habit archival

**Components:**
- Warning message about archival
- Optional reason input
- "Archive" button (destructive style)
- "Cancel" button
- Information about data retention

### 6. Archived Habits View
**Purpose**: Show archived habits with option to reactivate

**Components:**
- List of archived habits
- Archive date
- Archive reason (if provided)
- "Reactivate" button per habit
- Permanent delete option (if no completion history)

### 7. Reactivate Habit Dialog
**Purpose**: Confirm habit reactivation

**Components:**
- Information message
- New start date picker (default: today)
- Warning about streak reset
- "Reactivate" button
- "Cancel" button

## State Management

### Habit Store/Context
```typescript
interface HabitState {
  habits: Habit[];
  archivedHabits: Habit[];
  selectedHabit: Habit | null;
  loading: boolean;
  error: string | null;
  filters: {
    category: string | null;
    searchQuery: string;
    sortBy: 'name' | 'created' | 'streak';
    sortOrder: 'asc' | 'desc';
  };
}

interface Habit {
  id: string;
  userId: string;
  name: string;
  description: string;
  frequency: FrequencyType;
  frequencyDetails: FrequencyDetails;
  category: string;
  startDate: string;
  targetDuration?: number;
  reminderEnabled: boolean;
  isArchived: boolean;
  currentStreak: number;
  createdAt: string;
  updatedAt: string;
  archivedAt?: string;
}
```

### Actions
- `fetchHabits(userId: string)`
- `createHabit(data: CreateHabitData)`
- `updateHabit(id: string, data: UpdateHabitData)`
- `archiveHabit(id: string, reason?: string)`
- `reactivateHabit(id: string, startDate: string)`
- `selectHabit(id: string)`
- `setFilters(filters: Partial<Filters>)`

## API Integration

### Service Layer
```typescript
class HabitService {
  async getHabits(userId: string): Promise<Habit[]>
  async getHabitById(id: string): Promise<Habit>
  async createHabit(data: CreateHabitDto): Promise<Habit>
  async updateHabit(id: string, data: UpdateHabitDto): Promise<Habit>
  async archiveHabit(id: string, reason?: string): Promise<void>
  async reactivateHabit(id: string, startDate: string): Promise<Habit>
  async getArchivedHabits(userId: string): Promise<Habit[]>
}
```

## Routing

- `/habits` - Habit list view (default)
- `/habits/new` - Create habit form
- `/habits/:id` - Habit detail view
- `/habits/:id/edit` - Edit habit form
- `/habits/archived` - Archived habits view

## User Interactions & Feedback

### Success Messages
- "Habit created successfully!"
- "Habit updated successfully!"
- "Habit archived"
- "Habit reactivated!"

### Error Messages
- "Failed to create habit. Please try again."
- "This habit name already exists"
- "You've reached the maximum number of habits"
- "Network error. Changes not saved."

### Loading States
- Skeleton loaders for habit cards
- Button loading indicators
- Full-page loader for initial load
- Optimistic updates for better UX

### Animations
- Fade in for new habits
- Slide out for archived habits
- Smooth transitions between views
- Micro-interactions on buttons

## Accessibility Requirements
- ARIA labels on all interactive elements
- Keyboard navigation support
- Focus management in modals
- Screen reader announcements for state changes
- Color contrast compliance (WCAG AA)
- Form labels and error associations
- Skip navigation links

## Responsive Design

### Mobile (< 768px)
- Single column layout
- Bottom sheet for create/edit forms
- Simplified habit cards
- Hamburger menu
- Touch-friendly buttons (min 44px)

### Tablet (768px - 1024px)
- Two-column habit grid
- Side panel for forms
- Adaptive navigation

### Desktop (> 1024px)
- Three-column habit grid
- Modal dialogs for forms
- Persistent navigation
- Advanced filters visible

## Performance Optimization
- Lazy load archived habits view
- Virtual scrolling for 100+ habits
- Image optimization for category icons
- Debounced search input
- Memoized habit list rendering
- Code splitting by route

## Browser Support
- Chrome (last 2 versions)
- Firefox (last 2 versions)
- Safari (last 2 versions)
- Edge (last 2 versions)
- Mobile Safari (iOS 13+)
- Chrome Mobile (Android 8+)

## Third-Party Libraries
- Date picker: react-datepicker or native
- Form validation: react-hook-form + zod
- UI components: Tailwind CSS or Material-UI
- State management: Redux Toolkit or Zustand
- API client: Axios or Fetch API
- Icons: react-icons or heroicons

## Testing Requirements
- Unit tests for all components
- Integration tests for forms
- E2E tests for critical flows:
  - Create habit flow
  - Edit habit flow
  - Archive/reactivate flow
- Accessibility tests with jest-axe
- Visual regression tests for UI components

## Analytics & Tracking
- Track habit creation events
- Track category popularity
- Track feature usage (archive, edit)
- Track form abandonment rate
- Track time to create habit
- Track errors and failures
