# Backend Requirements - Follow-Up Management

## Domain Events
- FollowUpScheduled
- FollowUpCompleted
- FollowUpMissed
- AutomaticFollowUpSuggested

## API Endpoints

### POST /api/followups
Schedule a follow-up.

**Request Body**:
```json
{
  "contactId": "guid",
  "scheduledDate": "datetime",
  "followUpType": "string",
  "reason": "string",
  "contextNotes": "string",
  "priority": "string (low|medium|high|urgent)"
}
```

**Response**: `201 Created`
**Events Published**: `FollowUpScheduled`

### GET /api/followups
Get follow-ups with filtering.

**Query Parameters**:
- `status`: pending|completed|missed|snoozed
- `priority`: filter by priority
- `contactId`: filter by contact
- `dueDate`: filter by due date range
- `page`, `pageSize`

**Response**: `200 OK`

### GET /api/followups/due
Get follow-ups due today or overdue.

**Response**: `200 OK`

### PUT /api/followups/{id}/complete
Mark follow-up as completed.

**Request Body**:
```json
{
  "completionDate": "datetime",
  "method": "string",
  "outcome": "string",
  "nextFollowUpDate": "datetime"
}
```

**Response**: `200 OK`
**Events Published**: `FollowUpCompleted`

### PUT /api/followups/{id}/snooze
Snooze follow-up to later date.

**Request Body**:
```json
{
  "newDate": "datetime",
  "snoozeReason": "string"
}
```

**Response**: `200 OK`

### GET /api/followups/suggestions
Get AI-suggested follow-ups.

**Response**: `200 OK`
```json
{
  "suggestions": [{
    "contactId": "guid",
    "contactName": "string",
    "reason": "string",
    "suggestedDate": "datetime",
    "talkingPoints": ["string"],
    "lastInteraction": "datetime"
  }]
}
```

**Events Published**: `AutomaticFollowUpSuggested`

### POST /api/followups/batch
Create multiple follow-ups (e.g., after event).

**Request Body**:
```json
{
  "followUps": [{
    "contactId": "guid",
    "scheduledDate": "datetime",
    "contextNotes": "string"
  }],
  "batchContext": "string"
}
```

**Response**: `201 Created`

## Data Models

### FollowUp Entity
```csharp
public class FollowUp : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ContactId { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public string FollowUpType { get; private set; }
    public string Reason { get; private set; }
    public string ContextNotes { get; private set; }
    public Priority Priority { get; private set; }
    public FollowUpStatus Status { get; private set; }
    public DateTime? CompletedDate { get; private set; }

    public FollowUp Complete(string outcome, DateTime? nextFollowUp) { }
    public FollowUp Snooze(DateTime newDate) { }
    public FollowUp MarkMissed() { }
}
```

### FollowUpSuggestion Value Object
```csharp
public class FollowUpSuggestion
{
    public Guid ContactId { get; private set; }
    public string Reason { get; private set; }
    public DateTime SuggestedDate { get; private set; }
    public List<string> TalkingPoints { get; private set; }
    public SuggestionConfidence Confidence { get; private set; }
}
```

## Business Rules
- Scheduled date must be in the future
- Cannot complete follow-up before scheduled date
- Missed follow-ups auto-detected by background job
- Follow-up suggestions based on:
  - Time since last interaction (>30 days)
  - Relationship strength declining
  - Mutual contact activity
  - Contact job change or milestone

## Background Jobs
- Check for overdue follow-ups (hourly)
- Generate follow-up suggestions (daily)
- Send reminder notifications (configured intervals)

## Integration Points
- Notification service
- Relationship calculator
- Calendar integration
