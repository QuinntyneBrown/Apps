# Income Stream Management - Frontend Requirements

## Overview
The Income Stream Management UI provides interfaces for users to create, view, update, and manage their various side hustles and income-generating activities.

## User Interface Components

### 1. Income Streams Dashboard
**Purpose**: Display overview of all active income streams

**Components**:
- Summary cards showing:
  - Total active streams count
  - Total monthly revenue across all streams
  - Most profitable stream
  - Newest stream
- Income streams list/grid view
- Quick action buttons (Add New Stream, View History)
- Filter controls (Status, Business Type, Date Range)
- Sort controls (Name, Revenue, Start Date)
- Search functionality

**Data Display**:
- Stream name with business type icon
- Current status badge (Active, Reactivated)
- Expected vs. Actual monthly revenue
- Start date and duration active
- Quick stats (total revenue, clients, invoices)
- Action menu (Edit, View Details, Close, View Reports)

**Interactions**:
- Click stream card to view details
- Toggle between list and grid view
- Filter and sort streams
- Quick add new stream button
- Export stream list to CSV/Excel

### 2. Add/Edit Income Stream Form
**Purpose**: Create new or modify existing income stream

**Form Fields**:
- Stream Name (text input, required)
- Business Type (dropdown, required):
  - Freelancing
  - Consulting
  - E-Commerce
  - Content Creation
  - Tutoring
  - Crafts & Handmade
  - Services
  - Rental Income
  - Other
- Description (textarea, optional)
- Start Date (date picker, required)
- Expected Monthly Revenue (currency input, optional)
- Tags (tag input with autocomplete)

**Validation**:
- Real-time validation on field blur
- Stream name uniqueness check
- Start date cannot be in future
- Revenue must be positive number
- Display inline error messages

**Actions**:
- Save button (creates or updates stream)
- Cancel button (discards changes)
- Auto-save draft functionality

### 3. Income Stream Details View
**Purpose**: Show comprehensive information about a single income stream

**Sections**:
- **Header**:
  - Stream name and business type
  - Status badge
  - Action buttons (Edit, Close, Generate Report)

- **Overview Stats**:
  - Start date and duration active
  - Expected vs. Actual monthly revenue
  - Total lifetime revenue
  - Total expenses
  - Net profit/loss

- **Performance Tab**:
  - Revenue trend chart (monthly)
  - Revenue vs. Expenses comparison
  - Month-over-month growth
  - Key milestones achieved

- **Clients Tab**:
  - Associated clients list
  - Client revenue breakdown
  - Active vs. lost clients

- **Activity Timeline**:
  - Recent income receipts
  - Recent expenses
  - Invoices sent/paid
  - Milestones and achievements

**Interactions**:
- Switch between tabs
- Drill down into specific metrics
- Export data and reports
- Navigate to related clients or invoices

### 4. Close Income Stream Modal
**Purpose**: Allow user to close/discontinue an income stream

**Content**:
- Confirmation message
- Closure reason field (textarea, optional)
- End date picker (defaults to today)
- Summary of stream performance:
  - Total revenue earned
  - Duration active
  - Final P&L
- Warning about impact (historical data retained)

**Actions**:
- Confirm Close button
- Cancel button
- "I'll restart this later" checkbox

### 5. Income Stream History View
**Purpose**: Display all closed income streams

**Components**:
- Closed streams list with:
  - Stream name and business type
  - Active period (start to end date)
  - Total revenue earned
  - Closure reason
  - Reactivate button
- Filter by closure date range
- Sort by end date, revenue, duration
- Search functionality

**Interactions**:
- Click to view closed stream details (read-only)
- Reactivate stream button
- Export history to CSV

### 6. Reactivate Stream Confirmation
**Purpose**: Confirm reactivation of a closed stream

**Content**:
- Stream name and original details
- Previous active period
- Confirmation message
- Option to update expected monthly revenue

**Actions**:
- Confirm Reactivate button
- Cancel button

## Page Layouts

