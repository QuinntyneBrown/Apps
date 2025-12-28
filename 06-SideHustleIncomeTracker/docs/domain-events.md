# Domain Events - Side Hustle Income Tracker

## Overview
This document defines the domain events tracked in the Side Hustle Income Tracker application. These events capture significant business occurrences related to multiple income stream management, profit and loss tracking, client billing, and freelance business analytics.

## Events

### IncomeStreamEvents

#### IncomeStreamCreated
- **Description**: A new side hustle or income source has been registered
- **Triggered When**: User adds a new freelance gig, business, or income-generating activity
- **Key Data**: Stream ID, stream name, business type, start date, expected revenue, user ID, timestamp
- **Consumers**: Income aggregator, dashboard UI, tax category mapper

#### IncomeStreamClosed
- **Description**: A side hustle has been discontinued
- **Triggered When**: User marks an income stream as inactive or completed
- **Key Data**: Stream ID, closure date, total revenue earned, duration active, closure reason, user ID
- **Consumers**: Historical analyzer, performance reporter, tax summarizer

#### IncomeStreamReactivated
- **Description**: A previously closed income stream has been restarted
- **Triggered When**: User resumes a dormant side hustle
- **Key Data**: Stream ID, reactivation date, previous closure date, user ID, timestamp
- **Consumers**: Active stream manager, notification service, dashboard updater

### RevenueEvents

#### IncomeReceived
- **Description**: Payment has been received from a client or income source
- **Triggered When**: User records incoming payment for services or products
- **Key Data**: Income ID, stream ID, amount, client name, payment date, payment method, invoice ID, user ID
- **Consumers**: Revenue tracker, P&L calculator, cash flow analyzer, tax estimator

#### RecurringIncomeScheduled
- **Description**: Recurring revenue stream has been set up
- **Triggered When**: User configures expected recurring income (subscriptions, retainers, etc.)
- **Key Data**: Schedule ID, stream ID, amount, frequency, start date, end date, client ID, user ID
- **Consumers**: Income forecaster, cash flow projector, reminder service

#### PaymentOverdue
- **Description**: Expected payment has not been received by due date
- **Triggered When**: Invoice due date passes without payment recorded
- **Key Data**: Invoice ID, client ID, amount due, due date, days overdue, user ID, timestamp
- **Consumers**: Alert service, collection reminder, client follow-up system

#### LargePaymentReceived
- **Description**: Unusually large payment received compared to typical income
- **Triggered When**: Single payment exceeds threshold (e.g., 3x average payment)
- **Key Data**: Income ID, amount, client name, average comparison, milestone indicator, timestamp
- **Consumers**: Achievement service, tax planning advisor, cash management alert

### ExpenseEvents

#### BusinessExpenseRecorded
- **Description**: A business expense has been logged for a side hustle
- **Triggered When**: User records a deductible business expense
- **Key Data**: Expense ID, stream ID, amount, category, vendor, expense date, receipt attached, tax deductible, user ID
- **Consumers**: P&L calculator, tax deduction tracker, expense analyzer, receipt manager

#### MileageLogged
- **Description**: Business mileage has been recorded
- **Triggered When**: User logs miles driven for business purposes
- **Key Data**: Mileage ID, stream ID, miles, purpose, start location, end location, date, IRS rate, user ID
- **Consumers**: Mileage deduction calculator, expense tracker, tax reporter

#### ExpenseCategoryExceeded
- **Description**: Spending in an expense category has exceeded budget
- **Triggered When**: Category expenses surpass planned or historical averages
- **Key Data**: Stream ID, category, budget amount, actual spending, overage amount, timestamp
- **Consumers**: Budget alert service, expense optimizer, notification system

### ClientEvents

#### ClientAdded
- **Description**: A new client has been added to a side hustle
- **Triggered When**: User creates a client record for tracking projects and payments
- **Key Data**: Client ID, client name, contact info, stream ID, acquisition date, user ID, timestamp
- **Consumers**: Client manager, invoice generator, relationship tracker

#### ClientInvoiced
- **Description**: An invoice has been sent to a client
- **Triggered When**: User generates and sends invoice for services or products
- **Key Data**: Invoice ID, client ID, amount, line items, due date, invoice date, stream ID, user ID
- **Consumers**: Payment tracker, accounts receivable, reminder scheduler

#### ClientPaid
- **Description**: Client has completed payment for an invoice
- **Triggered When**: Payment received matches an outstanding invoice
- **Key Data**: Invoice ID, client ID, payment amount, payment date, payment method, user ID
- **Consumers**: Accounts receivable updater, revenue tracker, client history

#### ClientLost
- **Description**: A client relationship has ended
- **Triggered When**: User marks client as inactive or lost
- **Key Data**: Client ID, stream ID, end date, total revenue from client, loss reason, user ID
- **Consumers**: Client retention analyzer, revenue forecaster, historical archiver

### ProfitabilityEvents

#### ProfitLossCalculated
- **Description**: Profit and loss statement has been generated for a period
- **Triggered When**: User requests P&L report or end of month calculation runs
- **Key Data**: Stream ID, period, total revenue, total expenses, net profit/loss, profit margin, timestamp
- **Consumers**: Financial dashboard, tax estimator, performance analyzer, report generator

#### ProfitabilityThresholdReached
- **Description**: Side hustle has reached profitability milestone
- **Triggered When**: Net profit crosses significant threshold or turns positive
- **Key Data**: Stream ID, milestone amount, achievement date, time to profitability, user ID
- **Consumers**: Achievement service, notification system, motivation tracker

#### MonthlyRevenueRecordSet
- **Description**: Highest monthly revenue achieved for an income stream
- **Triggered When**: Month's revenue exceeds all previous monthly totals
- **Key Data**: Stream ID, record amount, previous record, month, growth percentage, user ID
- **Consumers**: Achievement service, performance analyzer, goal tracker

### TaxEvents

#### QuarterlyTaxEstimated
- **Description**: Estimated quarterly tax obligation has been calculated
- **Triggered When**: End of quarter or user requests tax estimate
- **Key Data**: Quarter, total income, total expenses, estimated tax owed, payment due date, user ID
- **Consumers**: Tax payment reminder, cash reserve advisor, tax planning module

#### TaxPaymentScheduled
- **Description**: Quarterly estimated tax payment has been scheduled
- **Triggered When**: User sets up or confirms quarterly tax payment
- **Key Data**: Payment ID, quarter, amount, payment date, payment method, user ID
- **Consumers**: Payment scheduler, cash flow planner, reminder service

#### DeductionOpportunityIdentified
- **Description**: System has identified potential tax deduction
- **Triggered When**: Analysis detects expenses that may qualify for deductions
- **Key Data**: Opportunity type, estimated deduction value, expense category, supporting documents needed, timestamp
- **Consumers**: Tax optimization advisor, notification service, documentation tracker

### GoalEvents

#### IncomeGoalSet
- **Description**: Revenue target has been set for an income stream
- **Triggered When**: User defines monthly, quarterly, or annual income goal
- **Key Data**: Goal ID, stream ID, target amount, time period, goal deadline, user ID, timestamp
- **Consumers**: Progress tracker, motivation system, forecasting service

#### IncomeGoalAchieved
- **Description**: Revenue target has been met or exceeded
- **Triggered When**: Income reaches or surpasses goal amount
- **Key Data**: Goal ID, stream ID, target amount, actual amount, achievement date, user ID
- **Consumers**: Achievement service, notification system, goal history tracker
