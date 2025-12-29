# Vaccination Management - Backend Requirements

## Overview
The Vaccination Management feature tracks pet vaccinations, manages vaccination schedules, sends reminders for upcoming boosters, and maintains comprehensive vaccination records for pets.

## Domain Model

### Entities

#### Vaccination
- **VaccinationId** (Guid): Unique identifier
- **PetId** (Guid): Reference to the pet
- **VaccineName** (string): Name of the vaccine (e.g., "Rabies", "DHPP", "Bordetella")
- **VaccineType** (enum): Type categorization (Core, NonCore, Lifestyle)
- **AdministeredDate** (DateTime): Date the vaccine was given
- **AdministeredBy** (string): Veterinarian or clinic name
- **BatchNumber** (string): Vaccine batch/lot number
- **ExpirationDate** (DateTime): Vaccine expiration date
- **NextDueDate** (DateTime): When the next booster is due
- **AdministrationSite** (string): Where the vaccine was given (e.g., "Left shoulder")
- **Dosage** (string): Dosage amount (e.g., "1.0 mL")
- **AdverseReactions** (string): Any reported reactions
- **CertificateNumber** (string): Official vaccination certificate number
- **Notes** (string): Additional notes
- **CreatedDate** (DateTime): Record creation timestamp
- **ModifiedDate** (DateTime): Last modification timestamp
- **IsActive** (bool): Soft delete flag

#### VaccinationSchedule
- **ScheduleId** (Guid): Unique identifier
- **PetId** (Guid): Reference to the pet
- **VaccineName** (string): Name of the scheduled vaccine
- **DueDate** (DateTime): When the vaccination is due
- **ReminderDate** (DateTime): When to send reminder
- **IsCompleted** (bool): Whether vaccination was administered
- **CompletedVaccinationId** (Guid?): Link to completed vaccination record
- **Priority** (enum): Priority level (Critical, High, Medium, Low)
- **CreatedDate** (DateTime): Record creation timestamp
- **ModifiedDate** (DateTime): Last modification timestamp

#### VaccinationTemplate
- **TemplateId** (Guid): Unique identifier
- **VaccineName** (string): Name of the vaccine
- **Species** (enum): Dog, Cat, Bird, Rabbit, etc.
- **FirstDoseAgeWeeks** (int): Age in weeks for first dose
- **BoosterIntervalMonths** (int): Months between boosters
- **IsCore** (bool): Whether this is a core vaccine
- **Description** (string): Vaccine description and purpose
- **IsActive** (bool): Whether template is active

### Value Objects

#### VaccineType (Enum)
- Core: Essential vaccines required for all pets
- NonCore: Recommended based on lifestyle and risk factors
- Lifestyle: Based on specific pet activities or location

#### Priority (Enum)
- Critical: Overdue or legally required
- High: Due within 2 weeks
- Medium: Due within 1-3 months
- Low: Due in more than 3 months

#### Species (Enum)
- Dog
- Cat
- Bird
- Rabbit
- Ferret
- Other

## Domain Events

### VaccinationAdministered
Raised when a vaccine is given to a pet.

**Properties:**
- VaccinationId (Guid)
- PetId (Guid)
- PetName (string)
- OwnerId (Guid)
- VaccineName (string)
- AdministeredDate (DateTime)
- NextDueDate (DateTime)
- AdministeredBy (string)
- CertificateNumber (string)
- EventTimestamp (DateTime)

**Triggers:**
- Create vaccination record in the system
- Update pet's vaccination history
- Create next scheduled booster if applicable
- Send confirmation notification to owner
- Update pet health dashboard

### VaccinationDue
Raised when a booster vaccination is approaching its due date.

**Properties:**
- ScheduleId (Guid)
- PetId (Guid)
- PetName (string)
- OwnerId (Guid)
- VaccineName (string)
- DueDate (DateTime)
- DaysUntilDue (int)
- Priority (Priority)
- LastAdministeredDate (DateTime?)
- EventTimestamp (DateTime)

**Triggers:**
- Send reminder notification to owner
- Display on dashboard reminders section
- Create calendar event
- Send email/SMS reminder based on user preferences
- Update reminder status

### VaccinationOverdue
Raised when a required vaccination has not been given by its due date.

**Properties:**
- ScheduleId (Guid)
- PetId (Guid)
- PetName (string)
- OwnerId (Guid)
- VaccineName (string)
- DueDate (DateTime)
- DaysOverdue (int)
- IsLegallyRequired (bool)
- Priority (Priority)
- LastAdministeredDate (DateTime?)
- EventTimestamp (DateTime)

