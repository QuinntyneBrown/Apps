# Value Assessment - Backend Requirements

## Domain Events
- **VehicleValueAssessed**: Current market value estimated
- **InvestmentCalculated**: Total financial investment computed
- **InsuranceUpdated**: Vehicle insurance adjusted

## API Endpoints
- `POST /api/projects/{id}/valuations` - Create valuation
- `GET /api/projects/{id}/valuations` - List valuations
- `GET /api/projects/{id}/investment` - Calculate total investment
- `POST /api/projects/{id}/insurance` - Update insurance info

## Data Models

### Valuation
```csharp
{
    "id": "guid",
    "projectId": "guid",
    "assessmentDate": "datetime",
    "estimatedValue": "decimal",
    "comparableSales": "array<object>",
    "valueFactors": "array<string>",
    "appreciationPotential": "enum[High, Moderate, Low]"
}
```

### Investment
```csharp
{
    "totalInvested": "decimal",
    "currentValue": "decimal",
    "roi": "decimal",
    "profitLoss": "decimal",
    "timeInvested": "decimal"
}
```
