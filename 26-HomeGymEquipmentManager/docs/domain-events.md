# Domain Events - Home Gym Equipment Manager

## Overview
This document defines the domain events tracked in the Home Gym Equipment Manager application. These events capture significant business occurrences related to equipment inventory, maintenance schedules, equipment usage, and lifecycle management.

## Events

### EquipmentEvents

#### EquipmentAdded
- **Description**: A new piece of gym equipment has been added to the inventory
- **Triggered When**: User registers new equipment with purchase details
- **Key Data**: Equipment ID, type, brand, model, purchase date, purchase price, warranty information, serial number
- **Consumers**: Inventory service, warranty tracker, depreciation calculator, insurance documentation

#### EquipmentRetired
- **Description**: Equipment has been marked as retired or disposed of
- **Triggered When**: User removes equipment from active inventory
- **Key Data**: Equipment ID, retirement date, retirement reason, disposal method, years of service
- **Consumers**: Inventory service, analytics service, tax documentation service

#### EquipmentRelocated
- **Description**: Equipment has been moved to a different location within the home gym
- **Triggered When**: User updates equipment location or layout
- **Key Data**: Equipment ID, previous location, new location, relocation date
- **Consumers**: Layout manager, space optimization service

### MaintenanceEvents

#### MaintenanceScheduled
- **Description**: A maintenance task has been scheduled for equipment
- **Triggered When**: User creates a maintenance schedule or system auto-schedules based on usage
- **Key Data**: Equipment ID, maintenance type, scheduled date, maintenance checklist, assigned person
- **Consumers**: Notification service, calendar integration, task management

#### MaintenanceCompleted
- **Description**: Scheduled or ad-hoc maintenance has been completed
- **Triggered When**: User marks maintenance task as complete with details
- **Key Data**: Equipment ID, maintenance date, work performed, parts replaced, cost, technician notes, next service date
- **Consumers**: Maintenance history, budget tracker, analytics service, warranty validation

#### MaintenanceOverdue
- **Description**: Scheduled maintenance has not been completed by the due date
- **Triggered When**: System detects maintenance task past due date
- **Key Data**: Equipment ID, maintenance type, original due date, days overdue
- **Consumers**: Alert service, priority notification system

### UsageEvents

#### EquipmentUsageLogged
- **Description**: A workout session using specific equipment has been recorded
- **Triggered When**: User logs equipment usage after workout
- **Key Data**: Equipment ID, usage date, duration, workout type, intensity level, user ID
- **Consumers**: Usage analytics, maintenance prediction, equipment lifespan calculator

#### UsageThresholdReached
- **Description**: Equipment has reached a predefined usage milestone
- **Triggered When**: System detects equipment usage hours/count exceeds threshold
- **Key Data**: Equipment ID, threshold type, current usage count, threshold value, date reached
- **Consumers**: Maintenance scheduler, performance monitoring, replacement planning

### WarrantyEvents

#### WarrantyRegistered
- **Description**: Warranty information has been registered for equipment
- **Triggered When**: User enters warranty details for new or existing equipment
- **Key Data**: Equipment ID, warranty provider, coverage type, start date, end date, terms, claim process
- **Consumers**: Warranty tracking service, maintenance scheduler, purchase planning

#### WarrantyExpiring
- **Description**: Equipment warranty is approaching expiration
- **Triggered When**: System detects warranty expiration within notification window (30/60/90 days)
- **Key Data**: Equipment ID, warranty end date, days until expiration, extended warranty options
- **Consumers**: Notification service, extended warranty recommendation engine

#### WarrantyClaimFiled
- **Description**: A warranty claim has been submitted for equipment
- **Triggered When**: User initiates warranty claim process
- **Key Data**: Equipment ID, claim date, claim reason, claim number, service provider, expected resolution date
- **Consumers**: Claim tracking service, equipment availability manager, cost tracking

### ConditionEvents

#### ConditionAssessmentRecorded
- **Description**: Equipment condition has been evaluated and documented
- **Triggered When**: User performs periodic condition assessment
- **Key Data**: Equipment ID, assessment date, condition rating, wear indicators, photos, assessor notes
- **Consumers**: Maintenance planning, replacement forecasting, resale value estimation

#### SafetyIssueReported
- **Description**: A safety concern has been identified with equipment
- **Triggered When**: User reports potential safety hazard or equipment malfunction
- **Key Data**: Equipment ID, issue description, severity level, report date, photos, immediate action taken
- **Consumers**: Alert service, maintenance priority queue, usage restriction service, incident tracking

### FinancialEvents

#### EquipmentDepreciationCalculated
- **Description**: Equipment value has been depreciated based on usage and time
- **Triggered When**: System performs periodic depreciation calculation (monthly/quarterly)
- **Key Data**: Equipment ID, original value, current value, depreciation amount, calculation method, calculation date
- **Consumers**: Asset tracking, insurance valuation, tax reporting, net worth calculator

#### EquipmentResaleValueUpdated
- **Description**: Estimated resale value has been updated based on market conditions
- **Triggered When**: System updates resale values or user manually adjusts estimate
- **Key Data**: Equipment ID, previous estimate, new estimate, market data source, update date
- **Consumers**: Asset management, upgrade planning, financial planning
