# Backend Requirements - Community Events

## API Endpoints
- POST /api/events - Create neighborhood event
- GET /api/events - List upcoming events
- POST /api/events/{id}/rsvp - RSVP to event
- POST /api/events/block-party - Request block party approval

## Domain Events
- NeighborhoodEventCreated
- EventRSVPReceived
- NeighborhoodPartyHosted
- BlockPartyApprovalRequested

## Database Schema
```sql
CREATE TABLE NeighborhoodEvents (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    OrganizerId UNIQUEIDENTIFIER NOT NULL,
    EventType VARCHAR(50),
    Title NVARCHAR(200),
    Description NVARCHAR(MAX),
    EventDate DATETIME2,
    Location NVARCHAR(500),
    Capacity INT,
    CreatedAt DATETIME2
);

CREATE TABLE EventRSVPs (
    EventId UNIQUEIDENTIFIER,
    NeighborId UNIQUEIDENTIFIER,
    ResponseType VARCHAR(20),
    HouseholdSize INT,
    PRIMARY KEY (EventId, NeighborId)
);
```
