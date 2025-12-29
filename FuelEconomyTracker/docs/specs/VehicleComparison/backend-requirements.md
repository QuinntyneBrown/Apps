# Vehicle Comparison - Backend Requirements

## API Endpoints

### GET /api/users/{userId}/vehicles/compare
**Query**: vehicleIds[]
**Response**: Side-by-side comparison of MPG, costs, efficiency

### GET /api/vehicles/{vehicleId}/epa-comparison
**Response**: Actual vs EPA ratings analysis
**Domain Events**: `EPARatingComparison`

## Domain Models

```csharp
public class VehicleComparison : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public List<Guid> VehicleIds { get; private set; }
    public DateTime ComparisonDate { get; private set; }
    public Dictionary<Guid, ComparisonMetrics> Metrics { get; private set; }
    public Guid EfficiencyWinner { get; private set; }
}

public class ComparisonMetrics
{
    public decimal AverageMPG { get; set; }
    public decimal MonthlyCost { get; set; }
    public decimal CostPerMile { get; set; }
    public UsagePattern Usage { get; set; }
}
```

## Business Logic
- Compare actual MPG across owned vehicles
- Calculate which vehicle is most economical for specific trips
- Compare real-world to EPA ratings
- Recommend vehicle selection based on trip type

## Database Schema

### vehicle_comparisons
- id, user_id, comparison_date, vehicle_ids (json)
- metrics (json), efficiency_winner

### epa_comparisons
- id, vehicle_id, epa_city, epa_highway, epa_combined
- actual_achieved, variance, expectations_met
