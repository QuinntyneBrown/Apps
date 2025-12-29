# Celebrations - Backend Requirements

## Domain Model
### Celebration Aggregate
- **CelebrationId**: Guid
- **DateId**: Guid
- **CelebrationDate**: DateTime
- **Notes**: string
- **Photos**: List<string>
- **Attendees**: List<string>
- **Rating**: int (1-5)
- **Status**: Completed, Skipped (enum)

## Commands
- MarkCelebrationCompletedCommand
- MarkCelebrationSkippedCommand
- AddCelebrationPhotosCommand
