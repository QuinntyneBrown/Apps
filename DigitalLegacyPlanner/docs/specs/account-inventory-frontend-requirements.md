# Account Inventory - Frontend Requirements

## Overview
User interface for managing digital account inventory, categorization, and value assessment.

## Pages & Components

### Account Inventory Dashboard
**Route**: `/accounts`

**Purpose**: Central hub for viewing and managing all digital accounts

**Components**:
- Account list with filtering and sorting
- Category navigation sidebar
- Quick stats (total accounts, by category, completion status)
- Add account button
- Search and filter controls

**Features**:
- Grid/List view toggle
- Sort by: name, date added, priority, value
- Filter by: category, type, priority
- Bulk selection for batch operations
- Export account list

### Add Account Form
**Route**: `/accounts/new`
**Modal**: `<AddAccountModal />`

**Purpose**: Register a new digital account

**Form Fields**:
- Service Name (required, text input with autocomplete)
- Account Type (required, dropdown: Social Media, Financial, Email, Storage, etc.)
- Importance Level (required, radio: Critical, High, Medium, Low)
- Username/Email (required, text input)
- Notes (optional, textarea)

**Features**:
- Service autocomplete from known services database
- Service logo display
- Template selection for common platforms
- Save and add another option
- Progress indicator

**Validation**:
- Required field validation
- Duplicate account detection
- Service name character limit (200)

### Account Details Page
**Route**: `/accounts/:id`

**Purpose**: View and edit complete account information

**Sections**:
1. **Basic Information**
   - Service name and logo
   - Account type badge
   - Username
   - Importance level indicator
   - Edit button

2. **Categorization**
   - Category tags
   - Legacy priority badge
   - Closure preference
   - Edit categorization button

3. **Access Details** (Encrypted, requires authentication)
   - Access method indicator
   - Password location reference
   - 2FA status badge
   - Recovery information
   - "View Credentials" button (requires re-authentication)
   - Last updated timestamp

4. **Value Assessment**
   - Monetary value display
   - Sentimental value indicator
   - Content preservation flag
   - Valuation notes
   - Edit value button

5. **Related Information**
   - Associated instructions
   - Assigned beneficiaries
   - Connected subscriptions

**Actions**:
- Edit account
- Delete account (with confirmation)
- View full access details
- Add to legacy instructions
- Export account details

### Categorization Form
**Component**: `<CategorizationModal />`

**Purpose**: Categorize and prioritize account for legacy planning

**Form Fields**:
- Category (required, dropdown or tag input)
- Legacy Priority (required, radio: Critical, High, Medium, Low)
- Closure Preference (required, dropdown: Close, Memorialize, Preserve, Transfer)
- Notes (optional, textarea)

**Visual Aids**:
- Priority level colors (Critical=red, High=orange, Medium=yellow, Low=green)
- Closure preference icons
- Examples for each option

### Access Details Form
**Component**: `<AccessDetailsModal />`

**Purpose**: Securely add account access information

**Security Notice**: Display encryption information and security assurance

**Form Fields**:
- Access Method (required, dropdown: Password, OAuth, SSO, Key, Other)
- Password Location Reference (text input, e.g., "Stored in 1Password")
- Has 2FA (checkbox)
- Two-Factor Details (conditional text input)
- Recovery Information (textarea, e.g., backup codes location)
- Security Questions (textarea)

**Features**:
- Show/hide password reference
- Copy to clipboard buttons
- Encryption indicator
- Security best practices tips

**Security**:
- Require re-authentication before viewing
- Session timeout after 5 minutes
- Audit log access
- "Viewing credentials" indicator

### Value Assessment Form
**Component**: `<ValueAssessmentModal />`

**Purpose**: Evaluate account's financial and sentimental value

**Form Fields**:
- Monetary Value (optional, currency input)
- Sentimental Value (optional, radio: High, Medium, Low)
- Content Worth Preserving (checkbox)
- Valuation Notes (optional, textarea)

**Features**:
- Currency selector
- Total portfolio value display
- Sentimental value indicators (heart icons)
- Examples of what to consider

### Account Import Wizard
**Route**: `/accounts/import`

**Purpose**: Bulk import accounts from password managers

**Steps**:
1. **Select Source**
   - Password manager selection (1Password, LastPass, Bitwarden, etc.)
   - CSV/JSON file upload
   - Instructions for export

2. **Map Fields**
   - Automatic field mapping
   - Preview mapped data
   - Manual adjustment options

3. **Review & Import**
   - Preview accounts to be imported
   - Select accounts to import
   - Set default values (importance, category)

4. **Completion**
   - Import summary
   - Success/error report
   - Navigate to review imported accounts

### Category Management
**Component**: `<CategorySidebar />`

**Purpose**: Navigate and manage account categories

