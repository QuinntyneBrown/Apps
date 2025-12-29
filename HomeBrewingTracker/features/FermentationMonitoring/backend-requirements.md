# Fermentation Monitoring - Backend

## API: POST /api/fermentations/start
Begin fermentation for brew session
Domain Events: FermentationStarted

## API: POST /api/fermentations/{id}/temperature
Log fermentation temperature
Domain Events: FermentationTemperatureLogged

## Domain Model
```csharp
public class Fermentation : AggregateRoot
{
    public Guid BrewSessionId { get; private set; }
    public Yeast YeastStrain { get; private set; }
    public DateTime PitchDate { get; private set; }
    public decimal PitchTemperature { get; private set; }
    public List<TemperatureReading> TempReadings { get; private set; }
    public DateTime? CompletionDate { get; private set; }
    public decimal FinalGravity { get; private set; }
}
```

## Database Schema
fermentations: id, brew_session_id, yeast_strain, pitch_date, pitch_temperature, completion_date, final_gravity, temp_readings (json)
