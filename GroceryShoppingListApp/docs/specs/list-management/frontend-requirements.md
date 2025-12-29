# List Management - Frontend Requirements

## Overview
The List Management frontend provides an intuitive interface for users to create, organize, share, and manage their shopping lists.

## User Stories

### US-LM-001: Create Shopping List
**As a** user
**I want to** create a new shopping list
**So that** I can organize my shopping needs

**Acceptance Criteria:**
- User can click "New List" button from the dashboard
- User can enter a list name (required, max 255 characters)
- User can optionally add tags for categorization
- User can mark the list as recurring (daily/weekly/monthly)
- User receives confirmation when the list is created
- New list appears in the active lists view

### US-LM-002: View Shopping Lists
**As a** user
**I want to** view all my shopping lists
**So that** I can access and manage them

**Acceptance Criteria:**
- User sees a dashboard with all active lists
- Each list shows: name, item count, creation date, owner/shared status
- User can filter lists by: active, completed, archived
- User can search lists by name or tags
- Lists are paginated (20 per page)
- User can sort lists by name, date, or item count

### US-LM-003: Share Shopping List
**As a** list owner
**I want to** share my list with family or friends
**So that** we can collaborate on shopping

**Acceptance Criteria:**
- User can click "Share" button on any owned list
- User can enter email addresses or select from contacts
- User can set permission level (view/edit/admin)
- User sees who the list is currently shared with
- User can remove shared access
- Shared users receive a notification

### US-LM-004: Complete Shopping List
**As a** user
**I want to** mark my list as completed
**So that** I can track finished shopping trips

**Acceptance Criteria:**
- User can manually mark a list as complete
- List is auto-completed when all items are purchased
- Completed lists show a summary (total items, total spent)
- User can reopen a completed list if needed
- Completed lists move to "Completed" tab

### US-LM-005: Archive Shopping List
**As a** user
**I want to** archive old shopping lists
**So that** I can keep my active lists organized

**Acceptance Criteria:**
- User can archive any completed list
- User can optionally add an archive reason
- Archived lists are hidden from main view
- User can view archived lists via filter
- User can unarchive a list

## UI Components

### ListDashboard Component
**Purpose:** Main view for displaying all shopping lists

**Props:**
- `lists`: Array of shopping list objects
- `onCreateList`: Function to create new list
- `onSelectList`: Function to navigate to list details
- `filters`: Object containing filter state

**State:**
- `filterStatus`: 'active' | 'completed' | 'archived'
- `searchQuery`: string
- `sortBy`: 'name' | 'date' | 'itemCount'
- `currentPage`: number

**UI Elements:**
- Header with "New List" button
- Search bar for filtering by name/tags
- Filter tabs (Active, Completed, Archived)
- Sort dropdown
- Grid/List view toggle
- List cards showing list summary
- Pagination controls

### ListCard Component
**Purpose:** Display individual list summary

**Props:**
- `list`: Shopping list object
- `onSelect`: Function to open list
- `onShare`: Function to open share dialog
- `onComplete`: Function to mark complete
- `onArchive`: Function to archive list
- `onDelete`: Function to delete list

**UI Elements:**
- List name (clickable)
- Item count badge
- Progress indicator (items purchased / total)
- Tags chips
- Owner/Shared badge
- Action menu (share, complete, archive, delete)
- Status indicator

### CreateListModal Component
**Purpose:** Modal for creating new shopping list

**Props:**
- `isOpen`: boolean
- `onClose`: Function to close modal
- `onCreate`: Function to create list

**Form Fields:**
- List name (text input, required)
- Tags (multi-select or chip input)
- Recurring checkbox
- Recurring pattern (dropdown: daily/weekly/monthly)

**Validation:**
- List name required, 1-255 characters
- At least one tag recommended (warning, not error)

### ShareListModal Component
**Purpose:** Modal for sharing shopping list

**Props:**
- `list`: Shopping list object
- `isOpen`: boolean
- `onClose`: Function to close modal
- `onShare`: Function to share with users

**UI Elements:**
- List of currently shared users with permission levels
- Remove button for each shared user
- Add user section:
  - Email/username input with autocomplete
  - Permission level dropdown
  - Add button
- Save/Cancel buttons

**Features:**
- Email validation
- Autocomplete from user's contacts
- Permission level explanations on hover
- Confirmation before removing access

### ListCompletionSummary Component
**Purpose:** Display summary when list is completed

**Props:**
- `list`: Completed shopping list object
- `onArchive`: Function to archive
- `onReopen`: Function to reopen list

