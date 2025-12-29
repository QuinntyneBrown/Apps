# Grooming Management - Backend Requirements

## Overview
The Grooming Management feature enables pet owners to schedule professional grooming appointments, track grooming sessions, and log at-home grooming activities for their pets with reminder notifications and grooming history tracking.

## Domain Events

### 1. GroomingAppointmentScheduled
Triggered when a grooming appointment is booked for a pet.

**Event Properties:**
- `AppointmentId` (Guid): Unique identifier for the appointment
- `PetId` (Guid): Pet receiving grooming service
- `AppointmentDate` (DateTime): Date and time of appointment
- `GroomerId` (Guid): Professional groomer ID
- `GroomerName` (string): Name of groomer/salon
- `ServiceType` (string): Type of grooming (e.g., "Full Groom", "Bath Only", "Nail Trim")
- `Services` (List<string>): Specific services included
- `EstimatedDuration` (int): Expected duration in minutes
- `Price` (decimal?): Cost of service
- `Notes` (string): Special instructions or requirements
- `ReminderTime` (DateTime): When to send reminder
- `BookedBy` (Guid): User who booked appointment
- `Location` (string): Salon address or mobile groomer
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- AppointmentDate must be in the future
- EstimatedDuration must be > 0
- ServiceType required
- Cannot double-book same pet at same time
- At least one service must be specified

**Triggered By:**
- User booking appointment online
- Admin/groomer scheduling appointment
- Import from grooming salon system

### 2. GroomingCompleted
Triggered when a grooming session is finished (professional or home).

**Event Properties:**
- `SessionId` (Guid): Unique identifier for the grooming session
- `PetId` (Guid): Pet that was groomed
- `CompletionDate` (DateTime): When grooming was completed
- `SessionType` (string): "Professional" or "Home"
- `GroomerId` (Guid?): Groomer ID if professional
- `GroomerName` (string): Groomer or person who performed grooming
- `ServicesPerformed` (List<string>): Services completed
- `DurationMinutes` (int): Actual time taken
- `Cost` (decimal?): Actual cost paid
- `PaymentMethod` (string?): How payment was made
- `NextRecommendedDate` (DateTime?): When next grooming recommended
- `BehaviorNotes` (string): Pet's behavior during session
- `ConditionNotes` (string): Observations about coat, skin, nails
- `PhotosUrls` (List<string>): Before/after photos
- `Rating` (int?): Quality rating 1-5
- `AppointmentId` (Guid?): Reference to appointment if applicable
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- CompletionDate must be <= current date
- DurationMinutes must be > 0
- At least one service must be performed
- If from appointment, AppointmentId must be valid
- Rating must be 1-5 if provided
- Cost must be >= 0 if provided

**Triggered By:**
- User marking appointment as complete
- User logging completed home grooming
- Auto-completion after appointment time
- Import from grooming salon system

**Side Effects:**
- If from appointment, marks appointment as completed
- Calculates and suggests next grooming date
- Updates grooming history
- May trigger GroomingRecommended if overdue

### 3. HomeGroomingLogged
Triggered when at-home grooming activity is recorded.

**Event Properties:**
- `HomeGroomingId` (Guid): Unique identifier
- `PetId` (Guid): Pet that was groomed
- `GroomingDate` (DateTime): When grooming was performed
- `PerformedBy` (string): Person who did the grooming
- `ActivitiesPerformed` (List<string>): Grooming activities done
- `DurationMinutes` (int): Time spent
- `ProductsUsed` (List<string>): Shampoos, tools, etc.
- `Notes` (string): Any observations or notes
- `PhotosUrls` (List<string>): Photos of results
- `NextPlannedDate` (DateTime?): When planning next home grooming
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- GroomingDate must be <= current date
- At least one activity must be performed
- DurationMinutes must be > 0
- PerformedBy required

**Triggered By:**
- User manually logging home grooming activity
- Quick log from grooming tracker

**Side Effects:**
- Updates last grooming date
- Updates grooming history
- May adjust next recommended grooming date

## Additional Domain Events

### 4. GroomingAppointmentCancelled
Triggered when a scheduled appointment is cancelled.

**Event Properties:**
- `AppointmentId` (Guid): Cancelled appointment ID
- `PetId` (Guid): Pet for the appointment
- `CancellationDate` (DateTime): When cancelled
- `CancelledBy` (Guid): Who cancelled
- `Reason` (string): Reason for cancellation
- `CancellationFee` (decimal?): Fee charged if applicable
- `RescheduledTo` (DateTime?): New appointment time if rescheduled
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- Cannot cancel already completed appointment
- Cannot cancel past appointments without admin override
- CancellationDate must be <= current date

