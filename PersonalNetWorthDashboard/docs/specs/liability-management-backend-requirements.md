# Backend Requirements - Liability Management

## API Endpoints
- POST /api/liabilities - Add liability (LiabilityAdded event)
- PUT /api/liabilities/{id}/balance - Update balance (LiabilityBalanceUpdated event)
- GET /api/liabilities - Get all liabilities
- GET /api/liabilities/{id}/payoff-projection - Get payoff timeline

## Models
```csharp
public class Liability {
    public Guid Id;
    public LiabilityType Type;
    public decimal PrincipalAmount;
    public decimal CurrentBalance;
    public decimal InterestRate;
    public string Creditor;
    public DateTime DueDate;
}
public enum LiabilityType { Mortgage, CarLoan, StudentLoan, CreditCard, PersonalLoan }
```
