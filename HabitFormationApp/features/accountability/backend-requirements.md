# Accountability - Backend Requirements

## Overview
The Accountability feature enables users to add accountability partners who can monitor progress, send encouragement, and receive alerts when users need support.

## Domain Events
- **AccountabilityPartnerAdded**: When a user adds an accountability partner
- **AccountabilityCheckInCompleted**: When a user completes a check-in with their partner
- **PartnerEncouragementSent**: When a partner sends encouragement
- **PartnerAlertTriggered**: When partner is notified of potential issues (missed habits, broken streaks)

## Aggregates

### AccountabilityPartnership
**Properties:**
- `Id` (Guid): Unique identifier
- `UserId` (Guid): User who created the partnership
- `PartnerId` (Guid): Partner user
- `HabitId` (Guid?): Specific habit (null for all habits)
- `Status` (PartnershipStatus): Pending, Active, Declined, Ended
- `PermissionLevel` (PermissionLevel): View, Comment, Encourage
- `CreatedAt` (DateTime)
- `AcceptedAt` (DateTime?)
- `EndedAt` (DateTime?)
- `LastCheckInAt` (DateTime?)

### CheckIn
**Properties:**
- `Id` (Guid)
- `PartnershipId` (Guid)
- `UserId` (Guid)
- `Message` (string)
- `Mood` (MoodLevel)
- `Challenges` (string?)
- `CreatedAt` (DateTime)

## Commands

### AddAccountabilityPartnerCommand
```csharp
public record AddAccountabilityPartnerCommand(
    Guid UserId,
    Guid PartnerId,
    Guid? HabitId,
    PermissionLevel PermissionLevel,
    string Message
);
```

### CompleteCheckInCommand
```csharp
public record CompleteCheckInCommand(
    Guid PartnershipId,
    Guid UserId,
    string Message,
    MoodLevel Mood,
    string? Challenges
);
```

### SendEncouragementCommand
```csharp
public record SendEncouragementCommand(
    Guid PartnershipId,
    Guid SenderId,
    string Message,
    EncouragementType Type
);
```

## API Endpoints

### POST /api/accountability/partners
Add accountability partner (sends invitation)

### GET /api/accountability/partners
Get all partnerships

### POST /api/accountability/checkins
Complete check-in

### POST /api/accountability/encouragement
Send encouragement to partner

### GET /api/accountability/partners/{id}/progress
Get partner's habit progress

## Business Rules
1. Users can have up to 5 active accountability partners
2. Partners must accept invitation
3. Partners can view habits based on permission level
4. Automatic alerts when user misses 3+ consecutive days
5. Weekly check-in reminders
6. Privacy controls on shared data

## Database Schema

```sql
CREATE TABLE AccountabilityPartnerships (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    PartnerId UNIQUEIDENTIFIER NOT NULL,
    HabitId UNIQUEIDENTIFIER NULL,
    Status NVARCHAR(20) NOT NULL,
    PermissionLevel NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    AcceptedAt DATETIME2 NULL,
    EndedAt DATETIME2 NULL,
    LastCheckInAt DATETIME2 NULL,
    CONSTRAINT FK_Partnerships_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_Partnerships_Partners FOREIGN KEY (PartnerId) REFERENCES Users(Id),
    CONSTRAINT FK_Partnerships_Habits FOREIGN KEY (HabitId) REFERENCES Habits(Id)
);

CREATE TABLE CheckIns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    PartnershipId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Message NVARCHAR(500) NOT NULL,
    Mood NVARCHAR(20) NOT NULL,
    Challenges NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL,
    CONSTRAINT FK_CheckIns_Partnerships FOREIGN KEY (PartnershipId) REFERENCES AccountabilityPartnerships(Id)
);

CREATE TABLE Encouragements (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    PartnershipId UNIQUEIDENTIFIER NOT NULL,
    SenderId UNIQUEIDENTIFIER NOT NULL,
    ReceiverId UNIQUEIDENTIFIER NOT NULL,
    Message NVARCHAR(500) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    ReadAt DATETIME2 NULL,
    CONSTRAINT FK_Encouragements_Partnerships FOREIGN KEY (PartnershipId) REFERENCES AccountabilityPartnerships(Id)
);
```

## Integration Points
- Notification service: Send partner alerts and encouragements
- User service: Validate partner relationships
- Habit service: Share habit progress data
- Analytics service: Track partnership effectiveness

## Security & Privacy
- Users control what partners can see
- Partners cannot modify user's habits
- Encrypted messages
- Ability to remove partners anytime
- Report/block inappropriate behavior
