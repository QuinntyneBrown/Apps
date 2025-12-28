# Domain Events - Household Budget Manager

## Overview
This document defines the domain events tracked in the Household Budget Manager application. These events capture significant business occurrences related to collaborative budgeting, expense tracking, category management, and household financial coordination.

## Events

### BudgetEvents

#### BudgetCreated
- **Description**: A new budget period has been created for the household
- **Triggered When**: User initializes a new monthly, weekly, or custom budget period
- **Key Data**: Budget ID, budget period, start date, end date, total budget amount, household ID, creator user ID, timestamp
- **Consumers**: Budget tracker, notification service, category allocator

#### BudgetCategoryAllocated
- **Description**: Budget amount has been allocated to a spending category
- **Triggered When**: User assigns a portion of the total budget to a specific category
- **Key Data**: Budget ID, category ID, allocated amount, percentage of total, user ID, timestamp
- **Consumers**: Budget tracker, spending monitor, category manager

#### BudgetAmendmentMade
- **Description**: An existing budget allocation has been modified mid-period
- **Triggered When**: Household members adjust category allocations during the budget period
- **Key Data**: Budget ID, category ID, previous amount, new amount, amendment reason, approver ID, timestamp
- **Consumers**: Audit log, budget tracker, notification service, household members

#### BudgetPeriodClosed
- **Description**: A budget period has ended and been finalized
- **Triggered When**: Budget period end date is reached or user manually closes it
- **Key Data**: Budget ID, total budgeted, total spent, surplus/deficit, category breakdown, closure timestamp
- **Consumers**: Historical analyzer, rollover calculator, report generator

### ExpenseEvents

#### ExpenseRecorded
- **Description**: A household expense has been logged
- **Triggered When**: Any household member records a purchase or payment
- **Key Data**: Expense ID, amount, category, description, merchant, payment method, recorder ID, receipt attached, timestamp
- **Consumers**: Budget tracker, category aggregator, spending analyzer, receipt manager

#### ExpenseApproved
- **Description**: A submitted expense has been approved by a household administrator
- **Triggered When**: Expense requiring approval is reviewed and accepted
- **Key Data**: Expense ID, approver ID, approval timestamp, approval notes
- **Consumers**: Budget deduction service, notification service, expense reporter

#### ExpenseRejected
- **Description**: A submitted expense has been rejected
- **Triggered When**: Expense is reviewed and denied by household administrator
- **Key Data**: Expense ID, rejector ID, rejection reason, rejection timestamp
- **Consumers**: Notification service, submitter alert, audit log

#### ExpenseRecategorized
- **Description**: An expense has been moved to a different budget category
- **Triggered When**: User or admin changes the category assignment of an expense
- **Key Data**: Expense ID, previous category, new category, reason, user ID, timestamp
- **Consumers**: Budget recalculator, category tracker, audit log

### CategoryEvents

#### CategoryLimitExceeded
- **Description**: Spending in a category has exceeded its allocated budget
- **Triggered When**: Cumulative expenses in a category surpass the budgeted amount
- **Key Data**: Category ID, budget limit, actual spending, overage amount, timestamp, household ID
- **Consumers**: Alert service, notification system, household members, budget advisor

#### CategoryCreated
- **Description**: A new spending category has been added to the budget
- **Triggered When**: User creates a custom category for tracking specific expenses
- **Key Data**: Category ID, category name, parent category, default allocation, icon, color, creator ID, timestamp
- **Consumers**: Budget allocator, expense tracker, UI customizer

#### SpendingPatternDetected
- **Description**: System has identified a recurring spending pattern in a category
- **Triggered When**: Analysis detects consistent spending behavior over multiple periods
- **Key Data**: Category ID, pattern type, frequency, average amount, confidence level, timestamp
- **Consumers**: Budget optimizer, recommendation engine, forecasting service

### CollaborationEvents

#### HouseholdMemberAdded
- **Description**: A new member has been added to the household budget
- **Triggered When**: User invites and another person accepts household membership
- **Key Data**: Member ID, email, role (admin/member/viewer), invitation timestamp, acceptance timestamp, inviter ID
- **Consumers**: Permission manager, notification service, audit log

#### HouseholdMemberRemoved
- **Description**: A member has been removed from the household budget
- **Triggered When**: Admin removes a member or member leaves the household
- **Key Data**: Member ID, removal reason, removal timestamp, removed by user ID
- **Consumers**: Access control, data archiver, notification service

#### BudgetShared
- **Description**: Budget has been shared with household members or external viewer
- **Triggered When**: User grants viewing or editing access to the budget
- **Key Data**: Budget ID, shared with user IDs, permission level, share timestamp, sharer ID
- **Consumers**: Permission service, notification system, collaboration tracker

### AlertEvents

#### LowBudgetWarning
- **Description**: A category is approaching its budget limit
- **Triggered When**: Spending reaches defined threshold (typically 80% of budget)
- **Key Data**: Category ID, budget limit, current spending, threshold percentage, timestamp, household ID
- **Consumers**: Alert service, notification system, household members

#### SurplusDetected
- **Description**: Significant unspent budget identified at period end
- **Triggered When**: Budget period closes with substantial remaining funds
- **Key Data**: Budget ID, surplus amount, unspent categories, suggestions for reallocation, timestamp
- **Consumers**: Recommendation engine, savings advisor, notification service

### ReconciliationEvents

#### AccountReconciled
- **Description**: Budget expenses have been reconciled with bank statement
- **Triggered When**: User completes reconciliation process matching expenses to transactions
- **Key Data**: Reconciliation ID, account ID, period, matched transactions, discrepancies, reconciler ID, timestamp
- **Consumers**: Accuracy tracker, discrepancy reporter, audit log

#### DiscrepancyIdentified
- **Description**: Mismatch found between budgeted expenses and actual bank transactions
- **Triggered When**: Reconciliation process detects unmatched or duplicate entries
- **Key Data**: Discrepancy ID, amount difference, transaction details, possible causes, timestamp
- **Consumers**: Alert service, reconciliation reviewer, fraud detection
