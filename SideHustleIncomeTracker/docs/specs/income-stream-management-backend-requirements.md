# Income Stream Management - Backend Requirements

## Overview
The Income Stream Management feature handles all operations related to creating, updating, and managing side hustles and income-generating activities.

## Domain Model

### IncomeStream Aggregate
- **StreamId**: Unique identifier (Guid)
- **UserId**: Owner of the income stream (Guid)
- **StreamName**: Name of the side hustle (string, max 200 chars)
- **BusinessType**: Type of business activity (enum: Freelancing, Consulting, ECommerce, ContentCreation, Tutoring, Crafts, Services, Rental, Other)
- **Description**: Detailed description of the income stream (string, max 1000 chars)
- **StartDate**: Date when income stream was started (DateTime)
- **EndDate**: Date when income stream was closed (DateTime, nullable)
- **Status**: Current status (enum: Active, Closed, Reactivated)
- **ExpectedMonthlyRevenue**: Anticipated monthly income (decimal, precision 18,2)
- **ActualMonthlyRevenue**: Calculated actual monthly revenue (decimal, precision 18,2)
- **TotalRevenueEarned**: Lifetime revenue from this stream (decimal, precision 18,2)
- **ClosureReason**: Reason for closing (string, max 500 chars, nullable)
- **ReactivationDate**: Date when stream was reactivated (DateTime, nullable)
- **Tags**: Optional tags for organization (List<string>)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification timestamp (DateTime)

## Commands

### CreateIncomeStreamCommand
- Validates required fields (StreamName, BusinessType, StartDate, UserId)
- Ensures StreamName is not empty and is unique for user
- Ensures StartDate is not in future
- Creates IncomeStream aggregate with Status = Active
- Raises **IncomeStreamCreated** domain event
- Returns StreamId

### UpdateIncomeStreamCommand
- Validates StreamId exists
- Validates ownership (UserId matches)
- Updates allowed fields (StreamName, Description, BusinessType, ExpectedMonthlyRevenue, Tags)
- Updates UpdatedAt timestamp
- Returns success indicator

### CloseIncomeStreamCommand
- Validates StreamId exists
- Validates ownership (UserId matches)
- Validates stream is currently Active or Reactivated
- Sets Status = Closed
- Sets EndDate = current date or specified date
- Optionally sets ClosureReason
- Calculates total duration active
- Raises **IncomeStreamClosed** domain event
- Returns success indicator

### ReactivateIncomeStreamCommand
- Validates StreamId exists
- Validates ownership (UserId matches)
- Validates stream is currently Closed
- Sets Status = Reactivated
- Sets ReactivationDate = current date
- Clears EndDate
- Raises **IncomeStreamReactivated** domain event
- Returns success indicator

## Queries

### GetActiveIncomeStreamsQuery
- Returns all active and reactivated income streams for a user
- Includes calculated actual monthly revenue
- Sorted by most recent StartDate or ReactivationDate
- Pagination support

### GetIncomeStreamByIdQuery
- Returns detailed information for a specific income stream
- Includes revenue summary and performance metrics
- Validates ownership

### GetIncomeStreamHistoryQuery
- Returns all closed income streams for a user
- Includes total revenue earned and duration active
- Sorted by EndDate descending
- Pagination support

### GetIncomeStreamSummaryQuery
- Returns aggregated summary across all income streams
- Total active streams count
- Total monthly revenue across all streams
- Total lifetime revenue
- Average revenue per stream

## Domain Events

### IncomeStreamCreated
**Published When**: New income stream is successfully created
**Event Data**:
- StreamId (Guid)
- UserId (Guid)
- StreamName (string)
- BusinessType (string)
- StartDate (DateTime)
- ExpectedMonthlyRevenue (decimal)
- Timestamp (DateTime)

**Subscribers**:
- Dashboard service (update active streams count)
- Notification service (welcome message)
- Analytics service (track new stream creation)

### IncomeStreamClosed
**Published When**: Income stream is marked as closed
**Event Data**:
- StreamId (Guid)
- UserId (Guid)
- StreamName (string)
- EndDate (DateTime)
- TotalRevenueEarned (decimal)
- DurationInDays (int)
- ClosureReason (string)
- Timestamp (DateTime)

**Subscribers**:
- Dashboard service (update active streams count)
- Analytics service (track stream closure patterns)
- Archive service (move to historical data)
- Tax service (final P&L calculation)

### IncomeStreamReactivated
**Published When**: Closed income stream is reactivated
**Event Data**:
- StreamId (Guid)
- UserId (Guid)
- StreamName (string)
- ReactivationDate (DateTime)
- PreviousEndDate (DateTime)
- Timestamp (DateTime)

**Subscribers**:
- Dashboard service (add back to active streams)
- Notification service (reactivation confirmation)
- Analytics service (track reactivation patterns)

## API Endpoints

### POST /api/income-streams
Creates a new income stream
- **Request Body**: CreateIncomeStreamCommand
- **Response**: 201 Created with StreamId
- **Authorization**: Requires authenticated user

### PUT /api/income-streams/{id}
Updates an existing income stream
- **Request Body**: UpdateIncomeStreamCommand
- **Response**: 200 OK
- **Authorization**: Requires ownership

### POST /api/income-streams/{id}/close
Closes an income stream
- **Request Body**: CloseIncomeStreamCommand
- **Response**: 200 OK
- **Authorization**: Requires ownership

### POST /api/income-streams/{id}/reactivate
Reactivates a closed income stream
- **Request Body**: ReactivateIncomeStreamCommand
- **Response**: 200 OK
- **Authorization**: Requires ownership

### GET /api/income-streams
Retrieves all active income streams for the current user
- **Query Parameters**: page, pageSize, sortBy, sortOrder
- **Response**: 200 OK with paginated list
- **Authorization**: Requires authenticated user

### GET /api/income-streams/{id}
Retrieves details of a specific income stream
- **Response**: 200 OK with income stream details
- **Authorization**: Requires ownership

### GET /api/income-streams/history
Retrieves all closed income streams
- **Query Parameters**: page, pageSize
- **Response**: 200 OK with paginated list
- **Authorization**: Requires authenticated user

### GET /api/income-streams/summary
Retrieves summary statistics across all income streams
- **Response**: 200 OK with summary data
- **Authorization**: Requires authenticated user

## Validation Rules

### Business Rules
- An income stream name must be unique per user
- Start date cannot be in the future
- Only Active or Reactivated streams can be closed
- Only Closed streams can be reactivated
- ExpectedMonthlyRevenue must be >= 0
- User can only modify their own income streams

### Data Validation
- StreamName: required, 1-200 characters
- BusinessType: required, must be valid enum value
- Description: optional, max 1000 characters
- StartDate: required, valid date
- ExpectedMonthlyRevenue: optional, >= 0, max 18 digits with 2 decimal places
- ClosureReason: optional, max 500 characters

## Error Handling
- **404 Not Found**: Income stream doesn't exist
- **403 Forbidden**: User doesn't own the income stream
- **400 Bad Request**: Validation errors
- **409 Conflict**: Stream name already exists for user, invalid state transition

## Performance Considerations
- Index on UserId and Status for fast active stream queries
- Index on UserId and EndDate for historical queries
- Cache income stream summary for frequently accessed data
- Lazy load revenue calculations to avoid N+1 queries
