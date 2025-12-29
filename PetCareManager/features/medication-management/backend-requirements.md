# Medication Management - Backend Requirements

## Overview
The Medication Management feature enables pet owners to track, schedule, and manage medications for their pets with automated reminders and supply monitoring.

## Domain Events

### 1. MedicationPrescribed
Triggered when a new medication is prescribed to a pet.

**Event Properties:**
- `MedicationId` (Guid): Unique identifier for the medication
- `PetId` (Guid): Pet receiving the medication
- `PrescriptionDate` (DateTime): When the medication was prescribed
- `MedicationName` (string): Name of the medication
- `Dosage` (string): Dosage amount (e.g., "50mg", "2 tablets")
- `Frequency` (string): How often to administer (e.g., "Twice daily", "Every 8 hours")
- `Route` (string): Administration method (e.g., "Oral", "Topical", "Injectable")
- `StartDate` (DateTime): When to begin medication
- `EndDate` (DateTime?): When to stop (null for ongoing)
- `VeterinarianId` (Guid): Prescribing veterinarian
- `Instructions` (string): Special instructions
- `InitialQuantity` (int): Starting supply quantity
- `RefillThreshold` (int): Quantity that triggers refill alert
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- StartDate must be >= PrescriptionDate
- EndDate must be > StartDate if specified
- InitialQuantity must be > 0
- RefillThreshold should be < InitialQuantity
- Frequency must be a valid frequency pattern

**Triggered By:**
- Veterinarian prescribing medication
- Manual medication entry by pet owner

### 2. MedicationAdministered
Triggered when a dose of medication is given to a pet.

**Event Properties:**
- `AdministrationId` (Guid): Unique identifier for this administration
- `MedicationId` (Guid): Medication being administered
- `PetId` (Guid): Pet receiving the dose
- `ScheduledTime` (DateTime): When the dose was scheduled
- `ActualTime` (DateTime): When the dose was actually given
- `AdministeredBy` (string): Person who gave the medication
- `DosageGiven` (string): Actual dosage administered
- `Notes` (string): Any observations or notes
- `RemainingQuantity` (int): Quantity left after administration
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- MedicationId must reference an active medication
- ActualTime should be within reasonable window of ScheduledTime
- RemainingQuantity must be >= 0
- Cannot administer discontinued medication
- Must verify sufficient supply exists

**Triggered By:**
- User marking scheduled dose as complete
- Manual dose recording

**Side Effects:**
- May trigger MedicationRefillNeeded if RemainingQuantity <= RefillThreshold
- Updates medication schedule for next dose

### 3. MedicationMissed
Triggered when a scheduled medication dose was not administered.

**Event Properties:**
- `MissedDoseId` (Guid): Unique identifier for the missed dose
- `MedicationId` (Guid): Medication that was missed
- `PetId` (Guid): Pet that missed the dose
- `ScheduledTime` (DateTime): When the dose was scheduled
- `MissedTime` (DateTime): When the miss was detected
- `Reason` (string?): Optional reason for missing dose
- `NotificationSent` (bool): Whether owner was notified
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- MissedTime must be > ScheduledTime
- Should track consecutive missed doses
- Grace period before marking as missed (e.g., 2 hours)
- Cannot mark future doses as missed

**Triggered By:**
- System check detecting unconfirmed scheduled dose
- User manually marking dose as missed

**Side Effects:**
- Sends notification to pet owner
- May trigger veterinarian alert for critical medications
- Updates adherence statistics

### 4. MedicationRefillNeeded
Triggered when medication supply falls below the refill threshold.

**Event Properties:**
- `RefillAlertId` (Guid): Unique identifier for the alert
- `MedicationId` (Guid): Medication needing refill
- `PetId` (Guid): Pet for whom medication is needed
- `CurrentQuantity` (int): Current supply amount
- `RefillThreshold` (int): Threshold that triggered alert
- `EstimatedDaysRemaining` (int): Calculated days until out
- `RefillSource` (string): Where to obtain refill (pharmacy, vet)
- `RefillInstructions` (string): How to order/obtain refill
- `Priority` (string): "Low", "Medium", "High", "Critical"
- `NotificationSent` (bool): Whether owner was notified
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- CurrentQuantity <= RefillThreshold
- EstimatedDaysRemaining calculated based on dosage frequency
- Priority based on days remaining and medication criticality
- Should not spam multiple alerts for same refill need
- Alert resolution tracked when refill obtained

**Triggered By:**
- MedicationAdministered event reducing quantity below threshold
- Manual quantity adjustment
- Scheduled inventory check

**Side Effects:**
- Sends notification to pet owner
- Creates refill task/reminder
- May integrate with pharmacy auto-refill

### 5. MedicationDiscontinued
Triggered when a medication is stopped or cancelled.

