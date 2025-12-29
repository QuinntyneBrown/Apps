# Privacy Management - Backend Requirements

## API Endpoints

#### POST /api/privacy/preferences
Set privacy preferences
- **Request Body**: `{ privacyLevel, dataDeletionWishes, exposureLimits }`
- **Events**: `PrivacyPreferenceSet`

#### POST /api/privacy/flag-content
Flag sensitive content
- **Request Body**: `{ contentLocation, sensitivityLevel, handlingInstructions }`
- **Events**: `SensitiveContentFlagged`

#### POST /api/privacy/schedule-deletion
Schedule data deletion
- **Request Body**: `{ dataLocations, deletionMethod, verificationRequired }`
- **Events**: `DataDeletionScheduled`

## Data Models

```csharp
public class PrivacyPreference
{
    public Guid Id { get; set; }
    public PrivacyLevel Level { get; set; }
    public bool DeleteSensitiveData { get; set; }
    public List<string> DataToDelete { get; set; }
    public string ExposureLimits { get; set; }
}
```
