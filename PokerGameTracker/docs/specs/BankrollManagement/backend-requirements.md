# Bankroll Management - Backend

## API: POST /api/bankroll/deposit
Add funds to bankroll
Domain Events: BankrollDeposit

## API: POST /api/bankroll/withdraw
Remove funds from bankroll
Domain Events: BankrollWithdrawal

## API: GET /api/bankroll/balance
Get current bankroll total

## Domain Model
```csharp
public class Bankroll : AggregateRoot
{
    public Guid UserId { get; private set; }
    public decimal CurrentBalance { get; private set; }
    public decimal AllTimeHigh { get; private set; }
    public List<Transaction> Transactions { get; private set; }

    public void Deposit(decimal amount)
    {
        CurrentBalance += amount;
        if (CurrentBalance > AllTimeHigh)
            AllTimeHigh = CurrentBalance;
        RaiseDomainEvent(new BankrollDeposit(...));
    }
}
```

## Database Schema
bankrolls: id, user_id, current_balance, all_time_high, last_updated
bankroll_transactions: id, bankroll_id, transaction_type, amount, transaction_date, description
