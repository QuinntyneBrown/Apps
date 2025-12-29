# Backend Requirements - Recipe Management

## Overview
Backend services for managing recipe creation, storage, modification, search, and ratings with full CQRS implementation and domain event publishing.

## Domain Events
- RecipeAdded
- RecipeModified
- RecipeFavorited
- RecipeRated

## API Endpoints

### Commands

#### POST /api/recipes
Create a new recipe
- **Request Body**: RecipeCreateDto (name, cuisine, ingredients[], instructions[], prepTime, cookTime, servings, difficulty, source, photos[])
- **Response**: 201 Created with RecipeDto and Location header
- **Events**: RecipeAdded
- **Validation**: Required fields, valid ingredient quantities, positive time values
- **Authorization**: Authenticated users

#### PUT /api/recipes/{id}
Update existing recipe
- **Request Body**: RecipeUpdateDto (modified fields, modification notes)
- **Response**: 200 OK with updated RecipeDto
- **Events**: RecipeModified
- **Validation**: Recipe exists, valid modifications
- **Authorization**: Recipe owner or admin
- **Business Rules**: Store previous version for history

#### POST /api/recipes/{id}/favorite
Mark recipe as favorite
- **Request Body**: FavoriteDto (category, personalRating)
- **Response**: 200 OK
- **Events**: RecipeFavorited
- **Authorization**: Authenticated users
- **Business Rules**: One favorite per user per recipe

#### DELETE /api/recipes/{id}/favorite
Remove recipe from favorites
- **Response**: 204 No Content
- **Authorization**: Authenticated users

#### POST /api/recipes/{id}/ratings
Rate and review recipe
- **Request Body**: RatingDto (score 1-5, reviewText, cookingDate, wouldMakeAgain, difficultyExperienced)
- **Response**: 201 Created with RatingDto
- **Events**: RecipeRated
- **Validation**: Valid score range, cooking date not in future
- **Authorization**: Authenticated users
- **Business Rules**: Update recipe average rating

#### POST /api/recipes/import
Import recipe from URL or file
- **Request Body**: ImportDto (url or file, source)
- **Response**: 201 Created with RecipeDto
- **Events**: RecipeAdded
- **Business Rules**: Parse recipe from common formats, extract metadata
- **Authorization**: Authenticated users

### Queries

#### GET /api/recipes
Search and list recipes with filters
- **Query Parameters**: search, cuisine, difficulty, maxPrepTime, maxCookTime, dietary tags, ingredientFilter, favoriteOnly, sortBy, page, pageSize
- **Response**: 200 OK with PagedResult<RecipeDto>
- **Performance**: Indexed full-text search, cached popular queries
- **Authorization**: Public (filtered by user access)

#### GET /api/recipes/{id}
Get recipe details
- **Response**: 200 OK with detailed RecipeDto including ingredients, instructions, nutrition, ratings
- **Performance**: Cached for popular recipes
- **Authorization**: Public

#### GET /api/recipes/{id}/versions
Get recipe version history
- **Response**: 200 OK with RecipeVersionDto[]
- **Authorization**: Recipe owner or admin

#### GET /api/recipes/{id}/ratings
Get all ratings for recipe
- **Query Parameters**: page, pageSize
- **Response**: 200 OK with PagedResult<RatingDto>
- **Authorization**: Public

#### GET /api/recipes/favorites
Get user's favorite recipes
- **Query Parameters**: category, sortBy, page, pageSize
- **Response**: 200 OK with PagedResult<RecipeDto>
- **Authorization**: Authenticated users

#### GET /api/recipes/popular
Get most popular recipes
- **Query Parameters**: timeRange, limit
- **Response**: 200 OK with RecipeDto[]
- **Performance**: Cached, updated hourly
- **Authorization**: Public

#### GET /api/recipes/suggested
Get personalized recipe suggestions
- **Query Parameters**: context (expiring-ingredients, family-favorites, rotation)
- **Response**: 200 OK with RecipeDto[]
- **Business Rules**: ML-based recommendations using rating history, preferences, meal patterns
- **Authorization**: Authenticated users

## Domain Models

### Recipe (Aggregate Root)
```csharp
public class Recipe
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public CuisineType Cuisine { get; private set; }
    public List<RecipeIngredient> Ingredients { get; private set; }
    public List<RecipeInstruction> Instructions { get; private set; }
    public TimeSpan PrepTime { get; private set; }
    public TimeSpan CookTime { get; private set; }
    public int Servings { get; private set; }
    public DifficultyLevel Difficulty { get; private set; }
    public string Source { get; private set; }
    public List<RecipePhoto> Photos { get; private set; }
    public List<DietaryTag> DietaryTags { get; private set; }
    public NutritionalInfo Nutrition { get; private set; }
    public RecipeStatistics Statistics { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    // Methods
    public void UpdateDetails(RecipeUpdateDto dto);
    public void AddRating(Rating rating);
    public void UpdateNutrition(NutritionalInfo nutrition);
}
```

