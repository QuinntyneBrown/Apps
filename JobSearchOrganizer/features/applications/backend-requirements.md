# Applications - Backend Requirements

## Overview
The Applications feature enables job seekers to track their job applications from initiation through completion, managing status updates, required documents, and communication history.

## Domain Events

### ApplicationStarted
Triggered when a user begins an application for a job listing.

**Event Data:**
- `applicationId` (Guid) - Unique identifier for the application
- `userId` (Guid) - User submitting the application
- `jobListingId` (Guid) - Associated job listing
- `startedAt` (DateTime) - When the application was started
- `targetSubmissionDate` (DateTime?) - Planned submission date
- `notes` (string?) - Initial notes about the application

### ApplicationSubmitted
Triggered when a user submits their application.

**Event Data:**
- `applicationId` (Guid)
- `userId` (Guid)
- `jobListingId` (Guid)
- `submittedAt` (DateTime)
- `submissionMethod` (string) - "Online Portal", "Email", "In Person", "Recruiter"
- `confirmationNumber` (string?) - Application confirmation reference
- `documentsSubmitted` (string[]) - List of documents included
- `coverLetterUsed` (bool)
- `notes` (string?) - Submission notes

### ApplicationStatusUpdated
Triggered when the application status changes.

**Event Data:**
- `applicationId` (Guid)
- `userId` (Guid)
- `previousStatus` (string)
- `newStatus` (string) - "Under Review", "Phone Screen", "Technical Assessment", "Interviewing", "Background Check", "Offer Extended"
- `updatedAt` (DateTime)
- `updatedBy` (string?) - Who updated (company contact name)
- `notes` (string?) - Status update notes
- `nextSteps` (string?) - Expected next actions

### ApplicationRejected
Triggered when an application is rejected.

**Event Data:**
- `applicationId` (Guid)
- `userId` (Guid)
- `rejectedAt` (DateTime)
- `rejectionReason` (string?) - "Not qualified", "Position filled", "Qualifications mismatch", "Other"
- `feedback` (string?) - Feedback provided by company
- `eligibleForReapplication` (bool)
- `reapplicationDate` (DateTime?) - Earliest date to reapply
- `notes` (string?)

### ApplicationWithdrawn
Triggered when the user withdraws their application.

**Event Data:**
- `applicationId` (Guid)
- `userId` (Guid)
- `withdrawnAt` (DateTime)
- `reason` (string) - "Accepted another offer", "Not interested", "Company culture concerns", "Compensation mismatch", "Other"
- `notes` (string?)

## Aggregates

### Application
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `JobListingId` (Guid)
- `Status` (ApplicationStatus)
- `StartedAt` (DateTime)
- `SubmittedAt` (DateTime?)
- `TargetSubmissionDate` (DateTime?)
- `SubmissionMethod` (string?)
- `ConfirmationNumber` (string?)
- `DocumentsSubmitted` (List<string>)
- `CoverLetterUsed` (bool)
- `CurrentStage` (string?)
- `RejectedAt` (DateTime?)
- `RejectionReason` (string?)
- `Feedback` (string?)
- `EligibleForReapplication` (bool)
- `ReapplicationDate` (DateTime?)
- `WithdrawnAt` (DateTime?)
- `WithdrawalReason` (string?)
- `Notes` (string?)
- `StatusHistory` (List<ApplicationStatusHistory>)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

**Business Rules:**
- Application must be linked to a valid job listing
- Cannot submit an application that hasn't been started
- Cannot update status of a rejected or withdrawn application
- Status transitions must follow logical progression
- Rejection reason required when rejecting
- Withdrawal reason required when withdrawing
- Cannot withdraw after rejection (already final)

### ApplicationStatusHistory (Value Object)
**Properties:**
- `Status` (string)
- `Timestamp` (DateTime)
- `UpdatedBy` (string?)
- `Notes` (string?)
- `NextSteps` (string?)

## Enums

### ApplicationStatus
- Draft
- Submitted
- UnderReview
- PhoneScreen
- TechnicalAssessment
- Interviewing
- BackgroundCheck
- OfferExtended
- Rejected
- Withdrawn

