# Nutrition Management - Frontend Requirements

## Overview
The Nutrition Management frontend provides an intuitive interface for pet owners to track feeding schedules, monitor nutrition, manage food brands, record treats, and report dietary issues with automated reminders and nutrition insights.

## User Stories

### As a Pet Owner
1. I want to set up a feeding schedule for my pet so I can maintain consistent meal times
2. I want to record when I feed my pet so I can track feeding compliance
3. I want to receive reminders before feeding times so I don't forget
4. I want to track treats given to my pet so I can monitor their daily intake
5. I want to see if I'm exceeding my pet's treat allowance
6. I want to change my pet's food brand with a transition plan
7. I want to report dietary issues or allergic reactions
8. I want to view my pet's nutrition history and statistics
9. I want to see daily calorie intake vs. target
10. I want to receive alerts for potential food allergies or patterns

### As a Veterinarian
1. I want to set recommended feeding schedules for my patients
2. I want to review pets' nutrition compliance before appointments
3. I want to be notified of dietary issues reported by owners
4. I want to recommend food brand changes based on health needs

## Pages/Views

### 1. Nutrition Dashboard
**Route:** `/pets/{petId}/nutrition`

**Purpose:** Main hub for viewing and managing nutrition for a pet

**Components:**
- Today's Feeding Schedule Widget
- Treat Allowance Tracker
- Current Food Brand Card
- Dietary Issues Alert Banner
- Feeding Compliance Summary
- Quick Add Feeding Button

**Features:**
- View today's feeding schedule with status
- Quick record feeding action
- Track treat allowance usage
- See food transition progress
- View active dietary issues
- Feeding compliance percentage

**Data Displayed:**
- Upcoming feeding times with countdown
- Feeding status (✓ Fed, ○ Pending, ❌ Missed)
- Treat count and calories for today
- Current food brand and transition status
- Daily calorie intake vs. target
- Active dietary issues count

### 2. Set Feeding Schedule
**Route:** `/pets/{petId}/nutrition/schedule/new` or `/pets/{petId}/nutrition/schedule/edit`

**Purpose:** Form to create or edit feeding schedule

**Form Fields:**
- Number of Meals Per Day (dropdown: 1-4+)
- Feeding Times (time pickers for each meal)
- Portion Sizes (number input with unit selector)
  - Units: Cups, Grams, Ounces
- Food Brand (autocomplete from database)
- Food Type (Dry, Wet, Raw, Homemade, Mixed)
- Daily Calorie Target (calculated based on pet weight/age)
- Calories Per Portion (auto-filled or manual)
- Schedule Type (Regular, Weight Management, Puppy/Kitten, Senior, Medical)
- Special Instructions (text area)
- Treat Allowance Per Day (number of treats and max calories)
- Set Feeding Reminders (checkbox)
- Reminder Time (X minutes before)

**Validation:**
- At least one feeding time required
- Feeding times must be unique
- Portion sizes must be positive
- Total daily portions should align with recommended amount
- Calorie target validation based on pet size

**UX Features:**
- Smart defaults based on pet age/weight
- Schedule templates (Puppy, Adult, Senior, Weight Loss)
- Calorie calculator tool
- Preview of daily schedule
- Warning if portions don't match recommendations

### 3. Record Feeding
**Route:** Modal/Drawer on nutrition dashboard

**Purpose:** Quick interface to record that pet was fed

**Fields:**
- Scheduled time (pre-filled, read-only)
- Actual time (defaults to now, editable)
- Portion given (pre-filled, editable)
- Portion unit (pre-filled)
- Fed by (defaults to current user)
- Food leftover (optional, amount not eaten)
- Notes (optional)
- Photo of meal (optional)

**UX Features:**
- One-tap "Fed Now" for exact scheduled portion
- Quick adjustment for partial portions
- Success confirmation with next feeding time
- Undo option for 30 seconds after recording

### 4. Food Brand Management
**Route:** `/pets/{petId}/nutrition/food-brand`

