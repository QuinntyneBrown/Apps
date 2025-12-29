# Parts Management - Frontend Requirements

## Pages/Views

### 1. Parts Catalog
**Route**: `/projects/{id}/parts`

**Components**:
- Parts list with status badges
- Filter by category and status
- Search by part name/number
- "Add Part" button
- Parts statistics cards
- Sourcing progress indicator

**Parts List Columns**:
- Part name and number
- Category
- Condition (NOS/Used/Repro)
- Status (Sourced/Ordered/Received/Installed)
- Supplier
- Price
- Actions menu

### 2. Add/Edit Part
**Modal or Route**: `/projects/{id}/parts/new`

**Form Fields**:
- Part name (text)
- Part number (text)
- Category (dropdown)
- Condition (radio)
- Supplier (searchable dropdown)
- Price (currency)
- Availability (dropdown)
- Notes (textarea)
- Photos (upload)

### 3. Order Management
**Route**: `/projects/{id}/orders`

**Components**:
- Order list with delivery status
- Filter by status (Pending, Shipped, Delivered)
- Expected delivery timeline
- Tracking number links
- "Place Order" button

**Order Card**:
- Parts ordered
- Order date
- Supplier info
- Total cost
- Delivery status
- Tracking info
- Receive/Return buttons

### 4. Installation Log
**Route**: `/projects/{id}/installations`

**Components**:
- Installation timeline
- Parts installed list
- Difficulty and time statistics
- Installation photos gallery

**Installation Entry**:
- Part installed
- Installation date
- Difficulty rating
- Time spent
- Fit quality
- Helper needed checkbox
- Notes and photos

### 5. Supplier Directory
**Route**: `/suppliers`

**Components**:
- Supplier cards with ratings
- Search and filter
- Add supplier form
- Parts sourced from each
- Contact information

## Components

### PartStatusBadge
- Color-coded status indicator
- Icon for each status
- Tooltip with details

### OrderTracker
- Visual delivery timeline
- Status updates
- Estimated delivery date
- Tracking integration

### InstallationDifficultyRating
- Star or difficulty level display
- Time estimate based on difficulty
- Helper recommendation

## State Management
```javascript
{
  parts: [],
  orders: [],
  installations: [],
  suppliers: [],
  filters: {
    category: 'all',
    status: 'all',
    search: ''
  }
}
```

## Features
- Barcode scanner for part numbers
- Price tracking and alerts
- Supplier rating system
- Auto-fill from previous orders
- Integration with parts websites
- Inventory shortage alerts
