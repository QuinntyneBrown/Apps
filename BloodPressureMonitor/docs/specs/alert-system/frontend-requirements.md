# Frontend Requirements - Alert System

## Overview
The Alert System frontend provides visual and interactive notifications for blood pressure anomalies and health concerns.

## User Interface Components

### 1. Alert Notification Banner

**Location:** Top of dashboard when unacknowledged alerts exist

**Display:**
- Alert severity icon (color-coded)
- Alert title
- Brief message
- "View Details" button
- "Dismiss" button (X icon)
- Count of additional alerts if multiple

**Severity Styling:**
- Critical: Red background, white text, urgent icon
- High: Orange background, dark text, warning icon
- Medium: Yellow background, dark text, caution icon
- Low: Blue background, dark text, info icon

**Behavior:**
- Auto-dismiss low severity after 10 seconds (unless hovered)
- Critical alerts require explicit dismissal
- Stack multiple alerts vertically
- Animate slide-in from top

### 2. Alert Center

**Location:** Dedicated alerts page, accessible from notification bell icon in header

**Layout:**
- Header with "Alerts" title and filter options
- Unacknowledged alerts section (top)
- Acknowledged alerts section (below, collapsible)
- Empty state if no alerts

**Filter Options:**
- All / Critical / High / Medium / Low
- Date range
- Type: High BP / Low BP / Crisis / Irregular Heartbeat
- Acknowledged / Unacknowledged

**Alert Card (Unacknowledged):**
- Severity indicator (colored left border)
- Alert icon
- Title (bold)
- Message
- Related reading (systolic/diastolic, timestamp)
- Recommended action (expandable)
- Time ago ("5 minutes ago")
- Actions:
  - "View Reading" button
  - "Acknowledge" button
  - "Dismiss" (X icon)

**Alert Card (Acknowledged):**
- Greyed out appearance
- Acknowledged checkmark
- Timestamp of acknowledgment
- Collapsed by default

### 3. Alert Detail Modal

**Triggered:** Clicking on alert card or "View Details"

**Sections:**
- Alert summary (severity, type, title)
- Related reading details
  - BP values with chart showing where it falls
  - Pulse
  - Date and time
  - Context
- Explanation
  - What this alert means
  - Why it was triggered
  - Severity classification
- Recommended Actions (bulleted list)
  - Immediate actions
  - Follow-up recommendations
  - When to seek medical help
- Related Information
  - Link to recent trend
  - Link to similar past alerts
  - Educational resources
- Actions:
  - "View Full Reading"
  - "Contact Doctor" (if crisis)
  - "Acknowledge Alert"
  - "Set Reminder for Follow-up"
  - Close (X)

### 4. Alert Settings

**Location:** Settings page, "Alerts & Notifications" section

**Enable/Disable Alerts:**
- Master toggle for all alerts
- Individual toggles per alert type:
  - High blood pressure alerts
  - Low blood pressure alerts
  - Hypertensive crisis alerts
  - Irregular heartbeat alerts

**Threshold Configuration:**
- High BP Custom Threshold
  - Systolic slider (120-160, default 140)
  - Diastolic slider (80-100, default 90)
  - "Use doctor's recommendation" checkbox
- Low BP Custom Threshold
  - Systolic slider (70-100, default 90)
  - Diastolic slider (40-70, default 60)

**Notification Channels:**
- Push notifications toggle
- Email notifications toggle
- SMS notifications toggle (with phone number input)
- In-app only toggle

**Quiet Hours:**
- Enable quiet hours toggle
- Start time picker
- End time picker
- "Critical alerts override quiet hours" checkbox (default on)

**Test Notification:**
- "Send Test Alert" button
- Dropdown to select alert type to test

### 5. Push Notification (Mobile/Browser)

**Critical Alert:**
- Title: "URGENT: Hypertensive Crisis Detected"
- Body: "Your BP reading of 185/125 requires immediate medical attention"
- Icon: Red warning
- Sound: Urgent alert tone
- Actions: "View Details", "Dismiss"

**High/Medium Alert:**
- Title: "High Blood Pressure Detected"
- Body: "Your reading of 145/92 indicates Stage 1 Hypertension"
- Icon: Orange warning
- Sound: Standard notification
- Actions: "View", "Dismiss"

