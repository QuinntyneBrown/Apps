# Veterinary Management - Backend Requirements

## Overview
The Veterinary Management feature enables pet owners and veterinary staff to schedule, track, and manage veterinary appointments, diagnoses, and emergency situations for pets.

## Domain Events

### 1. VetAppointmentScheduled
**Description**: Raised when a veterinary appointment is successfully booked for a pet.

**Event Properties**:
- `appointmentId` (Guid): Unique identifier for the appointment
- `petId` (Guid): ID of the pet
- `ownerId` (Guid): ID of the pet owner
- `veterinarianId` (Guid): ID of the assigned veterinarian
- `appointmentDateTime` (DateTime): Scheduled date and time
- `appointmentType` (string): Type of appointment (checkup, vaccination, surgery, consultation)
- `reason` (string): Reason for the visit
- `scheduledBy` (string): User who scheduled the appointment
- `scheduledAt` (DateTime): When the appointment was scheduled
- `status` (string): Initial status (Scheduled)

**Triggers**:
- Owner books appointment through web/mobile interface
- Veterinary staff schedules appointment on behalf of owner
- Recurring appointment is auto-scheduled

**Handlers**:
- Send confirmation email/SMS to owner
- Send notification to veterinarian
- Create calendar entry
- Update pet's medical record timeline

---

### 2. VetAppointmentCompleted
**Description**: Raised when a veterinary appointment has been attended and marked as complete.

**Event Properties**:
- `appointmentId` (Guid): Unique identifier for the appointment
- `petId` (Guid): ID of the pet
- `veterinarianId` (Guid): ID of the veterinarian who conducted the visit
- `completedAt` (DateTime): When the appointment was completed
- `duration` (int): Actual duration in minutes
- `servicesProvided` (List<string>): Services performed during visit
- `notes` (string): Visit notes and observations
- `followUpRequired` (bool): Whether follow-up is needed
- `nextAppointmentDate` (DateTime?): Recommended next visit date
- `prescriptions` (List<Prescription>): Any prescriptions issued
- `totalCost` (decimal): Total cost of visit

**Triggers**:
- Veterinarian marks appointment as complete
- Automated completion after visit notes are submitted

**Handlers**:
- Update appointment status
- Generate invoice
- Update pet's medical history
- Send visit summary to owner
- Schedule follow-up appointment if required
- Update pet's health metrics

---

### 3. DiagnosisReceived
**Description**: Raised when a pet is diagnosed with a medical condition during or after a veterinary visit.

**Event Properties**:
- `diagnosisId` (Guid): Unique identifier for the diagnosis
- `petId` (Guid): ID of the pet
- `appointmentId` (Guid?): Related appointment (if applicable)
- `veterinarianId` (Guid): ID of the diagnosing veterinarian
- `diagnosisDate` (DateTime): When diagnosis was made
- `condition` (string): Medical condition diagnosed
- `severity` (string): Severity level (Mild, Moderate, Severe, Critical)
- `symptoms` (List<string>): Observed symptoms
- `diagnosisNotes` (string): Detailed diagnosis information
- `treatmentPlan` (string): Recommended treatment plan
- `prognosis` (string): Expected outcome
- `medications` (List<Medication>): Prescribed medications
- `restrictions` (List<string>): Activity or dietary restrictions
- `followUpRequired` (bool): Whether follow-up is needed

**Triggers**:
- Veterinarian submits diagnosis during appointment
- Lab results are processed and diagnosis is confirmed
- Specialist provides diagnosis

**Handlers**:
- Create treatment plan record
- Send diagnosis report to owner
- Schedule follow-up appointments
- Alert owner of severity and care requirements
- Update pet's medical conditions list
- Notify pet insurance provider if integrated

---

### 4. VeterinaryEmergency
**Description**: Raised when a pet requires urgent veterinary attention.

