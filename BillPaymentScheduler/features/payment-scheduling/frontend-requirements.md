# Payment Scheduling - Frontend Requirements

## Overview
The Payment Scheduling frontend provides users with an intuitive interface to schedule, manage, and track bill payments.

## User Stories

### As a User, I want to:
1. Schedule a payment for a specific bill and date
2. View all my scheduled payments in one place
3. Cancel a scheduled payment before it's executed
4. Modify payment details before execution
5. See upcoming payments in chronological order
6. View my payment history with confirmations
7. Retry failed payments
8. Set up recurring payments automatically
9. Manage my payment methods securely
10. Receive confirmation when payments are scheduled

## Pages/Views

### 1. Payment Scheduling Dashboard
**Route**: `/payments`

**Purpose**: Overview of all payment-related activities

**Components**:
- Payment summary cards (Scheduled, Completed, Failed, Total amount)
- Upcoming payments timeline (next 30 days)
- Recent payment history
- Quick actions (Schedule payment, Add payment method)
- Payment calendar view
- Payment status filters

**Features**:
- Filter by status (All, Scheduled, Completed, Failed, Cancelled)
- Filter by date range
- Filter by bill
- Sort by date, amount, status
- Search by bill name or confirmation number
- Export payment history

### 2. Schedule Payment Form
**Route**: `/payments/schedule`

**Purpose**: Create a new payment schedule

**Form Fields**:
- Bill selection (required, dropdown with search)
- Payment amount (required, currency input, auto-populated from bill)
- Scheduled date (required, date picker, min: tomorrow)
- Payment method (required, dropdown with saved methods)
- Recurring payment (toggle)
  - If enabled: Recurrence pattern (dropdown: Weekly, Bi-Weekly, Monthly, Quarterly, Annually)
- Notes (optional, textarea, max 500 chars)

**Validation**:
- Bill must be selected
- Amount must be > 0 and <= bill amount
- Scheduled date must be at least 1 day in future
- Payment method must be selected and valid
- Real-time validation with inline error messages

**Actions**:
- Schedule Payment (creates payment and returns to dashboard)
- Schedule & Add Another (creates payment and clears form)
- Cancel (returns to dashboard with confirmation if form is dirty)

**Additional Features**:
- Show bill details when selected (amount, due date, payee)
- Highlight if scheduled date is after bill due date
- Show available balance for selected payment method (if applicable)
- Calculate and display processing fee (if any)
- Preview recurring schedule (if recurring enabled)

### 3. Payment Details View
**Route**: `/payments/{paymentScheduleId}`

**Purpose**: View complete payment information

**Sections**:
- Payment summary card
  - Bill name, amount, scheduled date
  - Status badge
  - Confirmation number (if executed)
  - Payment method
- Bill details
  - Link to bill details page
  - Bill amount, due date, category
- Payment method details
  - Type, last 4 digits
  - Expiration date
- Payment timeline
  - Scheduled date and time
  - Executed date and time (if completed)
  - Cancelled date and time (if cancelled)
- Notes section
- Actions history
  - All modifications and attempts
  - User, timestamp, action taken

**Actions**:
- Modify Payment (if not executed)
- Cancel Payment (if not executed)
- Retry Payment (if failed)
- Print Receipt
- Download PDF
- Back to Payments

**Conditional Display**:
- Show confirmation number only if completed
- Show failure reason if failed
- Show cancellation reason if cancelled
- Enable actions based on payment status

### 4. Upcoming Payments View
**Route**: `/payments/upcoming`

**Purpose**: See all scheduled payments

**Features**:
- Timeline view showing payments by date
- Group by date (Today, Tomorrow, This Week, Later)
- Visual indicators for payment amount
- Payment method icons
- Quick actions on each payment
  - Execute now
  - Modify
  - Cancel
- Total amount due summary
- Filter by date range
- Filter by bill

**Payment Card**:
- Bill name
- Amount
- Scheduled date with countdown
- Payment method
- Status badge
- Quick action buttons

### 5. Payment History View
**Route**: `/payments/history`

**Purpose**: View completed and failed payments

**Features**:
- Tabular view with columns:
  - Date
  - Bill
  - Amount
  - Payment Method
  - Status
  - Confirmation Number
  - Actions
- Date range filter
- Status filter (All, Completed, Failed)
- Search by bill name or confirmation number
- Export to CSV/PDF
- Pagination
- Sort by any column

**Actions per Payment**:
- View Details
- Download Receipt
- Retry (if failed)
- Report Issue

### 6. Payment Methods Management
**Route**: `/payment-methods`

**Purpose**: Manage saved payment methods

**Features**:
- List of saved payment methods
- Add new payment method
- Set default payment method
- Remove payment method
- Edit payment method details

**Payment Method Card**:
- Type icon (bank, credit card, etc.)
- Display name
- Last 4 digits
- Expiration date (if applicable)
- Default badge
- Status (active/expired)
- Actions (Edit, Delete, Set as Default)