**Purpose:** Manage current food brand and track history

**Sections:**

#### Current Food Brand
- Brand name and type
- Started date
- Days on current brand
- Transition status (if applicable)
- Transition progress bar
- Transition schedule remaining

#### Change Food Brand
- Current brand (read-only)
- New brand (autocomplete)
- Food type (dropdown)
- Reason for change (dropdown + text)
- Veterinarian approved (checkbox)
- Transition plan:
  - Gradual transition (recommended 7-14 days)
  - Transition schedule generator
  - Daily mixture ratios
- Special notes

#### Food Brand History
- Timeline of all food changes
- Date changed
- Previous/new brand
- Reason for change
- Transition success/issues
- Filter by date range

**Features:**
- Food brand database lookup
- Nutritional information display
- Allergy warning for known triggers
- Vet-recommended brands highlighted
- Transition plan generator
- Progress tracker during transition

### 5. Treat Tracker
**Route:** `/pets/{petId}/nutrition/treats`

**Purpose:** Track and manage pet treats

**Sections:**

#### Today's Treats
- Progress bar: X of Y treats used
- Calorie bar: X of Y calories used
- Warning if approaching/exceeding limit
- List of treats given today with times

#### Record Treat
- Treat type (dropdown: Dental, Training, Biscuit, Fruit, etc.)
- Treat name (autocomplete from history)
- Quantity (number)
- Estimated calories per treat
- Given by (text)
- Reason (Training, Reward, Recreation)
- Time given (defaults to now)
- Notes (optional)

#### Treat History
- Calendar view of treats given
- Filter by date range, treat type
- Statistics:
  - Average treats per day
  - Most common treats
  - Days over allowance
  - Total calories from treats

#### Treat Library
- Save common treats with calorie info
- Quick select from favorites
- Add custom treats

**Features:**
- Daily allowance progress indicator
- Warning when approaching limit
- Alert when exceeded
- Treat suggestions based on remaining allowance
- Calorie lookup database

### 6. Dietary Issues
**Route:** `/pets/{petId}/nutrition/issues`

**Purpose:** Report and track food-related health problems

**Sections:**

#### Active Issues
- Cards for each active issue
- Severity badges (Mild, Moderate, Severe, Critical)
- Days since onset
- Current status
- Quick actions (Update, Resolve, Contact Vet)

#### Report New Issue
- Issue category (dropdown):
  - Allergic Reaction
  - Vomiting
  - Diarrhea
  - Constipation
  - Loss of Appetite
  - Weight Change
  - Food Refusal
  - Other
- Severity (required)
- Symptoms (multi-select checkboxes + custom)
- Suspected trigger (text + suggestions)
- Onset date (date picker)
- Duration (dropdown: Hours, Days, Weeks)
- Current food brand (auto-filled)
- Recent food change (auto-detected, yes/no)
- Actions taken (multi-select)
- Veterinarian notified (checkbox)
- Attach photos/documents
- Detailed description

#### Issue History
- Timeline of all issues
- Filter by status, severity, category
- Pattern analysis showing:
  - Correlation with food changes
  - Seasonal patterns
  - Trigger foods
- Export for vet

**Features:**
- Severity-based color coding
- Automatic vet notification for severe issues
- Pattern recognition alerts
- Food allergy correlation
- Link to food brand at time of issue
- Resolution tracking
- Vet communication interface

### 7. Feeding History
**Route:** `/pets/{petId}/nutrition/history`

**Purpose:** Complete record of all feedings

**Features:**
- Calendar view with feeding markers
- List view with details
- Filter by date range
- Search by food brand
- Export options (PDF, CSV)

**Information Displayed:**
- Date and time (scheduled vs. actual)
- Portion given
- Food brand
- Fed by
- Food leftover (if any)
- Notes
- Status indicator

**Statistics:**
- Feeding compliance rate
- Average feedings per day
- Most missed feeding times
- On-time feeding percentage

