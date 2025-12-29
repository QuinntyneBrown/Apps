# Frontend Requirements - Rating and Review Management

## Pages/Views

### My Ratings
- URL: `/ratings`
- Purpose: View and manage all user ratings
- Components: RatingsGrid, RatingFilter, RatingStatistics

### My Reviews
- URL: `/reviews`
- Purpose: View and manage written reviews
- Components: ReviewsList, ReviewEditor, SpoilerToggle

### My Favorites
- URL: `/favorites`
- Purpose: Manage favorite content collection
- Components: FavoritesGallery, CategoryFilter, RewatchTracker

### Rate and Review Modal
- Purpose: Combined interface for rating and reviewing content
- Components: StarRating, ReviewTextArea, ThemeSelector, SpoilerCheckbox

## Components

### StarRating
- Interactive star rating component
- Support for half-stars or decimal ratings
- Configurable scale (5-star, 10-point, etc.)
- Display-only mode for showing ratings
- Mood indicator integration
- Props: value, scale, readonly, onChange, showMood

### RatingSlider
- Alternative to star rating for granular control
- Numeric display of rating value
- Color-coded by rating level
- Props: value, min, max, step, onChange

### ReviewTextArea
- Rich text editor for writing reviews
- Character count (50-10000)
- Formatting toolbar (bold, italic, lists)
- Spoiler text highlighting
- Auto-save draft functionality
- Props: value, onChange, minLength, maxLength

### SpoilerToggle
- Checkbox to mark review as containing spoilers
- Warning indicator when enabled
- Tooltip explaining spoiler policy
- Props: hasSpoilers, onChange

### ThemeSelector
- Multi-select for review themes
- Suggested themes based on content
- Custom theme entry
- Tag-based UI
- Props: selectedThemes, onThemeChange, suggestions

### ReviewCard
- Display formatted review
- Show/hide spoiler button
- Edit/delete actions for own reviews
- Themes displayed as tags
- Recommendation indicator
- Props: review, editable, onEdit, onDelete

### FavoriteButton
- Heart icon to mark/unmark favorites
- Animated state change
- Shows favorite status
- Quick category assignment
- Props: contentId, isFavorite, onToggle

### FavoriteCategoryBadge
- Visual indicator of favorite category
- Color-coded categories
- Click to filter by category
- Props: category, clickable, onClick

### RatingStatisticsWidget
- Shows user's rating distribution
- Average rating display
- Highest/lowest rated content
- Rating trends over time
- Props: statistics, timeRange

### RatingsGrid
- Grid display of all user ratings
- Sortable by rating value, date, title
- Filterable by content type, rating range
- Visual rating indicators
- Props: ratings, onSort, onFilter

### RewatchCounter
- Badge showing rewatch count for favorites
- Increment button for tracking rewatches
- Props: count, onIncrement

### MoodIndicator
- Visual representation of mood during rating
- Icon or color-coded display
- Dropdown to select mood
- Props: mood, editable, onChange

### TargetAudienceSelector
- Dropdown or tags for target audience
- Options: Family-friendly, Adults, Genre fans, etc.
- Multiple selection support
- Props: selectedAudiences, onChange

### RecommendationToggle
- Switch to indicate if user would recommend
- Thumbs up/down or yes/no toggle
- Props: wouldRecommend, onChange

## State Management

### Ratings Store
```typescript
interface RatingsState {
  ratings: Rating[];
  reviews: Review[];
  favorites: Favorite[];
  statistics: RatingStatistics;
  loading: boolean;
  error: string | null;
}

interface Rating {
  id: string;
  contentId: string;
  contentType: 'Movie' | 'TVShow';
  title: string;
  posterUrl: string;
  ratingValue: number;
  ratingScale: string;
  ratingDate: Date;
  viewingDate?: Date;
  isRewatchRating: boolean;
  mood?: string;
}

interface Review {
  id: string;
  contentId: string;
  contentType: 'Movie' | 'TVShow';
  title: string;
  reviewText: string;
  hasSpoilers: boolean;
  reviewDate: Date;
  themesDiscussed: string[];
  wouldRecommend: boolean;
  targetAudience: string;
  relatedRating?: Rating;
}

interface Favorite {
  id: string;
  contentId: string;
  contentType: 'Movie' | 'TVShow';
  title: string;
  posterUrl: string;
  addedDate: Date;
  category: string;
  rewatchCount: number;
  emotionalSignificance: string;
  rating?: Rating;
}

interface RatingStatistics {
  totalRatings: number;
  averageRating: number;
  ratingDistribution: Map<number, number>;
  highestRated: Rating[];
  lowestRated: Rating[];
  mostRewatched: Favorite[];
}
```

