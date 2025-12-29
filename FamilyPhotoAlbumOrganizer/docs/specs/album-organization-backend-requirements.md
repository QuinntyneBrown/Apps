# Album Organization - Backend Requirements

## Overview
The Album Organization feature manages photo albums, allowing users to create, organize, and manage collections of related photos.

## Domain Model

### Album Aggregate
- **AlbumId**: Unique identifier (Guid)
- **UserId**: Owner of the album (Guid)
- **Name**: Album name (string, max 200 chars)
- **Description**: Optional description (string, max 1000 chars)
- **CoverPhotoId**: Photo used as album cover (Guid, nullable)
- **PrivacyLevel**: Album visibility (enum: Private, FamilyOnly, Public)
- **Theme**: Album theme/category (string, max 100 chars)
- **PhotoCount**: Number of photos in album (int)
- **CreatedAt**: Album creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)
- **SortOrder**: How photos are ordered (enum: DateAsc, DateDesc, Manual, Name)
- **IsShared**: Whether album is shared (bool)
- **Tags**: Collection of tag IDs (List<string>)

### AlbumPhoto Junction
- **AlbumId**: Reference to album (Guid)
- **PhotoId**: Reference to photo (Guid)
- **Position**: Photo order in album (int)
- **AddedAt**: When photo was added (DateTime)
- **AddedBy**: User who added photo (Guid)

## Commands

### CreateAlbumCommand
- Validates required fields (Name, UserId)
- Creates Album aggregate
- Raises **AlbumCreated** domain event
- Returns AlbumId

### AddPhotosToAlbumCommand
- Validates AlbumId and PhotoIds exist
- Adds photos to album
- Updates photo count
- Raises **PhotoAddedToAlbum** for each photo
- Returns success indicator

### RemovePhotoFromAlbumCommand
- Validates album and photo exist
- Removes photo from album
- Updates photo count
- Raises **PhotoRemovedFromAlbum** event
- Returns success indicator

### ReorganizeAlbumCommand
- Validates AlbumId exists
- Updates photo positions
- Raises **AlbumReorganized** event
- Returns success indicator

### ShareAlbumCommand
- Validates AlbumId exists
- Sets sharing permissions
- Raises **AlbumShared** event
- Returns share information

### UpdateAlbumCommand
- Validates AlbumId exists
- Updates album properties
- Returns success indicator

### DeleteAlbumCommand
- Validates AlbumId exists
- Soft deletes album
- Does not delete photos
- Returns success indicator

## Queries

### GetAlbumByIdQuery
- Returns Album details with photo count
- Includes cover photo
- Returns null if not found

### GetAlbumsByUserIdQuery
- Returns all albums for user
- Supports pagination and sorting
- Returns list of AlbumDto

### GetAlbumPhotosQuery
- Returns photos in album
- Respects sort order
- Supports pagination
- Returns list of PhotoDto

### GetSharedAlbumsQuery
- Returns albums shared with user
- Includes sharing permissions
- Returns list of AlbumDto

## API Endpoints

### POST /api/albums
- Creates new album
- Returns: 201 Created with AlbumDto

### PUT /api/albums/{albumId}
- Updates album
- Returns: 200 OK

### DELETE /api/albums/{albumId}
- Deletes album
- Returns: 204 No Content

### POST /api/albums/{albumId}/photos
- Adds photos to album
- Returns: 200 OK

### DELETE /api/albums/{albumId}/photos/{photoId}
- Removes photo from album
- Returns: 204 No Content

### PUT /api/albums/{albumId}/reorganize
- Reorders photos in album
- Returns: 200 OK

### POST /api/albums/{albumId}/share
- Shares album
- Returns: 200 OK with share link

### GET /api/albums/{albumId}
- Gets album details
- Returns: 200 OK with AlbumDto

### GET /api/albums
- Gets all user albums
- Returns: 200 OK with AlbumDto list

### GET /api/albums/{albumId}/photos
- Gets album photos
- Returns: 200 OK with PhotoDto list

## Business Rules

1. **Album Name**: Must be unique per user
2. **Photo Limit**: No hard limit on photos per album
3. **Cover Photo**: Must be a photo in the album
4. **Privacy**: Cannot change from Public to Private if shared
5. **Deletion**: Photos remain when album is deleted
6. **Duplicates**: Same photo can be in multiple albums