### 8. Nutrition Statistics
**Route:** `/pets/{petId}/nutrition/statistics`

**Purpose:** Visualize and analyze nutrition data

**Visualizations:**
- Daily calorie intake chart (line chart)
- Feeding compliance calendar (heatmap)
- Treat frequency chart (bar chart)
- Weight trend correlation (line chart)
- Food brand timeline

**Metrics:**
- Feeding compliance rate
- Average daily calories
- Target calorie compliance
- Treat compliance rate
- Weight change correlation

**Filters:**
- Date range selector
- Include/exclude treats
- Comparison mode (multiple periods)

**Export Options:**
- PDF nutrition report for vet
- CSV data export
- Email report

**Insights:**
- Feeding patterns (best/worst days)
- Calorie trend analysis
- Weight correlation insights
- Treat impact on nutrition
- Recommendations for improvement

### 9. Today's Feeding Schedule
**Route:** `/nutrition/today` or `/pets/{petId}/nutrition/today`

**Purpose:** Day-at-a-glance view of feeding schedule

**Features:**
- Timeline view by hour
- Grouped by pet (if multiple)
- Status indicators for each feeding
- Quick action buttons
- Countdown to next feeding
- Filter by pet

**Display:**
- Current time indicator on timeline
- Past feedings (gray with status)
- Current/upcoming feedings (highlighted)
- Feeding details on tap/click
- Portion sizes and food type

**Actions:**
- Mark as fed (single tap)
- Mark as missed
- Snooze reminder (15 min, 30 min, 1 hour)
- View nutrition details

### 10. Food Transition Tracker
**Route:** `/pets/{petId}/nutrition/transition`

**Purpose:** Monitor food brand transition progress

**Sections:**

#### Transition Progress
- Days into transition (X of Y)
- Progress bar
- Current mixture ratio
- Next milestone date
- Completion estimate

#### Daily Schedule
- Today's mixture ratio (e.g., 75% old, 25% new)
- Portion breakdown per feeding
- Calendar with daily ratios
- Completion checklist

#### Transition Notes
- Daily observations
- Digestive health tracking
- Appetite tracking
- Any issues or concerns
- Photos

#### Health Monitoring
- Stool quality tracker
- Energy level
- Appetite changes
- Any adverse reactions
- Quick report issue button

**Features:**
- Automatic ratio calculator
- Daily reminders with correct portions
- Issue detection and alerts
- Vet notification option
- Success/completion celebration
- Transition history archive

## Components

### Reusable UI Components

#### FeedingCard
Displays summary of a feeding with quick actions.

**Props:**
- feeding (object)
- showActions (boolean)
- compact (boolean)

**Features:**
- Time and status indicator
- Portion size display
- Fed by information
- Quick action menu
- Click to expand details

#### FeedingStatusBadge
Shows status of a feeding.

**Props:**
- status (enum: Pending, Fed, Missed, Skipped)
- size (small, medium, large)

**Variants:**
- ✓ Fed (green)
- ❌ Missed (red)
- ⏰ Pending (yellow)
- ⊘ Skipped (gray)

#### TreatAllowanceGauge
Visual indicator of daily treat usage.

**Props:**
- current (number)
- max (number)
- currentCalories (number)
- maxCalories (number)

**Features:**
- Circular or bar progress indicator
- Color-coded (green ok, yellow warning, red exceeded)
- Treat count and calorie display
- Warning icon when exceeded
- Remaining treats display

#### CalorieTracker
Shows daily calorie intake vs. target.

**Props:**
- currentCalories (number)
- targetCalories (number)
- includesTreats (boolean)

**Features:**
- Progress bar
- Percentage display
- Breakdown (food vs. treats)
- Color-coded status
- Over/under target indicator

#### FoodBrandCard
Displays current food brand information.

**Props:**
- foodBrand (object)
- inTransition (boolean)
- transitionProgress (number)

**Features:**
- Brand name and logo
- Food type badge
- Transition status
- Nutritional highlights
- Change brand button

