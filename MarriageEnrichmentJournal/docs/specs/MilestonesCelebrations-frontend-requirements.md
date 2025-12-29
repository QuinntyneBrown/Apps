# Frontend Requirements - Milestones & Celebrations

## Pages/Views

### Relationship Timeline
**Route**: `/milestones/timeline`

**Components**:
- TimelineVisualization
- MilestoneMarker
- FilterControls
- AddMilestoneButton

**Features**:
- Visual chronological timeline
- Milestones displayed with photos
- Filter by type, year
- Click to view details
- Scroll to specific date
- Add new milestone

### Create Milestone Page
**Route**: `/milestones/create`

**Components**:
- MilestoneForm
- TypeSelector
- DatePicker
- PhotoUploader
- SignificanceEditor

**Fields**:
- Milestone type (dropdown)
- Date (date picker)
- Title (text input)
- Significance (textarea)
- Celebration notes (textarea)
- Photos (multi-upload)

### Milestone Detail View
**Component**: `MilestoneDetailPage`

**Display**:
- Title and date
- Type badge
- Significance text
- Celebration notes
- Photo gallery
- Edit/delete options

### Achievements Gallery
**Route**: `/achievements`

**Components**:
- AchievementGrid
- BadgeDisplay
- ProgressIndicators
- LockedAchievements

**Features**:
- Display earned badges
- Show locked (not yet earned) achievements
- Progress bars for next milestone
- Achievement details on hover/click
- Share achievement option

### Upcoming Reminders
**Component**: `ReminderWidget` (dashboard widget)

**Display**:
- Next 3 upcoming milestones
- Days until each
- Quick action to view details
- "View All" link

## Components

### TimelineVisualization
**Type**: Interactive vertical timeline
**Features**:
- Year markers
- Milestone nodes
- Photo thumbnails
- Type icons
- Smooth scrolling

### MilestoneMarker
**Display**:
- Date
- Title
- Type icon and color
- Thumbnail (if photo exists)
- Click to expand

### PhotoUploader
**Features**:
- Drag and drop
- Multi-select
- Preview thumbnails
- Progress indicators
- Reorder photos
- Add captions

### BadgeDisplay
**Props**:
- `achievement` (object)
- `size` (small|medium|large)
- `locked` (boolean)

**Display**:
- Badge icon/image
- Title
- Description on hover
- Locked state (grayscale)
- Unlock animation

### AchievementCelebration
**Component**: Modal with confetti animation
**Trigger**: When achievement unlocked
**Display**:
- Badge icon
- Congratulations message
- Achievement details
- Share button
- Close button

## State Management

### milestonesSlice
```typescript
{
  milestones: {
    items: [],
    timeline: [],
    loading: boolean,
    error: string | null
  },
  achievements: {
    earned: [],
    locked: [],
    recent: Achievement | null,
    loading: boolean
  },
  reminders: {
    upcoming: [],
    dismissed: [],
    loading: boolean
  }
}
```

## Real-time Features
- Celebration animation when achievement unlocked
- Notification when spouse adds milestone
- Reminder notifications (push if enabled)

## Responsive Design
- Mobile: Vertical timeline, swipeable photos
- Desktop: Horizontal or vertical timeline option
- Touch-optimized photo gallery

## Animations

### Celebration Effects
- Confetti animation for achievements
- Badge unlock animation (scale + glow)
- Timeline entry appear animation
- Photo upload success checkmark

### Transitions
- Smooth scroll to timeline date
- Fade in/out for modals
- Slide in for notifications

## Accessibility
- Alt text for all photos
- Keyboard navigation through timeline
- Screen reader announcements for achievements
- Focus management in modals
