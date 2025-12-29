# Workout Sessions - Frontend Requirements

## Overview
The Workout Sessions frontend provides a real-time interface for logging workout sessions with timer, set tracking, and quick data entry optimized for use during workouts.

## Pages/Views

### 1. Active Session View
**Route:** `/workout-sessions/active`

**Purpose:** Primary interface for logging exercises during an active workout session.

**Features:**
- Session timer (elapsed time)
- Exercise list with current targets
- Quick set logging with keyboard shortcuts
- Rest timer between sets
- Real-time progress tracking
- Notes for each set
- Session summary statistics
- Quick exercise substitution

**State Management:**
```typescript
interface ActiveSessionState {
  session: {
    id: string;
    workoutName: string;
    startTime: Date;
    currentExercise: number;
  };
  exercises: ExerciseInSession[];
  timer: {
    elapsed: number;
    restRemaining: number;
    isResting: boolean;
  };
  quickEntry: {
    weight: number;
    reps: number;
    unit: 'pounds' | 'kilograms';
  };
}
```

### 2. Session History View
**Route:** `/workout-sessions/history`

**Purpose:** View past workout sessions with filtering and analytics.

**Features:**
- Calendar view of sessions
- List/Grid view toggle
- Filter by date range, status, plan
- Session cards with summary stats
- Quick view session details
- Export session data
- Compare sessions

### 3. Session Detail View
**Route:** `/workout-sessions/{id}`

**Purpose:** Detailed view of a completed or in-progress session.

**Features:**
- Full session information
- Exercise breakdown with all sets
- Charts (volume per exercise, reps distribution)
- Personal records highlighted
- Notes and observations
- Print/Export options

## Components

### SessionTimer
```typescript
interface SessionTimerProps {
  startTime: Date;
  onPause: () => void;
  onResume: () => void;
  onComplete: () => void;
}
```

### ExerciseLogger
```typescript
interface ExerciseLoggerProps {
  exercise: Exercise;
  targetSets: number;
  targetReps: string;
  completedSets: ExerciseSet[];
  onLogSet: (set: SetData) => void;
  onRest: (duration: number) => void;
}
```

### RestTimer
```typescript
interface RestTimerProps {
  duration: number;
  onComplete: () => void;
  onSkip: () => void;
  onAddTime: (seconds: number) => void;
}
```

### QuickSetEntry
```typescript
interface QuickSetEntryProps {
  defaultWeight: number;
  defaultReps: number;
  onSubmit: (weight: number, reps: number) => void;
  previousSet?: ExerciseSet;
}
```

## Keyboard Shortcuts

- **Enter**: Log current set with displayed values
- **Ctrl/Cmd + Enter**: Log set and start rest timer
- **Up/Down Arrows**: Adjust reps
- **Left/Right Arrows**: Adjust weight
- **Space**: Skip rest timer
- **Esc**: Cancel current entry
- **N**: Add note to current set
- **S**: Substitute exercise

## Real-time Features

### Auto-save
- Save sets immediately after logging
- Sync to server every 5 seconds
- Local storage backup
- Offline support with sync on reconnect

### Live Statistics
- Running totals (volume, sets, duration)
- PR indicators when weight/reps exceed previous best
- Session comparison to previous similar workout
- Estimated calories burned

## Validation

- Reps: 1-1000
- Weight: 0-10000
- Rest time: 0-600 seconds
- Exercise must be from plan or library
- Cannot log sets without active session

## Error Handling

- Network errors: Queue sets locally, retry on reconnect
- Server errors: Show message, allow retry
- Validation errors: Highlight fields with messages
- Session timeout: Prompt to resume or abandon

## Performance Optimizations

- Virtualize exercise list for long workouts
- Debounce weight/rep adjustments
- Lazy load session history
- Cache previous sets for quick entry
- Optimize re-renders with React.memo
- Use web workers for calculations

## Accessibility

- Large touch targets for gym use
- High contrast mode option
- Voice commands (future)
- Screen reader support
- Haptic feedback on mobile

## Mobile Optimizations

- Landscape mode support
- Prevent screen sleep during session
- Swipe gestures for navigation
- Bottom sheet for quick actions
- Large number inputs for gloved hands
