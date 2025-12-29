# Synthesis - Backend Requirements

## API Endpoints
- POST /api/synthesis/summarize - Generate summary
- POST /api/synthesis/extract-highlights - Extract key points
- GET /api/synthesis/insights - Get synthesized insights
- POST /api/notes/{id}/evergreen - Mark as evergreen

## Domain Events
- NoteSynthesized
- InsightExtracted
- EvergreenNoteCreated
- KnowledgeDistilled

## Business Logic
- Progressive summarization layers
- Key concept extraction
- Cross-note theme detection
- Automated insight generation
