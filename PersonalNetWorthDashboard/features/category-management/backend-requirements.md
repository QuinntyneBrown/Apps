# Backend Requirements - Category Management

## API Endpoints
- POST /api/categories - Create category (AssetCategoryCreated event)
- PUT /api/categories/{id} - Update category
- DELETE /api/categories/{id} - Delete category
- GET /api/categories/summary - Get totals by category

## Models
```csharp
public class AssetCategory {
    public Guid Id;
    public string CategoryName;
    public Guid ParentCategoryId;
    public string Color;
    public string Icon;
}
```
