# Meal Planning - Wireframes

## 1. Meal Plans List Page

### Layout
```
+----------------------------------------------------------+
|  [Logo] MealPrepPlanner          [Search] [User Menu]   |
+----------------------------------------------------------+
|  Dashboard | Meal Plans | Recipes | Grocery | Nutrition |
+----------------------------------------------------------+
|                                                          |
|  Meal Plans                        [+ Create New Plan]  |
|  ___________                                             |
|                                                          |
|  Filter:  [All ▼] [Status ▼] [Date Range: ____]        |
|                                                          |
|  +----------------------------------------------------+  |
|  | Weekly Meal Prep                    [Active]      |  |
|  | Dec 25, 2025 - Dec 31, 2025                       |  |
|  | 18/21 meals completed                             |  |
|  | [Progress bar =================>    ] 85%         |  |
|  |                                                    |  |
|  | [View] [Edit] [Complete] [...]                    |  |
|  +----------------------------------------------------+  |
|                                                          |
|  +----------------------------------------------------+  |
|  | January Meal Plan                   [Draft]       |  |
|  | Jan 1, 2026 - Jan 7, 2026                         |  |
|  | 14/21 meals planned                               |  |
|  | [Progress bar ===========>          ] 66%         |  |
|  |                                                    |  |
|  | [View] [Edit] [Activate] [Delete] [...]           |  |
|  +----------------------------------------------------+  |
|                                                          |
|  +----------------------------------------------------+  |
|  | Holiday Meal Plan                [Completed]      |  |
|  | Dec 18, 2025 - Dec 24, 2025                       |  |
|  | 21/21 meals completed                             |  |
|  | [Progress bar ========================] 100%      |  |
|  |                                                    |  |
|  | [View] [Duplicate] [Export]                       |  |
|  +----------------------------------------------------+  |
|                                                          |
|  [< Prev]  Page 1 of 3  [Next >]                        |
|                                                          |
+----------------------------------------------------------+
```

### Components
1. **Header**: Navigation menu, search, user menu
2. **Page Title**: "Meal Plans" with create button
3. **Filters**: Status dropdown, date range picker
4. **Meal Plan Cards**: Display plan summary with actions
5. **Pagination**: Navigate between pages

## 2. Meal Plan Details - Calendar View

### Layout
```
+----------------------------------------------------------+
|  [Logo] MealPrepPlanner          [Search] [User Menu]   |
+----------------------------------------------------------+
|  Dashboard | Meal Plans | Recipes | Grocery | Nutrition |
+----------------------------------------------------------+
|                                                          |
|  [< Back] Weekly Meal Prep      [Active]  [Activate ▼]  |
|  Dec 25, 2025 - Dec 31, 2025                            |
|  ________________________________________________________  |
|                                                          |
|  [Calendar View] [List View]           [Week ▼] [Edit]  |
|                                                          |
|  +----------------------------------------------------+  |
|  | Statistics                                         |  |
|  | Total Meals: 21  | Completed: 18  | Remaining: 3  |  |
|  | Completion: 85%  | Calories: 12,500 (avg 1,785/d) |  |
|  +----------------------------------------------------+  |
|                                                          |
|    Mon 12/25   Tue 12/26   Wed 12/27   Thu 12/28  ...   |
|  +----------+----------+----------+----------+------+    |
|  |Breakfast |Breakfast |Breakfast |Breakfast |      |    |
|  |[✓]       |[✓]       |[✓]       |[ ]       |      |    |
|  |Oatmeal   |Smoothie  |Pancakes  |          |      |    |
|  |2 servings|1 serving |4 servings|          |      |    |
|  +----------+----------+----------+----------+------+    |
|  |Lunch     |Lunch     |Lunch     |Lunch     |      |    |
|  |[✓]       |[✓]       |[✓]       |[ ]       |      |    |
|  |Chicken   |Leftover  |Salad     |          |      |    |
|  |Salad     |Chicken   |Bowl      |          |      |    |
|  +----------+----------+----------+----------+------+    |
|  |Dinner    |Dinner    |Dinner    |Dinner    |      |    |
|  |[✓]       |[✓]       |[✓]       |[ ]       |      |    |
|  |Stir Fry  |Pasta     |Grilled   |          |      |    |
|  |4 servings|4 servings|Salmon    |          |      |    |
|  +----------+----------+----------+----------+------+    |
|  |Snack     |Snack     |Snack     |Snack     |      |    |
|  |[✓]       |[✓]       |[✓]       |[✓]       |      |    |
|  |Protein   |Fruit     |Nuts      |Yogurt    |      |    |
|  |Shake     |          |          |          |      |    |
|  +----------+----------+----------+----------+------+    |
|                                                          |
|  Click on empty slot to add meal, or drag recipe here   |
|                                                          |
+----------------------------------------------------------+
```

