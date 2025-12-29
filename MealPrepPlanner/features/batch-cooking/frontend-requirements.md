# Batch Cooking - Frontend Requirements

## 1. Overview

Provides tools for planning, executing, and tracking batch cooking sessions with recipe scaling, ingredient consolidation, and storage management.

## 2. Key Pages

### 2.1 Batch Cooking Dashboard
- Upcoming sessions calendar
- Active session tracker
- Quick create session button
- Session history

### 2.2 Create/Edit Session
- Session name and date picker
- Add recipes with multipliers
- Recipe cards with scaling options
- Consolidated shopping list preview
- Storage planning

### 2.3 Active Session View
- Step-by-step cooking guide
- Recipe checklist
- Timers for multiple dishes
- Progress tracker
- Notes and tips

### 2.4 Storage Planner
- Container recommendations
- Labeling suggestions
- Freezer/fridge organization
- Expiry date tracking

## 3. Components

### 3.1 SessionCard
- Session name and date
- Recipe count and servings
- Status badge
- Quick actions

### 3.2 RecipeScaler
- Original servings display
- Multiplier selector (1x, 2x, 3x, custom)
- Scaled ingredients list
- Nutrition per serving

### 3.3 CookingTimer
- Multiple simultaneous timers
- Recipe association
- Audio/visual alerts
- Pause/reset controls

### 3.4 IngredientConsolidator
- Combined shopping list
- Grouped by category
- Quantity totals
- Export to grocery list

### 3.5 StorageTracker
- Container labeling
- Freeze/refrigerate date
- Expiry countdown
- Inventory view

## 4. Features

### 4.1 Smart Cooking Order
- Suggest optimal cooking sequence
- Identify parallel cooking opportunities
- Equipment usage visualization

### 4.2 Container Planning
- Recommend container sizes
- Calculate portions per container
- Generate labels

### 4.3 Reheating Guide
- Instructions per recipe
- Time and temperature
- Quality tips

### 4.4 Cost Tracking
- Total session cost
- Cost per serving
- Budget comparison

## 5. User Flows

### 5.1 Create Batch Session
1. Choose session date
2. Add recipes to session
3. Set multipliers for each
4. Review consolidated ingredients
5. Save session

### 5.2 Execute Batch Cooking
1. Start session
2. View cooking order
3. Set timers
4. Check off completed recipes
5. Log containers and storage
6. Complete session

## 6. Visualizations

- Timeline for cooking order
- Gantt chart for parallel tasks
- Storage capacity gauge
- Cost breakdown pie chart
