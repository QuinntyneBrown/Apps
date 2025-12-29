# Trusted Contacts - Backend Requirements

## Overview
Backend services for managing digital executors, emergency contacts, and access permissions.

## API Endpoints

#### POST /api/executors
Designate a digital executor
- **Request Body**: `{ name, email, phone, scopeOfAuthority, accessLevel }`
- **Response**: `201 Created`
- **Events**: `DigitalExecutorDesignated`

#### POST /api/emergency-contacts
Add emergency contact
- **Request Body**: `{ name, email, phone, relationship, authorityLevel }`
- **Response**: `201 Created`
- **Events**: `EmergencyContactAdded`

#### POST /api/permissions
Grant access permissions
- **Request Body**: `{ granteeId, accountIds, accessScope, activationConditions }`
- **Response**: `201 Created`
- **Events**: `AccessPermissionGranted`

#### POST /api/executors/{executorId}/notify
Notify executor of their role
- **Request Body**: `{ notificationMethod, message }`
- **Response**: `200 OK`
- **Events**: `ExecutorNotified`

#### GET /api/executors
Get all executors and contacts
- **Response**: `200 OK` with contacts list

## Data Models

```csharp
public class DigitalExecutor
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string ScopeOfAuthority { get; set; }
    public AccessLevel AccessLevel { get; set; }
    public bool IsNotified { get; set; }
    public bool HasAccepted { get; set; }
    public DateTime DesignatedAt { get; set; }
}

public class AccessPermission
{
    public Guid Id { get; set; }
    public Guid GranteeId { get; set; }
    public List<Guid> AccountIds { get; set; }
    public AccessScope Scope { get; set; }
    public string ActivationConditions { get; set; }
    public DateTime GrantedAt { get; set; }
}
```

## Domain Events

```csharp
public class DigitalExecutorDesignated : DomainEvent
{
    public Guid ExecutorId { get; set; }
    public string ExecutorName { get; set; }
    public AccessLevel AccessLevel { get; set; }
}

public class ExecutorNotified : DomainEvent
{
    public Guid ExecutorId { get; set; }
    public string NotificationMethod { get; set; }
    public bool AcceptanceReceived { get; set; }
}
```

## Security Requirements
- Verify email addresses before granting access
- Require acceptance confirmation from executors
- Encrypt contact information
- Audit all permission grants
- Support multi-factor authentication for high-access executors
