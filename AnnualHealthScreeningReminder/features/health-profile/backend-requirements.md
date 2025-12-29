# Health Profile - Backend Requirements

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/profile | Get health profile |
| PUT | /api/profile | Update profile |
| POST | /api/profile/risk-factors | Add risk factor |
| POST | /api/profile/family-history | Add family history |

## Domain Events

| Event | Trigger | Key Data |
|-------|---------|----------|
| RiskFactorAdded | New risk factor | userId, riskFactor, screeningImpact |
| FamilyHistoryUpdated | History added | userId, condition, relatives |

## Database Schema

```sql
CREATE TABLE HealthProfiles (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    DateOfBirth DATE,
    Gender NVARCHAR(20),
    Height DECIMAL(5,2),
    Weight DECIMAL(5,2)
);

CREATE TABLE RiskFactors (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    FactorType NVARCHAR(100),
    OnsetDate DATE,
    Severity NVARCHAR(50)
);

CREATE TABLE FamilyHistory (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Relative NVARCHAR(50),
    Condition NVARCHAR(200),
    AgeAtDiagnosis INT
);
```