**Event Properties**:
- `emergencyId` (Guid): Unique identifier for the emergency
- `petId` (Guid): ID of the pet
- `ownerId` (Guid): ID of the pet owner
- `reportedAt` (DateTime): When emergency was reported
- `emergencyType` (string): Type of emergency (Injury, Poisoning, Acute Illness, Accident)
- `symptoms` (List<string>): Emergency symptoms
- `description` (string): Detailed description of emergency
- `location` (string): Pet's current location
- `urgencyLevel` (string): Critical, High, Medium
- `reportedBy` (string): Who reported the emergency
- `contactNumber` (string): Emergency contact number
- `triageStatus` (string): Initial triage assessment
- `assignedVetId` (Guid?): Veterinarian assigned to emergency

**Triggers**:
- Owner reports emergency through app
- Automated detection (wearable device alerts)
- Veterinary staff identifies emergency during visit
- Emergency hotline call is logged

**Handlers**:
- Send immediate alert to on-call veterinarian
- Create emergency appointment (highest priority)
- Send emergency care instructions to owner
- Notify emergency response team
- Log emergency in system
- Send location of nearest emergency vet clinic
- Update pet's status to "Emergency"
- Notify emergency contacts

---

## Aggregates

### VetAppointment
**Properties**:
- AppointmentId (Guid)
- PetId (Guid)
- OwnerId (Guid)
- VeterinarianId (Guid)
- ScheduledDateTime (DateTime)
- ActualStartTime (DateTime?)
- ActualEndTime (DateTime?)
- AppointmentType (enum)
- Status (enum: Scheduled, Confirmed, InProgress, Completed, Cancelled, NoShow)
- Reason (string)
- Notes (string)
- ServicesProvided (List<Service>)
- CreatedAt (DateTime)
- UpdatedAt (DateTime)

**Methods**:
- Schedule()
- Confirm()
- StartAppointment()
- CompleteAppointment()
- CancelAppointment()
- Reschedule()

---

### Diagnosis
**Properties**:
- DiagnosisId (Guid)
- PetId (Guid)
- AppointmentId (Guid?)
- VeterinarianId (Guid)
- DiagnosisDate (DateTime)
- Condition (string)
- Severity (enum)
- Symptoms (List<string>)
- TreatmentPlan (string)
- Prognosis (string)
- Medications (List<Medication>)
- FollowUpDate (DateTime?)
- Status (enum: Active, Resolved, Chronic)

**Methods**:
- CreateDiagnosis()
- UpdateTreatmentPlan()
- MarkResolved()
- AddMedication()
- ScheduleFollowUp()

---

### VeterinaryEmergency
**Properties**:
- EmergencyId (Guid)
- PetId (Guid)
- OwnerId (Guid)
- ReportedAt (DateTime)
- EmergencyType (enum)
- Symptoms (List<string>)
- Description (string)
- UrgencyLevel (enum)
- TriageStatus (string)
- AssignedVetId (Guid?)
- ResolutionTime (DateTime?)
- Outcome (string)
- Status (enum: Reported, Triaged, InProgress, Resolved, Escalated)

**Methods**:
- ReportEmergency()
- AssignVeterinarian()
- UpdateTriageStatus()
- ResolveEmergency()
- Escalate()

---

## API Endpoints

### Appointments

#### POST /api/veterinary/appointments
**Description**: Schedule a new veterinary appointment

**Request Body**:
```json
{
  "petId": "guid",
  "veterinarianId": "guid",
  "appointmentDateTime": "2025-01-15T10:00:00Z",
  "appointmentType": "checkup",
  "reason": "Annual wellness exam"
}
```

**Response**: 201 Created
```json
{
  "appointmentId": "guid",
  "status": "scheduled",
  "confirmationNumber": "VET-2025-001234"
}
```

---

#### GET /api/veterinary/appointments/{appointmentId}
**Description**: Get appointment details

**Response**: 200 OK
```json
{
  "appointmentId": "guid",
  "petId": "guid",
  "petName": "Max",
  "veterinarianId": "guid",
  "veterinarianName": "Dr. Sarah Johnson",
  "scheduledDateTime": "2025-01-15T10:00:00Z",
  "appointmentType": "checkup",
  "status": "scheduled",
  "reason": "Annual wellness exam"
}
```

---

#### PUT /api/veterinary/appointments/{appointmentId}/complete
**Description**: Mark appointment as completed

