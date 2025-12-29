# Documentation - Backend Requirements

## Domain Events
- **ProgressPhotoTaken**: Photo documenting restoration progress captured
- **BeforeAfterPhotoSet**: Comparison photos created
- **VehicleHistoryDocumented**: Car's provenance recorded
- **RestorationJournalEntryWritten**: Written account created

## API Endpoints
- `POST /api/projects/{id}/photos` - Upload photo
- `GET /api/projects/{id}/photos` - List photos
- `POST /api/projects/{id}/photo-sets` - Create before/after set
- `POST /api/projects/{id}/history` - Document vehicle history
- `POST /api/projects/{id}/journal` - Create journal entry
- `GET /api/projects/{id}/journal` - List journal entries

## Data Models

### Photo
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "url": "string",
    "thumbnailUrl": "string",
    "caption": "string",
    "photoDate": "datetime",
    "workStage": "string",
    "tags": "array<string>",
    "sharingEnabled": "boolean"
}
```

### JournalEntry
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "entryDate": "datetime",
    "title": "string",
    "content": "string",
    "mood": "enum[Excited, Frustrated, Satisfied, Overwhelmed]",
    "lessonsLearned": "string"
}
```
