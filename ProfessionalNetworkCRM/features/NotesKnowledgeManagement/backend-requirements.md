# Backend Requirements - Notes & Knowledge Management

## Domain Events
- ContactNoteAdded
- ConversationTopicLogged

## API Endpoints

### POST /api/contacts/{contactId}/notes
Add note to contact.

**Request Body**:
```json
{
  "content": "string",
  "noteType": "personal|professional|preferences|goals",
  "privacyLevel": "private|shared",
  "tags": ["string"]
}
```

**Response**: `201 Created`
**Events Published**: `ContactNoteAdded`

### GET /api/contacts/{contactId}/notes
Get notes for contact.

**Response**: `200 OK`

### POST /api/contacts/{contactId}/topics
Log conversation topic.

**Request Body**:
```json
{
  "topic": "string",
  "interestLevel": "high|medium|low",
  "lastDiscussed": "datetime"
}
```

**Response**: `201 Created`
**Events Published**: `ConversationTopicLogged`

### GET /api/notes/search
Search across all notes.

**Query Parameters**:
- `query`: search term
- `noteType`: filter by type
- `tags`: filter by tags

**Response**: `200 OK`

## Data Models

### ContactNote Entity
```csharp
public class ContactNote : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ContactId { get; private set; }
    public string Content { get; private set; }
    public NoteType Type { get; private set; }
    public PrivacyLevel Privacy { get; private set; }
    public List<string> Tags { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
```

### ConversationTopic Value Object
```csharp
public class ConversationTopic
{
    public string Topic { get; private set; }
    public InterestLevel Level { get; private set; }
    public DateTime LastDiscussed { get; private set; }
}
```
