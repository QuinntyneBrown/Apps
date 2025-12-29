# Sharing - Backend Requirements

## Overview
Manages photo and album sharing with family members and external users through shareable links and collaboration.

## Domain Model

### ShareLink Aggregate
- **LinkId**: Unique identifier (Guid)
- **ContentType**: Photo or Album (enum)
- **ContentId**: Reference to photo or album (Guid)
- **LinkUrl**: Shareable URL (string)
- **CreatedBy**: User who created link (Guid)
- **CreationDate**: When link was created (DateTime)
- **ExpirationDate**: When link expires (DateTime, nullable)
- **Password**: Optional password protection (string, hashed)
- **ViewLimit**: Max number of views (int, nullable)
- **ViewCount**: Current view count (int)
- **IsActive**: Whether link is active (bool)

### Collaborator Model
- **CollaboratorId**: Unique identifier (Guid)
- **AlbumId**: Album being collaborated on (Guid)
- **UserId**: Collaborator user (Guid)
- **InvitedBy**: User who sent invitation (Guid)
- **PermissionLevel**: View, Comment, or Contribute (enum)
- **InvitationDate**: When invited (DateTime)
- **AcceptanceStatus**: Pending, Accepted, Declined (enum)

## Commands

### GenerateShareLinkCommand
- Creates shareable link with options
- Raises **ShareLinkGenerated** event
- Returns share link URL

### AccessShareLinkCommand
- Validates link and permissions
- Increments view count
- Raises **ShareLinkAccessed** event
- Returns content

### AddCollaboratorCommand
- Invites user to collaborate
- Raises **CollaboratorAdded** event
- Returns success indicator

### ContributePhotoCommand
- Adds photo to shared album
- Raises **CollaborativePhotoAdded** event
- Returns PhotoId

## API Endpoints

### POST /api/share/link
- Generates share link
- Returns: 201 Created with share URL

### GET /api/share/{linkId}
- Accesses shared content
- Returns: 200 OK with content

### POST /api/albums/{albumId}/collaborators
- Adds collaborator
- Returns: 200 OK

### POST /api/albums/{albumId}/collaborate/photo
- Contributor adds photo
- Returns: 201 Created
