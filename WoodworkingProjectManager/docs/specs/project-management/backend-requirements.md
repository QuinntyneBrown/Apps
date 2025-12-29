# Project Management - Backend Requirements

## Domain Events
- ProjectConceived, ProjectPlanned, ProjectStarted, ProjectMilestoneReached, ProjectCompleted, ProjectAbandoned

## API Endpoints
- `POST /api/projects` - Create project
- `GET /api/projects` - List projects
- `POST /api/projects/{id}/plan` - Create build plan
- `POST /api/projects/{id}/milestone` - Add milestone

## Data Models
```csharp
WoodworkingProject {
    id: Guid
    name: string
    projectType: ProjectType
    difficulty: Difficulty
    designSketch: string
    materialList: Material[]
    estimatedCost: decimal
    status: ProjectStatus
}
```
