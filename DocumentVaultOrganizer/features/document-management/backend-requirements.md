# Document Management - Backend Requirements

## Domain Model
### Document Aggregate
- DocumentId, UserId, FileName, FilePath, FileSize, MimeType
- CategoryId, EncryptionStatus, VersionNumber
- UploadDate, UpdatedAt, IsDeleted

## Commands
- UploadDocumentCommand
- UpdateDocumentCommand
- DeleteDocumentCommand
- RestoreDocumentCommand

## Queries
- GetDocumentByIdQuery
- GetDocumentsByUserIdQuery
- SearchDocumentsQuery
- GetDocumentVersionsQuery

## API Endpoints
- POST /api/documents/upload
- PUT /api/documents/{id}
- DELETE /api/documents/{id}
- POST /api/documents/{id}/restore
- GET /api/documents/{id}
- GET /api/documents/search
