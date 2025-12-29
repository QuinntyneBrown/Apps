# Notes - Backend Requirements

## API Endpoints
- POST /api/notes - Create note
- PUT /api/notes/{id} - Update note
- DELETE /api/notes/{id} - Delete note
- POST /api/notes/merge - Merge notes
- POST /api/notes/split - Split note
- GET /api/notes - List all notes

## Domain Events
- NoteCreated
- NoteUpdated
- NoteDeleted
- NoteMerged
- NoteSplit

## Data Models
```csharp
public class Note {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public NoteType Type { get; set; }
    public string Source { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
}
```
