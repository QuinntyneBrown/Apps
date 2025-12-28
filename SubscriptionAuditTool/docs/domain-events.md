# Domain Events - Subscription Audit Tool

## Overview
This document defines the domain events tracked in the Subscription Audit Tool application. These events capture significant business occurrences related to recurring subscription management, cancellation tracking, renewal alerts, and subscription spend optimization.

## Events

### SubscriptionEvents

#### SubscriptionAdded
- **Description**: A new recurring subscription has been registered
- **Triggered When**: User adds a subscription to track (streaming, software, membership, etc.)
- **Key Data**: Subscription ID, service name, cost, billing frequency, start date, category, payment method, user ID, timestamp
- **Consumers**: Subscription tracker, cost aggregator, calendar scheduler, dashboard UI

#### SubscriptionRenewed
- **Description**: A subscription has automatically renewed
- **Triggered When**: Billing cycle completes and subscription continues
- **Key Data**: Subscription ID, renewal date, amount charged, next renewal date, renewal count, user ID
- **Consumers**: Expense tracker, renewal calendar, spending analyzer

#### SubscriptionCancelled
- **Description**: A subscription has been terminated
- **Triggered When**: User cancels subscription or marks it as ended
- **Key Data**: Subscription ID, cancellation date, cancellation reason, total amount paid, duration active, savings from cancellation, user ID
- **Consumers**: Savings calculator, historical archiver, category analyzer, reminder cleaner

#### SubscriptionPaused
- **Description**: A subscription has been temporarily suspended
- **Triggered When**: User pauses subscription without fully cancelling
- **Key Data**: Subscription ID, pause date, expected resume date, pause reason, user ID, timestamp
- **Consumers**: Reminder suppressor, spending calculator, notification service

#### SubscriptionReactivated
- **Description**: A cancelled or paused subscription has been restarted
- **Triggered When**: User resumes a previously stopped subscription
- **Key Data**: Subscription ID, reactivation date, previous end date, new terms, user ID
- **Consumers**: Active subscription manager, spending recalculator, reminder scheduler

### PricingEvents

#### PriceIncreaseDetected
- **Description**: Subscription cost has increased from previous billing
- **Triggered When**: User updates cost or automatic detection identifies price change
- **Key Data**: Subscription ID, previous price, new price, increase amount, increase percentage, effective date, user ID
- **Consumers**: Alert service, budget impact analyzer, cancellation recommender, notification system

#### PriceIncreaseScheduled
- **Description**: Service provider has announced upcoming price increase
- **Triggered When**: User enters advance notice of price change
- **Key Data**: Subscription ID, current price, future price, effective date, notice date, user ID, timestamp
- **Consumers**: Cancellation decision helper, alternative finder, reminder scheduler

#### TrialPeriodEnding
- **Description**: Free trial period is about to expire
- **Triggered When**: Trial end date approaches (e.g., 3 days before)
- **Key Data**: Subscription ID, trial end date, post-trial cost, days remaining, user ID, timestamp
- **Consumers**: Alert service, cancellation reminder, decision prompt

#### TrialConverted
- **Description**: Free trial has converted to paid subscription
- **Triggered When**: Trial period ends and user keeps subscription active
- **Key Data**: Subscription ID, trial start date, conversion date, new billing amount, user ID
- **Consumers**: Subscription tracker, expense updater, spending analyzer

### ReminderEvents

#### RenewalReminderSent
- **Description**: Notification sent about upcoming subscription renewal
- **Triggered When**: Renewal date approaching (configurable: 7, 3, 1 day before)
- **Key Data**: Subscription ID, renewal date, amount, days until renewal, reminder sent timestamp
- **Consumers**: Notification service, user email/SMS, reminder tracker

#### CancellationDeadlineApproaching
- **Description**: Deadline to cancel before next billing is near
- **Triggered When**: User-defined cancellation window approaching
- **Key Data**: Subscription ID, cancellation deadline, next billing date, amount, user ID, timestamp
- **Consumers**: Alert service, cancellation guide, notification system

