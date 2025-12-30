# Interface Control Document (ICD)
## FamilyCalendarEventPlanner HTTP API

**Version:** 1.0
**Date:** 2025-12-29
**Base URL:** `http://localhost:3200`

---

## 1. Overview

This document defines the HTTP interface contract between the FamilyCalendarEventPlanner.Api backend and the family-calendar-event-planner frontend application. All endpoints follow RESTful conventions and use JSON for request/response bodies.

### 1.1 Common Headers

| Header | Value | Required |
|--------|-------|----------|
| Content-Type | application/json | Yes (for POST/PUT) |
| Accept | application/json | Recommended |

### 1.2 Common Response Codes

| Code | Description |
|------|-------------|
| 200 | Success |
| 201 | Created |
| 400 | Bad Request |
| 404 | Not Found |
| 500 | Internal Server Error |

---

## 2. Calendar Events API

### 2.1 GET /api/events

Get all calendar events, optionally filtered by family ID.

**Query Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| familyId | Guid | No | Filter events by family ID |

**Response:** `200 OK`
```json
[
  {
    "eventId": "guid",
    "familyId": "guid",
    "creatorId": "guid",
    "title": "string",
    "description": "string",
    "startTime": "datetime",
    "endTime": "datetime",
    "location": "string",
    "eventType": "Appointment|FamilyDinner|Sports|School|Vacation|Birthday|Other",
    "recurrencePattern": {
      "frequency": "None|Daily|Weekly|Monthly|Yearly",
      "interval": "int",
      "endDate": "datetime|null",
      "daysOfWeek": ["Sunday", "Monday", ...]
    },
    "status": "Scheduled|Completed|Cancelled"
  }
]
```

### 2.2 GET /api/events/{eventId}

Get a specific calendar event by ID.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| eventId | Guid | Yes | The event ID |

**Response:** `200 OK`
```json
{
  "eventId": "guid",
  "familyId": "guid",
  "creatorId": "guid",
  "title": "string",
  "description": "string",
  "startTime": "datetime",
  "endTime": "datetime",
  "location": "string",
  "eventType": "Appointment|FamilyDinner|Sports|School|Vacation|Birthday|Other",
  "recurrencePattern": {
    "frequency": "None|Daily|Weekly|Monthly|Yearly",
    "interval": "int",
    "endDate": "datetime|null",
    "daysOfWeek": ["Sunday", "Monday", ...]
  },
  "status": "Scheduled|Completed|Cancelled"
}
```

### 2.3 POST /api/events

Create a new calendar event.

**Request Body:**
```json
{
  "familyId": "guid",
  "creatorId": "guid",
  "title": "string",
  "description": "string",
  "startTime": "datetime",
  "endTime": "datetime",
  "location": "string",
  "eventType": "Appointment|FamilyDinner|Sports|School|Vacation|Birthday|Other",
  "recurrencePattern": {
    "frequency": "None|Daily|Weekly|Monthly|Yearly",
    "interval": "int",
    "endDate": "datetime|null",
    "daysOfWeek": ["Sunday", "Monday", ...]
  }
}
```

**Response:** `201 Created`
```json
{
  "eventId": "guid",
  "familyId": "guid",
  "creatorId": "guid",
  "title": "string",
  "description": "string",
  "startTime": "datetime",
  "endTime": "datetime",
  "location": "string",
  "eventType": "Appointment|FamilyDinner|Sports|School|Vacation|Birthday|Other",
  "recurrencePattern": {
    "frequency": "None|Daily|Weekly|Monthly|Yearly",
    "interval": "int",
    "endDate": "datetime|null",
    "daysOfWeek": ["Sunday", "Monday", ...]
  },
  "status": "Scheduled"
}
```

### 2.4 PUT /api/events/{eventId}

Update an existing calendar event.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| eventId | Guid | Yes | The event ID |

**Request Body:**
```json
{
  "title": "string|null",
  "description": "string|null",
  "startTime": "datetime|null",
  "endTime": "datetime|null",
  "location": "string|null",
  "eventType": "Appointment|FamilyDinner|Sports|School|Vacation|Birthday|Other|null",
  "recurrencePattern": {
    "frequency": "None|Daily|Weekly|Monthly|Yearly",
    "interval": "int",
    "endDate": "datetime|null",
    "daysOfWeek": ["Sunday", "Monday", ...]
  }
}
```

**Response:** `200 OK` - Returns the updated CalendarEventDto

### 2.5 POST /api/events/{eventId}/cancel

Cancel a calendar event.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| eventId | Guid | Yes | The event ID |

**Response:** `200 OK` - Returns the updated CalendarEventDto with status "Cancelled"

### 2.6 POST /api/events/{eventId}/complete

