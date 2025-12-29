# Appliance Tracking - Backend Requirements

## Overview
Comprehensive appliance inventory management with warranty tracking, maintenance schedules, manual storage, and replacement planning.

## Domain Model

### Appliance Entity
```csharp
public class Appliance : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ApplianceCategory Category { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal PurchaseCost { get; set; }
    public string PurchaseLocation { get; set; }
    public DateTime? WarrantyExpiration { get; set; }
    public string ManualUrl { get; set; }
    public string Location { get; set; }
    public DateTime? InstallationDate { get; set; }
    public int ExpectedLifespanYears { get; set; }
    public ApplianceCondition Condition { get; set; }
    public string Notes { get; set; }
    public List<string> PhotoUrls { get; set; }
    public Guid PropertyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    // Navigation
    public Property Property { get; set; }
    public ICollection<ApplianceService> ServiceHistory { get; set; }
}
```

### ApplianceService Entity
```csharp
public class ApplianceService : BaseEntity
{
    public Guid Id { get; set; }
    public Guid ApplianceId { get; set; }
    public DateTime ServiceDate { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public Guid? ServiceProviderId { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public Appliance Appliance { get; set; }
    public ServiceProvider ServiceProvider { get; set; }
}
```

### Enumerations
```csharp
public enum ApplianceCategory
{
    HVAC,
    Kitchen,
    Laundry,
    WaterHeater,
    Refrigeration,
    Cooking,
    Dishwashing,
    SmallAppliance,
    Other
}

public enum ApplianceCondition
{
    Excellent,
    Good,
    Fair,
    Poor,
    NeedsReplacement
}
```

## API Endpoints

### Commands
- `POST /api/appliances` - Add new appliance
- `PUT /api/appliances/{id}` - Update appliance
- `DELETE /api/appliances/{id}` - Delete appliance
- `POST /api/appliances/{id}/services` - Record service
- `POST /api/appliances/{id}/upload-manual` - Upload manual PDF

### Queries
- `GET /api/appliances` - Get all appliances (filtered by category, property)
- `GET /api/appliances/{id}` - Get appliance by ID
- `GET /api/appliances/expiring-warranties` - Get appliances with expiring warranties (next 90 days)
- `GET /api/appliances/replacement-planning` - Get appliances nearing end of lifespan
- `GET /api/appliances/{id}/service-history` - Get service history

## Business Logic

### Warranty Expiration Alerts
- Send alert 90 days before expiration
- Send alert 30 days before expiration
- Send alert 7 days before expiration
- Mark as expired when date passed

### Replacement Planning
- Calculate age: `CurrentDate - PurchaseDate`
- Replacement recommendation when age > 80% of expected lifespan
- Factor in condition assessment
- Budget planning based on current market prices

### Maintenance Schedule
- Auto-create maintenance tasks based on appliance type
- HVAC: Quarterly filter changes, annual servicing
- Water Heater: Annual flushing
- Refrigerator: Semi-annual coil cleaning
- Dishwasher: Monthly filter cleaning

### Cost Tracking
- Total cost of ownership = Purchase + Service costs
- Calculate annual maintenance cost average
- Track repair vs replace decision point

## Validation Rules
- Name: Required, max 100 characters
- Category: Required, valid enum
- Brand: Required, max 50 characters
- Model: Required, max 100 characters
- SerialNumber: Optional, max 50 characters
- PurchaseDate: Required, cannot be future
- PurchaseCost: Required, must be >= 0
- WarrantyExpiration: Optional, must be > PurchaseDate
- ExpectedLifespanYears: Required, must be > 0

## Testing Requirements
- Unit tests for warranty expiration logic
- Integration tests for service history tracking
- E2E tests for appliance CRUD operations

---

**Version**: 1.0
**Last Updated**: 2025-12-29
