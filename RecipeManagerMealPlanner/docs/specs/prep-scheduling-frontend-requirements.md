# Frontend Requirements - Prep Scheduling

## Pages/Views

### 1. Prep Timeline Page (`/prep`)

**Components**:
- TimelineView: Gantt-style timeline with tasks
- PrepTaskList: Ordered list with timings
- ParallelTasksHighlight: Tasks that can be done simultaneously
- TaskAssignmentPanel: Assign tasks to people
- MakeAheadSuggestions: Components to prep in advance

**Features**:
- Visual timeline for multiple recipes
- Drag to reschedule tasks
- Assign tasks to helpers
- Start cooking mode (step-by-step with timers)
- Set reminders for time-sensitive tasks
- Show parallel opportunities
- Print prep timeline

**API Calls**:
- POST /api/prep/generate
- POST /api/prep/optimize
- GET /api/prep/timeline/{mealPlanId}

### 2. Cooking Mode

**Components**:
- CurrentStepDisplay: Large, clear current instruction
- NextStepPreview: What's coming next
- ActiveTimers: Running countdown timers
- CompletionCheckbox: Mark steps done
- ProgressIndicator: Overall cooking progress

**Features**:
- Full-screen cooking mode
- Voice commands (optional)
- Active timers with alerts
- Step-by-step navigation
- Ingredient quantities displayed
- Hands-free mode (auto-advance)

## State Management
- `prepSlice`: Active prep tasks and timeline
- Timer state for active cooking