Mark a calendar event as completed.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| eventId | Guid | Yes | The event ID |

**Response:** `200 OK` - Returns the updated CalendarEventDto with status "Completed"

---

## 3. Family Members API

### 3.1 GET /api/familymembers

Get all family members, optionally filtered by family ID and/or immediate family status.

**Query Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| familyId | Guid | No | Filter members by family ID |
| isImmediate | bool | No | Filter by immediate family status (true = immediate family only, false = extended family only) |

**Response:** `200 OK`
```json
[
  {
    "memberId": "guid",
    "familyId": "guid",
    "name": "string",
    "email": "string|null",
    "color": "string",
    "role": "Admin|Member|ViewOnly",
    "isImmediate": "boolean",
    "relationType": "Self|Spouse|Child|Parent|Sibling|Grandparent|Grandchild|AuntUncle|NieceNephew|Cousin|InLaw|Other"
  }
]
```

### 3.2 GET /api/familymembers/{memberId}

Get a specific family member by ID.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| memberId | Guid | Yes | The member ID |

**Response:** `200 OK`
```json
{
  "memberId": "guid",
  "familyId": "guid",
  "name": "string",
  "email": "string|null",
  "color": "string",
  "role": "Admin|Member|ViewOnly",
  "isImmediate": "boolean",
  "relationType": "Self|Spouse|Child|Parent|Sibling|Grandparent|Grandchild|AuntUncle|NieceNephew|Cousin|InLaw|Other"
}
```

### 3.3 POST /api/familymembers

Create a new family member.

**Request Body:**
```json
{
  "familyId": "guid",
  "name": "string",
  "email": "string|null",
  "color": "string",
  "role": "Admin|Member|ViewOnly",
  "isImmediate": "boolean",
  "relationType": "Self|Spouse|Child|Parent|Sibling|Grandparent|Grandchild|AuntUncle|NieceNephew|Cousin|InLaw|Other"
}
```

**Response:** `201 Created` - Returns the created FamilyMemberDto

### 3.4 PUT /api/familymembers/{memberId}

Update an existing family member's profile.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| memberId | Guid | Yes | The member ID |

**Request Body:**
```json
{
  "name": "string|null",
  "email": "string|null",
  "color": "string|null",
  "isImmediate": "boolean|null",
  "relationType": "Self|Spouse|Child|Parent|Sibling|Grandparent|Grandchild|AuntUncle|NieceNephew|Cousin|InLaw|Other|null"
}
```

**Response:** `200 OK` - Returns the updated FamilyMemberDto

### 3.5 PUT /api/familymembers/{memberId}/role

Change a family member's role.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| memberId | Guid | Yes | The member ID |

**Request Body:**
```json
{
  "role": "Admin|Member|ViewOnly"
}
```

**Response:** `200 OK` - Returns the updated FamilyMemberDto

---

## 4. Attendees API

### 4.1 GET /api/attendees

Get all attendees for a specific event.

**Query Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| eventId | Guid | Yes | The event ID |

**Response:** `200 OK`
```json
[
  {
    "attendeeId": "guid",
    "eventId": "guid",
    "familyMemberId": "guid",
    "rsvpStatus": "Pending|Accepted|Declined|Tentative",
    "responseTime": "datetime|null",
    "notes": "string"
  }
]
```

### 4.2 POST /api/attendees

Add an attendee to an event.

**Request Body:**
```json
{
  "eventId": "guid",
  "familyMemberId": "guid",
  "notes": "string|null"
}
```

**Response:** `201 Created` - Returns the created EventAttendeeDto

### 4.3 PUT /api/attendees/{attendeeId}/respond

Update an attendee's RSVP response.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| attendeeId | Guid | Yes | The attendee ID |

**Request Body:**
```json
{
  "rsvpStatus": "Pending|Accepted|Declined|Tentative",
  "notes": "string|null"
}
```

**Response:** `200 OK` - Returns the updated EventAttendeeDto

---

## 5. Availability API

### 5.1 GET /api/availability

Get availability blocks for a family member.

**Query Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| memberId | Guid | Yes | The family member ID |

**Response:** `200 OK`
```json
[
  {
    "blockId": "guid",
    "memberId": "guid",
    "startTime": "datetime",
    "endTime": "datetime",
    "blockType": "Busy|OutOfOffice|Personal",
    "reason": "string"
  }
]
```

### 5.2 POST /api/availability

Create an availability block.

**Request Body:**
```json
{
  "memberId": "guid",
  "startTime": "datetime",
  "endTime": "datetime",
  "blockType": "Busy|OutOfOffice|Personal",
  "reason": "string|null"
}
```

**Response:** `201 Created` - Returns the created AvailabilityBlockDto

