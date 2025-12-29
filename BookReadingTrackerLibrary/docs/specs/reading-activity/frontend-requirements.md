# Frontend Requirements - Reading Activity

## Pages/Views

### 1. Currently Reading View
**Route**: `/reading/current`

**Components**:
- CurrentlyReadingGrid - Grid of books currently being read
- ProgressCard - Individual book progress card
- QuickUpdateButton - Fast progress update
- StartReadingButton - Start new book

**State Management**:
- currentlyReading: ReadingSession[]
- isLoading: boolean

**User Actions**:
- View all currently reading books
- Update progress quickly
- Start reading a new book
- View detailed progress

### 2. Progress Update Modal
**Component**: `ProgressUpdateModal`

**Form Fields**:
- Current Page (number input, required)
- Update Notes (textarea, optional)

**Features**:
- Shows current progress visually
- Calculates pages read since last update
- Updates reading pace estimate
- Shows estimated completion date

**User Actions**:
- Enter current page number
- Add optional notes
- Save progress update

### 3. Complete Reading Modal
**Component**: `CompleteReadingModal`

**Form Fields**:
- Completion Date (date picker, default today)
- Total Reading Time (optional)
- Rating (1-5 stars, optional)
- Would Reread (checkbox)
- Emotional Impact (textarea)

**User Actions**:
- Confirm completion
- Rate the book
- Share thoughts
- Mark for rereading

### 4. Reading History View
**Route**: `/reading/history`

**Components**:
- ReadingHistoryTable - Chronological list
- YearSelector - Filter by year
- StatusFilter - Filter by completed/abandoned
- StatisticsPanel - Summary statistics

**State Management**:
- readingHistory: ReadingSession[]
- filters: HistoryFilters
- statistics: ReadingStatistics

**User Actions**:
- Browse reading history
- Filter by year and status
- View reading statistics
- Re-read books

### 5. Reading Statistics Dashboard
**Route**: `/reading/statistics`

**Components**:
- TotalBooksCard - Books completed this year
- TotalPagesCard - Pages read this year
- ReadingPaceChart - Pages per day over time
- CompletionTimeChart - Time to complete books
- GenreBreakdownChart - Books by genre
- ReadingStreakCard - Current and longest streak
- MonthlyProgressChart - Books per month

**State Management**:
- statistics: ReadingStatistics
- timeRange: 'month' | 'year' | 'all-time'

**User Actions**:
- View comprehensive statistics
- Change time range
- Export reading data
- Share achievements

### 6. Reading Streak Widget
**Component**: `ReadingStreakWidget`

**Display**:
- Current streak count
- Longest streak record
- Calendar view of reading days
- Streak status indicator

**Features**:
- Visual calendar highlighting reading days
- Streak milestones
- Encouragement messages

## UI Components

### ProgressCard
- Book cover and title
- Progress bar with percentage
- Current page / Total pages
- Reading pace (pages/day)
- Estimated completion date
- Quick update button

### ReadingPaceIndicator
- Visual gauge showing pace
- Comparison to personal average
- Trend indicator (speeding up/slowing down)

### ReadingStreakCalendar
- Monthly calendar view
- Highlighted reading days
- Streak count display
- Milestone indicators

## State Management

### Reading Store
```typescript
interface ReadingState {
  currentlyReading: ReadingSession[];
  readingHistory: ReadingSession[];
  statistics: ReadingStatistics;
  streak: ReadingStreak;
  isLoading: boolean;
  error: string | null;
}

interface ReadingStatistics {
  totalBooksRead: number;
  totalPagesRead: number;
  averageReadingSpeed: number;
  totalReadingTime: number;
  booksByGenre: { [genre: string]: number };
  booksByMonth: { [month: string]: number };
}
```

### Actions
- fetchCurrentlyReading()
- startReading(bookId, startInfo)
- updateProgress(sessionId, currentPage)
- completeReading(sessionId, completionInfo)
- abandonReading(sessionId, abandonInfo)
- fetchReadingHistory(filters)
- fetchStatistics(timeRange)
- fetchReadingStreak()

## Validation

1. Current page must be between 0 and total pages
2. Progress updates must be chronological
3. Completion date cannot be before start date
4. Rating must be between 1 and 5 (if provided)

## Responsive Design

### Desktop
- Multi-column progress cards
- Expanded statistics charts
- Side-by-side comparisons

### Mobile
- Single column cards
- Simplified charts
- Bottom sheet for updates
