# Sharing - Frontend Requirements

## Components

### Share Dialog
- Content selector (photo/album)
- Link generation options
- Expiration date picker
- Password protection toggle
- View limit input
- Copy link button
- Social sharing buttons

### Shared With Me View
- List of shared albums
- Contributor status
- Access level indicator
- Collaboration options

### Collaborator Management
- Invite collaborators
- Set permissions
- Remove collaborators
- View activity

## User Workflows

### Share Album Workflow
1. User selects album
2. Clicks "Share"
3. Sets sharing options
4. Generates link
5. Copies or sends link

### Collaborate on Album Workflow
1. User receives invitation
2. Accepts invitation
3. Opens shared album
4. Can view/contribute based on permissions
5. Adds photos if contributor

## State Management

```typescript
interface ShareState {
  shareLinks: ShareLink[];
  collaborators: Collaborator[];
  sharedWithMe: SharedAlbum[];
}

interface ShareLink {
  id: string;
  contentType: 'Photo' | 'Album';
  contentId: string;
  url: string;
  expiresAt?: Date;
  viewCount: number;
  viewLimit?: number;
}
```
