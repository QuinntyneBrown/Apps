# Meal Planning - Frontend Requirements

## 1. Overview

The Meal Planning frontend provides an intuitive interface for users to create, manage, and visualize their meal plans through calendar views, drag-and-drop functionality, and interactive meal assignment.

## 2. Pages and Routes

### 2.1 Meal Plans List
**Route**: `/meal-plans`

**Purpose**: Display all meal plans for the user

**Components**:
- Page header with "Create New Plan" button
- Filter controls (status, date range)
- Grid/list view of meal plans
- Quick actions (activate, edit, delete, duplicate)

**Data Requirements**:
- Fetch paginated meal plans
- Support filtering and sorting
- Real-time status updates

### 2.2 Meal Plan Details
**Route**: `/meal-plans/:id`

**Purpose**: View and edit specific meal plan

**Components**:
- Meal plan header (name, dates, status)
- Calendar view of planned meals
- Meal list view (alternative to calendar)
- Action buttons (activate, complete, edit, delete)
- Statistics panel (total meals, completion rate)

**Data Requirements**:
- Meal plan details with all meals
- Recipe information for assigned meals
- Nutritional summaries

### 2.3 Create/Edit Meal Plan
**Route**: `/meal-plans/new` or `/meal-plans/:id/edit`

**Purpose**: Create new or modify existing meal plan

**Components**:
- Form with name, start date, end date
- Meal slot configuration
- Recipe assignment interface
- Save/cancel buttons

**Validation**:
- Name required
- Valid date range
- End date after start date

### 2.4 Active Meal Plan Dashboard
**Route**: `/meal-plans/active` or `/dashboard`

**Purpose**: Quick view of current active meal plan

**Components**:
- Current week calendar
- Today's meals highlighted
- Quick add meal button
- Grocery list preview
- Nutrition summary
- Upcoming meals list

## 3. Components

### 3.1 MealPlanCard
**Purpose**: Display meal plan summary in list view

**Props**:
- `mealPlan`: MealPlan object
- `onActivate`: Function
- `onEdit`: Function
- `onDelete`: Function
- `onDuplicate`: Function

**Display**:
- Plan name
- Date range
- Status badge
- Meal count
- Progress indicator
- Action buttons

### 3.2 MealCalendar
**Purpose**: Interactive calendar for meal planning

**Props**:
- `mealPlan`: MealPlan object
- `meals`: Array of meals
- `onMealClick`: Function
- `onDateClick`: Function
- `onMealDrop`: Function (for drag-and-drop)

**Features**:
- Month/week/day views
- Drag-and-drop meal assignment
- Color-coded meal types
- Hover previews
- Click to view/edit meal

### 3.3 MealSlot
**Purpose**: Individual meal slot in calendar

**Props**:
- `meal`: Meal object
- `date`: Date
- `mealType`: MealType enum
- `onAssign`: Function
- `onRemove`: Function

**Display**:
- Recipe name or "Empty"
- Meal type icon
- Servings count
- Completion checkbox
- Quick actions menu

### 3.4 RecipeSelector
**Purpose**: Modal/drawer to select recipe for meal slot

**Props**:
- `onSelect`: Function
- `selectedRecipeId`: Guid (optional)
- `mealType`: MealType (for filtering)

**Features**:
- Search recipes
- Filter by category, dietary restrictions
- Show favorites
- Recipe preview
- Recently used recipes

### 3.5 MealPlanForm
**Purpose**: Form for creating/editing meal plan

**Fields**:
- Name (text input)
- Start date (date picker)
- End date (date picker)
- Description (textarea, optional)

**Validation**:
- Real-time validation
- Error messages
- Submit button state

### 3.6 MealPlanStats
**Purpose**: Display statistics for meal plan

**Metrics**:
- Total meals planned
- Completed meals
- Completion percentage
- Nutritional summary
- Cost estimate (if available)

### 3.7 MealTypeFilter
**Purpose**: Filter meals by type

**Options**:
- All meals
- Breakfast
- Lunch
- Dinner
- Snacks

### 3.8 QuickMealAdd
**Purpose**: Quick action to add meal to current date

