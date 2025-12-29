# Grocery Lists - Frontend Requirements

## 1. Overview

The Grocery Lists frontend provides an intuitive shopping list interface with category grouping, purchase tracking, and mobile-optimized design for in-store use.

## 2. Pages and Routes

### 2.1 Grocery Lists Dashboard
**Route**: `/grocery-lists`
- View all grocery lists
- Active, completed, archived tabs
- Generate from meal plan button
- Create manual list

### 2.2 Grocery List Details
**Route**: `/grocery-lists/:id`
- Categorized item list
- Checkboxes for purchased items
- Progress indicator
- Add item form
- Export/share options

### 2.3 Shopping Mode
**Route**: `/grocery-lists/:id/shop`
- Full-screen, mobile-optimized
- Large checkboxes
- Category navigation
- Keep screen awake
- Minimal distractions

## 3. Components

### 3.1 GroceryListCard
- List name and date
- Item count (purchased/total)
- Progress bar
- Quick actions

### 3.2 CategorySection
- Category header (collapsible)
- Items in category
- Category icon
- Item count

### 3.3 GroceryItemRow
- Checkbox for purchase status
- Item name and quantity
- Notes display
- Swipe to delete (mobile)

### 3.4 AddItemForm
- Quick add input
- Quantity and unit fields
- Category selector
- Save button

### 3.5 ProgressHeader
- Total items vs purchased
- Progress bar
- Estimated cost
- Complete button

## 4. State Management

```typescript
interface GroceryListState {
  lists: GroceryList[];
  activeList: GroceryList | null;
  loading: boolean;
  filter: 'active' | 'completed' | 'archived';
}

interface GroceryList {
  id: string;
  name: string;
  generatedDate: string;
  status: string;
  items: GroceryItem[];
  totalItems: number;
  purchasedItems: number;
  estimatedCost?: number;
}

interface GroceryItem {
  id: string;
  name: string;
  quantity: number;
  unit: string;
  category: string;
  isPurchased: boolean;
  notes?: string;
}
```

## 5. User Interactions

### 5.1 Generate from Meal Plan
1. Click "Generate from Meal Plan"
2. Select meal plan
3. Configure options (consolidate, optional ingredients)
4. Review generated list
5. Edit items if needed
6. Save list

### 5.2 Shopping Flow
1. Open grocery list
2. Enter shopping mode
3. Navigate to first category
4. Check off items as purchased
5. Move to next category
6. Complete when done

### 5.3 Add Manual Item
1. Type item name
2. Enter quantity/unit
3. Select category
4. Click add
5. Item appears in list

## 6. UI/UX Features

### 6.1 Category Organization
- Produce
- Meat & Seafood
- Dairy & Eggs
- Bakery
- Pantry
- Frozen
- Beverages
- Other

### 6.2 Visual Design
- Strikethrough for purchased items
- Green checkmarks
- Category color coding
- Progress visualization

### 6.3 Mobile Optimizations
- Large tap targets
- Swipe gestures
- Offline support
- Keep screen awake in shopping mode

### 6.4 Smart Features
- Auto-suggest items
- Recently purchased items
- Price tracking
- Store layout optimization

## 7. Offline Support

- Cache active grocery lists
- Queue purchases offline
- Sync when connection restored
- Indicate sync status

## 8. Export Options

- Print list
- Share via text/email
- Export to notes app
- Copy to clipboard

## 9. Accessibility

- Screen reader support
- Keyboard navigation
- High contrast mode
- Voice input for adding items
