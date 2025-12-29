# Backend Requirements - Gift Ideas

## Domain Events
- GiftIdeaAdded
- GiftIdeaShared
- GiftIdeaClaimed
- GiftIdeaRejected

## API Endpoints
- POST /api/gift-ideas - Add gift idea
- GET /api/gift-ideas - List ideas with filters
- POST /api/gift-ideas/{id}/share - Share idea
- POST /api/gift-ideas/{id}/claim - Claim idea
- DELETE /api/gift-ideas/{id} - Reject/remove idea

## Data Models
```typescript
GiftIdea {
  id: UUID,
  recipientId: UUID,
  description: string,
  category: string,
  priceRange: {min, max},
  whereToBuy: string,
  priority: 'low' | 'medium' | 'high',
  source: string,
  claimedBy: UUID,
  status: 'available' | 'claimed' | 'purchased' | 'rejected'
}
```

## Business Logic
- Prevent duplicate purchases through claiming
- Track idea sources for attribution
- Auto-prioritize based on upcoming occasions
