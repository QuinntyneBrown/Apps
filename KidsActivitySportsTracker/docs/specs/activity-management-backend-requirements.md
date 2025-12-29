# Activity Management - Backend Requirements

## Domain Model
### ActivityEnrollment Aggregate
- EnrollmentId, ChildId, ActivityType, Organization
- StartDate, EndDate, FeeAmount, Status
- Location, CoachName, ContactInfo

## Commands
- CreateEnrollmentCommand
- UpdateEnrollmentStatusCommand
- CompleteSeasonCommand

## Queries
- GetEnrollmentsByChildQuery
- GetActiveEnrollmentsQuery
- GetEnrollmentByIdQuery

## API Endpoints
- POST /api/enrollments
- PUT /api/enrollments/{id}/status
- GET /api/enrollments
- GET /api/children/{childId}/enrollments
