# Frontend Requirements - Privacy & Security

## Pages/Views

### Privacy Settings Page
**Route**: `/settings/privacy`

**Components**:
- PrivacySettingsForm
- DefaultPrivacySelector
- ContentTypeDefaults
- NotificationToggles
- AuditLogToggle

**Features**:
- Set default privacy level for new content
- Configure per-content-type defaults
- Enable/disable share notifications
- Enable/disable audit logging
- View current settings
- Save/cancel changes

### Private Journal View
**Route**: `/journal/private`

**Components**:
- PrivateEntryList
- CreatePrivateEntryButton
- ShareEntryControls

**Features**:
- Display all private entries
- Create new private entry
- Share private entry with spouse
- Delete private entry
- Filter by content type, date

### Audit Log Page
**Route**: `/settings/audit-log`

**Components**:
- AuditLogTable
- FilterControls
- ExportButton

**Features**:
- View all access events
- Filter by date, content type, action
- Export audit log
- See who accessed what and when

### Data Export Page
**Route**: `/settings/data-export`

**Components**:
- ExportRequestForm
- ExportHistoryList
- DownloadButton

**Features**:
- Request new data export
- Choose format (JSON, CSV, PDF)
- Include/exclude photos option
- View export request history
- Download completed exports
- See export status (processing/ready)

### Account Security Page
**Route**: `/settings/security`

**Components**:
- PasswordChangeForm
- MFASetup
- ActiveSessionsList
- DeleteAccountSection

**Features**:
- Change password
- Enable/disable MFA
- View active sessions
- Logout from specific sessions
- Logout from all devices
- Request account deletion

## Components

### PrivacyLevelSelector
**Component**: Radio group or toggle
**Options**:
- Private (only me)
- Shared (spouse can see)

**Display**:
- Icon for each level
- Description of what each means
- Current selection highlighted

### PrivateEntryCard
**Display**:
- Entry content preview
- Content type badge
- Creation date
- Privacy duration indicator
- Share button
- Delete button

### ShareEntryDialog
**Component**: Confirmation modal
**Fields**:
- Optional share reason (textarea)
- Confirm button
- Cancel button

**Display**:
- Warning that sharing is permanent
- Preview of entry to be shared

### AuditLogTable
**Columns**:
- Timestamp
- Content type
- Action (viewed/shared/edited/deleted)
- Actor (you/spouse)
- IP address (optional)

**Features**:
- Sort by column
- Pagination
- Responsive (stack on mobile)

### MFASetupWizard
**Steps**:
1. Choose MFA method (Authenticator app / SMS)
2. Scan QR code or enter code
3. Verify with code
4. Save backup codes

**Display**:
- Step indicator
- Instructions for each step
- Success confirmation

### ActiveSessionCard
**Display**:
- Device type/name
- Browser
- IP address
- Last active timestamp
- Current session indicator
- Logout button

## State Management

### privacySlice
```typescript
{
  settings: {
    defaults: {},
    loading: boolean,
    error: string | null
  },
  privateEntries: {
    items: [],
    loading: boolean
  },
  auditLog: {
    items: [],
    loading: boolean
  },
  dataExport: {
    requests: [],
    activeExport: null,
    loading: boolean
  }
}
```

### securitySlice
```typescript
{
  mfa: {
    enabled: boolean,
    method: string | null
  },
  sessions: {
    active: [],
    current: string,
    loading: boolean
  },
  passwordChange: {
    loading: boolean,
    success: boolean
  }
}
```

## Real-time Features
- Notification when data export is ready
- Alert when new session detected
- Live session list updates

## Security UI Patterns

### Sensitive Actions
- Require password confirmation for:
  - Changing password
  - Disabling MFA
  - Account deletion
  - Sharing private entries after long time

### Visual Indicators
- Lock icon for private content
- Shield icon for encrypted content
- Eye icon for shared content
- Warning colors for security alerts

### Confirmations
- Double confirmation for destructive actions
- Clear explanation of consequences
- Cannot be undone warnings

## Responsive Design
- Mobile: Stacked forms, simplified tables
- Desktop: Multi-column layouts, detailed tables
- Touch-friendly buttons for sensitive actions

## Accessibility
- Clear labels for all privacy controls
- Screen reader announcements for privacy changes
- Keyboard navigation for all security features
- High contrast mode support

## Error Handling

### Privacy Errors
- Failed to update settings: Show error, preserve form
- Failed to share entry: Allow retry
- Export failed: Provide troubleshooting steps

### Security Errors
- Failed login: Clear error messages
- MFA setup failed: Allow restart
- Session expired: Redirect to login with message
