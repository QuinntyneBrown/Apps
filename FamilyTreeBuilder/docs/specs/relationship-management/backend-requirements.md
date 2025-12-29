# Relationship Management - Backend Requirements

## Domain Model

### Relationship Aggregate
- **RelationshipId**: Guid
- **Person1Id**: Guid
- **Person2Id**: Guid
- **RelationshipType**: enum (Parent, Child, Spouse, Sibling, Adoptive)
- **StartDate**: DateTime?
- **EndDate**: DateTime?
- **IsActive**: bool

### Marriage Entity
- **MarriageId**: Guid
- **Spouse1Id**: Guid
- **Spouse2Id**: Guid
- **MarriageDate**: DateTime
- **MarriagePlace**: string
- **Status**: enum (Current, Divorced, Widowed)

## Commands
- EstablishRelationshipCommand
- RecordMarriageCommand
- RecordDivorceCommand
- RecordAdoptionCommand

## Domain Events
- RelationshipEstablished
- MarriageRecorded
- DivorceRecorded
- AdoptionRecorded

## API Endpoints
- POST /api/relationships
- POST /api/relationships/marriage
- PUT /api/relationships/{id}/divorce
- GET /api/relationships/person/{personId}
