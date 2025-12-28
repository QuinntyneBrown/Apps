# Domain Events - Home Improvement Project Manager

## Overview
This document defines the domain events tracked in the Home Improvement Project Manager application. These events capture significant business occurrences related to renovation projects, contractor management, budgeting, timeline tracking, permits, and project completion.

## Events

### ProjectEvents

#### ProjectCreated
- **Description**: New home improvement project has been initiated
- **Triggered When**: User defines renovation or improvement project
- **Key Data**: Project ID, project name, description, project type, scope, priority, estimated budget, target completion date, created by
- **Consumers**: Project portfolio, budget allocator, timeline planner, contractor matcher, permit checker

#### ProjectPhaseCompleted
- **Description**: Major milestone or phase of project has been finished
- **Triggered When**: Significant project stage reaches completion
- **Key Data**: Project ID, phase name, completion date, completed tasks, actual cost, timeline variance, quality assessment
- **Consumers**: Progress tracker, payment processor, next phase trigger, timeline updater, analytics

#### ProjectStatusChanged
- **Description**: Overall project status has been updated
- **Triggered When**: Project moves through lifecycle stages
- **Key Data**: Project ID, previous status, new status (planning/active/on-hold/completed/cancelled), change date, change reason
- **Consumers**: Dashboard updater, notification service, resource allocator, priority adjuster

#### ProjectCompleted
- **Description**: Home improvement project has been finished
- **Triggered When**: All work completed and final inspection passed
- **Key Data**: Project ID, completion date, final cost, budget variance, timeline variance, quality rating, lessons learned, photos
- **Consumers**: Archive service, financial closer, warranty tracker, contractor reviewer, home value updater

#### ProjectCancelled
- **Description**: Planned project has been abandoned
- **Triggered When**: User decides not to proceed with project
- **Key Data**: Project ID, cancellation date, cancellation reason, sunk costs, deposits recoverable, vendor notifications needed
- **Consumers**: Budget releaser, contractor notifier, permit canceller, resource liberator

### BudgetEvents

#### ProjectBudgetSet
- **Description**: Financial budget for project has been established
- **Triggered When**: User defines total budget and category allocations
- **Key Data**: Project ID, total budget, category breakdown, contingency amount, funding source, budget basis
- **Consumers**: Budget tracker, expense validator, overspend alerter, financial planner

#### ExpenseRecorded
- **Description**: Project-related cost has been logged
- **Triggered When**: User records purchase, payment, or invoice
- **Key Data**: Expense ID, project ID, amount, category, vendor, date, receipt, payment method, budget category
- **Consumers**: Budget tracker, spending analyzer, tax documentation, vendor payment tracker, remaining budget calculator

#### BudgetThresholdExceeded
- **Description**: Project spending has surpassed warning threshold
- **Triggered When**: Cumulative expenses reach percentage of budget (75%, 90%, 100%)
- **Key Data**: Project ID, threshold level, budgeted amount, actual spent, overage amount, remaining categories
- **Consumers**: Alert service, budget review trigger, scope adjuster, funding source checker

#### ChangeOrderApproved
- **Description**: Scope change with cost impact has been authorized
- **Triggered When**: User approves additional work beyond original scope
- **Key Data**: Change order ID, project ID, description, cost increase, timeline impact, approval date, approved by
- **Consumers**: Budget updater, timeline adjuster, contractor notifier, scope documenter

### ContractorEvents

#### ContractorQuoteRequested
- **Description**: Request for quote has been sent to contractor
- **Triggered When**: User solicits bid for project work
- **Key Data**: Quote request ID, project ID, contractor ID, scope description, requested quote date, response deadline
- **Consumers**: Quote tracker, response monitor, comparison facilitator, contractor communication

#### QuoteReceived
- **Description**: Contractor has submitted project bid
- **Triggered When**: Contractor provides pricing and scope proposal
- **Key Data**: Quote ID, contractor ID, project ID, quoted amount, scope details, timeline estimate, quote date, validity period
- **Consumers**: Quote comparator, decision support, budget validator, contractor evaluator

#### ContractorHired
- **Description**: Contractor has been selected and engaged for project
- **Triggered When**: User accepts quote and executes contract
- **Key Data**: Contract ID, project ID, contractor ID, contracted amount, scope, start date, completion date, payment terms, contract document
- **Consumers**: Project scheduler, payment scheduler, contractor tracker, insurance verifier, permit applicant

#### ContractorPaymentMade
- **Description**: Payment has been issued to contractor
- **Triggered When**: Milestone payment or final payment processed
- **Key Data**: Payment ID, contract ID, amount, payment date, payment type, work covered, remaining balance, retention held
- **Consumers**: Budget tracker, payment history, contractor relationship, project cost accumulator, tax records

#### ContractorPerformanceRated
- **Description**: Contractor work quality has been evaluated
- **Triggered When**: Project phase or entire project completed
- **Key Data**: Contractor ID, project ID, rating score, quality assessment, timeliness, communication, would hire again, review comments
- **Consumers**: Contractor directory, future hiring decisions, contractor relationship, rating aggregator

### PermitEvents

