# Frontend Requirements - Watchlist Management

## Pages/Views

### Watchlist Dashboard
- URL: `/watchlist`
- Purpose: Display and manage user's complete watchlist
- Components: WatchlistGrid, FilterPanel, SortControls, PriorityIndicator

### Add to Watchlist Modal
- Purpose: Search and add movies or TV shows to watchlist
- Components: ContentSearchBar, ContentResultsList, AddToWatchlistForm

### Watchlist Item Detail
- URL: `/watchlist/{itemId}`
- Purpose: View and edit detailed information for a watchlist item
- Components: ItemDetailCard, PriorityEditor, AvailabilityIndicator, RemoveButton

## Components

### WatchlistGrid
- Displays watchlist items in a sortable, filterable grid
- Supports drag-and-drop for priority reordering
- Shows item poster, title, year, genres, priority, and availability status
- Responsive design (grid on desktop, list on mobile)
- Props: items, onPriorityChange, onRemove, sortBy, filterCriteria

### ContentSearchBar
- Auto-complete search for movies and TV shows
- Integrates with external content API (TMDB)
- Displays search results with poster thumbnails
- Props: onSelect, placeholder, searchType

### AddToWatchlistForm
- Form to add selected content to watchlist
- Fields: Priority level, recommendation source, mood category, notes
- Validates required fields
- Shows success/error feedback
- Props: content, onSubmit, onCancel

### FilterPanel
- Sidebar or dropdown for filtering watchlist
- Filters: Genre, priority, availability, mood category, content type
- Shows active filter count
- Clear all filters option
- Props: filters, onFilterChange, activeFilters

### SortControls
- Dropdown or button group for sorting options
- Options: Priority, added date, title, release year, runtime
- Ascending/descending toggle
- Props: sortBy, sortOrder, onSortChange

### PriorityIndicator
- Visual representation of item priority
- Color-coded or star-based system
- Editable inline
- Props: priority, editable, onChange

### AvailabilityIndicator
- Shows streaming platform availability
- Color-coded status (available/unavailable)
- Platform logos
- Subscription requirement indicator
- Props: availability, platforms

### ItemDetailCard
- Comprehensive view of watchlist item
- Displays all metadata (genres, director, runtime, seasons)
- Shows recommendation source
- Time on watchlist
- Availability information
- Props: item, onEdit, onRemove

### RemoveFromWatchlistModal
- Confirmation dialog for removing items
- Dropdown for removal reason
- Optional alternative suggestion field
- Props: item, onConfirm, onCancel

### MoodCategorySelector
- Tag-based selector for mood categories
- Preset categories: Action-packed, Relaxing, Thought-provoking, Funny, Scary, etc.
- Custom category creation
- Props: selectedCategories, onCategoryChange

### WatchlistSummaryCard
- Dashboard widget showing watchlist statistics
- Total items count
- Breakdown by priority
- Recently added items
- Props: statistics, recentItems

## State Management

### Watchlist Store
```typescript
interface WatchlistState {
  items: WatchlistItem[];
  loading: boolean;
  error: string | null;
  filters: FilterCriteria;
  sortBy: SortOption;
  sortOrder: 'asc' | 'desc';
}

interface WatchlistItem {
  id: string;
  contentId: string;
  contentType: 'Movie' | 'TVShow';
  title: string;
  year: number;
  genres: string[];
  posterUrl: string;
  director?: string;
  runtime?: number;
  numberOfSeasons?: number;
  status?: string;
  addedDate: Date;
  priorityLevel: string;
  priorityRank: number;
  recommendationSource?: string;
  moodCategory?: string;
  availability: Availability[];
}

interface Availability {
  platform: string;
  isAvailable: boolean;
  subscriptionRequired: boolean;
  logoUrl: string;
}

interface FilterCriteria {
  genres: string[];
  priorities: string[];
  moodCategories: string[];
  contentType: 'All' | 'Movie' | 'TVShow';
  availability: 'All' | 'Available' | 'Unavailable';
}
```

### Actions
- `fetchWatchlist()`: Load user's watchlist
- `addMovie(movie, options)`: Add movie to watchlist
- `addTVShow(show, options)`: Add TV show to watchlist
- `removeItem(itemId, reason)`: Remove item from watchlist
- `updatePriority(itemId, newPriority)`: Change item priority
- `reorderItems(itemRankings)`: Bulk reorder watchlist
- `setFilters(filters)`: Apply filters to watchlist
- `setSorting(sortBy, sortOrder)`: Change sorting criteria
- `searchContent(query, type)`: Search for content to add

## User Interactions

### Adding to Watchlist
1. User clicks "Add to Watchlist" button
2. Modal opens with content search
3. User searches and selects content
4. User sets priority, mood category, and source
5. User clicks "Add"
6. Success message displays
7. Watchlist updates with new item

### Removing from Watchlist
1. User clicks remove button on item
2. Confirmation modal appears
3. User selects removal reason
4. User confirms removal
5. Item is removed with animation
6. Success message displays

### Reordering Watchlist
1. User drags item to new position (desktop)
2. Or uses up/down buttons (mobile)
3. Priority ranks update automatically
4. Changes save automatically
5. Visual feedback shows new order

### Filtering Watchlist
1. User opens filter panel
2. User selects filter criteria
3. Watchlist updates in real-time
4. Active filter count displays
5. User can clear individual or all filters

## Responsive Design

### Desktop (>1024px)
- Grid layout with 4-5 items per row
- Sidebar filter panel
- Inline priority editing
- Drag-and-drop reordering
- Hover effects for additional actions

### Tablet (768px - 1024px)
- Grid layout with 2-3 items per row
- Collapsible filter panel
- Touch-friendly controls
- Swipe actions for remove

### Mobile (<768px)
- List layout with full-width items
- Bottom sheet for filters
- Priority editing in detail view
- Simplified item cards

## Accessibility

### Keyboard Navigation
- Tab through all interactive elements
- Enter to select/activate
- Escape to close modals
- Arrow keys for reordering

### Screen Reader Support
- ARIA labels for all controls
- Announce priority changes
- Announce filter updates
- Describe availability status

### Visual Accessibility
- High contrast mode support
- Color-blind friendly priority indicators
- Text alternatives for icons
- Minimum touch target size: 44x44px

## Performance

### Optimization
- Virtual scrolling for large watchlists (>100 items)
- Lazy load poster images
- Debounce search input (300ms)
- Cache watchlist data in local storage
- Optimistic UI updates for priority changes

### Loading States
- Skeleton screens while loading watchlist
- Inline loading indicators for actions
- Progress indication for bulk operations

## Error Handling
- Network error retry mechanism
- Graceful degradation if images fail to load
- Clear error messages with action suggestions
- Offline mode with cached data

## Analytics Events
- `watchlist_item_added`: Track what users add
- `watchlist_item_removed`: Track removal reasons
- `watchlist_prioritized`: Track prioritization patterns
- `watchlist_filtered`: Track popular filter combinations
- `watchlist_sorted`: Track preferred sorting methods
