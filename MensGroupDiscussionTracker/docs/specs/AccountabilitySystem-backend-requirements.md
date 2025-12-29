# Backend Requirements - Accountability System

## API Endpoints

### POST /api/accountability/goals
**Request**: `{ description, timeframe, checkInFrequency, partnerIds }`
**Response**: 201 Created

### POST /api/accountability/check-ins
**Request**: `{ goalId, progressReport, challenges, victories }`
**Response**: 201 Created

### POST /api/accountability/struggles
**Request**: `{ struggleType, description, supportRequested }`
**Response**: 201 Created

### GET /api/accountability/partners/{id}/goals
**Response**: Partner's shared accountability goals

## Domain Events
- AccountabilityGoalSet
- ProgressCheckInCompleted
- AccountabilityPartnerAssigned
- GoalAchieved
- StrugglesShared

## Database Schema
```sql
CREATE TABLE AccountabilityGoals (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    MemberId UNIQUEIDENTIFIER NOT NULL,
    Description NVARCHAR(1000) NOT NULL,
    Timeframe VARCHAR(100),
    CheckInFrequency VARCHAR(50),
    Status VARCHAR(20),
    CreatedAt DATETIME2 NOT NULL,
    CompletedAt DATETIME2
);

CREATE TABLE AccountabilityPartners (
    GoalId UNIQUEIDENTIFIER NOT NULL,
    PartnerId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (GoalId, PartnerId)
);

CREATE TABLE ProgressCheckIns (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    GoalId UNIQUEIDENTIFIER NOT NULL,
    ProgressReport NVARCHAR(MAX),
    Challenges NVARCHAR(MAX),
    Victories NVARCHAR(MAX),
    CheckInDate DATETIME2 NOT NULL
);
```
