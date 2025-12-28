# Domain Events - Bill Payment Scheduler

## Overview
This document defines the domain events tracked in the Bill Payment Scheduler application. These events capture significant business occurrences related to bill tracking, payment scheduling, reminders, cash flow management, and late payment prevention.

## Events

### BillEvents

#### BillAdded
- **Description**: A new recurring or one-time bill has been registered
- **Triggered When**: User creates a bill entry for tracking and scheduling
- **Key Data**: Bill ID, payee name, amount, due date, frequency, category, autopay status, user ID, timestamp
- **Consumers**: Payment scheduler, cash flow projector, reminder service, dashboard UI

#### BillAmountChanged
- **Description**: Bill amount has been updated from previous cycle
- **Triggered When**: User updates bill amount due to rate change or variable billing
- **Key Data**: Bill ID, previous amount, new amount, change percentage, change reason, effective date, user ID
- **Consumers**: Budget adjuster, cash flow recalculator, variance tracker, notification service

#### BillDeleted
- **Description**: A bill has been removed from tracking
- **Triggered When**: User deletes bill (service cancelled, paid off, etc.)
- **Key Data**: Bill ID, payee name, deletion reason, final amount, deletion date, user ID
- **Consumers**: Historical archiver, payment scheduler cleanup, cash flow updater

#### BillDueDateChanged
- **Description**: Bill due date has been modified
- **Triggered When**: User updates due date or billing cycle changes
- **Key Data**: Bill ID, previous due date, new due date, change reason, user ID, timestamp
- **Consumers**: Payment rescheduler, reminder updater, calendar sync, cash flow adjuster

### PaymentEvents

#### PaymentScheduled
- **Description**: Payment has been queued for a future date
- **Triggered When**: User schedules payment in advance of due date
- **Key Data**: Payment ID, bill ID, scheduled date, amount, payment method, user ID, timestamp
- **Consumers**: Payment executor, reminder service, cash flow tracker, calendar integrator

#### PaymentExecuted
- **Description**: Scheduled payment has been processed
- **Triggered When**: Payment date arrives and payment is made
- **Key Data**: Payment ID, bill ID, payment date, amount paid, payment method, confirmation number, user ID
- **Consumers**: Payment history, bill status updater, cash flow recorder, expense tracker

#### PaymentCancelled
- **Description**: Scheduled payment has been cancelled before execution
- **Triggered When**: User cancels a pending payment
- **Key Data**: Payment ID, bill ID, cancellation date, cancelled amount, cancellation reason, user ID
- **Consumers**: Payment queue cleaner, cash flow adjuster, reminder canceller

#### PaymentFailed
- **Description**: Attempted payment was unsuccessful
- **Triggered When**: Payment processing fails due to insufficient funds, account issues, etc.
- **Key Data**: Payment ID, bill ID, failure date, amount, failure reason, retry scheduled, user ID
- **Consumers**: Alert service, retry scheduler, late fee risk tracker, notification system

### ReminderEvents

#### PaymentReminderSent
- **Description**: Notification sent about upcoming bill payment
- **Triggered When**: Bill due date approaching (e.g., 7, 3, 1 day before)
- **Key Data**: Reminder ID, bill ID, due date, amount, days until due, reminder type, sent timestamp
- **Consumers**: Notification service, user email/SMS, reminder tracker

#### LatePaymentWarning
- **Description**: Bill due date has passed without recorded payment
- **Triggered When**: Due date is reached and payment not marked as completed
- **Key Data**: Bill ID, amount due, due date, days overdue, late fee estimate, user ID, timestamp
- **Consumers**: Urgent alert service, late fee calculator, notification system, credit score monitor

#### EarlyPaymentDiscountAvailable
- **Description**: Bill offers discount for early payment
- **Triggered When**: User marks bill as having early payment discount or deadline approaching
- **Key Data**: Bill ID, discount amount, discount percentage, discount deadline, savings opportunity, user ID
- **Consumers**: Savings optimizer, payment recommender, notification service

### AutopayEvents

#### AutopayEnabled
- **Description**: Automatic payment has been activated for a bill
- **Triggered When**: User sets up autopay with payee or bank
- **Key Data**: Bill ID, autopay start date, payment method, authorization details, user ID, timestamp
- **Consumers**: Autopay tracker, manual reminder suppressor, payment executor

