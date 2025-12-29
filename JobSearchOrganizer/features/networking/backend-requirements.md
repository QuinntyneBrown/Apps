# Networking - Backend Requirements

## Overview
The Networking feature enables job seekers to manage professional contacts, track referrals, and conduct informational interviews.

## Domain Events

### ReferralRequested
- `referralId` (Guid)
- `userId` (Guid)
- `contactId` (Guid)
- `jobListingId` (Guid?)
- `requestedAt` (DateTime)
- `message` (string)
- `status` (string) - "Pending", "Provided", "Declined"

### ReferralReceived
- `referralId` (Guid)
- `userId` (Guid)
- `contactId` (Guid)
- `jobListingId` (Guid?)
- `receivedAt` (DateTime)
- `referralDetails` (string)
- `applicationId` (Guid?) - Created application

### InformationalInterviewConducted
- `informationalInterviewId` (Guid)
- `userId` (Guid)
- `contactId` (Guid)
- `conductedAt` (DateTime)
- `duration` (int) - Minutes
- `location` (string)
- `notes` (string)
- `followUpRequired` (bool)
- `insights` (string[])

## Aggregates

### Contact
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `Name` (string)
- `Title` (string?)
- `Company` (string?)
- `Email` (string?)
- `Phone` (string?)
- `LinkedInUrl` (string?)
- `Relationship` (string) - "Former Colleague", "Recruiter", "Mentor", "Friend", "Other"
- `Notes` (string?)
- `Tags` (List<string>)
- `LastContactedAt` (DateTime?)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

### Referral
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `ContactId` (Guid)
- `JobListingId` (Guid?)
- `Status` (ReferralStatus)
- `RequestedAt` (DateTime)
- `RequestMessage` (string)
- `ReceivedAt` (DateTime?)
- `ReferralDetails` (string?)
- `ApplicationId` (Guid?)
- `Notes` (string?)

### InformationalInterview
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `ContactId` (Guid)
- `ScheduledAt` (DateTime?)
- `ConductedAt` (DateTime?)
- `Duration` (int?)
- `Location` (string?)
- `Notes` (string?)
- `Insights` (List<string>)
- `FollowUpRequired` (bool)
- `ThankYouSent` (bool)

## Enums

### ReferralStatus
- Pending
- Provided
- Declined
- Expired

### RelationshipType
- FormerColleague
- Recruiter
- Mentor
- Friend
- Industry Contact
- Other

## API Endpoints

### POST /api/contacts
Create new contact.

### GET /api/contacts
Get all contacts with filters.

### GET /api/contacts/{id}
Get contact details with interaction history.

### PUT /api/contacts/{id}
Update contact information.

### DELETE /api/contacts/{id}
Delete contact.

### POST /api/referrals
Request a referral.

### PUT /api/referrals/{id}/receive
Mark referral as received.

### GET /api/referrals
Get all referrals.

### POST /api/informational-interviews
Schedule informational interview.

### PUT /api/informational-interviews/{id}/complete
Mark informational interview as completed.

### GET /api/informational-interviews
Get all informational interviews.

## Data Storage

**Contacts Table:**
- Id, UserId, Name, Title, Company
- Email, Phone, LinkedInUrl
- Relationship, Notes
- LastContactedAt, CreatedAt, UpdatedAt

**ContactTags Table:**
- Id, ContactId, Tag

**Referrals Table:**
- Id, UserId, ContactId, JobListingId
- Status, RequestedAt, RequestMessage
- ReceivedAt, ReferralDetails
- ApplicationId, Notes

**InformationalInterviews Table:**
- Id, UserId, ContactId
- ScheduledAt, ConductedAt, Duration
- Location, Notes, FollowUpRequired
- ThankYouSent

**InformationalInterviewInsights Table:**
- Id, InformationalInterviewId, Insight

## Validation Rules

- Contact Name: Required, max 200 chars
- Email: Valid email format
- LinkedIn URL: Valid URL format
- Referral message: Required, max 2000 chars
- Informational interview notes: Max 10000 chars

## Integration Points

- LinkedIn profile import
- Email integration for outreach
- Calendar sync for informational interviews
- Link to job listings and applications

## Security

- Authentication required
- Users access only their own contacts
- Contact information encryption
