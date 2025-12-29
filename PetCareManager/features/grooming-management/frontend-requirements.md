# Grooming Management - Frontend Requirements

## Overview
The Grooming Management frontend provides an intuitive interface for pet owners to schedule grooming appointments, track grooming history, and log at-home grooming activities with reminder notifications and grooming insights.

## User Stories

### As a Pet Owner
1. I want to schedule a grooming appointment for my pet so I can ensure regular professional care
2. I want to see all upcoming grooming appointments so I can plan my schedule
3. I want to receive reminders before grooming appointments so I don't forget
4. I want to log at-home grooming activities so I can track all grooming care
5. I want to view my pet's complete grooming history to share with groomers or vets
6. I want to see when my pet is due for grooming so I can maintain a regular schedule
7. I want to track grooming costs over time to manage my pet care budget
8. I want to save my preferred groomers for easy rebooking
9. I want to upload before/after photos to track my pet's appearance
10. I want to rate and review grooming services

### As a Professional Groomer
1. I want to view my scheduled appointments for the day/week
2. I want to mark appointments as completed and add notes
3. I want to recommend next grooming dates for clients
4. I want to view pet grooming history and preferences

## Pages/Views

### 1. Grooming Dashboard
**Route:** `/pets/{petId}/grooming`

**Purpose:** Main hub for viewing and managing all grooming activities

**Components:**
- Upcoming Appointments Widget
- Last Grooming Summary
- Next Recommended Grooming Alert
- Quick Actions (Schedule, Log Home Grooming)
- Grooming History Timeline
- Grooming Statistics Card

**Features:**
- Filter by appointment status (Upcoming, Completed, Cancelled)
- Search by groomer name or service type
- Sort by date
- Quick reschedule/cancel buttons
- Visual grooming timeline
- Overdue grooming alerts

**Data Displayed:**
- Next appointment details with countdown
- Days since last grooming
- Grooming frequency average
- Total grooming sessions this year
- Preferred groomer information
- Quick grooming status indicators

### 2. Schedule Appointment
**Route:** `/pets/{petId}/grooming/schedule`

**Purpose:** Form to book a new grooming appointment

**Form Fields:**
- Pet Selection (if multiple pets)
- Appointment Date & Time (date/time picker)
- Groomer Selection (dropdown with search)
  - Browse saved groomers
  - Search new groomers
  - Add new groomer
- Service Type (Full Groom, Bath Only, Nail Trim, etc.)
- Specific Services (checkboxes):
  - Bath & Brush
  - Haircut/Trim
  - Nail Clipping
  - Ear Cleaning
  - Teeth Brushing
  - Anal Gland Expression
  - De-shedding Treatment
  - Flea Treatment
  - Other (custom input)
- Estimated Duration (auto-calculated or manual)
- Estimated Price
- Location:
  - Salon Address
  - Mobile Groomer (comes to you)
- Special Instructions/Notes (text area)
- Reminder Settings:
  - Enable reminders (toggle)
  - Remind me: 24 hours, 2 hours, 30 minutes before
  - Notification method: Push, Email, SMS

**Validation:**
- Date must be in future
- At least one service required
- Groomer name required
- All required fields marked with *

**UX Features:**
- Groomer availability calendar
- Service package templates
- Copy from previous appointment
- Recurring appointment option
- Save new groomer to favorites
- Price estimate calculator

### 3. Groomer Directory
**Route:** `/groomers`

**Purpose:** Browse and select professional groomers

**Features:**
- Search by name, location, or specialty
- Filter by:
  - Mobile/Salon
  - Service types offered
  - Price range
  - Rating
  - Distance
- Sort by rating, price, distance, reviews
- Map view of nearby groomers
- List/Grid view toggle

**Groomer Card Display:**
- Groomer name and business name
- Profile photo/logo
- Rating (stars) and review count
- Specialties/Certifications
- Services offered
- Price range ($, $$, $$$)
- Location/service area
- Mobile groomer badge
- Distance from user
- Availability status
- [Book Appointment] button
- [Save to Favorites] button

**Detail View:**
- Full profile information
- Photo gallery
- Complete service menu with prices
- Reviews and ratings
- Business hours
- Certifications and experience
- Cancellation policy
- Contact information

### 4. Complete Appointment
**Route:** Modal/Drawer from appointment list

**Purpose:** Record completion of grooming appointment