**Request Body**:
```json
{
  "duration": 30,
  "servicesProvided": ["Physical Examination", "Vaccination"],
  "notes": "Pet is healthy. Updated vaccinations.",
  "followUpRequired": false,
  "prescriptions": []
}
```

**Response**: 200 OK

---

#### GET /api/veterinary/appointments
**Description**: List appointments with filtering

**Query Parameters**:
- `petId` (optional)
- `ownerId` (optional)
- `veterinarianId` (optional)
- `status` (optional)
- `fromDate` (optional)
- `toDate` (optional)
- `pageNumber` (default: 1)
- `pageSize` (default: 20)

**Response**: 200 OK
```json
{
  "appointments": [...],
  "totalCount": 45,
  "pageNumber": 1,
  "pageSize": 20
}
```

---

### Diagnoses

#### POST /api/veterinary/diagnoses
**Description**: Create a new diagnosis

**Request Body**:
```json
{
  "petId": "guid",
  "appointmentId": "guid",
  "veterinarianId": "guid",
  "condition": "Kennel Cough",
  "severity": "Moderate",
  "symptoms": ["Coughing", "Lethargy"],
  "diagnosisNotes": "Classic symptoms of kennel cough",
  "treatmentPlan": "Antibiotics for 7 days, rest",
  "medications": [
    {
      "name": "Doxycycline",
      "dosage": "10mg/kg",
      "frequency": "Twice daily",
      "duration": "7 days"
    }
  ]
}
```

**Response**: 201 Created

---

#### GET /api/veterinary/diagnoses/pet/{petId}
**Description**: Get all diagnoses for a pet

**Response**: 200 OK
```json
{
  "diagnoses": [
    {
      "diagnosisId": "guid",
      "diagnosisDate": "2025-01-15T10:30:00Z",
      "condition": "Kennel Cough",
      "severity": "Moderate",
      "status": "Active",
      "veterinarianName": "Dr. Sarah Johnson"
    }
  ]
}
```

---

### Emergencies

#### POST /api/veterinary/emergencies
**Description**: Report a veterinary emergency

**Request Body**:
```json
{
  "petId": "guid",
  "emergencyType": "Injury",
  "symptoms": ["Limping", "Bleeding"],
  "description": "Pet was hit by a car, bleeding from leg",
  "urgencyLevel": "Critical",
  "location": "123 Main St",
  "contactNumber": "+1-555-0123"
}
```

**Response**: 201 Created
```json
{
  "emergencyId": "guid",
  "status": "reported",
  "nearestEmergencyClinic": {
    "name": "24/7 Emergency Vet",
    "address": "456 Oak Ave",
    "phone": "+1-555-9999",
    "distance": "2.3 miles"
  },
  "estimatedWaitTime": "Immediate",
  "triageInstructions": "Keep pet calm, apply pressure to wound, transport immediately"
}
```

---

#### GET /api/veterinary/emergencies/{emergencyId}
**Description**: Get emergency details

**Response**: 200 OK

---

#### PUT /api/veterinary/emergencies/{emergencyId}/triage
**Description**: Update emergency triage status

**Request Body**:
```json
{
  "triageStatus": "Critical - Immediate attention required",
  "assignedVetId": "guid"
}
```

**Response**: 200 OK

---

## Database Schema

### VetAppointments Table
```sql
CREATE TABLE VetAppointments (
    AppointmentId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PetId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Pets(PetId),
    OwnerId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Owners(OwnerId),
    VeterinarianId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Veterinarians(VeterinarianId),
    ScheduledDateTime DATETIME2 NOT NULL,
    ActualStartTime DATETIME2 NULL,
    ActualEndTime DATETIME2 NULL,
    AppointmentType NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Reason NVARCHAR(500),
    Notes NVARCHAR(MAX),
    ConfirmationNumber NVARCHAR(50) UNIQUE,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    INDEX IX_VetAppointments_PetId (PetId),
    INDEX IX_VetAppointments_ScheduledDateTime (ScheduledDateTime),
    INDEX IX_VetAppointments_Status (Status)
);
```

