# Backend Requirements - Gratitude Journaling

## API Endpoints

### POST /api/gratitude-entries
Create a new gratitude journal entry.

**Request Body**:
```json
{
  "content": "string (required, max 2000 chars)",
  "category": "enum (action|quality|moment) (required)",
  "privacyLevel": "enum (private|shared) (required)",
  "spouseId": "guid (required)"
}
```

**Response**: 201 Created
```json
{
  "id": "guid",
  "authorId": "guid",
  "spouseId": "guid",
  "content": "string",
  "category": "string",
  "privacyLevel": "string",
  "createdAt": "datetime",
  "sharedAt": "datetime?"
}
```

### GET /api/gratitude-entries
Retrieve gratitude entries for the authenticated user.

**Query Parameters**:
- `includePrivate` (boolean, default: true)
- `includeSpouseShared` (boolean, default: true)
- `category` (string, optional filter)
- `startDate` (datetime, optional)
- `endDate` (datetime, optional)
- `page` (int, default: 1)
- `pageSize` (int, default: 20, max: 100)

**Response**: 200 OK
```json
{
  "items": [...],
  "totalCount": 0,
  "page": 1,
  "pageSize": 20
}
```

### PUT /api/gratitude-entries/{id}/share
Share a private gratitude entry with spouse.

**Response**: 200 OK

### POST /api/gratitude-entries/{id}/acknowledge
Acknowledge a gratitude entry from spouse.

**Request Body**:
```json
{
  "responseMessage": "string (optional, max 500 chars)",
  "emotionalReaction": "enum (grateful|loved|happy|touched|surprised) (optional)"
}
```

**Response**: 201 Created

### GET /api/gratitude-entries/streaks
Get streak information for authenticated user.

**Response**: 200 OK
```json
{
  "currentStreak": 7,
  "longestStreak": 15,
  "totalEntries": 42,
  "nextMilestone": 30,
  "achievedMilestones": [7, 14]
}
```

## Domain Events

### GratitudeEntryCreated
**Payload**:
```json
{
  "entryId": "guid",
  "authorId": "guid",
  "spouseId": "guid",
  "content": "string",
  "category": "string",
  "privacyLevel": "string",
  "createdAt": "datetime"
}
```

**Handlers**:
- Update gratitude feed
- Check for streak achievement
- Update positivity metrics

### GratitudeSharedWithSpouse
**Payload**:
```json
{
  "entryId": "guid",
  "sharedAt": "datetime",
  "deliveryMethod": "string",
  "partnerId": "guid"
}
```

**Handlers**:
- Send notification to spouse
- Update emotional connection tracker
- Log appreciation delivery

### GratitudeAcknowledged
**Payload**:
```json
{
  "entryId": "guid",
  "acknowledgedBy": "guid",
  "acknowledgedAt": "datetime",
  "responseMessage": "string",
  "emotionalReaction": "string"
}
```

**Handlers**:
- Update feedback loop tracker
- Notify original author
- Update relationship satisfaction metrics

### GratitudeStreakAchieved
**Payload**:
```json
{
  "userId": "guid",
  "streakLength": 0,
  "streakType": "string",
  "achievedAt": "datetime",
  "consistencyScore": 0.0
}
```

**Handlers**:
- Send achievement notification
- Update user badges
- Trigger celebration animation

## Database Schema

### GratitudeEntries Table
```sql
CREATE TABLE GratitudeEntries (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AuthorId UNIQUEIDENTIFIER NOT NULL,
    SpouseId UNIQUEIDENTIFIER NOT NULL,
    Content NVARCHAR(2000) NOT NULL,
    Category VARCHAR(20) NOT NULL CHECK (Category IN ('action', 'quality', 'moment')),
    PrivacyLevel VARCHAR(20) NOT NULL CHECK (PrivacyLevel IN ('private', 'shared')),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    SharedAt DATETIME2 NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (AuthorId) REFERENCES Users(Id),
    FOREIGN KEY (SpouseId) REFERENCES Users(Id)
);

CREATE INDEX IX_GratitudeEntries_AuthorId ON GratitudeEntries(AuthorId);
CREATE INDEX IX_GratitudeEntries_SpouseId_SharedAt ON GratitudeEntries(SpouseId, SharedAt);
CREATE INDEX IX_GratitudeEntries_CreatedAt ON GratitudeEntries(CreatedAt);
```

### GratitudeAcknowledgments Table
```sql
CREATE TABLE GratitudeAcknowledgments (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EntryId UNIQUEIDENTIFIER NOT NULL,
    AcknowledgedBy UNIQUEIDENTIFIER NOT NULL,
    ResponseMessage NVARCHAR(500) NULL,
    EmotionalReaction VARCHAR(20) NULL,
    AcknowledgedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (EntryId) REFERENCES GratitudeEntries(Id),
    FOREIGN KEY (AcknowledgedBy) REFERENCES Users(Id)
);

CREATE INDEX IX_GratitudeAcknowledgments_EntryId ON GratitudeAcknowledgments(EntryId);
```

### GratitudeStreaks Table
```sql
CREATE TABLE GratitudeStreaks (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    CurrentStreak INT NOT NULL DEFAULT 0,
    LongestStreak INT NOT NULL DEFAULT 0,
    LastEntryDate DATE NOT NULL,
    TotalEntries INT NOT NULL DEFAULT 0,
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE UNIQUE INDEX IX_GratitudeStreaks_UserId ON GratitudeStreaks(UserId);
```

## Business Logic

### Streak Calculation
- Entry must be created within 24 hours of previous entry to maintain streak
- Streak resets to 0 if more than 48 hours pass without entry
- Milestones at: 7, 14, 30, 60, 90, 180, 365 days

### Privacy Rules
- Private entries only visible to author
- Once shared, entries visible to spouse
- Cannot unshare an entry once shared
- Spouse receives notification when entry is shared

### Validation Rules
- Content cannot be empty or whitespace only
- Category must be valid enum value
- Author and spouse must be in active relationship
- User can only acknowledge entries shared with them

## Performance Requirements
- Entry creation: <100ms
- Feed retrieval: <300ms for 20 items
- Streak calculation: <50ms
- Notification delivery: <30 seconds

## Security Requirements
- Entries encrypted at rest using AES-256
- Only author and (if shared) spouse can access entry
- API endpoints require authentication
- Rate limiting: 100 requests per minute per user
