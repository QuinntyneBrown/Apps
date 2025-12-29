# Backend Requirements - Bucket List Management

## Domain Events
- BucketListItemAdded
- ItemDetailEnriched
- ItemPriorityChanged
- ItemCompleted
- ItemRemoved

## API Endpoints
- POST /api/bucket-list/items - Create bucket list item
- PUT /api/bucket-list/items/{id} - Update item details
- PUT /api/bucket-list/items/{id}/priority - Change priority
- POST /api/bucket-list/items/{id}/complete - Mark as completed
- DELETE /api/bucket-list/items/{id} - Remove item
- GET /api/bucket-list/items - List items with filters

## Data Models
```typescript
BucketListItem {
  id: UUID,
  userId: UUID,
  title: string,
  description: string,
  category: string,
  priority: 'low' | 'medium' | 'high',
  inspirationSource: string,
  whyImportant: string,
  idealTiming: string,
  shareWith: string[],
  status: 'wishlist' | 'planning' | 'in-progress' | 'completed' | 'removed',
  completionDate: DateTime,
  satisfactionRating: number,
  createdAt: DateTime
}
```

## Business Logic
- Validate item titles are unique per user
- Track all priority changes in audit log
- Require satisfaction rating (1-5) on completion
- Store removal reasons for analytics
- Auto-categorize based on keywords
