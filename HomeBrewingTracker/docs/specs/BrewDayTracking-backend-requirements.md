# Brew Day Tracking - Backend

## API: POST /api/brew-sessions/start
Start brewing session for recipe
Domain Events: BrewSessionStarted

## API: POST /api/brew-sessions/{id}/gravity
Record gravity reading (OG/FG)
Domain Events: GravityReadingTaken

## Domain Model
```csharp
public class BrewSession : AggregateRoot
{
    public Guid RecipeId { get; private set; }
    public DateTime StartTime { get; private set; }
    public decimal ActualOG { get; private set; }
    public decimal TargetOG { get; private set; }
    public decimal Efficiency { get; private set; }
    public List<MashTemperature> MashTemps { get; private set; }
}
```

## Database Schema
brew_sessions: id, recipe_id, start_time, completion_time, actual_og, target_og, efficiency, mash_temps (json), notes
