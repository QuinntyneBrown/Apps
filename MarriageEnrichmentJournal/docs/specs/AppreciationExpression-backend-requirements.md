# Backend Requirements - Appreciation Expression

## API Endpoints

### POST /api/appreciations
Create a new appreciation expression.

**Request Body**:
```json
{
  "recipientId": "guid (required)",
  "appreciationType": "enum (words|actions|qualities) (required)",
  "specificInstance": "string (required, max 1000 chars)",
  "intensityLevel": "int (1-5) (required)"
}
```

**Response**: 201 Created

### GET /api/appreciations
Retrieve appreciations for authenticated user.

**Query Parameters**:
- `type` (filter: sent|received|all, default: all)
- `appreciationType` (words|actions|qualities)
- `page`, `pageSize`

**Response**: 200 OK

### POST /api/appreciations/{id}/reciprocate
Respond to appreciation with own appreciation.

**Request Body**:
```json
{
  "appreciationType": "enum (words|actions|qualities) (required)",
  "specificInstance": "string (required, max 1000 chars)",
  "intensityLevel": "int (1-5) (required)"
}
```

**Response**: 201 Created

### GET /api/love-languages/my-profile
Get love language analysis for authenticated user.

**Response**: 200 OK
```json
{
  "primaryLanguage": "words_of_affirmation",
  "secondaryLanguage": "quality_time",
  "confidenceScore": 0.85,
  "languageBreakdown": {
    "words_of_affirmation": 0.45,
    "acts_of_service": 0.30,
    "receiving_gifts": 0.10,
    "quality_time": 0.10,
    "physical_touch": 0.05
  },
  "analysisDate": "datetime"
}
```

### GET /api/love-languages/suggestions
Get personalized suggestions for expressing appreciation to spouse.

**Response**: 200 OK
```json
{
  "suggestions": [
    {
      "suggestion": "string",
      "loveLanguage": "string",
      "category": "string"
    }
  ]
}
```

## Domain Events

### AppreciationExpressed
**Payload**:
```json
{
  "appreciationId": "guid",
  "expresserId": "guid",
  "recipientId": "guid",
  "appreciationType": "string",
  "specificInstance": "string",
  "intensityLevel": 0,
  "expressedAt": "datetime"
}
```

### AppreciationReciprocated
**Payload**:
```json
{
  "originalAppreciationId": "guid",
  "reciprocationId": "guid",
  "reciprocatorId": "guid",
  "timeToReciprocate": "duration",
  "reciprocationContent": "string",
  "reciprocatedAt": "datetime"
}
```

### LoveLanguageIdentified
**Payload**:
```json
{
  "userId": "guid",
  "primaryLanguage": "string",
  "confidenceScore": 0.0,
  "supportingEvidence": [],
  "identifiedAt": "datetime"
}
```

## Database Schema

### Appreciations Table
```sql
CREATE TABLE Appreciations (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ExpresserId UNIQUEIDENTIFIER NOT NULL,
    RecipientId UNIQUEIDENTIFIER NOT NULL,
    AppreciationType VARCHAR(20) NOT NULL CHECK (AppreciationType IN ('words', 'actions', 'qualities')),
    SpecificInstance NVARCHAR(1000) NOT NULL,
    IntensityLevel INT NOT NULL CHECK (IntensityLevel BETWEEN 1 AND 5),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ReciprocatedByAppreciationId UNIQUEIDENTIFIER NULL,
    FOREIGN KEY (ExpresserId) REFERENCES Users(Id),
    FOREIGN KEY (RecipientId) REFERENCES Users(Id),
    FOREIGN KEY (ReciprocatedByAppreciationId) REFERENCES Appreciations(Id)
);
```

### LoveLanguageProfiles Table
```sql
CREATE TABLE LoveLanguageProfiles (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL UNIQUE,
    PrimaryLanguage VARCHAR(50) NOT NULL,
    SecondaryLanguage VARCHAR(50) NULL,
    ConfidenceScore DECIMAL(3,2) NOT NULL,
    LanguageBreakdownJson NVARCHAR(MAX) NOT NULL,
    LastAnalyzedAt DATETIME2 NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

## Business Logic

### Love Language Detection
- Analyze last 30 appreciations received
- Map appreciation types to love languages
- Require minimum 10 appreciations for analysis
- Update confidence score based on consistency
- Re-analyze monthly or when new data available

### Reciprocity Tracking
- Track time between appreciation and reciprocation
- Calculate reciprocity balance ratio
- Alert if imbalance exceeds threshold

## Performance Requirements
- Appreciation creation: <100ms
- Love language analysis: <500ms
- Suggestion generation: <200ms
