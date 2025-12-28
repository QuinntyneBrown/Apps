# Domain Events - Charitable Giving Tracker

## Overview
This document defines the domain events tracked in the Charitable Giving Tracker application. These events capture significant business occurrences related to charitable donations, tax-deductible contributions, nonprofit tracking, and philanthropic impact measurement.

## Events

### DonationEvents

#### DonationMade
- **Description**: A charitable donation has been recorded
- **Triggered When**: User logs a cash, check, or online donation to a charity
- **Key Data**: Donation ID, organization ID, amount, donation date, payment method, tax deductible, acknowledgment status, user ID, timestamp
- **Consumers**: Donation tracker, tax calculator, impact aggregator, receipt manager

#### RecurringDonationScheduled
- **Description**: Automatic recurring donation has been set up
- **Triggered When**: User configures monthly, quarterly, or annual recurring gift
- **Key Data**: Schedule ID, organization ID, amount, frequency, start date, end date, payment method, user ID, timestamp
- **Consumers**: Payment scheduler, annual projection calculator, donation executor

#### RecurringDonationExecuted
- **Description**: Scheduled recurring donation has been processed
- **Triggered When**: Recurring donation cycle triggers payment
- **Key Data**: Donation ID, schedule ID, organization ID, amount, execution date, next scheduled date, user ID
- **Consumers**: Donation tracker, tax aggregator, payment recorder, receipt requester

#### DonationRefunded
- **Description**: A donation has been returned or refunded
- **Triggered When**: Charity returns donation or transaction is reversed
- **Key Data**: Donation ID, original amount, refund amount, refund date, refund reason, user ID
- **Consumers**: Tax adjuster, donation history, accounting reconciler

### OrganizationEvents

#### CharityAdded
- **Description**: A new charitable organization has been added to tracking
- **Triggered When**: User creates record for a charity they support or plan to support
- **Key Data**: Organization ID, name, EIN, charity type, mission, website, user ID, timestamp
- **Consumers**: Organization manager, validation service, search index

#### CharityVerified
- **Description**: Organization's tax-exempt status has been verified
- **Triggered When**: System validates charity with IRS database or user confirms 501(c)(3) status
- **Key Data**: Organization ID, EIN, verification source, verification date, tax-exempt status, user ID
- **Consumers**: Tax deductibility validator, charity rating service, compliance tracker

#### CharityRatingUpdated
- **Description**: Third-party charity rating or score has been updated
- **Triggered When**: Integration retrieves new ratings from Charity Navigator, GiveWell, etc.
- **Key Data**: Organization ID, rating source, score, rating date, previous rating, user ID
- **Consumers**: Giving advisor, organization dashboard, recommendation engine

#### PreferredCharitySet
- **Description**: User has marked a charity as preferred or favorite
- **Triggered When**: User designates a charity for quick access or prioritization
- **Key Data**: Organization ID, preference level, set date, user ID
- **Consumers**: Quick donate feature, suggestion engine, dashboard highlighter

### TaxEvents

#### TaxDeductionCalculated
- **Description**: Total tax-deductible donations calculated for a period
- **Triggered When**: End of year or user requests tax summary
- **Key Data**: Tax year, total deductible amount, number of donations, qualified organizations count, timestamp, user ID
- **Consumers**: Tax report generator, Form 8283 preparer, annual summary

#### AcknowledgmentLetterReceived
- **Description**: Required donation acknowledgment has been obtained
- **Triggered When**: User uploads acknowledgment letter for donation over $250
- **Key Data**: Donation ID, organization ID, acknowledgment date, letter content, upload timestamp, user ID
- **Consumers**: Tax compliance checker, documentation manager, IRS requirement validator

#### AcknowledgmentLetterMissing
- **Description**: Required acknowledgment letter not received for large donation
- **Triggered When**: Donation exceeds $250 threshold without uploaded acknowledgment
- **Key Data**: Donation ID, amount, organization ID, donation date, days since donation, user ID
- **Consumers**: Alert service, documentation reminder, tax risk tracker

