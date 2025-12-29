# Backend Requirements - Trade Management
## API: POST /api/trades/{type} (purchase/sell/swap), GET /api/trades/gains
## Models: Trade (Id, Type, FromCoin, ToCoin, Amount, Price, CostBasis, RealizedGain)
## Logic: Cost basis calculation (FIFO/LIFO/HIFO), P&L tracking, tax lot management
## Events: CryptoPurchased, CryptoSold, CryptoSwapped
