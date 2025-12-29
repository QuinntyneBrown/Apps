# Backend Requirements - Review and Rating

## API Endpoints

- **POST /api/books/{id}/ratings** - Rate a book (BookRated event)
- **PUT /api/ratings/{id}** - Update rating
- **POST /api/books/{id}/reviews** - Write review (BookReviewWritten event)
- **PUT /api/reviews/{id}** - Update review
- **DELETE /api/reviews/{id}** - Delete review
- **POST /api/books/{id}/highlights** - Add highlight (BookHighlighted event)
- **POST /api/books/{id}/notes** - Add note (BookNoteAdded event)
- **GET /api/books/{id}/reviews** - Get book reviews
- **GET /api/highlights** - Get all highlights (searchable)
- **GET /api/notes** - Get all notes (filterable)

## Domain Models

```csharp
public class Rating
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public int RatingValue { get; set; } // 1-5
    public DateTime RatingDate { get; set; }
    public bool IsPrivate { get; set; }
}

public class Review
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string ReviewText { get; set; }
    public int? Rating { get; set; }
    public DateTime ReviewDate { get; set; }
    public bool HasSpoilers { get; set; }
    public List<string> ThemesDiscussed { get; set; }
    public string TargetAudience { get; set; }
    public bool WouldRecommend { get; set; }
}

public class Highlight
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string PassageText { get; set; }
    public int PageNumber { get; set; }
    public DateTime HighlightDate { get; set; }
    public string Category { get; set; }
    public string PersonalNote { get; set; }
}

public class Note
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string NoteContent { get; set; }
    public int? PageReference { get; set; }
    public NoteType NoteType { get; set; }
    public DateTime Timestamp { get; set; }
}

public enum NoteType { Analysis, Question, Connection }
```

## Business Rules
1. Rating value must be 1-5
2. Review text max 10,000 characters
3. One rating per user per book
4. Multiple reviews allowed (drafts/updates)
