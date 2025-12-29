# Backend Requirements - Meeting Management

## API Endpoints

### POST /api/meetings
**Request**: `{ date, time, location, topic, facilitatorId }`
**Response**: 201 Created

### GET /api/meetings
**Response**: List of meetings with RSVP counts

### POST /api/meetings/{id}/rsvp
**Request**: `{ attending: boolean }`
**Response**: 200 OK

### POST /api/meetings/{id}/start
**Response**: 200 OK (marks meeting started)

### POST /api/meetings/{id}/complete
**Request**: `{ topicsCovered, attendanceList, nextMeetingDate }`
**Response**: 200 OK

## Domain Events
- MeetingScheduled
- AttendanceRecorded
- MeetingStarted
- MeetingCompleted
- MeetingCancelled

## Database Schema
```sql
CREATE TABLE Meetings (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    GroupId UNIQUEIDENTIFIER NOT NULL,
    FacilitatorId UNIQUEIDENTIFIER NOT NULL,
    ScheduledDate DATETIME2 NOT NULL,
    Location NVARCHAR(200),
    VirtualLink NVARCHAR(500),
    Topic NVARCHAR(500),
    Status VARCHAR(20) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

CREATE TABLE MeetingAttendance (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    MeetingId UNIQUEIDENTIFIER NOT NULL,
    MemberId UNIQUEIDENTIFIER NOT NULL,
    RsvpStatus VARCHAR(20),
    ActualAttendance BIT,
    ArrivalTime DATETIME2,
    ParticipationLevel INT
);
```