**Features**:
- Floating action button or quick panel
- Select meal type
- Select recipe
- Auto-assign to today or next available slot

## 4. State Management

### 4.1 Meal Plan State
```typescript
interface MealPlanState {
  activePlan: MealPlan | null;
  plans: MealPlan[];
  selectedPlan: MealPlan | null;
  loading: boolean;
  error: string | null;
  filters: {
    status: MealPlanStatus | null;
    fromDate: Date | null;
    toDate: Date | null;
  };
  pagination: {
    page: number;
    pageSize: number;
    total: number;
  };
}
```

### 4.2 Actions
```typescript
// Async actions
fetchMealPlans()
fetchMealPlanById(id: string)
fetchActiveMealPlan()
createMealPlan(data: CreateMealPlanData)
updateMealPlan(id: string, data: UpdateMealPlanData)
activateMealPlan(id: string)
completeMealPlan(id: string)
deleteMealPlan(id: string)
assignMeal(planId: string, mealData: AssignMealData)
removeMeal(planId: string, mealId: string)

// Sync actions
setFilters(filters: MealPlanFilters)
setSelectedPlan(plan: MealPlan)
clearError()
```

## 5. API Integration

### 5.1 Service Layer
```typescript
class MealPlanService {
  async getMealPlans(filters?: MealPlanFilters): Promise<PagedResult<MealPlan>>
  async getMealPlanById(id: string): Promise<MealPlan>
  async getActiveMealPlan(): Promise<MealPlan | null>
  async createMealPlan(data: CreateMealPlanRequest): Promise<MealPlan>
  async updateMealPlan(id: string, data: UpdateMealPlanRequest): Promise<MealPlan>
  async activateMealPlan(id: string): Promise<void>
  async completeMealPlan(id: string): Promise<void>
  async deleteMealPlan(id: string): Promise<void>
  async assignMeal(planId: string, data: AssignMealRequest): Promise<Meal>
  async removeMeal(planId: string, mealId: string): Promise<void>
}
```

### 5.2 Request/Response Types
```typescript
interface CreateMealPlanRequest {
  name: string;
  startDate: string; // ISO date
  endDate: string; // ISO date
}

interface MealPlan {
  id: string;
  name: string;
  startDate: string;
  endDate: string;
  status: 'Draft' | 'Active' | 'Completed' | 'Archived';
  meals: Meal[];
  totalMeals: number;
  completedMeals: number;
}

interface Meal {
  id: string;
  recipeId?: string;
  recipeName?: string;
  mealType: 'Breakfast' | 'Lunch' | 'Dinner' | 'Snack';
  scheduledDate: string;
  servings: number;
  notes?: string;
  isCompleted: boolean;
}
```

## 6. User Interactions

### 6.1 Create Meal Plan Flow
1. Click "Create New Plan" button
2. Fill in plan details (name, dates)
3. Click "Save" or "Save & Add Meals"
4. If "Save & Add Meals", navigate to meal assignment view

### 6.2 Assign Meal Flow
1. Click on empty meal slot in calendar
2. Recipe selector modal opens
3. Search/browse recipes
4. Select recipe
5. Specify servings (default from recipe)
6. Add optional notes
7. Click "Assign"
8. Meal appears in calendar slot

### 6.3 Activate Meal Plan Flow
1. View meal plan details
2. Verify meals are assigned
3. Click "Activate" button
4. Confirmation dialog appears
5. Confirm activation
6. Previous active plan (if any) is deactivated
7. Grocery list is auto-generated
8. Success notification shown

### 6.4 Drag-and-Drop Meal Assignment
1. Open recipe panel or favorites
2. Drag recipe card
3. Drop on meal slot in calendar
4. Meal is auto-assigned
5. Visual feedback during drag

### 6.5 Complete Meal Plan Flow
1. View active meal plan
2. Click "Complete" button
3. Confirmation dialog with summary
4. Confirm completion
5. Plan status changes to Completed
6. Plan is archived

## 7. UI/UX Requirements

### 7.1 Calendar Views

**Month View**:
- Shows entire month
- Each day shows count of meals
- Click day to see meals
- Color indicators for meal types

