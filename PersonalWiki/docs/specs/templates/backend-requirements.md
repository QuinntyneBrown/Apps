# Templates - Backend Requirements

## API Endpoints

#### POST /api/templates
Create template
- **Request**: `{ name, content, description }`
- **Events**: `TemplateCreated`

#### PUT /api/templates/{id}
Update template
- **Events**: `TemplateUpdated`

#### POST /api/pages/from-template
Create page from template
- **Request**: `{ templateId, title, variables }`
- **Events**: `PageCreated`, `TemplateApplied`

## Data Models

```csharp
public class Template
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    public List<string> Variables { get; set; }
    public int UsageCount { get; set; }
}
```
