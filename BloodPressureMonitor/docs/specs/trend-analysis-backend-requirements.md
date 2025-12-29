# Backend Requirements - Trend Analysis

## Overview
Analyzes blood pressure patterns over time to identify trends, calculate statistics, and detect volatility.

## API Endpoints

### GET /api/trends/analysis
Get comprehensive trend analysis for a time period.

**Query Parameters:**
- `startDate` (required)
- `endDate` (required)
- `analysisType` (optional): daily, weekly, monthly

**Response:** 200 OK
```json
{
  "period": {"startDate": "2025-12-01", "endDate": "2025-12-29"},
  "averages": {"systolic": 125, "diastolic": 82, "pulse": 73},
  "trendDirection": "rising",
  "rateOfChange": {"systolic": 2.5, "diastolic": 1.2},
  "variance": {"systolic": 8.5, "diastolic": 4.2},
  "confidenceLevel": 0.85,
  "dataPoints": 56
}
```

**Domain Events Triggered:**
- TrendAnalysisCompleted

### GET /api/trends/volatility
Check for BP volatility patterns.

**Response:** 200 OK
```json
{
  "volatilityScore": 7.5,
  "fluctuationPattern": "high_variability",
  "concernLevel": "moderate",
  "recommendations": ["Review measurement technique", "Take readings at consistent times"]
}
```

**Domain Events Triggered:**
- VolatilityDetected (if score exceeds threshold)

### GET /api/trends/predictions
Get predicted future BP values based on current trend.

**Response:** 200 OK
```json
{
  "predictedValues": [
    {"date": "2026-01-15", "systolic": 128, "diastolic": 84, "confidence": 0.75},
    {"date": "2026-02-15", "systolic": 131, "diastolic": 86, "confidence": 0.65}
  ],
  "trendType": "rising",
  "modelAccuracy": 0.82
}
```

## Domain Models

```csharp
public class TrendAnalysis : AggregateRoot
{
    public Guid AnalysisId { get; private set; }
    public Guid UserId { get; private set; }
    public DateRange Period { get; private set; }
    public BPAverages Averages { get; private set; }
    public TrendDirection Direction { get; private set; }
    public RateOfChange RateOfChange { get; private set; }
    public double ConfidenceLevel { get; private set; }
    public DateTime AnalyzedAt { get; private set; }
}

public enum TrendDirection { Rising, Lowering, Stable, Volatile }
```

## Business Rules

### BR-TA-001: Minimum Data Points
- Require at least 7 readings for trend analysis
- At least 14 readings for high-confidence predictions

### BR-TA-002: Rising Trend Detection
- Systolic or diastolic increasing >5 mmHg per month
- At least 70% of readings showing upward pattern
- Statistical significance (p < 0.05)

### BR-TA-003: Lowering Trend Detection
- Systolic or diastolic decreasing >5 mmHg per month
- Sustained over at least 2 weeks

### BR-TA-004: Volatility Detection
- Standard deviation >15 mmHg systolic or >10 mmHg diastolic
- Coefficient of variation >12%

## Event Publishing

### BloodPressureRisingTrend
```json
{
  "eventType": "BloodPressureRisingTrend",
  "data": {
    "userId": "uuid",
    "trendStartDate": "2025-12-01",
    "rateOfIncrease": {"systolic": 2.5, "diastolic": 1.2},
    "projectedLevels": {"30days": {"systolic": 135, "diastolic": 88}},
    "confidenceLevel": 0.85
  }
}
```

## Database Schema

```sql
CREATE TABLE TrendAnalyses (
    AnalysisId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    AverageSystolic DECIMAL(5,2),
    AverageDiastolic DECIMAL(5,2),
    TrendDirection NVARCHAR(20),
    SystolicRateOfChange DECIMAL(5,2),
    DiastolicRateOfChange DECIMAL(5,2),
    ConfidenceLevel DECIMAL(3,2),
    DataPointsCount INT,
    AnalyzedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```
