# Linking System - Backend Requirements

## API Endpoints

#### POST /api/links/internal
Create internal link
- **Request**: `{ sourcePageId, targetPageId, linkText }`
- **Events**: `InternalLinkCreated`, `BacklinkUpdated`

#### GET /api/pages/{id}/backlinks
Get pages linking to this page
- **Response**: List of backlinks with context

#### GET /api/links/broken
Get all broken links
- **Response**: List of broken links for fixing

#### GET /api/pages/{id}/link-graph
Get link network for page
- **Response**: Graph data for visualization

## Business Logic

### Link Parser
- Extract [[WikiLinks]] from content
- Create link records automatically
- Detect broken links
- Generate link suggestions

### Graph Builder
- Build bidirectional link graph
- Calculate page rank/importance
- Identify hub pages
- Detect orphan pages
