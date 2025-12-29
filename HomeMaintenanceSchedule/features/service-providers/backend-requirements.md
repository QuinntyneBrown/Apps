# Service Providers - Backend Requirements

## Overview
The Service Providers backend manages a directory of trusted home service professionals with contact information, service history, ratings, and reviews to help homeowners maintain relationships with quality providers.

## Domain Model

### ServiceProvider Entity
```csharp
public class ServiceProvider : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string Specialty { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public Address Address { get; set; }
    public string LicenseNumber { get; set; }
    public string InsuranceInfo { get; set; }
    public decimal AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public bool IsPreferred { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
    public List<string> ServiceCategories { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    // Navigation properties
    public ICollection<Service> Services { get; set; }
    public ICollection<Task> AssignedTasks { get; set; }
}
```

### Service Entity
```csharp
public class Service : BaseEntity
{
    public Guid Id { get; set; }
    public Guid ServiceProviderId { get; set; }
    public Guid? TaskId { get; set; }
    public DateTime ServiceDate { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public int? DurationMinutes { get; set; }
    public int? Rating { get; set; }
    public string ReviewText { get; set; }
    public string InvoiceUrl { get; set; }
    public string Notes { get; set; }
    public List<string> PhotoUrls { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    // Navigation properties
    public ServiceProvider ServiceProvider { get; set; }
    public Task Task { get; set; }
}
```

### Value Objects
```csharp
public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
}

public class ContactInfo
{
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
}
```

## API Endpoints

### Service Provider Commands

#### Create Service Provider
- **Endpoint**: `POST /api/service-providers`
- **Request Body**:
```json
{
  "name": "John Smith",
  "companyName": "John's HVAC Service",
  "specialty": "HVAC Installation and Repair",
  "phone": "(555) 123-4567",
  "email": "john@johnsHVAC.com",
  "website": "https://johnsHVAC.com",
  "address": {
    "street": "123 Main St",
    "city": "Springfield",
    "state": "IL",
    "zipCode": "62701",
    "country": "USA"
  },
  "licenseNumber": "HVAC-12345",
  "insuranceInfo": "ABC Insurance, Policy #987654",
  "serviceCategories": ["HVAC", "Heating", "Cooling"],
  "notes": "Available weekends, 24/7 emergency service"
}
```
- **Response**: `201 Created` with ServiceProvider object
- **Domain Event**: `ProviderAdded`

#### Update Service Provider
- **Endpoint**: `PUT /api/service-providers/{id}`
- **Request Body**: Same as Create
- **Response**: `200 OK` with updated ServiceProvider object

#### Mark as Preferred
- **Endpoint**: `POST /api/service-providers/{id}/prefer`
- **Response**: `200 OK`

#### Deactivate Provider
- **Endpoint**: `POST /api/service-providers/{id}/deactivate`
- **Response**: `200 OK`

#### Delete Service Provider
- **Endpoint**: `DELETE /api/service-providers/{id}`
- **Response**: `204 No Content`

### Service History Commands

#### Record Service
- **Endpoint**: `POST /api/service-providers/{providerId}/services`
- **Request Body**:
```json
{
  "taskId": "guid",
  "serviceDate": "2025-01-15T14:30:00Z",
  "description": "Replaced HVAC filters and performed annual inspection",
  "cost": 185.00,
  "durationMinutes": 90,
  "rating": 5,
  "reviewText": "Excellent service, very professional",
  "invoiceUrl": "https://storage/invoice-123.pdf",
  "notes": "Recommended replacing outdoor unit in 2 years",
  "photoUrls": ["https://storage/service1.jpg"]
}
```
- **Response**: `201 Created` with Service object
- **Domain Events**: `ProviderServiceCompleted`, `ProviderRated`

#### Update Service Record
- **Endpoint**: `PUT /api/service-providers/{providerId}/services/{serviceId}`
- **Response**: `200 OK`

#### Add Rating/Review
- **Endpoint**: `POST /api/service-providers/{providerId}/rate`
- **Request Body**:
```json
{
  "serviceId": "guid",
  "rating": 5,
  "reviewText": "Excellent work, highly recommended!"
}
```
- **Response**: `200 OK`
- **Domain Event**: `ProviderRated`

### Queries

#### Get All Service Providers
- **Endpoint**: `GET /api/service-providers`
- **Query Parameters**:
  - `specialty` (optional): Filter by specialty
  - `category` (optional): Filter by service category
  - `isPreferred` (optional): Filter preferred providers
  - `minRating` (optional): Minimum average rating
  - `isActive` (optional): Filter active/inactive
  - `search` (optional): Search name/company
  - `pageNumber`, `pageSize`
- **Response**: `200 OK` with paginated list

#### Get Provider by ID
- **Endpoint**: `GET /api/service-providers/{id}`
- **Response**: `200 OK` with ServiceProvider object

