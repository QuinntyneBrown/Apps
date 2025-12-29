# Backend Requirements - Account Integration

## API Endpoints
- POST /api/accounts/link - Link account (AccountLinked event)
- POST /api/accounts/{id}/sync - Sync account (AccountSyncCompleted/Failed events)
- DELETE /api/accounts/{id} - Unlink account
- GET /api/accounts/sync-status - Get sync status for all accounts

## Models
```csharp
public class LinkedAccount {
    public Guid Id;
    public string InstitutionName;
    public string AccountType;
    public DateTime LinkDate;
    public bool AutoSyncEnabled;
    public DateTime LastSyncDate;
    public SyncStatus LastSyncStatus;
}
public enum SyncStatus { Success, Failed, Pending }
```
