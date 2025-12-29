# Projects - Backend Requirements

## API Endpoints
- POST /api/projects - Create project
- PUT /api/projects/{id} - Update project
- PUT /api/projects/{id}/status - Change status
- GET /api/projects - List projects (filter by status)
- DELETE /api/projects/{id} - Archive project

## Domain Events
- ProjectCreated
- ProjectStatusChanged
- ProjectCompleted
- ProjectArchived
- MilestoneReached

## Data Models
```csharp
public class Project {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ProjectStatus Status { get; set; }
    public Priority Priority { get; set; }
    public DateTime? TargetDate { get; set; }
    public int CompletionPercentage { get; set; }
}

public enum ProjectStatus {
    Idea, Planning, Active, OnHold, Completed, Archived
}
```
