# Backend Requirements - Communication Tools

## API Endpoints

### GET /api/conversation-prompts
Get conversation starter prompts.

**Query Parameters**:
- `category` (romantic|family|deep|light)
- `count` (int, default: 1)
- `exclude` (comma-separated prompt IDs)

**Response**: 200 OK

### POST /api/prompts/{id}/answer
Submit answer to conversation prompt.

**Request Body**:
```json
{
  "responseContent": "string (required, max 2000 chars)",
  "shareWithSpouse": "boolean (default: true)"
}
```

**Response**: 201 Created

### POST /api/joint-journals
Create collaborative journal entry.

**Request Body**:
```json
{
  "title": "string (required)",
  "content": "string (required)",
  "coAuthorId": "guid (required)",
  "startedAt": "datetime"
}
```

**Response**: 201 Created

### PUT /api/joint-journals/{id}
Update joint journal entry (collaborative editing).

**Request Body**:
```json
{
  "content": "string",
  "contributorId": "guid",
  "lastModified": "datetime"
}
```

**Response**: 200 OK

### POST /api/communication-goals
Set a communication goal for the couple.

**Request Body**:
```json
{
  "goalDescription": "string (required)",
  "targetBehavior": "string (required)",
  "successCriteria": "string (required)",
  "timeline": "string (e.g., '30 days')",
  "partnerId": "guid (required)"
}
```

**Response**: 201 Created

### POST /api/communication-wins
Record a communication success.

**Request Body**:
```json
{
  "whatHappened": "string (required)",
  "skillsUsed": ["string"],
  "outcome": "string (required)",
  "celebrationNotes": "string (optional)"
}
```

**Response**: 201 Created

### GET /api/communication-wins
Get communication success history.

**Response**: 200 OK

## Domain Events

### PromptAnswered
### JointJournalEntryCreated
### CommunicationGoalSet
### CommunicationWinRecorded

## Database Schema

### ConversationPrompts Table
```sql
CREATE TABLE ConversationPrompts (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PromptText NVARCHAR(500) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    DifficultyLevel INT NOT NULL,
    UsageCount INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1
);
```

### PromptResponses Table
```sql
CREATE TABLE PromptResponses (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PromptId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ResponseContent NVARCHAR(2000) NOT NULL,
    SharedWithSpouse BIT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (PromptId) REFERENCES ConversationPrompts(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### JointJournals Table
```sql
CREATE TABLE JointJournals (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Author1Id UNIQUEIDENTIFIER NOT NULL,
    Author2Id UNIQUEIDENTIFIER NOT NULL,
    StartedAt DATETIME2 NOT NULL,
    CompletedAt DATETIME2 NULL,
    CreationDuration INT NULL,
    AgreementLevel INT NULL,
    FOREIGN KEY (Author1Id) REFERENCES Users(Id),
    FOREIGN KEY (Author2Id) REFERENCES Users(Id)
);
```

### CommunicationGoals Table
```sql
CREATE TABLE CommunicationGoals (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    GoalDescription NVARCHAR(500) NOT NULL,
    TargetBehavior NVARCHAR(500) NOT NULL,
    SuccessCriteria NVARCHAR(500) NOT NULL,
    Timeline VARCHAR(100) NOT NULL,
    SetById UNIQUEIDENTIFIER NOT NULL,
    PartnerId UNIQUEIDENTIFIER NOT NULL,
    SetAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CompletedAt DATETIME2 NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (SetById) REFERENCES Users(Id),
    FOREIGN KEY (PartnerId) REFERENCES Users(Id)
);
```

### CommunicationWins Table
```sql
CREATE TABLE CommunicationWins (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    WhatHappened NVARCHAR(1000) NOT NULL,
    SkillsUsed NVARCHAR(500) NULL,
    Outcome NVARCHAR(1000) NOT NULL,
    CelebrationNotes NVARCHAR(500) NULL,
    RecordedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

## Business Logic

### Collaborative Editing
- Use operational transformation or CRDT for conflict resolution
- Track each partner's contributions
- Measure time to completion
- Calculate agreement level based on edits

### Prompt Effectiveness
- Track which prompts lead to responses
- Measure depth of sharing
- Rotate prompts to avoid repetition

## Performance Requirements
- Real-time collaboration latency: <500ms
- Prompt generation: <100ms
- Joint journal updates: <200ms
