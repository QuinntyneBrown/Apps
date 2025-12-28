# Domain Events - Real Estate Investment Analyzer

## Overview
This document defines the domain events tracked in the Real Estate Investment Analyzer application. These events capture significant business occurrences related to property investment analysis, cash flow projections, ROI calculations, and rental property management evaluation.

## Events

### PropertyEvents

#### PropertyAdded
- **Description**: A property has been added for investment analysis
- **Triggered When**: User creates new property record for evaluation or tracking
- **Key Data**: Property ID, address, property type, purchase price, acquisition date, user ID, timestamp
- **Consumers**: Property portfolio manager, analysis calculator, dashboard UI

#### PropertyPurchased
- **Description**: Property acquisition has been completed
- **Triggered When**: User marks property as purchased/closed
- **Key Data**: Property ID, purchase price, closing date, down payment, loan amount, closing costs, user ID
- **Consumers**: Portfolio tracker, cash flow calculator, mortgage scheduler, tax basis recorder

#### PropertySold
- **Description**: Property has been disposed of
- **Triggered When**: User records property sale
- **Key Data**: Property ID, sale price, sale date, sale costs, total profit/loss, holding period, user ID
- **Consumers**: ROI calculator, capital gains calculator, portfolio updater, performance analyzer

#### PropertyValueUpdated
- **Description**: Estimated property value has been modified
- **Triggered When**: User updates market value estimate or appraisal received
- **Key Data**: Property ID, previous value, new value, value change, update source, update date, user ID
- **Consumers**: Equity calculator, portfolio value updater, ROI recalculator

### AnalysisEvents

#### InvestmentAnalysisPerformed
- **Description**: Comprehensive property analysis has been calculated
- **Triggered When**: User requests complete investment analysis for property
- **Key Data**: Analysis ID, property ID, NOI, cap rate, cash-on-cash return, IRR, assumptions used, timestamp, user ID
- **Consumers**: Report generator, comparison ranker, decision support system

#### CashFlowProjected
- **Description**: Multi-year cash flow projection has been generated
- **Triggered When**: User creates or updates cash flow forecast
- **Key Data**: Property ID, projection years, annual cash flows, cumulative cash flow, assumptions, timestamp, user ID
- **Consumers**: Cash flow visualizer, ROI calculator, investment comparator

#### ROICalculated
- **Description**: Return on investment metrics computed
- **Triggered When**: Property analysis runs or key inputs change
- **Key Data**: Property ID, ROI percentage, cash-on-cash return, total return, annualized return, time period, user ID
- **Consumers**: Performance dashboard, investment ranker, goal tracker

#### ComparableAnalysisCompleted
- **Description**: Comparable properties analysis finished
- **Triggered When**: User analyzes similar properties in area for valuation
- **Key Data**: Property ID, comparable count, average price per sqft, value range, analysis date, user ID
- **Consumers**: Valuation adjuster, market analyzer, pricing recommender

### IncomeEvents

#### RentalIncomeProjected
- **Description**: Expected rental income has been estimated
- **Triggered When**: User inputs or updates rent assumptions
- **Key Data**: Property ID, monthly rent, annual gross income, vacancy rate, effective gross income, user ID, timestamp
- **Consumers**: Cash flow calculator, NOI calculator, analysis engine

#### RentalIncomeReceived
- **Description**: Actual rental payment collected from tenant
- **Triggered When**: User logs rent payment received
- **Key Data**: Income ID, property ID, amount, payment date, tenant ID, lease period, user ID
- **Consumers**: Cash flow tracker, actual vs. projected analyzer, income aggregator

#### VacancyOccurred
- **Description**: Property has become vacant
- **Triggered When**: Tenant moves out or property listed as unoccupied
- **Key Data**: Property ID, vacancy start date, previous tenant, expected vacancy duration, user ID
- **Consumers**: Cash flow adjuster, vacancy cost calculator, leasing status tracker

#### PropertyRented
- **Description**: Property has been leased to a tenant
- **Triggered When**: Lease agreement executed and tenant moves in
- **Key Data**: Property ID, tenant ID, monthly rent, lease start date, lease term, security deposit, user ID
- **Consumers**: Income projector, vacancy tracker, lease manager, cash flow updater

### ExpenseEvents

#### OperatingExpenseRecorded
- **Description**: Property operating expense has been logged
- **Triggered When**: User records property-related expense
- **Key Data**: Expense ID, property ID, expense category, amount, date, vendor, tax deductible, user ID
- **Consumers**: NOI calculator, cash flow tracker, tax deduction aggregator, expense analyzer

#### CapitalExpenditureLogged
- **Description**: Major property improvement or capital expense recorded
- **Triggered When**: User logs significant property upgrade or repair
- **Key Data**: Capex ID, property ID, expense type, amount, date, depreciation period, user ID
- **Consumers**: Depreciation scheduler, cost basis adjuster, cash flow tracker, ROI calculator

