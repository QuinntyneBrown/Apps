# Backend Requirements - Milestones & Celebrations

## API Endpoints

### POST /api/milestones
Create a relationship milestone.

**Request Body**:
```json
{
  "milestoneType": "enum (anniversary|achievement|special_event|custom) (required)",
  "date": "date (required)",
  "title": "string (required, max 200 chars)",
  "significance": "string (max 1000 chars)",
  "celebrationNotes": "string (optional)",
  "photos": ["photoId"]
}
```

**Response**: 201 Created

### GET /api/milestones
Retrieve milestones.

**Query Parameters**:
- `type` (filter by milestone type)
- `year` (filter by year)
- `upcoming` (boolean, for reminder purposes)
- `page`, `pageSize`

**Response**: 200 OK

### GET /api/milestones/timeline
Get chronological timeline of all milestones.

**Response**: 200 OK

### POST /api/milestones/{id}/photos
Upload photos for a milestone.

**Request**: Multipart form data
**Response**: 201 Created

### GET /api/milestones/upcoming-reminders
Get upcoming milestone reminders.

**Query Parameters**:
- `days` (int, default: 30)

**Response**: 200 OK

### POST /api/achievements/app-usage
Record app usage achievement.

**Request Body**:
```json
{
  "achievementType": "enum (streak|entries|goals|wins)",
  "milestone": "int",
  "achievedAt": "datetime"
}
```

**Response**: 201 Created

## Domain Events

### MilestoneCelebrated
**Payload**:
```json
{
  "milestoneId": "guid",
  "milestoneType": "string",
  "date": "date",
  "significance": "string",
  "celebrationNotes": "string",
  "photos": [],
  "celebratedAt": "datetime"
}
```

**Handlers**:
- Add to memory archive
- Schedule anniversary reminder
- Update relationship timeline

### PositivePatternIdentified
**Payload**:
```json
{
  "patternId": "guid",
  "patternType": "string",
  "frequency": "string",
  "examples": [],
  "bothPartnersContribution": boolean,
  "identifiedAt": "datetime"
}
```

**Handlers**:
- Send encouragement notification
- Add to strengths list
- Suggest amplification strategies

## Database Schema

### Milestones Table
```sql
CREATE TABLE Milestones (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CoupleId UNIQUEIDENTIFIER NOT NULL,
    MilestoneType VARCHAR(50) NOT NULL,
    Date DATE NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Significance NVARCHAR(1000) NULL,
    CelebrationNotes NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (CoupleId) REFERENCES Couples(Id),
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);

CREATE INDEX IX_Milestones_CoupleId_Date ON Milestones(CoupleId, Date);
```

### MilestonePhotos Table
```sql
CREATE TABLE MilestonePhotos (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MilestoneId UNIQUEIDENTIFIER NOT NULL,
    PhotoUrl NVARCHAR(500) NOT NULL,
    Caption NVARCHAR(200) NULL,
    UploadedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UploadedBy UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (MilestoneId) REFERENCES Milestones(Id),
    FOREIGN KEY (UploadedBy) REFERENCES Users(Id)
);
```

### AppUsageAchievements Table
```sql
CREATE TABLE AppUsageAchievements (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    AchievementType VARCHAR(50) NOT NULL,
    Milestone INT NOT NULL,
    AchievedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    Acknowledged BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_AppUsageAchievements_UserId ON AppUsageAchievements(UserId);
```

### MilestoneReminders Table
```sql
CREATE TABLE MilestoneReminders (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MilestoneId UNIQUEIDENTIFIER NOT NULL,
    ReminderDate DATE NOT NULL,
    ReminderType VARCHAR(50) NOT NULL,
    IsSent BIT NOT NULL DEFAULT 0,
    SentAt DATETIME2 NULL,
    FOREIGN KEY (MilestoneId) REFERENCES Milestones(Id)
);

CREATE INDEX IX_MilestoneReminders_ReminderDate ON MilestoneReminders(ReminderDate);
```

## Business Logic

### Reminder Scheduling
- Create reminders 1 week and 1 day before milestone date
- For anniversaries, create reminder 1 month, 1 week, and 1 day before
- Send reminders at optimal time (morning of reminder day)
- Mark as sent after delivery

### Achievement Detection
- Monitor for specific thresholds (7, 14, 30, 60, 90, 180, 365 day streaks)
- Detect total entry milestones (10, 25, 50, 100, 250, 500)
- Track goal completions (1, 5, 10, 25)
- Celebrate communication wins milestones

### Photo Management
- Support JPEG, PNG formats
- Max 10MB per photo
- Store in cloud storage
- Generate thumbnails
- Limit 20 photos per milestone

## Performance Requirements
- Milestone creation: <200ms
- Timeline retrieval: <500ms
- Photo upload: <3 seconds per photo
- Reminder processing: Batch job runs daily at 2 AM
