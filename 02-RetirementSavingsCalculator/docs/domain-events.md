# Domain Events - Retirement Savings Calculator

## Overview
This document defines the domain events tracked in the Retirement Savings Calculator application. These events capture significant business occurrences related to retirement planning, scenario modeling, income stream projections, and savings strategies.

## Events

### ScenarioEvents

#### RetirementScenarioCreated
- **Description**: A new retirement planning scenario has been created
- **Triggered When**: User initializes a new retirement calculation with specific parameters
- **Key Data**: Scenario ID, scenario name, current age, retirement age, life expectancy, user ID, timestamp
- **Consumers**: Calculation engine, scenario manager, comparison service

#### RetirementAgeUpdated
- **Description**: The target retirement age in a scenario has been modified
- **Triggered When**: User adjusts their planned retirement age
- **Key Data**: Scenario ID, previous retirement age, new retirement age, impact on savings needed, timestamp, user ID
- **Consumers**: Recalculation engine, savings gap analyzer, notification service

#### ScenarioDeleted
- **Description**: A retirement scenario has been removed
- **Triggered When**: User deletes a scenario they no longer need
- **Key Data**: Scenario ID, scenario name, deletion timestamp, user ID
- **Consumers**: Data archiver, scenario manager

### SavingsEvents

#### CurrentSavingsUpdated
- **Description**: The current retirement savings balance has been updated
- **Triggered When**: User inputs current 401k, IRA, or other retirement account balances
- **Key Data**: Scenario ID, previous balance, new balance, account types included, timestamp, user ID
- **Consumers**: Calculation engine, progress tracker, savings gap analyzer

#### MonthlyContributionSet
- **Description**: Monthly retirement contribution amount has been defined or changed
- **Triggered When**: User sets or updates their monthly savings contribution
- **Key Data**: Scenario ID, previous contribution, new contribution, contribution frequency, employer match details, timestamp, user ID
- **Consumers**: Projection calculator, savings tracker, compound interest calculator

#### SavingsGoalReached
- **Description**: User's current savings have reached a milestone toward their retirement goal
- **Triggered When**: Current savings crosses predefined percentage thresholds (25%, 50%, 75%, 100%)
- **Key Data**: Scenario ID, milestone percentage, current savings, goal amount, achievement date, user ID
- **Consumers**: Achievement service, notification service, progress visualizer

### IncomeEvents

#### RetirementIncomeStreamAdded
- **Description**: A new income source has been added to retirement projections
- **Triggered When**: User adds Social Security, pension, rental income, or other retirement income
- **Key Data**: Income stream ID, source type, estimated monthly amount, start age, end age, inflation adjustment, user ID
- **Consumers**: Income calculator, cash flow projector, tax estimator

#### SocialSecurityBenefitEstimated
- **Description**: Social Security benefit amount has been calculated or updated
- **Triggered When**: User enters earnings history or updates retirement age affecting benefits
- **Key Data**: Scenario ID, benefit amount, claiming age, reduction/increase percentage, timestamp, user ID
- **Consumers**: Income projector, tax calculator, optimization engine

#### IncomeStreamRemoved
- **Description**: A retirement income source has been removed from projections
- **Triggered When**: User deletes an income stream from their scenario
- **Key Data**: Income stream ID, source type, amount, removal timestamp, user ID
- **Consumers**: Income recalculator, cash flow projector

### ProjectionEvents

#### RetirementProjectionCalculated
- **Description**: Complete retirement savings projection has been computed
- **Triggered When**: User requests calculation or scenario parameters change
- **Key Data**: Scenario ID, total savings at retirement, monthly retirement income, savings gap, success probability, assumptions used, timestamp
- **Consumers**: Results dashboard, recommendation engine, report generator

#### InflationRateAdjusted
- **Description**: The assumed inflation rate for projections has been changed
- **Triggered When**: User modifies the inflation assumption in their scenario
- **Key Data**: Scenario ID, previous rate, new rate, impact on projections, timestamp, user ID
- **Consumers**: Recalculation engine, purchasing power analyzer

#### InvestmentReturnAssumptionUpdated
- **Description**: Expected investment return rate has been modified
- **Triggered When**: User adjusts the assumed annual return on investments
- **Key Data**: Scenario ID, previous return rate, new return rate, risk level, impact on final savings, timestamp, user ID
- **Consumers**: Projection recalculator, risk analyzer, monte carlo simulator

### ExpenseEvents

#### RetirementExpensesEstimated
- **Description**: Estimated monthly or annual retirement expenses have been set
- **Triggered When**: User inputs their expected living expenses in retirement
- **Key Data**: Scenario ID, monthly expense amount, expense categories, inflation adjustment, timestamp, user ID
- **Consumers**: Income needs calculator, savings gap analyzer, budget planner

#### ExpenseCategoryAdded
- **Description**: A new expense category has been added to retirement planning
- **Triggered When**: User adds healthcare, travel, housing, or other expense category
- **Key Data**: Category ID, category name, estimated amount, start year, end year, user ID
- **Consumers**: Expense aggregator, cash flow projector, tax planning module

### ComparisonEvents

#### ScenarioComparisonGenerated
- **Description**: Multiple retirement scenarios have been compared side-by-side
- **Triggered When**: User requests comparison of different retirement strategies
- **Key Data**: Scenario IDs compared, key differences, best/worst case outcomes, timestamp, user ID
- **Consumers**: Comparison visualizer, recommendation engine, decision support system

#### OptimalStrategyIdentified
- **Description**: System has identified the optimal retirement strategy based on user constraints
- **Triggered When**: Analysis completes across multiple scenarios
- **Key Data**: Optimal scenario ID, key factors, trade-offs, confidence score, timestamp, user ID
- **Consumers**: Recommendation service, notification service, report generator
