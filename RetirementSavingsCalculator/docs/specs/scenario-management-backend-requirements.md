# Backend Requirements - Scenario Management

## API Endpoints

### POST /api/scenarios
Create retirement scenario
```json
Request: {
  "name": "Retire at 65",
  "currentAge": 35,
  "retirementAge": 65,
  "lifeExpectancy": 90,
  "currentSavings": 50000,
  "monthlyContribution": 500
}
Response: 201 Created {scenarioId, ...}
```
**Events**: RetirementScenarioCreated

### PUT /api/scenarios/{id}/retirement-age
Update retirement age
**Events**: RetirementAgeUpdated

### GET /api/scenarios
List all user scenarios

### DELETE /api/scenarios/{id}
Delete scenario
**Events**: ScenarioDeleted

### POST /api/scenarios/{id}/duplicate
Duplicate scenario for what-if analysis

## Domain Models

```csharp
public class RetirementScenario : AggregateRoot
{
    public Guid ScenarioId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public int CurrentAge { get; private set; }
    public int RetirementAge { get; private set; }
    public int LifeExpectancy { get; private set; }
    public decimal CurrentSavings { get; private set; }
    public decimal MonthlyContribution { get; private set; }
    public AssumptionSet Assumptions { get; private set; }

    public void UpdateRetirementAge(int newAge);
    public void UpdateSavings(decimal amount);
    public RetirementScenario Duplicate(string newName);
}

public class AssumptionSet : ValueObject
{
    public decimal InflationRate { get; private set; } // 0.03 = 3%
    public decimal InvestmentReturnRate { get; private set; }
    public decimal TaxRate { get; private set; }
}
```

## Business Rules
- BR-SM-001: Retirement age must be >= current age
- BR-SM-002: Life expectancy must be > retirement age
- BR-SM-003: Default inflation: 3%, investment return: 7%, tax rate: 22%
- BR-SM-004: User can have max 10 active scenarios

## Database Schema
```sql
CREATE TABLE RetirementScenarios (
    ScenarioId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200),
    CurrentAge INT,
    RetirementAge INT,
    LifeExpectancy INT,
    CurrentSavings DECIMAL(15,2),
    MonthlyContribution DECIMAL(10,2),
    InflationRate DECIMAL(5,4),
    InvestmentReturnRate DECIMAL(5,4),
    TaxRate DECIMAL(5,4),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);
```
