# Frontend Requirements - Activity Categorization

## User Interface Components

### Category Picker
**Purpose:** Select category for time entry
**Components:**
- Searchable dropdown with icons
- Color-coded category badges
- Hierarchical category display
- "Create New Category" option
- Recently used categories section

**Behavior:**
- Filter as user types
- Show category hierarchy with indentation
- Display category colors
- Quick access to top 5 recent categories

### Category Manager
**Purpose:** View and manage all categories
**Components:**
- Category list with edit/delete actions
- Add category button
- Category hierarchy tree view
- Budget status indicators
- Predefined vs custom category tabs

**Behavior:**
- Drag and drop to reorganize
- Collapse/expand category groups
- Confirm before deleting (check for usage)
- Show time usage statistics per category

### Custom Category Form
**Purpose:** Create new category
**Components:**
- Category name input
- Description textarea
- Color picker
- Parent category dropdown
- Icon selector
- Save/Cancel buttons

**Behavior:**
- Validate unique name
- Preview category badge
- Suggest colors not already used
- Show icon library

### Budget Setting Interface
**Purpose:** Set time budgets for categories
**Components:**
- Category selector
- Target hours input
- Period selector (daily/weekly/monthly)
- Rationale textarea
- Current usage display
- Save button

**Behavior:**
- Calculate remaining budget
- Show budget utilization bar
- Warn if current usage already exceeds
- Display historical budget performance

### Category Distribution Chart
**Purpose:** Visualize time across categories
**Components:**
- Pie chart or donut chart
- Category legend with percentages
- Total time display
- Date range selector
- Export button

**Behavior:**
- Interactive segments (click to filter)
- Show/hide categories
- Animate transitions
- Update on date range change

### Budget Alert Banner
**Purpose:** Notify when budget exceeded
**Components:**
- Alert message with category name
- Overage amount display
- "View Details" link
- Dismiss button

**Behavior:**
- Appear at top of page
- Animate entrance
- Persist until dismissed
- Link to budget management

## User Flows

### Flow 1: Create Custom Category
1. User clicks "Add Category"
2. Category form modal opens
3. User enters name and description
4. User selects color from picker
5. User optionally selects parent category
6. User chooses icon
7. User clicks "Save"
8. System validates and creates category
9. Category appears in list
10. Success message shown

### Flow 2: Set Category Budget
1. User navigates to Budget Management
2. User clicks "Set Budget" for category
3. Budget form appears
4. User enters target hours
5. User selects period (e.g., weekly)
6. User enters rationale
7. User clicks "Save Budget"
8. System activates budget monitoring
9. Budget indicator appears on category
10. Confirmation shown

### Flow 3: Handle Budget Overage
1. User logs time entry that exceeds budget
2. System detects overage
3. Alert banner appears at top
4. User clicks "View Details"
5. Budget details modal opens
6. User sees budgeted vs actual
7. User decides action (adjust budget or behavior)
8. User dismisses alert

### Flow 4: Categorize Time Entry
1. User creates/edits time entry
2. Category picker appears
3. User searches or browses categories
4. User selects category
5. System applies category
6. Entry updates with category color
7. Category distribution updates

## UI States

### Loading States
- Categories loading: Show skeleton list
- Budget calculation: Show spinner in budget status

### Error States
- Category creation failed: Show error message in form
- Budget exceeded: Show warning banner
- Duplicate category name: Highlight field with error

### Empty States
- No custom categories: Show "Create your first category" message
- No budgets set: Show "Set a budget to track spending" CTA

## Responsive Design
- Mobile: Full-screen category picker, stacked form fields
- Tablet: Modal category manager
- Desktop: Sidebar category panel

## Accessibility
- Keyboard navigation in category picker
- Color-blind friendly palette options
- Screen reader descriptions for categories
- Focus management in modals

## Performance
- Cache category list in localStorage
- Lazy load category icons
- Debounce category search (300ms)
- Memoize category hierarchy calculations

## Validation Rules
- Category name required, max 255 characters
- Color code must be valid hex
- Parent category cannot create circular reference
- Target hours must be > 0
- Budget period must be daily, weekly, or monthly
