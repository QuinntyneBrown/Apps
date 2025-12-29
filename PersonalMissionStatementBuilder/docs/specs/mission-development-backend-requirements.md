# Mission Development - Backend Requirements

## Domain Model

### Mission Aggregate
- **MissionId**: Guid
- **UserId**: Guid
- **Title**: string
- **MissionText**: string
- **Status**: enum (Draft, Active, UnderReview)
- **Version**: int
- **CreatedDate**: DateTime
- **FinalizedDate**: DateTime?
- **LastReviewedDate**: DateTime?
- **NextReviewDate**: DateTime?

## Commands
- CreateMissionDraftCommand
- ReviseMissionCommand
- FinalizeMissionCommand
- ReaffirmMissionCommand
- ScheduleReviewCommand

## Domain Events
- MissionDraftCreated
- MissionRevised
- MissionFinalized
- MissionReaffirmed

## API Endpoints
- POST /api/missions
- PUT /api/missions/{id}
- POST /api/missions/{id}/finalize
- POST /api/missions/{id}/reaffirm
- GET /api/missions/current
- GET /api/missions/{id}/history
