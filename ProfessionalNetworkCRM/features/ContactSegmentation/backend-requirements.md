# Backend Requirements - Contact Segmentation

## Domain Events
- ContactSegmentCreated
- BulkOutreachPlanned

## API Endpoints

### POST /api/segments
Create contact segment.

**Request Body**:
```json
{
  "name": "string",
  "criteria": {
    "tags": ["string"],
    "relationshipType": "string",
    "industry": "string",
    "custom": {}
  },
  "purpose": "string"
}
```

**Response**: `201 Created`
**Events Published**: `ContactSegmentCreated`

### GET /api/segments
Get all segments.

**Response**: `200 OK`

### GET /api/segments/{id}/contacts
Get contacts in segment.

**Response**: `200 OK`

### POST /api/campaigns
Plan bulk outreach campaign.

**Request Body**:
```json
{
  "segmentId": "guid",
  "messageTemplate": "string",
  "personalizationFields": ["string"],
  "scheduledDate": "datetime"
}
```

**Response**: `201 Created`
**Events Published**: `BulkOutreachPlanned`

## Data Models

### ContactSegment Entity
```csharp
public class ContactSegment : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public SegmentCriteria Criteria { get; private set; }
    public string Purpose { get; private set; }

    public List<Guid> GetContactIds() { }
}
```
