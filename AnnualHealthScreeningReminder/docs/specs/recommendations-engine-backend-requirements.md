# Recommendations Engine - Backend Requirements

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/recommendations | Get personalized recommendations |
| GET | /api/recommendations/{id} | Get recommendation details |
| POST | /api/recommendations/recalculate | Recalculate based on profile changes |
| GET | /api/screening-guidelines | Get clinical guidelines |

## Domain Events

| Event | Trigger | Key Data |
|-------|---------|----------|
| ScreeningRecommendationGenerated | Age/profile trigger | recommendationId, screeningType, reason, dueDate |
| RecommendationCustomized | Risk factor added | userId, screeningType, adjustedInterval |
| AgeBasedScreeningTriggered | Birthday milestone | userId, newScreenings, ageThreshold |

## Database Schema

```sql
CREATE TABLE Recommendations (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ScreeningTypeId UNIQUEIDENTIFIER NOT NULL,
    ReasonCode NVARCHAR(100),
    DueDate DATE,
    Priority INT,
    Status NVARCHAR(50),
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE ClinicalGuidelines (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ScreeningTypeId UNIQUEIDENTIFIER NOT NULL,
    MinAge INT,
    MaxAge INT,
    Gender NVARCHAR(20),
    IntervalMonths INT,
    RiskFactorAdjustments NVARCHAR(MAX),
    Source NVARCHAR(500)
);
```
