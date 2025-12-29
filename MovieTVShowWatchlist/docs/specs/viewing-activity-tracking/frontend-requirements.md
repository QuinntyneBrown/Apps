# Frontend Requirements - Viewing Activity Tracking

## Pages/Views

### Viewing History
- URL: `/history`
- Purpose: Display complete viewing history with filters
- Components: HistoryTimeline, HistoryFilters, ViewingStatsSummary

### Show Progress Dashboard
- URL: `/progress/{showId}`
- Purpose: Track progress through a TV series
- Components: SeasonProgressBar, EpisodeChecklist, NextEpisodeCard

### Mark as Watched Modal
- Purpose: Quick interface to mark content as watched
- Components: WatchedForm, CompanionSelector, ContextPicker

## Components

### HistoryTimeline
- Chronological display of viewing activity
- Grouped by date or month
- Shows poster, title, watch date, platform, context
- Filter and search functionality
- Props: viewingRecords, onFilter, dateRange

### MarkAsWatchedButton
- Quick action button on content cards
- Opens mark as watched modal
- Shows confirmation feedback
- Props: content, onComplete

### WatchedForm
- Form to record viewing details
- Fields: Watch date, platform, location, context, companions
- Rewatch checkbox for movies
- Auto-detect binge sessions for TV shows
- Props: content, onSubmit, onCancel

### SeasonProgressBar
- Visual progress bar for each season
- Shows episodes watched vs total
- Click to expand episode list
- Color-coded completion status
- Props: season, progress, onEpisodeClick

### EpisodeChecklist
- List of episodes with checkboxes
- Mark individual episodes as watched
- Shows air dates and episode titles
- Disable future episodes
- Props: episodes, watchedEpisodes, onToggle

### NextEpisodeCard
- Highlighted card for next episode to watch
- "Watch Next" action button
- Episode thumbnail and description
- Availability information
- Props: episode, onMarkWatched

### ViewingStatsSummary
- Dashboard widget with key statistics
- Total movies/shows/episodes watched
- Total viewing time
- Current streak
- This week/month comparison
- Props: statistics, period

### BingeSessionIndicator
- Badge showing binge viewing sessions
- Episode count in session
- Session duration
- Props: session, episodes

### AbandonContentModal
- Dialog to mark content as abandoned
- Dropdown for abandon reason
- Progress slider
- Quality rating
- Would retry checkbox
- Props: content, progress, onSubmit, onCancel

### ViewingCompanionSelector
- Tag input for people watched with
- Autocomplete from previous companions
- Add new companions
- Props: companions, onCompanionChange

### ViewingContextPicker
- Radio buttons or dropdown for context
- Options: Theater, Home Streaming, DVD/Blu-ray, TV Broadcast
- Props: selectedContext, onChange

### ShowCompletionBadge
- Celebratory badge for completed shows
- Shows completion date
- Total episodes and viewing time
- Share completion option
- Props: show, completionData

## State Management

### Viewing Store
```typescript
interface ViewingState {
  viewingHistory: ViewingRecord[];
  showProgress: Map<string, ShowProgress>;
  binges Sessions: BingeSession[];
  statistics: ViewingStatistics;
  loading: boolean;
  error: string | null;
}

interface ViewingRecord {
  id: string;
  contentId: string;
  contentType: 'Movie' | 'Episode';
  title: string;
  watchDate: Date;
  platform: string;
  location: string;
  context: string;
  watchedWith: string[];
  isRewatch: boolean;
  posterUrl: string;
  episodeInfo?: {
    showId: string;
    seasonNumber: number;
    episodeNumber: number;
    bingeSessionId?: string;
  };
}

interface ShowProgress {
  showId: string;
  showTitle: string;
  totalEpisodes: number;
  episodesWatched: number;
  completedSeasons: number;
  lastWatchedSeason: number;
  lastWatchedEpisode: number;
  nextEpisode: Episode;
  isCompleted: boolean;
  completionDate?: Date;
}

interface ViewingStatistics {
  totalMovies: number;
  totalShows: number;
  totalEpisodes: number;
  totalHours: number;
  currentStreak: number;
  longestStreak: number;
  thisWeek: number;
  thisMonth: number;
  byGenre: Map<string, number>;
  byPlatform: Map<string, number>;
}
```

### Actions
- `markMovieWatched(movieId, details)`: Record movie viewing
- `markEpisodeWatched(episodeId, details)`: Record episode viewing
- `abandonContent(contentId, details)`: Mark as abandoned
- `fetchViewingHistory(filters)`: Load viewing history
- `fetchShowProgress(showId)`: Load show progress
- `fetchStatistics(period)`: Load viewing statistics

## User Interactions

### Marking Movie as Watched
1. User clicks "Mark as Watched" on movie
2. Modal opens with viewing details form
3. User enters watch date, platform, context, companions
4. User checks rewatch if applicable
5. User clicks "Save"
6. Success animation and confirmation
7. Movie moves to viewing history

### Tracking TV Show Progress
1. User navigates to show progress page
2. Season progress bars display
3. User clicks on episode to mark as watched
4. Episode checkbox toggles
5. Progress bar updates
6. Next episode card highlights next to watch
7. Completion detected and celebrated

### Abandoning Content
1. User clicks "Abandon" or "DNF" button
2. Modal opens with abandon form
3. User selects reason and rates
4. User indicates if they would retry
5. User confirms
6. Content moved to abandoned list
7. Removed from active watching

## Responsive Design

### Desktop (>1024px)
- Multi-column history layout
- Sidebar with statistics
- Inline episode marking
- Expanded progress visualization

### Tablet (768px - 1024px)
- Two-column history layout
- Collapsible stats panel
- Touch-optimized checkboxes

### Mobile (<768px)
- Single column timeline
- Bottom sheet for details
- Swipe to mark as watched
- Compact progress indicators

## Accessibility

### Keyboard Navigation
- Tab through episodes in checklist
- Space to toggle watched status
- Enter to open details
- Keyboard date picker

### Screen Reader Support
- Announce progress updates
- Describe completion status
- Read episode details
- Announce binge sessions

## Performance

### Optimization
- Virtual scrolling for long history (>500 items)
- Lazy load episode details
- Cache show progress locally
- Debounce episode marking (prevent double-clicks)
- Batch episode updates

### Loading States
- Skeleton screens for history
- Progress bar loading animation
- Inline spinners for actions

## Error Handling
- Retry failed viewing records
- Offline queuing of watch actions
- Conflict resolution for concurrent updates
- Clear error messages

## Analytics Events
- `movie_watched`: Track movies marked as watched
- `episode_watched`: Track episode completions
- `season_completed`: Track season completions
- `show_completed`: Track series completions
- `content_abandoned`: Track abandon reasons
- `binge_session_detected`: Track binging behavior
