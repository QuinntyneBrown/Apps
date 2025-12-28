# Domain Events - Freelance Project Manager

## Overview
This document defines the domain events tracked in the Freelance Project Manager application. These events capture significant business occurrences related to client management, project execution, time tracking, invoicing, payment collection, and freelance business operations.

## Events

### ClientEvents

#### ClientOnboarded
- **Description**: New client has been added to freelance business
- **Triggered When**: User adds client to system
- **Key Data**: Client ID, client name, company, contact info, industry, discovery source, onboarding date, client type, payment terms, contract signed
- **Consumers**: Client directory, project associator, invoice settings, relationship tracker, communication hub

#### ClientContractSigned
- **Description**: Service agreement with client has been executed
- **Triggered When**: Contract or agreement signed by both parties
- **Key Data**: Contract ID, client ID, contract date, contract terms, scope of work, rate, payment terms, contract duration, renewal terms
- **Consumers**: Contract vault, payment terms setter, scope reference, legal protection, project authorizer

#### ClientRelationshipUpdated
- **Description**: Client relationship status has changed
- **Triggered When**: Client becomes active, inactive, or relationship quality changes
- **Key Data**: Client ID, relationship status, update date, status reason, last project date, future project likelihood
- **Consumers**: Client status tracker, pipeline forecaster, relationship manager, reactivation identifier

#### ClientReferralReceived
- **Description**: Existing client has referred new client
- **Triggered When**: New client arrives via existing client referral
- **Key Data**: Referral ID, referring client ID, new client ID, referral date, project value, referral incentive, gratitude expressed
- **Consumers**: Referral tracker, relationship value recorder, incentive processor, gratitude reminder

### ProjectEvents

#### ProjectProposed
- **Description**: Project proposal has been submitted to client
- **Triggered When**: User sends project proposal or quote
- **Key Data**: Proposal ID, client ID, proposal date, scope, deliverables, timeline, estimated hours, quoted price, proposal status
- **Consumers**: Proposal tracker, sales pipeline, follow-up scheduler, conversion monitor, proposal library

#### ProjectAccepted
- **Description**: Client has approved project proposal
- **Triggered When**: Client accepts proposal and authorizes work
- **Key Data**: Project ID, proposal ID, acceptance date, final scope, agreed price, start date, deadline, payment schedule, deposit required
- **Consumers**: Project activator, invoice generator, calendar blocker, revenue forecaster, workload planner

#### ProjectStarted
- **Description**: Active work on project has commenced
- **Triggered When**: First work session logged on project
- **Key Data**: Project ID, start date, initial tasks, milestone plan, client kickoff completed, deliverables timeline
- **Consumers**: Project tracker, time tracking enabler, progress monitor, deadline tracker, client communication

#### ProjectMilestoneCompleted
- **Description**: Significant project checkpoint has been reached
- **Triggered When**: Major deliverable or phase completed
- **Key Data**: Project ID, milestone name, completion date, deliverable submitted, client approval status, milestone payment trigger
- **Consumers**: Progress tracker, invoice trigger, client notification, payment requester, timeline updater

#### ProjectDelivered
- **Description**: Final project deliverables have been submitted to client
- **Triggered When**: All project work completed and delivered
- **Key Data**: Project ID, delivery date, deliverables submitted, client acceptance, final revisions needed, delivery method
- **Consumers**: Delivery tracker, acceptance monitor, final invoice trigger, project closer, portfolio candidate

#### ProjectCompleted
- **Description**: Project has been fully completed and accepted
- **Triggered When**: Client accepts final deliverables
- **Key Data**: Project ID, completion date, total hours, final cost, client satisfaction, testimonial received, portfolio added
- **Consumers**: Project archiver, invoice finalizer, payment collector, client satisfaction tracker, portfolio builder

#### ProjectCancelled
- **Description**: Project has been terminated before completion
- **Triggered When**: Client or freelancer cancels project
- **Key Data**: Project ID, cancellation date, cancellation reason, completion percentage, work delivered, final payment, kill fee
- **Consumers**: Project closer, partial invoice generator, calendar liberator, cancellation analyzer, relationship manager

### TimeTrackingEvents

