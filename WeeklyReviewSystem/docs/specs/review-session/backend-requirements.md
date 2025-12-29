# Review Session - Backend Requirements

## Domain Model

### WeeklyReview Aggregate
- **ReviewId**: Guid
- **UserId**: Guid
- **WeekEndingDate**: DateTime
- **StartedAt**: DateTime
- **CompletedAt**: DateTime?
- **Status**: enum (InProgress, Completed, Skipped)
- **Duration**: TimeSpan
- **CompletenessScore**: decimal
- **Sections**: List<ReviewSection>

### ReviewSection Entity
- **SectionId**: Guid
- **SectionType**: enum (Accomplishments, Challenges, Gratitude, Planning)
- **IsCompleted**: bool
- **CompletedAt**: DateTime?
- **Content**: string

## Commands
- StartWeeklyReviewCommand
- CompleteSectionCommand
- CompleteWeeklyReviewCommand
- SkipReviewCommand

## Domain Events
- WeeklyReviewStarted
- ReviewSectionCompleted
- WeeklyReviewCompleted
- ReviewSkipped

## API Endpoints
- POST /api/reviews/start
- PUT /api/reviews/{id}/sections/{sectionId}/complete
- POST /api/reviews/{id}/complete
- GET /api/reviews/current
- GET /api/reviews/history
- GET /api/reviews/streak
