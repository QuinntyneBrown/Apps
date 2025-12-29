# Project Management - Backend Requirements

## Overview
Backend API for managing classic car restoration projects from inception to completion.

## Domain Events
- **RestorationProjectStarted**: Triggered when a new restoration project is created
- **ProjectScopesDefined**: Triggered when restoration scope and objectives are set
- **ProjectMilestoneReached**: Triggered when a major phase is completed
- **RestorationCompleted**: Triggered when the project is finished
- **ProjectAbandoned**: Triggered when a project is discontinued

## API Endpoints

### Projects
- `POST /api/projects` - Create new restoration project
- `GET /api/projects` - List all projects with filtering and pagination
- `GET /api/projects/{id}` - Get project details
- `PUT /api/projects/{id}` - Update project information
- `DELETE /api/projects/{id}` - Delete/archive project
- `POST /api/projects/{id}/scope` - Define project scope
- `GET /api/projects/{id}/scope` - Get project scope details
- `PUT /api/projects/{id}/scope` - Update project scope

### Milestones
- `POST /api/projects/{id}/milestones` - Create milestone
- `GET /api/projects/{id}/milestones` - List project milestones
- `GET /api/projects/{id}/milestones/{milestoneId}` - Get milestone details
- `PUT /api/projects/{id}/milestones/{milestoneId}` - Update milestone
- `DELETE /api/projects/{id}/milestones/{milestoneId}` - Delete milestone
- `POST /api/projects/{id}/milestones/{milestoneId}/complete` - Mark milestone as complete

### Project Status
- `POST /api/projects/{id}/complete` - Mark project as completed
- `POST /api/projects/{id}/abandon` - Mark project as abandoned
- `GET /api/projects/{id}/status` - Get project status summary
- `GET /api/projects/{id}/timeline` - Get project timeline

## Data Models

### RestorationProject
```csharp
{
    "id": "guid",
    "vehicleMake": "string",
    "vehicleModel": "string",
    "year": "int",
    "vin": "string",
    "acquisitionDate": "datetime",
    "purchasePrice": "decimal",
    "conditionAtStart": "string",
    "targetCompletion": "datetime",
    "status": "enum[Planning, InProgress, Completed, Abandoned]",
    "createdAt": "datetime",
    "updatedAt": "datetime"
}
```

### ProjectScope
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "restorationType": "enum[Concours, Restomod, Driver]",
    "systemsToRestore": "array<string>",
    "originalityLevel": "enum[FullyOriginal, ModernizedPerformance, Custom]",
    "budgetCeiling": "decimal",
    "qualityTargets": "string",
    "createdAt": "datetime",
    "updatedAt": "datetime"
}
```

### Milestone
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "name": "string",
    "description": "string",
    "targetDate": "datetime",
    "completionDate": "datetime?",
    "qualityAssessment": "string?",
    "timeSpent": "decimal?",
    "costsToDate": "decimal?",
    "status": "enum[Pending, InProgress, Completed]",
    "createdAt": "datetime",
    "updatedAt": "datetime"
}
```

## Business Rules

### Project Creation
- VIN must be unique across all projects
- Purchase price must be positive
- Acquisition date cannot be in the future
- At least vehicle make and model are required

### Scope Definition
- Budget ceiling must be greater than purchase price
- Systems to restore must be validated against predefined list
- Quality targets must be defined for each restoration type

### Milestones
- Milestones must have unique names within a project
- Target dates must be after project start date
- Completed milestones cannot be deleted
- Quality assessment required when marking complete

### Project Completion
- All active milestones must be completed or cancelled
- Completion date cannot be before project start
- Before/after comparison required for completion
- Total costs must be calculated

### Project Abandonment
- Must specify reason for abandonment
- Progress percentage required
- Salvageable parts list recommended
- Cannot abandon already completed project

## Validation Rules

### Vehicle Information
- Make: Required, 1-50 characters
- Model: Required, 1-50 characters
- Year: Required, 1900-current year
- VIN: Required, 17 characters (for modern vehicles)
- Purchase price: Required, >= 0

### Scope
- Restoration type: Required, must be valid enum
- Budget ceiling: Required, > 0
- Quality targets: Required, max 500 characters

### Milestones
- Name: Required, 1-100 characters
- Target date: Required
- Time spent: >= 0
- Costs: >= 0

## Integration Points

### Event Publishing
- Publish domain events to event bus
- Support for event replay and audit
- Event versioning for backward compatibility

### Photo Service
- Integration for project photos
- Before/after comparison generation
- Milestone photo attachment

### Budget Service
- Real-time budget tracking
- Cost allocation to milestones
- Budget alert notifications

### Notification Service
- Milestone reminders
- Project deadline alerts
- Budget overage notifications

## Performance Requirements
- Project list query: < 500ms for 100 projects
- Project details load: < 200ms
- Milestone operations: < 300ms
- Support for 50 concurrent project updates
- Pagination support for large result sets

## Security Requirements
- User must own project to modify
- Read access can be shared with collaborators
- Project deletion requires confirmation
- Audit log for all project changes
- Role-based access: Owner, Collaborator, Viewer

## Error Handling
- Invalid VIN format returns 400 with validation errors
- Project not found returns 404
- Duplicate VIN returns 409 Conflict
- Unauthorized access returns 403
- Server errors return 500 with correlation ID

## Testing Requirements
- Unit tests for business rules
- Integration tests for API endpoints
- Event publishing verification
- Performance tests for list queries
- Security tests for authorization
