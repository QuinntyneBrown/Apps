# Domain Events - Home Inventory Manager

## Overview
This document defines the domain events tracked in the Home Inventory Manager application. These events capture significant business occurrences related to cataloging possessions, insurance documentation, item valuation, location tracking, and loss prevention.

## Events

### ItemEvents

#### ItemAdded
- **Description**: New possession has been added to home inventory
- **Triggered When**: User catalogs item with details and documentation
- **Key Data**: Item ID, name, description, category, purchase date, purchase price, current value, location, serial number, photos, receipt
- **Consumers**: Inventory database, insurance valuation, search indexer, location tracker, depreciation calculator

#### ItemUpdated
- **Description**: Existing inventory item details have been modified
- **Triggered When**: User edits item information or updates value
- **Key Data**: Item ID, updated fields, previous values, new values, update timestamp, update reason
- **Consumers**: Inventory database, insurance recalculator, change log, value tracker

#### ItemRemoved
- **Description**: Item has been removed from active inventory
- **Triggered When**: User disposes, sells, donates, or loses item
- **Key Data**: Item ID, removal date, removal reason, disposal method, final value, ownership duration
- **Consumers**: Inventory updater, insurance adjuster, tax documentation, loss tracker, analytics

#### ItemRelocated
- **Description**: Item has been moved to different location in home
- **Triggered When**: User updates item storage location
- **Key Data**: Item ID, previous location, new location, relocation date, reason for move
- **Consumers**: Location index, room inventory, finder tool, organization optimizer

### ValuationEvents

#### ItemValuationUpdated
- **Description**: Item's estimated current value has been recalculated
- **Triggered When**: User manually updates value or system auto-calculates depreciation
- **Key Data**: Item ID, previous value, new value, valuation method, market data source, update date
- **Consumers**: Total inventory value, insurance coverage calculator, net worth tracker, depreciation analytics

#### BulkRevaluationCompleted
- **Description**: Multiple items have been revalued simultaneously
- **Triggered When**: User or system performs category or location-based revaluation
- **Key Data**: Number of items, category/location, total value change, valuation date, method used
- **Consumers**: Insurance coverage review, portfolio analyzer, adjustment recommendations

#### HighValueItemIdentified
- **Description**: Item value exceeds high-value threshold requiring special attention
- **Triggered When**: Item valuation reaches or exceeds configured threshold
- **Key Data**: Item ID, current value, threshold exceeded, insurance implications, documentation completeness
- **Consumers**: Insurance alert, documentation checker, security recommendations, special coverage advisor

### LocationEvents

#### LocationCreated
- **Description**: New storage location has been added to home layout
- **Triggered When**: User defines new room, storage area, or container
- **Key Data**: Location ID, location name, location type, parent location, description, capacity
- **Consumers**: Location hierarchy, inventory organizer, space optimizer, search filter

#### LocationInventoryCompleted
- **Description**: All items in specific location have been cataloged/verified
- **Triggered When**: User completes inventory count for room or storage area
- **Key Data**: Location ID, inventory date, item count, total value, missing items, new items found
- **Consumers**: Completion tracker, accuracy validator, inventory reconciliation, insurance documentation

#### ItemLocationUnknown
- **Description**: Item cannot be located in expected storage location
- **Triggered When**: User marks item as misplaced during inventory check
- **Key Data**: Item ID, expected location, last seen date, search status, item value, urgency
- **Consumers**: Search assistant, alert system, loss prevention, recovery workflow

### InsuranceEvents

#### InsuranceCoverageCalculated
- **Description**: Total required insurance coverage has been computed
- **Triggered When**: Inventory value changes trigger coverage recalculation
- **Key Data**: Calculation date, total inventory value, current coverage amount, coverage gap, recommended coverage
- **Consumers**: Insurance advisor, coverage gap alert, policy review trigger, recommendation engine

#### InsuranceDocumentationGenerated
- **Description**: Comprehensive insurance documentation package has been created
- **Triggered When**: User requests insurance report or files claim
- **Key Data**: Report ID, generation date, items included, total value, photos included, receipt copies, item count
- **Consumers**: Insurance claim support, PDF generator, document vault, sharing service

