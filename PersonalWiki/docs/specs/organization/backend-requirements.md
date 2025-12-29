# Organization - Backend Requirements

## API Endpoints

#### POST /api/namespaces
Create namespace
- **Request**: `{ name, description, parentId }`
- **Events**: `NamespaceCreated`

#### POST /api/categories
Create category
- **Request**: `{ name, description }`
- **Events**: `CategoryCreated`

#### POST /api/pages/{id}/categorize
Add page to categories
- **Request**: `{ categoryIds }`
- **Events**: `PageCategorized`

#### PUT /api/pages/{id}/move
Move page to different namespace
- **Request**: `{ newNamespaceId }`
- **Events**: `PageMovedToNamespace`

## Data Models

```csharp
public class Namespace
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? ParentId { get; set; }
    public int PageCount { get; set; }
}

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Guid> PageIds { get; set; }
}
```
