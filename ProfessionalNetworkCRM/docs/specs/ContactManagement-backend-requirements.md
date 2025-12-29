# Backend Requirements - Contact Management

## Domain Events
- ContactAdded
- ContactUpdated
- ContactTagged
- ContactRelationshipDefined
- ContactDeactivated

## API Endpoints

### POST /api/contacts
Create a new professional contact.

**Request Body**:
```json
{
  "name": "string (required)",
  "title": "string",
  "company": "string",
  "industry": "string",
  "email": "string",
  "phone": "string",
  "linkedInUrl": "string",
  "metDate": "datetime",
  "metLocation": "string",
  "connectionSource": "string",
  "relationshipType": "string",
  "tags": ["string"],
  "notes": "string"
}
```

**Response**: `201 Created`
```json
{
  "id": "guid",
  "name": "string",
  "createdAt": "datetime",
  "...allFields"
}
```

**Events Published**: `ContactAdded`, `ContactTagged`, `ContactRelationshipDefined`

### GET /api/contacts
Retrieve contacts with filtering and pagination.

**Query Parameters**:
- `search`: string (search across name, company, title)
- `tags`: comma-separated tag list
- `relationshipType`: filter by relationship type
- `industry`: filter by industry
- `active`: boolean (default: true)
- `page`: integer (default: 1)
- `pageSize`: integer (default: 50, max: 200)
- `sortBy`: name|company|lastInteraction|relationshipStrength
- `sortOrder`: asc|desc

**Response**: `200 OK`
```json
{
  "contacts": [{
    "id": "guid",
    "name": "string",
    "title": "string",
    "company": "string",
    "relationshipType": "string",
    "relationshipStrength": "number",
    "lastInteractionDate": "datetime",
    "tags": ["string"]
  }],
  "totalCount": "integer",
  "page": "integer",
  "pageSize": "integer"
}
```

### GET /api/contacts/{id}
Retrieve detailed contact information.

**Response**: `200 OK`
```json
{
  "id": "guid",
  "name": "string",
  "title": "string",
  "company": "string",
  "industry": "string",
  "email": "string",
  "phone": "string",
  "linkedInUrl": "string",
  "metDate": "datetime",
  "metLocation": "string",
  "connectionSource": "string",
  "relationshipType": "string",
  "relationshipStrength": "number",
  "tags": ["string"],
  "mutualContacts": [{"id": "guid", "name": "string"}],
  "active": "boolean",
  "createdAt": "datetime",
  "updatedAt": "datetime",
  "lastInteractionDate": "datetime",
  "interactionCount": "integer"
}
```

### PUT /api/contacts/{id}
Update contact information.

**Request Body**: (all fields optional except id)
```json
{
  "name": "string",
  "title": "string",
  "company": "string",
  "industry": "string",
  "email": "string",
  "phone": "string",
  "linkedInUrl": "string",
  "updateSource": "manual|linkedin|automated"
}
```

**Response**: `200 OK`
**Events Published**: `ContactUpdated`

### POST /api/contacts/{id}/tags
Add tags to contact.

**Request Body**:
```json
{
  "tags": ["string"],
  "tagCategory": "string",
  "autoTagged": "boolean"
}
```

**Response**: `200 OK`
**Events Published**: `ContactTagged`

### DELETE /api/contacts/{id}/tags/{tag}
Remove tag from contact.

**Response**: `204 No Content`

### PUT /api/contacts/{id}/relationship
Define or update relationship type.

**Request Body**:
```json
{
  "relationshipType": "string (mentor|colleague|client|prospect|vendor|friend)",
  "relationshipStrength": "integer (1-10)",
  "connectionContext": "string",
  "relationshipStartDate": "datetime"
}
```

**Response**: `200 OK`
**Events Published**: `ContactRelationshipDefined`

### POST /api/contacts/{id}/deactivate
Mark contact as inactive.

**Request Body**:
```json
{
  "reason": "string",
  "reactivationConditions": "string"
}
```

**Response**: `200 OK`
**Events Published**: `ContactDeactivated`

### POST /api/contacts/{id}/reactivate
Reactivate deactivated contact.

**Response**: `200 OK`

### POST /api/contacts/import
Import contacts from external source.

**Request Body**:
```json
{
  "source": "linkedin|google|csv",
  "data": "string|array",
  "autoTag": "boolean",
  "importTag": "string"
}
```

