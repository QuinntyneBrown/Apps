# Specialized Deductions - Backend Requirements
## Domain Model
**MileageLog**: MileageId, Miles, Purpose, StartLocation, EndLocation, Date, IRSRate, DeductionAmount, UserId
**HomeOfficeCalculation**: CalculationId, OfficeSquareFeet, TotalHomeSquareFeet, Method, DeductionAmount, TaxYear, UserId
**CharitableDonation**: DonationId, OrganizationName, EIN, Amount, DonationType, Date, AcknowledgmentReceived, UserId
## Commands
- LogBusinessMileageCommand: Raises **BusinessMileageLogged**
- UpdateMileageRateCommand: Raises **MileageRateUpdated**
- CalculateHomeOfficeCommand: Raises **HomeOfficeDeductionCalculated**
- RecordCharitableDonationCommand: Raises **CharitableDonationRecorded**
## API Endpoints
- POST /api/deductions/mileage, POST /api/deductions/home-office, POST /api/deductions/charitable