**Triggers:**
- Send urgent notification to owner
- Escalate priority level
- Display prominent alert on dashboard
- Send multiple reminder channels (email, SMS, push)
- Flag pet record for attention
- Alert if legally required vaccination (e.g., rabies)

## API Endpoints

### Commands

#### POST /api/vaccinations
Create a new vaccination record.
```json
{
  "petId": "guid",
  "vaccineName": "string",
  "vaccineType": "Core|NonCore|Lifestyle",
  "administeredDate": "datetime",
  "administeredBy": "string",
  "batchNumber": "string",
  "expirationDate": "datetime",
  "nextDueDate": "datetime",
  "administrationSite": "string",
  "dosage": "string",
  "adverseReactions": "string",
  "certificateNumber": "string",
  "notes": "string"
}
```

#### PUT /api/vaccinations/{id}
Update an existing vaccination record.

#### DELETE /api/vaccinations/{id}
Soft delete a vaccination record.

#### POST /api/vaccination-schedules
Create a vaccination schedule entry.
```json
{
  "petId": "guid",
  "vaccineName": "string",
  "dueDate": "datetime",
  "reminderDate": "datetime",
  "priority": "Critical|High|Medium|Low"
}
```

#### PUT /api/vaccination-schedules/{id}/complete
Mark a scheduled vaccination as completed.
```json
{
  "vaccinationId": "guid"
}
```

#### POST /api/vaccination-schedules/generate
Generate vaccination schedule for a pet based on templates.
```json
{
  "petId": "guid",
  "species": "Dog|Cat|Bird|Rabbit|Ferret|Other",
  "birthDate": "datetime",
  "includeNonCore": "bool"
}
```

### Queries

#### GET /api/vaccinations/pet/{petId}
Get all vaccination records for a specific pet.

**Query Parameters:**
- includeInactive (bool): Include soft-deleted records

#### GET /api/vaccinations/{id}
Get a specific vaccination record by ID.

#### GET /api/vaccination-schedules/pet/{petId}
Get vaccination schedule for a specific pet.

**Query Parameters:**
- includeCompleted (bool): Include completed vaccinations
- startDate (DateTime): Filter from date
- endDate (DateTime): Filter to date

#### GET /api/vaccination-schedules/due
Get all vaccinations due within a specified period.

**Query Parameters:**
- days (int): Number of days to look ahead (default: 30)
- ownerId (Guid): Filter by owner

#### GET /api/vaccination-schedules/overdue
Get all overdue vaccinations.

**Query Parameters:**
- ownerId (Guid): Filter by owner
- priority (Priority): Filter by priority level

#### GET /api/vaccination-templates
Get available vaccination templates.

**Query Parameters:**
- species (Species): Filter by species
- coreOnly (bool): Only core vaccines

#### GET /api/vaccinations/report/{petId}
Generate a comprehensive vaccination report for a pet.

**Response:**
```json
{
  "petId": "guid",
  "petName": "string",
  "species": "string",
  "vaccinations": [
    {
      "vaccineName": "string",
      "administeredDate": "datetime",
      "nextDueDate": "datetime",
      "administeredBy": "string",
      "certificateNumber": "string"
    }
  ],
  "upcomingSchedule": [
    {
      "vaccineName": "string",
      "dueDate": "datetime",
      "daysUntilDue": "int",
      "priority": "string"
    }
  ],
  "complianceStatus": "UpToDate|DueSoon|Overdue"
}
```

## Business Rules

### Vaccination Records
1. **Unique Constraint**: A pet cannot have duplicate vaccination records for the same vaccine on the same date
2. **Date Validation**: Administered date cannot be in the future
3. **Next Due Date**: Must be after the administered date
4. **Batch Number**: Required for legal compliance tracking
5. **Adverse Reactions**: Must be recorded within 24 hours if any occur
6. **Certificate Number**: Required for legally mandated vaccines (e.g., rabies)

### Vaccination Schedules
1. **Auto-Generation**: When VaccinationAdministered event is raised, automatically create next booster schedule if applicable
2. **Reminder Timing**: Default reminder is 2 weeks before due date, configurable per vaccine
3. **Priority Escalation**:
   - Changes to High when due date is within 2 weeks
   - Changes to Critical when overdue
