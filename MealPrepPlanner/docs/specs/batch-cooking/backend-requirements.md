# Batch Cooking - Backend Requirements

## 1. Overview

The Batch Cooking module helps users plan and execute efficient bulk cooking sessions, optimizing recipes for multiple servings and managing storage schedules.

## 2. Domain Model

### 2.1 Aggregates

#### BatchCookingSession (Aggregate Root)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `UserId` (Guid): Session owner
  - `Name` (string): Session name (e.g., "Sunday Meal Prep")
  - `ScheduledDate` (DateTime): When to cook
  - `Status` (enum): Planned, InProgress, Completed
  - `Recipes` (List<BatchRecipe>): Recipes to prepare
  - `TotalPrepTime` (int): Estimated total time
  - `TotalServings` (int): Total servings produced
  - `Notes` (string): Session notes
  - `CreatedDate` (DateTime): Creation timestamp

#### BatchRecipe (Entity)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `SessionId` (Guid): Parent session
  - `RecipeId` (Guid): Recipe reference
  - `Multiplier` (int): How many batches (e.g., 3x recipe)
  - `TotalServings` (int): Servings to produce
  - `StorageMethod` (string): Freeze, Refrigerate
  - `ContainerCount` (int): Number of containers
  - `ExpiryDate` (DateTime?): When it expires
  - `IsCompleted` (bool): Cooking completed

#### BatchIngredient (Value Object)
- Consolidated ingredients across all recipes
- Quantity adjustments for batch sizes
- Shopping list generation

### 2.2 Enums

**SessionStatus**: Planned, InProgress, Completed, Cancelled

**StorageMethod**: Refrigerate, Freeze, Pantry

## 3. Commands

### 3.1 CreateBatchCookingSession
```csharp
public class CreateBatchCookingSessionCommand : ICommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public DateTime ScheduledDate { get; set; }
    public List<BatchRecipeDto> Recipes { get; set; }
}
```

### 3.2 AddRecipeToSession
```csharp
public class AddRecipeToSessionCommand : ICommand
{
    public Guid SessionId { get; set; }
    public Guid RecipeId { get; set; }
    public int Multiplier { get; set; }
    public string StorageMethod { get; set; }
}
```

### 3.3 StartBatchCookingSession
```csharp
public class StartBatchCookingSessionCommand : ICommand
{
    public Guid SessionId { get; set; }
}
```

### 3.4 CompleteSessionRecipe
```csharp
public class CompleteSessionRecipeCommand : ICommand
{
    public Guid SessionId { get; set; }
    public Guid BatchRecipeId { get; set; }
    public int ActualServings { get; set; }
}
```

## 4. Queries

### 4.1 GetBatchCookingSessions
```csharp
public class GetBatchCookingSessionsQuery : IQuery<List<BatchSessionDto>>
{
    public Guid UserId { get; set; }
    public SessionStatus? Status { get; set; }
}
```

### 4.2 GetSessionIngredients
```csharp
public class GetSessionIngredientsQuery : IQuery<List<BatchIngredientDto>>
{
    public Guid SessionId { get; set; }
}
```

### 4.3 GetUpcomingSessions
```csharp
public class GetUpcomingSessionsQuery : IQuery<List<BatchSessionDto>>
{
    public Guid UserId { get; set; }
    public DateTime FromDate { get; set; }
}
```

## 5. API Endpoints

- POST /api/batch-cooking/sessions - Create session
- GET /api/batch-cooking/sessions - List sessions
- GET /api/batch-cooking/sessions/{id} - Get session details
- POST /api/batch-cooking/sessions/{id}/start - Start session
- POST /api/batch-cooking/sessions/{id}/complete - Complete session
- GET /api/batch-cooking/sessions/{id}/ingredients - Get consolidated ingredients

## 6. Business Rules

1. Recipes automatically scaled based on multiplier
2. Ingredients consolidated across all recipes in session
3. Storage recommendations based on food type
4. Expiry dates calculated from storage method
5. Prep time estimated based on parallel cooking
6. Container recommendations based on servings

## 7. Database Schema

```sql
CREATE TABLE BatchCookingSessions (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200),
    ScheduledDate DATETIME2,
    Status NVARCHAR(50),
    TotalPrepTime INT,
    TotalServings INT,
    Notes NVARCHAR(1000),
    CreatedDate DATETIME2 DEFAULT GETUTCDATE()
);

CREATE TABLE BatchRecipes (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    SessionId UNIQUEIDENTIFIER NOT NULL,
    RecipeId UNIQUEIDENTIFIER NOT NULL,
    Multiplier INT DEFAULT 1,
    TotalServings INT,
    StorageMethod NVARCHAR(50),
    ContainerCount INT,
    ExpiryDate DATETIME2,
    IsCompleted BIT DEFAULT 0,
    CONSTRAINT FK_BatchRecipes_Sessions FOREIGN KEY (SessionId) REFERENCES BatchCookingSessions(Id) ON DELETE CASCADE
);
```

## 8. Service Interfaces

```csharp
public interface IBatchCookingService
{
    Task<BatchSessionDto> CreateSessionAsync(CreateBatchCookingSessionCommand command);
    Task<List<BatchIngredientDto>> GetConsolidatedIngredientsAsync(Guid sessionId);
    Task StartSessionAsync(Guid sessionId);
    Task CompleteSessionAsync(Guid sessionId);
}

public interface IIngredientConsolidator
{
    List<BatchIngredient> ConsolidateForSession(List<BatchRecipe> recipes);
}
```

## 9. Features

- Smart cooking order optimization (prep overlapping recipes together)
- Equipment usage tracking (oven, stovetop)
- Storage container planning
- Reheating instructions for each batch
- Cost per serving calculation
- Freezer inventory tracking
