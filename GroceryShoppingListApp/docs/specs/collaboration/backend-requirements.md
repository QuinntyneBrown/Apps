# Collaboration - Backend Requirements

## Overview
The Collaboration feature enables real-time sharing, suggestions, and synchronization of shopping lists among multiple users.

## Domain Events

### ItemSuggestionMade
**Trigger:** When a user suggests an item to add to a shared list
**Payload:**
```json
{
  "suggestionId": "uuid",
  "listId": "uuid",
  "suggestedBy": "uuid",
  "itemName": "string",
  "quantity": "integer",
  "unit": "string",
  "reason": "string",
  "suggestedAt": "datetime"
}
```

### ItemSuggestionApproved
**Trigger:** When a suggestion is approved and added to the list
**Payload:**
```json
{
  "suggestionId": "uuid",
  "listId": "uuid",
  "approvedBy": "uuid",
  "itemId": "uuid",
  "approvedAt": "datetime"
}
```

### RealTimeUpdateSynced
**Trigger:** When any change to a shared list is synced to all users
**Payload:**
```json
{
  "listId": "uuid",
  "updateType": "item_added | item_removed | item_purchased | list_updated",
  "updatedBy": "uuid",
  "changeData": "object",
  "syncedAt": "datetime"
}
```

## API Endpoints

### POST /api/lists/{listId}/suggestions
Create an item suggestion
- **Request Body:**
  ```json
  {
    "itemName": "string",
    "quantity": "integer",
    "unit": "string",
    "reason": "string"
  }
  ```
- **Response:** 201 Created

### GET /api/lists/{listId}/suggestions
Get all pending suggestions for a list
- **Response:** 200 OK
  ```json
  {
    "suggestions": [
      {
        "suggestionId": "uuid",
        "itemName": "string",
        "suggestedBy": {
          "userId": "uuid",
          "userName": "string"
        },
        "quantity": "integer",
        "reason": "string",
        "suggestedAt": "datetime"
      }
    ]
  }
  ```

### POST /api/suggestions/{suggestionId}/approve
Approve a suggestion
- **Response:** 200 OK

### DELETE /api/suggestions/{suggestionId}
Reject a suggestion
- **Response:** 204 No Content

### WebSocket /ws/lists/{listId}
Real-time updates for a shopping list
- **Messages:**
  - `item.added`
  - `item.updated`
  - `item.purchased`
  - `item.removed`
  - `list.updated`
  - `user.joined`
  - `user.left`

## Database Schema

### item_suggestions Table
```sql
CREATE TABLE item_suggestions (
    suggestion_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    list_id UUID NOT NULL REFERENCES shopping_lists(list_id) ON DELETE CASCADE,
    suggested_by UUID NOT NULL REFERENCES users(user_id),
    item_name VARCHAR(255) NOT NULL,
    quantity INTEGER DEFAULT 1,
    unit VARCHAR(50),
    reason TEXT,
    status VARCHAR(50) DEFAULT 'pending',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    approved_by UUID REFERENCES users(user_id),
    approved_at TIMESTAMP
);

CREATE INDEX idx_suggestions_list ON item_suggestions(list_id);
CREATE INDEX idx_suggestions_status ON item_suggestions(status);
```

### realtime_activity Table
```sql
CREATE TABLE realtime_activity (
    activity_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    list_id UUID NOT NULL REFERENCES shopping_lists(list_id) ON DELETE CASCADE,
    user_id UUID NOT NULL REFERENCES users(user_id),
    activity_type VARCHAR(100) NOT NULL,
    activity_data JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_activity_list ON realtime_activity(list_id);
CREATE INDEX idx_activity_created ON realtime_activity(created_at);
```

## Business Rules

1. **Suggestions**
   - Any shared user can make suggestions
   - Only list owner or admin can approve/reject
   - Approved suggestions automatically become items
   - Rejected suggestions are soft-deleted

2. **Real-time Sync**
   - All changes broadcast via WebSocket
   - Include user information with changes
   - Conflict resolution: last-write-wins
   - Offline changes queued and synced on reconnect

3. **Presence**
   - Track which users are currently viewing list
   - Show "typing" indicators for item additions
   - Display last seen timestamps

4. **Notifications**
   - Notify on new suggestions
   - Notify on item additions/purchases by others
   - Notify on list completion
   - Configurable notification preferences

## Integration Points

- **List Management:** Share and permission management
- **Item Management:** Item changes trigger sync
- **Notification Service:** Real-time push notifications
- **WebSocket Service:** Bi-directional communication