#### Get Provider Service History
- **Endpoint**: `GET /api/service-providers/{id}/services`
- **Query Parameters**:
  - `startDate`, `endDate` (optional)
  - `pageNumber`, `pageSize`
- **Response**: `200 OK` with paginated Service list

#### Get Provider Statistics
- **Endpoint**: `GET /api/service-providers/{id}/statistics`
- **Response**:
```json
{
  "totalServices": 25,
  "averageRating": 4.8,
  "reviewCount": 20,
  "totalCost": 3750.00,
  "averageCost": 150.00,
  "mostRecentService": "2025-01-15T14:30:00Z",
  "categoryBreakdown": {
    "HVAC": 15,
    "Plumbing": 10
  }
}
```

#### Search Providers
- **Endpoint**: `GET /api/service-providers/search`
- **Query Parameters**:
  - `query`: Search term
  - `category`: Filter by category
  - `location`: Search by proximity (future feature)
- **Response**: `200 OK` with matching providers

#### Get Preferred Providers
- **Endpoint**: `GET /api/service-providers/preferred`
- **Response**: `200 OK` with list of preferred providers

## Business Logic

### Rating Calculation
- Average rating calculated from all service ratings
- Formula: `SUM(ratings) / COUNT(ratings)`
- Recalculated on each new rating
- Displayed to 1 decimal place
- Minimum 3 ratings to display average (otherwise show "New")

### Service Cost Tracking
- Track total spent per provider
- Calculate average cost per service
- Identify cost trends over time
- Compare costs between providers in same category

### Preferred Provider Logic
- Users can mark multiple providers as preferred
- Preferred providers show first in lists
- Quick access from task assignment
- Badge indicator in UI

### Provider Performance Metrics
- Response time (future: time to first service after contact)
- Completion rate (tasks completed vs assigned)
- On-time rate (services completed on scheduled date)
- Cost accuracy (estimated vs actual)

## Domain Events

### ProviderAdded Event
```csharp
public class ProviderAddedEvent : IDomainEvent
{
    public Guid ProviderId { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public ContactInfo ContactInfo { get; set; }
    public DateTime OccurredAt { get; set; }
}
```
**Handlers**:
- Send welcome email to provider (if email provided)
- Add to provider directory index
- Log in audit trail

### ProviderServiceCompleted Event
```csharp
public class ProviderServiceCompletedEvent : IDomainEvent
{
    public Guid ProviderId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime ServiceDate { get; set; }
    public Guid? TaskId { get; set; }
    public decimal Cost { get; set; }
    public int? DurationMinutes { get; set; }
    public DateTime OccurredAt { get; set; }
}
```
**Handlers**:
- Update provider statistics
- Link to task (if applicable)
- Prompt for rating (if not provided)
- Update service history

### ProviderRated Event
```csharp
public class ProviderRatedEvent : IDomainEvent
{
    public Guid ProviderId { get; set; }
    public Guid ServiceId { get; set; }
    public int Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime ServiceDate { get; set; }
    public DateTime OccurredAt { get; set; }
}
```
**Handlers**:
- Recalculate average rating
- Increment review count
- Send thank you to user
- Update provider profile

## Validation Rules

### Service Provider
- `Name`: Required, max 100 characters
- `CompanyName`: Optional, max 150 characters
- `Specialty`: Required, max 200 characters
- `Phone`: Required, valid phone format
- `Email`: Optional, valid email format
- `Website`: Optional, valid URL format
- `LicenseNumber`: Optional, max 50 characters
- `ServiceCategories`: At least one category required

### Service Record
- `ServiceProviderId`: Required, must exist
- `ServiceDate`: Required, cannot be in future
- `Description`: Required, max 1000 characters
- `Cost`: Required, must be >= 0
- `DurationMinutes`: Optional, must be > 0
- `Rating`: Optional, must be 1-5
- `ReviewText`: Optional, max 1000 characters

## Security

### Authorization
- Users can only access providers they created
- Service providers can view their own profile (future feature)
- Admin users can access all providers

### Data Protection
- Phone/email encrypted at rest
- Sensitive info (SSN, etc.) never stored
- License/insurance info access logged

## Performance Considerations

- Index on `CreatedBy`, `Specialty`, `IsPreferred`, `AverageRating`
- Composite index on `(IsActive, AverageRating DESC)`
- Cache frequently accessed preferred providers
- Denormalize average rating (calculated field)

## Error Handling

- `404 Not Found`: Provider doesn't exist
- `400 Bad Request`: Validation errors
- `403 Forbidden`: User doesn't have access
- `409 Conflict`: Cannot delete provider with active services

## Testing Requirements

### Unit Tests
- Rating calculation logic
- Service cost aggregation
- Provider search filtering
- Domain event publishing

### Integration Tests
- CRUD operations
- Service history tracking
- Rating/review workflow
- Provider statistics calculation

---

**Version**: 1.0
**Last Updated**: 2025-12-29
