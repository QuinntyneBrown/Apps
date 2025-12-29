# Backend Requirements - Nutrition Tracking

## Domain Events
- MealNutritionCalculated
- DailyNutritionSummarized
- DietaryGoalSet
- DietaryViolationDetected

## API Endpoints

### Commands
- POST /api/nutrition/calculate - Calculate recipe nutrition
- POST /api/nutrition/goals - Set dietary goals
- PUT /api/nutrition/goals/{id} - Update goals
- POST /api/nutrition/daily-summary - Generate daily summary

### Queries
- GET /api/nutrition/recipes/{id} - Get recipe nutrition
- GET /api/nutrition/daily/{date} - Daily nutrition summary
- GET /api/nutrition/goals - User's dietary goals
- GET /api/nutrition/trends - Weekly/monthly trends
- GET /api/nutrition/check-violation - Check meal against restrictions

## Domain Models

```csharp
public class NutritionalInfo
{
    public int Calories { get; private set; }
    public decimal Protein { get; private set; }
    public decimal Carbohydrates { get; private set; }
    public decimal Fat { get; private set; }
    public decimal Fiber { get; private set; }
    public decimal Sugar { get; private set; }
    public decimal Sodium { get; private set; }
    public Dictionary<string, decimal> Vitamins { get; private set; }
    public Dictionary<string, decimal> Minerals { get; private set; }
    public List<string> Allergens { get; private set; }
}

public class DietaryGoal
{
    public Guid Id { get; private set; }
    public Guid HouseholdMemberId { get; private set; }
    public GoalType Type { get; private set; }
    public Dictionary<string, decimal> Targets { get; private set; }
    public List<string> Restrictions { get; private set; }
    public DateTime StartDate { get; private set; }
}
```

## Business Rules
- Calculate nutrition using USDA database
- Adjust for serving size
- Detect allergens automatically
- Alert dietary violations before meal planning
- Track progress toward daily/weekly goals
- Support common diets: Keto, Paleo, Vegan, Low-carb, etc.

## Integration
- USDA FoodData Central API
- Nutritionix API (fallback)
