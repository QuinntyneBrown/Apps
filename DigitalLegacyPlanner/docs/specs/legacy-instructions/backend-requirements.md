# Legacy Instructions - Backend Requirements

## Overview
Backend services for creating and managing legacy instructions for digital accounts, including memorialization preferences, data downloads, and content distribution.

## API Endpoints

### Instructions Management

#### POST /api/instructions
Create legacy instructions for an account
- **Request Body**:
  ```json
  {
    "accountId": "guid",
    "preferredAction": "close|memorialize|preserve|transfer",
    "detailedSteps": "string",
    "priority": "high|medium|low",
    "notes": "string"
  }
  ```
- **Response**: `201 Created` with instruction object
- **Events**: Triggers `LegacyInstructionsCreated`

#### PUT /api/instructions/{instructionId}/memorialization
Set memorialization preferences
- **Request Body**:
  ```json
  {
    "accountId": "guid",
    "memorializeAccount": "boolean",
    "memorialSettings": {
      "allowComments": "boolean",
      "displayProfile": "boolean",
      "customMessage": "string"
    }
  }
  ```
- **Response**: `200 OK`
- **Events**: Triggers `MemorializationPreferenceSet`

#### POST /api/instructions/data-download
Create data download instructions
- **Request Body**:
  ```json
  {
    "accountId": "guid",
    "dataTypesToPreserve": ["photos", "videos", "posts", "messages"],
    "downloadMethod": "platform_tool|manual|api",
    "storageLocation": "string",
    "downloadInstructions": "string"
  }
  ```
- **Response**: `201 Created`
- **Events**: Triggers `DataDownloadInstructed`

#### POST /api/instructions/content-distribution
Plan content distribution to beneficiaries
- **Request Body**:
  ```json
  {
    "accountId": "guid",
    "contentIds": ["guid"],
    "beneficiaries": [
      {
        "contactId": "guid",
        "contentTypes": ["photos", "documents"],
        "accessLevel": "view|download|full"
      }
    ],
    "distributionMethod": "email|cloud_share|physical_media",
    "notes": "string"
  }
  ```
- **Response**: `201 Created`
- **Events**: Triggers `ContentDistributionPlanned`

#### GET /api/instructions
Get all legacy instructions for user
- **Query Parameters**: `accountId`, `action`, `priority`
- **Response**: `200 OK` with array of instructions

#### GET /api/instructions/{instructionId}
Get specific instruction details
- **Response**: `200 OK` with instruction object

#### PUT /api/instructions/{instructionId}
Update existing instructions
- **Request Body**: Updated instruction fields
- **Response**: `200 OK` with updated object

#### DELETE /api/instructions/{instructionId}
Remove legacy instructions
- **Response**: `204 No Content`

## Data Models

### LegacyInstruction
```csharp
public class LegacyInstruction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AccountId { get; set; }
    public PreferredAction PreferredAction { get; set; }
    public string DetailedSteps { get; set; }
    public Priority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Notes { get; set; }
}
```

### MemorializationPreference
```csharp
public class MemorializationPreference
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public bool MemorializeAccount { get; set; }
    public MemorialSettings Settings { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class MemorialSettings
{
    public bool AllowComments { get; set; }
    public bool DisplayProfile { get; set; }
    public string CustomMessage { get; set; }
}
```

### DataDownloadInstruction
```csharp
public class DataDownloadInstruction
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public List<string> DataTypesToPreserve { get; set; }
    public DownloadMethod DownloadMethod { get; set; }
    public string StorageLocation { get; set; }
    public string DetailedInstructions { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### ContentDistributionPlan
```csharp
public class ContentDistributionPlan
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public List<Guid> ContentIds { get; set; }
    public List<BeneficiaryAssignment> Beneficiaries { get; set; }
    public DistributionMethod DistributionMethod { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class BeneficiaryAssignment
{
    public Guid ContactId { get; set; }
    public List<string> ContentTypes { get; set; }
    public AccessLevel AccessLevel { get; set; }
}
```

## Domain Events

### LegacyInstructionsCreated
```csharp
public class LegacyInstructionsCreated : DomainEvent
{
    public Guid InstructionId { get; set; }
    public Guid AccountId { get; set; }
    public PreferredAction PreferredAction { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### MemorializationPreferenceSet
```csharp
public class MemorializationPreferenceSet : DomainEvent
{
    public Guid PreferenceId { get; set; }
    public Guid AccountId { get; set; }
    public bool Memorialize { get; set; }
    public DateTime SetAt { get; set; }
}
```

### DataDownloadInstructed
```csharp
public class DataDownloadInstructed : DomainEvent
{
    public Guid InstructionId { get; set; }
    public Guid AccountId { get; set; }
    public List<string> DataTypes { get; set; }
    public string StorageLocation { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### ContentDistributionPlanned
```csharp
public class ContentDistributionPlanned : DomainEvent
{
    public Guid PlanId { get; set; }
    public Guid AccountId { get; set; }
    public int BeneficiaryCount { get; set; }
    public DateTime PlannedAt { get; set; }
}
```

## Business Logic

### Instruction Creation Service
- Validate account exists
- Check for conflicting instructions
- Generate platform-specific guidance
- Publish LegacyInstructionsCreated event

### Memorialization Service
- Validate platform supports memorialization
- Provide platform-specific settings options
- Generate memorialization workflow
- Publish MemorializationPreferenceSet event

### Data Download Service
- Validate data types for platform
- Generate download procedure
- Calculate storage requirements
- Publish DataDownloadInstructed event

### Distribution Planning Service
- Validate all beneficiaries exist
- Check content availability
- Plan distribution logistics
- Publish ContentDistributionPlanned event

## Platform-Specific Guidance

### Social Media Platforms
- **Facebook**: Memorialization vs. deletion options
- **Instagram**: Memorial account process
- **Twitter/X**: Account deactivation procedure
- **LinkedIn**: Profile removal steps

### Email Platforms
- **Gmail**: Account closure and data export (Google Takeout)
- **Outlook**: Account deletion process
- **Yahoo**: Account termination procedure

### Cloud Storage
- **Google Drive**: Data download and sharing
- **Dropbox**: Transfer ownership or download
- **OneDrive**: Account closure and data preservation

## Security Requirements

- Encrypt sensitive instructions
- Restrict access to account owner and authorized executors
- Audit all instruction access
- Validate beneficiary permissions

## Validation Rules

- Preferred action: Required, valid enum
- Detailed steps: Max 5000 characters
- Account must exist and belong to user
- Beneficiaries must be registered contacts
- Content IDs must be valid

## Performance Requirements

- Instruction creation: < 200ms
- Instruction retrieval: < 150ms
- Complex distribution planning: < 500ms

## Database Schema

### Tables
- `legacy_instructions` - Core instruction data
- `memorialization_preferences` - Platform-specific preferences
- `data_download_instructions` - Download procedures
- `content_distribution_plans` - Distribution planning
- `beneficiary_assignments` - Content-beneficiary mappings
