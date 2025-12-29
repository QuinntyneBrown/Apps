# Item Management - Backend Requirements

## Overview
The Item Management feature enables users to add, update, mark as purchased, and remove items from their shopping lists. This is a core feature that works in conjunction with List Management.

## Domain Events

### ItemAddedToList
**Trigger:** When a user adds an item to a shopping list
**Payload:**
```json
{
  "itemId": "uuid",
  "listId": "uuid",
  "userId": "uuid",
  "itemName": "string",
  "quantity": "integer",
  "unit": "string",
  "category": "string",
  "estimatedPrice": "decimal?",
  "addedAt": "datetime"
}
```

### ItemQuantityUpdated
**Trigger:** When a user changes the quantity of an item
**Payload:**
```json
{
  "itemId": "uuid",
  "listId": "uuid",
  "userId": "uuid",
  "oldQuantity": "integer",
  "newQuantity": "integer",
  "updatedAt": "datetime"
}
```

### ItemMarkedPurchased
**Trigger:** When a user marks an item as purchased
**Payload:**
```json
{
  "itemId": "uuid",
  "listId": "uuid",
  "userId": "uuid",
  "purchasedAt": "datetime",
  "actualPrice": "decimal",
  "storeName": "string?",
  "purchasedQuantity": "integer"
}
```

### ItemMarkedUnavailable
**Trigger:** When a user marks an item as unavailable at a store
**Payload:**
```json
{
  "itemId": "uuid",
  "listId": "uuid",
  "userId": "uuid",
  "storeName": "string",
  "markedAt": "datetime",
  "reason": "string?"
}
```

### ItemRemoved
**Trigger:** When a user removes an item from a list
**Payload:**
```json
{
  "itemId": "uuid",
  "listId": "uuid",
  "userId": "uuid",
  "removedAt": "datetime",
  "reason": "string?"
}
```

## API Endpoints

### POST /api/shopping-lists/{listId}/items
Add an item to a shopping list
- **Request Body:**
  ```json
  {
    "name": "string",
    "quantity": "integer",
    "unit": "string",
    "category": "string",
    "estimatedPrice": "decimal?",
    "notes": "string?"
  }
  ```
- **Response:** 201 Created
  ```json
  {
    "itemId": "uuid",
    "listId": "uuid",
    "name": "string",
    "quantity": "integer",
    "unit": "string",
    "addedAt": "datetime"
  }
  ```

### GET /api/shopping-lists/{listId}/items
Retrieve all items in a shopping list
- **Query Parameters:**
  - `status`: all | pending | purchased | unavailable
  - `category`: string
- **Response:** 200 OK
  ```json
  {
    "items": [
      {
        "itemId": "uuid",
        "name": "string",
        "quantity": "integer",
        "unit": "string",
        "category": "string",
        "status": "pending | purchased | unavailable",
        "estimatedPrice": "decimal",
        "actualPrice": "decimal?",
        "addedAt": "datetime",
        "purchasedAt": "datetime?"
      }
    ]
  }
  ```

### PUT /api/shopping-lists/{listId}/items/{itemId}
Update an item's details
- **Request Body:**
  ```json
  {
    "name": "string",
    "quantity": "integer",
    "unit": "string",
    "category": "string",
    "estimatedPrice": "decimal",
    "notes": "string"
  }
  ```
- **Response:** 200 OK

### PATCH /api/shopping-lists/{listId}/items/{itemId}/quantity
Update item quantity
- **Request Body:**
  ```json
  {
    "quantity": "integer"
  }
  ```
- **Response:** 200 OK

### POST /api/shopping-lists/{listId}/items/{itemId}/purchase
Mark item as purchased
- **Request Body:**
  ```json
  {
    "actualPrice": "decimal",
    "purchasedQuantity": "integer",
    "storeName": "string?"
  }
  ```
- **Response:** 200 OK

### POST /api/shopping-lists/{listId}/items/{itemId}/unavailable
Mark item as unavailable
- **Request Body:**
  ```json
  {
    "storeName": "string",
    "reason": "string?"
  }
  ```
- **Response:** 200 OK

### POST /api/shopping-lists/{listId}/items/{itemId}/undo-purchase
Undo a purchase (uncheck item)
- **Response:** 200 OK

### DELETE /api/shopping-lists/{listId}/items/{itemId}
Remove an item from the list
- **Request Body (optional):**
  ```json
  {
    "reason": "string?"
  }
  ```
- **Response:** 204 No Content

### POST /api/shopping-lists/{listId}/items/bulk
Add multiple items at once
- **Request Body:**
  ```json
  {
    "items": [
      {
        "name": "string",
        "quantity": "integer",
        "unit": "string",
        "category": "string"
      }
    ]
  }
  ```
- **Response:** 201 Created

### GET /api/items/suggestions
Get item suggestions based on history
- **Query Parameters:**
  - `query`: string (search term)
  - `limit`: integer (default 10)
- **Response:** 200 OK
  ```json
  {
    "suggestions": [
      {
        "name": "string",
        "category": "string",
        "averagePrice": "decimal",
        "commonUnit": "string",
        "frequency": "integer"
      }
    ]
  }
  ```