#### UnusedSubscriptionIdentified
- **Description**: System detected a subscription not being actively used
- **Triggered When**: Heuristic analysis or user marking indicates low usage
- **Key Data**: Subscription ID, last reported use date, monthly cost, inactivity period, user ID
- **Consumers**: Cancellation recommender, savings opportunity alert, usage tracker

### SpendingEvents

#### MonthlySubscriptionCostCalculated
- **Description**: Total monthly subscription spending has been computed
- **Triggered When**: End of month or user requests spending summary
- **Key Data**: Month, total monthly cost, number of active subscriptions, category breakdown, timestamp, user ID
- **Consumers**: Spending dashboard, budget analyzer, trend tracker, report generator

#### AnnualSubscriptionCostProjected
- **Description**: Projected annual subscription spending calculated
- **Triggered When**: User requests projection or periodic calculation runs
- **Key Data**: Projected annual cost, current monthly average, trending direction, year, user ID, timestamp
- **Consumers**: Budget planner, savings opportunity finder, financial dashboard

#### SpendingLimitExceeded
- **Description**: Total subscription spending has exceeded user-defined budget
- **Triggered When**: Monthly subscription costs surpass budget threshold
- **Key Data**: Budget limit, actual spending, overage amount, number of subscriptions, timestamp, user ID
- **Consumers**: Alert service, cancellation advisor, notification system

#### SavingsOpportunityIdentified
- **Description**: System found potential to save money on subscriptions
- **Triggered When**: Analysis detects duplicate services, cheaper alternatives, or unused subscriptions
- **Key Data**: Opportunity type, potential monthly savings, affected subscriptions, recommendation, timestamp
- **Consumers**: Recommendation engine, alert service, optimization advisor

### CategoryEvents

#### SubscriptionCategorized
- **Description**: Subscription has been assigned to a category
- **Triggered When**: User assigns or system auto-categorizes subscription
- **Key Data**: Subscription ID, category name, categorization method (manual/auto), user ID, timestamp
- **Consumers**: Category analyzer, spending breakdown, filter service

#### CategorySpendingAnalyzed
- **Description**: Total spending calculated for a subscription category
- **Triggered When**: User requests category analysis or periodic rollup runs
- **Key Data**: Category name, total monthly cost, number of subscriptions, percentage of total, timestamp
- **Consumers**: Spending dashboard, category comparison, budget allocator

### DuplicateEvents

#### DuplicateSubscriptionDetected
- **Description**: System identified potentially duplicate or overlapping services
- **Triggered When**: Analysis finds similar services or multiple subscriptions to same provider
- **Key Data**: Subscription IDs, similarity reason, combined cost, consolidation opportunity, timestamp
- **Consumers**: Optimization advisor, alert service, cancellation recommender

#### DuplicateResolved
- **Description**: User has addressed duplicate subscription issue
- **Triggered When**: User cancels one of duplicate subscriptions or marks as intentional
- **Key Data**: Subscription IDs involved, resolution action, savings realized, resolution date, user ID
- **Consumers**: Savings tracker, optimization tracker, historical logger

### PaymentEvents

#### PaymentMethodUpdated
- **Description**: Subscription billing payment method has changed
- **Triggered When**: User updates credit card or payment source for subscription
- **Key Data**: Subscription ID, previous payment method, new payment method, update date, user ID
- **Consumers**: Payment tracker, renewal validator, security logger

#### PaymentFailed
- **Description**: Subscription payment attempt was unsuccessful
- **Triggered When**: Billing fails due to insufficient funds, expired card, etc.
- **Key Data**: Subscription ID, attempted amount, failure reason, failure date, retry scheduled, user ID
- **Consumers**: Alert service, payment method updater, subscription status tracker
