# Frontend Requirements - Gratitude Journaling

## Pages/Views

### Gratitude Feed (Main View)
**Route**: `/gratitude`

**Components**:
- GratitudeFeedContainer
- GratitudeEntryCard
- CreateGratitudeButton
- FilterBar
- StreakDisplay

**Features**:
- Display list of gratitude entries in reverse chronological order
- Show author, date, content preview, category badge
- Indicate whether entry is private or shared
- Show acknowledgment status on shared entries
- Infinite scroll or pagination
- Filter by category, date range, shared/private status
- Pull-to-refresh on mobile

**State Management**:
- Current filter settings
- Loaded entries list
- Loading state
- Pagination metadata
- Current streak information

### Create Gratitude Entry Modal
**Component**: `CreateGratitudeModal`

**Fields**:
- Content textarea (required, max 2000 chars, character counter)
- Category selection (radio buttons: Action, Quality, Moment)
- Privacy level (toggle: Private / Share with Spouse)
- Category descriptions/examples

**Validation**:
- Content required and non-empty
- Category selection required
- Show inline validation errors

**UX**:
- Autofocus on content field when opened
- Save draft to local storage
- Confirm before closing if unsaved changes
- Success animation after creation
- Option to create another entry after success

### Entry Detail View
**Component**: `GratitudeEntryDetail`

**Display**:
- Full entry content
- Author and date
- Category badge
- Privacy status
- Acknowledgment section (if shared and acknowledged)

**Actions** (author only):
- Share entry (if private)
- Delete entry (with confirmation)
- Edit content (within 1 hour of creation)

**Actions** (spouse, if shared):
- Acknowledge entry
- Add response message
- Select emotional reaction

### Streak Dashboard
**Component**: `StreakDashboard`

**Metrics Display**:
- Current streak number (large, prominent)
- Longest streak achieved
- Total entries count
- Progress bar to next milestone
- Calendar heatmap of entry days
- Achievement badges earned

**Visual Design**:
- Celebration animations for milestones
- Progress indicators
- Motivational messaging
- Achievement unlock notifications

### Acknowledgment Interface
**Component**: `AcknowledgeGratitudeForm`

**Fields**:
- Emotional reaction selector (icons: grateful, loved, happy, touched, surprised)
- Optional response message (textarea, max 500 chars)

**UX**:
- Quick-acknowledge with just reaction tap
- Optional to add message
- Smooth animation on submit
- Notify partner in real-time if online

## Components

### GratitudeEntryCard
**Props**:
- entry (object)
- onAcknowledge (function)
- onShare (function)
- onDelete (function)

**Display**:
- Entry content (truncated if long, expand on click)
- Category badge with icon
- Author name and avatar
- Timestamp (relative, e.g., "2 hours ago")
- Privacy indicator icon
- Acknowledgment count/status

**Interactions**:
- Click to view full details
- Quick acknowledge button (if spouse's shared entry)
- Share button (if own private entry)
- More actions menu

### CategoryBadge
**Props**:
- category (string)

**Display**:
- Color-coded badge
- Icon for category
- Category name

**Styling**:
- Action: Blue
- Quality: Green
- Moment: Purple

### StreakCounter
**Props**:
- currentStreak (number)
- size (string: small|medium|large)

**Display**:
- Flame/fire icon
- Streak number
- Label "day streak"

**Animations**:
- Pulse animation when streak increases
- Celebration confetti at milestones

## State Management

### Redux Store Slices

#### gratitudeSlice
```typescript
{
  entries: {
    items: GratitudeEntry[],
    loading: boolean,
    error: string | null,
    hasMore: boolean,
    currentPage: number
  },
  filters: {
    category: string | null,
    privacyLevel: string | null,
    startDate: Date | null,
    endDate: Date | null
  },
  streak: {
    currentStreak: number,
    longestStreak: number,
    totalEntries: number,
    milestones: number[],
    loading: boolean
  }
}
```

### Actions
- `fetchGratitudeEntries(filters)`
- `createGratitudeEntry(data)`
- `shareGratitudeEntry(id)`
- `acknowledgeGratitudeEntry(id, data)`
- `deleteGratitudeEntry(id)`
- `updateFilters(filters)`
- `fetchStreakData()`

## Real-time Features

### WebSocket Events

#### Subscriptions
- Subscribe to spouse's gratitude shares
- Subscribe to acknowledgments of own entries
- Subscribe to streak achievements

#### Event Handlers
- `gratitude.shared`: Show notification, update feed
- `gratitude.acknowledged`: Show notification, update entry
- `streak.milestone`: Show celebration modal

## Responsive Design

### Mobile (< 768px)
- Single column feed
- Bottom navigation
- Floating action button for create
- Swipe gestures for actions
- Full-screen create modal

### Tablet (768px - 1024px)
- Two-column feed
- Side panel for filters
- Modal for create (not full-screen)

### Desktop (> 1024px)
- Three-column layout (filters, feed, streak sidebar)
- Keyboard shortcuts (N for new entry, F for filter)
- Hover effects on cards

## Accessibility

### Requirements
- ARIA labels on all interactive elements
- Keyboard navigation support
- Focus management in modals
- Screen reader announcements for dynamic content
- Sufficient color contrast (WCAG AA)
- Text alternatives for icons

### Keyboard Shortcuts
- `N`: New gratitude entry
- `F`: Focus filter bar
- `Esc`: Close modal/clear filters
- `Arrow keys`: Navigate entries

## Performance

### Optimization
- Lazy load images
- Virtual scrolling for large lists
- Debounce filter inputs
- Optimistic UI updates
- Cache entry list locally
- Prefetch next page on scroll

### Metrics
- Time to interactive: <2 seconds
- First contentful paint: <1 second
- Smooth scrolling at 60fps

## Error Handling

### Error States
- Network error: Show retry button
- No entries: Empty state with motivational CTA
- Failed create: Show error, preserve draft
- Failed acknowledgment: Show error, allow retry

### User Feedback
- Loading spinners during async operations
- Success toasts for completed actions
- Error messages with clear next steps
- Confirmation dialogs for destructive actions
