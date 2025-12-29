# Organization - Backend Requirements

## API Endpoints
- POST /api/tags - Create tag
- POST /api/folders - Create folder
- POST /api/collections - Create collection
- PUT /api/notes/{id}/move - Move note
- POST /api/notes/{id}/tag - Tag note

## Domain Events
- TagAdded
- FolderCreated
- NoteMoved
- CollectionCreated

## Data Models
```csharp
public class Folder {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
}

public class Tag {
    public string Name { get; set; }
    public int NoteCount { get; set; }
}

public class Collection {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Guid> NoteIds { get; set; }
}
```
