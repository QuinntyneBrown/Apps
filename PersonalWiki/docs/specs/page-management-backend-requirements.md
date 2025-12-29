# Page Management - Backend Requirements

## API Endpoints

#### POST /api/pages
Create new wiki page
- **Request**: `{ title, content, namespaceId, initialCategories }`
- **Events**: `PageCreated`

#### PUT /api/pages/{id}
Update page content
- **Request**: `{ content, editSummary }`
- **Events**: `PageUpdated`, `VersionCreated`

#### DELETE /api/pages/{id}
Soft delete page
- **Request**: `{ deletionReason }`
- **Events**: `PageDeleted`

#### POST /api/pages/{id}/restore
Restore deleted page
- **Events**: `PageRestored`

#### PUT /api/pages/{id}/rename
Rename page with redirect
- **Request**: `{ newTitle, createRedirect }`
- **Events**: `PageRenamed`

## Data Models

```csharp
public class WikiPage
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid NamespaceId { get; set; }
    public Guid AuthorId { get; set; }
    public int CurrentVersion { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
    public bool IsDeleted { get; set; }
}

public class PageVersion
{
    public Guid Id { get; set; }
    public Guid PageId { get; set; }
    public int VersionNumber { get; set; }
    public string Content { get; set; }
    public string EditSummary { get; set; }
    public Guid EditorId { get; set; }
    public DateTime CreatedAt { get; set; }
}
```
