# Backend Requirements - Conference Management

## Domain Events
- ConferenceRegistered
- ConferenceAttended
- ConferenceCancelled
- ConferenceSchedulePlanned

## API Endpoints

### Commands
- POST /api/conferences - Register for conference
- PUT /api/conferences/{id} - Update conference details
- POST /api/conferences/{id}/attend - Mark as attended
- POST /api/conferences/{id}/cancel - Cancel registration
- POST /api/conferences/{id}/schedule - Plan session schedule

### Queries
- GET /api/conferences - List all conferences (upcoming, past, cancelled)
- GET /api/conferences/{id} - Get conference details
- GET /api/conferences/upcoming - Get upcoming conferences
- GET /api/conferences/{id}/schedule - Get planned schedule

## Domain Model
```csharp
public class Conference
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public RegistrationType RegistrationType { get; private set; }
    public decimal Cost { get; private set; }
    public ConferenceStatus Status { get; private set; }
    public string ConfirmationNumber { get; private set; }
}
```

## Business Rules
- Cannot register for past conferences
- Must have unique confirmation number
- Cancellation within 30 days may incur fees
- Track refund status for cancelled registrations
