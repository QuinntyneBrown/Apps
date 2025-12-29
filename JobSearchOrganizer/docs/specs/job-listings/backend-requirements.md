# Job Listings - Backend Requirements

## Overview
The Job Listings feature enables job seekers to discover, save, and manage job opportunities throughout their job search process.

## Domain Events

### JobListingDiscovered
Triggered when a user discovers a new job listing from any source.

**Event Data:**
- `jobListingId` (Guid) - Unique identifier for the job listing
- `userId` (Guid) - User who discovered the listing
- `title` (string) - Job title
- `company` (string) - Company name
- `location` (string) - Job location (city, state/country, or "Remote")
- `description` (string) - Full job description
- `sourceUrl` (string) - URL where the listing was found
- `discoveredAt` (DateTime) - When the listing was discovered
- `salary` (SalaryRange?, nullable) - Salary information if available
  - `min` (decimal)
  - `max` (decimal)
  - `currency` (string)
  - `period` (string) - "hourly", "annual", etc.
- `employmentType` (string) - "Full-time", "Part-time", "Contract", etc.
- `experienceLevel` (string) - "Entry", "Mid", "Senior", "Lead", etc.
- `tags` (string[]) - Keywords/technologies mentioned

### JobListingSaved
Triggered when a user saves a job listing to apply later.

**Event Data:**
- `jobListingId` (Guid)
- `userId` (Guid)
- `savedAt` (DateTime)
- `notes` (string?) - Optional notes about why this listing is interesting
- `priority` (string) - "High", "Medium", "Low"
- `deadline` (DateTime?) - Application deadline if known
- `category` (string?) - User-defined category/folder

### JobListingArchived
Triggered when a user archives a job listing (no longer interested).

**Event Data:**
- `jobListingId` (Guid)
- `userId` (Guid)
- `archivedAt` (DateTime)
- `reason` (string?) - "No longer available", "Not interested", "Requirements don't match", etc.

## Aggregates

### JobListing
**Properties:**
- `Id` (Guid)
- `UserId` (Guid)
- `Title` (string)
- `Company` (string)
- `Location` (string)
- `Description` (string)
- `SourceUrl` (string)
- `DiscoveredAt` (DateTime)
- `Salary` (SalaryRange?)
- `EmploymentType` (string)
- `ExperienceLevel` (string)
- `Tags` (List<string>)
- `Status` (JobListingStatus) - "Discovered", "Saved", "Archived"
- `SavedAt` (DateTime?)
- `Notes` (string?)
- `Priority` (string?)
- `Deadline` (DateTime?)
- `Category` (string?)
- `ArchivedAt` (DateTime?)
- `ArchiveReason` (string?)
- `CreatedAt` (DateTime)
- `UpdatedAt` (DateTime)

**Business Rules:**
- A job listing must have a title, company, and source URL
- Location cannot be empty
- Status transitions: Discovered -> Saved or Archived; Saved -> Archived
- Once archived, a listing cannot be un-archived (must be re-discovered)
- Priority is required when saving a listing
- Tags should be normalized to lowercase and deduplicated

### SalaryRange (Value Object)
**Properties:**
- `Min` (decimal)
- `Max` (decimal)
- `Currency` (string)
- `Period` (string)

**Business Rules:**
- Min must be less than or equal to Max
- Currency must be a valid 3-letter code (USD, EUR, GBP, etc.)
- Period must be one of: "hourly", "daily", "weekly", "monthly", "annual"

## API Endpoints

### POST /api/job-listings
Create a new job listing (discover).

**Request:**
```json
{
  "title": "Senior Software Engineer",
  "company": "Tech Corp",
  "location": "San Francisco, CA",
  "description": "We are looking for...",
  "sourceUrl": "https://example.com/jobs/123",
  "salary": {
    "min": 120000,
    "max": 180000,
    "currency": "USD",
    "period": "annual"
  },
  "employmentType": "Full-time",
  "experienceLevel": "Senior",
  "tags": ["C#", ".NET", "Azure", "Microservices"]
}
```

**Response:** 201 Created
```json
{
  "id": "uuid",
  "status": "Discovered",
  "discoveredAt": "2025-12-28T10:00:00Z"
}
```

### POST /api/job-listings/{id}/save
Save a discovered job listing.

**Request:**
```json
{
  "notes": "Great company culture, matches my skills",
  "priority": "High",
  "deadline": "2026-01-15T23:59:59Z",
  "category": "Dream Jobs"
}
```

**Response:** 200 OK

### POST /api/job-listings/{id}/archive
Archive a job listing.

**Request:**
```json
{
  "reason": "Requirements don't match"
}
```

**Response:** 200 OK

### GET /api/job-listings
Get all job listings for the current user.

**Query Parameters:**
- `status` (string?) - Filter by status: "Discovered", "Saved", "Archived"
- `priority` (string?) - Filter by priority: "High", "Medium", "Low"
- `category` (string?) - Filter by category
- `tags` (string[]?) - Filter by tags (AND logic)
- `company` (string?) - Filter by company name (partial match)
- `search` (string?) - Full-text search across title, company, description
- `pageSize` (int) - Default 20, max 100
- `pageNumber` (int) - Default 1

