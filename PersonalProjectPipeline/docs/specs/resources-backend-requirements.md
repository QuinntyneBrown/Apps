# Resources - Backend Requirements

## API Endpoints
- POST /api/resources - Add resource
- GET /api/projects/{projectId}/resources - Get resources
- POST /api/resources/upload - Upload file
- POST /api/resources/link - Add external link

## Domain Events
- ResourceAdded
- FileUploaded
- ExternalLinkAdded

## Data Models
```csharp
public class Resource {
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public ResourceType Type { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string FilePath { get; set; }
}
```