**Triggered By:**
- User cancelling appointment
- Groomer cancelling appointment
- Admin cancellation

**Side Effects:**
- Marks appointment as cancelled
- Sends cancellation notification
- May trigger refund process

### 5. GroomingRecommended
Triggered when system determines pet needs grooming.

**Event Properties:**
- `RecommendationId` (Guid): Unique identifier
- `PetId` (Guid): Pet needing grooming
- `RecommendedDate` (DateTime): When grooming is recommended
- `Reason` (string): Why grooming is recommended
- `Priority` (string): "Low", "Medium", "High", "Overdue"
- `LastGroomingDate` (DateTime): When last groomed
- `DaysSinceLastGrooming` (int): Days elapsed
- `SuggestedServices` (List<string>): Recommended services
- `NotificationSent` (bool): Whether notification was sent
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- RecommendedDate should be >= current date
- DaysSinceLastGrooming must be > 0
- Priority based on days since last grooming and pet breed

**Triggered By:**
- Scheduled background job checking grooming due dates
- Completion of grooming session (calculates next date)
- Manual recommendation by user

**Side Effects:**
- Sends notification to pet owner
- Creates reminder

## Aggregates

### GroomingAppointment Aggregate
**Root Entity:** GroomingAppointment

**Entities:**
- GroomingAppointment (root)
- AppointmentService
- AppointmentReminder

**Value Objects:**
- GroomingLocation (SalonName, Address, Phone, IsMobile)
- AppointmentStatus (Scheduled, Confirmed, InProgress, Completed, Cancelled, NoShow)
- Price (Amount, Currency, IsPaid)

**Invariants:**
- Scheduled appointment must have future date
- Completed appointment cannot be modified
- Cancelled appointment cannot be completed
- Services list cannot be empty

### GroomingSession Aggregate
**Root Entity:** GroomingSession

**Entities:**
- GroomingSession (root)
- ServiceRecord
- PhotoGallery

**Value Objects:**
- GroomingDuration (StartTime, EndTime, TotalMinutes)
- GroomerDetails (GroomerId, Name, Specialty, Rating)
- PetCondition (CoatCondition, SkinCondition, NailsCondition, EarsCondition)
- GroomingCost (Amount, PaymentMethod, IsPaid, PaidDate)

**Invariants:**
- Session must have completion date
- At least one service must be recorded
- Duration must be positive
- Professional sessions must have groomer

### Pet Aggregate Extension
Grooming management extends the Pet aggregate with:
- GroomingSchedule (preferred frequency)
- GroomingHistory (past sessions)
- PreferredGroomer
- GroomingPreferences (sensitivities, special needs)
- NextGroomingRecommendation

## API Endpoints

### Commands

#### POST /api/grooming/appointments/schedule
Schedule a new grooming appointment.

**Request:**
```json
{
  "petId": "guid",
  "appointmentDate": "datetime",
  "groomerId": "guid",
  "groomerName": "string",
  "serviceType": "string",
  "services": ["string"],
  "estimatedDuration": "number",
  "price": "decimal",
  "location": {
    "salonName": "string",
    "address": "string",
    "phone": "string",
    "isMobile": "bool"
  },
  "notes": "string",
  "reminderMinutesBefore": "number"
}
```

**Response:** 201 Created with AppointmentId

#### POST /api/grooming/appointments/{appointmentId}/complete
Mark appointment as completed.

**Request:**
```json
{
  "completionDate": "datetime",
  "servicesPerformed": ["string"],
  "durationMinutes": "number",
  "actualCost": "decimal",
  "paymentMethod": "string",
  "behaviorNotes": "string",
  "conditionNotes": "string",
  "rating": "number",
  "photoUrls": ["string"],
  "nextRecommendedDate": "datetime"
}
```

**Response:** 200 OK

#### POST /api/grooming/appointments/{appointmentId}/cancel
Cancel a scheduled appointment.

**Request:**
```json
{
  "reason": "string",
  "cancellationFee": "decimal",
  "rescheduleToDate": "datetime"
}
```

**Response:** 200 OK

#### POST /api/grooming/home-grooming/log
Log an at-home grooming session.

**Request:**
```json
{
  "petId": "guid",
  "groomingDate": "datetime",
  "performedBy": "string",
  "activitiesPerformed": ["string"],
  "durationMinutes": "number",
  "productsUsed": ["string"],
  "notes": "string",
  "photoUrls": ["string"],
  "nextPlannedDate": "datetime"
}
```

