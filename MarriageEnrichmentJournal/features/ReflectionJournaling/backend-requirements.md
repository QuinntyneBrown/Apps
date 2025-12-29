# Backend Requirements - Reflection Journaling

## API Endpoints

### GET /api/reflection-prompts
Get available reflection prompts.

**Query Parameters**:
- `category` (individual|couple|conflict|growth)
- `random` (boolean, default: false)

**Response**: 200 OK

### POST /api/reflections
Create a new reflection journal entry.

**Request Body**:
```json
{
  "promptId": "guid (optional)",
  "reflectionContent": "string (required, max 5000 chars)",
  "privacyLevel": "enum (private|spouse_visible|joint) (required)",
  "conflictRelated": "boolean",
  "themesIdentified": ["string"],
  "coAuthorId": "guid (optional, for joint)"
}
```

**Response**: 201 Created

### GET /api/reflections
Retrieve reflection entries.

**Query Parameters**:
- `type` (personal|conflict|growth|weekly_review)
- `privacyLevel`
- `startDate`, `endDate`
- `page`, `pageSize`

**Response**: 200 OK

### POST /api/reflections/weekly-review
Complete weekly relationship review.

**Request Body**:
```json
{
  "highlights": "string (max 2000 chars)",
  "challenges": "string (max 2000 chars)",
  "goalsForNextWeek": "string (max 1000 chars)",
  "satisfactionRating": "int (1-10)",
  "coAuthorId": "guid"
}
```

**Response**: 201 Created

### GET /api/reflections/growth-timeline
Get chronological timeline of growth moments.

**Response**: 200 OK

### POST /api/reflections/{id}/themes
Add or update identified themes.

**Request Body**:
```json
{
  "themes": ["string"]
}
```

**Response**: 200 OK

## Domain Events

### ReflectionJournalCreated
### ConflictReflectionRecorded
### GrowthMomentCaptured
### WeeklyReviewCompleted

## Database Schema

### Reflections Table
```sql
CREATE TABLE Reflections (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AuthorId UNIQUEIDENTIFIER NOT NULL,
    CoAuthorId UNIQUEIDENTIFIER NULL,
    PromptId UNIQUEIDENTIFIER NULL,
    ReflectionContent NVARCHAR(MAX) NOT NULL,
    PrivacyLevel VARCHAR(20) NOT NULL,
    ReflectionType VARCHAR(50) NOT NULL,
    ConflictRelated BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (AuthorId) REFERENCES Users(Id),
    FOREIGN KEY (CoAuthorId) REFERENCES Users(Id),
    FOREIGN KEY (PromptId) REFERENCES ReflectionPrompts(Id)
);
```

### ReflectionThemes Table
```sql
CREATE TABLE ReflectionThemes (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ReflectionId UNIQUEIDENTIFIER NOT NULL,
    Theme VARCHAR(100) NOT NULL,
    IdentifiedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (ReflectionId) REFERENCES Reflections(Id)
);
```

### WeeklyReviews Table
```sql
CREATE TABLE WeeklyReviews (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ReflectionId UNIQUEIDENTIFIER NOT NULL UNIQUE,
    ReviewWeekStart DATE NOT NULL,
    ReviewWeekEnd DATE NOT NULL,
    Highlights NVARCHAR(2000) NOT NULL,
    Challenges NVARCHAR(2000) NOT NULL,
    GoalsForNextWeek NVARCHAR(1000) NOT NULL,
    SatisfactionRating INT NOT NULL CHECK (SatisfactionRating BETWEEN 1 AND 10),
    CompletedAt DATETIME2 NOT NULL,
    FOREIGN KEY (ReflectionId) REFERENCES Reflections(Id)
);
```

### ReflectionPrompts Table
```sql
CREATE TABLE ReflectionPrompts (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PromptText NVARCHAR(500) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    TargetAudience VARCHAR(50) NOT NULL,
    DifficultyLevel INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
```

## Business Logic

### Theme Identification
- Extract common keywords from reflections
- Group related reflections by theme
- Track theme frequency over time

### Privacy Rules
- Private: Only author can view
- Spouse_visible: Both partners can view
- Joint: Collaborative entry by both

### Weekly Review Scheduling
- Suggest review every 7 days
- Send reminder if not completed
- Track completion consistency
