# Backend Requirements - Family Preferences

## Domain Events
- DietaryPreferenceUpdated
- FamilyFavoriteIdentified

## API Endpoints

### Commands
- POST /api/household/members - Add family member
- PUT /api/household/members/{id}/preferences - Update preferences
- PUT /api/household/members/{id}/restrictions - Update restrictions

### Queries
- GET /api/household/members - List all members
- GET /api/household/members/{id} - Get member details
- GET /api/household/favorites - Family favorite recipes
- GET /api/recipes/filter-by-member/{id} - Recipes compatible with member

## Domain Models

```csharp
public class HouseholdMember
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public List<FoodPreference> Preferences { get; private set; }
    public List<DietaryRestriction> Restrictions { get; private set; }
    public List<Allergen> Allergens { get; private set; }
}

public class DietaryRestriction
{
    public string IngredientName { get; private set; }
    public RestrictionSeverity Severity { get; private set; }
    public string Reason { get; private set; }
    public DateTime EffectiveDate { get; private set; }
}
```

## Business Rules
- Track likes, dislikes, allergies per person
- Severity levels: Preference vs Allergy (critical)
- Identify family favorites (recipes all members rate 4+)
- Filter recipes by member compatibility
- Alert if meal includes allergen
- Support temporary restrictions
