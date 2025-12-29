# Frontend Requirements: Expense Management

## Overview
The Expense Management frontend provides an intuitive interface for pet owners to track expenses, manage insurance claims, and view financial insights for their pets.

## User Stories

### Expense Tracking
1. **As a pet owner**, I want to quickly log a pet expense so I can track my spending
2. **As a pet owner**, I want to categorize expenses so I can see spending patterns
3. **As a pet owner**, I want to attach receipt photos so I have proof of purchase
4. **As a pet owner**, I want to view expense history so I can track spending over time
5. **As a pet owner**, I want to see spending summaries by category so I can budget better
6. **As a pet owner**, I want to tag expenses so I can organize them my way
7. **As a pet owner**, I want to edit or delete expenses so I can correct mistakes

### Insurance Claims
8. **As a pet owner**, I want to create an insurance claim from eligible expenses
9. **As a pet owner**, I want to track claim status so I know when to expect reimbursement
10. **As a pet owner**, I want to receive notifications when my claim is settled
11. **As a pet owner**, I want to see which expenses are claimable
12. **As a pet owner**, I want to upload supporting documents for claims
13. **As a pet owner**, I want to see my claim history

### Analytics & Reporting
14. **As a pet owner**, I want to see monthly spending trends
15. **As a pet owner**, I want to compare expenses across my pets
16. **As a pet owner**, I want to export expense data for tax purposes
17. **As a pet owner**, I want to see my total out-of-pocket costs after reimbursements

## Page Structure

### 1. Expense Dashboard
**Route**: `/pets/{petId}/expenses`

**Components**:
- ExpenseSummaryCards (total spent, avg per month, pending claims)
- ExpenseChart (monthly trend line chart)
- CategoryBreakdown (pie chart)
- RecentExpensesList (table with last 10 expenses)
- QuickAddExpenseButton (floating action button)

**Features**:
- Date range selector (This Month, Last 3 Months, This Year, Custom)
- Pet selector dropdown (if user has multiple pets)
- Export button (CSV, PDF)
- Filter by category, date range, claim status

### 2. Add/Edit Expense Modal
**Trigger**: Click "Add Expense" button or edit icon

**Form Fields**:
- Pet selector (dropdown, required)
- Amount (number input, required)
- Currency (dropdown, default USD)
- Category (dropdown, required)
- Description (textarea, required)
- Date (date picker, required)
- Vendor (text input, required)
- Payment method (dropdown, required)
- Receipt upload (file input, optional)
- Mark as insurable (checkbox)
- Tags (multi-select/tag input)

**Validation**:
- Amount must be positive
- Date cannot be in future
- All required fields must be filled
- Receipt file must be < 10MB, accepted formats: PDF, JPG, PNG

**Actions**:
- Save button (primary)
- Cancel button (secondary)
- Save & Add Another button

### 3. Expense List View
**Route**: `/pets/{petId}/expenses/list`

**Components**:
- SearchBar (search by description, vendor)
- FilterPanel (category, date range, claim status, tags)
- SortControls (date, amount, category)
- ExpenseTable with columns:
  - Date
  - Category (with icon)
  - Description
  - Vendor
  - Amount
  - Payment Method
  - Claim Status (badge)
  - Actions (view, edit, delete, add to claim)
- Pagination controls

**Features**:
- Bulk selection for creating claims
- Bulk delete (with confirmation)
- Inline editing for quick updates
- Quick view panel (slide-in on row click)
- Receipt preview on hover

### 4. Expense Detail View
**Route**: `/pets/{petId}/expenses/{expenseId}`

**Sections**:
- Expense Information Card
  - Category badge with icon
  - Amount (large, prominent)
  - Description
  - Date, vendor, payment method
  - Tags (clickable)
  - Claim status badge
- Receipt Display
  - Image/PDF viewer
  - Download button
  - Replace receipt button
- Claim Information (if associated with claim)
  - Claim ID (link to claim detail)
  - Claim status
  - Approved/denied amount
- Action Buttons
  - Edit expense
  - Delete expense
  - Add to claim (if not claimed)
  - Remove from claim (if in draft claim)

### 5. Create Insurance Claim Flow
**Route**: `/pets/{petId}/claims/new`

**Step 1: Select Expenses**
- Filter expenses by "insurable" and "not claimed"
- Checkbox selection
- Show total claim amount
- Continue button

**Step 2: Claim Details**
- Policy number (text input)
- Claim type (dropdown)
- Insurance provider (text input or dropdown)
- Contact email (email input)
- Notes (textarea)

**Step 3: Upload Documents**
- Drag & drop file upload
- Support multiple files
- File preview with remove option
- File type/size validation

**Step 4: Review & Submit**
- Summary of selected expenses
- Claim details review
- Total claim amount (prominent)
- Edit buttons for each section
- Submit claim button
- Save as draft button

