# Session Management - Backend Requirements

## Domain Events
- PhotoSessionPlanned, PhotoSessionStarted, SessionSettingsRecorded, PhotoSessionCompleted, SessionNotesAdded

## API Endpoints
- `POST /api/sessions` - Create session
- `GET /api/sessions` - List sessions
- `POST /api/sessions/{id}/start` - Start session
- `POST /api/sessions/{id}/complete` - End session
- `POST /api/sessions/{id}/settings` - Log settings

## Data Models
```csharp
PhotoSession {
    id: Guid
    sessionType: SessionType
    scheduledDate: DateTime
    location: string
    client: string?
    shotList: string[]
    startTime: DateTime?
    endTime: DateTime?
    photosCaptured: int
    status: SessionStatus
}
```
