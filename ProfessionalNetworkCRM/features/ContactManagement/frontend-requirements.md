# Frontend Requirements - Contact Management

## User Interface Components

### Contact List View
**Route**: `/contacts`

**Layout**:
- Header with search bar and filter controls
- Action buttons: Add Contact, Import Contacts, Export
- List/Grid toggle view option
- Sortable columns (name, company, last interaction, relationship strength)
- Pagination controls
- Quick actions menu per contact (edit, view, deactivate)

**Filters**:
- Search by name, company, title
- Filter by tags (multi-select dropdown)
- Filter by relationship type (dropdown)
- Filter by industry (dropdown)
- Active/Inactive toggle
- Advanced filters (date range, interaction count, relationship strength)

**List Item Display**:
- Contact photo/avatar
- Name (clickable to detail view)
- Title and Company
- Relationship type badge
- Relationship strength indicator (visual gauge)
- Tags (first 3 visible, +N more)
- Last interaction date (with time ago format)
- Quick action buttons (call, email, view)

**State Management**:
- Contacts list (paginated)
- Current page and page size
- Active filters and search query
- Sort column and direction
- Selected contacts for bulk actions
- Loading and error states

### Contact Detail View
**Route**: `/contacts/{id}`

**Sections**:
1. **Header Card**:
   - Large contact photo
   - Name (editable inline)
   - Title and Company (editable inline)
   - LinkedIn profile link button
   - Contact methods (email, phone) with click-to-action
   - Edit, Deactivate, Delete buttons

2. **Key Information Card**:
   - Industry
   - Relationship type with strength indicator
   - Met date and location
   - Connection source
   - Tags (with add/remove capability)
   - Mutual connections (clickable)

3. **Tabs**:
   - **Overview**: Summary stats and relationship insights
   - **Interactions**: Timeline of all interactions (from InteractionTracking)
   - **Follow-ups**: Scheduled and completed follow-ups
   - **Notes**: All notes and conversation topics
   - **Opportunities**: Related opportunities
   - **Value Exchange**: Value given/received history

**Actions**:
- Edit contact information
- Add/remove tags
- Update relationship type
- Schedule follow-up
- Log interaction
- Add note
- Request introduction
- Deactivate/Reactivate

**State Management**:
- Contact details
- Interaction history
- Follow-ups list
- Notes list
- Related data (opportunities, value exchanges)
- Edit mode toggles
- Dirty state tracking

### Add/Edit Contact Form
**Route**: `/contacts/new` or `/contacts/{id}/edit`

**Form Fields**:
- Name* (required)
- Title
- Company
- Industry (dropdown with search)
- Email
- Phone
- LinkedIn URL
- Met Date (date picker)
- Met Location
- Connection Source (dropdown: Conference, Referral, LinkedIn, College, Previous Job, Other)
- Relationship Type (dropdown)
- Initial Tags (multi-select with autocomplete)
- Initial Notes (textarea)

**Validation**:
- Name required, 2-200 characters
- Email format validation
- LinkedIn URL format validation
- Phone number format validation
- Met Date cannot be in future

**Actions**:
- Save (with loading state)
- Save and Add Another
- Cancel (with unsaved changes warning)

**Features**:
- Auto-save draft to local storage
- LinkedIn profile import (fetch data from LinkedIn URL)
- Duplicate detection warning (based on email)
- Company autocomplete from existing contacts

**State Management**:
- Form data (controlled inputs)
- Validation errors
- Dirty/pristine state
- Save in progress
- Duplicate warnings

### Contact Import Modal
**Trigger**: Import Contacts button

**Steps**:
1. **Source Selection**:
   - LinkedIn import
   - Google Contacts import
   - CSV upload
   - Manual entry

2. **Data Upload** (CSV):
   - Drag-and-drop file upload
   - File format instructions
   - Column mapping interface
   - Preview of imported data

3. **Options**:
   - Auto-tag imported contacts
   - Import tag name
   - Skip duplicates checkbox
   - Merge duplicates option

4. **Progress**:
   - Import progress bar
   - Contacts imported count
   - Errors/warnings list
   - Cancel import option

