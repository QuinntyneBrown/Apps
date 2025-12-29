# Revenue Tracking - Frontend Requirements

## Overview
UI for recording income, managing recurring revenue, and monitoring payment status.

## Key Components

### 1. Income Recording Form
- Quick income entry with autocomplete
- Fields: Amount, Stream, Client, Payment Date, Method, Description
- Receipt/invoice attachment option
- Save and add another workflow

### 2. Income List View
- Tabular display of all income
- Filters: Stream, Date Range, Payment Method, Client
- Sort by Date, Amount, Stream
- Export to CSV/Excel
- Quick edit and delete actions

### 3. Recurring Income Manager
- List of active recurring schedules
- Add/Edit/Cancel schedule forms
- Next expected payment dates
- Payment history for each schedule
- Overdue payment alerts

### 4. Revenue Dashboard
- Total income charts (daily, weekly, monthly)
- Income by stream pie chart
- Income by payment method breakdown
- Recent income transactions
- Month-over-month comparison

### 5. Payment Tracking
- Overdue payments list
- Expected vs. actual payment comparison
- Payment reminders and notifications

## Responsive Design
- Mobile-first income entry
- Responsive tables with horizontal scroll
- Touch-friendly date pickers
- Bottom sheet modals on mobile

## API Integration
- POST /api/income (record income)
- GET /api/income/stream/{id} (fetch by stream)
- POST /api/income/recurring (schedule recurring)
- GET /api/income/statistics (dashboard data)

## User Feedback
- Success: "Income recorded: $X from Client"
- Warning: "Payment overdue: Invoice #123"
- Milestone: "New record! Highest payment received!"
