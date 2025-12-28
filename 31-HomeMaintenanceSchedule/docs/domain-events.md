# Domain Events - Home Maintenance Schedule

## Overview
This document defines the domain events tracked in the Home Maintenance Schedule application. These events capture significant business occurrences related to scheduled maintenance tasks, service history, contractor management, property upkeep, and preventive maintenance.

## Events

### MaintenanceTaskEvents

#### TaskScheduled
- **Description**: A maintenance task has been added to the schedule
- **Triggered When**: User creates recurring or one-time maintenance task
- **Key Data**: Task ID, task type, description, frequency, next due date, priority, estimated cost, assigned person, location in home
- **Consumers**: Calendar integration, reminder scheduler, budget planner, task assignment system

#### TaskCompleted
- **Description**: Scheduled maintenance task has been finished
- **Triggered When**: User marks task as complete with details
- **Key Data**: Task ID, completion date, actual cost, time spent, service provider, work performed, photos, parts used, next scheduled date
- **Consumers**: Service history, budget tracker, performance analytics, warranty tracker, next occurrence scheduler

#### TaskPostponed
- **Description**: Scheduled maintenance has been delayed to later date
- **Triggered When**: User reschedules task to future date
- **Key Data**: Task ID, original due date, new due date, postponement reason, postponement count, impact assessment
- **Consumers**: Schedule updater, priority recalculator, reminder adjuster, analytics

#### TaskOverdue
- **Description**: Maintenance task has not been completed by due date
- **Triggered When**: Due date passes without completion
- **Key Data**: Task ID, due date, days overdue, priority level, potential consequences, urgency score
- **Consumers**: Alert system, priority notifications, risk assessor, escalation workflow

#### TaskCancelled
- **Description**: Scheduled maintenance task has been removed from schedule
- **Triggered When**: User determines task no longer necessary
- **Key Data**: Task ID, cancellation date, cancellation reason, task history, was recurring
- **Consumers**: Schedule updater, analytics, budget adjuster, audit log

### ServiceProviderEvents

#### ProviderAdded
- **Description**: New contractor or service provider has been added to directory
- **Triggered When**: User enters contractor information
- **Key Data**: Provider ID, business name, services offered, contact info, license number, insurance info, hourly rate, rating
- **Consumers**: Provider directory, task assignment system, quote request workflow

#### ProviderServiceCompleted
- **Description**: Service provider has completed contracted work
- **Triggered When**: User logs service provider work completion
- **Key Data**: Service ID, provider ID, task ID, completion date, services performed, cost, invoice number, payment status
- **Consumers**: Payment tracker, service history, provider performance, tax documentation

#### ProviderRated
- **Description**: User has reviewed and rated service provider performance
- **Triggered When**: User submits provider rating and review
- **Key Data**: Provider ID, rating score, review text, work quality, timeliness, communication, would recommend, service date
- **Consumers**: Provider ranking, recommendation engine, quality tracker, future selection

#### ProviderWarningIssued
- **Description**: Service provider has received negative feedback or issue flag
- **Triggered When**: User reports problem with provider performance
- **Key Data**: Provider ID, issue type, severity, description, resolution status, impact on future use
- **Consumers**: Provider vetting, alert system, contract review, replacement search

### SeasonalMaintenanceEvents

#### SeasonalChecklistGenerated
- **Description**: Seasonal maintenance tasks have been automatically created
- **Triggered When**: Season change triggers seasonal task generation
- **Key Data**: Season, checklist ID, tasks included, due dates, estimated total cost, climate-specific items
- **Consumers**: Task scheduler, reminder system, budget planner, preparation notifications

#### SeasonalPreparationCompleted
- **Description**: All seasonal preparation tasks have been finished
- **Triggered When**: User completes all tasks for season transition
- **Key Data**: Season, completion date, tasks completed, total cost, readiness score
- **Consumers**: Achievement system, analytics, compliance tracker, next season scheduler

### EmergencyMaintenanceEvents

#### EmergencyRepairReported
- **Description**: Urgent maintenance issue requiring immediate attention has been identified
- **Triggered When**: User reports emergency maintenance need (leak, outage, hazard)
- **Key Data**: Emergency ID, issue type, severity, location, safety risk, timestamp, immediate actions taken
- **Consumers**: Priority alert system, emergency contact notifier, service dispatch, incident tracker

