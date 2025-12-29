# Insurance Integration - Backend Requirements

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/insurance | Get insurance info |
| PUT | /api/insurance | Update insurance |
| GET | /api/insurance/coverage/{screeningId} | Check coverage |
| GET | /api/insurance/benefits | Get preventive benefits |

## Domain Events

| Event | Trigger | Key Data |
|-------|---------|----------|
| InsuranceCoverageVerified | Coverage checked | screeningId, covered, copay |
| PreventiveCareMaximized | Benefits optimized | userId, usedBenefits, savings |

## Database Schema

```sql
CREATE TABLE InsuranceInfo (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Provider NVARCHAR(200),
    PlanName NVARCHAR(200),
    MemberId NVARCHAR(100),
    GroupNumber NVARCHAR(100),
    EffectiveDate DATE,
    TerminationDate DATE
);

CREATE TABLE CoverageVerifications (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    InsuranceId UNIQUEIDENTIFIER NOT NULL,
    ScreeningTypeId UNIQUEIDENTIFIER NOT NULL,
    Covered BIT,
    Copay DECIMAL(10,2),
    PreAuthRequired BIT,
    VerifiedAt DATETIME2
);
```
