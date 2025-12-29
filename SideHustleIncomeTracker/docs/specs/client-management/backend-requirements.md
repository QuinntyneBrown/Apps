# Client Management - Backend Requirements

## Domain Model
### Client Aggregate
- ClientId, UserId, ClientName, Email, Phone, Address, StreamId, AcquisitionDate, Status (Active/Inactive/Lost), TotalRevenue, CreatedAt, UpdatedAt

### Invoice Aggregate
- InvoiceId, ClientId, StreamId, UserId, InvoiceNumber, Amount, LineItems, InvoiceDate, DueDate, Status (Draft/Sent/Paid/Overdue), PaidDate, PaymentMethod

## Commands
- AddClientCommand: Creates client, raises **ClientAdded** event
- SendInvoiceCommand: Generates invoice, raises **ClientInvoiced** event
- RecordPaymentCommand: Marks invoice paid, raises **ClientPaid** event
- MarkClientLostCommand: Deactivates client, raises **ClientLost** event

## Queries
- GetClientsByStreamQuery, GetClientByIdQuery, GetClientInvoicesQuery, GetOutstandingInvoicesQuery, GetClientRevenueQuery

## API Endpoints
- POST /api/clients (add client)
- GET /api/clients (list clients)
- POST /api/clients/{id}/invoices (create invoice)
- GET /api/clients/{id}/invoices (get invoices)
- PUT /api/invoices/{id}/pay (mark paid)

## Events
- **ClientAdded**: Initializes client in CRM
- **ClientInvoiced**: Schedules payment reminder
- **ClientPaid**: Updates revenue, clears AR
- **ClientLost**: Triggers retention analysis