### 6. Claims Dashboard
**Route**: `/pets/{petId}/claims`

**Components**:
- ClaimSummaryCards (total claims, pending, approved, denied)
- ClaimStatusChart (donut chart by status)
- RecentClaimsList (table)
- Create New Claim button

**Filters**:
- Status filter (all, draft, submitted, under review, settled)
- Date range
- Insurance provider

### 7. Claim Detail View
**Route**: `/pets/{petId}/claims/{claimId}`

**Sections**:
- Claim Status Header
  - Status badge (large, color-coded)
  - Claim ID
  - Submission date
  - Settlement date (if settled)
- Claim Information Card
  - Policy number
  - Claim type
  - Insurance provider
  - Contact email
  - Total claim amount
  - Approved amount (if settled)
  - Denied amount (if settled)
- Timeline Component
  - Claim created
  - Documents uploaded
  - Claim submitted
  - Under review
  - Settled
- Expense List
  - Table of associated expenses
  - Individual approval status (if settled)
- Documents Section
  - List of uploaded documents
  - Preview and download buttons
- Settlement Details (if settled)
  - Settlement status
  - Reimbursement method
  - Estimated payment date
  - Denial reason (if applicable)
  - Adjustment details
- Action Buttons
  - Edit claim (if draft)
  - Submit claim (if draft)
  - Add expenses (if draft)
  - Delete claim (if draft)

### 8. Analytics & Reports Page
**Route**: `/pets/{petId}/expenses/analytics`

**Sections**:
- Date Range Selector
- Total Spending Overview
  - Total spent
  - Total reimbursed
  - Net out-of-pocket
- Spending Trends
  - Monthly bar chart
  - Category trend lines
- Category Breakdown
  - Pie chart
  - Table with percentages
- Top Vendors
  - List of most frequent vendors
  - Total spent per vendor
- Insurance Summary
  - Claims filed vs settled
  - Average approval rate
  - Total reimbursed
- Export Options
  - Export to CSV
  - Export to PDF report
  - Email report

## Component Specifications

### ExpenseCard Component
**Props**:
- expense: ExpenseModel
- onEdit: Function
- onDelete: Function
- onViewDetail: Function
- compact: boolean (optional)

**Display**:
```
+----------------------------------+
| [Icon] Category      $Amount     |
| Description                      |
| Vendor • Date • [Claim Badge]    |
| [Tags...]                        |
+----------------------------------+
```

### ClaimStatusBadge Component
**Props**:
- status: ClaimStatus
- size: 'small' | 'medium' | 'large'

