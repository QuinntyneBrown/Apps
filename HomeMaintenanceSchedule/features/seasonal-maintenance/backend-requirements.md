# Seasonal Maintenance - Backend Requirements

## Overview
The Seasonal Maintenance backend manages seasonal checklists for home preparation (Spring, Summer, Fall, Winter) with automated reminders and progress tracking.

## Domain Model

### SeasonalChecklist Entity
```csharp
public class SeasonalChecklist : BaseEntity
{
    public Guid Id { get; set; }
    public Season Season { get; set; }
    public int Year { get; set; }
    public Guid PropertyId { get; set; }
    public string Location { get; set; }
    public List<ChecklistItem> Items { get; set; }
    public int TotalCount { get; set; }
    public int CompletedCount { get; set; }
    public ChecklistStatus Status { get; set; }
    public DateTime GeneratedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    // Navigation
    public Property Property { get; set; }
}
```

### ChecklistItem
```csharp
public class ChecklistItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskCategory Category { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedDate { get; set; }
    public Guid? RelatedTaskId { get; set; }
    public int SortOrder { get; set; }
}
```

### Enumerations
```csharp
public enum Season { Spring, Summer, Fall, Winter }
public enum ChecklistStatus { NotStarted, InProgress, Completed }
```

## API Endpoints

### Commands
- `POST /api/seasonal-checklists/generate` - Generate checklist for season/year
- `POST /api/seasonal-checklists/{id}/items/{itemId}/complete` - Mark item complete
- `PUT /api/seasonal-checklists/{id}/items` - Update checklist items
- `DELETE /api/seasonal-checklists/{id}` - Delete checklist

### Queries
- `GET /api/seasonal-checklists` - Get all checklists (filtered by season, year, property)
- `GET /api/seasonal-checklists/{id}` - Get checklist by ID
- `GET /api/seasonal-checklists/current` - Get checklist for current season
- `GET /api/seasonal-checklists/templates` - Get seasonal templates

## Domain Events

### SeasonalChecklistGenerated
```csharp
public class SeasonalChecklistGeneratedEvent : IDomainEvent
{
    public Guid ChecklistId { get; set; }
    public Season Season { get; set; }
    public int Year { get; set; }
    public int TaskCount { get; set; }
    public string Location { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### SeasonalPreparationCompleted
```csharp
public class SeasonalPreparationCompletedEvent : IDomainEvent
{
    public Guid ChecklistId { get; set; }
    public Season Season { get; set; }
    public int Year { get; set; }
    public DateTime CompletionDate { get; set; }
    public int TasksCompleted { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## Business Logic

### Checklist Generation
- Generate based on season and location
- Include standard tasks for season
- Consider climate zone (e.g., northern climates need winterization)
- Auto-link existing recurring tasks
- Set recommended completion dates

### Default Templates
**Spring Checklist**:
- Inspect roof for winter damage
- Clean gutters and downspouts
- Service HVAC system
- Check exterior paint and caulking
- Test irrigation system
- Fertilize lawn
- Inspect foundation for cracks

**Summer Checklist**:
- Clean/maintain air conditioning
- Inspect deck and patio
- Check plumbing for leaks
- Clean dryer vents
- Power wash exterior
- Trim trees and shrubs

**Fall Checklist**:
- Clean gutters
- Winterize outdoor faucets
- Service heating system
- Seal windows and doors
- Rake leaves
- Store outdoor furniture
- Check attic insulation

**Winter Checklist**:
- Inspect heating system
- Clean fireplace/chimney
- Check for ice dams
- Test carbon monoxide detectors
- Insulate pipes
- Emergency supplies check

## Validation Rules
- Only one active checklist per season/year/property
- Season must be valid enum value
- Year must be current or future year
- All items must have unique IDs

## Testing Requirements
- Unit tests for checklist generation logic
- Integration tests for completion tracking
- E2E tests for seasonal workflow

---

**Version**: 1.0
**Last Updated**: 2025-12-29
