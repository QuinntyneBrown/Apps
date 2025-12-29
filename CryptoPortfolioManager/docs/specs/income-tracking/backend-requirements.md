# Backend Requirements - Income Tracking
## API: GET /api/income/{type}, GET /api/income/summary
## Models: CryptoIncome (Id, Type, Coin, Amount, FiatValueAtReceipt, Date, Source)
## Logic: Staking/mining/airdrop detection, FMV calculation at receipt, APY calculation
## Events: StakingRewardReceived, MiningIncomeReceived, AirdropReceived
