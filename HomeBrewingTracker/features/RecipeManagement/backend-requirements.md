# Recipe Management - Backend

## API: POST /api/recipes
Create beer recipe with grain bill, hops, yeast
Domain Events: RecipeCreated

## Domain Model
```csharp
public class Recipe : AggregateRoot
{
    public string BeerName { get; private set; }
    public string Style { get; private set; }
    public decimal TargetABV { get; private set; }
    public decimal TargetIBU { get; private set; }
    public List<Grain> GrainBill { get; private set; }
    public List<HopAddition> HopSchedule { get; private set; }
    public Yeast YeastStrain { get; private set; }
    public decimal BatchSize { get; private set; }
}
```

## Database Schema
recipes: id, user_id, beer_name, style, target_abv, target_ibu, batch_size, grain_bill (json), hop_schedule (json), yeast_strain