#### TimeEntryLogged
- **Description**: Work session on project has been recorded
- **Triggered When**: User logs time spent on project work
- **Key Data**: Entry ID, project ID, date, duration, task description, billable flag, hourly rate, total value, entry method
- **Consumers**: Time tracker, invoice line item generator, project hour accumulator, productivity analyzer, billing calculator

#### TimeEntryModified
- **Description**: Previously logged time entry has been edited
- **Triggered When**: User corrects or updates time entry
- **Key Data**: Entry ID, previous duration, new duration, modification date, modification reason, billing impact
- **Consumers**: Time tracker updater, invoice adjuster, audit log, accuracy maintainer

#### BillableHoursAccumulated
- **Description**: Billable time threshold has been reached
- **Triggered When**: Project billable hours reach invoice trigger point
- **Key Data**: Project ID, accumulated hours, accumulated value, billing trigger threshold, invoice ready flag
- **Consumers**: Invoice generator, billing alert, payment collection trigger, cash flow planner

#### NonBillableTimeTracked
- **Description**: Non-billable work has been logged
- **Triggered When**: User tracks administrative or non-client time
- **Key Data**: Entry ID, date, duration, activity type, reason non-billable, opportunity cost
- **Consumers**: Productivity analyzer, overhead tracker, profitability calculator, efficiency optimizer

### InvoiceEvents

#### InvoiceGenerated
- **Description**: Client invoice has been created
- **Triggered When**: User creates invoice for completed work
- **Key Data**: Invoice ID, client ID, project ID, invoice date, line items, subtotal, tax, total, payment terms, due date, invoice number
- **Consumers**: Invoice sender, payment tracker, accounts receivable, revenue recognizer, cash flow forecaster

#### InvoiceSent
- **Description**: Invoice has been delivered to client
- **Triggered When**: User sends invoice to client
- **Key Data**: Invoice ID, send date, send method, recipient, payment instructions, payment link, reminder schedule
- **Consumers**: Payment tracker, reminder scheduler, client communication, accounts receivable, delivery confirmation

#### InvoiceViewed
- **Description**: Client has opened invoice
- **Triggered When**: Invoice tracking detects client opened invoice
- **Key Data**: Invoice ID, view date, viewer, view count, time to view, payment likelihood indicator
- **Consumers**: Payment predictor, follow-up timing optimizer, engagement tracker, payment anticipator

#### PaymentReceived
- **Description**: Client payment has been collected
- **Triggered When**: Invoice payment received and confirmed
- **Key Data**: Payment ID, invoice ID, payment date, amount paid, payment method, transaction fee, full/partial payment, remaining balance
- **Consumers**: Accounts receivable, cash flow tracker, revenue recorder, client payment history, partial payment handler

#### PaymentOverdue
- **Description**: Invoice payment due date has passed
- **Triggered When**: Invoice due date arrives without payment
- **Key Data**: Invoice ID, due date, days overdue, amount owed, reminder sent, escalation level, client payment history
- **Consumers**: Overdue alert, reminder escalator, collection workflow, client relationship manager, cash flow impact

#### PaymentReminderSent
- **Description**: Payment reminder notification has been delivered
- **Triggered When**: Automated or manual payment reminder sent
- **Key Data**: Reminder ID, invoice ID, reminder date, reminder type, reminder message, escalation level, response tracking
- **Consumers**: Collection workflow, payment prompter, reminder scheduler, client communication, accounts receivable

#### InvoiceDisputed
- **Description**: Client has contested invoice charges
- **Triggered When**: Client raises issue with invoice
- **Key Data**: Invoice ID, dispute date, dispute reason, disputed items, disputed amount, resolution status, client communication
- **Consumers**: Dispute resolver, client relationship manager, invoice adjuster, payment hold, resolution workflow

### ExpenseEvents

#### BusinessExpenseLogged
- **Description**: Freelance business expense has been recorded
- **Triggered When**: User logs business-related expense
- **Key Data**: Expense ID, date, amount, category, description, vendor, receipt, tax deductible, project ID if applicable
- **Consumers**: Expense tracker, tax preparation, profitability calculator, reimbursement processor, financial reporting