5. **Completion**:
   - Success summary
   - Contacts imported count
   - View imported contacts button
   - Import another file

**State Management**:
- Import step
- Selected source
- File upload state
- Import progress
- Import results

### Contact Tags Management
**Location**: Embedded in contact detail and list views

**Features**:
- Tag input with autocomplete from existing tags
- Tag categories displayed with color coding
- Click tag to filter all contacts by that tag
- Remove tag with X button
- Tag usage count tooltip
- Bulk tag operations in list view

**Tag Categories**:
- Industry (blue)
- Skill (green)
- Location (orange)
- Interest (purple)
- Custom (gray)

### Contact Merge Interface
**Route**: `/contacts/merge`

**Layout**:
- Side-by-side comparison of duplicate contacts
- Field-by-field selection (choose primary or duplicate value)
- Merge preview
- Confirm merge button
- Cancel button

**Features**:
- Highlight differences between contacts
- Show which data will be preserved
- Warning about irreversible action
- Merge interaction history from both

### Bulk Actions
**Location**: Contact list view

**Actions**:
- Add tags to selected contacts
- Change relationship type
- Deactivate selected contacts
- Export selected contacts
- Create segment from selected

**UI**:
- Checkbox selection in list
- Select all (page/all) options
- Bulk action toolbar appears when items selected
- Confirmation modals for destructive actions

## Mobile Responsive Design

### Contact List (Mobile)
- Card-based layout (stacked)
- Swipe actions (edit, deactivate, view)
- Floating Add button
- Pull-to-refresh
- Infinite scroll instead of pagination
- Compact filter drawer

### Contact Detail (Mobile)
- Sticky header with contact name
- Collapsible sections
- Bottom navigation for actions
- Swipeable tabs

### Quick Add Contact (Mobile)
- Minimal form with required fields only
- Voice-to-text for notes
- Photo capture from camera
- QR code business card scan

## State Management Architecture

### Redux/State Slices
```typescript
interface ContactsState {
  contacts: {
    items: Contact[];
    totalCount: number;
    loading: boolean;
    error: string | null;
  };
  currentContact: {
    data: Contact | null;
    loading: boolean;
    error: string | null;
  };
  filters: {
    search: string;
    tags: string[];
    relationshipType: string;
    industry: string;
    active: boolean;
  };
  pagination: {
    page: number;
    pageSize: number;
  };
  sort: {
    field: string;
    order: 'asc' | 'desc';
  };
  selectedIds: string[];
  tags: Tag[];
}
```

### API Integration
- RTK Query for contact CRUD operations
- Optimistic updates for better UX
- Cache invalidation on mutations
- Real-time updates via WebSocket for multi-device sync

## User Experience Features

### Search & Filtering
- Debounced search (300ms)
- Instant filter application
- Clear all filters button
- Save filter presets
- Search highlighting in results
- Recent searches dropdown

### Performance Optimizations
- Virtual scrolling for large lists
- Lazy loading of contact details
- Image lazy loading and optimization
- Skeleton screens during load
- Prefetch on hover for instant navigation

### Accessibility
- ARIA labels for all interactive elements
- Keyboard navigation support
- Focus management in modals
- Screen reader announcements for actions
- High contrast mode support

### Error Handling
- Inline validation errors
- Toast notifications for actions
- Retry failed requests
- Offline mode indicators
- Graceful degradation

### Empty States
- No contacts yet: onboarding with import options
- No search results: suggestions to modify filters
- No tags: prompt to add tags for organization

### Keyboard Shortcuts
- `N`: New contact
- `S`: Focus search
- `F`: Toggle filters
- `Esc`: Close modals
- `Enter`: Save forms
- `Arrow keys`: Navigate list

## Analytics & Tracking

### Events to Track
- Contact created
- Contact viewed
- Contact edited
- Contact searched
- Filter applied
- Tag added/removed
- Import completed
- Bulk action performed

### Metrics
- Time to create contact
- Search usage frequency
- Most used filters
- Tag adoption rate
- Import source popularity