#### EmergencyRepairResolved
- **Description**: Emergency maintenance issue has been addressed
- **Triggered When**: Emergency repair work is completed
- **Key Data**: Emergency ID, resolution date, service provider, work performed, cost, time to resolution, temporary vs permanent fix
- **Consumers**: Incident closer, cost tracker, follow-up scheduler, performance analytics, insurance claim preparation

### BudgetEvents

#### MaintenanceBudgetSet
- **Description**: Annual or monthly maintenance budget has been established
- **Triggered When**: User defines budget allocation for home maintenance
- **Key Data**: Budget ID, period, total amount, category allocations, reserve fund, adjustment rules
- **Consumers**: Budget tracker, spending monitor, forecaster, alert system

#### BudgetThresholdReached
- **Description**: Maintenance spending has reached significant portion of budget
- **Triggered When**: Cumulative costs reach threshold percentage (50%, 75%, 90%)
- **Key Data**: Budget period, threshold percentage, amount spent, amount remaining, major expenses, forecast to period end
- **Consumers**: Spending alert, task prioritization, budget review trigger, expense analyzer

#### UnexpectedExpenseLogged
- **Description**: Unplanned maintenance cost has been incurred
- **Triggered When**: User logs expense outside scheduled maintenance
- **Key Data**: Expense ID, amount, reason, urgency, budget impact, prevention possible flag
- **Consumers**: Budget adjuster, emergency fund tracker, preventive maintenance recommender, financial planning

### WarrantyEvents

#### WarrantyServiceScheduled
- **Description**: Maintenance covered by warranty has been scheduled
- **Triggered When**: User books warranty service for covered item
- **Key Data**: Warranty ID, item, service date, provider, coverage details, claim number, expected cost savings
- **Consumers**: Warranty tracker, appointment scheduler, cost savings monitor, service coordinator

#### WarrantyExpiring
- **Description**: Home system or appliance warranty is approaching expiration
- **Triggered When**: Warranty expiration within notification window (30/60/90 days)
- **Key Data**: Item, warranty type, expiration date, coverage details, renewal options, maintenance recommendations
- **Consumers**: Notification service, renewal recommender, inspection scheduler, decision support

### InspectionEvents

#### InspectionScheduled
- **Description**: Professional home inspection has been scheduled
- **Triggered When**: User books inspector for system assessment
- **Key Data**: Inspection ID, inspection type, inspector, scheduled date, systems covered, cost, reason for inspection
- **Consumers**: Calendar integration, preparation checklist, document organizer, follow-up planner

#### InspectionCompleted
- **Description**: Home inspection has been performed and report received
- **Triggered When**: Inspector completes assessment and provides findings
- **Key Data**: Inspection ID, completion date, inspector, findings summary, issues identified, recommendations, priority repairs, report document
- **Consumers**: Task generator, priority scheduler, budget planner, contractor selection, compliance tracker

#### InspectionIssueIdentified
- **Description**: Inspector has found maintenance issue requiring attention
- **Triggered When**: Inspection report includes problem or recommendation
- **Key Data**: Issue ID, inspection ID, severity, location, description, estimated repair cost, safety concern, timeline recommendation
- **Consumers**: Task creator, priority assessor, contractor matcher, budget impactor, safety alert

### PreventiveMaintenanceEvents

#### PreventiveMaintenanceOptimized
- **Description**: System has optimized maintenance schedule based on historical data
- **Triggered When**: Analytics identify opportunity to adjust maintenance frequency
- **Key Data**: System/item, current frequency, recommended frequency, cost impact, risk analysis, data basis
- **Consumers**: Schedule adjuster, cost optimizer, risk manager, user notification

#### SystemLifespanTracked
- **Description**: System has updated expected lifespan for home system or appliance
- **Triggered When**: Age, usage, or maintenance history triggers lifespan recalculation
- **Key Data**: System ID, installation date, current age, expected remaining life, replacement cost estimate, degradation factors
- **Consumers**: Replacement planner, budget forecaster, maintenance intensifier, capital planning

#### MaintenanceEffectivenessAnalyzed
- **Description**: System has evaluated maintenance strategy effectiveness
- **Triggered When**: Sufficient data exists to analyze maintenance outcomes
- **Key Data**: System/area, maintenance frequency, costs, issue frequency, system performance, optimization recommendations
- **Consumers**: Strategy adjuster, insight generator, cost-benefit analyzer, decision support
