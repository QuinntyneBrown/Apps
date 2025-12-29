# Frontend Requirements - Grocery Shopping

## Pages/Views

### 1. Shopping List Page (`/grocery`)

**Components**:
- ShoppingListHeader: Generate from meal plans, add manual item
- CategorySections: Items grouped by store category (Produce, Dairy, Meat, etc.)
- ShoppingListItem: Checkbox, item name, quantity, needed by date
- CostEstimate: Running total and estimated cost
- CompleteShoppingButton: Finish trip

**Features**:
- Check items as purchased
- Auto-organize by store layout
- Show which recipes need each item
- Edit quantities on the fly
- Add manual items not from recipes
- Multiple list views: By category, by recipe, flat list
- Offline support for in-store use
- Share list with family members
- Print-friendly version

**API Calls**:
- GET /api/grocery-lists
- POST /api/grocery-lists (generate from meal plans)
- PUT /api/grocery-lists/{id}/items/{itemId}/purchase
- POST /api/grocery-lists/{id}/complete

### 2. Generate List Dialog

**Components**:
- DateRangePicker: Select meal plan date range
- PantryCheckToggle: Exclude items already owned
- StoreSelector: Organize by store layout
- PreviewList: Show items before generating

**Features**:
- Auto-select next 7 days by default
- Check pantry and exclude owned items
- Consolidate duplicate ingredients
- Show estimated cost based on price history

## Shared Components

### CheckableListItem
Item with checkbox, strike-through on check, undo option

### CategoryAccordion
Collapsible sections for each store category

## User Workflows

### Generate Shopping List
1. Click "Generate List"
2. Select date range (default next 7 days)
3. Toggle "Check pantry" (exclude owned items)
4. Preview list
5. Confirm
6. List generated with all ingredients organized
7. Start shopping

### Shop with List
1. Open shopping list (works offline)
2. Check items as added to cart
3. Edit quantities if different
4. Add manual items if needed
5. View running total
6. Complete shopping
7. Pantry auto-updated

## State Management
- `shoppingListSlice`: Active lists and items
- Offline sync with IndexedDB

## Performance
- Offline-first architecture
- Background sync when online
- Optimistic UI updates
