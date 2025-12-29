# Frontend Requirements - Activity Management

## Overview
The Activity Management frontend provides pet owners with an intuitive interface to track exercise sessions, monitor activity goals, and log behavioral incidents. The UI emphasizes ease of use, visual feedback, and data-driven insights.

## Technology Stack

### Core Framework
- **React 18+** with TypeScript
- **React Router** for navigation
- **Context API / Redux Toolkit** for state management

### UI Components
- **Material-UI (MUI)** or **Tailwind CSS** for styling
- **Chart.js** or **Recharts** for data visualization
- **React Hook Form** for form management
- **Yup** or **Zod** for validation

### Additional Libraries
- **Date-fns** or **Day.js** for date manipulation
- **React Query** or **SWR** for API data fetching
- **Axios** for HTTP requests
- **React Toastify** for notifications
- **Framer Motion** for animations

## Page Structure

### 1. Activity Dashboard
**Route:** `/pets/{petId}/activity`

**Purpose:** Central hub for activity management and overview

**Components:**
- Activity Summary Cards
- Goal Progress Indicators
- Recent Sessions List
- Quick Action Buttons
- Activity Calendar Heat Map
- Weekly/Monthly Charts

**Features:**
- Display current activity statistics
- Show goal completion percentage
- Highlight upcoming exercise reminders
- Quick log exercise button
- View recent behavior incidents
- Filter by date range

**API Calls:**
- `GET /api/v1/activity-analytics/{petId}/summary`
- `GET /api/v1/exercise-goals/{petId}?status=Active`
- `GET /api/v1/exercise-sessions/{petId}?page=1&pageSize=5`

### 2. Log Exercise Session
**Route:** `/pets/{petId}/activity/log-session`

**Purpose:** Form to record a new exercise activity

**Form Fields:**
- Activity Type (dropdown): Walk, Run, Play, Swim, Fetch, Hike, Training, Other
- Start Date/Time (datetime picker)
- Duration (number input with minutes)
- Distance (optional, number input with unit selector: meters/km/miles)
- Intensity Level (radio buttons): Low, Medium, High
- Location (text input, optional)
- Notes (textarea, optional)

**Features:**
- Real-time calorie calculation based on pet profile
- Quick duration buttons (15, 30, 45, 60 minutes)
- Current location suggestion (GPS integration)
- Save as draft functionality
- Photo upload capability

**Validation Rules:**
- Activity type is required
- Duration must be 1-1440 minutes
- Start time cannot be in future
- End time must be after start time

**API Calls:**
- `POST /api/v1/exercise-sessions`

**Success Action:**
- Show success toast notification
- Redirect to Activity Dashboard
- Update dashboard data automatically

### 3. Exercise Sessions History
**Route:** `/pets/{petId}/activity/sessions`

**Purpose:** View and manage all exercise sessions

**Components:**
- Filterable Data Table/List
- Search Bar
- Date Range Picker
- Activity Type Filter
- Sort Options
- Pagination Controls
- Export to CSV button

**Table Columns:**
- Date & Time
- Activity Type (with icon)
- Duration
- Distance
- Intensity
- Calories Burned
- Actions (View, Edit, Delete)

**Features:**
- Inline editing for quick updates
- Bulk delete capability
- Group by week/month view
- Statistics summary above table
- Click row to view details

**API Calls:**
- `GET /api/v1/exercise-sessions/{petId}`
- `DELETE /api/v1/exercise-sessions/{sessionId}`

### 4. Session Detail View
**Route:** `/pets/{petId}/activity/sessions/{sessionId}`

**Purpose:** Detailed view of a specific exercise session

**Sections:**
- Session Summary Card
- Activity Metrics
- Map View (if location available)
- Notes Section
- Photos/Media Gallery
- Edit/Delete Actions

**Features:**
- Full session information display
- Visual intensity indicator
- Achievement badges (if milestones reached)
- Share session capability
- Add to calendar option