**Fields:**
- Appointment details (pre-filled, read-only)
- Actual Completion Date/Time (defaults to now)
- Services Performed (checkboxes, pre-selected from appointment)
- Actual Duration
- Actual Cost Paid
- Payment Method (Cash, Credit, Debit, Check, Other)
- Pet Behavior Notes:
  - Excellent
  - Good
  - Nervous
  - Difficult
  - Custom notes
- Condition Observations:
  - Coat condition
  - Skin condition
  - Nail condition
  - Ear condition
  - Custom notes
- Upload Photos (before/after)
- Rate Service (1-5 stars)
- Written Review (optional)
- Recommend Next Grooming Date (date picker)

**UX Features:**
- Quick complete with defaults
- Photo upload with preview
- Auto-suggest next date based on breed
- Save groomer rating
- One-tap rebook option

### 5. Log Home Grooming
**Route:** `/pets/{petId}/grooming/home/log`

**Purpose:** Record at-home grooming activities

**Form Fields:**
- Pet Selection
- Grooming Date & Time (defaults to now)
- Performed By (text input, defaults to current user)
- Activities Performed (checkboxes):
  - Bathing
  - Brushing
  - Nail Trimming
  - Ear Cleaning
  - Teeth Brushing
  - Eye Cleaning
  - Paw Pad Trimming
  - Mat Removal
  - Other
- Duration (minutes)
- Products Used (multi-select or free text):
  - Shampoo
  - Conditioner
  - Brushes/combs used
  - Nail clippers
  - Other tools
- Notes (text area)
- Upload Photos
- Next Planned Home Grooming (date picker)

**UX Features:**
- Quick log templates (e.g., "Weekly Brush", "Bath Day")
- Product favorites list
- Time tracking (start/stop timer)
- Photo upload
- Save as routine/template

### 6. Grooming History
**Route:** `/pets/{petId}/grooming/history`

**Purpose:** Complete record of all grooming activities

**Features:**
- Tabbed view: All | Professional | Home
- Timeline view or list view toggle
- Filter by:
  - Date range
  - Groomer
  - Service type
  - Session type
- Search by notes/services
- Export to PDF/CSV
- Photo gallery view

**Information Displayed:**
- Session date and time
- Session type (Professional/Home)
- Groomer name or person who performed
- Services/activities performed
- Duration
- Cost (if professional)
- Rating (if professional)
- Behavior notes
- Condition observations
- Photo thumbnails (click to view)
- Next recommended date

**Interaction:**
- Click row to expand details
- View before/after photos
- Rebook appointment (if professional)
- Repeat home grooming routine
- Share record with vet/groomer
- Add notes to past sessions

### 7. Upcoming Appointments
**Route:** `/pets/{petId}/grooming/appointments`

**Purpose:** View and manage scheduled appointments

**Features:**
- Calendar view
- List view
- Filter by pet (if multiple)
- Sort by date
- Status filters (Confirmed, Pending, All)

**Appointment Card Display:**
- Date and time with countdown
- Groomer name and location
- Service type and services
- Estimated duration
- Price
- Status badge
- Quick actions menu

**Actions:**
- View details
- Reschedule
- Cancel
- Add to calendar
- Get directions (if salon)
- Contact groomer
- Edit notes/instructions

**Reminder Management:**
- View upcoming reminders
- Modify reminder times
- Disable reminders for specific appointment

### 8. Grooming Calendar
**Route:** `/grooming/calendar`

**Purpose:** Calendar view of all grooming appointments and recommendations

**Features:**
- Monthly calendar grid
- Week view option
- Day view option
- Color-coded events:
  - Professional appointments (blue)
  - Home grooming sessions (green)
  - Recommended grooming (yellow)
- Filter by pet
- Navigate months/weeks
- Today button

**Calendar Events Show:**
- Time
- Pet name (if multiple pets)
- Groomer/person
- Service type
- Quick status icon

**Interactions:**
- Click date to add appointment/log grooming
- Click event to view details
- Drag to reschedule (with confirmation)
- Export calendar to iCal

### 9. Grooming Statistics & Insights
**Route:** `/pets/{petId}/grooming/statistics`

**Purpose:** Visualize and analyze grooming patterns and costs

**Visualizations:**
- Grooming frequency trend (line chart)
- Cost analysis over time (bar chart)
- Professional vs. Home sessions (pie chart)
- Services breakdown (bar chart)
- Groomer comparison (if multiple used)
- Monthly/Yearly spending

**Filters:**
- Date range selector
- Session type
- Groomer

