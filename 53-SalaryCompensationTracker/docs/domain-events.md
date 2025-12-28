# Domain Events - Salary & Compensation Tracker

## Overview
This application tracks domain events related to salary history, compensation changes, market rate comparisons, and career financial progression. These events enable professionals to make informed career decisions and negotiate effectively.

## Events

### CompensationEvents

#### BaseCompensationRecorded
- **Description**: A new base salary or compensation amount has been recorded
- **Triggered When**: User enters current or historical base salary information
- **Key Data**: Compensation ID, amount, currency, effective date, job title, company, employment type, recorded by, timestamp
- **Consumers**: Compensation history tracker, market comparison engine, tax calculator, career progression analyzer

#### CompensationIncreaseReceived
- **Description**: User has received a salary increase or raise
- **Triggered When**: User records a salary increase event
- **Key Data**: Increase ID, previous amount, new amount, increase percentage, increase type (merit/promotion/market adjustment), effective date, reason
- **Consumers**: Career milestone tracker, market trend analyzer, notification service, financial planning tool

#### BonusReceived
- **Description**: User has received a bonus payment
- **Triggered When**: User records a bonus payment
- **Key Data**: Bonus ID, amount, bonus type (performance/signing/retention/annual), payment date, performance period, company, notes
- **Consumers**: Total compensation calculator, tax estimator, financial planning, income tracker

#### StockEquityGranted
- **Description**: User has been granted stock options or equity compensation
- **Triggered When**: User records an equity grant
- **Key Data**: Grant ID, grant type (RSU/ISO/NSO), number of shares, grant price, vesting schedule, grant date, company, estimated value
- **Consumers**: Equity tracker, total compensation calculator, vesting reminder service, financial planning tool

#### CompensationPackageNegotiated
- **Description**: User has completed a compensation negotiation
- **Triggered When**: User records details of a compensation negotiation
- **Key Data**: Negotiation ID, initial offer, final offer, negotiation outcome, components negotiated, company, job title, negotiation date
- **Consumers**: Negotiation strategy analyzer, offer comparison tool, career advice engine, historical tracker

### BenefitsEvents

#### BenefitsPackageRecorded
- **Description**: User has documented their benefits package details
- **Triggered When**: User enters information about health, retirement, and other benefits
- **Key Data**: Benefits ID, health insurance details, retirement match percentage, PTO days, other benefits, monetary value estimate, company, effective date
- **Consumers**: Total compensation calculator, benefits comparison tool, job offer evaluator, financial planning

#### RetirementContributionUpdated
- **Description**: Retirement contribution rate or employer match has changed
- **Triggered When**: User updates 401k/retirement contribution information
- **Key Data**: Update ID, contribution percentage, employer match, contribution limit, effective date, plan type, vesting schedule
- **Consumers**: Retirement planning tool, tax calculator, total compensation tracker, financial projections

#### BenefitsEnrollmentCompleted
- **Description**: User has completed benefits enrollment for a period
- **Triggered When**: User finalizes their benefits selections
- **Key Data**: Enrollment ID, enrollment period, selected plans, coverage levels, dependents, total cost, employer contribution, effective date
- **Consumers**: Benefits cost tracker, tax estimator, health planning tool, dependent manager

### MarketRateEvents

#### MarketRateResearched
- **Description**: User has researched market rates for their role
- **Triggered When**: User enters market salary data from research
- **Key Data**: Research ID, job title, location, experience level, salary range (min/max/median), data source, research date, industry
- **Consumers**: Compensation comparison engine, negotiation preparation tool, career planning, market trend analyzer

#### CompensationBenchmarked
- **Description**: User's current compensation has been compared against market rates
- **Triggered When**: System or user performs a benchmark comparison
- **Key Data**: Benchmark ID, current compensation, market median, percentile ranking, gap amount, gap percentage, comparison date, data sources
- **Consumers**: Career decision support, negotiation advisor, job search trigger, market positioning tracker

#### MarketTrendIdentified
- **Description**: System has identified a relevant market compensation trend
- **Triggered When**: Analysis reveals significant market movement in user's field
- **Key Data**: Trend ID, job category, geographic region, trend direction, magnitude, timeframe, data confidence, affected roles
- **Consumers**: Notification service, career advice engine, negotiation timing advisor, market intelligence

### CareerProgressionEvents

#### JobChangeRecorded
- **Description**: User has changed jobs or positions
- **Triggered When**: User records a new position or company change
- **Key Data**: Change ID, previous company/title, new company/title, start date, compensation change, reason for change, career move type
- **Consumers**: Career history tracker, compensation progression analyzer, career pattern identifier, resume builder

#### PromotionReceived
- **Description**: User has been promoted to a new position
- **Triggered When**: User records a promotion event
- **Key Data**: Promotion ID, previous title, new title, promotion date, compensation increase, new responsibilities, company, promotion path
- **Consumers**: Career milestone tracker, compensation analyzer, skill development tracker, achievement system

#### CompensationReviewScheduled
- **Description**: A compensation review or performance review has been scheduled
- **Triggered When**: User sets a reminder for upcoming compensation discussion
- **Key Data**: Review ID, scheduled date, review type, current compensation, target increase, preparation status, company, manager
- **Consumers**: Reminder service, preparation tool, negotiation strategy advisor, documentation organizer

#### CounterOfferReceived
- **Description**: User has received a counter-offer from current or prospective employer
- **Triggered When**: User records a counter-offer during job negotiation or retention
- **Key Data**: Offer ID, offering company, offer amount, offer components, original offer, decision deadline, offer context, acceptance status
- **Consumers**: Offer comparison tool, decision support system, negotiation tracker, career advisor

### AnalyticsEvents

#### CompensationGrowthCalculated
- **Description**: System has calculated compensation growth over a period
- **Triggered When**: User requests or system generates growth analysis
- **Key Data**: Calculation ID, time period, starting compensation, ending compensation, total growth percentage, annualized growth, inflation-adjusted growth
- **Consumers**: Career dashboard, financial planning, market comparison, career success metrics

#### TotalCompensationReconciled
- **Description**: System has calculated total compensation including all components
- **Triggered When**: User requests or system performs comprehensive compensation calculation
- **Key Data**: Reconciliation ID, base salary, bonuses, equity value, benefits value, perks value, total compensation, calculation date, period
- **Consumers**: Offer comparison, market benchmarking, tax planning, career decision support