**API Calls:**
- `GET /api/v1/exercise-sessions/{sessionId}/details`
- `PUT /api/v1/exercise-sessions/{sessionId}`
- `DELETE /api/v1/exercise-sessions/{sessionId}`

### 5. Set Exercise Goals
**Route:** `/pets/{petId}/activity/set-goal`

**Purpose:** Create new activity goals

**Form Fields:**
- Goal Type (radio buttons): Daily, Weekly, Monthly
- Goal Period Start Date (date picker)
- Recurring Goal (checkbox)
- Target Duration (number input, minutes)
- Target Distance (optional, number input)
- Target Sessions Count (optional, number input)
- Target Calories (optional, calculated or manual)

**Features:**
- Recommended goals based on pet breed/age
- Visual goal preview
- Difficulty indicator
- Template selection (Beginner, Moderate, Advanced)
- Multiple goal targets
- Custom reminders setup

**Validation Rules:**
- At least one target must be specified
- Start date cannot be in the past
- Only one active goal per type allowed
- Recurring goals cannot have end date

**API Calls:**
- `POST /api/v1/exercise-goals`
- `GET /api/v1/exercise-goals/{petId}` (to check existing goals)

### 6. Goals Management
**Route:** `/pets/{petId}/activity/goals`

**Purpose:** View and manage exercise goals

**Components:**
- Active Goals Cards
- Completed Goals List
- Failed Goals Archive
- Goal Progress Bars
- Statistics Panel

**Goal Card Elements:**
- Goal type badge
- Progress percentage
- Current vs Target metrics
- Days remaining
- Achievement status
- Edit/Cancel actions

**Features:**
- Visual progress indicators
- Goal comparison charts
- Mark goal as complete manually
- Archive completed goals
- Goal history trends
- Motivation messages

**API Calls:**
- `GET /api/v1/exercise-goals/{petId}`
- `GET /api/v1/exercise-goals/{goalId}/progress`
- `PUT /api/v1/exercise-goals/{goalId}`
- `DELETE /api/v1/exercise-goals/{goalId}`

### 7. Log Behavior Incident
**Route:** `/pets/{petId}/activity/log-incident`

**Purpose:** Document behavioral incidents

**Form Fields:**
- Behavior Type (dropdown): Aggression, Anxiety, Destructive, Excessive Barking, Other
- Severity Level (radio buttons with visual indicators): Minor, Moderate, Severe
- Date/Time of Incident (datetime picker)
- Duration (optional, minutes)
- Description (textarea, required, min 10 chars)
- Triggers (multi-select tags)
- Context (textarea, optional)
- Actions Taken (textarea, optional)
- Requires Follow-up (checkbox)
- Add Photo/Video (file upload)

**Features:**
- Common triggers quick-select
- Severity level color coding
- Voice-to-text for description
- Save as draft
- Related incident suggestions
- Veterinarian notification option

**Validation Rules:**
- Description minimum 10 characters
- Occurred time cannot be in future
- Severity must be selected
- At least one trigger recommended

**API Calls:**
- `POST /api/v1/behavior-incidents`

### 8. Behavior Incidents History
**Route:** `/pets/{petId}/activity/incidents`

**Purpose:** View and analyze behavior incidents

**Components:**
- Timeline View (default)
- Calendar View (toggle)
- Filter Panel
- Severity Distribution Chart
- Trigger Analysis Panel

**Timeline Card Elements:**
- Date & Time
- Behavior Type Icon
- Severity Badge
- Brief Description
- Triggers Tags
- Follow-up Status
- View Details Link

**Features:**
- Filter by behavior type, severity, date range
- Search functionality
- Pattern recognition highlights
- Export to PDF for vet visits
- Print incident report
- Mark as resolved

**API Calls:**
- `GET /api/v1/behavior-incidents/{petId}`
- `GET /api/v1/behavior-analytics/{petId}/patterns`

### 9. Behavior Analytics
**Route:** `/pets/{petId}/activity/behavior-analytics`

**Purpose:** Visualize behavioral patterns and trends

