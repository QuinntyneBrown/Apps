# Item Management - Frontend Requirements

## Overview
The Item Management frontend provides an intuitive interface for users to add, manage, and track items within their shopping lists.

## User Stories

### US-IM-001: Add Item to List
**As a** user
**I want to** add items to my shopping list
**So that** I can track what I need to buy

**Acceptance Criteria:**
- User can click "Add Item" button or use quick-add input
- User can enter item name (required)
- User can specify quantity and unit
- User can select/enter category
- User can optionally add estimated price and notes
- Item suggestions appear as user types
- Item is added to list immediately with optimistic UI
- Duplicate items show merge prompt

### US-IM-002: Update Item Quantity
**As a** user
**I want to** adjust item quantities
**So that** I can buy the right amount

**Acceptance Criteria:**
- User can click +/- buttons to adjust quantity
- User can directly edit quantity field
- Quantity changes are saved immediately
- Quantity cannot be less than 1
- Setting quantity to 0 prompts to remove item

### US-IM-003: Mark Item as Purchased
**As a** user
**I want to** check off items as I shop
**So that** I can track my progress

**Acceptance Criteria:**
- User can tap/click checkbox to mark purchased
- Purchased items are visually distinct (strikethrough, dimmed)
- User can optionally enter actual price paid
- User can undo purchase by unchecking
- Progress bar updates automatically
- List completion triggers when all items purchased

### US-IM-004: Mark Item as Unavailable
**As a** user
**I want to** mark items as unavailable
**So that** I know what to buy elsewhere

**Acceptance Criteria:**
- User can long-press or right-click for context menu
- User can select "Mark Unavailable" option
- User can optionally add reason/note
- Unavailable items are visually distinct
- User can mark unavailable items as purchased later
- User can remove unavailable status

### US-IM-005: Remove Item from List
**As a** user
**I want to** remove items I no longer need
**So that** my list stays current

**Acceptance Criteria:**
- User can swipe to delete (mobile) or click delete button
- Confirmation prompt for removal
- Removed items show undo option (5 second timeout)
- Removed items are soft-deleted from database
- Progress bar updates after removal

### US-IM-006: Organize Items by Category
**As a** user
**I want to** see items grouped by category
**So that** I can shop more efficiently

**Acceptance Criteria:**
- Items automatically grouped by category
- Categories shown in store-layout order
- User can customize category order
- Uncategorized items shown separately
- User can collapse/expand categories
- Category totals shown (items, estimated cost)

### US-IM-007: Get Item Suggestions
**As a** user
**I want to** see item suggestions based on history
**So that** I can quickly add common items

**Acceptance Criteria:**
- Autocomplete shows as user types
- Suggestions based on personal purchase history
- Suggestions include typical price and category
- User can tap suggestion to auto-fill
- Recently used items shown prominently
- Common items for category shown

## UI Components

### QuickAddItem Component
**Purpose:** Fast item entry at top of list

**Props:**
- `listId`: UUID
- `onItemAdded`: Function callback
- `suggestions`: Array of suggested items

**State:**
- `inputValue`: string
- `showSuggestions`: boolean
- `selectedCategory`: string

**UI Elements:**
- Text input with autocomplete
- Quick quantity buttons (+1, +2, +5)
- Category selector
- Add button
- Suggestions dropdown

### ItemCard Component
**Purpose:** Display individual item in list

**Props:**
- `item`: Item object
- `onUpdate`: Function to update item
- `onPurchase`: Function to mark purchased
- `onRemove`: Function to remove item
- `editable`: boolean (based on permissions)

**State:**
- `isEditing`: boolean
- `quantity`: number
- `showDetails`: boolean

**UI Elements:**
- Checkbox for purchase status
- Item name (editable on click)
- Quantity +/- controls
- Unit selector
- Price display (estimated/actual)
- Category badge
- Notes section (collapsible)
- Action menu (edit, mark unavailable, delete)
- Swipe actions (mobile)

### CategorySection Component
**Purpose:** Group items by category

