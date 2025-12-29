# Backend Requirements - Library Management

## API Endpoints

### Books
- **POST /api/books** - Add a new book to library
  - Request: BookDTO (title, author, ISBN, publicationYear, publisher, format, acquisitionDate, acquisitionSource, purchasePrice, shelfLocation)
  - Response: BookDTO with generated ID
  - Events: BookAddedToLibrary

- **PUT /api/books/{id}** - Update book details
  - Request: BookDTO
  - Response: Updated BookDTO
  - Events: None (or BookUpdated if tracking changes)

- **DELETE /api/books/{id}** - Remove book from library
  - Request: RemovalReasonDTO (reason, recipient, salePrice, notes)
  - Response: 204 No Content
  - Events: BookRemoved

- **GET /api/books/{id}** - Get book details
  - Response: BookDTO with full details

- **GET /api/books** - List all books with pagination and filtering
  - Query params: page, size, format, genre, sortBy
  - Response: PagedResponse<BookDTO>

### Categorization
- **POST /api/books/{id}/categories** - Categorize a book
  - Request: CategorizationDTO (genres, categories, tags, collections, series, priority)
  - Response: BookDTO
  - Events: BookCategorized

- **GET /api/categories** - Get all categories/genres
  - Response: List<CategoryDTO>

### Lending
- **POST /api/books/{id}/loans** - Lend a book
  - Request: LoanDTO (borrowerName, loanDate, expectedReturnDate, conditionNotes, reminderPreference)
  - Response: LoanDTO with generated ID
  - Events: BookLent

- **PUT /api/loans/{id}/return** - Mark book as returned
  - Request: ReturnDTO (returnDate, conditionOnReturn, borrowerFeedback)
  - Response: LoanDTO
  - Events: BookReturned

- **GET /api/loans** - Get all active and historical loans
  - Query params: status (active/returned), borrower
  - Response: List<LoanDTO>

- **GET /api/loans/overdue** - Get overdue loans
  - Response: List<LoanDTO>

### Statistics
- **GET /api/library/statistics** - Get library statistics
  - Response: LibraryStatisticsDTO (totalBooks, booksByFormat, booksByCategory, totalValue)

## Domain Models

### Book
```csharp
public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public string Publisher { get; set; }
    public BookFormat Format { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public string AcquisitionSource { get; set; }
    public decimal PurchasePrice { get; set; }
    public string ShelfLocation { get; set; }
    public List<string> Genres { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Tags { get; set; }
    public List<string> Collections { get; set; }
    public string SeriesName { get; set; }
    public int? SeriesNumber { get; set; }
    public int PriorityLevel { get; set; }
    public DateTime CategorizationDate { get; set; }
    public BookStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public enum BookFormat
{
    Physical,
    Digital,
    Audiobook
}

public enum BookStatus
{
    Available,
    Loaned,
    Reading,
    Removed
}
```

### Loan
```csharp
public class Loan
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string BorrowerName { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ExpectedReturnDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public string ConditionNotes { get; set; }
    public string ConditionOnReturn { get; set; }
    public string BorrowerFeedback { get; set; }
    public int RenewalCount { get; set; }
    public ReminderPreference ReminderPreference { get; set; }
    public LoanStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public enum ReminderPreference
{
    None,
    OnDueDate,
    OneDayBefore,
    ThreeDaysBefore,
    OneWeekBefore
}

public enum LoanStatus
{
    Active,
    Returned,
    Overdue
}
```

## Domain Events

### BookAddedToLibrary
```csharp
public class BookAddedToLibraryEvent : DomainEvent
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public string Publisher { get; set; }
    public BookFormat Format { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public string AcquisitionSource { get; set; }
    public decimal PurchasePrice { get; set; }
    public string ShelfLocation { get; set; }
}
```

### BookCategorized
```csharp
public class BookCategorizedEvent : DomainEvent
{
    public Guid BookId { get; set; }
    public List<string> Genres { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Tags { get; set; }
    public List<string> CustomCollections { get; set; }
    public string SeriesName { get; set; }
    public int? SeriesNumber { get; set; }
    public int PriorityLevel { get; set; }
    public DateTime CategorizationDate { get; set; }
}
```