**Colors**:
- Draft: Gray (#6B7280)
- Submitted: Blue (#3B82F6)
- Under Review: Yellow (#F59E0B)
- Approved: Green (#10B981)
- Denied: Red (#EF4444)
- Partially Approved: Orange (#F97316)
- Paid: Purple (#8B5CF6)

### CategoryIcon Component
**Props**:
- category: ExpenseCategory
- size: number

**Icon Mapping**:
- VeterinaryCare: Medical cross icon
- Food: Food bowl icon
- Grooming: Scissors icon
- Boarding: House icon
- Training: Book icon
- Supplies: Shopping bag icon
- Insurance: Shield icon
- Other: Circle icon

### ExpenseSummaryCard Component
**Props**:
- title: string
- amount: number
- currency: string
- change: number (percentage change)
- icon: ReactNode
- color: string

**Display**:
```
+-------------------------+
| [Icon]           [Trend]|
| Title                   |
| $Amount                 |
| +X% from last period    |
+-------------------------+
```

### FileUploadZone Component
**Props**:
- accept: string (file types)
- maxSize: number (bytes)
- multiple: boolean
- onUpload: Function
- onError: Function

**Features**:
- Drag and drop support
- Click to browse
- File preview thumbnails
- Progress indicator
- Error messages
- Remove file button

### ExpenseChart Component
**Props**:
- expenses: ExpenseModel[]
- chartType: 'line' | 'bar' | 'pie'
- groupBy: 'month' | 'category' | 'vendor'
- height: number

**Chart Library**: Recharts or Chart.js

## State Management

### Global State (Redux/Context)
```typescript
interface ExpenseState {
  expenses: {
    byId: Record<string, Expense>;
    allIds: string[];
    selectedPetId: string | null;
    filters: ExpenseFilters;
    sortBy: SortOptions;
    loading: boolean;
    error: string | null;
  };
  claims: {
    byId: Record<string, InsuranceClaim>;
    allIds: string[];
    draftClaim: DraftClaim | null;
    loading: boolean;
    error: string | null;
  };
  ui: {
    showAddExpenseModal: boolean;
    showCreateClaimModal: boolean;
    selectedExpenseId: string | null;
    selectedClaimId: string | null;
  };
}
```

### Actions
- `fetchExpenses(petId, filters)`
- `addExpense(expense)`
- `updateExpense(expenseId, updates)`
- `deleteExpense(expenseId)`
- `fetchClaims(petId, filters)`
- `createClaim(claim)`
- `submitClaim(claimId)`
- `fetchClaimDetail(claimId)`
- `setFilters(filters)`
- `setSortBy(sortOptions)`

## API Integration

### API Client
```typescript
class ExpenseAPI {
  async getExpenses(petId: string, filters?: ExpenseFilters): Promise<Expense[]>
  async getExpense(expenseId: string): Promise<Expense>
  async createExpense(expense: CreateExpenseRequest): Promise<Expense>
  async updateExpense(expenseId: string, updates: UpdateExpenseRequest): Promise<Expense>
  async deleteExpense(expenseId: string): Promise<void>
  async uploadReceipt(expenseId: string, file: File): Promise<string>
  async getExpenseSummary(petId: string, year: number): Promise<ExpenseSummary>
}

class ClaimAPI {
  async getClaims(petId: string, filters?: ClaimFilters): Promise<InsuranceClaim[]>
  async getClaim(claimId: string): Promise<InsuranceClaim>
  async createClaim(claim: CreateClaimRequest): Promise<InsuranceClaim>
  async submitClaim(claimId: string): Promise<InsuranceClaim>
  async uploadDocument(claimId: string, file: File): Promise<string>
}
```

### Real-time Updates (SignalR/WebSocket)
Subscribe to events for real-time UI updates:
- `expense.created` - Refresh expense list
- `expense.updated` - Update expense in list
- `expense.deleted` - Remove expense from list
- `claim.submitted` - Update claim status
- `claim.settled` - Show notification, update claim detail

## Validation Rules

### Client-Side Validation
- All required fields must be filled before submission
- Amount must be positive number with max 2 decimal places
- Date must be valid and not in future
- Email must be valid format
- File size must not exceed limits
- File type must be in accepted list

### Error Handling
- Display field-level errors inline
- Display form-level errors in alert banner
- Show API errors in toast notifications
- Provide clear, actionable error messages
- Offer retry option for failed operations

## Responsive Design

### Mobile (< 768px)
- Stack cards vertically
- Collapse filter panel into drawer
- Simplify table to card list view
- Bottom sheet for modals
- Floating action button for add expense
- Horizontal scroll for wide tables

### Tablet (768px - 1024px)
- 2-column grid for summary cards
- Side panel for filters
- Modal dialogs at 80% width
- Condensed table columns

### Desktop (> 1024px)
- 3-4 column grid for summary cards
- Sidebar filter panel
- Modal dialogs at fixed 600-800px width
- Full table with all columns
- Multi-panel layouts

## Accessibility (WCAG 2.1 AA)

### Requirements
- All interactive elements keyboard accessible
- Focus indicators on all focusable elements
- ARIA labels for icon buttons
- Screen reader announcements for dynamic content
- Sufficient color contrast (4.5:1 for text)
- Form labels associated with inputs
- Error messages announced to screen readers
- Skip links for navigation
- Alt text for images/icons

### Keyboard Navigation
- Tab through interactive elements
- Enter/Space to activate buttons
- Escape to close modals
- Arrow keys for dropdown navigation
- Ctrl+S to save forms (where applicable)

## Performance Requirements

### Initial Load
- First Contentful Paint: < 1.5s
- Time to Interactive: < 3s
- Bundle size: < 250KB (gzipped)

### Runtime Performance
- List rendering: 60fps for scrolling
- Chart animations: 60fps
- Modal open/close: < 200ms
- Form submission feedback: < 100ms

### Optimization Strategies
- Lazy load routes
- Virtual scrolling for long lists
- Image lazy loading
- Debounce search/filter inputs
- Memoize expensive calculations
- Code splitting by route
- Cache API responses (5 minutes)

## Browser Support

### Supported Browsers
- Chrome/Edge (last 2 versions)
- Firefox (last 2 versions)
- Safari (last 2 versions)
- Mobile Safari iOS 13+
- Chrome Android (last 2 versions)

### Progressive Enhancement
- Core functionality works without JavaScript
- Graceful degradation for older browsers
- Polyfills for missing features

## Security

### Input Sanitization
- Sanitize all user inputs to prevent XSS
- Validate file uploads before preview
- Encode data before rendering

### Authentication
- Require valid JWT token for all API calls
- Auto-redirect to login if token expired
- Refresh token before expiration

### Data Protection
- No sensitive data in localStorage (use sessionStorage)
- Clear sensitive data on logout
- HTTPS only for all requests
- Receipt URLs must be pre-signed

## Testing Requirements

### Unit Tests
- Test all components with Jest + React Testing Library
- Test state management actions/reducers
- Test utility functions
- Target: 80% code coverage

### Integration Tests
- Test complete user flows (add expense, create claim)
- Test form validation
- Test API integration with mock server
- Test real-time updates

### E2E Tests (Cypress/Playwright)
- Test critical paths:
  1. User logs in, adds expense, views dashboard
  2. User creates claim from expenses
  3. User submits claim
  4. User views claim status
- Test responsive layouts
- Test accessibility

### Visual Regression Tests
- Screenshot comparison for key pages
- Test across different viewport sizes

## UI Design Tokens

### Colors
```css
--primary: #3B82F6;
--primary-dark: #2563EB;
--primary-light: #60A5FA;
--secondary: #8B5CF6;
--success: #10B981;
--warning: #F59E0B;
--error: #EF4444;
--info: #3B82F6;

--gray-50: #F9FAFB;
--gray-100: #F3F4F6;
--gray-200: #E5E7EB;
--gray-300: #D1D5DB;
--gray-500: #6B7280;
--gray-700: #374151;
--gray-900: #111827;

--text-primary: #111827;
--text-secondary: #6B7280;
--text-disabled: #9CA3AF;

--bg-primary: #FFFFFF;
--bg-secondary: #F9FAFB;
--bg-tertiary: #F3F4F6;
```

### Typography
```css
--font-family: 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;

--text-xs: 0.75rem;    /* 12px */
--text-sm: 0.875rem;   /* 14px */
--text-base: 1rem;     /* 16px */
--text-lg: 1.125rem;   /* 18px */
--text-xl: 1.25rem;    /* 20px */
--text-2xl: 1.5rem;    /* 24px */
--text-3xl: 1.875rem;  /* 30px */
--text-4xl: 2.25rem;   /* 36px */

--font-normal: 400;
--font-medium: 500;
--font-semibold: 600;
--font-bold: 700;

--line-height-tight: 1.25;
--line-height-normal: 1.5;
--line-height-relaxed: 1.75;
```

### Spacing
```css
--spacing-1: 0.25rem;  /* 4px */
--spacing-2: 0.5rem;   /* 8px */
--spacing-3: 0.75rem;  /* 12px */
--spacing-4: 1rem;     /* 16px */
--spacing-5: 1.25rem;  /* 20px */
--spacing-6: 1.5rem;   /* 24px */
--spacing-8: 2rem;     /* 32px */
--spacing-10: 2.5rem;  /* 40px */
--spacing-12: 3rem;    /* 48px */
--spacing-16: 4rem;    /* 64px */
```

### Border Radius
```css
--radius-sm: 0.25rem;  /* 4px */
--radius-md: 0.375rem; /* 6px */
--radius-lg: 0.5rem;   /* 8px */
--radius-xl: 0.75rem;  /* 12px */
--radius-full: 9999px;
```

### Shadows
```css
--shadow-sm: 0 1px 2px 0 rgba(0, 0, 0, 0.05);
--shadow-md: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
--shadow-lg: 0 10px 15px -3px rgba(0, 0, 0, 0.1);
--shadow-xl: 0 20px 25px -5px rgba(0, 0, 0, 0.1);
```

## Internationalization (i18n)

### Supported Languages (Initial)
- English (en-US)
- Spanish (es-ES) - Future

### Translation Keys
```json
{
  "expenses.title": "Expense Management",
  "expenses.add": "Add Expense",
  "expenses.edit": "Edit Expense",
  "expenses.delete": "Delete Expense",
  "expenses.total": "Total Expenses",
  "expenses.category.veterinaryCare": "Veterinary Care",
  "expenses.category.food": "Food",
  "claims.title": "Insurance Claims",
  "claims.create": "Create Claim",
  "claims.submit": "Submit Claim",
  "claims.status.draft": "Draft",
  "claims.status.submitted": "Submitted"
}
```

### Number/Date Formatting
- Use locale-aware formatting for currency
- Format dates according to user's locale
- Support multiple currencies

## Analytics & Tracking

### Events to Track
- Page views (dashboard, expense list, claim detail)
- User actions (add expense, create claim, submit claim)
- Form submissions (success/failure)
- Filter/search usage
- Export actions
- Error occurrences

### Metrics Dashboard
- Daily active users
- Expenses created per day
- Claims submitted per day
- Average time to create expense
- Most used categories
- Conversion rate (expenses to claims)

## Documentation

### User Documentation
- Getting Started Guide
- How to Add an Expense
- How to File an Insurance Claim
- Understanding Your Expense Dashboard
- FAQ

### Developer Documentation
- Component API documentation (Storybook)
- State management guide
- API integration guide
- Testing guide
- Deployment guide