**Components:**
- Incident Frequency Chart
- Trigger Correlation Matrix
- Severity Trend Line
- Time-of-Day Heat Map
- Recommendations Panel
- Professional Consultation Prompt

**Features:**
- Interactive charts
- Date range selection
- Behavior type filtering
- Downloadable reports
- Comparison with breed averages
- AI-generated insights

**API Calls:**
- `GET /api/v1/behavior-analytics/{petId}/patterns`

### 10. Activity Analytics
**Route:** `/pets/{petId}/activity/analytics`

**Purpose:** Comprehensive activity statistics and trends

**Components:**
- Activity Overview Dashboard
- Time Series Charts (daily/weekly/monthly)
- Activity Type Distribution (pie chart)
- Goal Achievement History
- Calorie Burn Trends
- Comparison Charts (vs previous period)
- Insights and Recommendations

**Chart Types:**
- Line Chart: Duration over time
- Bar Chart: Sessions per week
- Pie Chart: Activity type distribution
- Area Chart: Calorie burn trends
- Heat Map: Activity calendar

**Features:**
- Multiple time period views
- Export charts as images
- Custom date range selection
- Print-friendly report view
- Share analytics
- Goal vs actual comparison

**API Calls:**
- `GET /api/v1/activity-analytics/{petId}/summary?period=Month`

## Shared Components

### ActivitySummaryCard
**Props:**
- `totalSessions`: number
- `totalDuration`: number
- `totalDistance`: number
- `totalCalories`: number
- `period`: string

**Displays:** Key metrics in card format with icons

### GoalProgressBar
**Props:**
- `goalId`: string
- `current`: number
- `target`: number
- `label`: string
- `color`: string

**Features:** Animated progress bar with percentage

### ExerciseSessionCard
**Props:**
- `session`: ExerciseSession object
- `onEdit`: function
- `onDelete`: function
- `onView`: function

**Displays:** Session summary with quick actions

### BehaviorIncidentCard
**Props:**
- `incident`: BehaviorIncident object
- `onView`: function
- `showActions`: boolean

**Features:** Color-coded by severity

### ActivityCalendar
**Props:**
- `sessions`: ExerciseSession[]
- `onDateSelect`: function

**Features:** Heat map showing activity intensity

### QuickLogButton
**Props:**
- `petId`: string
- `activityType`: string (optional preset)

**Features:** Floating action button for quick logging

### StatCard
**Props:**
- `title`: string
- `value`: number | string
- `icon`: ReactNode
- `trend`: number (optional)
- `subtitle`: string (optional)

**Features:** Compact stat display with optional trend indicator

### ActivityTypeIcon
**Props:**
- `activityType`: string
- `size`: number

**Returns:** Appropriate icon for activity type

## State Management

### Global State (Context/Redux)
```typescript
interface ActivityState {
  activePet: Pet | null;
  sessions: ExerciseSession[];
  goals: ExerciseGoal[];
  incidents: BehaviorIncident[];
  analytics: ActivityAnalytics | null;
  loading: boolean;
  error: string | null;
}
```

### Actions
- `setActivePet(pet: Pet)`
- `fetchSessions(petId: string, params: QueryParams)`
- `addSession(session: ExerciseSession)`
- `updateSession(sessionId: string, data: Partial<ExerciseSession>)`
- `deleteSession(sessionId: string)`
- `fetchGoals(petId: string)`
- `addGoal(goal: ExerciseGoal)`
- `updateGoal(goalId: string, data: Partial<ExerciseGoal>)`
- `deleteGoal(goalId: string)`
- `fetchIncidents(petId: string, params: QueryParams)`
- `addIncident(incident: BehaviorIncident)`
- `fetchAnalytics(petId: string, period: string)`

## Responsive Design Requirements

### Mobile (< 768px)
- Single column layouts
- Bottom navigation for main sections
- Collapsible filters
- Touch-optimized buttons (min 44x44px)
- Swipe gestures for navigation
- Simplified charts
- Floating action button for quick log

