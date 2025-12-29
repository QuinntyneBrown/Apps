# Backend Requirements - Session Tracking

## Domain Events
- SessionAttended, SessionRated, SessionNotesCreated, SpeakerFollowUpScheduled

## API Endpoints
- POST /api/sessions/{id}/attend, POST /api/sessions/{id}/notes, POST /api/sessions/{id}/rate
- GET /api/sessions, GET /api/conferences/{id}/sessions

## Domain Model
```csharp
public class Session { public Guid Id; public string Title; public List<string> Speakers; public DateTime StartTime; public string Track; }
public class SessionNotes { public Guid Id; public string Content; public List<string> ActionItems; }
```