### SubmissionMethod
- OnlinePortal
- Email
- InPerson
- Recruiter
- Referral

## API Endpoints

### POST /api/applications
Start a new application.

**Request:**
```json
{
  "jobListingId": "uuid",
  "targetSubmissionDate": "2026-01-15T23:59:59Z",
  "notes": "Need to tailor resume for this position"
}
```

**Response:** 201 Created
```json
{
  "id": "uuid",
  "status": "Draft",
  "startedAt": "2025-12-28T10:00:00Z"
}
```

### POST /api/applications/{id}/submit
Submit an application.

**Request:**
```json
{
  "submissionMethod": "OnlinePortal",
  "confirmationNumber": "APP-2025-12345",
  "documentsSubmitted": ["Resume", "Cover Letter", "Portfolio"],
  "coverLetterUsed": true,
  "notes": "Submitted through company careers page"
}
```

**Response:** 200 OK

### PUT /api/applications/{id}/status
Update application status.

**Request:**
```json
{
  "status": "PhoneScreen",
  "updatedBy": "Jane Smith, HR Manager",
  "notes": "Phone screen scheduled for next week",
  "nextSteps": "Prepare for 30-minute phone interview"
}
```

**Response:** 200 OK

### POST /api/applications/{id}/reject
Mark application as rejected.

**Request:**
```json
{
  "rejectionReason": "Qualifications mismatch",
  "feedback": "Looking for more senior candidate",
  "eligibleForReapplication": true,
  "reapplicationDate": "2026-06-01T00:00:00Z",
  "notes": "Consider applying for mid-level positions"
}
```

**Response:** 200 OK

### POST /api/applications/{id}/withdraw
Withdraw application.

**Request:**
```json
{
  "reason": "Accepted another offer",
  "notes": "Accepted position at different company"
}
```

**Response:** 200 OK

### GET /api/applications
Get all applications for the current user.

**Query Parameters:**
- `status` (string?) - Filter by status
- `jobListingId` (Guid?) - Filter by job listing
- `dateFrom` (DateTime?) - Applications started after date
- `dateTo` (DateTime?) - Applications started before date
- `pageSize` (int) - Default 20, max 100
- `pageNumber` (int) - Default 1

**Response:** 200 OK
```json
{
  "items": [
    {
      "id": "uuid",
      "jobListing": {
        "id": "uuid",
        "title": "Senior Software Engineer",
        "company": "Tech Corp"
      },
      "status": "Interviewing",
      "startedAt": "2025-12-20T10:00:00Z",
      "submittedAt": "2025-12-22T15:30:00Z",
      "currentStage": "Second round interview scheduled"
    }
  ],
  "totalCount": 15,
  "pageSize": 20,
  "pageNumber": 1
}
```

### GET /api/applications/{id}
Get application details.

**Response:** 200 OK
```json
{
  "id": "uuid",
  "jobListing": {
    "id": "uuid",
    "title": "Senior Software Engineer",
    "company": "Tech Corp",
    "location": "San Francisco, CA"
  },
  "status": "Interviewing",
  "startedAt": "2025-12-20T10:00:00Z",
  "submittedAt": "2025-12-22T15:30:00Z",
  "submissionMethod": "OnlinePortal",
  "confirmationNumber": "APP-2025-12345",
  "documentsSubmitted": ["Resume", "Cover Letter"],
  "coverLetterUsed": true,
  "notes": "Tailored resume for Azure experience",
  "statusHistory": [
    {
      "status": "Interviewing",
      "timestamp": "2025-12-26T14:00:00Z",
      "updatedBy": "Jane Smith",
      "notes": "Second round scheduled",
      "nextSteps": "Technical interview on Jan 5"
    },
    {
      "status": "PhoneScreen",
      "timestamp": "2025-12-24T10:00:00Z",
      "updatedBy": "John Doe",
      "notes": "Phone screen completed"
    },
    {
      "status": "Submitted",
      "timestamp": "2025-12-22T15:30:00Z"
    }
  ]
}
```

### PUT /api/applications/{id}
Update application details.

**Request:**
```json
{
  "notes": "Updated notes",
  "targetSubmissionDate": "2026-01-20T23:59:59Z"
}
```

