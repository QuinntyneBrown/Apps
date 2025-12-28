# Domain Events - Personal Net Worth Dashboard

## Overview
This document defines the domain events tracked in the Personal Net Worth Dashboard application. These events capture significant business occurrences related to asset management, liability tracking, investment monitoring, and net worth calculations.

## Events

### AssetEvents

#### AssetAdded
- **Description**: A new asset has been added to the user's portfolio
- **Triggered When**: User successfully creates a new asset entry (bank account, investment, property, vehicle, etc.)
- **Key Data**: Asset ID, asset type, initial value, currency, acquisition date, user ID, timestamp
- **Consumers**: Net worth calculator, dashboard aggregator, analytics service, audit log

#### AssetValueUpdated
- **Description**: The value of an existing asset has been modified
- **Triggered When**: User manually updates asset value or automated sync updates the value
- **Key Data**: Asset ID, previous value, new value, update source (manual/automated), timestamp, user ID
- **Consumers**: Net worth calculator, trend analyzer, notification service, change history tracker

#### AssetRemoved
- **Description**: An asset has been removed from the portfolio
- **Triggered When**: User deletes an asset or marks it as sold/disposed
- **Key Data**: Asset ID, asset type, final value, removal reason, timestamp, user ID
- **Consumers**: Net worth calculator, historical data archiver, audit log

### LiabilityEvents

#### LiabilityAdded
- **Description**: A new liability has been added to the user's financial profile
- **Triggered When**: User creates a new liability entry (mortgage, loan, credit card debt, etc.)
- **Key Data**: Liability ID, liability type, principal amount, interest rate, creditor, due date, user ID, timestamp
- **Consumers**: Net worth calculator, debt tracker, payment reminder service, dashboard aggregator

#### LiabilityBalanceUpdated
- **Description**: The outstanding balance of a liability has changed
- **Triggered When**: User records a payment or updates the balance manually
- **Key Data**: Liability ID, previous balance, new balance, payment amount, timestamp, user ID
- **Consumers**: Net worth calculator, debt payoff tracker, progress analyzer, notification service

#### LiabilityPaidOff
- **Description**: A liability has been completely paid off
- **Triggered When**: Liability balance reaches zero
- **Key Data**: Liability ID, liability type, original amount, total interest paid, payoff date, duration, user ID
- **Consumers**: Achievement service, analytics service, notification service, historical data archiver

### NetWorthEvents

#### NetWorthCalculated
- **Description**: The user's net worth has been recalculated
- **Triggered When**: Assets or liabilities are added, updated, or removed
- **Key Data**: Net worth amount, total assets, total liabilities, calculation timestamp, change from previous, user ID
- **Consumers**: Dashboard UI, trend analyzer, milestone tracker, notification service

#### NetWorthMilestoneReached
- **Description**: User's net worth has reached a significant milestone
- **Triggered When**: Net worth crosses predefined thresholds ($0, $100K, $500K, $1M, etc.)
- **Key Data**: Milestone amount, achievement date, time to reach milestone, user ID
- **Consumers**: Achievement service, notification service, analytics service

### AccountEvents

#### AccountLinked
- **Description**: An external financial account has been linked for automatic syncing
- **Triggered When**: User successfully connects a bank, brokerage, or other financial institution
- **Key Data**: Account ID, institution name, account type, sync frequency, link timestamp, user ID
- **Consumers**: Sync scheduler, asset manager, security audit log

#### AccountSyncCompleted
- **Description**: Automatic synchronization of linked account data has completed
- **Triggered When**: Scheduled or manual sync finishes successfully
- **Key Data**: Account ID, sync timestamp, items updated, new balance, sync status, user ID
- **Consumers**: Asset value updater, notification service, sync history tracker

#### AccountSyncFailed
- **Description**: Automatic synchronization of linked account failed
- **Triggered When**: Sync process encounters an error or authentication failure
- **Key Data**: Account ID, error type, error message, failure timestamp, retry count, user ID
- **Consumers**: Error notification service, support alert system, retry scheduler

### InvestmentEvents

#### InvestmentPositionAdded
- **Description**: A new investment position has been added to the portfolio
- **Triggered When**: User adds a stock, bond, mutual fund, or other investment
- **Key Data**: Position ID, ticker symbol, quantity, purchase price, purchase date, asset class, user ID
- **Consumers**: Portfolio analyzer, asset allocator, performance tracker

#### InvestmentReturnsCalculated
- **Description**: Investment returns and performance metrics have been calculated
- **Triggered When**: Market data updates or user requests performance analysis
- **Key Data**: Position ID, return percentage, gain/loss amount, time period, calculation timestamp, user ID
- **Consumers**: Performance dashboard, analytics service, tax reporting module

### CategoryEvents

#### AssetCategoryCreated
- **Description**: A new custom asset category has been created
- **Triggered When**: User creates a custom category for organizing assets
- **Key Data**: Category ID, category name, parent category, color, icon, user ID, timestamp
- **Consumers**: Asset organizer, dashboard UI, filter service

#### AssetRecategorized
- **Description**: An asset has been moved to a different category
- **Triggered When**: User changes the category of an existing asset
- **Key Data**: Asset ID, previous category, new category, timestamp, user ID
- **Consumers**: Dashboard aggregator, analytics service, organization tracker
