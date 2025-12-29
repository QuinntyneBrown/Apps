# Interviews - Backend Requirements

## Overview
The Interviews feature enables job seekers to schedule, prepare for, conduct, and follow up on job interviews throughout their hiring process.

## Domain Events

### InterviewScheduled
Triggered when an interview is scheduled.

**Event Data:**
- `interviewId` (Guid) - Unique identifier
- `userId` (Guid)
- `applicationId` (Guid) - Associated application
- `interviewType` (string) - "Phone Screen", "Video", "In-Person", "Technical", "Panel", "Final"
- `scheduledAt` (DateTime) - Interview date and time
- `duration` (int) - Duration in minutes
- `location` (string?) - Physical address or video link
- `interviewers` (string[]) - List of interviewer names
- `timezone` (string)
- `scheduledOn` (DateTime) - When it was scheduled

### InterviewPrepared
Triggered when preparation tasks are completed.

**Event Data:**
- `interviewId` (Guid)
- `userId` (Guid)
- `preparedAt` (DateTime)
- `preparationNotes` (string) - Prep notes
- `researchCompleted` (bool)
- `questionsPrepped` (bool)
- `materialsGathered` (bool)

### InterviewCompleted
Triggered when an interview is finished.

**Event Data:**
- `interviewId` (Guid)
- `userId` (Guid)
- `completedAt` (DateTime)
- `actualDuration` (int?) - Actual duration in minutes
- `performanceRating` (int?) - Self-rating 1-5
- `interviewNotes` (string) - Post-interview notes
- `nextSteps` (string?) - Expected next steps
- `followUpRequired` (bool)

### InterviewRescheduled
Triggered when an interview is rescheduled.

**Event Data:**
- `interviewId` (Guid)
- `userId` (Guid)
- `previousDateTime` (DateTime)
- `newDateTime` (DateTime)
- `rescheduledAt` (DateTime)
- `reason` (string?) - Reason for reschedule
- `initiatedBy` (string) - "Candidate" or "Company"

### ThankYouNoteSent
Triggered when thank you note is sent.

**Event Data:**
- `interviewId` (Guid)
- `userId` (Guid)
- `sentAt` (DateTime)
- `recipients` (string[]) - List of recipients
- `method` (string) - "Email", "LinkedIn", "Handwritten"
- `notes` (string?)

## Aggregates

### Interview
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `ApplicationId` (Guid)
- `InterviewType` (string)
- `Status` (InterviewStatus)
- `ScheduledAt` (DateTime)
- `Duration` (int)
- `Location` (string?)
- `Interviewers` (List<string>)
- `Timezone` (string)
- `CompletedAt` (DateTime?)
- `ActualDuration` (int?)
- `PerformanceRating` (int?)
- `InterviewNotes` (string?)
- `NextSteps` (string?)
- `FollowUpRequired` (bool)
- `PreparationNotes` (string?)
- `ResearchCompleted` (bool)
- `QuestionsPrepped` (bool)
- `MaterialsGathered` (bool)
- `ThankYouNoteSent` (bool)
- `ThankYouSentAt` (DateTime?)
- `RescheduleHistory` (List<RescheduleEvent>)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

**Business Rules:**
- Interview must be linked to an application
- Scheduled time must be in the future (unless rescheduled)
- Cannot complete an interview before scheduled time (unless rescheduled)
- Performance rating must be 1-5
- Duration must be positive
- Cannot send thank you note before interview completion

### RescheduleEvent (Value Object)
**Properties:**
- `PreviousDateTime` (DateTime)
- `NewDateTime` (DateTime)
- `RescheduledAt` (DateTime)
- `Reason` (string?)
- `InitiatedBy` (string)

## Enums

### InterviewStatus
- Scheduled
- Prepared
- Completed
- Rescheduled
- Cancelled

### InterviewType
- PhoneScreen
- Video
- InPerson
- Technical
- Panel
- Final

## API Endpoints

### POST /api/interviews
Schedule a new interview.

**Request:**
```json
{
  "applicationId": "uuid",
  "interviewType": "Video",
  "scheduledAt": "2026-01-05T14:00:00Z",
  "duration": 60,
  "location": "https://zoom.us/j/12345",
  "interviewers": ["Jane Smith", "John Doe"],
  "timezone": "America/Los_Angeles"
}
```

