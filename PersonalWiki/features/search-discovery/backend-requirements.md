# Search & Discovery - Backend Requirements

## API Endpoints

#### GET /api/search
Full-text search
- **Query**: `{ q, namespace, category, limit }`
- **Response**: Ranked search results
- **Events**: `PageSearched`

#### POST /api/search/reindex
Rebuild search index
- **Events**: `SearchIndexRebuilt`

#### GET /api/pages/recent
Get recently modified pages
- **Response**: Latest changes

#### GET /api/pages/popular
Get popular pages
- **Response**: Most viewed pages
- **Events**: `PopularPageIdentified`

## Business Logic
- Elasticsearch integration
- TF-IDF ranking
- Title boost
- Namespace filtering
- Faceted search
- Search analytics
