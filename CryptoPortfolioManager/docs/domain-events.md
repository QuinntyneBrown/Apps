# Domain Events - Crypto Portfolio Manager

## Overview
This document defines the domain events tracked in the Crypto Portfolio Manager application. These events capture significant business occurrences related to cryptocurrency portfolio tracking, multi-wallet management, transaction recording, and tax reporting for digital assets.

## Events

### WalletEvents

#### WalletAdded
- **Description**: A cryptocurrency wallet has been added to portfolio
- **Triggered When**: User registers a wallet address or exchange account for tracking
- **Key Data**: Wallet ID, wallet address, wallet type (hot/cold/exchange), blockchain, label, user ID, timestamp
- **Consumers**: Wallet aggregator, balance fetcher, transaction importer, portfolio dashboard

#### WalletSynced
- **Description**: Wallet balance and transactions have been synchronized
- **Triggered When**: Automatic or manual sync retrieves latest blockchain data
- **Key Data**: Wallet ID, sync timestamp, new transactions count, updated balances, sync status, user ID
- **Consumers**: Portfolio updater, transaction processor, balance calculator, price aggregator

#### WalletRemoved
- **Description**: Wallet has been removed from portfolio tracking
- **Triggered When**: User deletes wallet from tracking
- **Key Data**: Wallet ID, removal date, final balance, total transactions, removal reason, user ID
- **Consumers**: Portfolio recalculator, historical archiver, sync service cleanup

#### WalletBalanceUpdated
- **Description**: Cryptocurrency balance in wallet has changed
- **Triggered When**: Transaction occurs or price update affects balance
- **Key Data**: Wallet ID, coin symbol, previous balance, new balance, change source, timestamp
- **Consumers**: Portfolio value calculator, balance dashboard, alert checker

### TransactionEvents

#### TransactionDetected
- **Description**: New blockchain transaction involving user's wallet identified
- **Triggered When**: Blockchain sync discovers new transaction
- **Key Data**: Transaction ID, wallet ID, transaction hash, type (receive/send/swap), amount, coin, timestamp, block number
- **Consumers**: Transaction importer, tax processor, balance updater, notification service

#### TransactionCategorized
- **Description**: Transaction has been assigned a type and tax treatment
- **Triggered When**: User or system categorizes transaction (trade, transfer, income, etc.)
- **Key Data**: Transaction ID, category, tax treatment, cost basis method, categorization confidence, user ID
- **Consumers**: Tax calculator, cost basis tracker, P&L analyzer

#### TransactionImported
- **Description**: Historical transactions have been bulk imported
- **Triggered When**: User imports transactions from CSV or exchange API
- **Key Data**: Import ID, source, transaction count, date range, import timestamp, user ID
- **Consumers**: Portfolio reconstructor, cost basis calculator, tax analyzer

### TradeEvents

#### CryptoPurchased
- **Description**: Cryptocurrency has been acquired
- **Triggered When**: User buys crypto with fiat or receives from transfer
- **Key Data**: Trade ID, coin symbol, quantity, purchase price, fiat value, purchase date, exchange, user ID
- **Consumers**: Cost basis tracker, portfolio aggregator, tax lot manager, holdings updater

#### CryptoSold
- **Description**: Cryptocurrency has been sold or exchanged
- **Triggered When**: User sells crypto for fiat or trades for another coin
- **Key Data**: Trade ID, coin symbol, quantity sold, sale price, fiat value, realized gain/loss, sale date, user ID
- **Consumers**: Tax calculator, capital gains tracker, portfolio updater, P&L analyzer

#### CryptoSwapped
- **Description**: One cryptocurrency exchanged for another
- **Triggered When**: User performs crypto-to-crypto trade
- **Key Data**: Swap ID, from coin, to coin, from amount, to amount, exchange rate, swap date, platform, user ID
- **Consumers**: Tax event recorder, cost basis adjuster, portfolio rebalancer

### PortfolioEvents

#### PortfolioValueCalculated
- **Description**: Total portfolio value in fiat currency computed
- **Triggered When**: Price update triggers or user requests valuation
- **Key Data**: Total value, value by coin, value change 24h, calculation timestamp, user ID
- **Consumers**: Portfolio dashboard, performance tracker, alert checker, historical recorder

#### AllocationAnalyzed
- **Description**: Portfolio allocation percentages calculated across holdings
- **Triggered When**: Holdings change or user requests allocation view
- **Key Data**: Allocation by coin, allocation by asset type, concentration risks, diversification score, timestamp
- **Consumers**: Allocation visualizer, rebalancing advisor, risk analyzer