### 5.3 DELETE /api/availability/{blockId}

Delete an availability block.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| blockId | Guid | Yes | The block ID |

**Response:** `204 No Content`

---

## 6. Conflicts API

### 6.1 GET /api/conflicts

Get all schedule conflicts, optionally filtered.

**Query Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| memberId | Guid | No | Filter by affected member ID |
| isResolved | bool | No | Filter by resolution status |

**Response:** `200 OK`
```json
[
  {
    "conflictId": "guid",
    "conflictingEventIds": ["guid", "guid"],
    "affectedMemberIds": ["guid"],
    "conflictSeverity": "Low|Medium|High|Critical",
    "isResolved": "boolean",
    "resolvedAt": "datetime|null"
  }
]
```

### 6.2 POST /api/conflicts

Detect and create a schedule conflict.

**Request Body:**
```json
{
  "conflictingEventIds": ["guid", "guid"],
  "affectedMemberIds": ["guid"],
  "conflictSeverity": "Low|Medium|High|Critical"
}
```

**Response:** `201 Created` - Returns the created ScheduleConflictDto

### 6.3 POST /api/conflicts/{conflictId}/resolve

Resolve a schedule conflict.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| conflictId | Guid | Yes | The conflict ID |

**Response:** `200 OK` - Returns the updated ScheduleConflictDto with isResolved=true

---

## 7. Reminders API

### 7.1 GET /api/reminders

Get all reminders for a specific event or recipient.

**Query Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| eventId | Guid | No | Filter by event ID |
| recipientId | Guid | No | Filter by recipient ID |

**Response:** `200 OK`
```json
[
  {
    "reminderId": "guid",
    "eventId": "guid",
    "recipientId": "guid",
    "reminderTime": "datetime",
    "deliveryChannel": "Email|Push|SMS",
    "sent": "boolean"
  }
]
```

### 7.2 POST /api/reminders

Schedule a new reminder.

**Request Body:**
```json
{
  "eventId": "guid",
  "recipientId": "guid",
  "reminderTime": "datetime",
  "deliveryChannel": "Email|Push|SMS"
}
```

**Response:** `201 Created` - Returns the created EventReminderDto

### 7.3 PUT /api/reminders/{reminderId}/reschedule

Reschedule a reminder.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| reminderId | Guid | Yes | The reminder ID |

**Request Body:**
```json
{
  "reminderTime": "datetime"
}
```

**Response:** `200 OK` - Returns the updated EventReminderDto

### 7.4 POST /api/reminders/{reminderId}/send

Mark a reminder as sent.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| reminderId | Guid | Yes | The reminder ID |

**Response:** `200 OK` - Returns the updated EventReminderDto with sent=true

### 7.5 DELETE /api/reminders/{reminderId}

Delete a reminder.

**Path Parameters:**
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| reminderId | Guid | Yes | The reminder ID |

**Response:** `204 No Content`

---

## 8. Enumerations

### 8.1 EventType
- Appointment
- FamilyDinner
- Sports
- School
- Vacation
- Birthday
- Other

### 8.2 EventStatus
- Scheduled
- Completed
- Cancelled

### 8.3 RecurrenceFrequency
- None
- Daily
- Weekly
- Monthly
- Yearly

### 8.4 MemberRole
- Admin
- Member
- ViewOnly

### 8.5 RelationType
- Self
- Spouse
- Child
- Parent
- Sibling
- Grandparent
- Grandchild
- AuntUncle
- NieceNephew
- Cousin
- InLaw
- Other

### 8.6 RSVPStatus
- Pending
- Accepted
- Declined
- Tentative

### 8.7 BlockType
- Busy
- OutOfOffice
- Personal

### 8.8 ConflictSeverity
- Low
- Medium
- High
- Critical

### 8.9 NotificationChannel
- Email
- Push
- SMS

---

## 9. Frontend Service Configuration

### 9.1 Base URL Configuration
```typescript
// environment.ts
export const environment = {
  production: false,
  apiBaseUrl: 'http://localhost:3200'
};
```

### 9.2 Service URL Pattern
All services SHALL append `/api/{resource}` to the baseUrl:
```typescript
private readonly baseUrl = environment.apiBaseUrl;

getEvents(): Observable<CalendarEvent[]> {
  return this.http.get<CalendarEvent[]>(`${this.baseUrl}/api/events`);
}
```

---

## Document History

| Version | Date | Description |
|---------|------|-------------|
| 1.0 | 2025-12-29 | Initial ICD document |
| 1.1 | 2025-12-30 | Added IsImmediate, RelationType to FamilyMember; made email optional; added isImmediate query filter |

---

**End of Interface Control Document**
