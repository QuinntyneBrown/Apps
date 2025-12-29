# Session Management - Frontend Requirements

## Overview
User interface components for managing focus sessions including timer display, controls, and session history.

---

## Pages

### Session Timer Page (`/session`)
**Purpose**: Main focus session interface

**Components**:
- Timer Display (large, centered)
- Session Type Indicator
- Task/Project Label
- Control Buttons (Start/Pause/Resume/Stop)
- Distraction Quick-Log Button
- Session Progress Ring

**States**:
- Idle (no active session)
- Active (timer running)
- Paused (timer frozen)
- Completing (rating prompt)

---

### Session History Page (`/sessions`)
**Purpose**: View past sessions

**Components**:
- Date Range Filter
- Status Filter Chips
- Session Cards List
- Pagination Controls
- Export Button

---

## Components

### TimerDisplay
```typescript
interface TimerDisplayProps {
  remainingSeconds: number;
  totalSeconds: number;
  status: 'idle' | 'active' | 'paused';
}
```

**Visual Requirements**:
- Large circular timer (min 200px diameter)
- Progress ring showing completion %
- MM:SS format display
- Color changes: green (active), yellow (paused), blue (idle)

---

### SessionControls
```typescript
interface SessionControlsProps {
  status: 'idle' | 'active' | 'paused';
  onStart: () => void;
  onPause: () => void;
  onResume: () => void;
  onStop: () => void;
}
```

**Buttons**:
- Start (green, prominent) - shown when idle
- Pause (yellow) - shown when active
- Resume (green) - shown when paused
- Stop (red) - shown when active or paused

---

### SessionSetupModal
```typescript
interface SessionSetupModalProps {
  isOpen: boolean;
  onClose: () => void;
  onStart: (config: SessionConfig) => void;
  tasks: Task[];
  projects: Project[];
}
```

**Fields**:
- Session Type Selector (Pomodoro/Short/Long/Custom)
- Custom Duration Slider (if custom selected)
- Task Dropdown (optional)
- Project Dropdown (optional)
- Start Session Button

---

### SessionCompletionModal
```typescript
interface SessionCompletionModalProps {
  session: Session;
  onComplete: (rating: number, progress: string) => void;
  onAbandon: (reason: string) => void;
}
```

**Fields**:
- Quality Rating (1-5 stars)
- Progress Notes (textarea)
- Complete Button
- Mark as Abandoned Button

---

### SessionCard
```typescript
interface SessionCardProps {
  session: Session;
  onClick: () => void;
}
```

**Display**:
- Date/Time
- Duration
- Status Badge
- Task Name (if any)
- Quality Rating Stars
- Focus Score Badge

---

## User Flows

### Start Session Flow
1. User clicks "Start Session" button
2. Session Setup Modal opens
3. User selects session type
4. User optionally selects task/project
5. User clicks "Start"
6. Timer begins countdown
7. Focus mode activates (if configured)

### Complete Session Flow
1. Timer reaches zero
2. Notification/sound plays
3. Completion Modal opens
4. User rates quality (1-5 stars)
5. User adds progress notes (optional)
6. User clicks "Complete"
7. Success message displayed
8. Break prompt shown

### Abandon Session Flow
1. User clicks "Stop" button
2. Confirmation dialog appears
3. User confirms abandonment
4. Reason input shown (optional)
5. Session marked as abandoned
6. Dashboard updates

---

## State Management

### Session Store
```typescript
interface SessionState {
  currentSession: Session | null;
  remainingSeconds: number;
  status: 'idle' | 'active' | 'paused';
  pauseCount: number;
  totalPauseTime: number;
}
```

### Actions
- `startSession(config: SessionConfig)`
- `pauseSession(reason?: string)`
- `resumeSession()`
- `completeSession(rating: number, progress: string)`
- `abandonSession(reason?: string)`
- `tickTimer()`

---

## Validation Rules

| Field | Rule |
|-------|------|
| Custom Duration | 5-120 minutes |
| Quality Rating | 1-5 (required on complete) |
| Progress Notes | Max 1000 characters |
| Abandon Reason | Max 500 characters |

---

## Accessibility Requirements

- Timer updates announced to screen readers every minute
- All buttons have clear labels
- Keyboard shortcuts: Space (pause/resume), Escape (stop)
- High contrast mode support
- Focus indicators on all interactive elements

---

## Responsive Design

| Breakpoint | Timer Size | Layout |
|------------|------------|--------|
| Mobile (<768px) | 200px | Single column, controls below |
| Tablet (768-1024px) | 250px | Two column |
| Desktop (>1024px) | 300px | Centered with side panels |
