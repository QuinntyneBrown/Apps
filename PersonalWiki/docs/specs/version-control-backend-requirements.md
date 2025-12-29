# Version Control - Backend Requirements

## API Endpoints

#### GET /api/pages/{id}/versions
Get version history
- **Response**: List of all page versions

#### GET /api/versions/{versionId}
Get specific version content
- **Response**: Version details and content

#### POST /api/pages/{id}/revert/{versionId}
Revert to previous version
- **Events**: `PageReverted`, `VersionCreated`

#### GET /api/versions/compare
Compare two versions
- **Request**: `{ pageId, version1, version2 }`
- **Response**: Diff highlighting changes

## Business Logic
- Store complete content snapshots
- Generate text diffs
- Track edit summaries
- Calculate contribution statistics