**Statistics Displayed:**
- Total grooming sessions (period)
- Professional vs. Home ratio
- Average frequency (days)
- Total spent
- Average cost per session
- Most common services
- Preferred groomer
- Longest gap between grooming
- Grooming compliance rate

**Insights:**
- Spending trends (up/down)
- Frequency recommendations
- Cost-saving suggestions
- Optimal grooming schedule
- Groomer loyalty discount opportunities

**Export Options:**
- PDF report
- CSV data
- Email report

### 10. Grooming Preferences
**Route:** `/pets/{petId}/grooming/preferences`

**Purpose:** Set grooming schedule preferences and pet-specific needs

**Settings:**

#### Grooming Schedule
- Recommended frequency (every X weeks)
- Preferred day of week
- Preferred time of day
- Auto-schedule recommendations

#### Pet Grooming Needs
- Coat type (Short, Medium, Long, Curly, Wire, etc.)
- Grooming difficulty level
- Special sensitivities
- Behavioral notes for groomers
- Health considerations
- Preferred grooming style/cut

#### Favorite Groomers
- List of saved groomers
- Default groomer selection
- Groomer notes and preferences

#### Notification Preferences
- Enable grooming reminders
- Reminder timing preferences
- Notification channels
- Overdue grooming alerts

#### Budget Settings
- Monthly grooming budget
- Alert when approaching budget
- Cost tracking preferences

## Components

### Reusable UI Components

#### AppointmentCard
Displays appointment summary with quick actions.

**Props:**
- appointment (object)
- showActions (boolean)
- compact (boolean)

**Features:**
- Countdown timer for upcoming
- Status badge
- Quick action menu
- Click to expand details
- Color-coded by status

#### GroomingSessionCard
Shows grooming session summary.

**Props:**
- session (object)
- showPhotos (boolean)
- expandable (boolean)

**Features:**
- Session type icon
- Service badges
- Cost display
- Rating stars
- Photo thumbnails
- Expand for full details

#### GroomerCard
Displays groomer information.

**Props:**
- groomer (object)
- showBookButton (boolean)
- compact (boolean)

**Features:**
- Profile photo
- Rating display
- Service badges
- Distance indicator
- Mobile groomer badge
- Favorite toggle
- Book button

#### GroomingTimeline
Visual timeline of grooming history.

**Props:**
- sessions (array)
- showPhotos (boolean)
- interactive (boolean)

**Features:**
- Chronological display
- Professional/Home indicators
- Photo thumbnails
- Click to expand
- Smooth scrolling

#### ServiceSelector
Multi-select component for grooming services.

**Props:**
- selectedServices (array)
- serviceOptions (array)
- allowCustom (boolean)
- onChange (function)

**Features:**
- Checkbox list
- Service categories
- Popular services first
- Custom service input
- Service duration estimates
- Price calculator integration

#### GroomingCalendar
Calendar view component.

**Props:**
- appointments (array)
- sessions (array)
- recommendations (array)
- onDateClick (function)
- onEventClick (function)

**Features:**
- Month/week/day views
- Event color coding
- Drag-and-drop reschedule
- Today highlight
- Event tooltips

#### PhotoGallery
Before/after photo display.

**Props:**
- photos (array)
- allowUpload (boolean)
- maxPhotos (number)

**Features:**
- Grid layout
- Lightbox view
- Before/after slider
- Upload interface
- Delete photos
- Photo captions

#### GroomingRecommendationBanner
Alert for upcoming grooming needs.

**Props:**
- recommendation (object)
- onSchedule (function)
- onDismiss (function)

**Features:**
- Priority color coding
- Days overdue display
- Quick schedule button
- Snooze option
- Dismissible

#### CostTracker
Displays grooming costs and budget.

**Props:**
- sessions (array)
- budget (number)
- period (string)

**Features:**
- Total spent display
- Budget progress bar
- Cost breakdown
- Spending trend
- Budget alerts

## State Management

### Global State (Context/Redux)

#### Grooming State
```javascript
{
  appointments: {
    byId: {
      [appointmentId]: {
        id,
        petId,
        appointmentDate,
        groomerId,
        groomerName,
        serviceType,
        services,
        estimatedDuration,
        price,
        location,
        notes,
        status,
        reminderTime
      }
    },
    allIds: [],
    upcoming: [],
    loading: false,
    error: null
  },
  sessions: {
    byId: {
      [sessionId]: {
        id,
        petId,
        completionDate,
        sessionType,
        groomerName,
        servicesPerformed,
        durationMinutes,
        cost,
        rating,
        photos,
        notes
      }
    },
    allIds: [],
    history: [],
    loading: false,
    error: null
  },
  groomers: {
    byId: {
      [groomerId]: {
        id,
        name,
        businessName,
        rating,
        reviewCount,
        specialties,
        location,
        isMobile,
        priceRange,
        isFavorite
      }
    },
    allIds: [],
    favorites: [],
    loading: false,
    error: null
  },
  recommendations: {
    current: {
      petId,
      recommendedDate,
      priority,
      daysSinceLastGrooming,
      suggestedServices
    },
    dismissed: []
  }
}
```