**Props:**
- `category`: Category object
- `items`: Array of items in category
- `onToggle`: Function to collapse/expand
- `isCollapsed`: boolean

**UI Elements:**
- Category header with icon and name
- Item count and total price
- Collapse/expand button
- List of ItemCard components
- Category color/badge

### PurchaseDialog Component
**Purpose:** Capture purchase details

**Props:**
- `item`: Item object
- `isOpen`: boolean
- `onConfirm`: Function to confirm purchase
- `onCancel`: Function to close dialog

**Form Fields:**
- Actual price paid (number input)
- Purchased quantity (default: full quantity)
- Store name (optional, with autocomplete)
- Notes (optional)

### BulkItemInput Component
**Purpose:** Add multiple items at once

**Props:**
- `listId`: UUID
- `onItemsAdded`: Function callback

**UI Elements:**
- Multi-line text area
- Format hints (one item per line)
- Parse button
- Preview of parsed items
- Bulk add button

### ItemSuggestions Component
**Purpose:** Display autocomplete suggestions

**Props:**
- `query`: string
- `suggestions`: Array of suggested items
- `onSelect`: Function when suggestion selected

**UI Elements:**
- List of suggestion cards
- Item name, category, average price
- Frequency indicator
- Quick add button per suggestion

## Page Layouts

### List Items View (Within List Details)
```
┌─────────────────────────────────────────────────┐
│ ← Weekly Groceries                    [⋮ Menu] │
│ Progress: ████████░░ 8/12 items (67%)          │
├─────────────────────────────────────────────────┤
│ 🔍 Add item...              [Category ▼] [+]   │
├─────────────────────────────────────────────────┤
│ 🥬 Produce (3 items • $12.50)         [−]      │
│ ┌─────────────────────────────────────────┐    │
│ │ ☐ Apples        2 lbs    $3.99         │    │
│ │                          [− 2 +]  [⋮]   │    │
│ ├─────────────────────────────────────────┤    │
│ │ ☑ Lettuce       1 head   $2.49  ✓      │    │
│ │                          [− 1 +]  [⋮]   │    │
│ ├─────────────────────────────────────────┤    │
│ │ ☐ Tomatoes      5 count  $6.02         │    │
│ │                          [− 5 +]  [⋮]   │    │
│ └─────────────────────────────────────────┘    │
│                                                 │
│ 🥛 Dairy (2 items • $8.50)            [−]      │
│ ┌─────────────────────────────────────────┐    │
│ │ ☑ Milk          1 gallon $3.99  ✓      │    │
│ │ ☐ Cheese        8 oz     $4.51         │    │
│ └─────────────────────────────────────────┘    │
│                                                 │
│ 🥩 Meat (1 item • $12.00)             [−]      │
│ ...                                             │
└─────────────────────────────────────────────────┘
```

### Mobile Item View
```
┌─────────────────────────────────┐
│ ← Weekly Groceries        ⋮     │
│ ████████░░ 8/12 (67%)           │
├─────────────────────────────────┤
│ 🔍 Add item...            [+]   │
├─────────────────────────────────┤
│ 🥬 Produce (3) • $12.50    ▼    │
│                                 │
│ ┌─────────────────────────────┐ │
│ │ ☐ Apples                    │ │
│ │   2 lbs • $3.99             │ │
│ │   [− 2 +]                   │ │
│ ├─────────────────────────────┤ │
│ │ ☑ Lettuce     ✓             │ │
│ │   1 head • $2.49            │ │
│ │   [− 1 +]                   │ │
│ └─────────────────────────────┘ │
│                                 │
│ 🥛 Dairy (2) • $8.50       ▼    │
│ ...                             │
└─────────────────────────────────┘
```

### Purchase Confirmation Dialog
```
┌─────────────────────────────────┐
│ Mark as Purchased          ✕    │
├─────────────────────────────────┤
│ Apples (2 lbs)                  │
│                                 │
│ Actual Price Paid               │
│ ┌─────────────────────────┐     │
│ │ $ 4.25                  │     │
│ └─────────────────────────┘     │
│ Estimated: $3.99                │
│                                 │
│ Store (optional)                │
│ ┌─────────────────────────┐     │
│ │ Walmart                 │     │
│ └─────────────────────────┘     │
│                                 │
│ [Quick Mark]  [Mark with Price] │
└─────────────────────────────────┘
```

