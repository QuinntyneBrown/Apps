# Progress Tracking - Backend Requirements

## Overview
Track body measurements, strength progression, and fitness metrics over time with charts and analytics.

## Domain Model

### ProgressMeasurement (Aggregate Root)
```csharp
public class ProgressMeasurement : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime MeasurementDate { get; private set; }
    public decimal? Weight { get; private set; }
    public decimal? BodyFatPercentage { get; private set; }
    public Dictionary<string, decimal> Measurements { get; private set; }
    public string Notes { get; private set; }
    public string PhotoUrl { get; private set; }

    public static ProgressMeasurement Create(Guid userId, DateTime date, decimal? weight,
        decimal? bodyFat, Dictionary<string, decimal> measurements = null)
    {
        return new ProgressMeasurement
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MeasurementDate = date,
            Weight = weight,
            BodyFatPercentage = bodyFat,
            Measurements = measurements ?? new Dictionary<string, decimal>(),
            CreatedDate = DateTime.UtcNow
        };
    }
}
```

### FitnessGoal (Entity)
```csharp
public class FitnessGoal : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public GoalType Type { get; private set; }
    public string Description { get; private set; }
    public decimal TargetValue { get; private set; }
    public decimal? CurrentValue { get; private set; }
    public DateTime TargetDate { get; private set; }
    public GoalStatus Status { get; private set; }
}

public enum GoalType { WeightLoss, WeightGain, StrengthGoal, BodyFatReduction, MuscleGain }
public enum GoalStatus { Active, Achieved, Missed, Abandoned }
```

## Commands
- RecordProgressMeasurementCommand
- UpdateProgressMeasurementCommand
- CreateFitnessGoalCommand
- UpdateGoalProgressCommand

## Queries
- GetProgressHistoryQuery
- GetProgressChartsQuery
- GetUserGoalsQuery
- GetStatisticsQuery

## Database Schema
```sql
CREATE TABLE ProgressMeasurements (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    MeasurementDate DATETIME2 NOT NULL,
    Weight DECIMAL(6,2),
    BodyFatPercentage DECIMAL(5,2),
    Measurements NVARCHAR(MAX),
    Notes NVARCHAR(2000),
    PhotoUrl NVARCHAR(500),
    CreatedDate DATETIME2 NOT NULL,
    INDEX IX_ProgressMeasurements_UserId (UserId),
    INDEX IX_ProgressMeasurements_MeasurementDate (MeasurementDate)
);

CREATE TABLE FitnessGoals (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Type INT NOT NULL,
    Description NVARCHAR(500),
    TargetValue DECIMAL(10,2),
    CurrentValue DECIMAL(10,2),
    TargetDate DATETIME2,
    Status INT NOT NULL,
    CreatedDate DATETIME2 NOT NULL
);
```

## Business Rules
1. Measurements can be recorded at most once per day
2. Weight must be between 50-500 lbs/kg
3. Body fat % must be between 3-60%
4. Goals must have future target dates
5. Achieved goals are archived
