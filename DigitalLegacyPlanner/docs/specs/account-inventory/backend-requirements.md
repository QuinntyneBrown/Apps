# Account Inventory - Backend Requirements

## Overview
Backend services for managing digital account inventory, categorization, access details storage, and value assessment.

## API Endpoints

### Account Management

#### POST /api/accounts/register
Register a new digital account in the legacy inventory
- **Request Body**:
  ```json
  {
    "serviceName": "string",
    "accountType": "social|financial|email|storage|entertainment|professional|other",
    "importanceLevel": "critical|high|medium|low",
    "username": "string",
    "notes": "string"
  }
  ```
- **Response**: `201 Created` with account object
- **Events**: Triggers `DigitalAccountRegistered`

#### PUT /api/accounts/{accountId}/categorize
Categorize and prioritize an account
- **Request Body**:
  ```json
  {
    "category": "string",
    "legacyPriority": "critical|high|medium|low",
    "closurePreference": "close|memorialize|preserve|transfer",
    "notes": "string"
  }
  ```
- **Response**: `200 OK` with updated account
- **Events**: Triggers `AccountCategorized`

#### POST /api/accounts/{accountId}/access-details
Add encrypted access details for an account
- **Request Body**:
  ```json
  {
    "accessMethod": "password|oauth|sso|key|other",
    "passwordLocationReference": "string",
    "has2FA": "boolean",
    "twoFactorDetails": "string",
    "recoveryInfo": "string",
    "securityQuestions": "string"
  }
  ```
- **Response**: `201 Created`
- **Events**: Triggers `AccountAccessDetailsAdded`
- **Security**: Encrypt all credential data before storage

#### PUT /api/accounts/{accountId}/value
Assess the value of an account
- **Request Body**:
  ```json
  {
    "monetaryValue": "decimal",
    "sentimentalValue": "high|medium|low",
    "contentWorthPreserving": "boolean",
    "valuationNotes": "string"
  }
  ```
- **Response**: `200 OK` with updated account
- **Events**: Triggers `AccountValueAssessed`

#### GET /api/accounts
Retrieve all accounts for the user
- **Query Parameters**: `category`, `priority`, `type`
- **Response**: `200 OK` with array of accounts

#### GET /api/accounts/{accountId}
Get details for a specific account
- **Response**: `200 OK` with account object (credentials decrypted for authorized users)

#### DELETE /api/accounts/{accountId}
Remove an account from inventory
- **Response**: `204 No Content`

## Data Models

### DigitalAccount
```csharp
public class DigitalAccount
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ServiceName { get; set; }
    public AccountType AccountType { get; set; }
    public ImportanceLevel ImportanceLevel { get; set; }
    public string Username { get; set; }
    public string Category { get; set; }
    public LegacyPriority LegacyPriority { get; set; }
    public ClosurePreference ClosurePreference { get; set; }
    public decimal? MonetaryValue { get; set; }
    public SentimentalValue? SentimentalValue { get; set; }
    public bool ContentWorthPreserving { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Notes { get; set; }
}
```

### AccountAccessDetails (Encrypted)
```csharp
public class AccountAccessDetails
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public AccessMethod AccessMethod { get; set; }
    public string EncryptedPasswordLocation { get; set; }
    public bool Has2FA { get; set; }
    public string EncryptedTwoFactorDetails { get; set; }
    public string EncryptedRecoveryInfo { get; set; }
    public string EncryptedSecurityQuestions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

## Domain Events

### DigitalAccountRegistered
```csharp
public class DigitalAccountRegistered : DomainEvent
{
    public Guid AccountId { get; set; }
    public Guid UserId { get; set; }
    public string ServiceName { get; set; }
    public AccountType AccountType { get; set; }
    public ImportanceLevel ImportanceLevel { get; set; }
    public string Username { get; set; }
    public DateTime RegisteredAt { get; set; }
}
```

### AccountCategorized
```csharp
public class AccountCategorized : DomainEvent
{
    public Guid AccountId { get; set; }
    public string Category { get; set; }
    public LegacyPriority LegacyPriority { get; set; }
    public ClosurePreference ClosurePreference { get; set; }
    public DateTime CategorizedAt { get; set; }
}
```

### AccountAccessDetailsAdded
```csharp
public class AccountAccessDetailsAdded : DomainEvent
{
    public Guid AccountId { get; set; }
    public AccessMethod AccessMethod { get; set; }
    public bool Has2FA { get; set; }
    public DateTime AddedAt { get; set; }
}
```

### AccountValueAssessed
```csharp
public class AccountValueAssessed : DomainEvent
{
    public Guid AccountId { get; set; }
    public decimal? MonetaryValue { get; set; }
    public SentimentalValue? SentimentalValue { get; set; }
    public bool ContentWorthPreserving { get; set; }
    public DateTime AssessedAt { get; set; }
}
```

## Business Logic

### Account Registration Service
- Validate service name and account type
- Check for duplicate accounts
- Generate unique account identifier
- Publish DigitalAccountRegistered event

### Categorization Service
- Validate category and priority values
- Update account classification
- Publish AccountCategorized event

### Access Details Service
- Encrypt all sensitive credential information using AES-256
- Store encryption key references securely
- Validate access method details
- Support key rotation for encrypted data
- Publish AccountAccessDetailsAdded event

### Value Assessment Service
- Calculate total portfolio value across accounts
- Identify high-value accounts for priority handling
- Track valuation history
- Publish AccountValueAssessed event

## Security Requirements

### Encryption
- Use AES-256 encryption for all credentials
- Implement key management with AWS KMS or Azure Key Vault
- Encrypt data at rest and in transit
- Support per-user encryption keys

### Access Control
- Only account owner can view decrypted credentials
- Executors gain access only after emergency activation
- Audit all credential access attempts
- Implement rate limiting on credential access

### Data Protection
- Never log decrypted credentials
- Implement secure credential deletion
- Support credential export for password managers
- Comply with data protection regulations

## Integration Points

### Password Manager Import
- Support import from 1Password, LastPass, Bitwarden
- Parse CSV/JSON export formats
- Auto-categorize based on service recognition
- Bulk account registration

### Service Recognition
- Maintain database of known services
- Auto-populate service metadata (logo, type, category)
- Provide standardized account naming

## Performance Requirements

- Account registration: < 200ms
- Credential encryption: < 100ms
- Account retrieval: < 150ms
- Bulk import: Handle 500+ accounts in < 10 seconds

## Database Schema

### Tables
- `digital_accounts` - Core account information
- `account_access_details` - Encrypted credentials
- `account_categories` - Category definitions
- `account_valuations` - Historical value assessments

### Indexes
- `idx_accounts_user_id` - For user account queries
- `idx_accounts_category` - For category filtering
- `idx_accounts_priority` - For priority sorting
- `idx_access_details_account_id` - For credential lookups

## Validation Rules

- Service name: Required, max 200 characters
- Username: Required, max 200 characters
- Account type: Must be valid enum value
- Importance level: Must be valid enum value
- Monetary value: Non-negative decimal
- Category: Max 100 characters
