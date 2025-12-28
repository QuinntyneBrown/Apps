# Domain Events - Mortgage Payoff Optimizer

## Overview
This document defines the domain events tracked in the Mortgage Payoff Optimizer application. These events capture significant business occurrences related to mortgage management, payment strategies, refinancing analysis, and loan optimization.

## Events

### MortgageEvents

#### MortgageAdded
- **Description**: A mortgage loan has been added to the system
- **Triggered When**: User inputs their mortgage details for tracking and optimization
- **Key Data**: Mortgage ID, principal amount, interest rate, loan term, start date, lender, property address, user ID, timestamp
- **Consumers**: Payment calculator, amortization scheduler, dashboard UI

#### MortgageTermsUpdated
- **Description**: Mortgage terms have been modified
- **Triggered When**: Refinance occurs or user corrects mortgage details
- **Key Data**: Mortgage ID, previous terms, new terms (rate/term/balance), update reason, effective date, user ID
- **Consumers**: Payment recalculator, savings analyzer, notification service

#### MortgagePaidOff
- **Description**: Mortgage has been completely paid off
- **Triggered When**: Final payment reduces balance to zero
- **Key Data**: Mortgage ID, original amount, total interest paid, payoff date, years to payoff, savings from strategy, user ID
- **Consumers**: Achievement service, historical archiver, analytics service, celebration trigger

### PaymentEvents

#### RegularPaymentMade
- **Description**: Scheduled mortgage payment has been recorded
- **Triggered When**: User logs monthly mortgage payment
- **Key Data**: Payment ID, mortgage ID, amount, payment date, principal portion, interest portion, remaining balance, user ID
- **Consumers**: Balance updater, amortization tracker, payment history, progress calculator

#### ExtraPaymentMade
- **Description**: Additional principal payment has been made beyond regular payment
- **Triggered When**: User makes extra payment toward principal
- **Key Data**: Payment ID, mortgage ID, extra amount, payment date, interest saved, time saved, new payoff date, user ID
- **Consumers**: Payoff accelerator, savings calculator, progress tracker, motivation service

#### BiweeklyPaymentScheduled
- **Description**: Biweekly payment strategy has been activated
- **Triggered When**: User switches from monthly to biweekly payment schedule
- **Key Data**: Mortgage ID, biweekly amount, start date, projected savings, new payoff date, user ID, timestamp
- **Consumers**: Payment scheduler, savings projector, amortization recalculator

#### PaymentMissed
- **Description**: Scheduled payment was not made by due date
- **Triggered When**: Payment due date passes without recorded payment
- **Key Data**: Payment ID, mortgage ID, amount due, due date, days late, user ID, timestamp
- **Consumers**: Alert service, late fee calculator, notification system

### StrategyEvents

#### PayoffStrategyCreated
- **Description**: New mortgage payoff strategy has been configured
- **Triggered When**: User sets up a specific payoff approach (extra payments, biweekly, lump sum, etc.)
- **Key Data**: Strategy ID, mortgage ID, strategy type, parameters, projected payoff date, projected savings, user ID, timestamp
- **Consumers**: Strategy executor, savings calculator, comparison analyzer

#### PayoffStrategyCompared
- **Description**: Multiple payoff strategies have been compared side-by-side
- **Triggered When**: User analyzes different approaches to paying off mortgage
- **Key Data**: Strategy IDs compared, payoff dates, total interest for each, total savings, recommended strategy, timestamp
- **Consumers**: Decision support system, visualization service, recommendation engine

#### PayoffStrategyActivated
- **Description**: User has committed to a specific payoff strategy
- **Triggered When**: User selects and begins following a payoff plan
- **Key Data**: Strategy ID, mortgage ID, activation date, commitment level, user ID
- **Consumers**: Payment scheduler, progress tracker, accountability system

#### StrategyMilestoneReached
- **Description**: Significant progress milestone achieved in payoff strategy
- **Triggered When**: Key thresholds reached (25%/50%/75% paid, X years saved, etc.)
- **Key Data**: Milestone type, achievement date, progress percentage, interest saved to date, user ID
- **Consumers**: Achievement service, notification system, motivation tracker