**Low Severity Alert:**
- Title: "Blood Pressure Note"
- Body: "Your BP is slightly elevated"
- Icon: Blue info
- Sound: Gentle notification
- Actions: "View", "Dismiss"

### 6. Alert History Chart

**Location:** Alert Center page, above alert list

**Display:**
- Line chart showing alert frequency over time
- Color-coded bars for each severity level
- Time range selector (7 days, 30 days, 3 months)
- Interactive tooltips showing alert details

## User Flows

### Flow 1: Receive and Acknowledge Critical Alert
1. User records BP reading of 185/125
2. System immediately triggers crisis alert
3. Push notification appears on all devices
4. Alert banner appears at top of app
5. User clicks notification or banner
6. Alert detail modal opens
7. User reads explanation and recommended actions
8. User clicks "Acknowledge Alert"
9. Optional: User clicks "Contact Doctor" (opens emergency contacts)
10. Alert moves to acknowledged section

### Flow 2: Configure Alert Settings
1. User navigates to Settings
2. User clicks "Alerts & Notifications"
3. Alert settings page loads
4. User adjusts high BP threshold sliders
5. User enables quiet hours
6. User sets quiet hours time range
7. User enables SMS notifications
8. User enters phone number
9. User clicks "Send Test Alert"
10. Test notification delivered
11. User clicks "Save Settings"
12. Settings saved with confirmation

### Flow 3: Review Alert History
1. User clicks notification bell icon
2. Alert Center opens
3. User sees 3 unacknowledged alerts
4. User clicks "Filter" and selects "High Severity"
5. List filters to show only high severity alerts
6. User expands alert to read details
7. User clicks "View Reading"
8. Reading detail page opens
9. User returns to Alert Center
10. User acknowledges all alerts via "Acknowledge All" button

## Responsive Design

### Mobile
- Full-screen alert detail modals
- Bottom sheet for quick alert preview
- Floating notification bell badge
- Swipe to acknowledge gesture
- Sticky critical alert banner

### Tablet/Desktop
- Side panel for alert details
- Hover states on alert cards
- Keyboard shortcuts (A to acknowledge, Esc to close)
- Multi-select for batch acknowledge

## Accessibility

- High contrast mode for severity colors
- Screen reader announces new alerts
- Keyboard navigation through alerts (Tab, Enter)
- Alert severity conveyed through text, not just color
- Focus management when modal opens
- ARIA live regions for new alerts

## Animations

- Alert banner slide-in from top (300ms ease-out)
- Alert card fade-in (200ms)
- Severity indicator pulse for critical alerts
- Smooth expand/collapse for acknowledged section
- Confetti animation for "All alerts cleared!"

## State Management

### Local State
- Alert detail modal open/closed
- Filter selections
- Expanded/collapsed sections

### Global State
- Unacknowledged alerts count (for badge)
- Alert list
- Alert settings
- Real-time alert updates via WebSocket

### Persistent State
- Last viewed alert timestamp
- User alert preferences
- Dismissed alert IDs (24 hour cache)

## Real-Time Updates

- WebSocket connection for instant alert delivery
- Update alert badge count in real-time
- Auto-refresh alert list when new alert arrives
- Sound notification for new critical alert
- Visual animation for incoming alert

## Offline Behavior

- Queue alert acknowledgments for sync when online
- Show cached alerts in Alert Center
- Disable "Send Test Alert" when offline
- Display offline indicator in settings

## Error Handling

### Error Messages
- "Failed to load alerts. Please try again."
- "Could not acknowledge alert. Check your connection."
- "Settings save failed. Your changes were not applied."
- "Test notification could not be sent."

### Recovery Actions
- Retry button for failed operations
- Auto-retry for critical operations
- Fallback to cached data when API fails

## Analytics Events

- `alert_triggered` - Alert created and displayed
- `alert_acknowledged` - User acknowledges alert
- `alert_dismissed` - User dismisses without acknowledging
- `alert_detail_viewed` - User views alert details
- `alert_settings_changed` - User modifies settings
- `emergency_contact_initiated` - User clicks "Contact Doctor"
- `test_notification_sent` - User tests notifications
