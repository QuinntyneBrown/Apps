# Receipt Management - Backend Requirements
## Domain Model
**Receipt Aggregate**: ReceiptId, DeductionId, FileType, FileSize, StorageUrl, OCRText, OCRConfidence, UploadDate, ExpiryDate, UserId
## Commands
- UploadReceiptCommand: Raises **ReceiptUploaded**, triggers OCR
- ProcessOCRCommand: Raises **ReceiptOCRProcessed**
- CheckMissingReceiptsCommand: Raises **ReceiptMissing** for large deductions
- CheckExpiringReceiptsCommand: Raises **ReceiptExpiring**
## Queries
- GetReceiptsByDeductionQuery, GetMissingReceiptsQuery, GetExpiringReceiptsQuery
## API Endpoints
- POST /api/receipts/upload, GET /api/receipts/{id}, GET /api/receipts/missing