**Response**: `202 Accepted`
```json
{
  "importId": "guid",
  "status": "processing",
  "estimatedCompletion": "datetime"
}
```

### GET /api/contacts/import/{importId}
Check status of import operation.

**Response**: `200 OK`
```json
{
  "importId": "guid",
  "status": "processing|completed|failed",
  "contactsImported": "integer",
  "contactsFailed": "integer",
  "errors": ["string"]
}
```

### GET /api/contacts/tags
Get all unique tags across contacts.

**Response**: `200 OK`
```json
{
  "tags": [
    {"tag": "string", "count": "integer", "category": "string"}
  ]
}
```

### POST /api/contacts/merge
Merge duplicate contacts.

**Request Body**:
```json
{
  "primaryContactId": "guid",
  "duplicateContactId": "guid",
  "fieldPreferences": {
    "email": "primary|duplicate",
    "phone": "primary|duplicate"
  }
}
```

**Response**: `200 OK`

## Data Models

### Contact Entity
```csharp
public class Contact : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Title { get; private set; }
    public string Company { get; private set; }
    public string Industry { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string LinkedInUrl { get; private set; }
    public DateTime? MetDate { get; private set; }
    public string MetLocation { get; private set; }
    public string ConnectionSource { get; private set; }
    public string RelationshipType { get; private set; }
    public int RelationshipStrength { get; private set; }
    public List<string> Tags { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Contact AddTag(string tag, string category = null) { }
    public Contact RemoveTag(string tag) { }
    public Contact UpdateRelationship(string type, int strength) { }
    public Contact Deactivate(string reason) { }
    public Contact Reactivate() { }
}
```

### ContactTag Value Object
```csharp
public class ContactTag
{
    public string Tag { get; private set; }
    public string Category { get; private set; }
    public DateTime TaggedDate { get; private set; }
    public bool AutoTagged { get; private set; }
}
```

### MutualConnection Value Object
```csharp
public class MutualConnection
{
    public Guid ContactId { get; private set; }
    public string ContactName { get; private set; }
    public string ConnectionContext { get; private set; }
}
```

## Business Rules

### Contact Creation
- Name is required and must be 2-200 characters
- Email must be valid format if provided
- LinkedIn URL must be valid LinkedIn profile URL if provided
- ConnectionSource must be from predefined list or "Other"
- Tags are case-insensitive and normalized to lowercase
- Duplicate email detection (warn but allow)

### Contact Updates
- Track field-level changes for audit trail
- Update source (manual/linkedin/automated) is recorded
- UpdatedAt timestamp is automatically set
- Changes to key fields (name, company, title) trigger ContactUpdated event

### Relationship Types
- Allowed values: Mentor, Colleague, Client, Prospect, Vendor, Friend, Family, Other
- RelationshipStrength is 1-10 scale (calculated or manual)
- ConnectionContext is free text describing how they know each other

### Tagging
- Maximum 50 tags per contact
- Tag length: 1-50 characters
- Tag categories: Industry, Skill, Location, Interest, Custom
- System tags (prefixed with sys:) are auto-generated and protected

### Deactivation
- Deactivated contacts excluded from default queries
- Last interaction date preserved for reactivation decisions
- Reactivation conditions stored for follow-up suggestions
- Can reactivate at any time

## Performance Requirements
- Contact list queries return within 500ms
- Support 10,000+ contacts per user
- Search indexes on name, company, email, tags
- Pagination required for all list endpoints
- Implement caching for frequently accessed contacts

## Security Requirements
- User can only access their own contacts
- Contact data encrypted at rest
- PII (email, phone) logged with audit trail
- GDPR: support export and delete all contact data
- Rate limiting on import operations

## Integration Points
- LinkedIn API for profile import and sync
- Google Contacts API for import
- Email service for contact enrichment
- Calendar service for meeting association
- Search service for full-text search

## Event Sourcing
All contact changes emit domain events:
- ContactAdded: new contact created
- ContactUpdated: fields modified
- ContactTagged: tags added
- ContactRelationshipDefined: relationship type/strength set
- ContactDeactivated: contact archived

Events are consumed by:
- Search indexer
- Relationship calculator
- Follow-up scheduler
- Analytics service
- Audit log