#### NonCashDonationValued
- **Description**: Fair market value assigned to non-cash donation
- **Triggered When**: User logs donation of goods, stock, or property
- **Key Data**: Donation ID, item description, quantity, assessed value, valuation method, appraisal required, user ID, timestamp
- **Consumers**: Tax deduction calculator, Form 8283 generator, appraisal tracker

### ImpactEvents

#### AnnualGivingGoalSet
- **Description**: User has established charitable giving target for the year
- **Triggered When**: User sets annual donation goal amount
- **Key Data**: Goal ID, target amount, tax year, goal type, creation date, user ID, timestamp
- **Consumers**: Progress tracker, recommendation engine, motivation service

#### GivingGoalReached
- **Description**: Annual charitable giving goal has been achieved
- **Triggered When**: Total donations meet or exceed goal amount
- **Key Data**: Goal ID, target amount, actual amount, achievement date, days to achieve, user ID
- **Consumers**: Achievement service, notification system, celebration trigger

#### ImpactMilestoneReached
- **Description**: Cumulative giving has reached significant milestone
- **Triggered When**: Lifetime or annual donations cross threshold ($1K, $5K, $10K, etc.)
- **Key Data**: Milestone amount, milestone type (lifetime/annual), achievement date, total donations count, user ID
- **Consumers**: Achievement service, impact visualizer, notification system

#### BeneficiaryImpactReported
- **Description**: Charity has reported impact of donations
- **Triggered When**: User logs or imports charity impact report
- **Key Data**: Organization ID, impact metric, beneficiaries helped, program outcomes, reporting period, user ID
- **Consumers**: Impact dashboard, giving effectiveness analyzer, motivation system

### ReportingEvents

#### AnnualDonationReportGenerated
- **Description**: Year-end summary of charitable giving created
- **Triggered When**: User requests annual giving report for taxes or personal review
- **Key Data**: Report ID, tax year, total donated, organizations supported, tax deduction total, generation timestamp, user ID
- **Consumers**: Report delivery, tax preparation, PDF generator

#### CharityDiversificationAnalyzed
- **Description**: Analysis of giving distribution across causes and organizations
- **Triggered When**: User requests giving pattern analysis
- **Key Data**: Analysis period, number of organizations, cause category breakdown, concentration metrics, timestamp, user ID
- **Consumers**: Diversification advisor, giving strategy planner, visualization service

### MatchingEvents

#### EmployerMatchAvailable
- **Description**: Employer matching program identified for a donation
- **Triggered When**: User links employer or system identifies match-eligible donation
- **Key Data**: Donation ID, employer ID, match ratio, maximum match amount, match status, user ID
- **Consumers**: Match claim tracker, employer integration, impact multiplier

#### EmployerMatchClaimed
- **Description**: Employer matching funds have been requested
- **Triggered When**: User submits employer match request
- **Key Data**: Match ID, donation ID, match amount, submission date, approval status, user ID
- **Consumers**: Match tracker, total impact calculator, follow-up reminder

#### EmployerMatchReceived
- **Description**: Employer has confirmed matching donation to charity
- **Triggered When**: Employer processes match and confirms with charity
- **Key Data**: Match ID, match amount, confirmation date, total impact, user ID
- **Consumers**: Impact aggregator, achievement tracker, tax recorder

### PledgeEvents

#### PledgeMade
- **Description**: User has committed to make future donation
- **Triggered When**: User creates pledge for future giving campaign or timeline
- **Key Data**: Pledge ID, organization ID, pledge amount, pledge date, fulfillment deadline, user ID, timestamp
- **Consumers**: Pledge tracker, reminder scheduler, commitment monitor

#### PledgeFulfilled
- **Description**: Pledged donation has been completed
- **Triggered When**: User makes donation that satisfies a pledge
- **Key Data**: Pledge ID, donation ID, fulfillment date, fulfilled amount, user ID
- **Consumers**: Pledge manager, commitment tracker, notification service

#### PledgeReminder
- **Description**: Reminder sent about unfulfilled pledge
- **Triggered When**: Pledge deadline approaching or overdue
- **Key Data**: Pledge ID, organization ID, amount remaining, deadline, days until/overdue, user ID
- **Consumers**: Reminder notification, email service, commitment tracker
