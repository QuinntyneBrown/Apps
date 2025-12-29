# Backend Requirements - Relationship Intelligence

## Domain Events
- RelationshipStrengthCalculated
- StrongTieIdentified
- WeakTieDetected
- RelationshipMilestoneReached

## API Endpoints

### GET /api/relationships/{contactId}/strength
Get relationship strength score.

**Response**: `200 OK`
```json
{
  "contactId": "guid",
  "strengthScore": "number (0-100)",
  "calculatedAt": "datetime",
  "factors": {
    "interactionFrequency": 40,
    "recency": 30,
    "interactionQuality": 20,
    "valueExchange": 10
  },
  "trend": "strengthening|stable|weakening",
  "trendPercentage": "number"
}
```

### POST /api/relationships/calculate
Trigger relationship strength calculation for contacts.

**Request Body**:
```json
{
  "contactIds": ["guid"],
  "forceRecalculate": "boolean"
}
```

**Response**: `202 Accepted`
**Events Published**: `RelationshipStrengthCalculated`

### GET /api/relationships/strong-ties
Get contacts identified as strong ties.

**Response**: `200 OK`

### GET /api/relationships/weak-ties
Get contacts with weakening relationships.

**Response**: `200 OK`

### GET /api/relationships/at-risk
Get relationships at risk of becoming dormant.

**Response**: `200 OK`

### GET /api/relationships/{contactId}/milestones
Get relationship milestones.

**Response**: `200 OK`
```json
{
  "milestones": [{
    "type": "anniversary|interaction_count|collaboration",
    "date": "datetime",
    "description": "string",
    "significance": "string"
  }]
}
```

### GET /api/analytics/relationships
Get relationship health analytics.

**Response**: `200 OK`
```json
{
  "averageStrength": "number",
  "strengthDistribution": {
    "strong": 10,
    "medium": 25,
    "weak": 15
  },
  "dormantCount": "integer",
  "atRiskCount": "integer",
  "trendingUp": "integer",
  "trendingDown": "integer"
}
```

## Data Models

### RelationshipStrength Value Object
```csharp
public class RelationshipStrength
{
    public int Score { get; private set; } // 0-100
    public DateTime CalculatedAt { get; private set; }
    public Dictionary<string, int> Factors { get; private set; }
    public RelationshipTrend Trend { get; private set; }

    public static RelationshipStrength Calculate(
        List<Interaction> interactions,
        List<ValueExchange> exchanges,
        DateTime firstContact
    ) { }
}
```

### RelationshipMilestone Value Object
```csharp
public class RelationshipMilestone
{
    public MilestoneType Type { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    public int InteractionCount { get; private set; }
}
```

## Business Rules

### Strength Calculation Algorithm
**Factors**:
1. **Interaction Frequency (40%)**:
   - Weekly: 100 points
   - Bi-weekly: 80 points
   - Monthly: 60 points
   - Quarterly: 40 points
   - Less: 20 points

2. **Recency (30%)**:
   - Within 7 days: 100 points
   - Within 30 days: 80 points
   - Within 90 days: 60 points
   - Within 180 days: 40 points
   - Older: decay function

3. **Interaction Quality (20%)**:
   - In-person meetings: 100 points
   - Video calls: 80 points
   - Phone calls: 60 points
   - Emails: 40 points
   - Messages: 20 points

4. **Value Exchange Balance (10%)**:
   - Mutual value: 100 points
   - Slightly imbalanced: 80 points
   - One-sided: 50 points

**Strong Tie Threshold**: Score >= 75
**Weak Tie Threshold**: Score < 40
**At Risk**: Score dropped >15 points in 30 days

### Milestones
- 1-year anniversary
- 100 interactions
- 500 interactions
- Major collaboration completed
- Referral/introduction made

## Background Jobs
- Calculate relationship strengths (daily)
- Detect weak ties (daily)
- Identify milestones (daily)
- Generate re-engagement suggestions (daily)

## Integration Points
- Interaction service
- Value exchange service
- Follow-up service
- Notification service
