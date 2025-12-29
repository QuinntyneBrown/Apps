# Frontend Requirements - Library Management

## Pages/Views

### 1. Library Catalog View
**Route**: `/library`

**Components**:
- BookGrid/BookList - Display books in grid or list view
- SearchBar - Search across title, author, ISBN
- FilterPanel - Filter by format, genre, category, status
- SortDropdown - Sort by title, author, acquisition date, rating
- ViewToggle - Switch between grid and list view
- AddBookButton - Opens add book modal

**State Management**:
- books: Book[]
- filters: FilterCriteria
- searchQuery: string
- viewMode: 'grid' | 'list'
- pagination: PaginationState

**User Actions**:
- Search for books by text
- Filter books by multiple criteria
- Sort books by various fields
- Switch between grid and list views
- Click book to view details
- Click "Add Book" to open add modal

### 2. Book Details View
**Route**: `/library/books/{id}`

**Components**:
- BookHeader - Title, author, cover image
- BookInfo - Publication details, format, ISBN
- AcquisitionInfo - Purchase date, source, price
- LocationInfo - Shelf location
- CategoryTags - Genres, categories, tags, collections
- ActionButtons - Edit, Lend, Remove, Start Reading
- LoanHistory - List of all loans for this book
- ReadingHistory - Reading sessions for this book

**State Management**:
- book: Book
- loanHistory: Loan[]
- isLoading: boolean

**User Actions**:
- View complete book details
- Edit book information
- Lend book to someone
- Remove book from library
- Start reading the book
- View loan and reading history

### 3. Add/Edit Book Modal
**Component**: `AddEditBookModal`

**Form Fields**:
- Title (required)
- Author(s) (required, multi-input)
- ISBN (optional, with lookup button)
- Publication Year
- Publisher
- Format (dropdown: Physical/Digital/Audiobook)
- Acquisition Date (date picker)
- Acquisition Source
- Purchase Price
- Shelf Location
- Cover Image Upload

**Features**:
- ISBN lookup to auto-fill book details
- Multi-author input with add/remove
- Form validation
- Image upload or URL

**User Actions**:
- Enter book details manually
- Use ISBN lookup for automatic data
- Add multiple authors
- Upload or link cover image
- Save or cancel

### 4. Categorize Book Modal
**Component**: `CategorizeBookModal`

**Form Fields**:
- Genres (multi-select dropdown)
- Categories (multi-select dropdown)
- Tags (tag input with autocomplete)
- Custom Collections (multi-select)
- Series Name
- Series Number
- Priority Level (slider 1-5)

**Features**:
- Autocomplete for existing tags
- Create new categories on the fly
- Visual priority selector

**User Actions**:
- Select multiple genres and categories
- Add tags with autocomplete
- Assign to collections
- Set series information
- Adjust priority level
- Save categorization

### 5. Lend Book Modal
**Component**: `LendBookModal`

**Form Fields**:
- Borrower Name (required, with autocomplete from previous borrowers)
- Loan Date (date picker, default today)
- Expected Return Date (date picker, required)
- Condition Notes (textarea)
- Reminder Preference (dropdown)

**Features**:
- Autocomplete borrower names
- Validation: return date must be after loan date
- Quick date presets (1 week, 2 weeks, 1 month)

**User Actions**:
- Enter borrower information
- Set loan and return dates
- Add condition notes
- Configure reminder preferences
- Confirm loan

### 6. Return Book Modal
**Component**: `ReturnBookModal`

**Form Fields**:
- Return Date (date picker, default today)
- Condition on Return (textarea)
- Borrower Feedback (textarea, optional)

**Features**:
- Shows original loan details
- Calculates days borrowed
- Highlights if overdue

**User Actions**:
- Confirm return date
- Document book condition
- Record borrower feedback
- Complete return

### 7. Active Loans View
**Route**: `/library/loans`

**Components**:
- LoansTable - List of active loans
- OverdueSection - Highlighted overdue loans
- FilterTabs - All / Active / Overdue / Returned
- SearchBar - Search by book or borrower

**State Management**:
- loans: Loan[]
- filter: LoanFilter
- overdueLoans: Loan[]

**User Actions**:
- View all active loans
- Filter by loan status
- Mark book as returned
- Send reminder to borrower
- View loan history

### 8. Library Statistics Dashboard
**Route**: `/library/statistics`

**Components**:
- TotalBooksCard - Total book count
- FormatBreakdownChart - Pie chart of formats
- CategoryDistributionChart - Bar chart of categories
- CollectionValueCard - Total library value
- AcquisitionTimelineChart - Books acquired over time
- MostLentBooksTable - Books with most loans

**State Management**:
- statistics: LibraryStatistics
- timeRange: string

**User Actions**:
- View library overview
- Analyze collection composition
- Track collection growth
- Identify popular books

## UI Components

### BookCard
- Displays book cover thumbnail
- Shows title and author
- Indicates status (Available, Loaned, Reading)
- Shows rating if available
- Click to view details

### BookListItem
- Horizontal layout with thumbnail
- Shows key details inline
- Quick action buttons (Lend, Read, Edit)
- Status badge

### LoanStatusBadge
- Visual indicator for loan status
- Color coded: Green (Available), Yellow (Loaned), Red (Overdue)
- Shows days until/past due date

### ISBNLookup
- Input field with lookup button
- Loading indicator during lookup
- Auto-fill confirmation dialog

## State Management

### Library Store (Redux/Context)
```typescript
interface LibraryState {
  books: Book[];
  selectedBook: Book | null;
  loans: Loan[];
  statistics: LibraryStatistics;
  filters: FilterCriteria;
  isLoading: boolean;
  error: string | null;
}

interface FilterCriteria {
  format?: BookFormat;
  genres?: string[];
  categories?: string[];
  status?: BookStatus;
  searchQuery?: string;
}
```

### Actions
- fetchBooks()
- fetchBookById(id)
- addBook(book)
- updateBook(id, book)
- removeBook(id, reason)
- categorizeBook(id, categories)
- lendBook(id, loan)
- returnBook(loanId, returnInfo)
- fetchLoans()
- fetchStatistics()

## Validation

### Client-Side Validation
1. Book title: Required, max 500 characters
2. Authors: At least one required
3. ISBN: Optional, but if provided must be 10 or 13 digits
4. Publication year: Between 1000 and current year + 1
5. Purchase price: Non-negative number
6. Expected return date: Must be in future
7. Borrower name: Required for loans

### Error Handling
- Display inline validation errors
- Show toast notifications for API errors
- Graceful degradation if ISBN lookup fails
- Confirmation dialogs for destructive actions (remove book)

## Responsive Design

### Desktop (> 1024px)
- Grid view: 4-5 books per row
- Side panel for filters
- Expanded book cards with more details

### Tablet (768px - 1024px)
- Grid view: 2-3 books per row
- Collapsible filter panel
- Condensed book cards

### Mobile (< 768px)
- List view only (1 column)
- Bottom sheet for filters
- Simplified book cards
- Stacked form fields in modals

## Accessibility

1. All interactive elements keyboard navigable
2. ARIA labels for icon buttons
3. Screen reader announcements for status changes
4. Color contrast ratio of at least 4.5:1
5. Focus indicators on all focusable elements
6. Alt text for book cover images

## Performance Optimization

1. Virtualized scrolling for large book lists (> 100 books)
2. Lazy loading of book cover images
3. Debounced search input (300ms)
4. Cached filter results
5. Optimistic UI updates for common actions
6. Pagination for book list (25 per page)
