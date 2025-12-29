# Timeline - Backend Requirements

## API Endpoints
- GET /api/projects/{id}/timeline - Get timeline data
- POST /api/milestones - Create milestone
- GET /api/timeline/critical-path - Calculate critical path

## Domain Events
- MilestoneCreated
- TimelineUpdated
- DeadlineApproaching

## Business Logic
- Calculate project critical path
- Identify scheduling conflicts
- Generate Gantt chart data
- Detect overdue tasks