**Event Properties:**
- `MedicationId` (Guid): Medication being discontinued
- `PetId` (Guid): Pet whose medication is stopped
- `DiscontinuedDate` (DateTime): When medication was stopped
- `DiscontinuedBy` (Guid): User who discontinued (owner or vet)
- `Reason` (string): Reason for discontinuation
- `ReasonCategory` (string): "Completed", "Adverse Reaction", "Ineffective", "Other"
- `WasCompletedAsScheduled` (bool): Whether full course was completed
- `FinalNotes` (string): Any final observations
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- Cannot discontinue already discontinued medication
- DiscontinuedDate must be >= medication start date
- Should capture reason for discontinuation
- Cancels all future scheduled doses
- Preserves historical administration records

**Triggered By:**
- Veterinarian ending prescription
- Medication course completion
- Adverse reaction or ineffectiveness
- Owner stopping medication

**Side Effects:**
- Cancels all pending scheduled doses
- Removes from active medication list
- Sends confirmation notification
- Archives medication record

## Aggregates

### Medication Aggregate
**Root Entity:** Medication

**Entities:**
- Medication (root)
- AdministrationSchedule
- AdministrationHistory
- RefillHistory

**Value Objects:**
- Dosage (Amount, Unit)
- Frequency (Interval, TimesPerDay, SpecificTimes)
- MedicationSupply (CurrentQuantity, RefillThreshold, Unit)
- PrescriptionDetails (VeterinarianId, PrescriptionNumber, PrescriptionDate)

**Invariants:**
- Active medication must have valid schedule
- Cannot administer more than available supply
- Schedule must align with frequency pattern
- Discontinued medications cannot be modified

### Pet Aggregate Extension
Medication management extends the Pet aggregate with:
- MedicationList (collection of active medications)
- MedicationHistory (past medications)
- AdherenceStatistics

## API Endpoints

### Commands

#### POST /api/medications/prescribe
Prescribe new medication for a pet.

**Request:**
```json
{
  "petId": "guid",
  "medicationName": "string",
  "dosage": {
    "amount": "string",
    "unit": "string"
  },
  "frequency": {
    "type": "Daily|Hourly|Weekly|Custom",
    "interval": "number",
    "timesPerDay": "number",
    "specificTimes": ["HH:mm"]
  },
  "route": "Oral|Topical|Injectable|Other",
  "startDate": "datetime",
  "endDate": "datetime?",
  "veterinarianId": "guid",
  "instructions": "string",
  "initialQuantity": "number",
  "refillThreshold": "number",
  "refillSource": "string"
}
```

**Response:** 201 Created with MedicationId

#### POST /api/medications/{medicationId}/administer
Record medication administration.

**Request:**
```json
{
  "scheduledTime": "datetime",
  "actualTime": "datetime",
  "administeredBy": "string",
  "dosageGiven": "string",
  "notes": "string"
}
```

**Response:** 200 OK

#### POST /api/medications/{medicationId}/mark-missed
Mark a scheduled dose as missed.

**Request:**
```json
{
  "scheduledTime": "datetime",
  "reason": "string"
}
```

**Response:** 200 OK

#### POST /api/medications/{medicationId}/refill
Record medication refill.

**Request:**
```json
{
  "quantityAdded": "number",
  "refillDate": "datetime",
  "refillSource": "string",
  "cost": "decimal?"
}
```

**Response:** 200 OK

#### POST /api/medications/{medicationId}/discontinue
Discontinue a medication.

**Request:**
```json
{
  "discontinuedDate": "datetime",
  "reason": "string",
  "reasonCategory": "Completed|AdverseReaction|Ineffective|Other",
  "finalNotes": "string"
}
```

**Response:** 200 OK

### Queries

#### GET /api/pets/{petId}/medications/active
Get all active medications for a pet.

**Response:**
```json
{
  "medications": [
    {
      "medicationId": "guid",
      "name": "string",
      "dosage": "string",
      "frequency": "string",
      "nextDueTime": "datetime",
      "currentQuantity": "number",
      "daysSupplyRemaining": "number",
      "needsRefill": "bool"
    }
  ]
}
```

#### GET /api/pets/{petId}/medications/schedule
Get medication schedule for a specific date range.

**Query Parameters:**
- startDate (required)
- endDate (required)

**Response:**
```json
{
  "schedule": [
    {
      "medicationId": "guid",
      "medicationName": "string",
      "scheduledTime": "datetime",
      "status": "Pending|Administered|Missed",
      "dosage": "string"
    }
  ]
}
```

#### GET /api/medications/{medicationId}/history
Get administration history for a medication.

**Query Parameters:**
- page (default: 1)
- pageSize (default: 20)

**Response:**
```json
{
  "history": [
    {
      "administrationId": "guid",
      "scheduledTime": "datetime",
      "actualTime": "datetime",
      "administeredBy": "string",
      "dosageGiven": "string",
      "notes": "string"
    }
  ],
  "totalCount": "number",
  "adherenceRate": "decimal"
}
```

#### GET /api/pets/{petId}/medications/adherence
Get medication adherence statistics.