### RefinanceEvents

#### RefinanceOpportunityIdentified
- **Description**: Market conditions suggest potential refinancing benefit
- **Triggered When**: Interest rates drop below user's current rate by threshold amount
- **Key Data**: Mortgage ID, current rate, market rate, potential savings, break-even period, timestamp
- **Consumers**: Alert service, refinance analyzer, notification system

#### RefinanceAnalysisRequested
- **Description**: User has requested detailed refinance scenario analysis
- **Triggered When**: User initiates refinance calculator with specific terms
- **Key Data**: Analysis ID, mortgage ID, proposed terms, closing costs, analysis timestamp, user ID
- **Consumers**: Refinance calculator, break-even analyzer, comparison generator

#### RefinanceExecuted
- **Description**: Mortgage has been refinanced with new terms
- **Triggered When**: User completes refinance and updates mortgage details
- **Key Data**: Mortgage ID, old terms, new terms, closing costs, expected lifetime savings, refinance date, user ID
- **Consumers**: Mortgage updater, savings tracker, payment recalculator, historical logger

#### RefinanceOpportunityDismissed
- **Description**: User has declined to pursue a refinance opportunity
- **Triggered When**: User explicitly dismisses refinance recommendation
- **Key Data**: Opportunity ID, mortgage ID, dismissal reason, dismissed date, user ID
- **Consumers**: Recommendation suppressor, preference learner, analytics tracker

### AmortizationEvents

#### AmortizationScheduleGenerated
- **Description**: Complete payment schedule has been calculated
- **Triggered When**: Mortgage added or terms updated
- **Key Data**: Mortgage ID, schedule array, total payments, total interest, payoff date, generation timestamp
- **Consumers**: Schedule viewer, payment tracker, progress calculator

#### AmortizationScheduleRecalculated
- **Description**: Payment schedule has been updated based on strategy changes
- **Triggered When**: Extra payments or strategy changes affect schedule
- **Key Data**: Mortgage ID, previous payoff date, new payoff date, months saved, interest saved, recalc timestamp
- **Consumers**: Progress tracker, savings visualizer, notification service

#### PrincipalPercentageIncreased
- **Description**: Principal portion of payment has exceeded interest portion
- **Triggered When**: Payment composition shifts to majority principal
- **Key Data**: Mortgage ID, payment number, principal amount, interest amount, crossover date
- **Consumers**: Milestone tracker, achievement service, visualization updater

### SavingsEvents

#### InterestSavingsCalculated
- **Description**: Total interest savings from optimization strategy computed
- **Triggered When**: Regular calculation cycle or user requests savings analysis
- **Key Data**: Mortgage ID, baseline total interest, optimized total interest, savings amount, savings percentage, timestamp
- **Consumers**: Savings dashboard, motivation system, report generator

#### TimeSavingsCalculated
- **Description**: Time reduction to payoff from optimization calculated
- **Triggered When**: Strategy comparison or progress update runs
- **Key Data**: Mortgage ID, baseline payoff date, optimized payoff date, months saved, years saved, timestamp
- **Consumers**: Progress visualizer, achievement tracker, comparison analyzer

#### SavingsMilestoneReached
- **Description**: Interest savings have reached significant threshold
- **Triggered When**: Cumulative interest saved crosses milestone ($10K, $50K, $100K, etc.)
- **Key Data**: Mortgage ID, milestone amount, achievement date, total saved to date, user ID
- **Consumers**: Achievement service, celebration trigger, notification system

### GoalEvents

#### PayoffGoalSet
- **Description**: Target payoff date or savings goal has been established
- **Triggered When**: User sets desired mortgage-free date or interest savings target
- **Key Data**: Goal ID, mortgage ID, goal type, target date/amount, required monthly extra payment, user ID, timestamp
- **Consumers**: Goal tracker, payment recommender, progress monitor

#### PayoffGoalAdjusted
- **Description**: Payoff goal has been modified
- **Triggered When**: User changes target date or savings objective
- **Key Data**: Goal ID, previous target, new target, adjustment reason, impact on payments, user ID, timestamp
- **Consumers**: Strategy recalculator, payment adjuster, notification service