### Tablet (768px - 1024px)
- Two-column layouts where appropriate
- Side navigation drawer
- Full-featured charts
- Modal dialogs for forms

### Desktop (> 1024px)
- Multi-column layouts
- Persistent side navigation
- Rich data visualizations
- Inline editing capabilities
- Hover states and tooltips

## Accessibility Requirements

1. **WCAG 2.1 AA Compliance**
   - Proper heading hierarchy
   - ARIA labels for interactive elements
   - Alt text for all images
   - Color contrast ratio ≥ 4.5:1

2. **Keyboard Navigation**
   - All interactive elements accessible via Tab
   - Escape key closes modals
   - Enter key submits forms
   - Arrow keys for calendar navigation

3. **Screen Reader Support**
   - Semantic HTML elements
   - ARIA live regions for dynamic content
   - Descriptive link text
   - Form label associations

4. **Focus Management**
   - Visible focus indicators
   - Focus trap in modals
   - Focus restoration after modal close

## Performance Optimization

1. **Code Splitting**
   - Lazy load route components
   - Dynamic imports for heavy libraries
   - Bundle size < 500KB gzipped

2. **Caching Strategy**
   - Cache API responses with React Query
   - Stale-while-revalidate pattern
   - Optimistic UI updates

3. **Image Optimization**
   - Lazy loading images
   - Responsive images with srcset
   - WebP format with fallbacks

4. **Rendering Optimization**
   - React.memo for expensive components
   - useMemo/useCallback for derived values
   - Virtualization for long lists

## Error Handling

### Network Errors
- Retry mechanism with exponential backoff
- Offline mode detection
- Error boundary components
- User-friendly error messages

### Validation Errors
- Inline field validation
- Error message below field
- Form-level error summary
- Prevent submission until valid

### User Feedback
- Loading spinners for async operations
- Success/Error toast notifications
- Confirmation dialogs for destructive actions
- Progress indicators for multi-step processes

## Notifications

### Types
1. **Success**: Green toast with checkmark icon
2. **Error**: Red toast with error icon
3. **Warning**: Yellow toast with warning icon
4. **Info**: Blue toast with info icon

### Triggers
- Session logged successfully
- Goal created/updated
- Incident reported
- Goal milestone reached
- Daily/weekly summary
- Reminder notifications

### Push Notifications (PWA)
- Exercise reminders
- Goal deadline approaching
- Weekly activity summary
- Behavior pattern alerts

## Offline Support

### PWA Capabilities
- Service worker for offline access
- Cache critical assets
- Offline session logging (sync when online)
- Background sync for data submission

### Offline Indicators
- Banner showing offline status
- Disabled features grayed out
- Queue indicator for pending syncs

## Localization

### Supported Features
- Multi-language support (i18n)
- Date/time formatting
- Unit conversions (metric/imperial)
- Currency formatting (for premium features)

### Languages
- English (default)
- Spanish
- French
- German
- Additional as needed

## Testing Requirements

### Unit Tests
- Component rendering tests
- User interaction tests
- Form validation tests
- Utility function tests
- 80% coverage minimum

### Integration Tests
- API integration tests
- User flow tests
- Form submission tests

### E2E Tests
- Critical user journeys
- Cross-browser testing
- Mobile device testing

### Tools
- Jest for unit tests
- React Testing Library
- Cypress or Playwright for E2E
- Storybook for component development

## Security Considerations

1. **Authentication**
   - Token-based authentication
   - Auto-logout on token expiry
   - Secure token storage

2. **Input Sanitization**
   - XSS prevention
   - SQL injection prevention (backend)
   - Validate all user inputs

3. **HTTPS Only**
   - Enforce secure connections
   - HSTS headers

4. **Content Security Policy**
   - Restrict external resources
   - Inline script restrictions

## Analytics Tracking

### Events to Track
- Page views
- Session logging
- Goal creation
- Incident reporting
- Feature usage
- Error occurrences
- User engagement time

### Tools
- Google Analytics or Mixpanel
- Custom event tracking
- Conversion funnels
- User behavior analysis