### Diagnoses Table
```sql
CREATE TABLE Diagnoses (
    DiagnosisId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PetId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Pets(PetId),
    AppointmentId UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES VetAppointments(AppointmentId),
    VeterinarianId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Veterinarians(VeterinarianId),
    DiagnosisDate DATETIME2 NOT NULL,
    Condition NVARCHAR(200) NOT NULL,
    Severity NVARCHAR(50) NOT NULL,
    Symptoms NVARCHAR(MAX),
    DiagnosisNotes NVARCHAR(MAX),
    TreatmentPlan NVARCHAR(MAX),
    Prognosis NVARCHAR(500),
    FollowUpDate DATETIME2 NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    INDEX IX_Diagnoses_PetId (PetId),
    INDEX IX_Diagnoses_DiagnosisDate (DiagnosisDate)
);
```

### VeterinaryEmergencies Table
```sql
CREATE TABLE VeterinaryEmergencies (
    EmergencyId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    PetId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Pets(PetId),
    OwnerId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Owners(OwnerId),
    ReportedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    EmergencyType NVARCHAR(50) NOT NULL,
    Symptoms NVARCHAR(MAX),
    Description NVARCHAR(MAX),
    Location NVARCHAR(500),
    UrgencyLevel NVARCHAR(50) NOT NULL,
    TriageStatus NVARCHAR(200),
    AssignedVetId UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES Veterinarians(VeterinarianId),
    ResolutionTime DATETIME2 NULL,
    Outcome NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL,
    ContactNumber NVARCHAR(20),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    INDEX IX_VeterinaryEmergencies_PetId (PetId),
    INDEX IX_VeterinaryEmergencies_Status (Status),
    INDEX IX_VeterinaryEmergencies_ReportedAt (ReportedAt)
);
```

### Medications Table
```sql
CREATE TABLE Medications (
    MedicationId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    DiagnosisId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Diagnoses(DiagnosisId),
    Name NVARCHAR(200) NOT NULL,
    Dosage NVARCHAR(100) NOT NULL,
    Frequency NVARCHAR(100) NOT NULL,
    Duration NVARCHAR(100),
    Instructions NVARCHAR(500),
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
```

---

## Business Rules

1. **Appointment Scheduling**:
   - Appointments must be scheduled at least 1 hour in advance (except emergencies)
   - Maximum 4 appointments per hour per veterinarian
   - Appointments cannot be scheduled for past dates
   - Owner must confirm appointment within 24 hours or it's auto-cancelled

2. **Emergency Handling**:
   - Critical emergencies get automatic priority
   - Emergency appointments override regular appointments
   - On-call veterinarian must be notified within 2 minutes
   - Emergency status must be updated every 15 minutes

3. **Diagnoses**:
   - Only licensed veterinarians can create diagnoses
   - Diagnosis must be linked to appointment or emergency
   - Severe/Critical diagnoses trigger automatic owner notification
   - Diagnoses with prescriptions require follow-up scheduling

4. **Data Retention**:
   - Appointment records retained for 7 years
   - Diagnosis records retained indefinitely
   - Emergency records retained for 10 years

---

## Integration Points

1. **Email/SMS Service**: Appointment confirmations and reminders
2. **Calendar Service**: Sync appointments to owner/vet calendars
3. **Payment Service**: Process appointment and service payments
4. **Insurance Provider**: Submit claims for covered procedures
5. **Lab Systems**: Import test results
6. **Pharmacy Systems**: Send prescriptions
7. **Emergency Services**: Nearest clinic locator

---

## Security & Permissions

- **Owners**: Can schedule appointments, view their pet's records, report emergencies
- **Veterinarians**: Full access to appointments, diagnoses, and medical records
- **Veterinary Staff**: Can schedule appointments, view records, cannot create diagnoses
- **Emergency Responders**: Can view and update emergency records only
- **Admin**: Full access to all features and data

---

## Performance Requirements

- Appointment booking response time: < 500ms
- Emergency report processing: < 100ms
- Emergency veterinarian notification: < 2 minutes
- Support 1000 concurrent users
- Handle 10,000 appointments per day
