# Pet Profile - Backend Requirements

## Overview
The Pet Profile feature manages comprehensive pet information within the PetCareManager application. This feature handles pet registration, profile management, weight tracking, and lifecycle events through a domain-driven design approach with event sourcing.

## Domain Model

### Entities

#### Pet (Aggregate Root)
**Properties:**
- `Id` (Guid) - Unique identifier
- `HouseholdId` (Guid) - Reference to the household
- `Name` (string) - Pet's name (required, max 100 characters)
- `Species` (enum: Dog, Cat, Bird, Fish, Reptile, SmallMammal, Other) - Type of pet
- `Breed` (string) - Specific breed (optional, max 100 characters)
- `DateOfBirth` (DateTime?) - Pet's birth date (optional)
- `Gender` (enum: Male, Female, Unknown) - Pet's gender
- `Color` (string) - Primary color/markings (optional, max 50 characters)
- `MicrochipNumber` (string?) - Microchip ID (optional, unique, max 15 characters)
- `CurrentWeight` (decimal?) - Most recent weight in kg (optional)
- `PhotoUrl` (string?) - URL to pet's photo (optional)
- `IsDeceased` (bool) - Indicates if pet has passed away
- `DateOfDeath` (DateTime?) - Date pet passed away (optional)
- `MedicalNotes` (string?) - General medical information (optional, max 2000 characters)
- `SpecialNeeds` (string?) - Special care requirements (optional, max 1000 characters)
- `CreatedAt` (DateTime) - Registration timestamp
- `UpdatedAt` (DateTime) - Last modification timestamp
- `CreatedBy` (string) - User who registered the pet
- `UpdatedBy` (string) - User who last updated the pet

#### WeightRecord (Entity)
**Properties:**
- `Id` (Guid) - Unique identifier
- `PetId` (Guid) - Foreign key to Pet
- `Weight` (decimal) - Weight in kg (required, must be > 0)
- `RecordedAt` (DateTime) - When weight was measured
- `RecordedBy` (string) - User who recorded the weight
- `Notes` (string?) - Optional notes about the weight measurement (max 500 characters)

### Value Objects

#### PetAge
**Properties:**
- `Years` (int)
- `Months` (int)

**Calculations:**
- Computed from DateOfBirth
- Returns null if DateOfBirth is not set

### Enumerations

```csharp
public enum Species
{
    Dog = 1,
    Cat = 2,
    Bird = 3,
    Fish = 4,
    Reptile = 5,
    SmallMammal = 6,
    Other = 99
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Unknown = 0
}
```

## Domain Events

### 1. PetRegistered
**Triggered when:** A new pet is added to a household

**Properties:**
- `PetId` (Guid)
- `HouseholdId` (Guid)
- `Name` (string)
- `Species` (Species)
- `Breed` (string?)
- `DateOfBirth` (DateTime?)
- `Gender` (Gender)
- `RegisteredAt` (DateTime)
- `RegisteredBy` (string)

**Event Handlers:**
- Send welcome notification to household members
- Create default health record template
- Initialize vaccination schedule based on species
- Update household statistics

### 2. PetProfileUpdated
**Triggered when:** Pet information is modified

**Properties:**
- `PetId` (Guid)
- `HouseholdId` (Guid)
- `UpdatedFields` (Dictionary<string, object>) - Changed fields and new values
- `UpdatedAt` (DateTime)
- `UpdatedBy` (string)

**Event Handlers:**
- Notify household members of changes
- Log audit trail
- Update search index
- Recalculate pet age if DateOfBirth changed

### 3. PetWeightRecorded
**Triggered when:** A new weight measurement is logged

**Properties:**
- `PetId` (Guid)
- `HouseholdId` (Guid)
- `WeightRecordId` (Guid)
- `Weight` (decimal)
- `PreviousWeight` (decimal?)
- `RecordedAt` (DateTime)
- `RecordedBy` (string)

**Event Handlers:**
- Update Pet.CurrentWeight
- Check weight thresholds and send alerts if abnormal change
- Update weight trend analytics
- Notify veterinarian if configured

### 4. PetPassedAway
**Triggered when:** A pet is marked as deceased

**Properties:**
- `PetId` (Guid)
- `HouseholdId` (Guid)
- `PetName` (string)
- `DateOfDeath` (DateTime)
- `RecordedAt` (DateTime)
- `RecordedBy` (string)

**Event Handlers:**
- Set Pet.IsDeceased = true
- Send condolence notification to household
- Archive active reminders/appointments
- Update household statistics
- Generate memorial record

## API Endpoints

### Pet Management

#### POST /api/pets
**Description:** Register a new pet

**Request Body:**
```json
{
  "householdId": "guid",
  "name": "string",
  "species": "Dog|Cat|Bird|Fish|Reptile|SmallMammal|Other",
  "breed": "string?",
  "dateOfBirth": "date?",
  "gender": "Male|Female|Unknown",
  "color": "string?",
  "microchipNumber": "string?",
  "photoUrl": "string?",
  "medicalNotes": "string?",
  "specialNeeds": "string?"
}
```