**Response:** 200 OK

### DELETE /api/applications/{id}
Delete an application (only for draft applications).

**Response:** 204 No Content

### GET /api/applications/statistics
Get application statistics.

**Response:** 200 OK
```json
{
  "total": 45,
  "draft": 3,
  "submitted": 8,
  "underReview": 12,
  "interviewing": 5,
  "rejected": 15,
  "withdrawn": 2,
  "averageTimeToSubmit": "2.5 days",
  "successRate": 0.33,
  "mostCommonRejectionReason": "Qualifications mismatch"
}
```

## Data Storage

### Database Tables

**Applications**
- Id (PK, Guid)
- UserId (FK, Guid, Indexed)
- JobListingId (FK, Guid, Indexed)
- Status (nvarchar(50), Indexed)
- StartedAt (DateTime, Indexed)
- SubmittedAt (DateTime?, Indexed)
- TargetSubmissionDate (DateTime?)
- SubmissionMethod (nvarchar(50)?)
- ConfirmationNumber (nvarchar(200)?)
- CoverLetterUsed (bit)
- CurrentStage (nvarchar(200)?)
- RejectedAt (DateTime?)
- RejectionReason (nvarchar(200)?)
- Feedback (nvarchar(2000)?)
- EligibleForReapplication (bit)
- ReapplicationDate (DateTime?)
- WithdrawnAt (DateTime?)
- WithdrawalReason (nvarchar(200)?)
- Notes (nvarchar(2000)?)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)

**ApplicationDocuments**
- Id (PK, Guid)
- ApplicationId (FK, Guid)
- DocumentName (nvarchar(200))
- AddedAt (DateTime)

**ApplicationStatusHistory**
- Id (PK, Guid)
- ApplicationId (FK, Guid, Indexed)
- Status (nvarchar(50))
- Timestamp (DateTime, Indexed)
- UpdatedBy (nvarchar(200)?)
- Notes (nvarchar(2000)?)
- NextSteps (nvarchar(500)?)

**Indexes:**
- IX_Applications_UserId_Status
- IX_Applications_JobListingId
- IX_Applications_SubmittedAt
- IX_ApplicationStatusHistory_ApplicationId_Timestamp

## Validation Rules

### ApplicationStarted
- JobListingId: Required, must exist
- TargetSubmissionDate: Must be in the future
- Notes: Max 2000 characters

### ApplicationSubmitted
- SubmissionMethod: Required
- ConfirmationNumber: Max 200 characters
- DocumentsSubmitted: Each document max 200 characters
- Notes: Max 2000 characters

### ApplicationStatusUpdated
- Status: Required, valid enum value
- Status must follow valid transitions
- UpdatedBy: Max 200 characters
- Notes: Max 2000 characters
- NextSteps: Max 500 characters

### ApplicationRejected
- RejectionReason: Max 200 characters
- Feedback: Max 2000 characters
- ReapplicationDate: Must be in the future if provided

### ApplicationWithdrawn
- Reason: Required, max 200 characters
- Notes: Max 2000 characters

## Security

- All endpoints require authentication
- Users can only access their own applications
- Rate limiting: 100 requests per minute per user
- Prevent SQL injection and XSS attacks

## Integration Points

- Link to job listings
- Link to interviews
- Link to offers
- Link to networking contacts
- Calendar integration for reminders
- Email notifications for status changes

## Performance Requirements

- List endpoint must return results within 200ms
- Support for 1000+ applications per user
- Efficient status history queries

## Error Handling

**Error Responses:**
- 400 Bad Request - Invalid input data or status transition
- 401 Unauthorized - Missing or invalid authentication
- 403 Forbidden - Accessing another user's data
- 404 Not Found - Application or job listing not found
- 409 Conflict - Invalid state transition
- 422 Unprocessable Entity - Business rule violation
- 500 Internal Server Error - Server error

**Error Response Format:**
```json
{
  "error": {
    "code": "INVALID_STATUS_TRANSITION",
    "message": "Cannot update status of a withdrawn application",
    "details": {
      "currentStatus": "Withdrawn",
      "attemptedStatus": "Interviewing"
    }
  }
}
```