#### PermitApplicationSubmitted
- **Description**: Building permit application has been filed
- **Triggered When**: User or contractor submits permit paperwork to authorities
- **Key Data**: Permit ID, project ID, permit type, jurisdiction, application date, submitted by, application fee, required inspections
- **Consumers**: Permit tracker, project timeline, compliance monitor, inspection scheduler

#### PermitApproved
- **Description**: Required building permit has been granted
- **Triggered When**: Authority approves permit application
- **Key Data**: Permit ID, approval date, permit number, approved scope, conditions, expiration date, inspection requirements
- **Consumers**: Project unblock, work authorization, inspection scheduler, timeline updater, compliance tracker

#### PermitDenied
- **Description**: Building permit application has been rejected
- **Triggered When**: Authority declines to issue permit
- **Key Data**: Permit ID, denial date, denial reasons, required changes, resubmission possible, appeal options
- **Consumers**: Project delay notification, scope reviser, reapplication workflow, timeline adjuster

#### InspectionScheduled
- **Description**: Required inspection has been booked with authorities
- **Triggered When**: User schedules inspection for permit compliance
- **Key Data**: Inspection ID, permit ID, inspection type, scheduled date, inspector, preparation requirements, work to be inspected
- **Consumers**: Calendar integration, contractor notification, preparation checklist, work pause if needed

#### InspectionPassed
- **Description**: Code inspection has been successfully completed
- **Triggered When**: Inspector approves work quality and code compliance
- **Key Data**: Inspection ID, pass date, inspector notes, compliance confirmed, next inspection required, final inspection flag
- **Consumers**: Next phase authorizer, permit closer, project advancer, compliance recorder

#### InspectionFailed
- **Description**: Code inspection identified deficiencies
- **Triggered When**: Inspector finds code violations or quality issues
- **Key Data**: Inspection ID, fail date, deficiencies listed, correction requirements, re-inspection needed, timeline impact
- **Consumers**: Contractor notification, correction task creator, timeline adjuster, re-inspection scheduler, issue tracker

### ScheduleEvents

#### ProjectTimelineCreated
- **Description**: Detailed schedule for project has been established
- **Triggered When**: User or contractor creates project timeline
- **Key Data**: Project ID, start date, end date, phases, milestones, dependencies, critical path, resource assignments
- **Consumers**: Gantt chart generator, calendar integration, deadline tracker, dependency manager

#### MilestoneReached
- **Description**: Significant project checkpoint has been achieved
- **Triggered When**: Planned milestone completion date arrived and work complete
- **Key Data**: Project ID, milestone name, planned date, actual date, variance, dependencies cleared, payment trigger
- **Consumers**: Progress updater, payment processor, next phase enabler, stakeholder notifier

#### DelayReported
- **Description**: Project timeline has experienced setback
- **Triggered When**: Work falls behind schedule
- **Key Data**: Project ID, delay reason, days delayed, impact on completion date, mitigation plan, responsible party
- **Consumers**: Timeline adjuster, stakeholder notification, resource reallocation, recovery planner

#### WeatherDelayLogged
- **Description**: Work stoppage due to weather conditions
- **Triggered When**: Outdoor work cannot proceed due to weather
- **Key Data**: Project ID, delay date, weather condition, duration, work affected, rescheduled date
- **Consumers**: Schedule adjuster, contractor coordination, timeline recalculation, force majeure tracker

### MaterialEvents

#### MaterialsOrdered
- **Description**: Construction materials have been purchased
- **Triggered When**: User or contractor orders supplies for project
- **Key Data**: Order ID, project ID, materials list, supplier, order date, expected delivery, total cost, ordered by
- **Consumers**: Budget tracker, delivery scheduler, inventory tracker, work readiness checker

#### MaterialsDelivered
- **Description**: Ordered materials have arrived on site
- **Triggered When**: Delivery received and confirmed
- **Key Data**: Order ID, delivery date, items received, condition check, discrepancies, storage location, ready for use
- **Consumers**: Inventory updater, work enabler, contractor notification, payment trigger

#### MaterialShortageDetected
- **Description**: Insufficient materials identified to complete work
- **Triggered When**: Contractor or user identifies material deficit
- **Key Data**: Project ID, material needed, quantity short, impact on schedule, reorder required, cost impact
- **Consumers**: Purchasing workflow, schedule adjuster, budget impactor, delay notifier

### QualityEvents

#### WorkQualityInspected
- **Description**: Completed work has been evaluated for quality
- **Triggered When**: User or inspector reviews workmanship
- **Key Data**: Project ID, inspection date, work area, quality rating, issues found, contractor, photos, acceptance status
- **Consumers**: Quality tracker, contractor performance, payment authorization, punch list generator

#### PunchListCreated
- **Description**: List of final corrections has been compiled
- **Triggered When**: Final walkthrough identifies remaining items
- **Key Data**: Punch list ID, project ID, items listed, priority levels, responsible contractor, target completion date
- **Consumers**: Completion blocker, contractor task list, final payment holdback, project closer

#### PunchListItemCompleted
- **Description**: Correction item has been addressed
- **Triggered When**: Contractor fixes punch list issue
- **Key Data**: Item ID, completion date, verification status, before/after photos, remaining items count
- **Consumers**: Punch list tracker, project completion checker, final payment releaser