#### InsuranceClaimInitiated
- **Description**: Insurance claim has been started for lost, stolen, or damaged items
- **Triggered When**: User begins claim process for covered items
- **Key Data**: Claim ID, claimed items, claim type, incident date, total claim value, insurance provider, documentation attached
- **Consumers**: Claim tracker, documentation packager, insurance communication, item status updater

#### InsuranceClaimSettled
- **Description**: Insurance claim has been resolved with payout
- **Triggered When**: Claim finalized with insurance company
- **Key Data**: Claim ID, settlement amount, settlement date, items covered, replacement status, claim outcome
- **Consumers**: Item status updater, financial tracker, replacement workflow, claim history

### CategoryEvents

#### CategoryCreated
- **Description**: New inventory category has been established
- **Triggered When**: User defines custom category for organizing items
- **Key Data**: Category ID, category name, parent category, description, insurance classification, depreciation rules
- **Consumers**: Category hierarchy, item classifier, reporting groups, insurance mapping

#### CategoryConsolidated
- **Description**: Multiple categories have been merged
- **Triggered When**: User combines redundant or overlapping categories
- **Key Data**: Surviving category ID, merged category IDs, affected item count, consolidation date
- **Consumers**: Item recategorizer, hierarchy updater, search index, reporting adjuster

### PhotoDocumentationEvents

#### ItemPhotographed
- **Description**: Photo documentation has been added for item
- **Triggered When**: User uploads or captures photos of possession
- **Key Data**: Item ID, photo IDs, photo count, capture date, photo quality, angles captured
- **Consumers**: Photo gallery, insurance documentation, visual search, condition tracking

#### ReceiptScanned
- **Description**: Purchase receipt has been digitized and attached to item
- **Triggered When**: User uploads receipt image or PDF
- **Key Data**: Item ID, receipt ID, purchase date, purchase price, vendor, receipt image, OCR data
- **Consumers**: Proof of purchase vault, warranty tracker, expense documentation, tax records

#### SerialNumberRecorded
- **Description**: Item serial number or unique identifier has been documented
- **Triggered When**: User enters serial number for trackable item
- **Key Data**: Item ID, serial number, manufacturer, model number, registration status, verification method
- **Consumers**: Theft recovery database, warranty registration, authenticity verification, manufacturer recall checker

### SearchDiscoveryEvents

#### InventorySearchPerformed
- **Description**: User has searched for items in inventory
- **Triggered When**: User executes search query
- **Key Data**: Search query, filters applied, results count, search timestamp, user ID
- **Consumers**: Search analytics, popular item tracker, inventory usage patterns

#### MissingItemFound
- **Description**: Previously misplaced item has been located
- **Triggered When**: User updates status of missing item to found
- **Key Data**: Item ID, found location, found date, missing duration, search method, different than expected location
- **Consumers**: Location updater, status clearer, analytics, organization recommendations

### MaintenanceEvents

#### ItemMaintenanceScheduled
- **Description**: Maintenance or service has been scheduled for valuable item
- **Triggered When**: User creates maintenance reminder for item
- **Key Data**: Item ID, maintenance type, scheduled date, service provider, estimated cost, maintenance history
- **Consumers**: Reminder scheduler, maintenance tracker, cost estimator, service history

#### ItemConditionAssessed
- **Description**: Item physical condition has been evaluated and documented
- **Triggered When**: User performs periodic condition check
- **Key Data**: Item ID, condition rating, assessment date, issues noted, photos, value impact, maintenance needed
- **Consumers**: Condition tracker, value adjuster, maintenance planner, insurance documentation

### ReportingEvents

#### InventoryReportGenerated
- **Description**: Comprehensive inventory report has been created
- **Triggered When**: User requests summary report for specific purpose
- **Key Data**: Report ID, report type, date range, items included, total value, categories, generation date
- **Consumers**: Report viewer, PDF exporter, email service, archive storage

#### NetWorthImpactCalculated
- **Description**: Inventory's contribution to net worth has been computed
- **Triggered When**: Financial planning tool requests asset valuation
- **Key Data**: Calculation date, total inventory value, depreciated value, liquid value estimate, category breakdown
- **Consumers**: Net worth dashboard, financial planning, asset allocation, wealth tracking
