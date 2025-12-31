# SideHustleIncomeTracker - System Requirements

## Executive Summary

SideHustleIncomeTracker is a comprehensive side business and freelance income management system designed to help users track multiple income streams, manage clients and invoices, monitor expenses, analyze profitability, and plan for taxes across all their side hustle activities.

## Business Goals

- Enable effective management of multiple simultaneous income streams
- Provide clear visibility into profitability of each side hustle
- Simplify tax preparation through organized income and expense tracking
- Reduce manual effort in client billing and payment tracking
- Maximize profitability through expense monitoring and optimization
- Facilitate quarterly tax planning and estimated payment scheduling

## System Purpose
- Track multiple side hustles and income-generating activities
- Manage client relationships, invoicing, and payment collection
- Record and categorize business income and expenses
- Calculate profit and loss for each income stream
- Monitor mileage and other tax-deductible expenses
- Generate tax estimates and organize deductible information
- Set and track revenue and profitability goals
- Provide analytics and insights on side hustle performance

## Core Features

### 1. Income Stream Management
- Add, edit, and manage multiple side hustles
- Track income stream status (active, closed, reactivated)
- Monitor start dates and expected revenue by stream
- Categorize income sources by business type
- View historical performance of closed income streams
- Track closure reasons and reactivation dates

### 2. Revenue Tracking
- Record incoming payments and revenue
- Track payment methods and dates
- Associate revenue with specific income streams and clients
- Schedule and track recurring income (subscriptions, retainers)
- Monitor overdue payments and outstanding invoices
- Flag and celebrate large or milestone payments
- Generate revenue reports by stream, client, or time period

### 3. Expense Management
- Log business expenses by category
- Attach receipts and supporting documentation
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs the specified function correctly
  - **AC3**: Feature handles error conditions gracefully
- Tag expenses as tax-deductible
- Track expenses by income stream
- Monitor category budgets and spending limits
- Record business mileage with IRS rate calculations
- Alert when expense categories exceed budgets

### 4. Client Management
- Create and manage client profiles
- Track client contact information and history
- Generate and send invoices to clients
- Monitor invoice status (sent, paid, overdue)
- Track total revenue by client
- Manage client lifecycle (active, inactive, lost)
- Analyze client retention and revenue patterns

### 5. Profitability Analysis
- Calculate profit and loss by income stream
- Generate P&L reports for any time period
- Track profit margins and trends
- Identify most and least profitable income streams
- Monitor revenue records and milestones
- Analyze profitability thresholds and achievements
- Compare performance across multiple side hustles

### 6. Tax Planning
- Estimate quarterly tax obligations
- Track tax-deductible expenses by category
- Calculate mileage deductions using IRS rates
- Schedule estimated tax payments
- Identify tax deduction opportunities
- Generate tax summaries for filing preparation
- Alert for quarterly tax deadlines

### 7. Goal Tracking
- Set income goals by stream or overall
- Define monthly, quarterly, or annual targets
- Track progress toward revenue goals
- Celebrate goal achievements and milestones
- Analyze goal attainment rates
- Adjust goals based on performance

## Domain Events

### Income Stream Events
- **IncomeStreamCreated**: Triggered when a new side hustle is registered
- **IncomeStreamClosed**: Triggered when a side hustle is discontinued
- **IncomeStreamReactivated**: Triggered when a closed stream is restarted

### Revenue Events
- **IncomeReceived**: Triggered when payment is recorded
- **RecurringIncomeScheduled**: Triggered when recurring revenue is set up
- **PaymentOverdue**: Triggered when invoice payment is late
- **LargePaymentReceived**: Triggered when unusually large payment received

### Expense Events
- **BusinessExpenseRecorded**: Triggered when expense is logged
- **MileageLogged**: Triggered when business mileage is recorded
- **ExpenseCategoryExceeded**: Triggered when category spending exceeds budget

### Client Events
- **ClientAdded**: Triggered when new client is created
- **ClientInvoiced**: Triggered when invoice is sent to client
- **ClientPaid**: Triggered when client completes payment
- **ClientLost**: Triggered when client relationship ends

### Profitability Events
- **ProfitLossCalculated**: Triggered when P&L statement is generated
- **ProfitabilityThresholdReached**: Triggered when milestone achieved
- **MonthlyRevenueRecordSet**: Triggered when monthly revenue record set

### Tax Events
- **QuarterlyTaxEstimated**: Triggered when quarterly tax calculated
- **TaxPaymentScheduled**: Triggered when estimated tax payment scheduled
- **DeductionOpportunityIdentified**: Triggered when deduction identified

### Goal Events
- **IncomeGoalSet**: Triggered when revenue target is established
- **IncomeGoalAchieved**: Triggered when goal is met or exceeded

## Technical Architecture

### Backend
- .NET 8.0 Web API
- SQL Server database
- Domain-driven design with domain events
- CQRS pattern for command/query separation
- Event sourcing for audit trail
- Background jobs for scheduled calculations

### Frontend
- Modern SPA (Single Page Application)
- Responsive design for mobile and desktop
- Real-time notifications for payments and milestones
- Interactive dashboards and charts
- Intuitive invoice and expense entry
- Receipt upload and management

### Integration Points
- Payment processors for income tracking
- Notification services (email, SMS, push)
- Cloud storage for receipt and document management
- Tax software integration for export
- Accounting software integration (QuickBooks, etc.)

## User Roles
- **Freelancer/Solo Entrepreneur**: Full access to all features
- **Accountant/Bookkeeper**: Read access and tax reporting
- **Administrator**: System configuration and user management


## Multi-Tenancy Support

### Tenant Isolation
- **FR-MT-1**: Support for multi-tenant architecture with complete data isolation
  - **AC1**: Each tenant's data is completely isolated from other tenants
  - **AC2**: All queries are automatically filtered by TenantId
  - **AC3**: Cross-tenant data access is prevented at the database level
- **FR-MT-2**: TenantId property on all aggregate entities
  - **AC1**: Every aggregate root has a TenantId property
  - **AC2**: TenantId is set during entity creation
  - **AC3**: TenantId cannot be modified after creation
- **FR-MT-3**: Automatic tenant context resolution
  - **AC1**: TenantId is extracted from JWT claims or HTTP headers
  - **AC2**: Invalid or missing tenant context is handled gracefully
  - **AC3**: Tenant context is available throughout the request pipeline


## Security Requirements
- Secure authentication and authorization
- Encrypted storage of sensitive financial data
- Role-based access control
- Audit logging of all financial transactions
- Secure receipt and document storage
- Multi-factor authentication support

## Performance Requirements
- Support for 10,000+ concurrent users
- Real-time payment and expense tracking
- Dashboard load time under 2 seconds
- Receipt upload processing within 5 seconds
- Report generation within 10 seconds
- 99.9% uptime SLA

## Compliance Requirements
- GDPR compliance for user data
- SOC 2 Type II certification
- IRS record retention requirements (7 years)
- Data backup and disaster recovery
- Regular security audits

## Success Metrics
- User tracks average of 3+ income streams
- Invoice payment collection rate > 90%
- Tax preparation time reduced by 80%
- User satisfaction score > 4.5/5
- Monthly active users growth > 10%
- Receipt upload success rate > 95%

## Future Enhancements
- AI-powered expense categorization
- Automated invoice generation from time tracking
- Bank account integration for automatic transaction import
- Tax professional marketplace integration
- Mobile app for on-the-go expense tracking
- Client portal for invoice viewing and payment
- Integrations with payment platforms (Stripe, PayPal, Venmo)
- Automated mileage tracking via GPS
- Cash flow forecasting and projections
