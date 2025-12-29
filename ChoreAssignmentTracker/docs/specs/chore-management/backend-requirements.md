# Chore Management - Backend Requirements

## Overview
Manages household chores with creation, modification, assignment, and lifecycle tracking.

## Domain Model

### Chore Aggregate
- **ChoreId**: Guid
- **HouseholdId**: Guid
- **ChoreName**: string
- **Description**: string
- **Category**: ChoreCategory (Cleaning, Cooking, Outdoor, Maintenance, Pet Care, Other)
- **EstimatedDuration**: int (minutes)
- **DifficultyLevel**: DifficultyLevel (Easy, Medium, Hard)
- **PointValue**: int
- **Frequency**: Frequency (OneTime, Daily, Weekly, BiWeekly, Monthly)
- **IsActive**: bool
- **Instructions**: string
- **ImageUrl**: string (optional)
- **CreatedBy**: Guid
- **CreatedAt**: DateTime
- **UpdatedAt**: DateTime

### ChoreCategory Enum
- Cleaning, Cooking, Outdoor, Maintenance, PetCare, Other

### DifficultyLevel Enum
- Easy (1-5 points), Medium (6-10 points), Hard (11-20 points)

### Frequency Enum
- OneTime, Daily, Weekly, BiWeekly, Monthly

## Commands

### CreateChoreCommand
- Validates required fields
- Calculates point value based on difficulty and duration
- Creates Chore aggregate
- Raises **ChoreCreated** event
- Returns ChoreId

### ModifyChoreCommand
- Validates chore exists
- Updates chore properties
- Recalculates points if difficulty/duration changed
- Raises **ChoreModified** event
- Updates active assignments

### DeleteChoreCommand
- Validates chore exists
- Checks for active assignments
- Soft deletes chore
- Raises **ChoreDeleted** event
- Archives historical data

### AssignChoreCommand
- Validates chore and household member exist
- Creates assignment
- Sets due date based on frequency
- Raises **ChoreAssigned** event
- Notifies assignee

## Queries

### GetChoreByIdQuery
- Returns chore details
- Includes point value and category

### GetChoresByHouseholdQuery
- Returns all chores for household
- Supports filtering by category, difficulty, active status
- Includes assignment statistics

### GetChoresByCategoryQuery
- Groups chores by category
- Returns counts and point totals

## Domain Events

```csharp
public class ChoreCreated : DomainEvent
{
    public Guid ChoreId { get; set; }
    public string ChoreName { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public int EstimatedDuration { get; set; }
    public string DifficultyLevel { get; set; }
    public int PointValue { get; set; }
    public string Frequency { get; set; }
    public Guid CreatedBy { get; set; }
}

public class ChoreModified : DomainEvent
{
    public Guid ChoreId { get; set; }
    public List<string> ModifiedFields { get; set; }
    public Dictionary<string, object> PreviousValues { get; set; }
    public Dictionary<string, object> NewValues { get; set; }
    public Guid ModifiedBy { get; set; }
}

public class ChoreDeleted : DomainEvent
{
    public Guid ChoreId { get; set; }
    public string DeletionReason { get; set; }
    public int HistoricalCompletions { get; set; }
}

public class ChoreAssigned : DomainEvent
{
    public Guid AssignmentId { get; set; }
    public Guid ChoreId { get; set; }
    public Guid AssignedTo { get; set; }
    public Guid AssignedBy { get; set; }
    public DateTime AssignmentDate { get; set; }
    public DateTime DueDate { get; set; }
    public string AssignmentMethod { get; set; }
}
```

## API Endpoints

### POST /api/chores
- Creates new chore
- Returns: 201 Created with ChoreId

### PUT /api/chores/{choreId}
- Updates chore
- Returns: 200 OK

### DELETE /api/chores/{choreId}
- Deletes chore
- Returns: 204 No Content

### POST /api/chores/{choreId}/assign
- Assigns chore to member
- Returns: 201 Created with AssignmentId

### GET /api/chores/{choreId}
- Retrieves chore details
- Returns: 200 OK with ChoreDto

### GET /api/chores
- Retrieves all household chores
- Query params: category, difficulty, active
- Returns: 200 OK with list of ChoreDto

## Business Rules
1. Point value auto-calculated: Easy (1-5), Medium (6-10), Hard (11-20)
2. Can adjust points manually within difficulty range
3. Cannot delete chores with active assignments without confirmation
4. Chore name must be unique within household
5. Estimated duration must be > 0 and < 480 minutes (8 hours)
6. Archived chores maintain historical completion data