**Response:** 200 OK
```json
{
  "items": [
    {
      "id": "uuid",
      "title": "Senior Software Engineer",
      "company": "Tech Corp",
      "location": "San Francisco, CA",
      "status": "Saved",
      "priority": "High",
      "discoveredAt": "2025-12-28T10:00:00Z",
      "savedAt": "2025-12-28T11:00:00Z",
      "deadline": "2026-01-15T23:59:59Z",
      "tags": ["C#", ".NET", "Azure"]
    }
  ],
  "totalCount": 42,
  "pageSize": 20,
  "pageNumber": 1
}
```

### GET /api/job-listings/{id}
Get a specific job listing.

**Response:** 200 OK
```json
{
  "id": "uuid",
  "title": "Senior Software Engineer",
  "company": "Tech Corp",
  "location": "San Francisco, CA",
  "description": "Full description...",
  "sourceUrl": "https://example.com/jobs/123",
  "status": "Saved",
  "discoveredAt": "2025-12-28T10:00:00Z",
  "savedAt": "2025-12-28T11:00:00Z",
  "notes": "Great opportunity",
  "priority": "High",
  "deadline": "2026-01-15T23:59:59Z",
  "category": "Dream Jobs",
  "salary": {
    "min": 120000,
    "max": 180000,
    "currency": "USD",
    "period": "annual"
  },
  "employmentType": "Full-time",
  "experienceLevel": "Senior",
  "tags": ["C#", ".NET", "Azure", "Microservices"]
}
```

### PUT /api/job-listings/{id}
Update job listing details (title, description, notes, priority, etc.).

**Request:**
```json
{
  "notes": "Updated notes",
  "priority": "Medium",
  "category": "Backend Roles",
  "deadline": "2026-01-20T23:59:59Z"
}
```

**Response:** 200 OK

### DELETE /api/job-listings/{id}
Permanently delete a job listing.

**Response:** 204 No Content

### GET /api/job-listings/statistics
Get statistics about job listings.

**Response:** 200 OK
```json
{
  "total": 150,
  "discovered": 45,
  "saved": 82,
  "archived": 23,
  "byPriority": {
    "high": 15,
    "medium": 38,
    "low": 29
  },
  "withDeadlines": 12,
  "upcomingDeadlines": 3
}
```

## Data Storage

### Database Tables

**JobListings**
- Id (PK, Guid)
- UserId (FK, Guid, Indexed)
- Title (nvarchar(500))
- Company (nvarchar(200), Indexed)
- Location (nvarchar(200))
- Description (nvarchar(max))
- SourceUrl (nvarchar(1000))
- DiscoveredAt (DateTime, Indexed)
- SalaryMin (decimal(18,2)?)
- SalaryMax (decimal(18,2)?)
- SalaryCurrency (nvarchar(3)?)
- SalaryPeriod (nvarchar(20)?)
- EmploymentType (nvarchar(50))
- ExperienceLevel (nvarchar(50))
- Status (nvarchar(20), Indexed)
- SavedAt (DateTime?)
- Notes (nvarchar(2000)?)
- Priority (nvarchar(20)?)
- Deadline (DateTime?, Indexed)
- Category (nvarchar(100)?)
- ArchivedAt (DateTime?)
- ArchiveReason (nvarchar(500)?)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)

**JobListingTags**
- Id (PK, Guid)
- JobListingId (FK, Guid)
- Tag (nvarchar(100), Indexed)

**Indexes:**
- IX_JobListings_UserId_Status
- IX_JobListings_UserId_Priority
- IX_JobListings_UserId_Deadline
- IX_JobListings_Company
- IX_JobListingTags_Tag

### Event Store
Store all domain events in an event store table for event sourcing and audit trail.

## Validation Rules

### JobListingDiscovered
- Title: Required, max 500 characters
- Company: Required, max 200 characters
- Location: Required, max 200 characters
- SourceUrl: Required, valid URL format, max 1000 characters
- Description: Required
- Tags: Each tag max 100 characters, max 50 tags per listing

### JobListingSaved
- Priority: Required, must be "High", "Medium", or "Low"
- Notes: Max 2000 characters
- Category: Max 100 characters
- Deadline: Must be in the future

### JobListingArchived
- Reason: Max 500 characters

## Security

- All endpoints require authentication
- Users can only access their own job listings
- Rate limiting: 100 requests per minute per user
- Input sanitization to prevent XSS in description and notes fields

## Integration Points

- External job board APIs (LinkedIn, Indeed, Glassdoor) for importing listings
- Browser extension for quick job listing capture
- Email parsing for job alerts
- Calendar integration for deadline reminders

## Performance Requirements

- List endpoint must return results within 200ms for up to 1000 listings
- Search queries must complete within 500ms
- Support for 100,000+ job listings per user over time
- Implement pagination for all list endpoints

## Error Handling

**Error Responses:**
- 400 Bad Request - Invalid input data
- 401 Unauthorized - Missing or invalid authentication
- 403 Forbidden - Attempting to access another user's data
- 404 Not Found - Job listing not found
- 409 Conflict - Invalid status transition
- 429 Too Many Requests - Rate limit exceeded
- 500 Internal Server Error - Server error

**Error Response Format:**
```json
{
  "error": {
    "code": "INVALID_STATUS_TRANSITION",
    "message": "Cannot save an archived job listing",
    "details": {
      "currentStatus": "Archived",
      "attemptedAction": "Save"
    }
  }
}
```
