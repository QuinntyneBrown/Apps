# Backend Requirements - Progress and Contributions

## API Endpoints
- POST /api/goals/{id}/contributions - Record contribution (ManualContributionRecorded)
- POST /api/goals/{id}/contributions/auto - Setup automatic (AutomaticContributionMade)
- POST /api/goals/{id}/withdrawals - Record withdrawal (WithdrawalMade)
- GET /api/goals/{id}/progress - Get progress details (ProgressUpdated)
- POST /api/goals/{id}/link-account - Link bank account (GoalLinkedToAccount)
- POST /api/accounts/{id}/sync - Sync balance (AccountBalanceSynced)

## Domain Models
```csharp
public class Contribution {
    public Guid Id { get; set; }
    public Guid GoalId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ContributionDate { get; set; }
    public ContributionType Type { get; set; }
    public string Source { get; set; }
}
public enum ContributionType { Manual, Automatic }
```

## Business Rules
1. Contributions must be positive
2. Withdrawals decrease current amount
3. Progress = (CurrentAmount / TargetAmount) * 100
