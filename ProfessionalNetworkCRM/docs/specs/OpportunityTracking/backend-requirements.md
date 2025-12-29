# Backend Requirements - Opportunity Tracking

## Domain Events
- OpportunityIdentified
- IntroductionRequested
- IntroductionMade
- ReferralReceived

## API Endpoints

### POST /api/opportunities
Track new opportunity.

**Request Body**:
```json
{
  "contactId": "guid",
  "opportunityType": "job|client|partnership|speaking|collaboration",
  "description": "string",
  "potentialValue": "string",
  "status": "identified|pursuing|won|lost"
}
```

**Response**: `201 Created`
**Events Published**: `OpportunityIdentified`

### GET /api/opportunities
Get opportunities with filtering.

**Response**: `200 OK`

### POST /api/introductions/request
Request introduction.

**Request Body**:
```json
{
  "fromContactId": "guid",
  "toContactId": "guid",
  "purpose": "string"
}
```

**Response**: `201 Created`
**Events Published**: `IntroductionRequested`

### POST /api/introductions/make
Log introduction made.

**Response**: `201 Created`
**Events Published**: `IntroductionMade`

### POST /api/referrals
Track referral received.

**Response**: `201 Created`
**Events Published**: `ReferralReceived`

## Data Models

### Opportunity Entity
```csharp
public class Opportunity : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid ContactId { get; private set; }
    public OpportunityType Type { get; private set; }
    public string Description { get; private set; }
    public OpportunityStatus Status { get; private set; }
    public decimal? Value { get; private set; }
}
```

### Introduction Entity
```csharp
public class Introduction : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid FromContactId { get; private set; }
    public Guid ToContactId { get; private set; }
    public string Purpose { get; private set; }
    public IntroductionStatus Status { get; private set; }
}
```
