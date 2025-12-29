# Backend Requirements - Dream Journaling

## API Endpoints

### POST /api/dreams
Log a dream
- **Request Body**: sessionId, description, emotionalTone, vividness, isNightmare, userId
- **Events**: `DreamLogged`, `NightmareReported`

### GET /api/dreams/{userId}
Get user's dream journal
- **Query Parameters**: startDate, endDate, search

### GET /api/dreams/{dreamId}
Get specific dream entry

### PUT /api/dreams/{dreamId}
Update dream entry

### DELETE /api/dreams/{dreamId}
Delete dream entry

## Domain Models
### Dream: Id, SessionId, UserId, Description (5000 chars), EmotionalTone, Vividness (1-10), IsNightmare, Tags, LoggedAt

## Business Logic
- Correlate dream recall with REM sleep data
- Nightmare reduces sleep quality score by 5-15 points
- Identify recurring themes
- Privacy: encrypt dream descriptions at rest

## Events
DreamLogged, NightmareReported with dream metadata

## Database Schema
Dreams table with userId and sessionId indexing, encrypted description column