### Income Streams List Page
```
+------------------------------------------------------------------+
| SideHustleIncomeTracker          [Search...] [Notifications] [@] |
+------------------------------------------------------------------+
| Dashboard | Income Streams | Clients | Expenses | Reports | ... |
+------------------------------------------------------------------+
|                                                                   |
| Income Streams                                  [+ Add Stream]   |
|                                                                   |
| +--------+ +--------+ +--------+ +--------+                      |
| | Active | | Total  | | Highest| | This   |                      |
| | Streams| | Revenue| | Earning| | Month  |                      |
| |   5    | | $8,450 | | $3,200 | | $9,100 |                      |
| +--------+ +--------+ +--------+ +--------+                      |
|                                                                   |
| [All ▼] [Business Type ▼] [Sort: Revenue ▼]  [Grid/List Toggle] |
|                                                                   |
| +----------------------------------------------------------+    |
| | Freelance Writing                          [Active]   ⚙  |    |
| | Content Creation • Started Jan 2024                      |    |
| | Expected: $3,000/mo | Actual: $3,200/mo                  |    |
| | Total Revenue: $38,400 | 12 months active                |    |
| +----------------------------------------------------------+    |
|                                                                   |
| +----------------------------------------------------------+    |
| | Web Design Consulting                      [Active]   ⚙  |    |
| | Freelancing • Started Mar 2024                           |    |
| | Expected: $2,000/mo | Actual: $1,850/mo                  |    |
| | Total Revenue: $18,500 | 10 months active                |    |
| +----------------------------------------------------------+    |
|                                                                   |
| [View Closed Streams] [Export]                                  |
|                                                                   |
+------------------------------------------------------------------+
```

## State Management

### Component State
- Active income streams list
- Selected stream for details view
- Form state (add/edit mode)
- Filters and sort preferences
- Loading states
- Error states

### Global State
- Current user's income streams
- Summary statistics (cached)
- Recently viewed streams
- User preferences (view mode, default sort)

## API Integration

### Endpoints Used
- GET /api/income-streams (fetch active streams)
- GET /api/income-streams/{id} (fetch stream details)
- POST /api/income-streams (create new stream)
- PUT /api/income-streams/{id} (update stream)
- POST /api/income-streams/{id}/close (close stream)
- POST /api/income-streams/{id}/reactivate (reactivate stream)
- GET /api/income-streams/history (fetch closed streams)
- GET /api/income-streams/summary (fetch summary stats)

### Error Handling
- Display user-friendly error messages
- Handle network errors gracefully
- Retry failed requests
- Show loading states during API calls
- Validate data before submission

## Responsive Design

### Desktop (>1024px)
- Multi-column grid layout for stream cards
- Side-by-side form and preview
- Full dashboard with all summary cards
- Expanded action menus

### Tablet (768px - 1024px)
- Two-column grid layout
- Condensed summary cards
- Modal forms
- Simplified action menus

### Mobile (<768px)
- Single-column list view
- Stacked summary cards
- Full-screen modal forms
- Bottom sheet for actions
- Swipe gestures for quick actions

## Accessibility

- ARIA labels for all interactive elements
- Keyboard navigation support
- Focus management in modals
- Screen reader-friendly tables and forms
- High contrast mode support
- Proper heading hierarchy

## Performance Optimization

- Lazy loading for stream details
- Virtual scrolling for large lists
- Debounced search and filters
- Optimistic UI updates
- Cached summary statistics
- Progressive image loading for icons

## User Feedback

### Success Messages
- "Income stream created successfully"
- "Income stream updated"
- "Income stream closed"
- "Income stream reactivated"

### Confirmation Dialogs
- Confirm before closing stream
- Warn about data loss on cancel
- Confirm reactivation

### Loading States
- Skeleton screens for lists
- Spinner for form submissions
- Progress indicators for exports

### Error Messages
- "Failed to load income streams. Please try again."
- "Stream name already exists. Please choose another."
- "Unable to close stream. Please try again."
- Field-level validation errors

## Metrics and Analytics

### Track User Actions
- Streams created
- Streams closed
- Streams reactivated
- Time spent on details view
- Most used filters
- Form abandonment rate

### Performance Metrics
- Page load time
- API response time
- Form submission success rate
- Error rates
