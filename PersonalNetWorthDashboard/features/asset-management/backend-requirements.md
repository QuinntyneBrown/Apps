# Backend Requirements - Asset Management

## API Endpoints
- POST /api/assets - Add asset (AssetAdded event)
- PUT /api/assets/{id}/value - Update value (AssetValueUpdated event)
- DELETE /api/assets/{id} - Remove asset (AssetRemoved event)
- GET /api/assets - Get all assets
- PUT /api/assets/{id}/category - Recategorize (AssetRecategorized event)

## Models
```csharp
public class Asset {
    public Guid Id;
    public AssetType Type;
    public decimal CurrentValue;
    public string Currency;
    public DateTime AcquisitionDate;
    public string Category;
}
public enum AssetType { BankAccount, Investment, Property, Vehicle, Other }
```
