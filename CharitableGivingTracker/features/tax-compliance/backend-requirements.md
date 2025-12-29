# Tax Compliance - Backend Requirements

## Overview
Manages tax deduction calculations, acknowledgment letter tracking, Form 8283 preparation, and annual tax reporting.

## Domain Model

### TaxDeduction Aggregate
- **TaxDeductionId**: Unique identifier (Guid)
- **UserId**: User ID (Guid)
- **TaxYear**: Tax year (int)
- **TotalDeductibleAmount**: Total tax-deductible donations (decimal)
- **DonationCount**: Number of deductible donations (int)
- **OrganizationsCount**: Number of organizations donated to (int)
- **CalculatedAt**: When calculation performed (DateTime)

## Commands

### CalculateTaxDeductionCommand
- Validates TaxYear
- Sums all tax-deductible donations for year
- Creates TaxDeduction aggregate
- Raises **TaxDeductionCalculated** event

### GenerateForm8283Command
- Validates non-cash donations > $500
- Creates Form 8283 data
- Returns form data

## Domain Events

### TaxDeductionCalculated
```csharp
public class TaxDeductionCalculated : DomainEvent
{
    public int TaxYear { get; set; }
    public decimal TotalDeductibleAmount { get; set; }
    public int DonationCount { get; set; }
    public int OrganizationsCount { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

### AcknowledgmentLetterReceived
```csharp
public class AcknowledgmentLetterReceived : DomainEvent
{
    public Guid DonationId { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTime AcknowledgmentDate { get; set; }
    public string DocumentUrl { get; set; }
    public Guid UserId { get; set; }
    public DateTime OccurredAt { get; set; }
}
```

## API Endpoints

### GET /api/tax/deductions/{taxYear}
- Returns tax deduction summary for year

### POST /api/tax/calculate/{taxYear}
- Calculates tax deductions for year

### GET /api/tax/form8283/{taxYear}
- Generates Form 8283 data

### GET /api/tax/report/{taxYear}
- Generates annual tax report PDF
