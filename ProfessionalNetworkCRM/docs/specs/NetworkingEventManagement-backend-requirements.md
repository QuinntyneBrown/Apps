# Backend Requirements - Networking Event Management

## Domain Events
- EventAttended
- ContactMetAtEvent
- EventFollowUpGenerated

## API Endpoints

### POST /api/events
Log event attendance.

**Request Body**:
```json
{
  "name": "string",
  "eventType": "string",
  "date": "datetime",
  "location": "string",
  "attendees": "integer",
  "connectionsMade": "integer",
  "notes": "string"
}
```

**Response**: `201 Created`
**Events Published**: `EventAttended`

### POST /api/events/{eventId}/contacts
Add contact met at event.

**Response**: `201 Created`
**Events Published**: `ContactMetAtEvent`, `ContactAdded`

### POST /api/events/{eventId}/followups
Generate follow-up tasks for event.

**Response**: `201 Created`
**Events Published**: `EventFollowUpGenerated`

### GET /api/events
Get events with analytics.

**Response**: `200 OK`

## Data Models

### NetworkingEvent Entity
```csharp
public class NetworkingEvent : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string EventType { get; private set; }
    public DateTime Date { get; private set; }
    public string Location { get; private set; }
    public int ConnectionsMade { get; private set; }
    public List<Guid> ContactsMetIds { get; private set; }
}
```

## Business Rules
- Event date can be past or upcoming
- Contacts can be linked to event source
- Follow-ups auto-generated based on event context
