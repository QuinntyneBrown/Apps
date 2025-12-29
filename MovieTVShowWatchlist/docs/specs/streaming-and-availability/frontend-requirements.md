# Frontend Requirements - Streaming and Availability

## Pages/Views

### Streaming Services
- URL: `/subscriptions`
- Purpose: Manage streaming subscriptions and view ROI
- Components: SubscriptionsList, AddSubscriptionForm, ROIAnalytics

### Watch Parties
- URL: `/watch-parties`
- Purpose: Schedule and manage watch parties
- Components: WatchPartyCalendar, SchedulePartyForm, ParticipantsList

## Components

### AvailabilityBadge
- Shows where content is currently available
- Platform logos with availability status
- Subscription requirement indicator
- Click to view full availability details
- Props: availability, contentId

### StreamingPlatformSelector
- Multi-select for filtering by platform
- Platform logos/icons
- Show only available on selected platforms
- Props: platforms, selectedPlatforms, onChange

### SubscriptionCard
- Display subscription details
- Platform logo, tier, cost
- Usage statistics (content watched via this subscription)
- ROI indicator
- Cancel/edit buttons
- Props: subscription, usage, onEdit, onCancel

### AddSubscriptionForm
- Form to add new subscription
- Platform dropdown
- Tier selection
- Monthly cost input
- Start date picker
- Props: onSubmit, onCancel

### ROIAnalyticsCard
- Shows subscription value analysis
- Cost per content watched
- Utilization percentage
- Recommendations for optimization
- Props: subscriptions, usage

### ScheduleWatchPartyModal
- Modal to schedule a watch party
- Content selection
- Date/time picker
- Participant email/name inputs
- Platform selection
- Discussion plan text area
- Props: onSubmit, onCancel

### WatchPartyCard
- Display watch party details
- Content poster and title
- Date, time, host
- Participant list with RSVP status
- Join/edit/cancel buttons
- Props: watchParty, editable, onJoin, onCancel

### WatchPartyCalendar
- Calendar view of scheduled watch parties
- Month/week/day views
- Click date to schedule new party
- Click event to view details
- Props: watchParties, onDateClick, onEventClick

### ParticipantsList
- List of watch party participants
- RSVP status indicators
- Add/remove participant actions
- Send reminder button
- Props: participants, onAdd, onRemove

### NewEpisodeAlert
- Notification card for new episode
- Episode details (show, season, episode)
- Available on platform(s)
- Quick actions: Add to queue, mark as watched, dismiss
- Props: episode, onAction

### EpisodeAlertSubscription
- Toggle to enable/disable episode alerts for a show
- Notification method selection (email, push, SMS)
- Props: showId, isSubscribed, onToggle

### AvailabilityTimeline
- Timeline showing content availability changes
- When content was added/removed from platforms
- Upcoming removals/expiry warnings
- Props: contentId, availabilityHistory

### PlatformUsageChart
- Chart showing content watched per platform
- Comparison to subscription cost
- Identifies most/least used subscriptions
- Props: platformUsage, subscriptions

## State Management

```typescript
interface StreamingState {
  availability: Map<string, Availability[]>;
  subscriptions: Subscription[];
  watchParties: WatchParty[];
  episodeAlerts: EpisodeAlert[];
  loading: boolean;
  error: string | null;
}

interface Availability {
  platform: string;
  platformLogo: string;
  isAvailable: boolean;
  subscriptionRequired: boolean;
  availabilityWindow?: string;
  expiryDate?: Date;
  regionalRestrictions?: string[];
}

interface Subscription {
  id: string;
  platform: string;
  tier: string;
  monthlyCost: number;
  startDate: Date;
  endDate?: Date;
  status: string;
  contentWatchedCount: number;
  roi: number;
}

interface WatchParty {
  id: string;
  content: ContentSummary;
  scheduledDateTime: Date;
  platform: string;
  host: string;
  participants: Participant[];
  discussionPlan?: string;
  status: string;
}

interface Participant {
  name: string;
  email?: string;
  rsvpStatus: 'Pending' | 'Accepted' | 'Declined';
}

interface EpisodeAlert {
  id: string;
  show: ContentSummary;
  episode: EpisodeDetails;
  releaseDate: Date;
  platform: string;
  notificationDelivered: Date;
}
```

### Actions
- `fetchAvailability(contentId)`: Load content availability
- `addSubscription(details)`: Add new subscription
- `updateSubscription(id, details)`: Update subscription
- `cancelSubscription(id)`: Cancel subscription
- `scheduleWatchParty(details)`: Create watch party
- `updateWatchParty(id, details)`: Update watch party
- `cancelWatchParty(id)`: Cancel watch party
- `rsvpWatchParty(id, status)`: RSVP to watch party
- `subscribeToEpisodeAlerts(showId, method)`: Enable episode alerts
- `unsubscribeFromEpisodeAlerts(showId)`: Disable episode alerts

## User Interactions

### Checking Availability
1. User views content details
2. Availability badges show streaming platforms
3. User clicks for detailed availability
4. Modal shows all platforms, pricing, expiry dates
5. User can add content to watchlist or filter by their subscriptions

### Managing Subscriptions
1. User navigates to Subscriptions page
2. Current subscriptions display with usage stats
3. User can add new subscription
4. User reviews ROI analytics
5. System suggests optimization (e.g., cancel unused)

### Scheduling Watch Party
1. User clicks "Schedule Watch Party" on content
2. Modal opens with scheduling form
3. User selects date/time
4. User adds participants (email addresses)
5. User selects viewing platform
6. System sends calendar invites
7. Watch party appears in calendar

### Receiving Episode Alerts
1. New episode releases
2. User receives notification
3. User can view episode details
4. User can add to queue or mark as watched
5. User can dismiss or snooze alert

## Responsive Design
- Responsive subscription cards
- Mobile-friendly calendar
- Touch-optimized watch party creation
- Bottom sheet for availability details on mobile

## Analytics Events
- `availability_checked`: Track availability lookups
- `subscription_added`: Track subscription additions
- `watch_party_scheduled`: Track watch party creation
- `watch_party_rsvp`: Track RSVP engagement
- `episode_alert_enabled`: Track alert subscriptions
- `episode_alert_interacted`: Track notification interactions