### Interactions
- Click empty slot to open recipe selector
- Click filled slot to view/edit meal details
- Drag recipe from sidebar to slot
- Check box to mark meal completed
- Hover to see quick actions (edit, delete)

## 3. Create/Edit Meal Plan Form

### Layout
```
+----------------------------------------------------------+
|  Create New Meal Plan                           [X]     |
+----------------------------------------------------------+
|                                                          |
|  Plan Name *                                             |
|  [_____________________________________]                 |
|                                                          |
|  Start Date *                         End Date *         |
|  [____/____/________] 📅              [____/____/____] 📅|
|                                                          |
|  Description (optional)                                  |
|  [_____________________________________]                 |
|  [_____________________________________]                 |
|  [_____________________________________]                 |
|                                                          |
|  Quick Setup (optional)                                  |
|  [ ] Generate default meal slots                        |
|  Meals per day: [3 ▼]  (Breakfast, Lunch, Dinner)       |
|                                                          |
|  ________________________________________________________  |
|                                                          |
|               [Cancel]  [Save Draft]  [Save & Continue] |
|                                                          |
+----------------------------------------------------------+
```

### Validation
- Plan Name: Required, max 200 characters
- Start Date: Required, cannot be in past
- End Date: Required, must be after start date
- Real-time validation with error messages

## 4. Recipe Selector Modal

### Layout
```
+----------------------------------------------------------+
|  Select Recipe for Monday Breakfast            [X]      |
+----------------------------------------------------------+
|                                                          |
|  [Search recipes...]                    [Favorites] [All]|
|                                                          |
|  Categories: [All] [Breakfast] [Quick] [Healthy]        |
|                                                          |
|  +----------+  +----------+  +----------+  +----------+  |
|  | [Image]  |  | [Image]  |  | [Image]  |  | [Image]  |  |
|  | Oatmeal  |  | Smoothie |  | Pancakes |  | Eggs &   |  |
|  | Bowl     |  | Bowl     |  |          |  | Toast    |  |
|  |          |  |          |  |          |  |          |  |
|  | ⭐ 4.5   |  | ⭐ 4.8   |  | ⭐ 4.3   |  | ⭐ 5.0   |  |
|  | 10 min   |  | 5 min    |  | 20 min   |  | 15 min   |  |
|  | 320 cal  |  | 250 cal  |  | 450 cal  |  | 380 cal  |  |
|  |          |  |          |  |          |  |          |  |
|  | [Select] |  | [Select] |  | [Select] |  | [Select] |  |
|  +----------+  +----------+  +----------+  +----------+  |
|                                                          |
|  [Load more...]                                          |
|                                                          |
+----------------------------------------------------------+
```

### Features
- Search functionality
- Filter by category, dietary restrictions
- Show recipe preview (image, rating, time, calories)
- Quick select button
- Recently used recipes at top

## 5. Meal Details Panel

### Layout
```
+----------------------------------------------------------+
|  Meal Details                                   [X]     |
+----------------------------------------------------------+
|                                                          |
|  Monday, December 25                                     |
|  Breakfast                                               |
|  ________________________________________________________  |
|                                                          |
|  Recipe: Oatmeal Bowl                          ⭐ 4.5   |
|  [View Full Recipe]                                      |
|                                                          |
|  Servings                                                |
|  [-] [2] [+]                                             |
|                                                          |
|  Notes                                                   |
|  [_____________________________________]                 |
|  [_____________________________________]                 |
|                                                          |
|  Nutritional Information (per serving)                   |
|  Calories: 320 | Protein: 12g | Carbs: 45g | Fat: 8g    |
|                                                          |
|  Status                                                  |
|  [✓] Mark as completed                                   |
|                                                          |
|  ________________________________________________________  |
|                                                          |
|               [Remove from Plan]         [Save Changes]  |
|                                                          |
+----------------------------------------------------------+
```

### Actions
- Adjust servings
- Add notes
- Mark completed
- View full recipe details
- Remove from plan

## 6. Active Meal Plan Dashboard