### Actions
- `rateMovie(movieId, ratingDetails)`: Rate a movie
- `rateTVShow(showId, ratingDetails)`: Rate a TV show
- `updateRating(ratingId, newValue, mood)`: Update existing rating
- `deleteRating(ratingId)`: Delete a rating
- `writeReview(contentId, reviewDetails)`: Create a review
- `updateReview(reviewId, reviewDetails)`: Update review
- `deleteReview(reviewId)`: Delete review
- `addToFavorites(contentId, details)`: Mark as favorite
- `removeFromFavorites(favoriteId)`: Remove from favorites
- `incrementRewatch(favoriteId)`: Increment rewatch counter
- `fetchRatings(filters)`: Load user ratings
- `fetchReviews(filters)`: Load user reviews
- `fetchFavorites(filters)`: Load favorites
- `fetchStatistics()`: Load rating statistics

## User Interactions

### Rating Content
1. User clicks rate button on content
2. Rating modal opens
3. User selects star rating
4. User optionally selects mood
5. User clicks "Save Rating"
6. Success confirmation displays
7. Rating appears in user's ratings list

### Writing a Review
1. User clicks "Write Review" button
2. Review editor opens
3. User writes review text (min 50 chars)
4. User selects themes discussed
5. User checks spoiler warning if applicable
6. User indicates recommendation status
7. User specifies target audience
8. User clicks "Submit Review"
9. Review is published

### Marking as Favorite
1. User clicks heart icon on content
2. Favorite category selector appears
3. User selects category (or creates new)
4. User optionally adds emotional significance note
5. Content added to favorites
6. Heart icon fills to indicate favorite status

### Managing Favorites
1. User navigates to Favorites page
2. Favorites display in grid/gallery
3. User can filter by category
4. User can increment rewatch count
5. User can edit or remove favorites

## Responsive Design

### Desktop (>1024px)
- Multi-column rating grid
- Side-by-side rating and review forms
- Expanded review cards with full text
- Favorites in gallery layout

### Tablet (768px - 1024px)
- Two-column layouts
- Collapsible review text
- Touch-friendly rating controls

### Mobile (<768px)
- Single column lists
- Bottom sheet for rating/review forms
- Simplified favorite cards
- Tap to expand reviews

## Accessibility

### Keyboard Navigation
- Tab through star ratings
- Arrow keys to adjust rating value
- Enter to save rating
- Keyboard shortcuts for text formatting

### Screen Reader Support
- Announce rating value changes
- Describe spoiler warnings
- Read review content
- Announce favorite status changes

### Visual Accessibility
- High contrast star ratings
- Clear spoiler warnings
- Text alternatives for mood icons
- Minimum touch target size

## Performance

### Optimization
- Lazy load review text
- Virtual scrolling for large rating lists
- Debounce auto-save for drafts (2s)
- Cache ratings and favorites locally
- Optimize image loading for posters

### Loading States
- Skeleton screens for ratings grid
- Inline loading for rating updates
- Progress indicator for review submission

## Error Handling
- Validate review length before submission
- Handle network failures gracefully
- Show clear error messages
- Auto-save drafts to prevent data loss
- Retry failed submissions

## Analytics Events
- `content_rated`: Track rating submissions
- `review_written`: Track review creation
- `favorite_added`: Track favorite additions
- `review_edited`: Track review updates
- `spoiler_toggled`: Track spoiler usage
- `rewatch_incremented`: Track rewatch patterns
