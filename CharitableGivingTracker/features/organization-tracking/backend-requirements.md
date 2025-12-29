# Organization Tracking - Backend Requirements

## Overview
The Organization Tracking feature enables users to manage charitable organizations, verify 501(c)(3) status, track charity ratings, and set preferences for quick donations.

## Domain Model

### Organization Aggregate
- **OrganizationId**: Unique identifier (Guid)
- **UserId**: User who added the organization (Guid)
- **Name**: Organization name (string, max 200 chars)
- **EIN**: Employer Identification Number (string, 9 chars)
- **CharityType**: Type of charity (enum: Public, Private, Foundation, Religious, Educational, Other)
- **Mission**: Organization mission statement (string, max 1000 chars)
- **Website**: Organization website URL (string, max 500 chars)
- **ContactEmail**: Contact email (string, max 100 chars)
- **ContactPhone**: Contact phone (string, max 20 chars)
- **Address**: Mailing address (string, max 500 chars)
- **IsVerified**: 501(c)(3) verification status (bool)
- **VerificationSource**: Source of verification (enum: IRS, Manual, API)
- **VerifiedDate**: When verification completed (DateTime, nullable)
- **TaxExemptStatus**: Tax-exempt status (bool)
- **IsPreferred**: User marked as preferred (bool)
- **PreferenceLevel**: Priority level (int, 1-5)
- **Rating**: Charity rating score (decimal, nullable)
- **RatingSource**: Rating provider (string, max 100 chars)
- **RatingDate**: When rating last updated (DateTime, nullable)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

## Commands

### AddCharityCommand
- Validates Name and EIN
- Creates Organization aggregate
- Optionally triggers verification
- Raises **CharityAdded** domain event
- Returns OrganizationId

### VerifyCharityCommand
- Validates OrganizationId exists
- Calls IRS EIN verification API
- Updates IsVerified, TaxExemptStatus
- Raises **CharityVerified** domain event
- Returns verification result

### UpdateCharityRatingCommand
- Validates OrganizationId exists
- Fetches rating from external source
- Updates Rating, RatingSource, RatingDate
- Raises **CharityRatingUpdated** domain event
- Returns updated rating

### SetPreferredCharityCommand
- Validates OrganizationId exists
- Sets IsPreferred and PreferenceLevel
- Raises **PreferredCharitySet** domain event
- Returns success indicator

### UpdateOrganizationCommand
- Validates OrganizationId exists
- Updates organization details
- Returns success indicator

## Queries

### GetOrganizationByIdQuery
- Returns Organization details by OrganizationId

### GetOrganizationsByUserIdQuery
- Returns all organizations for a user
- Supports filtering by IsVerified, IsPreferred
- Supports sorting by Name, Rating
- Returns list of OrganizationDto

### GetPreferredOrganizationsQuery
- Returns user's preferred organizations
- Sorted by PreferenceLevel
- Returns list of OrganizationDto

### SearchOrganizationsQuery
- Searches organizations by name or EIN
- Returns matching organizations

## Domain Events

### CharityAdded
```csharp
public class CharityAdded : DomainEvent
{
    public Guid OrganizationId { get; set; }
    public string Name { get; set; }
    public string EIN { get; set; }
    public string CharityType { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### CharityVerified
```csharp
public class CharityVerified : DomainEvent
{
    public Guid OrganizationId { get; set; }
    public string EIN { get; set; }
    public string VerificationSource { get; set; }
    public bool TaxExemptStatus { get; set; }
    public DateTime VerificationDate { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### CharityRatingUpdated
```csharp
public class CharityRatingUpdated : DomainEvent
{
    public Guid OrganizationId { get; set; }
    public string RatingSource { get; set; }
    public decimal Score { get; set; }
    public DateTime RatingDate { get; set; }
    public decimal? PreviousRating { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PreferredCharitySet
```csharp
public class PreferredCharitySet : DomainEvent
{
    public Guid OrganizationId { get; set; }
    public int PreferenceLevel { get; set; }
    public DateTime SetDate { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/organizations
- Adds a new organization
- Request body: AddCharityCommand
- Returns: 201 Created with OrganizationId

### POST /api/organizations/{organizationId}/verify
- Verifies organization 501(c)(3) status
- Returns: 200 OK with verification result

### POST /api/organizations/{organizationId}/rating
- Updates charity rating
- Returns: 200 OK with updated rating

### POST /api/organizations/{organizationId}/prefer
- Sets organization as preferred
- Request body: { preferenceLevel: int }
- Returns: 200 OK

### PUT /api/organizations/{organizationId}
- Updates organization details
- Request body: UpdateOrganizationCommand
- Returns: 200 OK

### GET /api/organizations/{organizationId}
- Retrieves organization details
- Returns: 200 OK with OrganizationDto

### GET /api/organizations
- Lists user's organizations
- Query params: verified, preferred, page, pageSize
- Returns: 200 OK with OrganizationDto list

### GET /api/organizations/search
- Searches organizations
- Query params: query
- Returns: 200 OK with OrganizationDto list

## Business Rules

1. **EIN Format**: Must be 9 digits (XX-XXXXXXX)
2. **Verification Required**: Organizations must be verified before tax-deductible donations
3. **User Isolation**: Users manage their own organization list
4. **Rating Update**: Ratings updated automatically via background job
5. **Preferred Limit**: Maximum 10 preferred organizations
