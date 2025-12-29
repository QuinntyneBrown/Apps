# Frontend Requirements - Discovery and Recommendations

## Pages/Views

### Discovery Feed
- URL: `/discover`
- Purpose: Personalized content discovery and recommendations
- Components: RecommendationCarousel, DiscoveryFilters, TrendingSection, ForYouSection

### Recommendations
- URL: `/recommendations`
- Purpose: View and manage received and given recommendations
- Components: ReceivedRecommendations, GivenRecommendations, RecommendationFeedback

### Give Recommendation Modal
- Purpose: Share content recommendations with others
- Components: ContentSelector, RecipientInput, RecommendationForm

## Components

### RecommendationCarousel
- Horizontal scrollable carousel of recommended content
- Shows poster, title, similarity score, match reasons
- Quick actions: Add to watchlist, dismiss, more info
- Props: recommendations, onAddToWatchlist, onDismiss

### RecommendationCard
- Individual recommendation display
- Shows content poster and details
- Displays match reasons (badges/tags)
- Similarity score visualization
- Action buttons
- Props: recommendation, onAction

### ForYouSection
- Personalized recommendations based on viewing history
- Categorized by reason (because you watched X, based on your genre preferences)
- Explanation of why recommended
- Props: recommendations, genrePreferences

### TrendingSection
- Currently trending movies and shows
- Platform-specific trending (Netflix, HBO, etc.)
- Time period selector (today, this week, this month)
- Props: trendingContent, platform, timePeriod

### SimilarContentList
- List of similar content based on a specific item
- Shows similarity percentage
- Match reason tags (same director, similar genre, etc.)
- Props: sourceContent, similarItems

### GiveRecommendationForm
- Form to recommend content to someone
- Content search/selection
- Recipient input (email, phone, username)
- Reason text area
- Sharing method selection
- Props: onSubmit, onCancel

### ReceivedRecommendationsList
- List of recommendations received from others
- Filterable by source (friends, system, critics)
- Shows recommender name and reason
- Interest level indicator
- Actions: Add to watchlist, mark as watched, dismiss
- Props: recommendations, onFilter, onAction

### GivenRecommendationsList
- List of recommendations user has given
- Shows recipient and their feedback
- Status indicators (pending, watched, liked)
- Props: recommendations

### RecommendationFeedbackForm
- Form to provide feedback on received recommendation
- Did you watch it? (yes/no)
- Did you like it? (thumbs up/down)
- Optional feedback text
- Props: recommendation, onSubmit

### GenrePreferenceChart
- Visual representation of user's genre preferences
- Bar chart or radar chart
- Shows preference strength
- Trend indicators (up/down arrows)
- Props: preferences, chartType

### DiscoveryFilters
- Filter discovery feed by genre, release year, platform
- Exclude already watched toggle
- Minimum rating filter
- Props: filters, onFilterChange

### MatchReasonBadge
- Small badge showing why content matches
- Examples: "Same director", "Similar themes", "Fans also liked"
- Color-coded by reason type
- Props: reason, type

### InterestLevelSelector
- Selector for indicating interest in recommendation
- Options: Very interested, Interested, Maybe, Not interested
- Visual indicators (stars, colors, icons)
- Props: interestLevel, onChange

### RecommendationShareButton
- Button to share recommendations externally
- Dropdown for sharing method
- Social media integration
- Email/SMS options
- Props: content, onShare

## State Management