**UI Elements:**
- Completion checkmark icon
- Total items purchased count
- Total amount spent
- Completion date/time
- Option to archive or reopen
- Option to view detailed receipt

## Page Layouts

### Dashboard Page
```
┌─────────────────────────────────────────────────┐
│ Header: "My Shopping Lists"    [+ New List]    │
├─────────────────────────────────────────────────┤
│ [Search...]                     [Sort: Date ▼] │
│ [Active] [Completed] [Archived]  [Grid][List]  │
├─────────────────────────────────────────────────┤
│ ┌──────────┐ ┌──────────┐ ┌──────────┐        │
│ │ List 1   │ │ List 2   │ │ List 3   │        │
│ │ 5 items  │ │ 8 items  │ │ 3 items  │        │
│ │ 60% done │ │ 25% done │ │ 100%     │        │
│ └──────────┘ └──────────┘ └──────────┘        │
│                                                 │
│ ┌──────────┐ ┌──────────┐                     │
│ │ List 4   │ │ List 5   │                     │
│ │ 12 items │ │ 4 items  │                     │
│ │ 0% done  │ │ 75% done │                     │
│ └──────────┘ └──────────┘                     │
├─────────────────────────────────────────────────┤
│         [< Previous]  Page 1 of 3  [Next >]    │
└─────────────────────────────────────────────────┘
```

### List Details Page
```
┌─────────────────────────────────────────────────┐
│ [< Back] Grocery List - Week 1    [⋮ Actions]  │
│ Tags: [groceries] [weekly]                      │
│ Shared with: Alice, Bob                         │
├─────────────────────────────────────────────────┤
│ Progress: ████████░░ 8/10 items purchased      │
│ Budget: $45.00 / $50.00                         │
├─────────────────────────────────────────────────┤
│ [Items content from Item Management feature]    │
└─────────────────────────────────────────────────┘
```

## State Management

### Redux Store Structure
```javascript
{
  listManagement: {
    lists: {
      byId: {
        'list-1': { listId, name, ownerId, status, ... },
        'list-2': { ... }
      },
      allIds: ['list-1', 'list-2', ...],
      loading: false,
      error: null
    },
    filters: {
      status: 'active',
      searchQuery: '',
      sortBy: 'date',
      page: 1
    },
    selectedListId: 'list-1' | null,
    shares: {
      'list-1': [
        { userId, userName, permissionLevel }
      ]
    }
  }
}
```

### Actions
- `createList(listData)`
- `fetchLists(filters)`
- `fetchListById(listId)`
- `updateList(listId, updates)`
- `shareList(listId, shareData)`
- `completeList(listId)`
- `archiveList(listId, reason)`
- `deleteList(listId)`
- `setFilter(filterType, value)`
- `setSelectedList(listId)`

## Responsive Design

### Mobile (< 768px)
- Single column list view
- Hamburger menu for filters
- Swipe gestures for quick actions
- Bottom sheet for modals
- Sticky "New List" FAB button

### Tablet (768px - 1024px)
- Two column grid for list cards
- Side panel for filters
- Modal dialogs for create/share

### Desktop (> 1024px)
- Three column grid for list cards
- Persistent left sidebar for filters
- Modal dialogs for create/share
- Keyboard shortcuts support

## Accessibility Requirements

- All interactive elements keyboard navigable
- ARIA labels for screen readers
- Color contrast ratio minimum 4.5:1
- Focus indicators on all interactive elements
- Semantic HTML structure
- Alt text for all icons
- Announce dynamic content changes

## Performance Requirements

- Initial page load < 2 seconds
- List filtering/sorting < 100ms
- Smooth scrolling and animations (60fps)
- Optimistic UI updates for better perceived performance
- Lazy loading for list items
- Image optimization for any list thumbnails

## Error Handling

- Network errors: Show retry option
- Validation errors: Inline field-level feedback
- Permission errors: Clear messaging about access level
- 404 errors: Redirect to dashboard with notification
- Concurrent edit conflicts: Show merge dialog

## Notifications

- Success: "List created successfully"
- Success: "List shared with [users]"
- Success: "List completed"
- Success: "List archived"
- Info: "[User] shared a list with you"
- Warning: "This list has been deleted by the owner"
- Error: "Failed to create list. Please try again."

## Browser Support

- Chrome (latest 2 versions)
- Firefox (latest 2 versions)
- Safari (latest 2 versions)
- Edge (latest 2 versions)
- Mobile Safari (iOS 13+)
- Chrome Mobile (Android 8+)
