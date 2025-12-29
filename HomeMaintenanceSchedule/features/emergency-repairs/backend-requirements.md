# Emergency Repairs - Backend Requirements

## Overview
Manage urgent home repairs with priority handling, rapid service provider access, and real-time status tracking for emergencies like burst pipes, power outages, or HVAC failures.

## Domain Model

### EmergencyRepair Entity
```csharp
public class EmergencyRepair : BaseEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public EmergencyType Type { get; set; }
    public EmergencySeverity Severity { get; set; }
    public RepairStatus Status { get; set; }
    public DateTime ReportedDate { get; set; }
    public DateTime? ResponseDate { get; set; }
    public DateTime? ResolvedDate { get; set; }
    public Guid PropertyId { get; set; }
    public Guid? AssignedProviderId { get; set; }
    public string TemporarySolution { get; set; }
    public List<string> PhotoUrls { get; set; }
    public List<string> VideoUrls { get; set; }
    public decimal? EstimatedCost { get; set; }
    public decimal? ActualCost { get; set; }
    public string InsuranceClaimNumber { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    // Navigation
    public Property Property { get; set; }
    public ServiceProvider AssignedProvider { get; set; }
}
```

### Enumerations
```csharp
public enum EmergencyType
{
    PlumbingLeak,
    BurstPipe,
    PowerOutage,
    HVACFailure,
    RoofLeak,
    GasLeak,
    StructuralDamage,
    FloodingBasement,
    NoHeat,
    NoAC,
    SewerBackup,
    Other
}

public enum EmergencySeverity
{
    Critical,     // Life/property threatening, immediate
    Urgent,       // Major inconvenience, same day
    Moderate,     // Significant issue, 24-48 hours
    Low           // Minor emergency, within week
}

public enum RepairStatus
{
    Reported,
    Contacted,
    ProviderEnRoute,
    TemporaryFix,
    InProgress,
    Resolved,
    InsuranceClaim
}
```

## API Endpoints

### Commands
- `POST /api/emergency-repairs` - Report new emergency
- `PUT /api/emergency-repairs/{id}` - Update emergency
- `POST /api/emergency-repairs/{id}/assign-provider` - Assign provider
- `POST /api/emergency-repairs/{id}/update-status` - Update status
- `POST /api/emergency-repairs/{id}/resolve` - Mark resolved
- `POST /api/emergency-repairs/{id}/temporary-fix` - Record temporary solution

### Queries
- `GET /api/emergency-repairs` - Get all emergencies (filtered)
- `GET /api/emergency-repairs/{id}` - Get emergency by ID
- `GET /api/emergency-repairs/active` - Get active emergencies
- `GET /api/emergency-repairs/history` - Get resolved emergencies

## Business Logic

### Priority Handling
- Critical: Immediate notification, 24/7 provider contact
- Urgent: Within 1 hour response time
- Moderate: Same day response
- Low: Next business day

### Automated Notifications
- Send SMS/Email immediately on emergency report
- Alert preferred emergency providers
- Escalate if no response within timeframe
- Update stakeholders on status changes

### Response Time Tracking
- Track time from report to provider contact
- Track time from contact to arrival
- Track time from arrival to resolution
- Generate response time analytics

### Insurance Integration
- Flag emergencies for insurance claims
- Document damage with photos/videos
- Track claim numbers
- Generate insurance reports

## Validation Rules
- Title: Required, max 200 characters
- Type: Required, valid enum
- Severity: Required, valid enum
- ReportedDate: Required, cannot be future
- PhotoUrls: Max 10 photos
- VideoUrls: Max 3 videos, max 100MB each

## Testing Requirements
- Unit tests for priority logic
- Integration tests for notification workflow
- E2E tests for emergency reporting flow

---

**Version**: 1.0
**Last Updated**: 2025-12-29