**Response:** 201 Created
```json
{
  "id": "guid",
  "householdId": "guid",
  "name": "string",
  "species": "string",
  "createdAt": "datetime"
}
```

**Validations:**
- Name is required and max 100 characters
- HouseholdId must exist
- MicrochipNumber must be unique if provided
- DateOfBirth cannot be in the future

#### GET /api/pets/{id}
**Description:** Retrieve pet details

**Response:** 200 OK
```json
{
  "id": "guid",
  "householdId": "guid",
  "name": "string",
  "species": "string",
  "breed": "string?",
  "dateOfBirth": "date?",
  "age": {
    "years": "int",
    "months": "int"
  },
  "gender": "string",
  "color": "string?",
  "microchipNumber": "string?",
  "currentWeight": "decimal?",
  "photoUrl": "string?",
  "isDeceased": "bool",
  "dateOfDeath": "date?",
  "medicalNotes": "string?",
  "specialNeeds": "string?",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

#### GET /api/households/{householdId}/pets
**Description:** List all pets in a household

**Query Parameters:**
- `includeDeceased` (bool, default: false) - Include deceased pets

**Response:** 200 OK
```json
{
  "pets": [
    {
      "id": "guid",
      "name": "string",
      "species": "string",
      "breed": "string?",
      "age": { "years": "int", "months": "int" },
      "photoUrl": "string?",
      "isDeceased": "bool"
    }
  ],
  "total": "int"
}
```

#### PUT /api/pets/{id}
**Description:** Update pet profile

**Request Body:**
```json
{
  "name": "string",
  "breed": "string?",
  "dateOfBirth": "date?",
  "gender": "Male|Female|Unknown",
  "color": "string?",
  "microchipNumber": "string?",
  "photoUrl": "string?",
  "medicalNotes": "string?",
  "specialNeeds": "string?"
}
```

**Response:** 200 OK

**Validations:**
- Cannot change species after registration
- Cannot resurrect deceased pets
- MicrochipNumber must remain unique

#### DELETE /api/pets/{id}
**Description:** Soft delete a pet (archive)

**Response:** 204 No Content

**Business Rules:**
- Only soft delete (mark as inactive)
- Cannot delete if associated with active appointments
- Requires household owner permission

### Weight Tracking

#### POST /api/pets/{id}/weights
**Description:** Record a new weight measurement

**Request Body:**
```json
{
  "weight": "decimal",
  "recordedAt": "datetime",
  "notes": "string?"
}
```

**Response:** 201 Created
```json
{
  "id": "guid",
  "petId": "guid",
  "weight": "decimal",
  "recordedAt": "datetime",
  "recordedBy": "string"
}
```

**Validations:**
- Weight must be > 0
- RecordedAt cannot be in the future
- Cannot record weight for deceased pets

#### GET /api/pets/{id}/weights
**Description:** Retrieve weight history

**Query Parameters:**
- `from` (date?) - Start date filter
- `to` (date?) - End date filter
- `limit` (int, default: 50, max: 100) - Number of records

**Response:** 200 OK
```json
{
  "weights": [
    {
      "id": "guid",
      "weight": "decimal",
      "recordedAt": "datetime",
      "recordedBy": "string",
      "notes": "string?"
    }
  ],
  "trend": {
    "currentWeight": "decimal",
    "averageWeight": "decimal",
    "changePercentage": "decimal",
    "direction": "Increasing|Decreasing|Stable"
  }
}
```

### Lifecycle Events

#### POST /api/pets/{id}/mark-deceased
**Description:** Mark a pet as deceased

**Request Body:**
```json
{
  "dateOfDeath": "date"
}
```

**Response:** 200 OK

**Validations:**
- DateOfDeath cannot be before DateOfBirth
- DateOfDeath cannot be in the future
- Cannot mark already deceased pet

## Business Rules

1. **Pet Registration**
   - A pet must belong to exactly one household
   - Pet name is required but not unique across households
   - Microchip number must be globally unique when provided

2. **Weight Tracking**
   - Weight must be positive decimal
   - Cannot record weight for deceased pets
   - Weight changes > 20% from previous record trigger alerts

3. **Pet Lifecycle**
   - Once marked as deceased, most fields become read-only
   - Deceased pets can be hidden from default views but remain in database
   - Cannot schedule future appointments for deceased pets

4. **Data Integrity**
   - DateOfBirth cannot be in the future
   - DateOfDeath must be >= DateOfBirth
   - DateOfDeath cannot be in the future

5. **Authorization**
   - All household members can view pet profiles
   - Only household owner can delete pets
   - All household members can update profiles and record weights

## Database Schema

### Pets Table
```sql
CREATE TABLE Pets (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    HouseholdId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Species INT NOT NULL,
    Breed NVARCHAR(100),
    DateOfBirth DATE,
    Gender INT NOT NULL,
    Color NVARCHAR(50),
    MicrochipNumber VARCHAR(15) UNIQUE,
    CurrentWeight DECIMAL(5,2),
    PhotoUrl NVARCHAR(500),
    IsDeceased BIT NOT NULL DEFAULT 0,
    DateOfDeath DATE,
    MedicalNotes NVARCHAR(2000),
    SpecialNeeds NVARCHAR(1000),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL,
    CreatedBy NVARCHAR(100) NOT NULL,
    UpdatedBy NVARCHAR(100) NOT NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_Pets_Households FOREIGN KEY (HouseholdId)
        REFERENCES Households(Id),
    CONSTRAINT CK_Pets_DateOfDeath CHECK (DateOfDeath IS NULL OR DateOfDeath >= DateOfBirth),
    CONSTRAINT CK_Pets_Weight CHECK (CurrentWeight IS NULL OR CurrentWeight > 0)
);

