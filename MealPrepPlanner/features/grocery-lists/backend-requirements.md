# Grocery Lists - Backend Requirements

## 1. Overview

The Grocery Lists backend module automatically generates shopping lists from meal plans and provides manual list management with purchase tracking and categorization.

## 2. Domain Model

### 2.1 Aggregates

#### GroceryList (Aggregate Root)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `UserId` (Guid): List owner
  - `MealPlanId` (Guid?): Source meal plan (if auto-generated)
  - `Name` (string): List name
  - `GeneratedDate` (DateTime): Creation date
  - `Status` (enum): Active, Completed, Archived
  - `Items` (List<GroceryItem>): Shopping items
  - `TotalEstimatedCost` (decimal?): Cost estimate
  - `PurchaseDate` (DateTime?): Completion date

#### GroceryItem (Entity)
- **Properties**:
  - `Id` (Guid): Unique identifier
  - `GroceryListId` (Guid): Parent list
  - `IngredientId` (Guid?): Linked ingredient
  - `Name` (string): Item name
  - `Quantity` (decimal): Amount needed
  - `Unit` (string): Measurement unit
  - `Category` (string): Store section (Produce, Dairy, etc.)
  - `IsPurchased` (bool): Purchase status
  - `EstimatedPrice` (decimal?): Price estimate
  - `Notes` (string?): Additional notes
  - `Priority` (int): Sort order

### 2.2 Value Objects

#### GroceryListStatus (Enum)
- Active
- Completed
- Archived

#### StoreCategory (Enum)
- Produce
- Meat & Seafood
- Dairy & Eggs
- Bakery
- Pantry
- Frozen
- Beverages
- Other

## 3. Domain Events

### 3.1 GroceryListGenerated
```csharp
public class GroceryListGenerated : DomainEvent
{
    public Guid GroceryListId { get; set; }
    public Guid UserId { get; set; }
    public Guid MealPlanId { get; set; }
    public int ItemCount { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

### 3.2 GroceryItemPurchased
```csharp
public class GroceryItemPurchased : DomainEvent
{
    public Guid GroceryItemId { get; set; }
    public Guid GroceryListId { get; set; }
    public string ItemName { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public DateTime OccurredOn { get; set; }
}
```

## 4. Commands

### 4.1 GenerateGroceryListFromMealPlan
```csharp
public class GenerateGroceryListFromMealPlanCommand : ICommand
{
    public Guid UserId { get; set; }
    public Guid MealPlanId { get; set; }
    public bool ConsolidateItems { get; set; } = true;
    public bool IncludeOptionalIngredients { get; set; } = false;
}
```

### 4.2 AddItemToGroceryList
```csharp
public class AddItemToGroceryListCommand : ICommand
{
    public Guid GroceryListId { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public string Category { get; set; }
}
```

### 4.3 MarkItemPurchased
```csharp
public class MarkItemPurchasedCommand : ICommand
{
    public Guid GroceryListId { get; set; }
    public Guid ItemId { get; set; }
    public bool IsPurchased { get; set; }
    public decimal? ActualPrice { get; set; }
}
```

### 4.4 CompleteGroceryList
```csharp
public class CompleteGroceryListCommand : ICommand
{
    public Guid GroceryListId { get; set; }
    public Guid UserId { get; set; }
}
```

## 5. Queries

### 5.1 GetGroceryListById
```csharp
public class GetGroceryListByIdQuery : IQuery<GroceryListDto>
{
    public Guid GroceryListId { get; set; }
}
```

### 5.2 GetActiveGroceryLists
```csharp
public class GetActiveGroceryListsQuery : IQuery<List<GroceryListDto>>
{
    public Guid UserId { get; set; }
}
```

### 5.3 GetGroceryItemsByCategory
```csharp
public class GetGroceryItemsByCategoryQuery : IQuery<Dictionary<string, List<GroceryItemDto>>>
{
    public Guid GroceryListId { get; set; }
}
```

## 6. API Endpoints

### 6.1 POST /api/grocery-lists/generate
Generate list from meal plan

### 6.2 GET /api/grocery-lists/{id}
Get list details

### 6.3 POST /api/grocery-lists
Create manual list

### 6.4 POST /api/grocery-lists/{id}/items
Add item to list

### 6.5 PUT /api/grocery-lists/{listId}/items/{itemId}/purchased
Mark item as purchased

### 6.6 POST /api/grocery-lists/{id}/complete
Complete shopping list

### 6.7 GET /api/grocery-lists/active
Get active lists

## 7. Business Rules

1. Auto-generated lists consolidate duplicate ingredients
2. Items grouped by store category for efficient shopping
3. Quantities automatically summed when consolidating
4. Optional ingredients excluded by default
5. List auto-completes when all items purchased
6. Can create manual lists independent of meal plans
7. Items can be manually added/removed
8. Purchase history tracked for price estimation

## 8. Database Schema

```sql
CREATE TABLE GroceryLists (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    MealPlanId UNIQUEIDENTIFIER NULL,
    Name NVARCHAR(200) NOT NULL,
    GeneratedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    Status NVARCHAR(50) NOT NULL,
    TotalEstimatedCost DECIMAL(10,2),
    PurchaseDate DATETIME2,
    CONSTRAINT FK_GroceryLists_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_GroceryLists_MealPlans FOREIGN KEY (MealPlanId) REFERENCES MealPlans(Id)
);

CREATE TABLE GroceryItems (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    GroceryListId UNIQUEIDENTIFIER NOT NULL,
    IngredientId UNIQUEIDENTIFIER,
    Name NVARCHAR(200) NOT NULL,
    Quantity DECIMAL(10,2) NOT NULL,
    Unit NVARCHAR(50) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    IsPurchased BIT DEFAULT 0,
    EstimatedPrice DECIMAL(10,2),
    Notes NVARCHAR(500),
    Priority INT DEFAULT 0,
    CONSTRAINT FK_GroceryItems_GroceryLists FOREIGN KEY (GroceryListId) REFERENCES GroceryLists(Id) ON DELETE CASCADE
);
```

## 9. Service Interfaces

```csharp
public interface IGroceryListService
{
    Task<GroceryListDto> GenerateFromMealPlanAsync(GenerateGroceryListFromMealPlanCommand command);
    Task<GroceryItemDto> AddItemAsync(AddItemToGroceryListCommand command);
    Task MarkItemPurchasedAsync(MarkItemPurchasedCommand command);
    Task CompleteListAsync(CompleteGroceryListCommand command);
}

public interface IIngredientConsolidator
{
    List<GroceryItem> ConsolidateIngredients(List<RecipeIngredient> ingredients);
}
```