## State Management

### Redux Store Structure
```javascript
{
  itemManagement: {
    items: {
      byId: {
        'item-1': {
          itemId, name, quantity, unit,
          category, status, estimatedPrice,
          actualPrice, notes, addedAt, purchasedAt
        }
      },
      allIds: ['item-1', 'item-2', ...],
      byList: {
        'list-1': ['item-1', 'item-2', ...]
      },
      loading: false,
      error: null
    },
    categories: {
      all: [...],
      order: [...],
      collapsed: {
        'produce': false,
        'dairy': true
      }
    },
    suggestions: {
      items: [...],
      loading: false
    },
    filters: {
      showPurchased: true,
      showUnavailable: true,
      categoryFilter: null
    }
  }
}
```

### Actions
- `addItem(listId, itemData)`
- `addMultipleItems(listId, items)`
- `updateItem(itemId, updates)`
- `updateQuantity(itemId, quantity)`
- `markPurchased(itemId, purchaseData)`
- `markUnavailable(itemId, reason)`
- `undoPurchase(itemId)`
- `removeItem(itemId, reason)`
- `fetchItems(listId)`
- `fetchSuggestions(query)`
- `toggleCategoryCollapse(category)`
- `reorderItems(listId, newOrder)`

## Responsive Design

### Mobile (< 768px)
- Full-width item cards
- Swipe gestures for actions
- Bottom sheet for item details
- FAB for quick add
- Collapsible categories by default
- Large tap targets for checkboxes

### Tablet (768px - 1024px)
- Two-column layout if space permits
- Side drawer for category filters
- Modal dialogs for forms
- Hybrid touch and mouse support

### Desktop (> 1024px)
- Fixed category sidebar (optional)
- Hover actions on items
- Keyboard shortcuts
- Inline editing for all fields
- Right-click context menus

## Accessibility Requirements

- Checkbox ARIA labels with item name
- Keyboard navigation through items
- Screen reader announcements for actions
- High contrast mode support
- Focus indicators on all interactive elements
- Semantic HTML for item structure

## Performance Requirements

- Virtual scrolling for lists > 50 items
- Optimistic UI updates (no waiting for server)
- Debounced autocomplete (300ms)
- Lazy load item suggestions
- Cache item history locally
- Background sync for offline changes

## Offline Support

- Queue item additions/updates when offline
- Show offline indicator
- Sync when connection restored
- Handle conflicts (last-write-wins or manual resolution)
- Cache list items for offline viewing

## Animations & Feedback

- Smooth checkbox animation
- Strikethrough animation for purchased items
- Slide-in for new items
- Fade-out for removed items (with undo)
- Quantity button press feedback
- Progress bar smooth transitions
- Category expand/collapse animation

## Error Handling

- Network errors: Show retry button
- Duplicate items: Show merge dialog
- Permission errors: Disable editing, show message
- Validation errors: Inline field feedback
- Sync conflicts: Show resolution dialog

## Notifications & Feedback

- Success: "Item added to list"
- Success: "Item marked as purchased"
- Info: "Item removed" (with undo)
- Warning: "Item already in list. Increase quantity?"
- Error: "Failed to add item. Please try again."
- Collaborative: "[User] added [Item] to list"
- Collaborative: "[User] purchased [Item]"

## Keyboard Shortcuts (Desktop)

- `A` or `N`: Add new item (focus quick-add)
- `Space`: Toggle selected item purchased
- `Delete`: Remove selected item
- `↑/↓`: Navigate items
- `Enter`: Edit selected item
- `Esc`: Cancel editing/close dialog
- `Ctrl+F`: Focus search/filter

## Gestures (Mobile)

- Swipe right: Mark as purchased
- Swipe left: Delete item
- Long press: Show context menu
- Pull down: Refresh list
- Pinch category header: Collapse/expand