**Response:** 201 Created

### PUT /api/interviews/{id}/prepare
Mark interview as prepared.

**Request:**
```json
{
  "preparationNotes": "Reviewed company products, prepared STAR examples",
  "researchCompleted": true,
  "questionsPrepped": true,
  "materialsGathered": true
}
```

**Response:** 200 OK

### PUT /api/interviews/{id}/complete
Mark interview as completed.

**Request:**
```json
{
  "completedAt": "2026-01-05T15:00:00Z",
  "actualDuration": 55,
  "performanceRating": 4,
  "interviewNotes": "Interview went well. Discussed Azure experience in detail.",
  "nextSteps": "Expecting feedback within 3-5 business days",
  "followUpRequired": true
}
```

**Response:** 200 OK

### PUT /api/interviews/{id}/reschedule
Reschedule an interview.

**Request:**
```json
{
  "newDateTime": "2026-01-08T14:00:00Z",
  "reason": "Scheduling conflict",
  "initiatedBy": "Candidate"
}
```

**Response:** 200 OK

### POST /api/interviews/{id}/thank-you
Record thank you note sent.

**Request:**
```json
{
  "recipients": ["jane.smith@techcorp.com", "john.doe@techcorp.com"],
  "method": "Email",
  "notes": "Sent personalized thank you emails"
}
```

**Response:** 200 OK

### GET /api/interviews
Get all interviews for the current user.

**Query Parameters:**
- `status` (string?)
- `applicationId` (Guid?)
- `dateFrom` (DateTime?)
- `dateTo` (DateTime?)
- `interviewType` (string?)

**Response:** 200 OK

### GET /api/interviews/{id}
Get interview details.

**Response:** 200 OK

### GET /api/interviews/upcoming
Get upcoming interviews (next 30 days).

**Response:** 200 OK

### PUT /api/interviews/{id}
Update interview details.

**Response:** 200 OK

### DELETE /api/interviews/{id}
Cancel/delete an interview.

**Response:** 204 No Content

## Data Storage

### Database Tables

**Interviews**
- Id (PK, Guid)
- UserId (FK, Guid, Indexed)
- ApplicationId (FK, Guid, Indexed)
- InterviewType (nvarchar(50))
- Status (nvarchar(50), Indexed)
- ScheduledAt (DateTime, Indexed)
- Duration (int)
- Location (nvarchar(500)?)
- Timezone (nvarchar(100))
- CompletedAt (DateTime?)
- ActualDuration (int?)
- PerformanceRating (int?)
- InterviewNotes (nvarchar(max)?)
- NextSteps (nvarchar(500)?)
- FollowUpRequired (bit)
- PreparationNotes (nvarchar(max)?)
- ResearchCompleted (bit)
- QuestionsPrepped (bit)
- MaterialsGathered (bit)
- ThankYouNoteSent (bit)
- ThankYouSentAt (DateTime?)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)

**InterviewInterviewers**
- Id (PK, Guid)
- InterviewId (FK, Guid)
- InterviewerName (nvarchar(200))

**InterviewRescheduleHistory**
- Id (PK, Guid)
- InterviewId (FK, Guid, Indexed)
- PreviousDateTime (DateTime)
- NewDateTime (DateTime)
- RescheduledAt (DateTime)
- Reason (nvarchar(500)?)
- InitiatedBy (nvarchar(50))

## Validation Rules

- InterviewType: Required, valid enum
- ScheduledAt: Required, must be valid future date
- Duration: Required, 15-480 minutes
- Location: Max 500 characters
- Interviewers: Max 20, each max 200 characters
- PerformanceRating: 1-5
- PreparationNotes: Max 10000 characters
- InterviewNotes: Max 10000 characters

## Security

- All endpoints require authentication
- Users can only access their own interviews
- Calendar integration requires OAuth consent

## Integration Points

- Calendar sync (Google, Outlook, iCal)
- Video conferencing links
- Email reminders
- Application tracking

## Performance Requirements

- List endpoint < 200ms
- Calendar integration sync < 5 seconds
- Support 1000+ interviews per user

## Error Handling

Standard error responses with appropriate status codes and messages.
