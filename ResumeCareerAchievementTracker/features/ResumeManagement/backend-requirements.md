# Backend Requirements - Resume Management

## API Endpoints
- POST /api/resumes - Create new resume version
- PUT /api/resumes/{id}/tailor - Customize for job description
- POST /api/resumes/{id}/optimize - Run ATS optimization
- POST /api/resumes/{id}/export - Generate PDF/Word file
- GET /api/resumes/{id}/keywords - Analyze keyword matching

## Domain Events
- ResumeVersionCreated
- ResumeTailored
- ResumeReviewed
- ResumeExported
- KeywordOptimized

## Database Schema
```sql
CREATE TABLE Resumes (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    VersionNumber INT,
    TargetRole NVARCHAR(200),
    TargetCompany NVARCHAR(200),
    Content NVARCHAR(MAX),
    Format VARCHAR(20),
    CreatedAt DATETIME2,
    LastModified DATETIME2
);

CREATE TABLE ResumeAchievements (
    ResumeId UNIQUEIDENTIFIER,
    AchievementId UNIQUEIDENTIFIER,
    DisplayOrder INT,
    CustomizedText NVARCHAR(MAX),
    PRIMARY KEY (ResumeId, AchievementId)
);

CREATE TABLE ATSOptimizations (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    ResumeId UNIQUEIDENTIFIER,
    TargetKeywords NVARCHAR(MAX),
    MatchScore DECIMAL(5,2),
    Suggestions NVARCHAR(MAX),
    OptimizedAt DATETIME2
);
```
