# Frontend Requirements - Sharing and Collaboration

## Key Components

### Recommend Modal
- Material selector
- Recipient picker
- Reason text area
- Send button

### Shared Lists
- List browser
- Follow/unfollow lists
- Add to my reading list

### Discussion Thread
- Comment thread
- Reply functionality
- Like/upvote
- Notification system

## State Management
```typescript
interface SharingState {
  recommendations: Recommendation[];
  sharedLists: ReadingList[];
  discussions: Discussion[];
}
```
