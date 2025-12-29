# Backend - Cost Sharing
## Events: ExpenseAdded, CostSplitCalculated, PaymentRecorded, SettlementCompleted
## Endpoints: POST /api/expenses, GET /api/events/{id}/splits, POST /api/payments
## Model: Expense { PayerId, Amount, Description }, Payment { PayerId, PayeeId, Amount }