## Database Schema

### shopping_list_items Table
```sql
CREATE TABLE shopping_list_items (
    item_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    list_id UUID NOT NULL REFERENCES shopping_lists(list_id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    quantity INTEGER NOT NULL DEFAULT 1,
    unit VARCHAR(50),
    category VARCHAR(100),
    status VARCHAR(50) NOT NULL DEFAULT 'pending',
    estimated_price DECIMAL(10, 2),
    actual_price DECIMAL(10, 2),
    notes TEXT,
    store_name VARCHAR(255),
    added_by UUID NOT NULL REFERENCES users(user_id),
    added_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    purchased_at TIMESTAMP,
    purchased_by UUID REFERENCES users(user_id),
    purchased_quantity INTEGER,
    sort_order INTEGER,
    is_unavailable BOOLEAN DEFAULT FALSE,
    unavailable_reason TEXT
);

CREATE INDEX idx_items_list ON shopping_list_items(list_id);
CREATE INDEX idx_items_status ON shopping_list_items(status);
CREATE INDEX idx_items_category ON shopping_list_items(category);
CREATE INDEX idx_items_added_by ON shopping_list_items(added_by);
```

### item_history Table
```sql
CREATE TABLE item_history (
    history_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(user_id),
    item_name VARCHAR(255) NOT NULL,
    category VARCHAR(100),
    unit VARCHAR(50),
    price_paid DECIMAL(10, 2),
    store_name VARCHAR(255),
    purchased_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_item_history_user ON item_history(user_id);
CREATE INDEX idx_item_history_name ON item_history(item_name);
```

### item_categories Table
```sql
CREATE TABLE item_categories (
    category_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(100) UNIQUE NOT NULL,
    icon VARCHAR(50),
    color VARCHAR(20),
    sort_order INTEGER
);

-- Seed common categories
INSERT INTO item_categories (name, icon, sort_order) VALUES
('Produce', '🥬', 1),
('Dairy', '🥛', 2),
('Meat & Seafood', '🥩', 3),
('Bakery', '🍞', 4),
('Pantry', '🥫', 5),
('Frozen', '🧊', 6),
('Beverages', '🥤', 7),
('Snacks', '🍿', 8),
('Personal Care', '🧴', 9),
('Household', '🧹', 10),
('Other', '📦', 11);
```

## Business Rules

1. **Item Addition**
   - Item name is required (1-255 characters)
   - Quantity must be positive integer (default: 1)
   - Unit is optional (e.g., lbs, oz, count, box)
   - Category is optional but recommended
   - Duplicate items in the same list should increment quantity instead of creating new entry

2. **Item Quantity**
   - Quantity must be >= 1
   - When quantity is updated to 0, item is removed
   - Partial purchases are supported (purchased_quantity can be less than quantity)

3. **Item Purchase**
   - Only items with status "pending" can be marked purchased
   - Actual price is optional but recommended for price tracking
   - Purchase can be undone (status reverts to pending)
   - Multiple users can mark the same item as purchased (collaborative shopping)

4. **Item Status**
   - pending: Item not yet purchased
   - purchased: Item has been purchased
   - unavailable: Item marked as not available at store
   - Items can transition: pending → purchased → pending (undo)
   - Items can transition: pending → unavailable → pending

5. **Item Suggestions**
   - Based on user's purchase history
   - Suggests common items, categories, and typical prices
   - Autocomplete when typing item names
   - Learn from other users (anonymized aggregate data)

6. **Item Ordering**
   - Items can be manually reordered within a list
   - Default sort: by category, then by add date
   - Alternative sorts: alphabetical, by status, by price

7. **Permissions**
   - View permission: Can view items
   - Edit permission: Can add, update, remove items
   - Admin permission: Full access including bulk operations

## Security Requirements

- Users can only access items in lists they own or have been shared with
- Item modifications require appropriate permission level
- Validate list ownership before allowing item operations
- Rate limiting on bulk item operations

## Performance Considerations

- Implement pagination for lists with many items (50+ items)
- Cache frequently accessed item suggestions
- Batch update operations for better performance
- Index on list_id, status, and category for fast queries
- Optimize for mobile: minimize API calls, support offline mode

## Integration Points

- **List Management:** Items belong to lists (one-to-many relationship)
- **Price Tracking:** Item purchases feed into price history
- **Budget Management:** Item actual prices contribute to budget tracking
- **Collaboration:** Real-time sync when multiple users modify items
- **Notification Service:** Notify when items are added/purchased in shared lists

## Edge Cases

1. **Concurrent Updates:** Handle multiple users editing the same item simultaneously
2. **List Completion:** Automatically mark list as complete when all items are purchased
3. **List Reopening:** When list is reopened, keep purchased items but reset their status
4. **Item Deletion:** Soft delete to preserve history and analytics
5. **Duplicate Detection:** Prompt user when adding item that already exists in list
6. **Unavailable Items:** Allow marking as unavailable without removing from list
