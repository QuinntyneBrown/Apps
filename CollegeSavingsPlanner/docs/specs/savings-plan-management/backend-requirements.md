# Savings Plan Management - Backend Requirements

## Overview
Manages 529 plans, ESAs, and other education savings accounts with beneficiary tracking and plan lifecycle management.

## Domain Model

### SavingsPlan Aggregate
- **PlanId**: Guid
- **UserId**: Guid (account owner)
- **PlanType**: PlanType (529, ESA, UTMA, UGMA, Other)
- **StatePlan**: string (e.g., "California 529")
- **CurrentBalance**: decimal
- **BeneficiaryId**: Guid
- **AccountNumber**: string
- **FinancialInstitution**: string
- **OpenDate**: DateTime
- **Status**: PlanStatus (Active, Closed, Transferred)
- **CreatedAt**: DateTime
- **UpdatedAt**: DateTime

### Beneficiary Entity
- **BeneficiaryId**: Guid
- **Name**: string
- **BirthDate**: DateTime
- **Relationship**: string
- **ExpectedEnrollmentYear**: int
- **CurrentAge**: int (calculated)
- **YearsUntilCollege**: int (calculated)

### PlanType Enum
- 529, ESA, UTMA, UGMA, Other

### PlanStatus Enum
- Active, Closed, Transferred

## Commands

### CreateSavingsPlanCommand
- Validates user exists
- Validates plan type
- Creates SavingsPlan aggregate
- Raises **SavingsPlanCreated** event
- Returns PlanId

### AddBeneficiaryCommand
- Validates plan exists
- Creates Beneficiary entity
- Calculates enrollment timeline
- Raises **BeneficiaryAdded** event
- Returns BeneficiaryId

### ChangeBeneficiaryCommand
- Validates beneficiary eligibility (family member)
- Updates plan beneficiary
- Raises **BeneficiaryChanged** event
- Notifies tax impact

### ClosePlanCommand
- Validates plan exists
- Records closure reason
- Finalizes balance
- Raises **PlanClosed** event

## Queries

### GetPlanByIdQuery
- Returns plan details with beneficiary
- Includes current balance and performance

### GetPlansByUserIdQuery
- Returns all plans for user
- Groups by beneficiary
- Includes projections

### GetBeneficiariesQuery
- Returns all beneficiaries for user
- Includes age and years until college

## Domain Events

```csharp
public class SavingsPlanCreated : DomainEvent
{
    public Guid PlanId { get; set; }
    public string PlanType { get; set; }
    public Guid BeneficiaryId { get; set; }
    public string StatePlan { get; set; }
    public decimal CurrentBalance { get; set; }
    public Guid UserId { get; set; }
}

public class BeneficiaryAdded : DomainEvent
{
    public Guid BeneficiaryId { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Guid PlanId { get; set; }
    public int ExpectedEnrollmentYear { get; set; }
    public Guid UserId { get; set; }
}

public class BeneficiaryChanged : DomainEvent
{
    public Guid PlanId { get; set; }
    public Guid PreviousBeneficiaryId { get; set; }
    public Guid NewBeneficiaryId { get; set; }
    public DateTime ChangeDate { get; set; }
    public string Reason { get; set; }
}

public class PlanClosed : DomainEvent
{
    public Guid PlanId { get; set; }
    public string ClosureReason { get; set; }
    public decimal FinalBalance { get; set; }
    public string BeneficiaryOutcome { get; set; }
}
```

## API Endpoints

### POST /api/savings-plans
- Creates new savings plan
- Request: CreateSavingsPlanCommand
- Returns: 201 Created with PlanId

### POST /api/savings-plans/{planId}/beneficiary
- Adds beneficiary to plan
- Request: AddBeneficiaryCommand
- Returns: 201 Created

### PUT /api/savings-plans/{planId}/beneficiary
- Changes plan beneficiary
- Request: ChangeBeneficiaryCommand
- Returns: 200 OK

### POST /api/savings-plans/{planId}/close
- Closes savings plan
- Request: { closureReason, finalBalance }
- Returns: 200 OK

### GET /api/savings-plans/{planId}
- Retrieves plan details
- Returns: 200 OK with PlanDto

### GET /api/savings-plans
- Retrieves all plans for user
- Returns: 200 OK with list of PlanDto

## Business Rules
1. 529 beneficiary must be family member (IRS rules)
2. Can only have one beneficiary per plan at a time
3. Beneficiary changes must comply with state 529 rules
4. Plan balance must be >= 0
5. Cannot reopen closed plans
6. ESA has age restrictions (must use before age 30)
