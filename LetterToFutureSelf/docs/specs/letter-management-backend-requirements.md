# Letter Management - Backend Requirements

## Domain Model

### Letter Aggregate
- **LetterId**: Guid
- **UserId**: Guid
- **Title**: string
- **Content**: string (encrypted)
- **WrittenDate**: DateTime
- **ScheduledDeliveryDate**: DateTime
- **ActualDeliveryDate**: DateTime?
- **Status**: enum (Draft, Scheduled, Delivered, Read, Archived)
- **Mood**: enum (Happy, Sad, Anxious, Excited, Reflective, Hopeful)
- **MoodIntensity**: int (1-10)
- **Tags**: List<string>
- **IsRead**: bool
- **ReadDate**: DateTime?
- **ReflectionNotes**: string

## Commands
- CreateLetterCommand
- ScheduleLetterCommand
- DeliverLetterCommand
- ReadLetterCommand
- AddReflectionCommand
- RescheduleLetterCommand

## Domain Events
- LetterCreated
- LetterScheduled
- LetterDelivered
- LetterRead
- LetterReflectionAdded

## API Endpoints
- POST /api/letters
- PUT /api/letters/{id}/schedule
- POST /api/letters/{id}/read
- POST /api/letters/{id}/reflection
- GET /api/letters/delivered
- GET /api/letters/archived