### Discovery Store
```typescript
interface DiscoveryState {
  recommendationsForYou: Recommendation[];
  trending: TrendingContent[];
  similarContent: Map<string, SimilarContent[]>;
  receivedRecommendations: ReceivedRecommendation[];
  givenRecommendations: GivenRecommendation[];
  genrePreferences: GenrePreference[];
  discoveryQueue: DiscoveryItem[];
  filters: DiscoveryFilters;
  loading: boolean;
  error: string | null;
}

interface Recommendation {
  id: string;
  contentId: string;
  contentType: 'Movie' | 'TVShow';
  title: string;
  posterUrl: string;
  year: number;
  genres: string[];
  recommendationScore: number;
  matchReasons: string[];
  source: string;
}

interface ReceivedRecommendation {
  id: string;
  content: ContentSummary;
  recommender: string;
  recommenderSource: string;
  reason: string;
  receptionDate: Date;
  interestLevel: string;
  addedToWatchlist: boolean;
  watched: boolean;
  liked?: boolean;
  feedback?: string;
}

interface GivenRecommendation {
  id: string;
  content: ContentSummary;
  recipient: string;
  reason: string;
  sharingMethod: string;
  shareDate: Date;
  recipientFeedback?: string;
}

interface GenrePreference {
  genre: string;
  preferenceStrength: number;
  viewingCount: number;
  averageRating: number;
  trendDirection: 'Increasing' | 'Stable' | 'Decreasing';
}

interface DiscoveryFilters {
  genres: string[];
  releaseYearMin: number;
  releaseYearMax: number;
  platforms: string[];
  excludeWatched: boolean;
  minRating: number;
}
```

### Actions
- `fetchRecommendations()`: Load personalized recommendations
- `fetchTrending(platform, period)`: Load trending content
- `fetchSimilarContent(contentId)`: Find similar content
- `receiveRecommendation(details)`: Record received recommendation
- `giveRecommendation(details)`: Share recommendation
- `provideFeedback(recommendationId, feedback)`: Give feedback on recommendation
- `addToDiscoveryQueue(contentId)`: Add to discovery queue
- `dismissRecommendation(recommendationId)`: Dismiss recommendation
- `fetchGenrePreferences()`: Load user's genre preferences
- `setDiscoveryFilters(filters)`: Apply discovery filters

## User Interactions

### Discovering Content
1. User navigates to Discovery page
2. Personalized recommendations load
3. User scrolls through recommendations
4. User sees match reasons for each item
5. User can add to watchlist or dismiss
6. User can adjust filters to refine results

### Receiving Recommendations
1. User receives notification of new recommendation
2. User views recommendation with reason
3. User indicates interest level
4. User optionally adds to watchlist
5. After watching, user provides feedback

### Giving Recommendations
1. User clicks "Recommend" on content
2. Modal opens with recommendation form
3. User enters recipient information
4. User writes reason for recommendation
5. User selects sharing method
6. User sends recommendation
7. User can later see recipient feedback

### Viewing Genre Preferences
1. User navigates to preferences section
2. Chart displays genre preferences
3. User sees preference strength and trends
4. User can use this to understand their taste

## Responsive Design

### Desktop (>1024px)
- Multi-column recommendation grid
- Side-by-side received/given recommendations
- Expanded match reasons
- Full chart visualizations

### Tablet (768px - 1024px)
- Two-column grid
- Collapsible filters
- Simplified charts
- Touch-friendly carousels

### Mobile (<768px)
- Single column vertical scrolling
- Swipeable recommendation cards
- Bottom sheet for filters
- Compact preference indicators

## Accessibility

### Keyboard Navigation
- Tab through recommendations
- Arrow keys for carousel navigation
- Enter to view details
- Keyboard shortcuts for quick actions

### Screen Reader Support
- Announce match reasons
- Describe similarity scores
- Read recommendation sources
- Announce filter changes

### Visual Accessibility
- High contrast for scores
- Clear distinction between recommendation sources
- Text alternatives for charts
- Readable font sizes

## Performance

### Optimization
- Lazy load recommendation images
- Virtual scrolling for large lists
- Cache recommendations locally
- Prefetch trending data
- Optimize chart rendering

### Loading States
- Skeleton screens for recommendations
- Progressive loading of carousels
- Inline loading for actions

## Error Handling
- Retry failed recommendation fetches
- Clear error messages
- Fallback to cached recommendations
- Validate recipient information
- Handle sharing failures gracefully

## Analytics Events
- `recommendation_viewed`: Track which recommendations are viewed
- `recommendation_added_to_watchlist`: Track conversion rate
- `recommendation_dismissed`: Track dismissal reasons
- `recommendation_given`: Track sharing activity
- `feedback_provided`: Track feedback quality
- `discovery_filter_applied`: Track popular filters
- `trending_content_clicked`: Track trending engagement
