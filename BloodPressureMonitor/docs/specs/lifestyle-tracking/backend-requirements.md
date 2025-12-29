# Backend Requirements - Lifestyle Tracking

## API Endpoints

### POST /api/lifestyle/factors
Log lifestyle factor.
```json
Request: {
  "factorType": "sodium",
  "value": 2400,
  "unit": "mg",
  "logDate": "2025-12-29",
  "notes": "High sodium day - ate out"
}
```
**Events:** LifestyleFactorLogged, SodiumIntakeTracked

### GET /api/lifestyle/correlations
Analyze correlations between lifestyle factors and BP.
```json
Response: {
  "factor": "sodium",
  "correlationStrength": 0.72,
  "impactDescription": "High sodium intake associated with 8 mmHg higher systolic BP",
  "recommendations": ["Limit sodium to <2300mg/day", "Choose low-sodium foods"]
}
```
**Events:** StressCorrelationIdentified, ExerciseImpactAnalyzed

### POST /api/lifestyle/exercise
Log exercise activity.
```json
Request: {
  "exerciseType": "cardio",
  "duration": 30,
  "intensity": "moderate",
  "completedAt": "2025-12-29T07:00:00Z"
}
```

### POST /api/lifestyle/stress
Log stress level.
```json
Request: {
  "stressLevel": 7,
  "triggers": ["work deadline", "traffic"],
  "logDate": "2025-12-29"
}
```

## Domain Models

```csharp
public class LifestyleFactor : AggregateRoot
{
    public Guid FactorId { get; private set; }
    public Guid UserId { get; private set; }
    public FactorType Type { get; private set; }
    public double Value { get; private set; }
    public string Unit { get; private set; }
    public DateTime LogDate { get; private set; }
    public string Notes { get; private set; }
}

public enum FactorType
{
    Sodium,
    Exercise,
    Stress,
    Alcohol,
    Caffeine,
    Sleep
}

public class FactorCorrelation : ValueObject
{
    public FactorType Factor { get; private set; }
    public double CorrelationStrength { get; private set; }
    public double BPImpact { get; private set; }
    public double ConfidenceLevel { get; private set; }
}
```

## Business Rules

### BR-LT-001: Correlation Analysis Criteria
- Require at least 30 data points for factor
- Require at least 30 BP readings in same period
- Calculate Pearson correlation coefficient
- Flag if correlation > 0.5 (moderate) or > 0.7 (strong)

### BR-LT-002: Sodium Recommendations
- Daily target: <2300mg
- Alert if exceeds 3000mg
- Correlate with next-day BP readings

### BR-LT-003: Exercise Impact
- Compare BP before/after exercise (immediate effect)
- Compare average BP on exercise days vs non-exercise days
- Identify optimal exercise types

### BR-LT-004: Stress Correlation
- Correlate stress level (1-10) with BP readings same day
- Identify stress threshold that impacts BP
- Recommend interventions if strong correlation

## Database Schema

```sql
CREATE TABLE LifestyleFactors (
    FactorId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    FactorType NVARCHAR(50) NOT NULL,
    Value DECIMAL(10,2) NOT NULL,
    Unit NVARCHAR(20),
    LogDate DATE NOT NULL,
    Notes NVARCHAR(500),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);

CREATE TABLE FactorCorrelations (
    CorrelationId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    FactorType NVARCHAR(50) NOT NULL,
    CorrelationStrength DECIMAL(3,2),
    BPImpactSystolic DECIMAL(5,2),
    BPImpactDiastolic DECIMAL(5,2),
    ConfidenceLevel DECIMAL(3,2),
    AnalyzedAt DATETIME2 DEFAULT GETUTCDATE()
);
```