**Add Payment Method Form**:
- Type (Bank Account, Credit Card, Debit Card)
- Display name
- Account/Card number
- Routing number (for bank accounts)
- Expiration date (for cards)
- CVV (not stored)
- Set as default option

**Security Features**:
- CVV required for adding cards
- PCI compliant input fields
- Tokenization for sensitive data
- Never display full numbers
- Confirm before deletion

### 7. Modify Payment Form
**Route**: `/payments/{paymentScheduleId}/edit`

**Purpose**: Edit scheduled payment details

**Features**:
- Pre-populated form with current values
- Same validation as schedule form
- Show warning if payment is within 24 hours
- Highlight changed fields
- Comparison view (old vs new)

**Restrictions**:
- Cannot modify executed payments
- Cannot modify cancelled payments
- Limited modifications within 24 hours of scheduled date
- Must provide reason for modification

### 8. Payment Calendar
**Route**: `/payments/calendar`

**Purpose**: Visualize payments on a calendar

**Features**:
- Monthly calendar view
- Payments displayed on scheduled dates
- Color-coded by status
- Color-coded by amount (small, medium, large)
- Click payment to view details
- Month/Year navigation
- Total amount per day
- Legend for color codes

**Interactions**:
- Hover to see payment preview
- Click to view full details
- Drag-and-drop to reschedule (with confirmation)

## UI Components

### PaymentScheduleCard
- Displays payment summary
- Status indicator
- Bill name, amount, date
- Payment method icon
- Quick action buttons
- Responsive design

### PaymentTimeline
- Chronological display of payments
- Group by time period
- Visual connection between payments
- Expandable for details

### PaymentStatusBadge
- Color-coded status indicator
- Icons for each status
- Tooltip with status description

### PaymentMethodSelector
- Dropdown with payment methods
- Visual icons for method types
- Show last 4 digits
- Add new method option
- Mark expired methods

### RecurrencePreview
- Shows next 5-10 occurrences
- Dates and amounts
- Edit recurrence pattern
- Cancel specific occurrences

### PaymentConfirmation
- Summary of payment details
- Amount breakdown
- Fees (if any)
- Total amount
- Confirm/Cancel actions

## State Management

### Payment State
```typescript
interface PaymentState {
  paymentSchedules: PaymentSchedule[];
  selectedPayment: PaymentSchedule | null;
  paymentMethods: PaymentMethod[];
  loading: boolean;
  error: string | null;
  filters: PaymentFilters;
  pagination: Pagination;
}
```

### Payment Models
```typescript
interface PaymentSchedule {
  paymentScheduleId: string;
  billId: string;
  billName: string;
  userId: string;
  paymentMethodId: string;
  amount: number;
  scheduledDate: Date;
  isRecurring: boolean;
  recurrencePattern: RecurrencePattern;
  status: PaymentStatus;
  confirmationNumber?: string;
  notes?: string;
  executedAt?: Date;
  cancelledAt?: Date;
  failureReason?: string;
  createdAt: Date;
  updatedAt: Date;
}

interface PaymentMethod {
  paymentMethodId: string;
  userId: string;
  type: PaymentMethodType;
  displayName: string;
  lastFourDigits: string;
  expirationDate?: Date;
  isDefault: boolean;
  isActive: boolean;
  isExpired: boolean;
}

enum PaymentStatus {
  Scheduled = 'Scheduled',
  InProgress = 'InProgress',
  Completed = 'Completed',
  Failed = 'Failed',
  Cancelled = 'Cancelled'
}

enum PaymentMethodType {
  BankAccount = 'BankAccount',
  CreditCard = 'CreditCard',
  DebitCard = 'DebitCard',
  PayPal = 'PayPal',
  Other = 'Other'
}

enum RecurrencePattern {
  None = 'None',
  Weekly = 'Weekly',
  BiWeekly = 'BiWeekly',
  Monthly = 'Monthly',
  Quarterly = 'Quarterly',
  Annually = 'Annually'
}
```

### Actions
- `loadPaymentSchedules(filters)`: Fetch payment schedules
- `loadPaymentSchedule(id)`: Fetch single payment
- `schedulePayment(data)`: Create new payment schedule
- `modifyPayment(id, updates)`: Update payment schedule
- `cancelPayment(id, reason)`: Cancel payment
- `executePayment(id)`: Execute payment immediately
- `retryPayment(id)`: Retry failed payment
- `loadPaymentMethods()`: Fetch payment methods
- `addPaymentMethod(data)`: Add new payment method
- `removePaymentMethod(id)`: Remove payment method
- `setDefaultPaymentMethod(id)`: Set default method

## API Integration

