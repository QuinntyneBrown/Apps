# Nutrition Tracking - Backend Requirements

## 1. Overview

The Nutrition Tracking module monitors daily nutritional intake, compares against goals, and provides insights into eating patterns and dietary compliance.

## 2. Domain Model

### 2.1 Aggregates

#### NutritionGoal (Aggregate Root)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `UserId` (Guid): Goal owner
  - `Name` (string): Goal name (e.g., "Weight Loss")
  - `DailyCalorieTarget` (int): Target calories
  - `ProteinGramsTarget` (decimal): Daily protein goal
  - `CarbsGramsTarget` (decimal): Daily carbs goal
  - `FatGramsTarget` (decimal): Daily fat goal
  - `FiberGramsTarget` (decimal?): Optional fiber goal
  - `StartDate` (DateTime): Goal start
  - `EndDate` (DateTime?): Optional end date
  - `IsActive` (bool): Active status

#### DailyNutrition (Aggregate Root)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `UserId` (Guid): User reference
  - `Date` (Date): Tracking date
  - `TotalCalories` (int): Consumed calories
  - `TotalProtein` (decimal): Consumed protein (g)
  - `TotalCarbs` (decimal): Consumed carbs (g)
  - `TotalFat` (decimal): Consumed fat (g)
  - `TotalFiber` (decimal): Consumed fiber (g)
  - `MealEntries` (List<MealEntry>): Meals tracked
  - `GoalId` (Guid?): Associated goal

#### MealEntry (Entity)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `DailyNutritionId` (Guid): Parent record
  - `RecipeId` (Guid?): Recipe reference
  - `MealType` (enum): Breakfast, Lunch, Dinner, Snack
  - `Servings` (decimal): Amount consumed
  - `Calories` (int): Calories from meal
  - `Protein` (decimal): Protein from meal
  - `Carbs` (decimal): Carbs from meal
  - `Fat` (decimal): Fat from meal
  - `Timestamp` (DateTime): When consumed

## 3. Domain Events

Events are triggered when nutrition data changes or goals are reached.

## 4. Commands

### 4.1 CreateNutritionGoal
```csharp
public class CreateNutritionGoalCommand : ICommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public int DailyCalorieTarget { get; set; }
    public decimal ProteinTarget { get; set; }
    public decimal CarbsTarget { get; set; }
    public decimal FatTarget { get; set; }
}
```

### 4.2 LogMeal
```csharp
public class LogMealCommand : ICommand
{
    public Guid UserId { get; set; }
    public Date Date { get; set; }
    public Guid? RecipeId { get; set; }
    public MealType MealType { get; set; }
    public decimal Servings { get; set; }
}
```

## 5. Queries

### 5.1 GetDailyNutrition
```csharp
public class GetDailyNutritionQuery : IQuery<DailyNutritionDto>
{
    public Guid UserId { get; set; }
    public Date Date { get; set; }
}
```

### 5.2 GetNutritionTrends
```csharp
public class GetNutritionTrendsQuery : IQuery<List<DailyNutritionDto>>
{
    public Guid UserId { get; set; }
    public Date StartDate { get; set; }
    public Date EndDate { get; set; }
}
```

### 5.3 CompareToGoals
```csharp
public class CompareToGoalsQuery : IQuery<GoalComparisonDto>
{
    public Guid UserId { get; set; }
    public Date Date { get; set; }
}
```

## 6. API Endpoints

- POST /api/nutrition/goals - Create nutrition goal
- GET /api/nutrition/goals/active - Get active goal
- POST /api/nutrition/log - Log meal
- GET /api/nutrition/daily/{date} - Get daily nutrition
- GET /api/nutrition/trends - Get nutrition trends
- GET /api/nutrition/comparison - Compare to goals

## 7. Business Rules

1. Only one active goal per user
2. Daily nutrition auto-calculated from logged meals
3. Historical data cannot be deleted
4. Nutrition values cached for performance
5. Goals can be updated but history preserved

## 8. Database Schema

```sql
CREATE TABLE NutritionGoals (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200),
    DailyCalorieTarget INT,
    ProteinTarget DECIMAL(10,2),
    CarbsTarget DECIMAL(10,2),
    FatTarget DECIMAL(10,2),
    IsActive BIT DEFAULT 1,
    StartDate DATETIME2,
    EndDate DATETIME2
);

CREATE TABLE DailyNutrition (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Date DATE NOT NULL,
    TotalCalories INT,
    TotalProtein DECIMAL(10,2),
    TotalCarbs DECIMAL(10,2),
    TotalFat DECIMAL(10,2),
    GoalId UNIQUEIDENTIFIER,
    UNIQUE(UserId, Date)
);

CREATE TABLE MealEntries (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    DailyNutritionId UNIQUEIDENTIFIER NOT NULL,
    RecipeId UNIQUEIDENTIFIER,
    MealType NVARCHAR(50),
    Servings DECIMAL(10,2),
    Calories INT,
    Protein DECIMAL(10,2),
    Carbs DECIMAL(10,2),
    Fat DECIMAL(10,2),
    Timestamp DATETIME2
);
```
