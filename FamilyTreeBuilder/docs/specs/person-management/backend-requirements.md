# Person Management - Backend Requirements

## Overview
Manages family member profiles including biographical information, life events, and genealogical data.

## Domain Model

### Person Aggregate
- **PersonId**: Unique identifier (Guid)
- **UserId**: Tree owner (Guid)
- **FullName**: Full name (string)
- **GivenName**: First name (string)
- **Surname**: Last name (string)
- **MiddleName**: Middle name (string, nullable)
- **MaidenName**: Maiden name (string, nullable)
- **Gender**: Gender (enum: Male, Female, Unknown)
- **BirthDate**: Date of birth (DateTime, nullable)
- **BirthPlace**: Place of birth (string)
- **IsLiving**: Living status (bool)
- **DeathDate**: Date of death (DateTime, nullable)
- **DeathPlace**: Place of death (string, nullable)
- **BurialPlace**: Burial location (string, nullable)
- **Occupation**: Career/occupation (string)
- **Education**: Education details (string)
- **Generation**: Generation level in tree (int)
- **Biography**: Life story (string)
- **CreatedAt**: Creation timestamp (DateTime)
- **UpdatedAt**: Last modification (DateTime)

## Commands

### AddPersonCommand
- Validates required fields (name, birth date)
- Creates Person aggregate
- Calculates generation level
- Raises **PersonAdded** domain event
- Returns PersonId

### UpdatePersonDetailsCommand
- Validates PersonId exists
- Updates biographical information
- Tracks changed fields
- Raises **PersonDetailsUpdated** domain event

### RecordDeathCommand
- Validates PersonId exists
- Sets IsLiving to false
- Records death details
- Raises **DeathRecorded** domain event

### MergePersonCommand
- Validates both PersonIds exist
- Merges duplicate person records
- Resolves data conflicts
- Updates relationships
- Raises **PersonMerged** domain event

## Domain Events

### PersonAdded
```csharp
public class PersonAdded : DomainEvent
{
    public Guid PersonId { get; set; }
    public string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string BirthPlace { get; set; }
    public bool IsLiving { get; set; }
    public int Generation { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### PersonDetailsUpdated
```csharp
public class PersonDetailsUpdated : DomainEvent
{
    public Guid PersonId { get; set; }
    public Dictionary<string, object> UpdatedFields { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### DeathRecorded
```csharp
public class DeathRecorded : DomainEvent
{
    public Guid PersonId { get; set; }
    public DateTime DeathDate { get; set; }
    public string DeathPlace { get; set; }
    public string BurialPlace { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### POST /api/persons
- Adds new person to tree
- Returns: 201 Created with PersonId

### PUT /api/persons/{personId}
- Updates person details
- Returns: 200 OK

### POST /api/persons/{personId}/death
- Records death information
- Returns: 200 OK

### POST /api/persons/merge
- Merges duplicate persons
- Request: { primaryId, mergeId }
- Returns: 200 OK

### GET /api/persons/{personId}
- Gets person details
- Returns: 200 OK with PersonDto

### GET /api/persons/tree/{treeId}
- Lists all persons in tree
- Supports filtering and sorting
- Returns: PersonDto list
