# Backend Requirements - Wishlist Management

## API Endpoints
- POST /api/wishlist - Add destination (DestinationAddedToWishlist)
- PUT /api/wishlist/{id}/prioritize - Prioritize (DestinationPrioritized)
- POST /api/wishlist/{id}/research - Save research (DestinationResearched)
- DELETE /api/wishlist/{id} - Remove (DestinationRemovedFromWishlist)
- GET /api/wishlist - Get all wishlist destinations

## Models
```csharp
public class WishlistDestination {
    public Guid Id;
    public string LocationName;
    public string Country;
    public int PriorityLevel;
    public string TargetSeason;
    public decimal EstimatedBudget;
    public string InspirationSource;
}
```
