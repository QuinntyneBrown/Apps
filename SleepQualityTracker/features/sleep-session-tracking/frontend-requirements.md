# Frontend Requirements - Sleep Session Tracking

## User Interface Components

### Sleep Session Entry Form
**Component**: SleepSessionForm
- Date picker for sleep date
- Time picker for sleep start time (bedtime)
- Time picker for wake time
- Quality rating slider (1-10) with visual feedback
- Optional notes text area (500 chars max)
- Save and Cancel buttons
- Form validation with real-time error messages

**Validation**:
- Wake time must be after sleep start time
- Cannot log future sleep sessions (except manual scheduling)
- Quality rating required
- Show calculated duration in hours and minutes

### Sleep Session List View
**Component**: SleepSessionList
- Table/card view of recent sleep sessions
- Columns: Date, Duration, Quality, Sleep Start, Wake Time
- Sort by date (default: newest first)
- Filter by date range
- Search functionality
- Pagination (20 sessions per page)
- Click session to view details

### Sleep Session Detail View
**Component**: SleepSessionDetail
- Display all session information
- Visual representation of sleep timeline
- Sleep stages breakdown (pie chart or bar chart)
- List of interruptions with times
- Quality score breakdown
- Edit and Delete buttons
- Navigation back to list

### Sleep Stages Visualization
**Component**: SleepStagesChart
- Stacked bar chart or area chart showing sleep stages over time
- Color-coded: Deep (dark blue), Light (light blue), REM (purple), Awake (red)
- Timeline on X-axis (bedtime to wake time)
- Duration labels for each stage
- Interactive tooltips with details

### Sleep Interruptions Log
**Component**: InterruptionLog
- Add interruption button
- List of interruptions for session
- Each entry shows: time, duration, reason
- Edit/delete options for each interruption
- Visual markers on sleep timeline

### Device Sync Interface
**Component**: DeviceSyncPanel
- List of connected devices
- Connect new device button
- Last sync timestamp for each device
- Manual sync trigger button
- Sync status indicator (syncing/success/error)
- Device authorization flow

### Sleep History Calendar
**Component**: SleepCalendar
- Monthly calendar view
- Each day shows sleep quality color-coded (green=good, yellow=ok, red=poor)
- Click day to view session details
- Visual indicators for days with/without data
- Quick summary tooltip on hover

## Page Layouts

### Dashboard Page
**Route**: /dashboard
- Summary cards: Last night's sleep, 7-day average, current streak
- Sleep quality trend chart (last 30 days)
- Quick log sleep button
- Recent sessions list (last 5)
- Upcoming sleep goal reminder

### Sleep Log Page
**Route**: /sleep/log
- Sleep session entry form
- Option to sync from device
- Recent sessions preview

### Sleep History Page
**Route**: /sleep/history
- Calendar view option
- List view option
- Filter and search controls
- Export data button
- Date range selector

### Session Detail Page
**Route**: /sleep/session/:id
- Full session details
- Sleep stages visualization
- Interruptions log
- Edit session button
- Related insights/correlations

## User Interactions

### Log New Sleep Session
1. User clicks "Log Sleep" button
2. Form opens with current date pre-selected
3. User enters bedtime and wake time
4. Duration auto-calculates and displays
5. User rates quality (1-10)
6. Optional: add notes
7. User clicks Save
8. Success message displays
9. Redirect to session detail or dashboard

### Sync from Device
1. User clicks "Sync from Device"
2. If not connected, show device connection flow
3. Show syncing indicator
4. Display number of sessions synced
5. Update sleep history list
6. Show success notification

### View Sleep Details
1. User clicks on session in list
2. Navigate to detail page
3. Display all session information
4. Show visualizations
5. Allow editing if needed

### Add Sleep Interruption
1. From session detail, click "Add Interruption"
2. Modal/form opens
3. Enter time (must be between bedtime and wake time)
4. Enter duration
5. Optionally add reason
6. Save interruption
7. Update session display

## State Management

### Global State
- Current user
- Connected devices
- Active sleep session (if in progress)
- User preferences (time format, temperature units)

### Component State
- Sleep sessions list (cached, paginated)
- Selected session details
- Form input values
- Loading states
- Error messages

## API Integration

### Fetch Sleep Sessions
```javascript
GET /api/sleep-sessions?userId={id}&startDate={date}&endDate={date}
```

### Create Sleep Session
```javascript
POST /api/sleep-sessions
Body: { sleepStartTime, wakeTime, qualityRating, userId }
```

### Update Sleep Session
```javascript
PUT /api/sleep-sessions/{id}
Body: { partial session data }
```

### Add Sleep Stages
```javascript
POST /api/sleep-sessions/{id}/stages
Body: { lightSleepDuration, deepSleepDuration, remDuration, awakeTime }
```

### Add Interruption
```javascript
POST /api/sleep-sessions/{id}/interruptions
Body: { interruptionTime, duration, reason }
```

### Sync from Device
```javascript
POST /api/sleep-sessions/sync
Body: { deviceType, deviceData, userId }
```

## Responsive Design

### Mobile (< 768px)
- Stack form fields vertically
- Full-width buttons
- Simplified table view (cards instead)
- Bottom navigation
- Swipe gestures for calendar

### Tablet (768px - 1024px)
- Two-column layout for forms
- Side panel for filters
- Comfortable touch targets

### Desktop (> 1024px)
- Multi-column dashboard layout
- Sidebar navigation
- Detailed table views
- Hover interactions

## Accessibility

- All form inputs have labels
- ARIA labels for interactive elements
- Keyboard navigation support
- Focus indicators visible
- Color not sole indicator (use icons + color)
- Screen reader announcements for async actions
- Minimum touch target size: 44x44px

## Performance

- Lazy load session list (virtualization for long lists)
- Cache recent sessions in memory
- Debounce search/filter inputs
- Optimize chart rendering (use canvas for large datasets)
- Progressive image loading
- Code splitting by route

## Error Handling

- Display user-friendly error messages
- Retry mechanism for failed API calls
- Offline mode indication
- Form validation errors inline
- Network error recovery
- Session timeout handling

## Notifications

- Success message on session save
- Sync completion notification
- Early wake detection alert
- Device connection status
- Validation errors
