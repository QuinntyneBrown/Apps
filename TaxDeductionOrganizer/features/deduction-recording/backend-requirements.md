# Deduction Recording - Backend Requirements
## Domain Model
**Deduction Aggregate**: DeductionId, Amount, Category, Date, Vendor, Description, TaxYear, ReceiptAttached, UserId, FlaggedForReview, ApprovedBy, CreatedAt
## Commands
- RecordDeductionCommand: Raises **DeductionRecorded**
- CategorizeDeductionCommand: Raises **DeductionCategorized**
- FlagDeductionCommand: Raises **DeductionFlagged**
- ApproveDeductionCommand: Raises **DeductionApproved**
## Queries
- GetDeductionsByYearQuery, GetDeductionsByCategoryQuery, GetFlaggedDeductionsQuery
## API Endpoints
- POST /api/deductions, PUT /api/deductions/{id}/categorize, PUT /api/deductions/{id}/flag
