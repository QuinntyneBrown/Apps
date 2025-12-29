# Backend Requirements - Privacy & Security

## API Endpoints

### POST /api/private-entries
Create a private journal entry.

**Request Body**:
```json
{
  "contentType": "enum (gratitude|reflection|note) (required)",
  "content": "string (required)",
  "privacyDuration": "enum (temporary|indefinite) (default: indefinite)"
}
```

**Response**: 201 Created

### PUT /api/private-entries/{id}/share
Share a previously private entry with spouse.

**Request Body**:
```json
{
  "shareReason": "string (optional)"
}
```

**Response**: 200 OK

### GET /api/privacy/settings
Get user's privacy preferences.

**Response**: 200 OK
```json
{
  "defaultPrivacyLevel": "string",
  "contentTypeDefaults": {},
  "shareNotifications": boolean,
  "auditLogEnabled": boolean
}
```

### PUT /api/privacy/settings
Update privacy preferences.

**Request Body**:
```json
{
  "defaultPrivacyLevel": "enum (private|shared)",
  "contentTypeDefaults": {
    "gratitude": "shared",
    "reflection": "private",
    "appreciation": "shared"
  },
  "shareNotifications": boolean,
  "auditLogEnabled": boolean
}
```

**Response**: 200 OK

### GET /api/privacy/audit-log
Get audit log of content access.

**Query Parameters**:
- `startDate`, `endDate`
- `contentType`
- `page`, `pageSize`

**Response**: 200 OK
```json
{
  "items": [
    {
      "timestamp": "datetime",
      "contentId": "guid",
      "contentType": "string",
      "action": "viewed|shared|edited",
      "actor": "guid"
    }
  ]
}
```

### POST /api/privacy/data-export
Request export of all personal data.

**Request Body**:
```json
{
  "format": "enum (json|csv|pdf)",
  "includePhotos": boolean
}
```

**Response**: 202 Accepted
```json
{
  "exportId": "guid",
  "estimatedCompletionTime": "datetime"
}
```

### GET /api/privacy/data-export/{exportId}
Check status and download data export.

**Response**: 200 OK (if complete) or 202 Accepted (if processing)

### DELETE /api/privacy/data
Request account and data deletion.

**Request Body**:
```json
{
  "reason": "string (optional)",
  "confirmationCode": "string (required)"
}
```

**Response**: 202 Accepted

## Domain Events

### PrivateEntryCreated
**Payload**:
```json
{
  "entryId": "guid",
  "authorId": "guid",
  "contentType": "string",
  "privacyDuration": "string",
  "createdAt": "datetime"
}
```

**Handlers**:
- Store in private journal archive
- Set privacy flags

### EntrySharedAfterDelay
**Payload**:
```json
{
  "entryId": "guid",
  "originalCreationDate": "datetime",
  "shareDate": "datetime",
  "delayDuration": "duration",
  "shareReason": "string",
  "sharedAt": "datetime"
}
```

**Handlers**:
- Update privacy level
- Notify spouse
- Track vulnerability metrics
- Log in audit trail

## Database Schema

### PrivacySettings Table
```sql
CREATE TABLE PrivacySettings (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL UNIQUE,
    DefaultPrivacyLevel VARCHAR(20) NOT NULL,
    ContentTypeDefaults NVARCHAR(MAX) NOT NULL,
    ShareNotifications BIT NOT NULL DEFAULT 1,
    AuditLogEnabled BIT NOT NULL DEFAULT 1,
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

### PrivateEntries Table
```sql
CREATE TABLE PrivateEntries (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    AuthorId UNIQUEIDENTIFIER NOT NULL,
    ContentType VARCHAR(50) NOT NULL,
    Content VARBINARY(MAX) NOT NULL, -- Encrypted
    PrivacyDuration VARCHAR(20) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    SharedAt DATETIME2 NULL,
    ShareReason NVARCHAR(500) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (AuthorId) REFERENCES Users(Id)
);

CREATE INDEX IX_PrivateEntries_AuthorId ON PrivateEntries(AuthorId);
```

### AuditLog Table
```sql
CREATE TABLE AuditLog (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    ContentId UNIQUEIDENTIFIER NULL,
    ContentType VARCHAR(50) NOT NULL,
    Action VARCHAR(50) NOT NULL,
    ActorId UNIQUEIDENTIFIER NOT NULL,
    IpAddress VARCHAR(45) NULL,
    UserAgent NVARCHAR(500) NULL,
    Timestamp DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (ActorId) REFERENCES Users(Id)
);

CREATE INDEX IX_AuditLog_UserId_Timestamp ON AuditLog(UserId, Timestamp);
CREATE INDEX IX_AuditLog_ContentId ON AuditLog(ContentId);
```

### DataExportRequests Table
```sql
CREATE TABLE DataExportRequests (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId UNIQUEIDENTIFIER NOT NULL,
    Format VARCHAR(20) NOT NULL,
    IncludePhotos BIT NOT NULL,
    Status VARCHAR(20) NOT NULL,
    RequestedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CompletedAt DATETIME2 NULL,
    DownloadUrl NVARCHAR(500) NULL,
    ExpiresAt DATETIME2 NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);
```

## Business Logic

### Encryption
- All private content encrypted at rest using AES-256
- User-specific encryption keys derived from master key
- Decrypt only when user requests their own content
- Never expose private content in logs or error messages

### Privacy Enforcement
- Private entries only accessible to author
- Shared entries accessible to both partners
- Joint entries accessible to both authors
- Deleted content marked as deleted, not physically removed

### Audit Logging
- Log all content views, edits, shares, deletes
- Include timestamp, actor, IP address, user agent
- Retain logs for 90 days
- User can view their own audit log

### Data Export
- Generate export in requested format
- Include all content created by user
- Optionally include photos
- Sign export file for integrity
- Auto-delete after 7 days
- Limit to 1 export per 24 hours

### Account Deletion
- Require confirmation code sent to email
- Soft delete: Mark account as deleted
- Retain data for 30 days for recovery
- After 30 days, hard delete all personal data
- Anonymize shared content (keep for spouse but remove identifiers)

## Security Requirements

### Authentication
- OAuth 2.0 with authorization code flow
- Support MFA (TOTP, SMS)
- Session timeout: 30 minutes inactivity
- Refresh token rotation
- Logout from all devices option

### API Security
- All endpoints require authentication
- Rate limiting: 100 requests per minute per user
- HTTPS only (TLS 1.3)
- CORS restricted to app domain
- CSRF protection

### Data Protection
- Encryption at rest: AES-256
- Encryption in transit: TLS 1.3
- Database-level encryption
- Encrypted backups
- Secure key management (Azure Key Vault or AWS KMS)

## Performance Requirements
- Privacy settings update: <100ms
- Audit log retrieval: <500ms
- Data export generation: <5 minutes
- Content decryption: <50ms
