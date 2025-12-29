# Client Management - Frontend Requirements

## Key Components
1. **Client Directory**: List with search, filter by stream/status
2. **Client Profile**: Details, revenue history, invoices, contact info
3. **Invoice Generator**: Line items, auto-numbering, PDF export
4. **Payment Tracker**: Outstanding invoices, overdue alerts
5. **Client Revenue Dashboard**: Top clients, lifetime value

## API Integration
- POST /api/clients, GET /api/clients, POST /api/clients/{id}/invoices
## User Feedback
- "Client added successfully"
- "Invoice #1234 sent to Client"
- "Payment received: $X from Client"
