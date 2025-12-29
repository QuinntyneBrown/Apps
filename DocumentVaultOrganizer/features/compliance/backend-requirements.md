# Compliance - Backend Requirements
## Domain Model
- RetentionPolicy: PolicyId, Name, RetentionPeriodDays
- LegalHold: HoldId, DocumentId, Reason, PlacedDate

## Commands
- ApplyRetentionPolicyCommand
- PlaceLegalHoldCommand
- ReleaseLegalHoldCommand

## API Endpoints
- POST /api/documents/{id}/retention-policy
- POST /api/documents/{id}/legal-hold
- DELETE /api/documents/{id}/legal-hold