**Query Parameters:**
- periodDays (default: 30)

**Response:**
```json
{
  "period": {
    "startDate": "datetime",
    "endDate": "datetime"
  },
  "overallAdherenceRate": "decimal",
  "totalScheduledDoses": "number",
  "administeredCount": "number",
  "missedCount": "number",
  "medicationBreakdown": [
    {
      "medicationId": "guid",
      "medicationName": "string",
      "adherenceRate": "decimal",
      "scheduledDoses": "number",
      "administeredDoses": "number",
      "missedDoses": "number"
    }
  ]
}
```

#### GET /api/medications/refills-needed
Get all medications needing refill.

**Query Parameters:**
- userId (to filter by owner)
- priority (optional filter)

**Response:**
```json
{
  "refillsNeeded": [
    {
      "medicationId": "guid",
      "medicationName": "string",
      "petId": "guid",
      "petName": "string",
      "currentQuantity": "number",
      "estimatedDaysRemaining": "number",
      "priority": "string",
      "refillSource": "string"
    }
  ]
}
```

## Background Services

### MedicationScheduleService
- Generates scheduled doses based on frequency
- Runs daily to create next 7 days of schedule
- Handles timezone conversions for scheduled times

### MedicationReminderService
- Sends notifications before scheduled doses
- Configurable reminder window (e.g., 30 min before)
- Supports multiple notification channels (push, email, SMS)

### MissedDoseDetectionService
- Runs hourly to check for missed doses
- Grace period before marking as missed
- Triggers MedicationMissed events
- Escalates for critical medications

### RefillMonitorService
- Monitors medication supply levels
- Projects days remaining based on usage
- Triggers MedicationRefillNeeded events
- Tracks refill alert history to avoid spam

## Data Models

### Medication Entity
```csharp
public class Medication
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public string Name { get; set; }
    public string Dosage { get; set; }
    public string Route { get; set; }
    public Frequency Frequency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid VeterinarianId { get; set; }
    public string Instructions { get; set; }
    public MedicationSupply Supply { get; set; }
    public MedicationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DiscontinuedAt { get; set; }
    public string DiscontinuationReason { get; set; }
}
```

### AdministrationRecord Entity
```csharp
public class AdministrationRecord
{
    public Guid Id { get; set; }
    public Guid MedicationId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime? ActualTime { get; set; }
    public string AdministeredBy { get; set; }
    public string DosageGiven { get; set; }
    public string Notes { get; set; }
    public AdministrationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### RefillRecord Entity
```csharp
public class RefillRecord
{
    public Guid Id { get; set; }
    public Guid MedicationId { get; set; }
    public DateTime RefillDate { get; set; }
    public int QuantityAdded { get; set; }
    public string RefillSource { get; set; }
    public decimal? Cost { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

## Validation Rules

### Medication Prescription
- Medication name required (max 200 chars)
- Dosage required (max 100 chars)
- Valid frequency pattern required
- Start date cannot be in past > 30 days
- End date must be after start date
- Initial quantity must be positive
- Refill threshold must be less than initial quantity

### Medication Administration
- Cannot administer discontinued medication
- Actual time must be <= current time
- Dosage given required
- Must have sufficient supply

### Refill Recording
- Quantity added must be positive
- Refill date must be <= current date
- Source required

## Security & Authorization

### Permissions
- **PetOwner**: Can manage medications for their own pets
- **Veterinarian**: Can prescribe medications for pets under their care
- **Admin**: Full access to all medication records

### Access Rules
- Users can only view/modify medications for pets they own or care for
- Veterinarians can only prescribe for pets they are treating
- Medication history is read-only after 24 hours
- Audit trail for all medication operations

## Integration Points

### Veterinary System Integration
- Import prescriptions from vet practice management systems
- Export medication history for vet appointments
- Bi-directional sync of prescription data

### Pharmacy Integration
- Auto-refill integration with partner pharmacies
- Pricing lookup for medications
- Refill delivery tracking

### Notification System
- Push notifications for scheduled doses
- Email summaries for missed doses
- SMS alerts for critical refills

### Calendar Integration
- Export medication schedule to calendar apps
- iCal format support
- Sync with family calendars

## Performance Requirements

- Schedule generation: < 1 second for 30-day period
- Medication list query: < 200ms
- Administration recording: < 300ms
- Support 10,000+ active medications
- Handle 100,000+ administration records
- Real-time notification delivery < 5 seconds

## Error Handling

### Business Rule Violations
- Return 400 Bad Request with detailed error message
- Include validation errors in response
- Provide corrective action guidance

### Not Found
- Return 404 for invalid medication/pet IDs
- Clear error messages

### Concurrency
- Optimistic concurrency control using row versions
- Return 409 Conflict for concurrent modifications
- Client must refresh and retry

### System Errors
- Log detailed error information
- Return 500 with generic message to client
- Alert monitoring system for critical failures
