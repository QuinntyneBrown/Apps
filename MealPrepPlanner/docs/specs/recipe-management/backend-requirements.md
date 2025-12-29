# Recipe Management - Backend Requirements

## 1. Overview

The Recipe Management backend module provides comprehensive APIs and services for creating, storing, organizing, and managing recipes with nutritional calculations and preparation tracking.

## 2. Domain Model

### 2.1 Aggregates

#### Recipe (Aggregate Root)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `UserId` (Guid): Recipe owner
  - `Name` (string): Recipe name (max 200 chars)
  - `Description` (string): Brief description
  - `CategoryId` (Guid?): Recipe category
  - `CuisineType` (string): e.g., Italian, Mexican
  - `PrepTime` (int): Preparation time in minutes
  - `CookTime` (int): Cooking time in minutes
  - `TotalTime` (int): Total time (calculated)
  - `Servings` (int): Number of servings
  - `Difficulty` (enum): Easy, Medium, Hard
  - `Instructions` (List<Instruction>): Step-by-step instructions
  - `Ingredients` (List<RecipeIngredient>): Required ingredients
  - `NutritionInfo` (NutritionInfo): Calculated nutrition
  - `IsFavorite` (bool): Favorited by user
  - `Rating` (decimal?): User rating (1-5)
  - `PrepCount` (int): Times prepared
  - `LastPreparedDate` (DateTime?): Last preparation
  - `ImageUrl` (string): Recipe photo URL
  - `Tags` (List<string>): Searchable tags
  - `CreatedDate` (DateTime): Creation timestamp
  - `LastModifiedDate` (DateTime): Last update

#### Instruction (Entity)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `RecipeId` (Guid): Parent recipe
  - `StepNumber` (int): Order sequence
  - `Description` (string): Step instructions
  - `ImageUrl` (string?): Optional step image

#### RecipeIngredient (Entity)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `RecipeId` (Guid): Parent recipe
  - `IngredientId` (Guid): Reference to ingredient
  - `Quantity` (decimal): Amount needed
  - `Unit` (string): Measurement unit
  - `Notes` (string?): e.g., "chopped", "diced"
  - `IsOptional` (bool): Optional ingredient

### 2.2 Value Objects

#### NutritionInfo
- `CaloriesPerServing` (int)
- `ProteinGrams` (decimal)
- `CarbsGrams` (decimal)
- `FatGrams` (decimal)
- `FiberGrams` (decimal)
- `SugarGrams` (decimal)
- `SodiumMg` (decimal)

#### Difficulty (Enum)
- Easy
- Medium
- Hard

## 3. Domain Events

