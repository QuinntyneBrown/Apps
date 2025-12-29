# Frontend Requirements - Discovery and Recommendation

## Pages/Views

### 1. Wishlist Page (`/wishlist`)
- PrioritizedWishlistGrid
- SortByPriority/Date
- AddBookButton
- AcquisitionPlanner
- AnticipatedDateCalendar

### 2. Recommendations Page (`/recommendations`)
- ReceivedRecommendationsTab
- GivenRecommendationsTab
- AIReccommendationsSection
- RecommendationCard
- AcceptanceButtons

### 3. Reading Lists Page (`/reading-lists`)
- MyListsGrid
- PublicListsBrowser
- CreateListButton
- ListDetailView
- ShareListButton

### 4. Discover Page (`/discover`)
- PersonalizedRecommendations
- TrendingBooks
- SimilarToWhatYouRead
- GenreExploration
- AddToWishlistButton

## UI Components

### WishlistCard
- Book cover and info
- Priority stars (1-5)
- Anticipated read date
- Acquisition plan badge
- Interest reason tooltip

### RecommendationCard
- Recommender avatar/name
- Book details
- Recommendation reason
- Accept/Decline buttons
- Add to wishlist button

### ReadingListCard
- List title and description
- Book count
- Preview thumbnails (first 3-4 books)
- Public/Private indicator
- Share button

## State Management
```typescript
interface DiscoveryState {
  wishlist: WishlistItem[];
  receivedRecommendations: BookRecommendation[];
  givenRecommendations: BookRecommendation[];
  readingLists: ReadingList[];
  aiSuggestions: Book[];
}
```

## Actions
- addToWishlist(bookId, details)
- updatePriority(wishlistId, priority)
- receiveRecommendation(recommendation)
- giveRecommendation(recommendation)
- createReadingList(list)
- fetchAISuggestions()
