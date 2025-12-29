# Backend Requirements - Discovery and Recommendation

## API Endpoints

- **POST /api/wishlist** - Add to wishlist (BookWishlisted event)
- **PUT /api/wishlist/{id}/priority** - Update priority
- **DELETE /api/wishlist/{id}** - Remove from wishlist
- **GET /api/wishlist** - Get wishlist with priorities
- **POST /api/recommendations/received** - Record recommendation (BookRecommendationReceived event)
- **POST /api/recommendations/given** - Give recommendation (BookRecommendationGiven event)
- **POST /api/reading-lists** - Create list (ReadingListCreated event)
- **PUT /api/reading-lists/{id}** - Update list
- **GET /api/reading-lists** - Get user's lists
- **GET /api/reading-lists/public** - Browse public lists
- **GET /api/recommendations/suggestions** - Get AI recommendations

## Domain Models

```csharp
public class WishlistItem
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public DateTime AddedDate { get; set; }
    public int PriorityLevel { get; set; } // 1-5
    public string SourceOfRecommendation { get; set; }
    public DateTime? AnticipatedReadDate { get; set; }
    public string AcquisitionPlan { get; set; }
    public string InterestReason { get; set; }
}

public class BookRecommendation
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string Recommender { get; set; }
    public string Recipient { get; set; }
    public string RecommendationReason { get; set; }
    public DateTime RecommendationDate { get; set; }
    public AcceptanceStatus AcceptanceStatus { get; set; }
    public string RecipientResponse { get; set; }
}

public enum AcceptanceStatus { Pending, Accepted, Declined, AddedToWishlist }

public class ReadingList
{
    public Guid Id { get; set; }
    public string ListName { get; set; }
    public string Description { get; set; }
    public List<Guid> BookIds { get; set; }
    public List<int> BookOrder { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsPublic { get; set; }
    public string Theme { get; set; }
}
```

## Business Rules
1. Wishlist items ordered by priority
2. Recommendations tracked bidirectionally
3. Reading lists support custom ordering
4. AI recommendations based on ratings and genres
