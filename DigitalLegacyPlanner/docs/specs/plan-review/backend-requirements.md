# Plan Review - Backend Requirements

## API Endpoints

#### POST /api/review
Review and update legacy plan
- **Request Body**: `{ reviewDate, changesMade, newAccountsAdded }`
- **Events**: `LegacyPlanReviewed`

#### GET /api/review/completeness
Assess plan completeness
- **Response**: Completion percentage and missing elements
- **Events**: `PlanCompletenessAssessed`

#### POST /api/review/reminder
Send annual update reminder
- **Events**: `AnnualUpdateReminder`

## Business Logic
- Calculate completion percentage
- Identify gaps in coverage
- Track accounts without instructions
- Monitor review frequency
- Send automated reminders
