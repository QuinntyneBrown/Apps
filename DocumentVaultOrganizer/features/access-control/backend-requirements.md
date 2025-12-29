# Access Control - Backend Requirements
## Domain Model
- ShareLink: LinkId, DocumentId, URL, ExpirationDate, PasswordHash
- DocumentPermission: PermissionId, DocumentId, UserId, AccessLevel

## Commands
- ShareDocumentCommand
- GenerateShareLinkCommand
- RevokeAccessCommand

## API Endpoints
- POST /api/documents/{id}/share
- POST /api/documents/{id}/share-link
- DELETE /api/documents/{id}/share/{userId}
