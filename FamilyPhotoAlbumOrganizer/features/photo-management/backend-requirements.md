# Photo Management - Backend Requirements

## Overview
The Photo Management feature handles all operations related to uploading, editing, viewing, and deleting photos in the family photo collection.

## Domain Model

### Photo Aggregate
- **PhotoId**: Unique identifier (Guid)
- **UserId**: Owner of the photo (Guid)
- **FileName**: Original file name (string, max 255 chars)
- **FilePath**: Storage path or blob URL (string, max 1000 chars)
- **FileSize**: Size in bytes (long)
- **MimeType**: File type (string, e.g., "image/jpeg")
- **Width**: Image width in pixels (int)
- **Height**: Image height in pixels (int)
- **ThumbnailPath**: Path to thumbnail version (string)
- **UploadDate**: When photo was uploaded (DateTime)
- **CaptureDate**: When photo was taken (DateTime, from EXIF)
- **Camera**: Camera model (string, from EXIF)
- **ISO**: ISO setting (int, from EXIF)
- **Aperture**: Aperture value (string, from EXIF)
- **ShutterSpeed**: Shutter speed (string, from EXIF)
- **FocalLength**: Focal length (string, from EXIF)
- **IsEdited**: Whether photo has been modified (bool)
- **OriginalPhotoId**: Reference to original if edited (Guid, nullable)
- **IsDeleted**: Soft delete flag (bool)
- **DeletedAt**: Deletion timestamp (DateTime, nullable)
- **Tags**: Collection of tag IDs (List<Guid>)
- **Albums**: Collection of album IDs (List<Guid>)
- **CreatedAt**: Record creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

## Commands

### UploadPhotoCommand
- Validates file type (JPEG, PNG, HEIC, RAW formats)
- Validates file size (max 50MB per photo)
- Generates unique PhotoId
- Extracts EXIF metadata
- Creates thumbnail (300x300px)
- Stores original in blob storage
- Raises **PhotoUploaded** domain event
- Returns PhotoId and upload status

### BulkUploadPhotosCommand
- Accepts multiple photos (max 100 per batch)
- Validates each photo
- Processes uploads in parallel
- Generates progress updates
- Raises **PhotoUploaded** for each photo
- Returns array of PhotoIds and statuses

### EditPhotoCommand
- Validates PhotoId exists
- Validates user has permission
- Preserves original photo
- Applies edits (crop, rotate, filters, adjustments)
- Generates edited version
- Creates new thumbnail
- Links to original via OriginalPhotoId
- Raises **PhotoEdited** domain event
- Returns edited PhotoId

### DeletePhotoCommand
- Validates PhotoId exists
- Validates user has permission
- Soft deletes photo (sets IsDeleted = true)
- Moves to trash (30-day retention)
- Updates album associations
- Raises **PhotoDeleted** domain event
- Returns success indicator

### RestorePhotoCommand
- Validates PhotoId exists in trash
- Validates restoration is within retention period
- Restores photo (sets IsDeleted = false)
- Re-associates with original albums
- Raises **PhotoRestored** domain event
- Returns success indicator

### PermanentlyDeletePhotoCommand
- Validates PhotoId exists in trash
- Removes from blob storage
- Removes thumbnails
- Removes all metadata
- Raises **PhotoDeleted** domain event (permanent flag)
- Returns success indicator

### DownloadPhotoCommand
- Validates PhotoId exists
- Validates user has permission
- Generates temporary download URL
- Logs download event
- Returns download URL with expiration

## Queries

### GetPhotoByIdQuery
- Returns Photo details by PhotoId
- Includes all metadata and EXIF data
- Returns null if not found or deleted

### GetPhotosByUserIdQuery
- Returns all photos for a specific user
- Excludes deleted photos by default
- Supports filtering by:
  - Date range (upload or capture date)
  - Albums
  - Tags
  - File type
  - Edited status
- Supports sorting by:
  - Upload date
  - Capture date
  - File size
  - File name
- Supports pagination
- Returns list of PhotoDto

### GetRecentPhotosQuery
- Returns recently uploaded photos
- Default: last 30 days
- Sorted by UploadDate descending
- Paginated results
- Returns list of PhotoDto