CREATE INDEX IX_Pets_HouseholdId ON Pets(HouseholdId);
CREATE INDEX IX_Pets_MicrochipNumber ON Pets(MicrochipNumber) WHERE MicrochipNumber IS NOT NULL;
CREATE INDEX IX_Pets_IsDeceased ON Pets(IsDeceased);
```

### WeightRecords Table
```sql
CREATE TABLE WeightRecords (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    PetId UNIQUEIDENTIFIER NOT NULL,
    Weight DECIMAL(5,2) NOT NULL,
    RecordedAt DATETIME2 NOT NULL,
    RecordedBy NVARCHAR(100) NOT NULL,
    Notes NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL,

    CONSTRAINT FK_WeightRecords_Pets FOREIGN KEY (PetId)
        REFERENCES Pets(Id) ON DELETE CASCADE,
    CONSTRAINT CK_WeightRecords_Weight CHECK (Weight > 0)
);

CREATE INDEX IX_WeightRecords_PetId_RecordedAt ON WeightRecords(PetId, RecordedAt DESC);
```

### DomainEvents Table
```sql
CREATE TABLE DomainEvents (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    AggregateId UNIQUEIDENTIFIER NOT NULL,
    EventType NVARCHAR(100) NOT NULL,
    EventData NVARCHAR(MAX) NOT NULL,
    OccurredAt DATETIME2 NOT NULL,
    ProcessedAt DATETIME2,
    IsProcessed BIT NOT NULL DEFAULT 0,

    CONSTRAINT CK_DomainEvents_EventData CHECK (ISJSON(EventData) = 1)
);

CREATE INDEX IX_DomainEvents_AggregateId ON DomainEvents(AggregateId);
CREATE INDEX IX_DomainEvents_IsProcessed ON DomainEvents(IsProcessed);
CREATE INDEX IX_DomainEvents_EventType ON DomainEvents(EventType);
```

## Event Processing

### Event Storage
- All domain events are stored in DomainEvents table
- Events are stored as JSON in EventData column
- Events are processed asynchronously by event handlers

### Event Handlers Pattern
```csharp
public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}
```

### Retry Policy
- Failed event handlers retry up to 3 times
- Exponential backoff: 1s, 5s, 30s
- Dead letter queue after all retries exhausted

## Integration Points

1. **Notification Service**
   - Send notifications for all domain events
   - Configure per-household notification preferences

2. **File Storage Service**
   - Store and retrieve pet photos
   - Generate thumbnails

3. **Analytics Service**
   - Track weight trends
   - Generate health insights

4. **Veterinary Integration**
   - Optional integration to share records
   - Sync vaccination schedules

## Performance Considerations

1. **Caching Strategy**
   - Cache pet profiles for 5 minutes
   - Invalidate cache on PetProfileUpdated event
   - Use distributed cache for multi-instance deployments

2. **Query Optimization**
   - Index on HouseholdId for fast household pet lookup
   - Compound index on PetId + RecordedAt for weight history
   - Consider read replicas for analytics queries

3. **Data Retention**
   - Keep all weight records indefinitely
   - Archive domain events older than 1 year
   - Retain deceased pet records permanently

## Security

1. **Authorization Rules**
   - Users can only access pets in their households
   - Household owners have elevated privileges
   - API keys for veterinary integration must be encrypted

2. **Data Protection**
   - Encrypt microchip numbers at rest
   - Encrypt medical notes containing sensitive information
   - Audit log all access to pet records

## Testing Requirements

1. **Unit Tests**
   - Domain model validation logic
   - Event handler business logic
   - Value object calculations (e.g., PetAge)

2. **Integration Tests**
   - API endpoint responses
   - Database transactions
   - Event publishing and handling

3. **Performance Tests**
   - List pets for household with 100+ pets
   - Weight history retrieval for 1000+ records
   - Concurrent weight recording

## Monitoring & Logging

1. **Metrics to Track**
   - Pet registration rate
   - Weight recording frequency
   - Event processing latency
   - API response times

2. **Alerts**
   - Failed domain event processing
   - Abnormal weight changes detected
   - API error rate > 5%

## Future Enhancements

1. Photo gallery support (multiple photos per pet)
2. Pet ancestry/pedigree tracking
3. AI-powered breed identification from photos
4. Integration with pet insurance providers
5. Social features (pet playdates, community)