#### AutopayDisabled
- **Description**: Automatic payment has been turned off
- **Triggered When**: User cancels autopay enrollment
- **Key Data**: Bill ID, autopay end date, reason for cancellation, user ID, timestamp
- **Consumers**: Manual reminder activator, payment scheduler, notification service

#### AutopayPaymentProcessed
- **Description**: Autopay system has processed a bill payment
- **Triggered When**: Automated payment completes successfully
- **Key Data**: Payment ID, bill ID, payment date, amount, confirmation number, user ID
- **Consumers**: Payment recorder, cash flow tracker, balance updater

### CashFlowEvents

#### CashFlowProjected
- **Description**: Future cash flow has been calculated based on scheduled bills
- **Triggered When**: User requests projection or system runs periodic calculation
- **Key Data**: Projection period, total outflows, daily/weekly/monthly breakdown, low balance dates, timestamp, user ID
- **Consumers**: Cash flow dashboard, low balance alerts, budget planner

#### LowBalanceRiskDetected
- **Description**: Projected cash flow shows potential insufficient funds
- **Triggered When**: Scheduled payments exceed projected available balance
- **Key Data**: Risk date, projected balance, scheduled payments due, shortfall amount, user ID, timestamp
- **Consumers**: Alert service, payment rescheduling advisor, overdraft prevention

#### PaymentCycleOptimized
- **Description**: System has identified better payment timing strategy
- **Triggered When**: Analysis finds opportunities to align payments with income
- **Key Data**: Current cycle, recommended cycle, cash flow improvement, affected bills, timestamp
- **Consumers**: Optimization advisor, notification service, payment rescheduler

### CategoryEvents

#### BillCategorized
- **Description**: Bill has been assigned to a spending category
- **Triggered When**: User assigns or system categorizes bill
- **Key Data**: Bill ID, category name, categorization method (manual/auto), user ID, timestamp
- **Consumers**: Category aggregator, spending analyzer, budget tracker

#### CategorySpendingCalculated
- **Description**: Total bill spending by category has been computed
- **Triggered When**: End of month or user requests category breakdown
- **Key Data**: Category name, period, total amount, number of bills, percentage of total, timestamp
- **Consumers**: Spending dashboard, budget comparison, category trends analyzer

### VariableBillEvents

#### VariableBillEstimated
- **Description**: Estimated amount set for variable bill (utilities, credit cards)
- **Triggered When**: User or system estimates upcoming variable bill amount
- **Key Data**: Bill ID, estimated amount, estimation method, estimation date, user ID
- **Consumers**: Cash flow projector, budget planner, variance tracker

#### ActualBillAmountReceived
- **Description**: Actual bill amount received differs from estimate
- **Triggered When**: User updates estimated bill with actual amount billed
- **Key Data**: Bill ID, estimated amount, actual amount, variance amount, variance percentage, user ID
- **Consumers**: Variance analyzer, estimation adjuster, cash flow updater, budget reconciler

### RecurringBillEvents

#### RecurringBillInstanceCreated
- **Description**: New instance of recurring bill has been generated
- **Triggered When**: Billing cycle creates next occurrence of recurring bill
- **Key Data**: Instance ID, bill ID, due date, amount, cycle number, user ID, timestamp
- **Consumers**: Payment scheduler, reminder service, cash flow projector

#### RecurringPatternUpdated
- **Description**: Recurring bill frequency or pattern has changed
- **Triggered When**: User modifies how often bill recurs
- **Key Data**: Bill ID, previous pattern, new pattern, effective date, user ID
- **Consumers**: Schedule regenerator, reminder recalculator, cash flow updater

### IntegrationEvents

#### BankAccountLinked
- **Description**: Bank account connected for payment automation
- **Triggered When**: User links checking account for bill pay
- **Key Data**: Account ID, bank name, account type, account number (masked), link date, user ID
- **Consumers**: Payment executor, balance checker, security logger

#### BillPaymentConfirmed
- **Description**: Bank or payee has confirmed payment receipt
- **Triggered When**: Confirmation received from payment processor or payee
- **Key Data**: Payment ID, bill ID, confirmation number, confirmation date, settlement date, user ID
- **Consumers**: Payment status updater, reconciliation service, history tracker
