# Collaboration - Frontend Requirements

## Overview
Provide real-time collaboration features for shared shopping lists with suggestions, presence, and live updates.

## User Stories

### US-CO-001: Suggest Items
**As a** shared user **I want to** suggest items for the list **So that** others can approve and add them

**Acceptance Criteria:**
- Suggest item button visible on shared lists
- Suggestion form with item details and reason
- View pending suggestions
- See who made each suggestion
- Approve/reject buttons for list owners

### US-CO-002: Real-time Updates
**As a** user **I want to** see changes made by others instantly **So that** I have the most current list

**Acceptance Criteria:**
- See items added by others in real-time
- See items checked off by others instantly
- Visual indicator for changes (highlight/animation)
- Show who made each change
- No page refresh needed

### US-CO-003: User Presence
**As a** user **I want to** see who else is viewing the list **So that** I can coordinate shopping

**Acceptance Criteria:**
- Avatar/name display of active users
- "Currently viewing" indicator
- See when others are typing/adding items
- Last seen timestamps for offline users

## UI Components

### SuggestionCard Component
```javascript
{
  suggestionId, itemName, suggestedBy,
  quantity, reason, suggestedAt,
  onApprove, onReject
}
```

### PresenceIndicator Component
- Active user avatars
- "X users viewing" count
- Typing indicators
- Last activity timestamps

### RealtimeToast Component
- Notification of remote changes
- User who made the change
- Action performed
- Undo option (if applicable)

## State Management

```javascript
{
  collaboration: {
    suggestions: {
      byListId: {
        'list-1': [...]
      },
      pending: 3
    },
    presence: {
      activeUsers: [...],
      typingUsers: [...]
    },
    realtimeUpdates: {
      connected: true,
      lastSync: datetime
    }
  }
}
```

## WebSocket Integration

- Connect to `/ws/lists/{listId}` on list open
- Handle incoming messages:
  - `item.added`: Add item to local state
  - `item.purchased`: Update item status
  - `user.joined`: Add to presence
  - `user.left`: Remove from presence
- Send outgoing messages:
  - User actions (add, update, remove items)
  - Typing indicators
  - Presence heartbeat

## Offline/Conflict Handling

- Queue changes when offline
- Show offline indicator
- Sync on reconnection
- Conflict resolution: show merge dialog if needed
- Optimistic updates with rollback on error

## Notifications

- "Alice suggested: Apples (2 lbs)"
- "Bob added Milk to the list"
- "Charlie purchased 5 items"
- "List completed by Alice"
