# Backend Requirements - Notes and Highlights

## Domain Events
- HighlightCreated
- NoteAdded
- KeyInsightCaptured

## Key API Endpoints
- POST /api/highlights - Create highlight
- POST /api/notes - Add note
- POST /api/insights - Capture key insight
- GET /api/reading-materials/{id}/highlights - Get all highlights
- GET /api/notes/search - Search across notes
- PUT /api/notes/{id} - Update note
- DELETE /api/highlights/{id} - Delete highlight

## Data Models
```csharp
public class Highlight : Entity
{
    public Guid Id { get; private set; }
    public Guid MaterialId { get; private set; }
    public string HighlightedText { get; private set; }
    public string PageLocation { get; private set; }
    public string Color { get; private set; }
}

public class Note : Entity
{
    public Guid Id { get; private set; }
    public Guid MaterialId { get; private set; }
    public string Content { get; private set; }
    public NoteType Type { get; private set; }
    public string SectionReference { get; private set; }
}
```

## Business Rules
- Highlights limited to 10,000 characters
- Notes support rich text formatting
- Insights can be tagged with skills
- Full-text search across all notes
