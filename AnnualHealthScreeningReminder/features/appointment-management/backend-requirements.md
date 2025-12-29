# Appointment Management - Backend Requirements

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/appointments | List appointments |
| POST | /api/appointments | Book appointment |
| PUT | /api/appointments/{id} | Update appointment |
| DELETE | /api/appointments/{id} | Cancel appointment |
| PUT | /api/appointments/{id}/complete | Mark completed |
| GET | /api/providers | Search providers |

## Domain Events

| Event | Trigger | Key Data |
|-------|---------|----------|
| AppointmentBooked | New booking | appointmentId, providerId, dateTime |
| AppointmentReminderSent | Before appointment | appointmentId, reminderType |
| AppointmentCompleted | Marked done | appointmentId, servicesReceived |
| AppointmentNoShow | Missed | appointmentId, rescheduledFlag |

## Database Schema

```sql
CREATE TABLE Appointments (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ProviderId UNIQUEIDENTIFIER NOT NULL,
    ScreeningId UNIQUEIDENTIFIER,
    AppointmentDate DATETIME2 NOT NULL,
    Status NVARCHAR(50),
    Location NVARCHAR(500),
    Notes NVARCHAR(MAX),
    CreatedAt DATETIME2 NOT NULL
);
```