### Service Methods
```typescript
class PaymentService {
  async getPaymentSchedules(params: PaymentQueryParams): Promise<PaymentResponse>;
  async getPaymentSchedule(id: string): Promise<PaymentSchedule>;
  async schedulePayment(data: SchedulePaymentRequest): Promise<PaymentSchedule>;
  async modifyPayment(id: string, updates: ModifyPaymentRequest): Promise<PaymentSchedule>;
  async cancelPayment(id: string, reason: string): Promise<void>;
  async executePayment(id: string): Promise<PaymentExecutionResult>;
  async retryPayment(id: string): Promise<PaymentExecutionResult>;
  async getPaymentHistory(params: HistoryParams): Promise<PaymentResponse>;
  async getUpcomingPayments(days: number): Promise<PaymentSchedule[]>;
  async getPaymentMethods(): Promise<PaymentMethod[]>;
  async addPaymentMethod(data: AddPaymentMethodRequest): Promise<PaymentMethod>;
  async removePaymentMethod(id: string): Promise<void>;
}
```

## User Flows

### Schedule Payment Flow
1. User navigates to "Schedule Payment"
2. Form loads with bill dropdown
3. User selects bill
4. Amount auto-populates from bill
5. User selects scheduled date (date picker opens)
6. User selects payment method
7. User optionally enables recurring payment
8. If recurring, user selects pattern
9. User adds optional notes
10. User clicks "Schedule Payment"
11. Validation runs
12. If valid, confirmation dialog appears
13. User confirms
14. API call to schedule payment
15. Success notification appears
16. User redirected to payment details page

### Cancel Payment Flow
1. User views payment details
2. User clicks "Cancel Payment"
3. Confirmation dialog appears with warning
4. User enters cancellation reason
5. User confirms cancellation
6. API call to cancel payment
7. Success notification appears
8. Payment status updates to "Cancelled"
9. Cancel button becomes disabled

### Retry Failed Payment Flow
1. User views failed payment details
2. User clicks "Retry Payment"
3. System checks payment method validity
4. If expired, prompt to update payment method
5. Confirmation dialog shows payment details
6. User confirms retry
7. API call to retry payment
8. Loading indicator appears
9. Result displayed (success or failure)
10. Payment status updates accordingly

## Real-time Updates

### WebSocket Events
- `payment_executed`: Update payment status when executed
- `payment_failed`: Update status and show notification
- `payment_cancelled`: Update status
- `payment_scheduled`: Add new payment to list

### Polling
- Poll upcoming payments every 5 minutes
- Poll payment status for in-progress payments every 30 seconds

## Notifications

### Success Notifications
- "Payment scheduled successfully"
- "Payment executed successfully"
- "Payment cancelled successfully"
- "Payment method added successfully"

### Error Notifications
- "Failed to schedule payment. Please try again."
- "Payment execution failed: [reason]"
- "Cannot cancel payment that has already been executed"
- "Payment method expired. Please update."

### Warning Notifications
- "This payment is scheduled after the bill due date"
- "Payment amount exceeds bill amount"
- "Payment method will expire before scheduled date"
- "Insufficient funds (if available from gateway)"

### Info Notifications
- "Payment will be executed on [date]"
- "Next recurring payment on [date]"
- "Payment reminder sent"

## Responsive Design

### Desktop (>1024px)
- Side-by-side layout for lists and details
- Multi-column forms
- Full calendar month view
- Expanded payment timeline

### Tablet (768px - 1024px)
- Stacked layout
- Two-column forms
- Calendar week view
- Collapsible timeline

### Mobile (<768px)
- Single column layout
- One-column forms
- Calendar agenda view
- Card-based payment list
- Bottom sheet for actions
- Swipe to cancel/modify

## Accessibility

- ARIA labels on all interactive elements
- Keyboard navigation for all features
- Screen reader announcements for status changes
- Focus management in dialogs
- High contrast mode support
- Clear error messages
- Sufficient color contrast (WCAG AA)

## Security Best Practices

- Never display full payment method numbers
- Mask sensitive information
- Require re-authentication for sensitive operations
- Session timeout for payment operations
- HTTPS only
- CSP headers
- Input sanitization
- XSS prevention

## Performance Optimization

- Virtual scrolling for payment lists
- Lazy load payment details
- Cache payment methods
- Debounce search and filters
- Optimize calendar rendering
- Minimize API calls
- Code splitting by route

## Analytics Events

- `payment_scheduled`: Track payment creation
- `payment_modified`: Track modifications
- `payment_cancelled`: Track cancellations
- `payment_retried`: Track retry attempts
- `payment_method_added`: Track payment method additions
- `calendar_view_used`: Track calendar usage
- `recurring_payment_setup`: Track recurring payment creation

## Testing Requirements

### Unit Tests
- Component rendering
- Form validation
- State management
- Utility functions
- Date calculations

### Integration Tests
- API integration
- Payment scheduling flow
- Payment cancellation flow
- Payment method management
- Real-time updates

### E2E Tests
- Complete payment scheduling flow
- Payment modification flow
- Payment cancellation flow
- Payment retry flow
- Payment method management flow

### Accessibility Tests
- Screen reader compatibility
- Keyboard navigation
- Color contrast
- ARIA labels
