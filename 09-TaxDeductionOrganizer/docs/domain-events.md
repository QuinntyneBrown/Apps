# Domain Events - Tax Deduction Organizer

## Overview
This document defines the domain events tracked in the Tax Deduction Organizer application. These events capture significant business occurrences related to tax deduction tracking, receipt management, category organization, and tax filing preparation.

## Events

### DeductionEvents

#### DeductionRecorded
- **Description**: A tax-deductible expense has been logged
- **Triggered When**: User enters a deductible expense with supporting documentation
- **Key Data**: Deduction ID, amount, category, date, vendor, description, tax year, receipt attached, user ID, timestamp
- **Consumers**: Deduction aggregator, category tracker, tax calculator, receipt manager

#### DeductionCategorized
- **Description**: Deduction has been assigned to IRS tax category
- **Triggered When**: User or system assigns deduction to specific tax category (Schedule A, C, etc.)
- **Key Data**: Deduction ID, category code, category name, confidence score, categorization method, user ID, timestamp
- **Consumers**: Tax form mapper, category totals calculator, audit checker

#### DeductionFlagged
- **Description**: Deduction has been marked for review or clarification
- **Triggered When**: System or user identifies questionable or incomplete deduction
- **Key Data**: Deduction ID, flag reason, flag type, flagged by, flag date, resolution needed
- **Consumers**: Review queue, alert service, audit risk analyzer

#### DeductionApproved
- **Description**: Flagged deduction has been verified and approved
- **Triggered When**: User or tax professional confirms deduction is valid
- **Key Data**: Deduction ID, approver, approval notes, approval date, supporting docs verified
- **Consumers**: Tax calculator, final report generator, audit trail

### ReceiptEvents

#### ReceiptUploaded
- **Description**: Receipt or supporting document has been uploaded
- **Triggered When**: User attaches receipt image or PDF to deduction
- **Key Data**: Receipt ID, deduction ID, file type, file size, upload date, OCR status, user ID
- **Consumers**: Receipt storage, OCR processor, document manager, backup service

#### ReceiptOCRProcessed
- **Description**: Receipt has been scanned and text extracted via OCR
- **Triggered When**: OCR processing completes on uploaded receipt
- **Key Data**: Receipt ID, extracted text, vendor name, amount, date, confidence scores, processing timestamp
- **Consumers**: Auto-fill service, deduction matcher, data validator

#### ReceiptMissing
- **Description**: Required receipt is not attached to a deduction
- **Triggered When**: Deduction exceeds threshold requiring receipt documentation
- **Key Data**: Deduction ID, amount, category, requirement reason, identification date
- **Consumers**: Alert service, completion checker, audit risk tracker

#### ReceiptExpiring
- **Description**: Receipt image quality degrading or approaching retention limit
- **Triggered When**: Receipt scan is old or quality insufficient for IRS requirements
- **Key Data**: Receipt ID, upload date, quality score, retention requirement, expiry date
- **Consumers**: Re-scan reminder, quality checker, retention manager

### CategoryEvents

#### CategoryLimitApproached
- **Description**: Deductions in a category approaching IRS limits or audit triggers
- **Triggered When**: Category total nears threshold that may raise audit flags
- **Key Data**: Category name, current total, threshold amount, percentage of limit, tax year, user ID
- **Consumers**: Alert service, audit risk analyzer, tax strategy advisor

#### CategoryTotalCalculated
- **Description**: Total deductions for a category have been computed
- **Triggered When**: End of period or user requests category summary
- **Key Data**: Category name, tax year, total amount, number of deductions, calculation timestamp
- **Consumers**: Tax form filler, deduction summary, reporting service

#### UnusualDeductionPatternDetected
- **Description**: System identified atypical deduction pattern that may need review
- **Triggered When**: Machine learning detects anomaly in deduction amounts or frequency
- **Key Data**: Pattern type, affected deductions, deviation from norm, risk level, detection timestamp
- **Consumers**: Review recommender, audit risk evaluator, user alert

### TaxYearEvents

#### TaxYearCreated
- **Description**: New tax year has been initialized for tracking
- **Triggered When**: User starts tracking deductions for a new tax year
- **Key Data**: Tax year, start date, end date, filing deadline, user ID, timestamp
- **Consumers**: Deduction organizer, deadline tracker, category initializer

#### TaxYearClosed
- **Description**: Tax year has been finalized and locked
- **Triggered When**: User completes filing or marks year as final
- **Key Data**: Tax year, closure date, total deductions, categories used, return filed status, user ID
- **Consumers**: Historical archiver, read-only enforcer, multi-year analyzer

#### FilingDeadlineApproaching
- **Description**: Tax filing deadline is nearing
- **Triggered When**: Deadline is X days away (e.g., 30, 15, 7, 1 day)
- **Key Data**: Tax year, filing deadline, days remaining, completion percentage, user ID, timestamp
- **Consumers**: Urgency alert service, notification system, completion tracker

### MileageEvents

#### BusinessMileageLogged
- **Description**: Business mileage has been recorded for tax deduction
- **Triggered When**: User logs miles driven for business purposes
- **Key Data**: Mileage ID, miles driven, purpose, start location, end location, date, IRS rate, deduction amount, user ID
- **Consumers**: Mileage deduction calculator, category aggregator, trip tracker

#### MileageRateUpdated
- **Description**: IRS standard mileage rate has been updated
- **Triggered When**: New tax year begins or IRS announces rate change
- **Key Data**: Effective date, previous rate, new rate, rate type (business/medical/charity), tax year
- **Consumers**: Deduction recalculator, historical mileage updater, notification service

### HomeOfficeEvents

#### HomeOfficeDeductionCalculated
- **Description**: Home office deduction amount has been computed
- **Triggered When**: User inputs square footage and calculates deduction
- **Key Data**: Calculation ID, office square feet, total home square feet, method (simplified/actual), deduction amount, tax year, user ID
- **Consumers**: Schedule C filler, deduction aggregator, documentation checker

#### HomeOfficeDocumentationAdded
- **Description**: Supporting documentation for home office deduction uploaded
- **Triggered When**: User attaches floor plan, utility bills, or other required docs
- **Key Data**: Document ID, document type, home office calculation ID, upload date, user ID
- **Consumers**: Documentation validator, audit preparation, requirement checker

### CharitableEvents

#### CharitableDonationRecorded
- **Description**: Charitable contribution has been logged
- **Triggered When**: User records cash or non-cash donation to qualified organization
- **Key Data**: Donation ID, organization name, EIN, amount/value, donation type, date, acknowledgment received, user ID
- **Consumers**: Schedule A aggregator, charity validator, receipt requirement checker

#### DonationAcknowledgmentReceived
- **Description**: Required donation acknowledgment letter obtained
- **Triggered When**: User uploads acknowledgment for donations over $250
- **Key Data**: Donation ID, acknowledgment date, letter uploaded, organization confirmed, user ID
- **Consumers**: Compliance checker, documentation manager, audit preparation

### ExportEvents

#### TaxReportGenerated
- **Description**: Summary report of all deductions has been created
- **Triggered When**: User requests export for tax preparation or filing
- **Key Data**: Report ID, tax year, format (PDF/CSV/Excel), total deductions by category, generation timestamp, user ID
- **Consumers**: Report delivery, tax software integration, filing preparation

#### DataExportedToTaxSoftware
- **Description**: Deduction data has been exported to tax preparation software
- **Triggered When**: User exports to TurboTax, H&R Block, or other tax software
- **Key Data**: Export ID, destination software, tax year, deduction count, export format, timestamp, user ID
- **Consumers**: Integration service, export logger, validation checker
