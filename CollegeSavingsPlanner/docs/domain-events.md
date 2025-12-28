# Domain Events - College Savings Planner

## Overview
This document defines the domain events tracked in the College Savings Planner application. These events capture significant business occurrences related to 529 plan management, college cost projections, beneficiary tracking, and education savings strategies.

## Events

### PlanEvents

#### SavingsPlanCreated
- **Description**: A new college savings plan has been established
- **Triggered When**: User creates a 529 or other education savings plan for a beneficiary
- **Key Data**: Plan ID, plan type (529/ESA/UTMA), beneficiary ID, state plan, current balance, user ID, timestamp
- **Consumers**: Plan manager, contribution tracker, projection calculator

#### BeneficiaryAdded
- **Description**: A child or student beneficiary has been added to a savings plan
- **Triggered When**: User designates who will receive the education funds
- **Key Data**: Beneficiary ID, name, birth date, relationship, plan ID, expected enrollment year, user ID, timestamp
- **Consumers**: Cost projector, age-based asset allocator, timeline calculator

#### BeneficiaryChanged
- **Description**: Plan beneficiary has been changed to a different eligible family member
- **Triggered When**: User transfers 529 plan to another qualifying beneficiary
- **Key Data**: Plan ID, previous beneficiary ID, new beneficiary ID, change date, reason, user ID
- **Consumers**: Plan manager, tax impact analyzer, notification service

#### PlanClosed
- **Description**: A college savings plan has been closed or liquidated
- **Triggered When**: All funds are withdrawn or plan is terminated
- **Key Data**: Plan ID, closure reason, final balance, beneficiary outcome, closure date, user ID
- **Consumers**: Historical archiver, tax reporter, performance analyzer

### ContributionEvents

#### ContributionMade
- **Description**: A deposit has been made to the college savings plan
- **Triggered When**: User or contributor adds funds to the plan
- **Key Data**: Contribution ID, plan ID, amount, contributor name, contribution date, payment method, user ID
- **Consumers**: Balance updater, tax benefit calculator, contribution tracker, thank you service

#### RecurringContributionScheduled
- **Description**: Automatic recurring contributions have been set up
- **Triggered When**: User configures automatic monthly/quarterly/annual deposits
- **Key Data**: Schedule ID, plan ID, amount, frequency, start date, end date, payment source, user ID
- **Consumers**: Payment scheduler, contribution executor, reminder service

#### ContributionLimitApproached
- **Description**: Plan contributions are nearing annual or lifetime limits
- **Triggered When**: Total contributions approach state-specific 529 limits
- **Key Data**: Plan ID, current total, contribution limit, remaining amount, limit type, timestamp
- **Consumers**: Alert service, contribution advisor, tax planning module

#### GiftContributionReceived
- **Description**: A third-party gift contribution has been received
- **Triggered When**: Family member or friend contributes to the plan via gift link
- **Key Data**: Contribution ID, plan ID, amount, gift giver name, occasion, gift date, user ID
- **Consumers**: Contribution tracker, thank you email service, notification system

### ProjectionEvents

#### CollegeCostProjected
- **Description**: Future college costs have been calculated for the beneficiary
- **Triggered When**: User runs projection or system updates with new tuition inflation rates
- **Key Data**: Beneficiary ID, projected total cost, cost breakdown (tuition/room/books), school type, enrollment year, timestamp
- **Consumers**: Savings gap calculator, contribution advisor, dashboard UI

#### SavingsGapIdentified
- **Description**: Shortfall between projected savings and college costs detected
- **Triggered When**: Projection shows current savings trajectory won't meet costs
- **Key Data**: Plan ID, beneficiary ID, projected savings, projected costs, gap amount, gap percentage, timestamp
- **Consumers**: Contribution recommendation engine, alert service, planning advisor

#### SavingsGoalAchieved
- **Description**: Savings have reached or exceeded the college cost projection
- **Triggered When**: Account balance meets or surpasses projected need
- **Key Data**: Plan ID, beneficiary ID, goal amount, actual balance, achievement date, user ID
- **Consumers**: Achievement service, notification system, contribution advisor

#### ProjectionParametersUpdated
- **Description**: Key assumptions in college cost projections have been modified
- **Triggered When**: User updates tuition inflation rate, school type, or attendance years
- **Key Data**: Plan ID, parameter changed, previous value, new value, impact on projection, timestamp, user ID
- **Consumers**: Recalculation engine, savings gap analyzer, notification service

### InvestmentEvents

#### InvestmentOptionChanged
- **Description**: Plan's investment allocation has been modified
- **Triggered When**: User changes from one investment portfolio to another
- **Key Data**: Plan ID, previous option, new option, reason, change date, user ID
- **Consumers**: Investment manager, performance tracker, risk analyzer

#### AgeBasedAllocationAdjusted
- **Description**: Age-based portfolio has automatically rebalanced to more conservative allocation
- **Triggered When**: Beneficiary age triggers scheduled asset allocation change
- **Key Data**: Plan ID, beneficiary age, previous allocation, new allocation, adjustment date
- **Consumers**: Investment manager, performance tracker, notification service

#### InvestmentPerformanceCalculated
- **Description**: Plan's investment returns have been calculated
- **Triggered When**: Periodic performance calculation runs (quarterly/annually)
- **Key Data**: Plan ID, time period, return percentage, gain/loss amount, benchmark comparison, timestamp
- **Consumers**: Performance dashboard, projection updater, report generator

### WithdrawalEvents

#### QualifiedWithdrawalMade
- **Description**: Tax-free withdrawal for qualified education expenses has been processed
- **Triggered When**: User withdraws funds for tuition, fees, books, or qualified expenses
- **Key Data**: Withdrawal ID, plan ID, amount, expense type, beneficiary ID, withdrawal date, user ID
- **Consumers**: Tax reporter, balance updater, expense tracker

#### NonQualifiedWithdrawalMade
- **Description**: Withdrawal for non-education purposes has been processed
- **Triggered When**: User withdraws funds for non-qualified expenses
- **Key Data**: Withdrawal ID, plan ID, amount, reason, tax penalty amount, withdrawal date, user ID
- **Consumers**: Tax calculator, penalty tracker, notification service, compliance logger

### ScholarshipEvents

#### ScholarshipAwarded
- **Description**: Beneficiary has received a scholarship
- **Triggered When**: User records scholarship award that may affect savings needs
- **Key Data**: Scholarship ID, beneficiary ID, scholarship name, amount, award year, renewable status, timestamp
- **Consumers**: Cost projector, savings gap recalculator, withdrawal planner

#### ScholarshipWithdrawalProcessed
- **Description**: Penalty-free withdrawal made due to scholarship
- **Triggered When**: User withdraws amount equal to scholarship without penalty
- **Key Data**: Withdrawal ID, plan ID, scholarship ID, amount, beneficiary ID, withdrawal date, user ID
- **Consumers**: Tax reporter, penalty exemption tracker, balance updater
