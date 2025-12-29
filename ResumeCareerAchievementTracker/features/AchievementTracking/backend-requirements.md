# Backend Requirements - Achievement Tracking

## API Endpoints
- POST /api/achievements - Log new achievement
- GET /api/achievements - List all achievements with filters
- PUT /api/achievements/{id}/quantify - Add metrics to achievement
- POST /api/achievements/{id}/categorize - Assign categories and skills

## Domain Events
- AchievementLogged
- AchievementQuantified
- AchievementCategorized
- RecognitionReceived

## Database Schema
```sql
CREATE TABLE Achievements (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Title NVARCHAR(200),
    Description NVARCHAR(MAX),
    AchievementDate DATE,
    Category VARCHAR(50),
    PositionId UNIQUEIDENTIFIER,
    ProjectId UNIQUEIDENTIFIER,
    ImpactMetrics NVARCHAR(MAX),
    CreatedAt DATETIME2
);

CREATE TABLE AchievementSkills (
    AchievementId UNIQUEIDENTIFIER,
    SkillId UNIQUEIDENTIFIER,
    RelevanceLevel INT,
    PRIMARY KEY (AchievementId, SkillId)
);

CREATE TABLE ImpactMetrics (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AchievementId UNIQUEIDENTIFIER,
    MetricType VARCHAR(50),
    MetricValue DECIMAL(18,2),
    BaselineComparison DECIMAL(18,2),
    PercentageImprovement DECIMAL(5,2)
);
```
