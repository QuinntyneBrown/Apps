# Frontend Requirements - Review and Rating

## Pages/Views

### 1. Book Review Page (`/books/{id}/review`)
- StarRating component (interactive)
- ReviewEditor (rich text)
- SpoilerToggle
- ThemesTags input
- WouldRecommend checkbox

### 2. Highlights Library (`/highlights`)
- SearchableHighlightsList
- FilterByBook/Category
- ExportButton
- QuoteCard component

### 3. Notes Collection (`/notes`)
- NotesGrid (Analysis/Question/Connection tabs)
- BookFilter
- SearchBar
- NoteCard with edit/delete

## UI Components

### StarRating
- Interactive 1-5 star selector
- Half-star support
- Display mode (read-only)

### ReviewCard
- Book info header
- Rating display
- Review text with spoiler blur
- Edit/Delete buttons
- Share button

### HighlightCard
- Quoted text styling
- Page number badge
- Personal note section
- Copy to clipboard button

## State Management
```typescript
interface ReviewState {
  bookReviews: Review[];
  highlights: Highlight[];
  notes: Note[];
  currentRating: Rating | null;
}
```

## Actions
- rateBook(bookId, rating)
- writeReview(bookId, review)
- addHighlight(bookId, highlight)
- addNote(bookId, note)
- searchHighlights(query)