**Features**:
- List all categories with account counts
- Filter accounts by category
- Visual category indicators
- Add/edit custom categories
- Collapse/expand categories

### Account Statistics
**Component**: `<AccountStatsCard />`

**Purpose**: Display account inventory statistics

**Metrics**:
- Total accounts registered
- Accounts by category (pie chart)
- Accounts by priority (bar chart)
- Completion percentage (accounts with full details)
- Total estimated value
- Accounts needing attention

## UI/UX Requirements

### Design System
- **Colors**:
  - Critical: Red (#DC2626)
  - High: Orange (#EA580C)
  - Medium: Yellow (#CA8A04)
  - Low: Green (#16A34A)
  - Neutral: Gray scale

- **Typography**:
  - Headers: Bold, 24-32px
  - Body: Regular, 14-16px
  - Labels: Medium, 12-14px

- **Icons**:
  - Service logos for known platforms
  - Priority indicators
  - Category icons
  - Security/encryption badge

### Responsive Design
- Desktop: Full dashboard with sidebar
- Tablet: Collapsible sidebar, grid view
- Mobile: Stack layout, bottom navigation

### Accessibility
- ARIA labels for all interactive elements
- Keyboard navigation support
- Screen reader announcements for actions
- High contrast mode support
- Focus indicators on all interactive elements

### Loading States
- Skeleton screens for account list
- Spinner for form submissions
- Progress indicator for bulk imports
- Optimistic UI updates

### Error Handling
- Inline form validation
- Error messages for failed API calls
- Retry options for network errors
- Graceful degradation for missing data

## Component Structure

```
/components/accounts/
  ├── AccountList.tsx
  ├── AccountCard.tsx
  ├── AccountDetails.tsx
  ├── AddAccountModal.tsx
  ├── CategorizationModal.tsx
  ├── AccessDetailsModal.tsx
  ├── ValueAssessmentModal.tsx
  ├── AccountStatsCard.tsx
  ├── CategorySidebar.tsx
  ├── ServiceAutocomplete.tsx
  └── AccountImportWizard/
      ├── SelectSource.tsx
      ├── MapFields.tsx
      ├── ReviewImport.tsx
      └── ImportComplete.tsx
```

## State Management

### Account State
```typescript
interface AccountState {
  accounts: DigitalAccount[];
  selectedAccount: DigitalAccount | null;
  loading: boolean;
  error: string | null;
  filters: {
    category: string[];
    priority: string[];
    type: string[];
    searchQuery: string;
  };
  sortBy: 'name' | 'dateAdded' | 'priority' | 'value';
  sortOrder: 'asc' | 'desc';
}
```

### Actions
- `loadAccounts()`
- `addAccount(account)`
- `updateAccount(id, updates)`
- `deleteAccount(id)`
- `setFilters(filters)`
- `setSorting(sortBy, sortOrder)`
- `importAccounts(accounts[])`

## Integration Requirements

### API Integration
- RESTful API calls to backend endpoints
- JWT authentication headers
- Error handling and retry logic
- Optimistic updates with rollback

### Service Recognition
- Autocomplete service names from API
- Display service logos from CDN
- Suggest account type based on service

### Encryption Indicator
- Visual indication of encrypted data
- Security badge on sensitive fields
- "Decrypting..." loading state

## Performance Requirements

- Account list render: < 100ms for 500 accounts
- Search/filter response: < 50ms
- Form submission feedback: Immediate optimistic update
- Import wizard: Handle 1000+ accounts smoothly

## User Flows

### Add New Account Flow
1. Click "Add Account" button
2. Fill service name (autocomplete suggests)
3. Select account type
4. Choose importance level
5. Enter username
6. Click "Save"
7. Success message
8. Option to add access details or categorization

### Complete Account Documentation Flow
1. User sees incomplete account indicator
2. Click "Complete Account" button
3. Guided wizard through:
   - Categorization
   - Access details
   - Value assessment
4. Progress indicator shows completion
5. Completion celebration message

### Bulk Import Flow
1. Navigate to Import Wizard
2. Select password manager source
3. Upload export file
4. Review field mapping
5. Preview accounts
6. Adjust defaults
7. Confirm import
8. View import summary
9. Navigate to review accounts

## Validation & Feedback

### Form Validation
- Real-time validation on blur
- Submit button disabled until valid
- Clear error messages
- Success animations

### User Feedback
- Toast notifications for actions
- Confirmation modals for destructive actions
- Progress indicators for long operations
- Empty states with helpful guidance

## Security Considerations

### Credential Display
- Require re-authentication to view credentials
- Auto-hide after 30 seconds
- Click to reveal pattern
- Audit log when credentials viewed

### Session Management
- Auto-lock after inactivity
- Secure session storage
- Clear sensitive data on logout

## Offline Support
- Cache account list for offline viewing
- Queue mutations when offline
- Sync when connection restored
- Offline indicator in UI