#### ClientExpenseBilled
- **Description**: Client-reimbursable expense has been invoiced
- **Triggered When**: Project expense added to client invoice
- **Key Data**: Expense ID, invoice ID, expense amount, markup applied, reimbursement status, receipt provided
- **Consumers**: Invoice line item, reimbursement tracker, profitability monitor, client billing

### SchedulingEvents

#### AvailabilityBlocked
- **Description**: Calendar time has been reserved for project work
- **Triggered When**: User blocks time for client project
- **Key Data**: Block ID, project ID, start time, end time, block type, recurring flag, calendar synced
- **Consumers**: Calendar integration, workload planner, capacity tracker, scheduling optimizer

#### CapacityReached
- **Description**: Freelancer workload has hit maximum threshold
- **Triggered When**: Scheduled projects reach capacity limit
- **Key Data**: capacity date, projects scheduled, hours committed, capacity percentage, new project acceptance impact
- **Consumers**: Project acceptance controller, pricing adjuster, deadline negotiator, capacity alert

#### DeadlineApproaching
- **Description**: Project deadline is near
- **Triggered When**: Project due date within warning window
- **Key Data**: Project ID, deadline date, days remaining, completion percentage, on-track status, urgency level
- **Consumers**: Deadline alert, work prioritizer, client update prompter, time allocation adjuster

### FinancialEvents

#### RevenueGoalSet
- **Description**: Income target for period has been established
- **Triggered When**: User sets monthly/quarterly/annual revenue goal
- **Key Data**: Goal ID, period, target revenue, current revenue, goal basis, required project count, required billable hours
- **Consumers**: Goal tracker, revenue monitor, project pipeline evaluator, pricing strategy

#### MonthlyRevenueCalculated
- **Description**: Total revenue for month has been computed
- **Triggered When**: Month ends and revenue tallied
- **Key Data**: Month, total revenue, revenue by client, revenue by project type, expenses, profit, goal variance
- **Consumers**: Financial dashboard, tax estimator, goal progress, profitability analyzer, revenue trends

#### ProfitMarginAnalyzed
- **Description**: Project or business profitability has been evaluated
- **Triggered When**: Project completes or period ends
- **Key Data**: Analysis period, revenue, expenses, profit, profit margin percentage, comparison to target, profitability drivers
- **Consumers**: Pricing strategy, client profitability ranker, service offering optimizer, financial health

### PortfolioEvents

#### PortfolioPieceAdded
- **Description**: Completed project has been added to portfolio
- **Triggered When**: User adds project to showcase portfolio
- **Key Data**: Portfolio ID, project ID, client name (anonymized if needed), description, deliverables, results achieved, testimonial, images
- **Consumers**: Portfolio showcase, sales tool, credibility builder, case study library

#### TestimonialReceived
- **Description**: Client feedback or testimonial has been obtained
- **Triggered When**: Client provides positive review or testimonial
- **Key Data**: Testimonial ID, client ID, project ID, testimonial text, rating, permission to publish, testimonial date
- **Consumers**: Testimonial library, portfolio builder, marketing materials, credibility enhancer, sales enabler

### ProposalEvents

#### ProposalTemplateCreated
- **Description**: Reusable proposal framework has been saved
- **Triggered When**: User saves proposal structure for future use
- **Key Data**: Template ID, template name, service type, sections included, pricing structure, terms, creation date
- **Consumers**: Template library, proposal generator, efficiency tool, consistency maintainer

#### ProposalFollowUpScheduled
- **Description**: Reminder to follow up on proposal has been set
- **Triggered When**: Proposal sent and follow-up scheduled
- **Key Data**: Proposal ID, follow-up date, follow-up method, days since proposal, conversion likelihood
- **Consumers**: Follow-up reminder, sales pipeline, conversion tracker, proposal status updater

#### ProposalRejected
- **Description**: Client has declined project proposal
- **Triggered When**: Client does not accept proposal
- **Key Data**: Proposal ID, rejection date, rejection reason, feedback received, future opportunity, lessons learned
- **Consumers**: Rejection tracker, proposal refinement, pricing strategy, win rate calculator, learning system
