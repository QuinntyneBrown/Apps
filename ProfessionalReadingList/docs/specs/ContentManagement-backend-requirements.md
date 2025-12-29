# Backend Requirements - Content Management

## Domain Events
- ArticleAdded
- BookAdded
- ReadingMaterialCategorized
- ReadingMaterialArchived

## Key API Endpoints
- POST /api/articles - Add article with URL or manual entry
- POST /api/books - Add book with ISBN lookup
- GET /api/reading-materials - Get all materials with filtering
- PUT /api/reading-materials/{id}/categorize - Add tags/categories
- POST /api/reading-materials/{id}/archive - Archive material
- GET /api/reading-materials/search - Full-text search
- POST /api/reading-materials/import - Import from external services

## Data Models
```csharp
public class ReadingMaterial : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public MaterialType Type { get; private set; } // Article, Book, Paper
    public string SourceUrl { get; private set; }
    public List<string> Categories { get; private set; }
    public List<string> Tags { get; private set; }
    public bool Archived { get; private set; }
}
```

## Business Rules
- Duplicate URL detection for articles
- Auto-metadata extraction from URLs
- ISBN validation and lookup for books
- Minimum one category required
- Archive preserves all data
