# Domain Events - Appliance Warranty & Manual Organizer

## Overview
This document defines the domain events tracked in the Appliance Warranty & Manual Organizer application. These events capture significant business occurrences related to appliance documentation, warranty tracking, service history, manual organization, and maintenance reminders.

## Events

### ApplianceEvents

#### ApplianceRegistered
- **Description**: New appliance has been added to household inventory
- **Triggered When**: User catalogs appliance with details and documentation
- **Key Data**: Appliance ID, name, brand, model number, serial number, purchase date, purchase price, retailer, installation date, location
- **Consumers**: Appliance inventory, warranty tracker, maintenance scheduler, value calculator, search indexer

#### ApplianceRelocated
- **Description**: Appliance has been moved to different room or property
- **Triggered When**: User updates appliance location
- **Key Data**: Appliance ID, previous location, new location, relocation date, reason for move
- **Consumers**: Location tracker, room inventory, manual finder, service scheduler

#### ApplianceRetired
- **Description**: Appliance has been disposed of or replaced
- **Triggered When**: User removes appliance from active inventory
- **Key Data**: Appliance ID, retirement date, retirement reason, replacement appliance ID, disposal method, age at retirement, final service date
- **Consumers**: Inventory updater, warranty closer, documentation archiver, replacement tracker, lifecycle analytics

#### ApplianceReplacementPlanned
- **Description**: Future appliance replacement has been scheduled
- **Triggered When**: User identifies appliance nearing end of life
- **Key Data**: Appliance ID, planned replacement date, replacement reason, estimated cost, replacement model researched, urgency
- **Consumers**: Budget planner, shopping reminder, research assistant, timeline tracker

### WarrantyEvents

#### WarrantyDocumentUploaded
- **Description**: Warranty documentation has been digitized and stored
- **Triggered When**: User uploads warranty paperwork
- **Key Data**: Warranty ID, appliance ID, document file, upload date, coverage start date, coverage end date, warranty type, coverage details
- **Consumers**: Warranty library, expiration tracker, claim supporter, OCR processor, search indexer

#### WarrantyExpiring
- **Description**: Appliance warranty is approaching expiration
- **Triggered When**: System detects warranty expiration within notification window (30/60/90 days)
- **Key Data**: Appliance ID, warranty end date, days until expiration, coverage type, extended warranty available, renewal cost
- **Consumers**: Notification service, extended warranty advisor, decision timeline, purchase reminder

#### WarrantyExpired
- **Description**: Warranty coverage period has ended
- **Triggered When**: Warranty end date passes
- **Key Data**: Appliance ID, expiration date, coverage type, final coverage details, extended warranty declined/accepted
- **Consumers**: Coverage status updater, service cost awareness, extended warranty final offer, insurance consideration

#### ExtendedWarrantyPurchased
- **Description**: Additional warranty coverage has been bought
- **Triggered When**: User purchases extended protection plan
- **Key Data**: Warranty ID, appliance ID, provider, purchase date, coverage start, coverage end, cost, coverage details, claim process
- **Consumers**: Warranty tracker, budget recorder, coverage calculator, claim process documenter

#### WarrantyClaimInitiated
- **Description**: Warranty claim has been filed for appliance issue
- **Triggered When**: User starts warranty claim process
- **Key Data**: Claim ID, appliance ID, claim date, issue description, claim number, service provider, coverage verification, expected outcome
- **Consumers**: Claim tracker, service coordinator, documentation compiler, outcome monitor

#### WarrantyClaimResolved
- **Description**: Warranty claim has been completed
- **Triggered When**: Claim processed and outcome determined
- **Key Data**: Claim ID, resolution date, outcome, repair/replacement decision, cost covered, cost paid, resolution satisfaction
- **Consumers**: Service history, claim outcome tracker, warranty value calculator, satisfaction recorder

### ManualEvents

#### ManualUploaded
- **Description**: User manual or documentation has been digitized
- **Triggered When**: User uploads manual PDF or photos
- **Key Data**: Manual ID, appliance ID, document type, file format, upload date, page count, language, manual version
- **Consumers**: Manual library, search indexer, OCR processor, quick reference, troubleshooting assistant

#### ManualAccessed
- **Description**: User has retrieved manual for reference
- **Triggered When**: User opens manual document
- **Key Data**: Manual ID, appliance ID, access date, access reason, search terms used, pages viewed
- **Consumers**: Usage analytics, popular manual tracker, appliance problem indicator, search optimization

#### TroubleshootingGuideCreated
- **Description**: Custom troubleshooting reference has been compiled
- **Triggered When**: User creates quick reference for common issues
- **Key Data**: Guide ID, appliance ID, common problems, solutions, creation date, based on experience, manual references
- **Consumers**: Quick help library, knowledge base, family sharing, problem solver

### ServiceEvents

#### ServiceCallScheduled
- **Description**: Repair or maintenance service has been booked
- **Triggered When**: User schedules technician appointment
- **Key Data**: Service ID, appliance ID, service type, service provider, scheduled date, issue description, warranty status, estimated cost
- **Consumers**: Calendar integration, reminder scheduler, service history, warranty claim checker

