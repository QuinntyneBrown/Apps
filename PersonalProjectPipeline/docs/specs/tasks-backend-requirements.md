# Tasks - Backend Requirements

## API Endpoints
- POST /api/tasks - Create task
- PUT /api/tasks/{id} - Update task
- PUT /api/tasks/{id}/complete - Mark complete
- POST /api/tasks/{id}/dependencies - Add dependency
- GET /api/projects/{projectId}/tasks - Get project tasks

## Domain Events
- TaskCreated
- TaskCompleted
- TaskDependencyAdded
- TaskRescheduled

## Data Models
```csharp
public class Task {
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; }
    public DateTime? DueDate { get; set; }
    public int EstimatedHours { get; set; }
    public bool IsCompleted { get; set; }
    public List<Guid> DependsOn { get; set; }
}
```