### BookLent
```csharp
public class BookLentEvent : DomainEvent
{
    public Guid LoanId { get; set; }
    public Guid BookId { get; set; }
    public string BorrowerName { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ExpectedReturnDate { get; set; }
    public string ConditionNotes { get; set; }
    public ReminderPreference ReminderPreference { get; set; }
}
```

### BookReturned
```csharp
public class BookReturnedEvent : DomainEvent
{
    public Guid LoanId { get; set; }
    public Guid BookId { get; set; }
    public DateTime ReturnDate { get; set; }
    public string ConditionOnReturn { get; set; }
    public int DaysBorrowed { get; set; }
    public string BorrowerFeedback { get; set; }
    public int RenewalCount { get; set; }
}
```

### BookRemoved
```csharp
public class BookRemovedEvent : DomainEvent
{
    public Guid BookId { get; set; }
    public DateTime RemovalDate { get; set; }
    public RemovalReason RemovalReason { get; set; }
    public string RecipientDestination { get; set; }
    public decimal? SalePrice { get; set; }
    public string RemovalNotes { get; set; }
}

public enum RemovalReason
{
    Donated,
    Sold,
    Lost,
    Damaged
}
```

## Business Rules

1. **BR-LM-001**: A book cannot be loaned if it's already in an active loan status
2. **BR-LM-002**: A book cannot be marked as returned without an active loan
3. **BR-LM-003**: ISBN must be valid (10 or 13 digits)
4. **BR-LM-004**: Expected return date must be after loan date
5. **BR-LM-005**: A book can only be removed if it's not currently loaned out
6. **BR-LM-006**: Overdue reminders should be sent daily for loans past expected return date
7. **BR-LM-007**: Loan history must be preserved even after book removal

## Database Schema

### Books Table
- Id (PK, GUID)
- Title (NVARCHAR(500), NOT NULL)
- Authors (NVARCHAR(MAX), JSON)
- ISBN (NVARCHAR(13), UNIQUE)
- PublicationYear (INT)
- Publisher (NVARCHAR(200))
- Format (INT, NOT NULL)
- AcquisitionDate (DATETIME2)
- AcquisitionSource (NVARCHAR(200))
- PurchasePrice (DECIMAL(10,2))
- ShelfLocation (NVARCHAR(100))
- Genres (NVARCHAR(MAX), JSON)
- Categories (NVARCHAR(MAX), JSON)
- Tags (NVARCHAR(MAX), JSON)
- Collections (NVARCHAR(MAX), JSON)
- SeriesName (NVARCHAR(200))
- SeriesNumber (INT, NULL)
- PriorityLevel (INT, DEFAULT 0)
- CategorizationDate (DATETIME2)
- Status (INT, NOT NULL)
- CreatedAt (DATETIME2, NOT NULL)
- UpdatedAt (DATETIME2, NOT NULL)

### Loans Table
- Id (PK, GUID)
- BookId (FK, GUID, NOT NULL)
- BorrowerName (NVARCHAR(200), NOT NULL)
- LoanDate (DATETIME2, NOT NULL)
- ExpectedReturnDate (DATETIME2, NOT NULL)
- ActualReturnDate (DATETIME2, NULL)
- ConditionNotes (NVARCHAR(MAX))
- ConditionOnReturn (NVARCHAR(MAX))
- BorrowerFeedback (NVARCHAR(MAX))
- RenewalCount (INT, DEFAULT 0)
- ReminderPreference (INT, NOT NULL)
- Status (INT, NOT NULL)
- CreatedAt (DATETIME2, NOT NULL)
- UpdatedAt (DATETIME2, NOT NULL)

## Integration Requirements

1. **ISBN Lookup Service**: Integrate with external API (e.g., Google Books API, Open Library) for automatic book metadata retrieval
2. **Search Indexing**: Use full-text search capability for book titles, authors, and tags
3. **Notification Service**: Send loan reminders via email or push notifications
4. **Event Store**: Persist all domain events for audit trail and event sourcing

## Validation Rules

1. Book title is required and max 500 characters
2. At least one author is required
3. ISBN must be 10 or 13 digits (if provided)
4. Publication year must be between 1000 and current year + 1
5. Purchase price must be non-negative
6. Borrower name is required for loans
7. Expected return date must be in the future when creating a loan