#### DietaryIssueAlert
Banner or card alerting user to dietary issue.

**Props:**
- issue (object)
- severity (string)
- onView (function)
- onUpdate (function)

**Features:**
- Severity color coding
- Days since onset
- Quick issue summary
- Action buttons
- Vet notification status

#### FeedingTimeline
Timeline view of feedings for a day.

**Props:**
- feedings (array)
- date (date)
- onFeedingClick (function)
- onRecordFeeding (function)

**Features:**
- Hour markers
- Current time indicator
- Feeding markers with status
- Interactive feeding dots
- Click to record or view

#### NutritionChart
Visual representation of nutrition data.

**Props:**
- data (object)
- chartType (line, bar, pie, calendar)
- period (7, 30, 90 days)

**Features:**
- Responsive design
- Tooltips with details
- Color-coded data
- Export image option
- Interactive legend

## State Management

### Global State (Context/Redux)

#### Nutrition State
```javascript
{
  nutritionPlan: {
    id: "guid",
    petId: "guid",
    feedingSchedule: {
      feedingTimes: [],
      dailyTotalPortion: 0,
      dailyCalorieTarget: 0
    },
    currentFoodBrand: {
      brandName: "",
      foodType: "",
      inTransition: false
    },
    treatAllowance: {
      maxPerDay: 0,
      maxCalories: 0,
      usedToday: 0,
      caloriesUsedToday: 0
    }
  },
  todayFeedings: [],
  todayTreats: [],
  dietaryIssues: [],
  loading: false,
  error: null
}
```

#### Feeding Schedule State
```javascript
{
  byDate: {
    [date]: [
      {
        id: "guid",
        scheduledTime: "datetime",
        status: "Pending|Fed|Missed",
        portionSize: 0,
        actualTime: null
      }
    ]
  },
  todaySchedule: [],
  upcomingFeeding: null,
  missedFeedingsCount: 0,
  loading: false
}
```

#### Food Transition State
```javascript
{
  activeTransition: {
    foodBrandChangeId: "guid",
    daysIntoTransition: 0,
    totalDays: 0,
    currentRatio: { old: 75, new: 25 },
    nextMilestone: "date",
    notes: []
  },
  transitionHistory: [],
  loading: false
}
```

### Local State (Component Level)
- Form state for schedules and food changes
- Modal/drawer open/close states
- Filter and search input values
- Calendar view date selection
- Expanded/collapsed card states

## API Integration

### API Service Layer
Create nutrition service with methods:
- `getNutritionPlan(petId)`
- `setFeedingSchedule(petId, data)`
- `updateFeedingSchedule(scheduleId, data)`
- `recordFeeding(petId, data)`
- `getFeedingSchedule(petId, startDate, endDate)`
- `getTodayFeedings(petId)`
- `recordTreat(petId, data)`
- `getTreatHistory(petId, startDate, endDate)`
- `changeFoodBrand(petId, data)`
- `getFoodBrandHistory(petId)`
- `reportDietaryIssue(petId, data)`
- `updateDietaryIssue(issueId, data)`
- `getDietaryIssues(petId, status)`
- `getNutritionStatistics(petId, period)`

### Real-time Updates
- WebSocket connection for feeding reminders
- Server-sent events for schedule updates
- Optimistic UI updates with rollback on error
- Background sync for offline changes

## Responsive Design

### Mobile (< 768px)
- Single column layout
- Bottom navigation bar
- Swipe gestures for actions
- Full-screen modals
- Simplified charts
- Collapsible sections
- Large touch targets (min 44x44px)

### Tablet (768px - 1024px)
- Two column layout where appropriate
- Side drawer for details
- Grid view for food/treat cards
- Expanded charts
- Floating action button

### Desktop (> 1024px)
- Multi-column layout
- Sidebar navigation
- Modal dialogs for forms
- Full data tables
- Dashboard with widgets
- Keyboard shortcuts