**Response:** 201 Created with HomeGroomingId

#### PUT /api/grooming/appointments/{appointmentId}
Update appointment details.

**Request:**
```json
{
  "appointmentDate": "datetime",
  "services": ["string"],
  "notes": "string",
  "price": "decimal"
}
```

**Response:** 200 OK

### Queries

#### GET /api/pets/{petId}/grooming/appointments
Get all appointments for a pet.

**Query Parameters:**
- status (optional filter: scheduled, completed, cancelled)
- startDate (optional)
- endDate (optional)

**Response:**
```json
{
  "appointments": [
    {
      "appointmentId": "guid",
      "appointmentDate": "datetime",
      "groomerName": "string",
      "serviceType": "string",
      "services": ["string"],
      "status": "string",
      "price": "decimal",
      "location": "string"
    }
  ]
}
```

#### GET /api/pets/{petId}/grooming/history
Get grooming history for a pet.

**Query Parameters:**
- page (default: 1)
- pageSize (default: 20)
- sessionType (optional: Professional, Home, All)

**Response:**
```json
{
  "history": [
    {
      "sessionId": "guid",
      "completionDate": "datetime",
      "sessionType": "string",
      "groomerName": "string",
      "servicesPerformed": ["string"],
      "cost": "decimal",
      "rating": "number",
      "photoCount": "number"
    }
  ],
  "totalCount": "number",
  "lastGroomingDate": "datetime",
  "averageFrequency": "number"
}
```

#### GET /api/pets/{petId}/grooming/upcoming
Get upcoming grooming appointments and recommendations.

**Response:**
```json
{
  "nextAppointment": {
    "appointmentId": "guid",
    "appointmentDate": "datetime",
    "groomerName": "string",
    "services": ["string"],
    "location": "string"
  },
  "recommendation": {
    "recommendedDate": "datetime",
    "priority": "string",
    "daysSinceLastGrooming": "number",
    "suggestedServices": ["string"]
  }
}
```

#### GET /api/grooming/appointments/{appointmentId}
Get detailed appointment information.

**Response:**
```json
{
  "appointmentId": "guid",
  "petId": "guid",
  "petName": "string",
  "appointmentDate": "datetime",
  "groomerId": "guid",
  "groomerName": "string",
  "serviceType": "string",
  "services": ["string"],
  "estimatedDuration": "number",
  "price": "decimal",
  "location": {
    "salonName": "string",
    "address": "string",
    "phone": "string",
    "isMobile": "bool"
  },
  "notes": "string",
  "status": "string",
  "reminderTime": "datetime",
  "createdAt": "datetime"
}
```

#### GET /api/pets/{petId}/grooming/statistics
Get grooming statistics and insights.

**Query Parameters:**
- periodMonths (default: 12)

**Response:**
```json
{
  "period": {
    "startDate": "datetime",
    "endDate": "datetime"
  },
  "totalSessions": "number",
  "professionalSessions": "number",
  "homeSessions": "number",
  "totalSpent": "decimal",
  "averageSessionCost": "decimal",
  "averageFrequencyDays": "number",
  "mostCommonServices": ["string"],
  "preferredGroomer": {
    "groomerId": "guid",
    "name": "string",
    "sessionCount": "number",
    "averageRating": "decimal"
  }
}
```

#### GET /api/groomers
Get list of available groomers.

**Query Parameters:**
- location (optional)
- serviceType (optional)
- rating (optional minimum rating)

**Response:**
```json
{
  "groomers": [
    {
      "groomerId": "guid",
      "name": "string",
      "businessName": "string",
      "rating": "decimal",
      "reviewCount": "number",
      "specialties": ["string"],
      "location": "string",
      "isMobile": "bool",
      "priceRange": "string",
      "availability": "string"
    }
  ]
}
```

## Background Services

### AppointmentReminderService
- Sends reminders before scheduled appointments
- Configurable reminder window (e.g., 24 hours, 1 hour before)
- Supports multiple notification channels (push, email, SMS)
- Handles timezone conversions

### GroomingRecommendationService
- Runs daily to check pets needing grooming
- Calculates recommended grooming dates based on:
  - Last grooming date
  - Pet breed grooming requirements
  - User-defined preferences
  - Coat type and length
- Triggers GroomingRecommended events
- Adjusts priority based on days overdue

### AppointmentFollowUpService
- Sends follow-up after appointments
- Requests completion confirmation
- Prompts for rating/review
- Suggests next appointment date

