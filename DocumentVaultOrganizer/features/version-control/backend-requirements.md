# Version Control - Backend Requirements
## Domain Model
- DocumentVersion: VersionId, DocumentId, VersionNumber, FilePath, CreatedAt

## Commands
- CreateVersionCommand
- RestoreVersionCommand
- CompareVersionsCommand

## API Endpoints
- GET /api/documents/{id}/versions
- POST /api/documents/{id}/versions/{versionId}/restore
- GET /api/documents/{id}/versions/compare
