# Expense Tracking - Frontend Requirements

## Pages/Views

### 1. Expense Dashboard
**Route**: `/projects/{id}/expenses`

**Components**:
- Budget overview cards
- Spent vs budgeted chart
- Expense category breakdown (pie/donut)
- Recent expenses list
- "Add Expense" button
- Budget alert banner
- Spending trend graph

**Budget Cards**:
- Total budget
- Total spent
- Remaining
- Percentage used
- Status indicator

### 2. Add Expense
**Modal or Route**: `/projects/{id}/expenses/new`

**Form Fields**:
- Amount (currency input, required)
- Category (dropdown, required)
- Date (date picker, required)
- Vendor (text input)
- Description (textarea, required)
- Receipt (file upload)
- Budget category (dropdown)
- Necessity level (radio buttons)
- Tax deductible (checkbox)

**Features**:
- Receipt photo capture
- OCR for receipt scanning
- Duplicate expense detection
- Budget impact preview

### 3. Expense List
**Route**: `/projects/{id}/expenses/list`

**Components**:
- Sortable/filterable table
- Category filters
- Date range filter
- Search by vendor/description
- Export to CSV/PDF button
- Bulk actions (tag, delete)

**Table Columns**:
- Date
- Category
- Vendor
- Description
- Amount
- Receipt (icon/link)
- Actions (Edit, Delete)

### 4. Budget Manager
**Route**: `/projects/{id}/budget`

**Components**:
- Total budget input
- Category allocation table
- Alert threshold slider
- Forecast projection
- Spending recommendations

**Category Allocation**:
- Category name
- Allocated amount (editable)
- Spent amount
- Remaining
- Progress bar
- Warning indicators

### 5. Tool Inventory
**Route**: `/tools`

**Components**:
- Tool list with photos
- Search and filter
- Add tool form
- ROI calculator
- Project allocation tracker

**Tool Card**:
- Tool name and photo
- Purchase date and cost
- Projects used in
- Usage count
- ROI calculation
- Storage location

## Components

### BudgetGauge
- Circular progress indicator
- Color coding (green/yellow/red)
- Percentage display
- Alert threshold marker

### ExpenseChart
- Category breakdown pie chart
- Time-series spending graph
- Budget vs actual comparison
- Interactive tooltips

### ReceiptViewer
- Image preview
- PDF viewer
- Download option
- OCR text extraction display

### BudgetAlertBanner
- Prominent warning display
- Overage amount
- Suggested actions
- Dismiss option

## State Management
```javascript
{
  expenses: [],
  budget: null,
  tools: [],
  totals: {
    spent: 0,
    budgeted: 0,
    remaining: 0
  },
  alerts: [],
  filters: {
    category: 'all',
    dateRange: null,
    search: ''
  }
}
```

## Features
- Receipt scanning with OCR
- Automatic expense categorization
- Budget forecasting
- Expense trends analysis
- Tax report generation
- Multi-currency support
- Recurring expense tracking
- Vendor management

## Responsive Design
- Mobile receipt capture
- Touch-optimized charts
- Responsive tables
- Compact mobile view
- Swipe to delete expenses