#### PropertyTaxAssessed
- **Description**: Annual property tax amount has been updated
- **Triggered When**: Tax assessment received or user updates tax amount
- **Key Data**: Property ID, tax year, assessed value, annual tax amount, previous tax, increase percentage, user ID
- **Consumers**: Operating expense updater, cash flow recalculator, NOI adjuster

#### MaintenanceReserveSet
- **Description**: Monthly maintenance reserve amount established
- **Triggered When**: User sets aside percentage for future repairs
- **Key Data**: Property ID, monthly reserve amount, annual reserve, reserve percentage, user ID, timestamp
- **Consumers**: Cash flow calculator, expense projector, reserve fund tracker

### FinancingEvents

#### MortgageObtained
- **Description**: Financing secured for property purchase
- **Triggered When**: User records mortgage details
- **Key Data**: Mortgage ID, property ID, loan amount, interest rate, loan term, monthly payment, lender, user ID
- **Consumers**: Cash flow calculator, debt service tracker, amortization scheduler, interest deduction calculator

#### MortgagePaymentMade
- **Description**: Monthly mortgage payment recorded
- **Triggered When**: User logs mortgage payment
- **Key Data**: Payment ID, mortgage ID, property ID, payment amount, principal portion, interest portion, payment date, user ID
- **Consumers**: Cash flow tracker, equity calculator, interest deduction aggregator

#### RefinanceCompleted
- **Description**: Property mortgage has been refinanced
- **Triggered When**: User updates mortgage with new refinanced terms
- **Key Data**: Property ID, old mortgage terms, new mortgage terms, closing costs, cash-out amount, refinance date, user ID
- **Consumers**: Cash flow recalculator, debt service updater, ROI analyzer

### MetricsEvents

#### CapRateCalculated
- **Description**: Capitalization rate computed for property
- **Triggered When**: NOI or property value changes trigger recalculation
- **Key Data**: Property ID, NOI, property value, cap rate percentage, calculation date, market cap rate comparison
- **Consumers**: Valuation analyzer, market comparator, investment attractiveness scorer

#### DebtServiceCoverageCalculated
- **Description**: DSCR metric computed for property
- **Triggered When**: NOI or debt service changes
- **Key Data**: Property ID, NOI, annual debt service, DSCR ratio, calculation date, user ID
- **Consumers**: Lending qualification checker, risk analyzer, cash flow health monitor

#### GrossRentMultiplierCalculated
- **Description**: GRM metric calculated for quick valuation
- **Triggered When**: Property analysis runs or rental income updated
- **Key Data**: Property ID, purchase price, gross annual rent, GRM value, market GRM comparison, timestamp
- **Consumers**: Quick valuation tool, market comparator, investment screener

### MarketEvents

#### MarketAppreciationApplied
- **Description**: Estimated market appreciation applied to property value
- **Triggered When**: Annual appreciation calculation runs
- **Key Data**: Property ID, appreciation rate, previous value, new value, appreciation year, user ID
- **Consumers**: Future value projector, equity calculator, ROI updater

#### RentGrowthApplied
- **Description**: Rental income increased based on market growth
- **Triggered When**: Annual rent escalation applied
- **Key Data**: Property ID, previous rent, new rent, growth rate, effective date, user ID
- **Consumers**: Income projector, cash flow updater, future returns calculator

#### MarketConditionsUpdated
- **Description**: Local market conditions and trends updated
- **Triggered When**: User updates or system imports market data
- **Key Data**: Market area, vacancy rates, average rents, cap rates, appreciation trends, update date
- **Consumers**: Assumption validator, market comparator, deal analyzer

### ScenarioEvents

#### InvestmentScenarioCreated
- **Description**: Alternative analysis scenario has been created
- **Triggered When**: User models different assumptions or strategies
- **Key Data**: Scenario ID, property ID, scenario name, key assumptions, creation date, user ID
- **Consumers**: Scenario comparator, sensitivity analyzer, decision support

#### ScenarioCompared
- **Description**: Multiple scenarios analyzed side-by-side
- **Triggered When**: User requests comparison of different investment approaches
- **Key Data**: Scenario IDs, comparison metrics, best/worst outcomes, sensitivity summary, timestamp
- **Consumers**: Decision dashboard, recommendation engine, risk analyzer

### ReportEvents

#### InvestmentReportGenerated
- **Description**: Comprehensive property analysis report created
- **Triggered When**: User requests detailed investment report
- **Key Data**: Report ID, property ID, report type, key metrics, assumptions, generation timestamp, user ID
- **Consumers**: Report delivery, PDF generator, presentation builder
