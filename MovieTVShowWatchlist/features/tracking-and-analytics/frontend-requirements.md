# Frontend Requirements - Tracking and Analytics

## Pages/Views

### Statistics Dashboard
- URL: `/statistics`
- Purpose: View comprehensive viewing statistics and analytics
- Components: StatsSummaryCards, ViewingTimeChart, GenreBreakdown, PlatformDistribution

### Year in Review
- URL: `/year-in-review/{year}`
- Purpose: View annual viewing summary
- Components: YearInReviewBanner, TopContentCards, ViewingTrendsChart, ShareButton

### Milestones
- URL: `/milestones`
- Purpose: View achieved and upcoming milestones
- Components: MilestoneTimeline, NextMilestoneProgress, CelebrationBadges

## Components

### StatsSummaryCards
- Grid of key statistics cards
- Total movies/shows/episodes watched
- Total viewing hours
- Current streak
- Average rating
- Props: statistics, period

### ViewingStreakBadge
- Visual display of current viewing streak
- Flame icon with streak count
- Longest streak indicator
- Streak status (active/broken)
- Props: currentStreak, longestStreak, lastWatched

### ViewingTimeChart
- Line or bar chart showing viewing over time
- Selectable time periods (week, month, year, all)
- Breakdown by content type
- Comparison to previous period
- Props: data, period, onPeriodChange

### GenreBreakdownChart
- Pie or bar chart of genre distribution
- Shows percentage and hours per genre
- Clickable to filter content by genre
- Props: genreData, onGenreClick

### PlatformDistributionChart
- Chart showing viewing by platform
- Platform logos with usage stats
- Subscription cost vs usage analysis
- Props: platformData, subscriptions

### MilestoneCard
- Card displaying a milestone achievement
- Celebration animation on first view
- Achievement date and details
- Share button
- Props: milestone, isNew, onShare

### MilestoneTimeline
- Chronological list of achieved milestones
- Progress indicators for next milestones
- Filter by milestone type
- Props: milestones, nextMilestones

### NextMilestoneProgress
- Progress bar toward next milestone
- Countdown (e.g., "23 more to reach 100 movies")
- Estimated time to achievement
- Props: currentCount, nextMilestone

### YearInReviewBanner
- Hero banner with year and total stats
- Animated numbers counting up
- Background image or gradient
- Props: year, statistics

### TopContentCards
- Showcase of top-rated/most-watched content
- Category tabs (highest rated, most rewatched, etc.)
- Content posters and details
- Props: topContent, categories

### ViewingTrendsChart
- Visualization of viewing patterns over year
- Monthly breakdown
- Seasonal trends
- Peak viewing periods
- Props: monthlyData, year

### ShareYearInReviewButton
- Button to share year-in-review
- Generate shareable image/link
- Social media integration
- Props: yearData, onShare

### StatsPeriodSelector
- Dropdown or tabs for time period selection
- Options: This Week, This Month, This Year, All Time, Custom Range
- Date range picker for custom
- Props: selectedPeriod, onPeriodChange

### ComparisonIndicator
- Shows increase/decrease vs previous period
- Percentage change with up/down arrow
- Color-coded (green increase, red decrease)
- Props: currentValue, previousValue, metric

### AverageRatingDisplay
- Large display of average user rating
- Star visualization
- Count of ratings given
- Distribution histogram
- Props: averageRating, totalRatings, distribution

## State Management

```typescript
interface AnalyticsState {
  statistics: ViewingStatistics;
  streak: ViewingStreak;
  milestones: Milestone[];
  nextMilestones: NextMilestone[];
  yearInReview: YearInReview | null;
  selectedPeriod: TimePeriod;
  loading: boolean;
  error: string | null;
}

interface ViewingStatistics {
  totalMovies: number;
  totalShows: number;
  totalEpisodes: number;
  totalHours: number;
  averageRating: number;
  genreBreakdown: Map<string, number>;
  platformDistribution: Map<string, number>;
  viewingOverTime: TimeSeriesData[];
  comparisonToPrevious: number;
}

interface ViewingStreak {
  currentStreak: number;
  longestStreak: number;
  lastWatchedDate: Date;
  status: 'Active' | 'Broken';
}

interface Milestone {
  id: string;
  type: string;
  metricAchieved: number;
  achievementDate: Date;
  celebrationTier: string;
  description: string;
}

interface YearInReview {
  year: number;
  totalMovies: number;
  totalShows: number;
  totalHours: number;
  topGenres: string[];
  topRatedContent: ContentSummary[];
  viewingTrends: any;
  memorableMoments: string[];
}
```

### Actions
- `fetchStatistics(period)`: Load viewing statistics
- `fetchStreak()`: Load current streak
- `fetchMilestones()`: Load milestones
- `fetchYearInReview(year)`: Load year-in-review
- `shareYearInReview(year)`: Share year-in-review
- `setPeriod(period)`: Change statistics period

## Responsive Design
- Responsive charts that adapt to screen size
- Stacked cards on mobile
- Horizontal scrolling for time charts on small screens

## Analytics Events
- `statistics_viewed`: Track dashboard views
- `year_in_review_generated`: Track year-in-review views
- `year_in_review_shared`: Track sharing
- `milestone_viewed`: Track milestone engagement
- `period_changed`: Track popular time periods