### RecipeIngredient (Value Object)
```csharp
public class RecipeIngredient
{
    public string Name { get; private set; }
    public decimal Quantity { get; private set; }
    public Unit Unit { get; private set; }
    public string Notes { get; private set; }
    public bool Optional { get; private set; }
    public string Category { get; private set; }
}
```

### Rating (Entity)
```csharp
public class Rating
{
    public Guid Id { get; private set; }
    public Guid RecipeId { get; private set; }
    public Guid UserId { get; private set; }
    public int Score { get; private set; } // 1-5
    public string ReviewText { get; private set; }
    public DateTime CookingDate { get; private set; }
    public bool WouldMakeAgain { get; private set; }
    public DifficultyLevel DifficultyExperienced { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
```

## Database Schema

### Tables
- **Recipes**: Main recipe table with core metadata
- **RecipeIngredients**: Ingredients for recipes (one-to-many)
- **RecipeInstructions**: Step-by-step instructions (one-to-many)
- **RecipePhotos**: Recipe photos with storage URLs (one-to-many)
- **RecipeDietaryTags**: Many-to-many recipe to dietary tags
- **RecipeRatings**: User ratings and reviews (one-to-many)
- **RecipeFavorites**: User favorites with categories (many-to-many)
- **RecipeVersions**: Version history for recipe modifications

### Indexes
- Recipes.Name (full-text index)
- Recipes.Cuisine + Recipes.Difficulty
- Recipes.CreatedAt DESC
- RecipeIngredients.Name (for ingredient search)
- RecipeRatings.RecipeId + RecipeRatings.Score
- RecipeFavorites.UserId + RecipeFavorites.RecipeId

## Business Rules

### Recipe Creation
- Recipe name required and must be 3-200 characters
- At least one ingredient required
- At least one instruction step required
- Prep time and cook time must be positive
- Servings must be at least 1
- Photos validated for format (JPEG, PNG, WEBP) and size (<5MB)
- Cuisine and difficulty must be valid enum values

### Recipe Modification
- Store previous version before modification
- Track modification date and user
- Recalculate nutrition if ingredients change
- Update meal plans using this recipe
- Notify users who favorited the recipe

### Recipe Rating
- Users can rate each recipe only once (update existing rating)
- Score must be 1-5
- Cooking date cannot be in the future
- Update recipe average rating and rating count
- Update recipe popularity score for recommendations

### Recipe Favorites
- Users can favorite unlimited recipes
- Optional categorization (Quick Weeknight, Special Occasion, etc.)
- Track favorite date for sorting
- Include in personalized recommendations

### Recipe Search
- Full-text search on recipe name and ingredient names
- Filter by cuisine, difficulty, time ranges, dietary tags
- Filter by available pantry ingredients
- Sort by relevance, rating, popularity, date added
- Pagination with 20 items per page default

## Validation Rules

### RecipeCreateDto
- Name: Required, 3-200 characters
- Cuisine: Required, valid enum
- Ingredients: Required, at least 1, max 100
- Instructions: Required, at least 1, max 50
- PrepTime: Required, 0-480 minutes
- CookTime: Required, 0-720 minutes
- Servings: Required, 1-100
- Difficulty: Required, valid enum
- Photos: Optional, max 10, each <5MB

### RecipeIngredientDto
- Name: Required, 1-100 characters
- Quantity: Required, positive decimal
- Unit: Required, valid enum
- Notes: Optional, max 200 characters

### RatingDto
- Score: Required, 1-5 integer
- ReviewText: Optional, max 2000 characters
- CookingDate: Required, not in future
- WouldMakeAgain: Required, boolean
- DifficultyExperienced: Required, valid enum

## Performance Optimization

### Caching Strategy
- Cache popular recipes (>100 views) for 1 hour
- Cache recipe search results for common queries for 15 minutes
- Invalidate cache on recipe modification
- Cache recipe ratings aggregates

### Query Optimization
- Use indexes for all search filters
- Implement full-text search with SQL Server Full-Text Search
- Lazy load recipe photos and instructions for list views
- Use projection for list views (don't load full entity)
- Batch load related data to avoid N+1 queries

### File Storage
- Upload recipe photos to Azure Blob Storage or AWS S3
- Generate thumbnails (small, medium, large) on upload
- Use CDN for photo delivery
- Implement lazy loading for photos in UI

## Error Handling
- 400 Bad Request: Invalid input, validation failures
- 401 Unauthorized: Authentication required
- 403 Forbidden: Insufficient permissions
- 404 Not Found: Recipe not found
- 409 Conflict: Duplicate recipe name for user
- 413 Payload Too Large: Photo file size exceeded
- 500 Internal Server Error: Unexpected errors

## Testing Requirements

### Unit Tests
- Recipe aggregate methods
- Rating calculation logic
- Search filter application
- Validation rules
- Domain event publishing

### Integration Tests
- Recipe CRUD operations
- Recipe import from URL
- Photo upload and storage
- Full-text search functionality
- Rating aggregation
- Favorite management

### Performance Tests
- Search performance with 10,000+ recipes
- Concurrent recipe creation
- Photo upload and processing
- Rating calculation under load
