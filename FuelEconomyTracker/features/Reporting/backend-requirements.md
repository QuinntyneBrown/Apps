# Reporting - Backend Requirements

## API Endpoints

### GET /api/reports/weekly
**Query**: vehicleId, week
**Response**: Weekly fuel economy summary

### GET /api/reports/annual
**Query**: vehicleId, year
**Response**: Annual fuel consumption and cost summary

### POST /api/reports/export
**Request**: Report type, format (PDF/CSV), parameters
**Response**: Download link

## Domain Models

```csharp
public class WeeklyReport : Entity
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public DateTime WeekEnding { get; private set; }
    public decimal AverageMPG { get; private set; }
    public decimal TotalFuelCost { get; private set; }
    public int MilesDriven { get; private set; }
    public FuelPurchase BestFillUp { get; private set; }
    public FuelPurchase WorstFillUp { get; private set; }
    public List<TrendIndicator> Trends { get; private set; }
    public string WeekOverWeekComparison { get; private set; }
}

public class AnnualReport : Entity
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public int Year { get; private set; }
    public decimal TotalSpent { get; private set; }
    public decimal TotalGallons { get; private set; }
    public int TotalMiles { get; private set; }
    public decimal AverageMPG { get; private set; }
    public List<Achievement> Achievements { get; private set; }
    public decimal CostPerMile { get; private set; }
    public string YearOverYearComparison { get; private set; }
}
```

## Business Logic
- Generate reports on demand or scheduled
- Aggregate data from multiple sources
- Calculate trends and comparisons
- Format for export (PDF, CSV, Excel)
- Include visualizations in PDF reports

## Domain Events

### WeeklyEconomyReportGenerated
```csharp
public class WeeklyEconomyReportGenerated : DomainEvent
{
    public Guid ReportId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTime WeekEnding { get; set; }
    public decimal AverageMPG { get; set; }
    public decimal TotalCost { get; set; }
    public string Trends { get; set; }
}
```

### AnnualFuelReportCompiled
```csharp
public class AnnualFuelReportCompiled : DomainEvent
{
    public Guid ReportId { get; set; }
    public Guid VehicleId { get; set; }
    public int Year { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal AverageMPG { get; set; }
    public List<Achievement> Achievements { get; set; }
}
```

## Database Schema

### reports
- id, vehicle_id, report_type, period_start, period_end
- generated_at, data (json), export_urls (json)

### report_schedules
- id, user_id, vehicle_id, report_type, frequency
- delivery_method, last_generated, next_due
