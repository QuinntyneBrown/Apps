# Meal Planning - Backend Requirements

## 1. Overview

The Meal Planning backend module provides APIs and services for creating, managing, and tracking meal plans. It implements domain-driven design with CQRS pattern and domain events.

## 2. Domain Model

### 2.1 Aggregates

#### MealPlan (Aggregate Root)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `UserId` (Guid): Owner of the meal plan
  - `Name` (string): Descriptive name
  - `StartDate` (DateTime): Plan start date
  - `EndDate` (DateTime): Plan end date
  - `Status` (enum): Draft, Active, Completed, Archived
  - `CreatedDate` (DateTime): Creation timestamp
  - `LastModifiedDate` (DateTime): Last update timestamp
  - `Meals` (List<Meal>): Collection of planned meals

#### Meal (Entity)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `MealPlanId` (Guid): Parent meal plan reference
  - `RecipeId` (Guid?): Optional recipe assignment
  - `MealType` (enum): Breakfast, Lunch, Dinner, Snack
  - `ScheduledDate` (DateTime): When the meal is planned
  - `Servings` (int): Number of servings
  - `Notes` (string): Optional notes
  - `IsCompleted` (bool): Completion status

### 2.2 Value Objects

#### DateRange
- `StartDate` (DateTime)
- `EndDate` (DateTime)
- Validation: EndDate must be after StartDate

#### MealType (Enum)
- Breakfast
- Lunch
- Dinner
- Snack
- Dessert

#### MealPlanStatus (Enum)
- Draft
- Active
- Completed
- Archived

## 3. Domain Events