### 3.1 RecipeAdded
```csharp
public class RecipeAdded : DomainEvent
{
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public int Servings { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

### 3.2 RecipeNutritionCalculated
```csharp
public class RecipeNutritionCalculated : DomainEvent
{
    public Guid RecipeId { get; set; }
    public int CaloriesPerServing { get; set; }
    public decimal ProteinGrams { get; set; }
    public decimal CarbsGrams { get; set; }
    public decimal FatGrams { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

### 3.3 RecipeFavorited
```csharp
public class RecipeFavorited : DomainEvent
{
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

### 3.4 RecipePrepared
```csharp
public class RecipePrepared : DomainEvent
{
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
    public DateTime PreparedDate { get; set; }
    public int Servings { get; set; }
    public decimal? UserRating { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

## 4. Commands

### 4.1 AddRecipe
```csharp
public class AddRecipeCommand : ICommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? CategoryId { get; set; }
    public string CuisineType { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int Servings { get; set; }
    public string Difficulty { get; set; }
    public List<InstructionDto> Instructions { get; set; }
    public List<RecipeIngredientDto> Ingredients { get; set; }
    public List<string> Tags { get; set; }
}
```

### 4.2 UpdateRecipe
```csharp
public class UpdateRecipeCommand : ICommand
{
    public Guid RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? CategoryId { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int Servings { get; set; }
}
```

### 4.3 ToggleFavoriteRecipe
```csharp
public class ToggleFavoriteRecipeCommand : ICommand
{
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
}
```

### 4.4 MarkRecipePrepared
```csharp
public class MarkRecipePreparedCommand : ICommand
{
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
    public DateTime PreparedDate { get; set; }
    public int Servings { get; set; }
    public decimal? Rating { get; set; }
}
```

### 4.5 CalculateRecipeNutrition
```csharp
public class CalculateRecipeNutritionCommand : ICommand
{
    public Guid RecipeId { get; set; }
}
```

## 5. Queries

### 5.1 GetRecipeById
```csharp
public class GetRecipeByIdQuery : IQuery<RecipeDto>
{
    public Guid RecipeId { get; set; }
    public Guid UserId { get; set; }
}
```

### 5.2 SearchRecipes
```csharp
public class SearchRecipesQuery : IQuery<PagedResult<RecipeSummaryDto>>
{
    public Guid UserId { get; set; }
    public string SearchTerm { get; set; }
    public Guid? CategoryId { get; set; }
    public string CuisineType { get; set; }
    public bool? FavoritesOnly { get; set; }
    public List<string> Tags { get; set; }
    public string Difficulty { get; set; }
    public int? MaxPrepTime { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
```

### 5.3 GetFavoriteRecipes
```csharp
public class GetFavoriteRecipesQuery : IQuery<List<RecipeSummaryDto>>
{
    public Guid UserId { get; set; }
}
```

### 5.4 GetRecentlyPrepared
```csharp
public class GetRecentlyPreparedQuery : IQuery<List<RecipeSummaryDto>>
{
    public Guid UserId { get; set; }
    public int Count { get; set; } = 10;
}
```

## 6. API Endpoints

### 6.1 POST /api/recipes
Create new recipe

### 6.2 GET /api/recipes/{id}
Get recipe details

### 6.3 GET /api/recipes
Search and list recipes

### 6.4 PUT /api/recipes/{id}
Update recipe

### 6.5 DELETE /api/recipes/{id}
Delete recipe

### 6.6 POST /api/recipes/{id}/favorite
Toggle favorite status

### 6.7 POST /api/recipes/{id}/prepare
Mark recipe as prepared

### 6.8 POST /api/recipes/{id}/calculate-nutrition
Calculate nutrition information

### 6.9 GET /api/recipes/favorites
Get favorite recipes

### 6.10 GET /api/recipes/recent
Get recently prepared recipes

## 7. Database Schema

### 7.1 Recipes Table
```sql
CREATE TABLE Recipes (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000),
    CategoryId UNIQUEIDENTIFIER,
    CuisineType NVARCHAR(50),
    PrepTime INT NOT NULL,
    CookTime INT NOT NULL,
    TotalTime INT NOT NULL,
    Servings INT NOT NULL,
    Difficulty NVARCHAR(20),
    IsFavorite BIT DEFAULT 0,
    Rating DECIMAL(3,2),
    PrepCount INT DEFAULT 0,
    LastPreparedDate DATETIME2,
    ImageUrl NVARCHAR(500),
    CaloriesPerServing INT,
    ProteinGrams DECIMAL(10,2),
    CarbsGrams DECIMAL(10,2),
    FatGrams DECIMAL(10,2),
    FiberGrams DECIMAL(10,2),
    SugarGrams DECIMAL(10,2),
    SodiumMg DECIMAL(10,2),
    CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
    LastModifiedDate DATETIME2 DEFAULT GETUTCDATE(),
    CONSTRAINT FK_Recipes_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE INDEX IX_Recipes_UserId ON Recipes(UserId);
CREATE INDEX IX_Recipes_Name ON Recipes(Name);
CREATE INDEX IX_Recipes_IsFavorite ON Recipes(IsFavorite);
CREATE FULLTEXT INDEX ON Recipes(Name, Description);
```

### 7.2 Instructions Table
```sql
CREATE TABLE Instructions (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RecipeId UNIQUEIDENTIFIER NOT NULL,
    StepNumber INT NOT NULL,
    Description NVARCHAR(2000) NOT NULL,
    ImageUrl NVARCHAR(500),
    CONSTRAINT FK_Instructions_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes(Id) ON DELETE CASCADE
);

CREATE INDEX IX_Instructions_RecipeId ON Instructions(RecipeId);
```

### 7.3 RecipeIngredients Table
```sql
CREATE TABLE RecipeIngredients (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RecipeId UNIQUEIDENTIFIER NOT NULL,
    IngredientId UNIQUEIDENTIFIER NOT NULL,
    Quantity DECIMAL(10,2) NOT NULL,
    Unit NVARCHAR(50) NOT NULL,
    Notes NVARCHAR(200),
    IsOptional BIT DEFAULT 0,
    CONSTRAINT FK_RecipeIngredients_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes(Id) ON DELETE CASCADE,
    CONSTRAINT FK_RecipeIngredients_Ingredients FOREIGN KEY (IngredientId) REFERENCES Ingredients(Id)
);

CREATE INDEX IX_RecipeIngredients_RecipeId ON RecipeIngredients(RecipeId);
```

### 7.4 RecipeTags Table
```sql
CREATE TABLE RecipeTags (
    RecipeId UNIQUEIDENTIFIER NOT NULL,
    Tag NVARCHAR(50) NOT NULL,
    PRIMARY KEY (RecipeId, Tag),
    CONSTRAINT FK_RecipeTags_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes(Id) ON DELETE CASCADE
);

CREATE INDEX IX_RecipeTags_Tag ON RecipeTags(Tag);
```

## 8. Business Rules

1. Recipe name must be unique per user
2. Must have at least one ingredient
3. Must have at least one instruction
4. Servings must be positive integer
5. Total time = PrepTime + CookTime
6. Nutrition automatically recalculated when ingredients change
7. PrepCount increments when marked as prepared
8. Cannot delete recipe if used in active meal plan

## 9. Validation Rules

- Name: Required, 3-200 characters
- Servings: Min 1, Max 100
- PrepTime: Min 0, Max 1440 (24 hours)
- CookTime: Min 0, Max 1440
- Rating: 0-5 with 0.5 increments
- Ingredients: At least 1 required
- Instructions: At least 1 required

## 10. Service Interfaces

```csharp
public interface IRecipeService
{
    Task<RecipeDto> AddRecipeAsync(AddRecipeCommand command);
    Task<RecipeDto> UpdateRecipeAsync(UpdateRecipeCommand command);
    Task DeleteRecipeAsync(Guid recipeId, Guid userId);
    Task ToggleFavoriteAsync(Guid recipeId, Guid userId);
    Task MarkPreparedAsync(MarkRecipePreparedCommand command);
    Task<NutritionInfo> CalculateNutritionAsync(Guid recipeId);
}

public interface INutritionCalculator
{
    Task<NutritionInfo> CalculateAsync(List<RecipeIngredient> ingredients, int servings);
}
```

## 11. Integration Points

- Nutritional database API (USDA FoodData Central)
- Image storage service
- Search indexing service
- Recipe import services (web scraping)
