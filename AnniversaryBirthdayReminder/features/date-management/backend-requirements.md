# Date Management - Backend Requirements

## Domain Model

### ImportantDate Aggregate
- **DateId**: Unique identifier (Guid)
- **UserId**: Owner (Guid)
- **PersonName**: Name of person or event (string)
- **DateType**: Birthday, Anniversary, Custom (enum)
- **DateValue**: The actual date (DateTime)
- **RecurrencePattern**: Annual, Custom (enum)
- **Relationship**: Family, Friend, Colleague, etc. (string)
- **Notes**: Optional notes (string)
- **IsActive**: Active tracking flag (bool)
- **CreatedAt**: Creation timestamp (DateTime)

## Commands
- CreateImportantDateCommand
- UpdateImportantDateCommand
- DeleteImportantDateCommand
- ToggleActiveDateCommand

## Queries
- GetDateByIdQuery
- GetDatesByUserIdQuery
- GetUpcomingDatesQuery
- GetDatesByPersonQuery

## API Endpoints
- POST /api/dates
- PUT /api/dates/{id}
- DELETE /api/dates/{id}
- GET /api/dates/{id}
- GET /api/dates
- GET /api/dates/upcoming

## Business Rules
1. Date must have valid DateValue
2. Person name required for birthdays/anniversaries
3. Cannot delete date with pending gifts
4. Recurrence pattern determines next occurrence
