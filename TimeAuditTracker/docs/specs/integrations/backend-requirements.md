# Backend Requirements - Integrations

## Domain Events
- CalendarSynced
- AppUsageImported

## API Endpoints
- POST /api/integrations/calendar/sync - Sync external calendar
- POST /api/integrations/app-usage/import - Import device usage data
- GET /api/integrations/status - Get integration status
- DELETE /api/integrations/{id} - Remove integration

## Data Models
```typescript
CalendarSync {
  id, provider, eventsImported, conflictsDetected, syncTimestamp
}
AppUsageImport {
  id, appsTracked[], usageDuration, categoriesAssigned
}
```

## Business Logic
- OAuth integration with Google Calendar, Outlook
- Import calendar events as time entries
- Detect conflicts between calendar and logged time
- Auto-categorize imported activities
- Import screen time data from iOS/Android
- Securely store integration credentials
