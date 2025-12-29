# Requirements - Personal Net Worth Dashboard

## Overview
A comprehensive dashboard for tracking assets, liabilities, investments, and net worth calculations to provide a complete financial picture.

## Features and Requirements

### 1. Asset Management
- **AM-001**: Users shall add assets (bank accounts, investments, property, vehicles) with type, initial value, currency
- **AM-002**: Users shall update asset values manually or via automated sync
- **AM-003**: Users shall remove assets with reason (sold/disposed)
- **AM-004**: System shall track asset value change history
- **AM-005**: Users shall categorize assets for organization

### 2. Liability Management
- **LM-001**: Users shall add liabilities (mortgages, loans, credit card debt) with principal, interest rate, creditor
- **LM-002**: Users shall update liability balances when payments made
- **LM-003**: System shall automatically mark liabilities as paid off when balance reaches zero
- **LM-004**: System shall track total interest paid per liability
- **LM-005**: Users shall view liability payment history

### 3. Net Worth Tracking
- **NWT-001**: System shall calculate net worth automatically (Total Assets - Total Liabilities)
- **NWT-002**: System shall recalculate net worth when assets/liabilities change
- **NWT-003**: System shall track net worth milestones ($0, $100K, $500K, $1M)
- **NWT-004**: System shall show net worth trends over time
- **NWT-005**: Users shall view net worth breakdown by asset/liability categories

### 4. Account Integration
- **AI-001**: Users shall link external accounts (banks, brokerages) for automatic syncing
- **AI-002**: System shall sync account balances on schedule or on-demand
- **AI-003**: System shall notify users of sync failures with error details
- **AI-004**: Users shall view sync history and status
- **AI-005**: System shall use secure OAuth for account connections

### 5. Investment Tracking
- **IT-001**: Users shall add investment positions (stocks, bonds, funds) with ticker, quantity, purchase price
- **IT-002**: System shall calculate investment returns and performance
- **IT-003**: System shall show portfolio allocation by asset class
- **IT-004**: System shall track cost basis and unrealized gains/losses
- **IT-005**: Users shall view investment performance over time periods

### 6. Category Management
- **CM-001**: Users shall create custom asset categories
- **CM-002**: Users shall recategorize assets for better organization
- **CM-003**: System shall show category-level totals and percentages
- **CM-004**: System shall support hierarchical categories

## System-Wide Requirements
- Performance: Dashboard load < 2s, support 500+ assets
- Security: Bank-level encryption, MFA support, secure credential storage
- Availability: 99.9% uptime, hourly backups
- Compliance: Financial data protection regulations