#### PortfolioRebalanced
- **Description**: Portfolio has been rebalanced to target allocations
- **Triggered When**: User executes trades to restore target percentages
- **Key Data**: Rebalance ID, trades executed, previous allocation, new allocation, rebalance date, user ID
- **Consumers**: Performance tracker, tax impact calculator, transaction logger

### PriceEvents

#### PriceAlertTriggered
- **Description**: Cryptocurrency price has crossed user-defined threshold
- **Triggered When**: Market price reaches alert target
- **Key Data**: Alert ID, coin symbol, alert type (above/below), target price, current price, trigger timestamp
- **Consumers**: Notification service, trading suggestion engine, alert manager

#### SignificantPriceMovement
- **Description**: Cryptocurrency price changed dramatically
- **Triggered When**: Price moves beyond threshold (e.g., ±10% in 24h)
- **Key Data**: Coin symbol, previous price, current price, change percentage, time period, timestamp
- **Consumers**: Volatility alert, news correlator, portfolio impact calculator

### TaxEvents

#### TaxableEventRecorded
- **Description**: Transaction creating tax obligation has been logged
- **Triggered When**: Sale, trade, or income event occurs
- **Key Data**: Event ID, event type, coin, amount, fiat value, gain/loss, tax year, user ID, timestamp
- **Consumers**: Tax calculator, Form 8949 generator, capital gains aggregator

#### CapitalGainCalculated
- **Description**: Capital gain or loss computed for a disposal
- **Triggered When**: Crypto sold or traded
- **Key Data**: Disposal ID, coin, quantity, cost basis, proceeds, gain/loss, holding period, tax year, user ID
- **Consumers**: Tax report generator, short/long-term classifier, tax liability estimator

#### TaxReportGenerated
- **Description**: Annual cryptocurrency tax report created
- **Triggered When**: User requests tax report for filing
- **Key Data**: Report ID, tax year, total gains, total losses, income, report format, generation timestamp, user ID
- **Consumers**: Report delivery, Form 8949 filler, tax software export

#### CostBasisMethodChanged
- **Description**: Cost basis calculation method updated
- **Triggered When**: User switches between FIFO, LIFO, HIFO methods
- **Key Data**: Previous method, new method, change date, tax impact, recalculation required, user ID
- **Consumers**: Tax recalculator, historical transaction processor, gain/loss updater

### IncomeEvents

#### StakingRewardReceived
- **Description**: Cryptocurrency staking reward earned
- **Triggered When**: Staking reward deposited to wallet
- **Key Data**: Reward ID, coin symbol, amount, fiat value at receipt, receipt date, staking platform, user ID
- **Consumers**: Income tracker, tax calculator, portfolio updater, yield analyzer

#### MiningIncomeReceived
- **Description**: Mining reward or payment received
- **Triggered When**: Mining payout deposited to wallet
- **Key Data**: Income ID, coin symbol, amount, fiat value, receipt date, mining pool, user ID
- **Consumers**: Income aggregator, tax reporter, profitability calculator

#### AirdropReceived
- **Description**: Airdrop or fork coins received
- **Triggered When**: Free distribution of tokens detected
- **Key Data**: Airdrop ID, coin symbol, quantity, fiat value, receipt date, source project, user ID
- **Consumers**: Income recorder, tax calculator, portfolio updater

### PerformanceEvents

#### PerformanceCalculated
- **Description**: Portfolio performance metrics computed
- **Triggered When**: Periodic calculation or user requests performance view
- **Key Data**: Time period, total return, percentage gain/loss, ROI, comparison to BTC/ETH, timestamp, user ID
- **Consumers**: Performance dashboard, investment analyzer, benchmark comparator

#### AllTimeHighReached
- **Description**: Portfolio value has reached new all-time high
- **Triggered When**: Current value exceeds all previous values
- **Key Data**: New high value, previous high, date achieved, time since last high, user ID
- **Consumers**: Achievement service, notification system, milestone tracker

### SecurityEvents

#### SuspiciousTransactionDetected
- **Description**: Unusual or potentially unauthorized transaction identified
- **Triggered When**: Transaction pattern analysis flags anomaly
- **Key Data**: Transaction ID, anomaly type, risk level, detection timestamp, user notified
- **Consumers**: Security alert, user notification, transaction hold service

#### WalletCompromiseRisk
- **Description**: Potential security risk to wallet detected
- **Triggered When**: Multiple failed access attempts or unusual activity pattern
- **Key Data**: Wallet ID, risk type, risk level, recommended actions, detection timestamp
- **Consumers**: Security alert system, wallet freeze service, user emergency contact
