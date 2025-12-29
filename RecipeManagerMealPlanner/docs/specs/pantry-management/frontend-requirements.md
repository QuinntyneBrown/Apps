# Frontend Requirements - Pantry Management

## Pages/Views

### 1. Pantry Inventory Page (`/pantry`)

**Components**:
- PantryHeader: Add item, search, filter by location
- ExpirationAlerts: Banner with expiring items
- LowStockAlerts: Items needing restock
- PantryItemList: Searchable list with quantities
- LocationTabs: Pantry, Fridge, Freezer, Other
- QuickAddButton: Scan barcode or manual entry

**Features**:
- Search pantry items
- Filter by location, category, expiration
- Sort by expiration date, alphabetical, quantity
- Quick add to shopping list
- Edit quantities inline
- Log usage when cooking
- Expiration alerts with recipe suggestions
- Barcode scanning for quick add (mobile)

**API Calls**:
- GET /api/pantry/items
- GET /api/pantry/expiring
- POST /api/pantry/items
- PUT /api/pantry/items/{id}/use

### 2. Add Pantry Item Dialog

**Components**:
- IngredientAutocomplete: Search existing ingredients
- QuantityInput: Amount and unit
- ExpirationDatePicker: Optional expiration
- LocationSelector: Where stored
- CostInput: Optional purchase cost

**Features**:
- Autocomplete from known ingredients
- Quick add from shopping list
- Set minimum stock threshold
- Bulk add from shopping trip

## User Workflows

### Add from Shopping Trip
1. Complete shopping trip
2. System prompts to add to pantry
3. Review purchased items
4. Set expiration dates
5. Bulk add to pantry
6. Inventory updated

### Use Pantry Item
1. Log meal as cooked
2. System auto-deducts ingredients
3. Pantry quantities updated
4. Low stock alert if below threshold

## State Management
- `pantrySlice`: Inventory items and alerts
- Real-time updates for shared household
