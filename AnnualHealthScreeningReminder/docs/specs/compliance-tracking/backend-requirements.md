# Compliance Tracking - Backend Requirements

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/compliance | Get compliance status |
| GET | /api/compliance/score | Get compliance score |
| GET | /api/compliance/history | Get compliance history |

## Domain Events

| Event | Trigger | Key Data |
|-------|---------|----------|
| ComplianceAchieved | All screenings current | userId, achievementDate, score |
| ComplianceLapsed | Screening becomes overdue | userId, overdueScreenings |

## Database Schema

```sql
CREATE TABLE ComplianceRecords (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Score DECIMAL(5,2),
    TotalScreenings INT,
    CurrentScreenings INT,
    OverdueScreenings INT,
    CalculatedAt DATETIME2 NOT NULL
);
```
