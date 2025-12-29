# Backend Requirements - Meal Planning

## Overview
Backend services for scheduling meals on a calendar, generating weekly meal plans, and tracking meal completion with domain events.

## Domain Events
- MealPlanned
- MealPlanWeekGenerated
- MealPlannedModified
- MealCompleted

## API Endpoints

### Commands

#### POST /api/meal-plans
Schedule a meal for specific date and meal type
- **Request Body**: MealPlanCreateDto (recipeId, plannedDate, mealType, servings, householdMemberIds[], dietaryModifications)
- **Response**: 201 Created with MealPlanDto
- **Events**: MealPlanned
- **Business Rules**: Check ingredient availability, validate household members, alert dietary violations

#### POST /api/meal-plans/week
Generate complete weekly meal plan
- **Request Body**: WeeklyPlanDto (startDate, householdSize, dietaryPreferences, planMethod: auto/manual)
- **Response**: 201 Created with MealPlanDto[]
- **Events**: MealPlanWeekGenerated
- **Business Rules**: Balance variety, honor preferences, avoid recent recipes, ensure nutritional balance

#### PUT /api/meal-plans/{id}
Modify or swap planned meal
- **Request Body**: MealPlanUpdateDto (newRecipeId, servings, reason)
- **Response**: 200 OK with updated MealPlanDto
- **Events**: MealPlannedModified
- **Business Rules**: Update grocery list, adjust prep schedule

#### DELETE /api/meal-plans/{id}
Remove meal from plan
- **Response**: 204 No Content
- **Business Rules**: Remove from grocery list, update calendar

#### POST /api/meal-plans/{id}/complete
Mark meal as cooked and completed
- **Request Body**: CompletionDto (actualServings, feedback, leftoversAmount, successRating)
- **Response**: 200 OK
- **Events**: MealCompleted
- **Business Rules**: Update pantry inventory, create leftover entry, prompt for recipe rating

### Queries

#### GET /api/meal-plans
Get meal plans with date range filter
- **Query Parameters**: startDate, endDate, mealType, status
- **Response**: 200 OK with MealPlanDto[]
- **Authorization**: Authenticated users (own meal plans only)

#### GET /api/meal-plans/calendar
Get calendar view of meal plans
- **Query Parameters**: year, month, view (day/week/month)
- **Response**: 200 OK with CalendarDto
- **Performance**: Cached for current and next month

#### GET /api/meal-plans/{id}
Get meal plan details
- **Response**: 200 OK with detailed MealPlanDto including recipe, ingredients, nutrition

#### GET /api/meal-plans/suggestions
Get meal suggestions for date
- **Query Parameters**: date, mealType, householdMembers[]
- **Response**: 200 OK with RecipeDto[]
- **Business Rules**: Consider rotation, preferences, pantry ingredients, nutrition goals

## Domain Models

### MealPlan (Aggregate Root)
```csharp
public class MealPlan
{
    public Guid Id { get; private set; }
    public Guid RecipeId { get; private set; }
    public Recipe Recipe { get; private set; }
    public DateTime PlannedDate { get; private set; }
    public MealType MealType { get; private set; }
    public int Servings { get; private set; }
    public List<Guid> HouseholdMemberIds { get; private set; }
    public string DietaryModifications { get; private set; }
    public MealPlanStatus Status { get; private set; }
    public CompletionInfo Completion { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
```

### MealType (Enum)
- Breakfast
- Lunch
- Dinner
- Snack

### MealPlanStatus (Enum)
- Planned
- InPreparation
- Completed
- Cancelled

## Database Schema

### Tables
- **MealPlans**: Main meal planning table
- **MealPlanMembers**: Many-to-many meal plans to household members
- **MealCompletions**: Completion details with feedback

### Indexes
- MealPlans.PlannedDate + MealPlans.MealType
- MealPlans.CreatedBy + MealPlans.PlannedDate
- MealPlans.Status

## Business Rules

### Meal Planning
- Planned date must be today or future
- Recipe must exist and be accessible to user
- Servings must accommodate all selected household members
- Check for dietary violations and alert user
- Automatically add ingredients to grocery list if shopping not yet done

### Weekly Plan Generation
- Balance cuisine types across week
- Avoid repeating recipes within 2 weeks (configurable)
- Consider prep time distribution (quick meals on busy days)
- Honor dietary preferences and restrictions
- Ensure nutritional balance across week
- Mix family favorites with new recipes

### Meal Completion
- Update pantry inventory based on ingredients used
- Create leftover entry if applicable
- Prompt user to rate recipe
- Update recipe popularity and statistics
- Adjust future meal suggestions based on success

## Validation Rules

### MealPlanCreateDto
- RecipeId: Required, must exist
- PlannedDate: Required, not in past
- MealType: Required, valid enum
- Servings: Required, positive integer
- HouseholdMemberIds: Optional array of valid member IDs

### WeeklyPlanDto
- StartDate: Required, must be Monday
- HouseholdSize: Required, positive integer
- DietaryPreferences: Optional array
- PlanMethod: Required, valid enum

## Performance Optimization
- Cache calendar view for current month
- Preload recipes for next 7 days
- Batch insert weekly plans in single transaction
- Index on planned date for fast calendar queries

## Testing Requirements
- Unit tests for meal plan generation algorithm
- Integration tests for calendar operations
- Test dietary violation detection
- Test grocery list synchronization