#### Statistics State
```javascript
{
  groomingStats: {
    totalSessions: 0,
    professionalSessions: 0,
    homeSessions: 0,
    totalSpent: 0,
    averageFrequency: 0,
    lastGroomingDate: null,
    nextRecommendedDate: null,
    loading: false,
    error: null
  }
}
```

#### Preferences State
```javascript
{
  groomingPreferences: {
    [petId]: {
      recommendedFrequency: 8,
      coatType: 'medium',
      preferredGroomer: 'guid',
      specialNeeds: [],
      notifications: {
        enabled: true,
        reminderTimes: [24, 2],
        channels: ['push', 'email']
      },
      budget: {
        monthly: 100,
        alertThreshold: 0.8
      }
    }
  }
}
```

### Local State (Component Level)
- Form state for scheduling/logging
- Modal/drawer open/close states
- Filter and search input values
- Pagination state
- Expanded/collapsed card states
- Photo upload progress
- Date picker states

## API Integration

### API Service Layer
Create grooming service with methods:
- `getAppointments(petId, filters)`
- `getAppointmentById(id)`
- `scheduleAppointment(data)`
- `updateAppointment(id, data)`
- `cancelAppointment(id, data)`
- `completeAppointment(id, data)`
- `getGroomingHistory(petId, filters)`
- `logHomeGrooming(data)`
- `getUpcomingGrooming(petId)`
- `getGroomers(filters)`
- `getGroomerById(id)`
- `getStatistics(petId, period)`
- `getRecommendations(petId)`

### Real-time Updates
- WebSocket connection for appointment updates
- Server-sent events for reminders
- Optimistic UI updates with rollback
- Background sync for offline changes

## Responsive Design

### Mobile (< 768px)
- Single column layout
- Bottom navigation bar
- Swipe gestures for actions
- Full-screen forms/modals
- Simplified calendar view
- Stacked cards
- Large touch targets (min 44x44px)
- Collapsible sections

### Tablet (768px - 1024px)
- Two column layout
- Side drawer for details
- Grid view for cards
- Split view for calendar
- Floating action button
- Modal forms

### Desktop (> 1024px)
- Multi-column layout
- Sidebar navigation
- Modal dialogs
- Full calendar view
- Dashboard with widgets
- Keyboard shortcuts
- Hover effects

## Accessibility

### WCAG 2.1 AA Compliance
- Semantic HTML structure
- ARIA labels and roles
- Keyboard navigation support
- Focus indicators
- Screen reader optimization
- Alt text for all images
- Sufficient color contrast (4.5:1)
- Resizable text up to 200%
- Skip navigation links

### Specific Features
- Date announcements for appointments
- Confirmation dialogs for cancellations
- Error messages with clear instructions
- Form field error associations
- Loading state announcements
- Success/failure notifications read aloud
- Calendar keyboard navigation

## Performance Optimization

### Loading Strategies
- Lazy load components
- Code splitting by route
- Image lazy loading and optimization
- Pagination for history
- Virtual scrolling for long lists
- Thumbnail generation for photos

### Caching
- Cache appointment data locally
- Service worker for offline access
- Cache API responses
- Optimistic updates
- Stale-while-revalidate strategy
- Photo caching

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
- Photo upload < 10 seconds

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
- No personal details in URLs
- Clear session data on logout
- Secure photo storage with access controls

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
- Currency formatting
- Unit conversion
- RTL layout support

## Analytics & Monitoring

### User Analytics
- Page views and routes
- Feature usage tracking
- User flows and funnels
- Appointment booking completion rates
- Form abandonment rates

### Events to Track
- Appointment scheduled
- Appointment completed
- Appointment cancelled
- Home grooming logged
- Groomer favorited
- Photo uploaded
- Report generated
- Recommendation dismissed

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
- Calendar interactions
- Photo upload

### E2E Tests
- Critical user journeys:
  - Schedule appointment
  - Complete appointment
  - Log home grooming
  - View history
  - Cancel appointment
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
