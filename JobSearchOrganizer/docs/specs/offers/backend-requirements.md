# Offers - Backend Requirements

## Overview
The Offers feature enables job seekers to track, negotiate, accept, or decline job offers throughout their job search.

## Domain Events

### OfferReceived
- `offerId` (Guid)
- `userId` (Guid)
- `applicationId` (Guid)
- `receivedAt` (DateTime)
- `baseSalary` (decimal)
- `currency` (string)
- `salaryPeriod` (string)
- `bonusAmount` (decimal?)
- `equityAmount` (string?)
- `benefits` (string[])
- `startDate` (DateTime?)
- `deadline` (DateTime?)
- `offerLetterUrl` (string?)

### OfferNegotiated
- `offerId` (Guid)
- `userId` (Guid)
- `negotiatedAt` (DateTime)
- `requestedSalary` (decimal?)
- `requestedBenefits` (string?)
- `counterOfferNotes` (string)
- `status` (string) - "Pending", "Accepted", "Declined"

### OfferAccepted
- `offerId` (Guid)
- `userId` (Guid)
- `acceptedAt` (DateTime)
- `finalSalary` (decimal)
- `finalBenefits` (string[])
- `startDate` (DateTime)
- `notes` (string?)

### OfferRejected
- `offerId` (Guid)
- `userId` (Guid)
- `rejectedAt` (DateTime)
- `reason` (string)
- `notes` (string?)

### OfferExpired
- `offerId` (Guid)
- `userId` (Guid)
- `expiredAt` (DateTime)
- `deadline` (DateTime)

## Aggregates

### Offer
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `ApplicationId` (Guid)
- `Status` (OfferStatus)
- `ReceivedAt` (DateTime)
- `BaseSalary` (decimal)
- `Currency` (string)
- `SalaryPeriod` (string)
- `BonusAmount` (decimal?)
- `EquityAmount` (string?)
- `Benefits` (List<string>)
- `StartDate` (DateTime?)
- `Deadline` (DateTime?)
- `OfferLetterUrl` (string?)
- `AcceptedAt` (DateTime?)
- `RejectedAt` (DateTime?)
- `ExpiredAt` (DateTime?)
- `RejectionReason` (string?)
- `NegotiationHistory` (List<NegotiationEvent>)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

**Business Rules:**
- Offer must be linked to an application
- Cannot accept expired offer
- Cannot negotiate after acceptance/rejection
- Deadline must be in the future
- Salary must be positive

## Enums

### OfferStatus
- Pending
- UnderNegotiation
- Accepted
- Rejected
- Expired

## API Endpoints

### POST /api/offers
Record new offer received.

### PUT /api/offers/{id}/negotiate
Submit counter offer.

### POST /api/offers/{id}/accept
Accept offer.

### POST /api/offers/{id}/reject
Reject offer.

### GET /api/offers
Get all offers.

### GET /api/offers/{id}
Get offer details.

### GET /api/offers/compare
Compare multiple offers side-by-side.

## Data Storage

**Offers Table:**
- Id, UserId, ApplicationId, Status
- BaseSalary, Currency, SalaryPeriod
- BonusAmount, EquityAmount
- Benefits (JSON or separate table)
- StartDate, Deadline
- AcceptedAt, RejectedAt, ExpiredAt
- OfferLetterUrl, Notes
- CreatedAt, UpdatedAt

**OfferBenefits Table:**
- Id, OfferId, BenefitName, BenefitValue

**OfferNegotiationHistory Table:**
- Id, OfferId, NegotiatedAt
- RequestedSalary, RequestedBenefits
- Notes, Status

## Validation Rules

- BaseSalary: Required, positive
- Currency: Required, valid code
- Benefits: Each max 200 chars
- Deadline: Must be future date
- OfferLetterUrl: Valid URL format

## Integration Points

- Link to applications
- Calendar integration for start date
- Email notifications for deadlines
- Document storage for offer letters

## Security

- Authentication required
- Users access only their offers
- Secure document storage
