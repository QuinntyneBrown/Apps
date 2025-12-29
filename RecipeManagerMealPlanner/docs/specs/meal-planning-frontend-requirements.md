# Frontend Requirements - Meal Planning

## Overview
Interactive calendar interface for planning, viewing, and managing meals with drag-and-drop functionality and weekly plan generation.

## Pages/Views

### 1. Meal Calendar Page (`/meal-plan`)

**Components**:
- MealCalendarHeader: View switcher (day/week/month), date navigation, generate plan button
- MealCalendar: Interactive calendar with meal slots
- MealSlot: Individual meal display with recipe info
- QuickAddMealButton: Add meal to specific date/time
- MealPlanSidebar: Today's meals, upcoming meals, suggestions

**Features**:
- Drag-and-drop recipes onto calendar
- Click date/meal slot to add meal
- View modes: Day, Week (default), Month
- Color-coding by meal type or cuisine
- Quick view recipe details on hover
- Edit/delete meals inline
- Mark meals as completed
- Generate full week meal plan with one click
- Print weekly meal plan

**State Management**:
- Calendar view mode and date range
- Loaded meal plans
- Selected date/meal slot
- Drag state for recipe placement

**API Calls**:
- GET /api/meal-plans/calendar
- POST /api/meal-plans
- PUT /api/meal-plans/{id}
- DELETE /api/meal-plans/{id}
- POST /api/meal-plans/week

### 2. Add Meal Dialog

**Components**:
- RecipeSelector: Search or browse recipes to add
- DateTimePicker: Select date and meal type
- ServingsAdjuster: Set number of servings
- HouseholdMemberSelector: Which family members will eat
- DietaryModificationInput: Optional modifications
- NutritionPreview: Show nutrition for planned meal

**Features**:
- Recipe search within dialog
- Filter recipes by dietary compatibility
- Show dietary violation warnings
- Auto-suggest recipes based on preferences
- Display ingredient availability from pantry
- Calculate and display nutritional info

**API Calls**:
- GET /api/recipes (search)
- GET /api/meal-plans/suggestions
- POST /api/meal-plans

### 3. Weekly Plan Generator Dialog

**Components**:
- WeekSelector: Choose week to plan
- PlanningPreferences: Auto-plan settings
- MealTypeToggle: Which meals to plan (breakfast/lunch/dinner)
- DietaryPreferenceSelector: Honor specific diets
- PlanPreview: Show generated plan before confirming
- RegenerateButton: Generate different plan

**Features**:
- One-click auto-generation
- Customize which meal types to plan
- Set household size and dietary preferences
- Preview generated plan
- Regenerate if not satisfied
- Edit individual meals before confirming
- Show nutritional balance for week

**API Calls**:
- POST /api/meal-plans/week (generate)
- Preview returned plan before confirmation

### 4. Meal Completion Dialog

**Components**:
- CompletionForm: Rate the cooking experience
- ActualServingsInput: How many servings made
- SuccessRating: How did it turn out (1-5 stars)
- FeedbackTextarea: Optional notes
- LeftoversToggle: Any leftovers to log?
- LeftoversAmount: Quantity of leftovers
- RateRecipePrompt: Link to rate the recipe

**Features**:
- Quick completion (one-click)
- Detailed completion (with feedback)
- Leftover logging integrated
- Automatic pantry inventory update
- Prompt to rate recipe if not rated
- Update meal plan statistics

**API Calls**:
- POST /api/meal-plans/{id}/complete
- POST /api/leftovers (if leftovers logged)

## Shared Components

### MealCard
Display meal in calendar slot with recipe thumbnail, name, time metadata

### CalendarGrid
Reusable calendar component with customizable cell rendering

### RecipeQuickView
Popover showing recipe details on hover

## User Workflows

### Plan Single Meal
1. Navigate to date on calendar
2. Click "Add Meal" on desired date/meal type
3. Search or browse recipes
4. Select recipe
5. Adjust servings
6. Select household members (optional)
7. Confirm and save
8. Meal appears on calendar

### Generate Weekly Meal Plan
1. Click "Generate Week Plan"
2. Select week to plan
3. Choose meal types (breakfast, lunch, dinner)
4. Set household size and preferences
5. Click "Generate"
6. Review generated plan
7. Edit any meals if desired or regenerate
8. Confirm plan
9. All meals added to calendar
10. Grocery list auto-generated

### Modify Planned Meal
1. Click on planned meal in calendar
2. Choose "Swap Recipe" or "Edit Details"
3. Select new recipe or adjust servings
4. Save changes
5. Grocery list updated automatically

### Complete Meal After Cooking
1. Click on today's completed meal
2. Click "Mark as Cooked"
3. Provide quick feedback (optional)
4. Log leftovers if any
5. Prompted to rate recipe
6. Meal marked complete
7. Pantry updated

## Responsive Design

### Mobile
- Day view optimized for mobile
- Swipe between dates
- Bottom sheet for adding meals
- Compact meal cards

### Desktop
- Week view default
- Sidebar with today's summary
- Drag-and-drop from recipe sidebar
- Keyboard shortcuts for navigation

## State Management

### Redux/Zustand Slices
- `mealPlanSlice`: Calendar meal plans
- `calendarViewSlice`: View settings and date range

### React Query Keys
- `['meal-plans', startDate, endDate]`
- `['calendar', year, month]`
- `['meal-suggestions', date, mealType]`

## Performance
- Prefetch next/previous month on navigation
- Optimistic updates for meal completion
- Lazy load recipe details on hover
- Virtual scrolling for month view

## Accessibility
- Keyboard navigation: Arrow keys to navigate dates, Enter to add meal
- ARIA labels for calendar cells
- Screen reader announcements for date changes
- Focus management for dialogs