4. **Legal Requirements**: Rabies and other legally required vaccines have Critical priority
5. **Completion**: Once marked complete, schedule entry is linked to vaccination record

### Notifications
1. **VaccinationDue**: Sent 2 weeks, 1 week, and 3 days before due date
2. **VaccinationOverdue**: Sent on due date, then every week until completed
3. **Legal Vaccines**: Additional urgent notifications for legally required vaccines
4. **Multi-Channel**: Use owner's preferred notification channels (email, SMS, push)

## Data Validation

### Vaccination
- VaccineName: Required, max 100 characters
- AdministeredDate: Required, cannot be future date
- AdministeredBy: Required, max 200 characters
- BatchNumber: Required for core vaccines, max 50 characters
- NextDueDate: Must be after AdministeredDate
- Dosage: Max 50 characters
- CertificateNumber: Required for rabies and legally mandated vaccines, max 100 characters

### VaccinationSchedule
- VaccineName: Required, max 100 characters
- DueDate: Required, cannot be in the past when creating
- ReminderDate: Must be before DueDate
- Priority: Required, valid enum value

## Integration Points

### Event Handlers
1. **PetRegistered Event**: Generate initial vaccination schedule based on species and age
2. **PetBirthDateUpdated Event**: Recalculate vaccination schedule
3. **OwnerPreferencesUpdated Event**: Update notification preferences

### External Systems
1. **Notification Service**: Send email, SMS, and push notifications
2. **Calendar Service**: Create calendar events for vaccination appointments
3. **Reporting Service**: Generate vaccination certificates and compliance reports
4. **Veterinary Clinic Integration**: Import vaccination records from clinics (future)

## Security & Authorization

### Permissions
- **Owner**: Full CRUD on their pets' vaccination records
- **Veterinarian**: Create and update vaccination records for assigned pets
- **Admin**: Full access to all vaccination data
- **Viewer**: Read-only access to vaccination records

### Data Protection
- Vaccination records contain medical information - ensure HIPAA-like protection
- Audit log all changes to vaccination records
- Encrypt sensitive data (batch numbers, certificate numbers)

## Performance Considerations

### Indexing
- Index on PetId for fast pet-specific queries
- Index on DueDate for schedule queries
- Index on OwnerId for owner dashboard queries
- Composite index on (PetId, AdministeredDate) for chronological queries

### Caching
- Cache vaccination templates (rarely change)
- Cache pet vaccination summary for dashboard display
- Cache overdue vaccination counts for notifications

### Background Jobs
- **Daily Job**: Check for due and overdue vaccinations, raise appropriate events
- **Weekly Job**: Generate upcoming vaccination reports for owners
- **Monthly Job**: Generate compliance reports for admin review

## Testing Requirements

### Unit Tests
- Vaccination entity validation
- Domain event creation and properties
- Business rule enforcement
- Date calculations for due dates and reminders

### Integration Tests
- End-to-end API workflows
- Event handler processing
- Database operations and transactions
- External service integrations

### Test Scenarios
1. **Happy Path**: Record vaccination, verify event raised, check schedule created
2. **Overdue Detection**: Create schedule with past due date, verify overdue event
3. **Reminder Generation**: Verify reminders sent at correct intervals
4. **Duplicate Prevention**: Attempt to create duplicate vaccination record
5. **Legal Compliance**: Verify rabies vaccination triggers appropriate priority
6. **Adverse Reaction**: Record adverse reaction, verify notification sent

## Migration Strategy

### Database Migrations
1. Create Vaccination table with indexes
2. Create VaccinationSchedule table with indexes
3. Create VaccinationTemplate table with seed data
4. Add foreign key constraints to Pet table

### Seed Data
- Pre-populate VaccinationTemplate table with common vaccines for dogs and cats
- Include core vaccines: Rabies, DHPP (dogs), FVRCP (cats), Bordetella
- Include non-core vaccines based on common use cases

## Monitoring & Observability

### Metrics
- Number of vaccinations recorded per day
- Number of overdue vaccinations by priority
- Notification delivery success rate
- Average time between due date and administration
- Compliance rate by vaccine type

### Alerts
- Alert when overdue vaccinations exceed threshold
- Alert on notification delivery failures
- Alert on adverse reaction reports

## Future Enhancements
1. Veterinary clinic integration for automatic record import
2. QR code vaccination certificates
3. AI-based vaccination schedule recommendations
4. Integration with pet insurance systems
5. Multi-language support for international users
6. Vaccine effectiveness tracking and population analytics
