# Backend Requirements - Risk Assessment

## API Endpoints

### GET /api/risk/cardiovascular
Calculate cardiovascular risk based on BP trends.
```json
Response: {
  "riskLevel": "moderate",
  "riskScore": 6.5,
  "contributingFactors": [
    {"factor": "elevated_bp", "impact": "high"},
    {"factor": "uncontrolled_hypertension_duration", "impact": "medium"}
  ],
  "recommendations": [
    "Consult doctor about medication adjustment",
    "Increase exercise to 150 min/week"
  ],
  "assessmentDate": "2025-12-29T00:00:00Z"
}
```
**Events:** CardiovascularRiskAssessed

### GET /api/risk/target-organ-damage
Assess risk of organ damage from prolonged hypertension.
```json
Response: {
  "overallRisk": "elevated",
  "atRiskOrgans": [
    {"organ": "heart", "riskLevel": "moderate"},
    {"organ": "kidneys", "riskLevel": "moderate"},
    {"organ": "eyes", "riskLevel": "low"}
  ],
  "durationOfElevation": 90,
  "urgency": "schedule_appointment_soon"
}
```
**Events:** TargetOrganDamageRiskElevated

### GET /api/risk/history
Get historical risk assessments.

## Domain Models

```csharp
public class RiskAssessment : AggregateRoot
{
    public Guid AssessmentId { get; private set; }
    public Guid UserId { get; private set; }
    public RiskLevel Level { get; private set; }
    public double RiskScore { get; private set; }
    public List<RiskFactor> ContributingFactors { get; private set; }
    public List<string> Recommendations { get; private set; }
    public DateTime AssessedAt { get; private set; }
}

public enum RiskLevel
{
    Low,
    Moderate,
    High,
    VeryHigh
}

public class RiskFactor : ValueObject
{
    public string Factor { get; private set; }
    public string Impact { get; private set; }
    public double Score { get; private set; }
}

public class OrganDamageRisk : AggregateRoot
{
    public Guid RiskId { get; private set; }
    public Guid UserId { get; private set; }
    public RiskLevel OverallRisk { get; private set; }
    public List<OrganRisk> AtRiskOrgans { get; private set; }
    public int DurationOfElevation { get; private set; }
    public string Urgency { get; private set; }
    public DateTime AssessedAt { get; private set; }
}

public class OrganRisk : ValueObject
{
    public string Organ { get; private set; }
    public RiskLevel RiskLevel { get; private set; }
}
```

## Business Rules

### BR-RA-001: Cardiovascular Risk Calculation
Based on:
- Average BP over last 90 days
- Duration of uncontrolled hypertension
- BP volatility
- Medication adherence
- Age (if available in user profile)

Risk Score (0-10):
- 0-3: Low
- 4-6: Moderate
- 7-8: High
- 9-10: Very High

### BR-RA-002: Target Organ Damage Risk
Organs assessed:
- Heart (left ventricular hypertrophy risk)
- Kidneys (chronic kidney disease risk)
- Eyes (retinopathy risk)
- Brain (stroke risk)

Factors:
- Duration of BP >140/90:
  - <30 days: Low
  - 30-90 days: Moderate
  - >90 days: High
- Severity of elevation
- Presence of hypertensive crises

### BR-RA-003: Assessment Frequency
- Auto-assess monthly
- Assess after 7+ consecutive high readings
- Assess after hypertensive crisis
- User can request on-demand

### BR-RA-004: Urgency Classification
- `monitor`: Continue current plan
- `schedule_appointment_soon`: Within 2-4 weeks
- `schedule_appointment_urgent`: Within 1 week
- `seek_immediate_care`: Go to ER/urgent care

## Event Handlers

### On BloodPressureRisingTrend
- Trigger risk reassessment
- If risk level increases, send alert

### On HypertensiveCrisisDetected
- Immediate risk assessment
- Elevate urgency level

## Event Publishing

```json
CardiovascularRiskAssessed: {
  "eventType": "CardiovascularRiskAssessed",
  "data": {
    "userId": "uuid",
    "riskLevel": "moderate",
    "contributingFactors": ["elevated_bp", "duration"],
    "riskScore": 6.5,
    "assessmentDate": "2025-12-29T00:00:00Z"
  }
}

TargetOrganDamageRiskElevated: {
  "eventType": "TargetOrganDamageRiskElevated",
  "data": {
    "userId": "uuid",
    "riskOrgans": ["heart", "kidneys"],
    "durationOfElevation": 95,
    "riskLevel": "high"
  }
}
```

## Database Schema

```sql
CREATE TABLE RiskAssessments (
    AssessmentId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    RiskLevel NVARCHAR(20) NOT NULL,
    RiskScore DECIMAL(3,1),
    ContributingFactors NVARCHAR(MAX),
    Recommendations NVARCHAR(MAX),
    AssessedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE OrganDamageRisks (
    RiskId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    OverallRisk NVARCHAR(20),
    AtRiskOrgans NVARCHAR(MAX),
    DurationOfElevation INT,
    Urgency NVARCHAR(50),
    AssessedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```

## External Risk Calculators

Consider integration with:
- Framingham Risk Score (CVD risk)
- ACC/AHA ASCVD Risk Calculator
- WHO CVD Risk Charts

Requires additional data:
- Age, gender, smoking status, cholesterol levels, diabetes status

## Medical Disclaimer

All risk assessments must include:
- "This is not medical advice"
- "Consult healthcare provider for diagnosis"
- "Risk calculation based on BP data only"
- "Additional factors may affect actual risk"
