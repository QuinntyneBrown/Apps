# Domain Events - Investment Portfolio Tracker

## Overview
This document defines the domain events tracked in the Investment Portfolio Tracker application. These events capture significant business occurrences related to multi-account portfolio management, position tracking, performance analytics, and investment decision-making.

## Events

### PortfolioEvents

#### PortfolioCreated
- **Description**: A new investment portfolio has been created
- **Triggered When**: User sets up a portfolio to track investments across accounts
- **Key Data**: Portfolio ID, portfolio name, strategy type, risk tolerance, user ID, timestamp
- **Consumers**: Portfolio manager, asset allocator, dashboard UI

#### PortfolioRebalanced
- **Description**: Portfolio has been rebalanced to match target allocation
- **Triggered When**: User executes trades to restore target asset allocation percentages
- **Key Data**: Portfolio ID, previous allocation, new allocation, trades executed, rebalance date, user ID
- **Consumers**: Performance tracker, tax calculator, transaction logger, compliance checker

#### PortfolioValueUpdated
- **Description**: Total portfolio value has been recalculated
- **Triggered When**: Market prices update or positions change
- **Key Data**: Portfolio ID, previous value, new value, change amount, change percentage, timestamp
- **Consumers**: Performance analyzer, dashboard UI, alert service

### PositionEvents

#### PositionOpened
- **Description**: A new investment position has been established
- **Triggered When**: User purchases a stock, bond, ETF, or other security
- **Key Data**: Position ID, ticker symbol, quantity, purchase price, purchase date, account ID, commission, user ID
- **Consumers**: Portfolio aggregator, cost basis tracker, tax lot manager, performance calculator

#### PositionIncreased
- **Description**: Additional shares/units have been added to an existing position
- **Triggered When**: User buys more of a security they already own
- **Key Data**: Position ID, additional quantity, purchase price, purchase date, new average cost, user ID
- **Consumers**: Cost basis calculator, position tracker, tax lot manager

#### PositionReduced
- **Description**: Shares/units have been sold from an existing position
- **Triggered When**: User sells part of their holdings in a security
- **Key Data**: Position ID, quantity sold, sale price, sale date, realized gain/loss, remaining quantity, user ID
- **Consumers**: Tax calculator, capital gains tracker, performance analyzer, position manager

#### PositionClosed
- **Description**: An investment position has been completely liquidated
- **Triggered When**: User sells all shares/units of a security
- **Key Data**: Position ID, total quantity sold, average sale price, total realized gain/loss, holding period, close date, user ID
- **Consumers**: Tax reporter, performance analyzer, historical data archiver, portfolio rebalancer

### AccountEvents

#### InvestmentAccountLinked
- **Description**: An external brokerage or investment account has been connected
- **Triggered When**: User successfully links a brokerage, 401k, or IRA account
- **Key Data**: Account ID, institution name, account type, account number (masked), link timestamp, user ID
- **Consumers**: Data sync service, portfolio aggregator, security manager

#### AccountHoldingsImported
- **Description**: Investment holdings have been imported from a linked account
- **Triggered When**: Initial import or scheduled sync retrieves account positions
- **Key Data**: Account ID, number of positions, total value, import timestamp, sync status, user ID
- **Consumers**: Position manager, portfolio aggregator, performance calculator

#### AccountSyncFailed
- **Description**: Automatic synchronization of account data failed
- **Triggered When**: Sync process encounters authentication or connection errors
- **Key Data**: Account ID, error type, error message, failure timestamp, retry count, user ID
- **Consumers**: Error handler, notification service, retry scheduler

### PerformanceEvents

#### PerformanceCalculated
- **Description**: Investment performance metrics have been computed
- **Triggered When**: Daily/weekly calculation cycle runs or user requests performance analysis
- **Key Data**: Portfolio ID, time period, total return, annualized return, benchmark comparison, timestamp
- **Consumers**: Performance dashboard, analytics service, report generator

#### BenchmarkOutperformed
- **Description**: Portfolio performance has exceeded its benchmark index
- **Triggered When**: Performance calculation shows returns above benchmark over a period
- **Key Data**: Portfolio ID, portfolio return, benchmark return, outperformance amount, time period, timestamp
- **Consumers**: Achievement service, notification system, performance reporter

#### AllocationDriftDetected
- **Description**: Current asset allocation has deviated significantly from targets
- **Triggered When**: Any asset class percentage moves beyond tolerance bands (e.g., ±5%)
- **Key Data**: Portfolio ID, target allocation, current allocation, drift percentages, detection timestamp
- **Consumers**: Rebalancing advisor, alert service, notification system

### DividendEvents

#### DividendReceived
- **Description**: Dividend payment has been received from a position
- **Triggered When**: Dividend is paid into account for a held security
- **Key Data**: Position ID, dividend amount, payment date, dividend type (qualified/ordinary), ex-dividend date, user ID
- **Consumers**: Income tracker, tax calculator, reinvestment service, performance calculator

#### DividendReinvested
- **Description**: Dividend payment has been automatically reinvested
- **Triggered When**: DRIP (Dividend Reinvestment Plan) purchases additional shares
- **Key Data**: Position ID, dividend amount, shares purchased, purchase price, reinvestment date, user ID
- **Consumers**: Position tracker, cost basis calculator, transaction logger

### TransactionEvents

#### TradeExecuted
- **Description**: A buy or sell order has been completed
- **Triggered When**: Trade order is filled by the broker
- **Key Data**: Transaction ID, trade type (buy/sell), ticker symbol, quantity, execution price, commission, trade date, user ID
- **Consumers**: Position manager, cost basis tracker, performance calculator, tax reporter

#### TradeImported
- **Description**: Historical trade data has been imported
- **Triggered When**: User imports trades from CSV, broker export, or manual entry
- **Key Data**: Transaction IDs, import source, number of trades, date range, import timestamp, user ID
- **Consumers**: Position reconciler, cost basis calculator, performance analyzer

### AlertEvents

#### PriceTargetReached
- **Description**: A security price has reached a user-defined target
- **Triggered When**: Market price crosses the target price threshold
- **Key Data**: Position ID, ticker symbol, target price, current price, alert type (above/below), timestamp
- **Consumers**: Notification service, trading suggestion engine

#### PortfolioLossLimitReached
- **Description**: Portfolio has declined to a predefined loss threshold
- **Triggered When**: Portfolio value drops by specified percentage or amount
- **Key Data**: Portfolio ID, loss threshold, current loss, portfolio value, timestamp
- **Consumers**: Risk management service, alert notification, defensive strategy advisor

#### ConcentrationRiskDetected
- **Description**: Single position represents excessive percentage of portfolio
- **Triggered When**: Any position exceeds concentration limit (e.g., >20% of portfolio)
- **Key Data**: Position ID, ticker symbol, position percentage, concentration limit, timestamp
- **Consumers**: Risk analyzer, diversification advisor, alert service
