# Backend Requirements - Wallet Management

## API Endpoints
- POST /api/wallets - Add wallet
- GET /api/wallets - List user wallets
- POST /api/wallets/{id}/sync - Trigger sync
- DELETE /api/wallets/{id} - Remove wallet
- GET /api/wallets/{id}/balance - Get current balance

## Domain Models
- Wallet: Id, UserId, Address, Type, Blockchain, Label, CreatedAt
- WalletBalance: WalletId, CoinSymbol, Balance, FiatValue, UpdatedAt

## Business Logic
- Blockchain API integration for balance fetching
- Auto-sync every 15 minutes
- Support hot/cold/exchange wallet types
- OAuth for exchange connections

## Events
WalletAdded, WalletSynced, WalletRemoved, WalletBalanceUpdated

## Database Schema
Wallets table, WalletBalances table with indexing on UserId and Blockchain