### 3.1 MealPlanCreated
```csharp
public class MealPlanCreated : DomainEvent
{
    public Guid MealPlanId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

### 3.2 MealPlanActivated
```csharp
public class MealPlanActivated : DomainEvent
{
    public Guid MealPlanId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ActivatedDate { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

### 3.3 MealPlanCompleted
```csharp
public class MealPlanCompleted : DomainEvent
{
    public Guid MealPlanId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CompletedDate { get; set; }
    public int TotalMeals { get; set; }
    public int CompletedMeals { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

### 3.4 MealAssigned
```csharp
public class MealAssigned : DomainEvent
{
    public Guid MealId { get; set; }
    public Guid MealPlanId { get; set; }
    public Guid RecipeId { get; set; }
    public MealType MealType { get; set; }
    public DateTime ScheduledDate { get; set; }
    public int Servings { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

## 4. Commands

### 4.1 CreateMealPlan
```csharp
public class CreateMealPlanCommand : ICommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
```

**Validation**:
- Name is required (max 200 characters)
- StartDate must be valid date
- EndDate must be after StartDate
- UserId must exist

### 4.2 ActivateMealPlan
```csharp
public class ActivateMealPlanCommand : ICommand
{
    public Guid MealPlanId { get; set; }
    public Guid UserId { get; set; }
}
```

**Business Rules**:
- Only one active meal plan per user
- Meal plan must be in Draft status
- Must have at least one meal assigned

### 4.3 AssignMealToSlot
```csharp
public class AssignMealToSlotCommand : ICommand
{
    public Guid MealPlanId { get; set; }
    public Guid RecipeId { get; set; }
    public MealType MealType { get; set; }
    public DateTime ScheduledDate { get; set; }
    public int Servings { get; set; }
    public string Notes { get; set; }
}
```

**Validation**:
- MealPlanId must exist
- RecipeId must exist
- ScheduledDate must be within meal plan date range
- Servings must be > 0

### 4.4 CompleteMealPlan
```csharp
public class CompleteMealPlanCommand : ICommand
{
    public Guid MealPlanId { get; set; }
    public Guid UserId { get; set; }
}
```

**Business Rules**:
- Meal plan must be Active
- EndDate must be reached or passed

### 4.5 RemoveMealFromPlan
```csharp
public class RemoveMealFromPlanCommand : ICommand
{
    public Guid MealPlanId { get; set; }
    public Guid MealId { get; set; }
}
```

### 4.6 UpdateMealPlan
```csharp
public class UpdateMealPlanCommand : ICommand
{
    public Guid MealPlanId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
```

## 5. Queries

### 5.1 GetMealPlanById
```csharp
public class GetMealPlanByIdQuery : IQuery<MealPlanDto>
{
    public Guid MealPlanId { get; set; }
    public Guid UserId { get; set; }
}
```

**Returns**: Detailed meal plan with all meals

### 5.2 GetActiveMealPlan
```csharp
public class GetActiveMealPlanQuery : IQuery<MealPlanDto>
{
    public Guid UserId { get; set; }
}
```

**Returns**: Currently active meal plan for user

### 5.3 GetMealPlansForUser
```csharp
public class GetMealPlansForUserQuery : IQuery<List<MealPlanSummaryDto>>
{
    public Guid UserId { get; set; }
    public MealPlanStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
```

**Returns**: Paginated list of meal plan summaries

### 5.4 GetMealsForDateRange
```csharp
public class GetMealsForDateRangeQuery : IQuery<List<MealDto>>
{
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
```

**Returns**: All scheduled meals in date range

## 6. DTOs

### 6.1 MealPlanDto
```csharp
public class MealPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public List<MealDto> Meals { get; set; }
    public int TotalMeals { get; set; }
    public int CompletedMeals { get; set; }
}
```

### 6.2 MealDto
```csharp
public class MealDto
{
    public Guid Id { get; set; }
    public Guid? RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string MealType { get; set; }
    public DateTime ScheduledDate { get; set; }
    public int Servings { get; set; }
    public string Notes { get; set; }
    public bool IsCompleted { get; set; }
}
```

## 7. API Endpoints

### 7.1 POST /api/meal-plans
Create new meal plan
- Request: CreateMealPlanCommand
- Response: 201 Created with MealPlanDto
- Auth: Required

### 7.2 GET /api/meal-plans/{id}
Get meal plan details
- Response: 200 OK with MealPlanDto
- Auth: Required

### 7.3 GET /api/meal-plans
List meal plans for user
- Query params: status, fromDate, toDate, page, pageSize
- Response: 200 OK with paginated list
- Auth: Required

### 7.4 GET /api/meal-plans/active
Get active meal plan
- Response: 200 OK with MealPlanDto or 404
- Auth: Required

### 7.5 PUT /api/meal-plans/{id}
Update meal plan
- Request: UpdateMealPlanCommand
- Response: 200 OK with MealPlanDto
- Auth: Required

### 7.6 POST /api/meal-plans/{id}/activate
Activate meal plan
- Response: 200 OK
- Auth: Required

### 7.7 POST /api/meal-plans/{id}/complete
Complete meal plan
- Response: 200 OK
- Auth: Required

### 7.8 POST /api/meal-plans/{id}/meals
Add meal to plan
- Request: AssignMealToSlotCommand
- Response: 201 Created with MealDto
- Auth: Required

### 7.9 DELETE /api/meal-plans/{planId}/meals/{mealId}
Remove meal from plan
- Response: 204 No Content
- Auth: Required

### 7.10 GET /api/meals
Get meals for date range
- Query params: startDate, endDate
- Response: 200 OK with list of MealDto
- Auth: Required

## 8. Database Schema

### 8.1 MealPlans Table
```sql
CREATE TABLE MealPlans (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    LastModifiedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT FK_MealPlans_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_MealPlans_UserId ON MealPlans(UserId);
CREATE INDEX IX_MealPlans_Status ON MealPlans(Status);
CREATE INDEX IX_MealPlans_StartDate ON MealPlans(StartDate);
```

### 8.2 Meals Table
```sql
CREATE TABLE Meals (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MealPlanId UNIQUEIDENTIFIER NOT NULL,
    RecipeId UNIQUEIDENTIFIER NULL,
    MealType NVARCHAR(50) NOT NULL,
    ScheduledDate DATETIME2 NOT NULL,
    Servings INT NOT NULL DEFAULT 1,
    Notes NVARCHAR(500) NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Meals_MealPlans FOREIGN KEY (MealPlanId) REFERENCES MealPlans(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Meals_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes(Id)
);

CREATE INDEX IX_Meals_MealPlanId ON Meals(MealPlanId);
CREATE INDEX IX_Meals_ScheduledDate ON Meals(ScheduledDate);
CREATE INDEX IX_Meals_RecipeId ON Meals(RecipeId);
```

## 9. Business Rules

### 9.1 Meal Plan Rules
1. User can have only one active meal plan at a time
2. Meal plan must have valid date range (EndDate > StartDate)
3. Cannot activate empty meal plan (must have at least one meal)
4. Cannot modify completed meal plans
5. Archived meal plans are read-only

### 9.2 Meal Assignment Rules
1. Scheduled date must be within meal plan date range
2. Cannot assign non-existent recipe
3. Servings must be positive integer
4. Can have multiple meals on same date with different meal types

## 10. Event Handlers

### 10.1 MealPlanCreatedHandler
- Send notification to user
- Initialize default meal slots (optional)
- Log analytics event

### 10.2 MealPlanActivatedHandler
- Deactivate other active plans for user
- Generate grocery list (trigger GroceryListGenerated event)
- Send activation notification

### 10.3 MealAssignedHandler
- Validate recipe availability
- Update meal plan statistics
- Trigger nutrition calculation

### 10.4 MealPlanCompletedHandler
- Archive completed plan
- Generate summary report
- Update user statistics

## 11. Repository Interfaces

### 11.1 IMealPlanRepository
```csharp
public interface IMealPlanRepository
{
    Task<MealPlan> GetByIdAsync(Guid id);
    Task<MealPlan> GetActiveByUserIdAsync(Guid userId);
    Task<IEnumerable<MealPlan>> GetByUserIdAsync(Guid userId, MealPlanStatus? status);
    Task AddAsync(MealPlan mealPlan);
    Task UpdateAsync(MealPlan mealPlan);
    Task DeleteAsync(Guid id);
}
```

## 12. Service Interfaces

### 12.1 IMealPlanService
```csharp
public interface IMealPlanService
{
    Task<MealPlanDto> CreateMealPlanAsync(CreateMealPlanCommand command);
    Task ActivateMealPlanAsync(ActivateMealPlanCommand command);
    Task CompleteMealPlanAsync(CompleteMealPlanCommand command);
    Task<MealDto> AssignMealAsync(AssignMealToSlotCommand command);
    Task RemoveMealAsync(Guid mealPlanId, Guid mealId);
}
```

## 13. Validation Rules

### 13.1 CreateMealPlan Validation
- Name: Required, max 200 characters
- StartDate: Required, cannot be in past
- EndDate: Required, must be after StartDate
- UserId: Required, must exist

### 13.2 AssignMeal Validation
- MealPlanId: Required, must exist
- RecipeId: Required, must exist
- ScheduledDate: Required, within plan date range
- Servings: Required, min 1, max 100
- MealType: Required, valid enum value

## 14. Error Handling

### 14.1 Error Codes
- `MEALPLAN_001`: Meal plan not found
- `MEALPLAN_002`: User already has active meal plan
- `MEALPLAN_003`: Invalid date range
- `MEALPLAN_004`: Cannot activate empty meal plan
- `MEALPLAN_005`: Cannot modify completed meal plan
- `MEAL_001`: Meal not found
- `MEAL_002`: Scheduled date outside plan range
- `MEAL_003`: Invalid meal type

## 15. Performance Considerations

### 15.1 Caching Strategy
- Cache active meal plan per user (15 min TTL)
- Cache meal plan details (5 min TTL)
- Invalidate on updates

### 15.2 Database Optimization
- Index on UserId, Status, StartDate
- Composite index on ScheduledDate + MealType
- Eager loading for Meals collection

## 16. Testing Requirements

### 16.1 Unit Tests
- Domain model behavior
- Command validation
- Business rule enforcement
- Event generation

### 16.2 Integration Tests
- API endpoint testing
- Database operations
- Event handler execution
- Repository operations

### 16.3 Test Scenarios
1. Create meal plan successfully
2. Activate meal plan with valid meals
3. Prevent multiple active plans
4. Assign meals to plan
5. Complete meal plan
6. Remove meal from plan
7. Query meal plans by status
8. Handle concurrent updates