### GetDeletedPhotosQuery
- Returns photos in trash
- Includes deletion date
- Shows days remaining before permanent deletion
- Sorted by DeletedAt descending
- Returns list of PhotoDto

### GetPhotosByAlbumIdQuery
- Returns all photos in specific album
- Respects album photo order
- Includes photo metadata
- Returns list of PhotoDto

### GetPhotoMetadataQuery
- Returns detailed EXIF and metadata for photo
- Includes camera settings
- Includes GPS coordinates if available
- Returns PhotoMetadataDto

### GetStorageUsageQuery
- Returns total storage used by user
- Breaks down by file types
- Includes trash storage
- Returns StorageUsageDto

## Domain Events

### PhotoUploaded
```csharp
public class PhotoUploaded : DomainEvent
{
    public Guid PhotoId { get; set; }
    public Guid UserId { get; set; }
    public string FileName { get; set; }
    public long FileSize { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public DateTime UploadDate { get; set; }
    public DateTime? CaptureDate { get; set; }
    public string SourceDevice { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PhotoEdited
```csharp
public class PhotoEdited : DomainEvent
{
    public Guid PhotoId { get; set; }
    public Guid OriginalPhotoId { get; set; }
    public Guid UserId { get; set; }
    public string EditType { get; set; }
    public Dictionary<string, object> EditParameters { get; set; }
    public DateTime EditTimestamp { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PhotoDeleted
```csharp
public class PhotoDeleted : DomainEvent
{
    public Guid PhotoId { get; set; }
    public Guid UserId { get; set; }
    public string FileName { get; set; }
    public bool SoftDelete { get; set; }
    public string DeletionReason { get; set; }
    public List<Guid> AlbumsAffected { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PhotoRestored
```csharp
public class PhotoRestored : DomainEvent
{
    public Guid PhotoId { get; set; }
    public Guid UserId { get; set; }
    public DateTime OriginalDeletionDate { get; set; }
    public List<Guid> AlbumsToRestoreTo { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/photos/upload
- Uploads a single photo
- Request: multipart/form-data with photo file
- Returns: 201 Created with PhotoDto
- Authorization: Authenticated user

### POST /api/photos/bulk-upload
- Uploads multiple photos
- Request: multipart/form-data with multiple files
- Returns: 201 Created with array of PhotoDto
- Authorization: Authenticated user

### PUT /api/photos/{photoId}/edit
- Edits a photo
- Request body: EditPhotoCommand with edit parameters
- Returns: 200 OK with edited PhotoDto
- Authorization: Photo owner

### DELETE /api/photos/{photoId}
- Soft deletes a photo (moves to trash)
- Returns: 204 No Content
- Authorization: Photo owner

### POST /api/photos/{photoId}/restore
- Restores a deleted photo from trash
- Returns: 200 OK with PhotoDto
- Authorization: Photo owner

### DELETE /api/photos/{photoId}/permanent
- Permanently deletes a photo from trash
- Returns: 204 No Content
- Authorization: Photo owner

### GET /api/photos/{photoId}
- Retrieves photo details
- Returns: 200 OK with PhotoDto
- Authorization: Photo owner or shared access

### GET /api/photos
- Retrieves all photos for current user
- Query params: filter, sort, page, pageSize
- Returns: 200 OK with paginated PhotoDto list
- Authorization: Authenticated user

### GET /api/photos/recent
- Retrieves recently uploaded photos
- Query params: days (default 30), page, pageSize
- Returns: 200 OK with PhotoDto list
- Authorization: Authenticated user

### GET /api/photos/trash
- Retrieves deleted photos
- Returns: 200 OK with PhotoDto list
- Authorization: Authenticated user

### GET /api/photos/{photoId}/metadata
- Retrieves detailed photo metadata
- Returns: 200 OK with PhotoMetadataDto
- Authorization: Photo owner or shared access

### GET /api/photos/{photoId}/download
- Generates download URL for photo
- Returns: 200 OK with download URL
- Authorization: Photo owner or shared access

### GET /api/photos/storage-usage
- Retrieves storage usage statistics
- Returns: 200 OK with StorageUsageDto
- Authorization: Authenticated user

## Business Rules

1. **File Size Limit**: Individual photos cannot exceed 50MB
2. **File Types**: Only image formats (JPEG, PNG, HEIC, RAW) allowed
3. **Trash Retention**: Deleted photos kept for 30 days before permanent deletion
4. **Storage Quota**: Users have storage limits based on subscription
5. **User Isolation**: Users can only access their own photos (unless shared)
6. **Original Preservation**: When editing, original photo must be preserved
7. **Virus Scanning**: All uploads must be scanned for malware
8. **EXIF Stripping**: Option to strip location data for privacy

## Data Persistence

### Photos Table
```sql
CREATE TABLE Photos (
    PhotoId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(1000) NOT NULL,
    FileSize BIGINT NOT NULL,
    MimeType NVARCHAR(100) NOT NULL,
    Width INT NOT NULL,
    Height INT NOT NULL,
    ThumbnailPath NVARCHAR(1000) NULL,
    UploadDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CaptureDate DATETIME2 NULL,
    Camera NVARCHAR(200) NULL,
    ISO INT NULL,
    Aperture NVARCHAR(50) NULL,
    ShutterSpeed NVARCHAR(50) NULL,
    FocalLength NVARCHAR(50) NULL,
    Latitude DECIMAL(10, 8) NULL,
    Longitude DECIMAL(11, 8) NULL,
    IsEdited BIT NOT NULL DEFAULT 0,
    OriginalPhotoId UNIQUEIDENTIFIER NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    DeletedAt DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT FK_Photos_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_Photos_OriginalPhoto FOREIGN KEY (OriginalPhotoId) REFERENCES Photos(PhotoId),
    INDEX IX_Photos_UserId (UserId),
    INDEX IX_Photos_UploadDate (UploadDate),
    INDEX IX_Photos_CaptureDate (CaptureDate),
    INDEX IX_Photos_IsDeleted (IsDeleted)
);
```

## Error Handling

### Validation Errors (400 Bad Request)
- Invalid file type
- File size exceeds limit
- Missing required metadata
- Invalid edit parameters

### Authorization Errors (403 Forbidden)
- User attempting to access another user's photo
- Insufficient permissions

### Not Found Errors (404 Not Found)
- PhotoId does not exist
- Photo was permanently deleted

### Conflict Errors (409 Conflict)
- Storage quota exceeded
- Duplicate upload detected

### Server Errors (500 Internal Server Error)
- Blob storage failures
- Image processing errors
- Virus scanning failures

## Integration Points

### Event Handlers
- **PhotoUploaded**: Trigger thumbnail generation, metadata extraction, backup queue
- **PhotoEdited**: Update thumbnails, notify album subscribers
- **PhotoDeleted**: Remove from search index, update album counts, schedule cleanup
- **PhotoRestored**: Re-index for search, update album counts

### Background Jobs
- **Thumbnail Generation**: Create multiple sizes (thumbnail, preview, full)
- **EXIF Extraction**: Parse and store metadata
- **Backup Job**: Upload to cloud backup storage
- **Trash Cleanup**: Permanently delete photos after 30 days
- **Storage Calculator**: Update user storage usage

## Performance Considerations

- Use blob storage CDN for fast photo delivery
- Generate multiple thumbnail sizes for different views
- Implement lazy loading for photo galleries
- Cache frequently accessed photos
- Use async processing for uploads
- Batch operations for bulk uploads
- Optimize image formats (WebP for web delivery)

## Security Considerations

- Virus scan all uploads
- Validate file signatures (not just extensions)
- Sanitize file names
- Implement rate limiting on uploads
- Encrypt photos at rest in blob storage
- Generate time-limited SAS tokens for downloads
- Strip sensitive EXIF data on sharing
- Log all photo access for audit

## Testing Requirements

### Unit Tests
- Domain model validation
- EXIF extraction logic
- Edit parameter validation
- Command handlers
- Query handlers

### Integration Tests
- Photo upload flow
- Bulk upload processing
- Edit and restore operations
- Storage integration
- Thumbnail generation

### Performance Tests
- Upload speed with large files
- Bulk upload (100 photos)
- Concurrent uploads
- Thumbnail generation time
- Query performance with 100k+ photos