### Layout
```
+----------------------------------------------------------+
|  [Logo] MealPrepPlanner          [Search] [User Menu]   |
+----------------------------------------------------------+
|  Dashboard | Meal Plans | Recipes | Grocery | Nutrition |
+----------------------------------------------------------+
|                                                          |
|  Active Meal Plan: Weekly Meal Prep                     |
|  ________________________________________________________  |
|                                                          |
|  +----------------------+  +---------------------------+  |
|  | Today's Meals        |  | Upcoming                  |  |
|  |                      |  |                           |  |
|  | [✓] Breakfast        |  | Tomorrow                  |  |
|  |     Oatmeal Bowl     |  | Breakfast: Smoothie       |  |
|  |                      |  | Lunch: Salad              |  |
|  | [ ] Lunch            |  | Dinner: Pasta             |  |
|  |     Chicken Salad    |  |                           |  |
|  |                      |  | Thursday                  |  |
|  | [ ] Dinner           |  | Breakfast: Pancakes       |  |
|  |     Stir Fry         |  | Lunch: Sandwich           |  |
|  |                      |  | Dinner: Grilled Salmon    |  |
|  | [✓] Snack            |  |                           |  |
|  |     Protein Shake    |  | [View Full Calendar]      |  |
|  +----------------------+  +---------------------------+  |
|                                                          |
|  +----------------------+  +---------------------------+  |
|  | Quick Stats          |  | Grocery List              |  |
|  |                      |  |                           |  |
|  | Completion: 85%      |  | [ ] Chicken breasts       |  |
|  | [Progress Bar=====>] |  | [ ] Brown rice            |  |
|  |                      |  | [ ] Broccoli              |  |
|  | Total Meals: 21      |  | [✓] Oats                  |  |
|  | Completed: 18        |  | [✓] Bananas               |  |
|  | Remaining: 3         |  | [ ] Salmon fillet         |  |
|  |                      |  |                           |  |
|  | Avg Calories: 1,785  |  | [View All Items]          |  |
|  +----------------------+  +---------------------------+  |
|                                                          |
|  +---------------------------------------------------+   |
|  | Nutrition Progress This Week                      |   |
|  |                                                   |   |
|  | Calories:   [=========>      ] 1,680 / 2,000     |   |
|  | Protein:    [============>   ] 85g / 100g        |   |
|  | Carbs:      [===========>    ] 180g / 200g       |   |
|  | Fat:        [=========>      ] 55g / 65g         |   |
|  +---------------------------------------------------+   |
|                                                          |
+----------------------------------------------------------+
```

### Sections
1. **Today's Meals**: List with completion checkboxes
2. **Upcoming Meals**: Next 2-3 days preview
3. **Quick Stats**: Progress and metrics
4. **Grocery List**: Top pending items
5. **Nutrition Progress**: Daily averages vs goals

## 7. Mobile View - Meal Plan Calendar

### Layout (Portrait)
```
+---------------------+
| ☰ MealPrepPlanner   |
+---------------------+
| Weekly Meal Prep    |
| Active      [···]   |
+---------------------+
|                     |
| [Calendar] [List]   |
|                     |
| < Mon 12/25 >       |
|                     |
| Breakfast     [✓]   |
| Oatmeal Bowl        |
| 2 servings          |
| [Tap to view]       |
|                     |
| Lunch         [ ]   |
| Chicken Salad       |
| 1 serving           |
| [Tap to view]       |
|                     |
| Dinner        [✓]   |
| Stir Fry            |
| 4 servings          |
| [Tap to view]       |
|                     |
| Snack         [✓]   |
| Protein Shake       |
| 1 serving           |
| [Tap to view]       |
|                     |
| [ + Add Meal ]      |
|                     |
+---------------------+
| [Quick Actions]     |
+---------------------+
```

### Mobile Features
- Swipe left/right to change days
- Tap meal to view details
- Bottom sheet for actions
- Floating action button for quick add
- Simplified navigation

## 8. Color Coding and Visual Indicators

### Meal Type Colors
- **Breakfast**: 🟡 Yellow/Orange (#FFA726)
- **Lunch**: 🟢 Green (#66BB6A)
- **Dinner**: 🔵 Blue (#42A5F5)
- **Snack**: 🟣 Purple (#AB47BC)

### Status Badges
- **Draft**: Gray (#9E9E9E)
- **Active**: Green (#4CAF50)
- **Completed**: Blue (#2196F3)
- **Archived**: Dark Gray (#616161)

### Progress Indicators
- Progress bars with percentage
- Color gradient (red → yellow → green)
- Completion checkmarks

### Icons
- 📅 Calendar
- ✓ Completed
- + Add
- ✏️ Edit
- 🗑️ Delete
- ⭐ Favorite/Rating
- ⏱️ Time
- 🔥 Calories
- 📊 Stats