### GroomingAnalyticsService
- Aggregates grooming statistics
- Calculates trends and patterns
- Identifies cost-saving opportunities
- Tracks groomer performance

## Data Models

### GroomingAppointment Entity
```csharp
public class GroomingAppointment
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public Guid GroomerId { get; set; }
    public string GroomerName { get; set; }
    public string ServiceType { get; set; }
    public List<string> Services { get; set; }
    public int EstimatedDuration { get; set; }
    public decimal? Price { get; set; }
    public GroomingLocation Location { get; set; }
    public string Notes { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime? ReminderTime { get; set; }
    public Guid BookedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public string CancellationReason { get; set; }
}
```

### GroomingSession Entity
```csharp
public class GroomingSession
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime CompletionDate { get; set; }
    public SessionType SessionType { get; set; }
    public Guid? GroomerId { get; set; }
    public string GroomerName { get; set; }
    public List<string> ServicesPerformed { get; set; }
    public int DurationMinutes { get; set; }
    public decimal? Cost { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime? NextRecommendedDate { get; set; }
    public string BehaviorNotes { get; set; }
    public string ConditionNotes { get; set; }
    public List<string> PhotoUrls { get; set; }
    public int? Rating { get; set; }
    public Guid? AppointmentId { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### HomeGroomingLog Entity
```csharp
public class HomeGroomingLog
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime GroomingDate { get; set; }
    public string PerformedBy { get; set; }
    public List<string> ActivitiesPerformed { get; set; }
    public int DurationMinutes { get; set; }
    public List<string> ProductsUsed { get; set; }
    public string Notes { get; set; }
    public List<string> PhotoUrls { get; set; }
    public DateTime? NextPlannedDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### Groomer Entity
```csharp
public class Groomer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BusinessName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public bool IsMobile { get; set; }
    public List<string> Specialties { get; set; }
    public List<string> ServicesOffered { get; set; }
    public string PriceRange { get; set; }
    public decimal AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public string Certifications { get; set; }
    public int YearsExperience { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## Validation Rules

### Appointment Scheduling
- Appointment date must be in future
- Pet ID must be valid
- Service type required (max 100 chars)
- At least one service required
- Estimated duration must be > 0 and < 480 minutes
- Price must be >= 0 if provided
- Groomer name required (max 200 chars)

### Grooming Completion
- Completion date must be <= current date
- At least one service performed required
- Duration must be > 0 and < 1440 minutes
- Rating must be 1-5 if provided
- Cost must be >= 0 if provided

### Home Grooming Logging
- Grooming date must be <= current date
- Performed by required (max 100 chars)
- At least one activity required
- Duration must be > 0 and < 1440 minutes

## Security & Authorization

### Permissions
- **PetOwner**: Can manage grooming for their own pets
- **Groomer**: Can view and update appointments for their clients
- **Admin**: Full access to all grooming records

### Access Rules
- Users can only view/modify grooming records for pets they own
- Groomers can only access appointments assigned to them
- Appointment cancellation within 24 hours may require admin approval
- Audit trail for all grooming operations

## Integration Points

### Grooming Salon Integration
- Import appointments from salon booking systems
- Export appointment confirmations
- Sync groomer availability
- Pricing and service catalog sync

### Payment Processing
- Integrate with payment gateways for online booking
- Track payment status
- Generate receipts
- Handle refunds for cancellations

### Calendar Integration
- Export appointments to calendar apps
- iCal format support
- Sync with family calendars
- Automatic reminder integration

### Photo Storage
- Cloud storage for grooming photos
- Before/after photo galleries
- Compression and optimization
- Secure access controls

### Notification System
- Push notifications for appointment reminders
- Email confirmations and receipts
- SMS alerts for urgent reminders
- In-app notifications

## Performance Requirements

- Appointment booking: < 500ms
- History query: < 300ms
- Dashboard load: < 400ms
- Support 50,000+ grooming records
- Handle 10,000+ appointments
- Real-time notification delivery < 5 seconds
- Photo upload: < 10 seconds per image

## Error Handling

### Business Rule Violations
- Return 400 Bad Request with detailed error message
- Include validation errors in response
- Provide corrective action guidance

### Not Found
- Return 404 for invalid appointment/session IDs
- Clear error messages

### Concurrency
- Optimistic concurrency control using row versions
- Return 409 Conflict for concurrent modifications
- Client must refresh and retry

### System Errors
- Log detailed error information
- Return 500 with generic message to client
- Alert monitoring system for critical failures
