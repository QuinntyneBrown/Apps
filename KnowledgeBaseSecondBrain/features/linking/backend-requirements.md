# Linking - Backend Requirements

## API Endpoints
- POST /api/links - Create link
- GET /api/notes/{id}/backlinks - Get backlinks
- GET /api/graph - Get knowledge graph data
- GET /api/notes/{id}/related - Get related notes

## Domain Events
- NotesLinked
- BacklinkCreated
- LinkClusterIdentified
- OrphanNoteDetected

## Graph Database
- Neo4j for relationship storage
- Calculate PageRank for note importance
- Detect communities/clusters
- Find shortest paths between concepts
