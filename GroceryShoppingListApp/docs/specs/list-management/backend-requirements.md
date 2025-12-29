# List Management - Backend Requirements

## Overview
The List Management feature enables users to create, share, complete, and archive shopping lists. This forms the core foundation of the GroceryShoppingListApp.

## Domain Events

### ShoppingListCreated
**Trigger:** When a user creates a new shopping list
**Payload:**
```json
{
  "listId": "uuid",
  "userId": "uuid",
  "listName": "string",
  "createdAt": "datetime",
  "tags": ["string"],
  "isRecurring": "boolean",
  "recurringPattern": "string?"
}
```

### ListShared
**Trigger:** When a list owner shares a list with other users
**Payload:**
```json
{
  "listId": "uuid",
  "ownerId": "uuid",
  "sharedWithUserIds": ["uuid"],
  "sharedAt": "datetime",
  "permissionLevel": "view | edit | admin"
}
```

### ListCompleted
**Trigger:** When all items in a list are purchased
**Payload:**
```json
{
  "listId": "uuid",
  "userId": "uuid",
  "completedAt": "datetime",
  "totalItems": "integer",
  "totalSpent": "decimal"
}
```

### ListArchived
**Trigger:** When a user archives a completed list
**Payload:**
```json
{
  "listId": "uuid",
  "userId": "uuid",
  "archivedAt": "datetime",
  "reason": "string?"
}
```

## API Endpoints

### POST /api/shopping-lists
Create a new shopping list
- **Request Body:**
  ```json
  {
    "name": "string",
    "tags": ["string"],
    "isRecurring": "boolean",
    "recurringPattern": "daily | weekly | monthly"
  }
  ```
- **Response:** 201 Created
  ```json
  {
    "listId": "uuid",
    "name": "string",
    "ownerId": "uuid",
    "createdAt": "datetime"
  }
  ```

### GET /api/shopping-lists
Retrieve all shopping lists for the authenticated user
- **Query Parameters:**
  - `status`: active | completed | archived
  - `page`: integer
  - `limit`: integer
- **Response:** 200 OK
  ```json
  {
    "lists": [
      {
        "listId": "uuid",
        "name": "string",
        "ownerId": "uuid",
        "itemCount": "integer",
        "status": "string",
        "createdAt": "datetime"
      }
    ],
    "total": "integer",
    "page": "integer",
    "limit": "integer"
  }
  ```

### GET /api/shopping-lists/{listId}
Retrieve a specific shopping list
- **Response:** 200 OK
  ```json
  {
    "listId": "uuid",
    "name": "string",
    "ownerId": "uuid",
    "status": "active | completed | archived",
    "tags": ["string"],
    "isRecurring": "boolean",
    "recurringPattern": "string?",
    "sharedWith": [
      {
        "userId": "uuid",
        "userName": "string",
        "permissionLevel": "string"
      }
    ],
    "createdAt": "datetime",
    "updatedAt": "datetime"
  }
  ```

### PUT /api/shopping-lists/{listId}
Update shopping list details
- **Request Body:**
  ```json
  {
    "name": "string",
    "tags": ["string"]
  }
  ```
- **Response:** 200 OK

### POST /api/shopping-lists/{listId}/share
Share a shopping list with other users
- **Request Body:**
  ```json
  {
    "userIds": ["uuid"],
    "permissionLevel": "view | edit | admin"
  }
  ```
- **Response:** 200 OK

### POST /api/shopping-lists/{listId}/complete
Mark a shopping list as completed
- **Response:** 200 OK

### POST /api/shopping-lists/{listId}/archive
Archive a shopping list
- **Request Body:**
  ```json
  {
    "reason": "string?"
  }
  ```
- **Response:** 200 OK

### DELETE /api/shopping-lists/{listId}
Permanently delete a shopping list
- **Response:** 204 No Content

## Database Schema

### shopping_lists Table
```sql
CREATE TABLE shopping_lists (
    list_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    owner_id UUID NOT NULL REFERENCES users(user_id),
    name VARCHAR(255) NOT NULL,
    status VARCHAR(50) NOT NULL DEFAULT 'active',
    tags TEXT[],
    is_recurring BOOLEAN DEFAULT FALSE,
    recurring_pattern VARCHAR(50),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    completed_at TIMESTAMP,
    archived_at TIMESTAMP,
    total_spent DECIMAL(10, 2)
);

CREATE INDEX idx_shopping_lists_owner ON shopping_lists(owner_id);
CREATE INDEX idx_shopping_lists_status ON shopping_lists(status);
```

### list_shares Table
```sql
CREATE TABLE list_shares (
    share_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    list_id UUID NOT NULL REFERENCES shopping_lists(list_id) ON DELETE CASCADE,
    user_id UUID NOT NULL REFERENCES users(user_id),
    permission_level VARCHAR(50) NOT NULL,
    shared_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(list_id, user_id)
);

CREATE INDEX idx_list_shares_user ON list_shares(user_id);
CREATE INDEX idx_list_shares_list ON list_shares(list_id);
```

## Business Rules

1. **List Creation**
   - List name is required and must be between 1-255 characters
   - A user can create unlimited lists
   - Tags are optional and can be used for categorization

2. **List Sharing**
   - Only the list owner can share a list
   - Permission levels: view (read-only), edit (add/remove items), admin (share/delete)
   - Users can be removed from shared lists by the owner

3. **List Completion**
   - A list is automatically marked completed when all items are purchased
   - Users can manually mark a list as completed
   - Completed lists can be reopened if needed

4. **List Archiving**
   - Only completed lists can be archived
   - Archived lists are hidden from the main view but can be accessed via filters
   - Archived lists can be unarchived

5. **Recurring Lists**
   - Recurring lists automatically create a new instance based on the pattern
   - New instances copy all items from the template list
   - Previous instances are automatically archived

## Security Requirements

- Users can only view/edit lists they own or have been shared with
- API endpoints must validate user permissions before allowing operations
- List sharing requires the recipient user to have an active account
- Soft delete option for lists to enable recovery

## Performance Considerations

- Implement pagination for list retrieval (default 20 items per page)
- Cache frequently accessed lists
- Use database indexes on owner_id and status fields
- Consider archiving old lists automatically after 90 days of inactivity

## Integration Points

- **User Service:** Validate user existence for sharing
- **Item Management:** Lists contain items (one-to-many relationship)
- **Budget Management:** Lists have associated budgets
- **Notification Service:** Send notifications when lists are shared or completed
