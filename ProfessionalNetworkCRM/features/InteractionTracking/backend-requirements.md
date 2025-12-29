# Backend Requirements - Interaction Tracking

## Domain Events
- InteractionLogged
- MeetingScheduled
- MeetingCompleted
- EmailExchangeTracked

## API Endpoints

### POST /api/interactions
Log a new interaction with a contact.

**Request Body**:
```json
{
  "contactId": "guid (required)",
  "interactionType": "string (meeting|call|email|message|coffee|lunch|conference)",
  "date": "datetime (required)",
  "duration": "integer (minutes)",
  "location": "string",
  "medium": "string",
  "topicsDiscussed": ["string"],
  "notes": "string",
  "sentiment": "string (positive|neutral|negative)",
  "followUpNeeded": "boolean",
  "followUpNotes": "string"
}
```

**Response**: `201 Created`
**Events Published**: `InteractionLogged`

### GET /api/interactions
Get interactions with filtering.

**Query Parameters**:
- `contactId`: filter by contact
- `type`: filter by interaction type
- `startDate`: filter from date
- `endDate`: filter to date
- `page`, `pageSize`

**Response**: `200 OK`

### GET /api/interactions/{id}
Get interaction details.

**Response**: `200 OK`

### PUT /api/interactions/{id}
Update interaction.

**Response**: `200 OK`

### DELETE /api/interactions/{id}
Delete interaction.

**Response**: `204 No Content`

### POST /api/meetings
Schedule a meeting.

**Request Body**:
```json
{
  "contactId": "guid",
  "scheduledDateTime": "datetime",
  "location": "string",
  "purpose": "string",
  "agendaItems": ["string"],
  "preparationNotes": "string",
  "calendarSynced": "boolean"
}
```

**Response**: `201 Created`
**Events Published**: `MeetingScheduled`

### PUT /api/meetings/{id}/complete
Mark meeting as completed.

**Request Body**:
```json
{
  "actualDate": "datetime",
  "attendees": ["string"],
  "duration": "integer",
  "topicsCovered": ["string"],
  "outcomes": "string",
  "actionItems": ["string"],
  "nextSteps": "string",
  "relationshipImpact": "string"
}
```

**Response**: `200 OK`
**Events Published**: `MeetingCompleted`, `InteractionLogged`

### GET /api/contacts/{contactId}/interactions
Get interaction timeline for contact.

**Response**: `200 OK`

### GET /api/analytics/interactions
Get interaction analytics.

**Query Parameters**:
- `startDate`, `endDate`
- `groupBy`: day|week|month
- `contactId`: optional

**Response**: `200 OK`
```json
{
  "totalInteractions": "integer",
  "byType": {"meeting": 10, "call": 5},
  "frequency": {"week1": 3, "week2": 5},
  "averageDuration": "integer"
}
```

## Data Models

### Interaction Entity
```csharp
public class Interaction : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ContactId { get; private set; }
    public InteractionType Type { get; private set; }
    public DateTime Date { get; private set; }
    public int? Duration { get; private set; }
    public string Location { get; private set; }
    public string Medium { get; private set; }
    public List<string> TopicsDiscussed { get; private set; }
    public string Notes { get; private set; }
    public Sentiment Sentiment { get; private set; }
    public bool FollowUpNeeded { get; private set; }
}
```

### Meeting Entity
```csharp
public class Meeting : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ContactId { get; private set; }
    public DateTime ScheduledDateTime { get; private set; }
    public string Location { get; private set; }
    public string Purpose { get; private set; }
    public List<string> AgendaItems { get; private set; }
    public MeetingStatus Status { get; private set; }
    public DateTime? CompletedDate { get; private set; }

    public Meeting Complete(MeetingOutcome outcome) { }
}
```

## Business Rules
- Interaction date cannot be in the future
- Duration must be positive if provided
- Meeting must reference valid contact
- Completed meetings auto-create interaction record
- Email exchanges can be auto-tracked with integration

## Performance Requirements
- Interaction timeline loads in <1 second
- Support 1000+ interactions per contact
- Real-time sync with calendar

## Integration Points
- Calendar API (Google, Outlook)
- Email API (Gmail, Outlook)
- Analytics service
