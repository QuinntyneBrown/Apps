# Backend Requirements - Transaction Tracking
## API: POST/GET/PUT/DELETE /api/transactions, POST /api/transactions/import
## Models: Transaction (Id, WalletId, Hash, Type, Amount, Coin, FiatValue, Timestamp, Category)
## Logic: Auto-detection from blockchain, CSV import, categorization (buy/sell/swap/transfer/income)
## Events: TransactionDetected, TransactionCategorized, TransactionImported