## Accessibility

### WCAG 2.1 AA Compliance
- Semantic HTML structure
- ARIA labels and roles
- Keyboard navigation support
- Focus indicators
- Screen reader optimization
- Alt text for images
- Sufficient color contrast (4.5:1)
- Resizable text up to 200%
- Skip navigation links

### Specific Features
- Time announcements for feedings
- Confirmation dialogs for critical actions
- Error messages with clear instructions
- Form field error associations
- Loading state announcements
- Success/failure notifications read aloud

## Performance Optimization

### Loading Strategies
- Lazy load components
- Code splitting by route
- Image lazy loading and optimization
- Pagination for large lists
- Virtual scrolling for long histories

### Caching
- Cache nutrition data locally
- Service worker for offline access
- Cache API responses
- Optimistic updates
- Stale-while-revalidate strategy

### Bundle Optimization
- Tree shaking
- Minimize dependencies
- Dynamic imports
- Compress assets
- CDN for static resources

**Target Metrics:**
- Initial load < 3 seconds
- Time to interactive < 5 seconds
- Lighthouse score > 90
- 60 FPS animations

## Error Handling

### User-Facing Errors
- Validation errors inline on forms
- Toast notifications for actions
- Error boundaries for component failures
- Retry mechanisms for failed requests
- Offline mode messaging
- Clear error messages with next steps

### Network Errors
- Retry logic with exponential backoff
- Queue actions when offline
- Sync when connection restored
- Show connection status indicator

### Form Validation
- Real-time field validation
- Submit button disabled until valid
- Clear error messages
- Highlight invalid fields
- Scroll to first error

## Security

### Input Validation
- Sanitize all user inputs
- Client-side validation (UX)
- Server-side validation (security)
- XSS prevention
- SQL injection prevention

### Authentication & Authorization
- JWT token management
- Automatic token refresh
- Logout on token expiration
- Role-based UI rendering
- Secure storage of credentials

### Data Protection
- HTTPS only
- Sensitive data encryption
- No pet details in URLs
- Clear session data on logout
- Comply with privacy regulations

## Internationalization (i18n)

### Language Support
- English (default)
- Spanish
- French
- German
- Extensible for additional languages

### Localization Features
- Translated UI text
- Localized date/time formats
- Unit conversion (metric/imperial)
- Weight and portion units
- RTL layout support

## Analytics & Monitoring

### User Analytics
- Page views and routes
- Feature usage tracking
- User flows and funnels
- Feeding completion rates
- Treat tracking adoption
- Error frequency

### Events to Track
- Feeding scheduled
- Feeding recorded
- Feeding missed
- Treat given
- Treat allowance exceeded
- Food brand changed
- Dietary issue reported
- Nutrition report generated

### Performance Monitoring
- Page load times
- API response times
- Error rates
- Crash reports
- User session recordings (optional, with consent)

## Testing Requirements

### Unit Tests
- Component rendering
- State management logic
- Utility functions
- Form validation
- Data transformations
- Coverage target: > 80%

### Integration Tests
- API service integration
- Form submission flows
- Navigation flows
- State updates
- Notification handling

### E2E Tests
- Critical user journeys:
  - Set feeding schedule and record feedings
  - Track treats and monitor allowance
  - Change food brand with transition
  - Report dietary issue
- Cross-browser testing
- Mobile device testing

### Accessibility Tests
- Automated a11y testing (axe, Pa11y)
- Screen reader testing
- Keyboard navigation testing
- Color contrast validation

## Browser Support

### Desktop
- Chrome (last 2 versions)
- Firefox (last 2 versions)
- Safari (last 2 versions)
- Edge (last 2 versions)

### Mobile
- iOS Safari (last 2 versions)
- Chrome Mobile (last 2 versions)
- Samsung Internet (latest)

### Progressive Enhancement
- Core functionality without JavaScript
- Enhanced features with JavaScript
- Graceful degradation for older browsers
- Polyfills for missing features