#### ServiceCompleted
- **Description**: Appliance service or repair has been finished
- **Triggered When**: Technician completes work and user logs details
- **Key Data**: Service ID, completion date, work performed, parts replaced, labor cost, parts cost, total cost, technician, warranty claim associated
- **Consumers**: Service history, expense tracker, warranty claim, maintenance log, next service predictor

#### RepairRecommendationReceived
- **Description**: Technician has suggested repair or replacement
- **Triggered When**: Service call results in repair vs replace advice
- **Key Data**: Service ID, appliance ID, recommendation, estimated repair cost, replacement cost estimate, decision timeline, technician reasoning
- **Consumers**: Decision support, budget planner, replacement researcher, cost-benefit analyzer

#### ServiceProviderRated
- **Description**: Service technician performance has been evaluated
- **Triggered When**: User reviews service quality
- **Key Data**: Service ID, provider ID, rating score, quality of work, timeliness, professionalism, would use again, review comments
- **Consumers**: Provider directory, future service selection, provider relationship, rating aggregator

### MaintenanceEvents

#### MaintenanceScheduleCreated
- **Description**: Recurring maintenance plan has been established
- **Triggered When**: User sets up regular service schedule based on manual
- **Key Data**: Schedule ID, appliance ID, maintenance tasks, frequency, next due date, manual reference, estimated cost per service
- **Consumers**: Reminder scheduler, calendar integration, maintenance tracker, budget planner

#### MaintenanceReminderSent
- **Description**: Upcoming maintenance notification has been delivered
- **Triggered When**: Scheduled maintenance due date approaches
- **Key Data**: Reminder ID, appliance ID, maintenance task, due date, last completed date, instructions, service provider suggestion
- **Consumers**: Notification service, user action prompt, service scheduler, compliance tracker

#### MaintenanceCompleted
- **Description**: Scheduled maintenance task has been performed
- **Triggered When**: User logs completion of maintenance
- **Key Data**: Maintenance ID, appliance ID, completion date, tasks performed, cost, performed by, next due date, condition notes
- **Consumers**: Maintenance history, next occurrence scheduler, appliance health tracker, cost recorder

#### MaintenanceOverdue
- **Description**: Scheduled maintenance has not been completed on time
- **Triggered When**: Due date passes without maintenance logged
- **Key Data**: Maintenance ID, appliance ID, due date, days overdue, potential consequences, urgency level
- **Consumers**: Alert service, escalated reminder, risk assessor, scheduling prompt

### RecallEvents

#### RecallNotificationReceived
- **Description**: Manufacturer recall affects registered appliance
- **Triggered When**: System detects recall matching appliance model/serial
- **Key Data**: Recall ID, appliance ID, recall date, recall reason, safety risk, remedy available, claim deadline, manufacturer contact
- **Consumers**: Alert service, urgent notification, remedy tracker, safety priority, claim initiator

#### RecallRemedyCompleted
- **Description**: Recall repair or replacement has been performed
- **Triggered When**: User completes recall resolution process
- **Key Data**: Recall ID, resolution date, remedy type, service provider, cost (should be $0), resolution documentation
- **Consumers**: Recall tracker, safety status updater, documentation vault, appliance record updater

### EnergyEfficiencyEvents

#### EnergyRatingRecorded
- **Description**: Appliance energy efficiency information has been logged
- **Triggered When**: User enters Energy Star or efficiency rating data
- **Key Data**: Appliance ID, efficiency rating, annual energy cost estimate, energy label, rating date, comparison to average
- **Consumers**: Energy tracker, cost estimator, replacement ROI calculator, environmental impact

#### HighEnergyUsageDetected
- **Description**: Appliance appears to be consuming excessive energy
- **Triggered When**: Energy monitoring integration or manual tracking shows high usage
- **Key Data**: Appliance ID, detection date, usage level, expected usage, cost impact, efficiency degradation
- **Consumers**: Alert service, maintenance trigger, replacement consideration, cost impact analyzer

### DocumentationEvents

#### PurchaseReceiptUploaded
- **Description**: Proof of purchase has been digitized
- **Triggered When**: User uploads receipt or invoice
- **Key Data**: Receipt ID, appliance ID, purchase date, retailer, amount paid, payment method, receipt image, warranty implications
- **Consumers**: Proof of purchase vault, warranty validation, expense record, tax documentation

#### InstallationGuideAccessed
- **Description**: Installation manual has been retrieved
- **Triggered When**: User opens installation documentation
- **Key Data**: Appliance ID, access date, installation guide type, accessed for (new install/relocation/troubleshooting)
- **Consumers**: Usage tracker, installation support, problem indicator, guide improvement

#### SpecificationSheetSaved
- **Description**: Technical specifications have been documented
- **Triggered When**: User uploads or enters spec sheet information
- **Key Data**: Appliance ID, specifications (dimensions, power, capacity, etc.), spec sheet source, entry date
- **Consumers**: Specification library, replacement sizing, installation planning, compatibility checker