**Week View**:
- Shows 7 days horizontally
- Meal slots for each day
- Drag-and-drop enabled
- Meal type columns

**Day View**:
- Shows single day
- All meal slots visible
- Detailed meal information
- Easy add/edit actions

### 7.2 Visual Design

**Color Coding**:
- Breakfast: Yellow/Orange
- Lunch: Green
- Dinner: Blue
- Snack: Purple
- Empty slot: Gray/Transparent

**Status Badges**:
- Draft: Gray
- Active: Green
- Completed: Blue
- Archived: Dark gray

**Icons**:
- Calendar icon for dates
- Checkmark for completed
- Plus icon for add actions
- Edit/trash icons for actions

### 7.3 Responsive Design

**Desktop** (>1200px):
- Full calendar view
- Sidebar with filters and stats
- Modal for recipe selection

**Tablet** (768px - 1200px):
- Condensed calendar view
- Collapsible sidebar
- Full-screen modals

**Mobile** (<768px):
- List view default
- Compact calendar option
- Bottom sheet for actions
- Swipe gestures

## 8. Validation and Error Handling

### 8.1 Form Validation
```typescript
const mealPlanValidation = {
  name: {
    required: true,
    maxLength: 200,
    message: 'Plan name is required (max 200 characters)'
  },
  startDate: {
    required: true,
    minDate: new Date(),
    message: 'Start date cannot be in the past'
  },
  endDate: {
    required: true,
    afterField: 'startDate',
    message: 'End date must be after start date'
  }
};
```

### 8.2 Error Messages
- "Failed to load meal plans. Please try again."
- "Cannot activate meal plan without meals."
- "You already have an active meal plan."
- "Failed to assign meal. Recipe may no longer exist."
- "Cannot modify completed meal plan."

### 8.3 Loading States
- Skeleton screens for calendar
- Spinner for form submissions
- Optimistic UI updates
- Error boundaries

## 9. Notifications

### 9.1 Success Messages
- "Meal plan created successfully"
- "Meal plan activated"
- "Meal assigned successfully"
- "Meal plan completed"

### 9.2 Toast Notifications
- Position: Top-right
- Duration: 3-5 seconds
- Auto-dismiss
- Action buttons (Undo, View)

## 10. Accessibility

### 10.1 ARIA Labels
- Calendar navigation buttons
- Meal slots with descriptive labels
- Form inputs with proper labels
- Action buttons with clear text

### 10.2 Keyboard Navigation
- Tab through meal slots
- Arrow keys for calendar navigation
- Enter to select/activate
- Escape to close modals

### 10.3 Screen Reader Support
- Announce meal assignments
- Read calendar navigation
- Form validation messages
- Status changes

## 11. Performance Optimization

### 11.1 Data Fetching
- Lazy load meal plans
- Paginated lists
- Cache active meal plan
- Debounced search

### 11.2 Rendering
- Virtual scrolling for long lists
- Memoized components
- Lazy load calendar events
- Optimized re-renders

## 12. Testing Requirements

### 12.1 Unit Tests
- Component rendering
- User interactions
- Form validation
- State management

### 12.2 Integration Tests
- API integration
- Calendar interactions
- Drag-and-drop functionality
- Navigation flows

### 12.3 E2E Tests
1. Create meal plan end-to-end
2. Activate meal plan
3. Assign meals via drag-and-drop
4. Complete meal plan
5. Filter and search meal plans

## 13. Analytics Events

### 13.1 Track Events
- `meal_plan_created`
- `meal_plan_activated`
- `meal_assigned`
- `meal_plan_completed`
- `calendar_view_changed`
- `recipe_searched_from_planner`

### 13.2 Event Properties
- User ID
- Plan ID
- Action timestamp
- View type (calendar/list)
- Source (drag-drop, click, quick-add)

## 14. Offline Support

### 14.1 Capabilities
- View cached meal plans
- Queue meal assignments
- Sync when online
- Offline indicator

### 14.2 Sync Strategy
- Background sync for updates
- Conflict resolution
- Last-write-wins for simple fields
- Manual resolution for complex conflicts
