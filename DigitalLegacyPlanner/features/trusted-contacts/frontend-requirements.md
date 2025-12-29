# Trusted Contacts - Frontend Requirements

## Pages & Components

### Executors & Contacts Dashboard
**Route**: `/contacts`

**Components**:
- Primary executor card with status badge
- Emergency contacts list
- Add executor/contact buttons
- Notification history
- Acceptance status indicators

### Add Executor Form
**Component**: `<AddExecutorModal />`

**Form Fields**:
- Name (required)
- Email (required, validated)
- Phone (required)
- Relationship (dropdown)
- Scope of Authority (checkboxes: All accounts, Specific accounts, Limited actions)
- Access Level (radio: Full, Limited, View-only)
- Personal message (textarea)

**Features**:
- Email validation
- Phone number formatting
- Authority scope builder
- Account selector for limited access
- Send notification option

### Permission Management
**Component**: `<PermissionManager />`

**Features**:
- Visual permission matrix (contacts × accounts)
- Drag-and-drop permission assignment
- Activation conditions selector
- Access level indicators (color-coded)
- Bulk permission actions

### Notification Center
**Component**: `<ExecutorNotifications />`

**Features**:
- Send invitation to executor
- Track notification status (Sent, Delivered, Opened, Accepted)
- Resend notification option
- Custom message editor
- Notification templates

## UI/UX Requirements

- Trust indicators for executors (verified email, accepted role)
- Clear permission visualization
- Easy notification sending
- Status tracking timeline
- Acceptance confirmation workflow

## Component Structure
```
/components/contacts/
  ├── ContactsDashboard.tsx
  ├── ExecutorCard.tsx
  ├── AddExecutorModal.tsx
  ├── PermissionManager.tsx
  ├── NotificationCenter.tsx
  └── AcceptanceStatus.tsx
```
