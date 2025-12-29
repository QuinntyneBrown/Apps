# Frontend Requirements - Nutrition Tracking

## Pages/Views

### 1. Nutrition Dashboard (`/nutrition`)

**Components**:
- NutritionHeader: Date selector, goal progress overview
- DailyMacroChart: Pie or bar chart for calories, protein, carbs, fat
- GoalProgressBars: Progress toward daily targets
- MealBreakdown: Nutrition by meal type
- TrendCharts: Weekly/monthly trends
- RecommendationCards: Suggestions to meet goals

**Features**:
- View daily nutrition summary
- Compare to dietary goals
- See weekly/monthly trends
- Filter by household member
- Export nutrition reports
- Set and track multiple goals
- Nutrition insights and recommendations

**API Calls**:
- GET /api/nutrition/daily/{date}
- GET /api/nutrition/goals
- GET /api/nutrition/trends

### 2. Set Goals Dialog

**Components**:
- GoalTypeSelector: Weight loss, muscle gain, maintenance, custom
- MacroTargets: Calories, protein, carbs, fat sliders
- DietaryRestrictions: Allergens, dietary preferences
- HouseholdMemberSelector: Apply to which family members

**Features**:
- Preset goal templates
- Custom macro targets
- Restriction management
- Multiple goals per person

**API Calls**:
- POST /api/nutrition/goals
- PUT /api/nutrition/goals/{id}

### 3. Recipe Nutrition View

**Components**:
- NutritionLabel: Standard nutrition facts panel
- ServingSizeAdjuster: Recalculate for different servings
- DetailedMicronutrients: Vitamins and minerals accordion
- AllergenWarnings: Highlighted allergen list

**Features**:
- Standard nutrition label format
- Adjustable serving size
- Detailed micronutrients
- Allergen highlighting
- Compare to daily goals

## Charts
- Macro distribution (pie chart)
- Daily trends (line chart)
- Goal progress (progress bars)
- Weekly comparison (bar chart)

## State Management
- `nutritionSlice`: Daily summaries, goals, trends
