# Alerts & Notifications - Frontend Requirements

## Pages

### Notifications Center (`/notifications`)
- **Active Alerts**: Unread notifications
- **Alert Categories**: Filter by type
- **Mark as Read**: Individual or bulk
- **Alert History**: Past 30 days

### Alert Preferences (`/settings/alerts`)
- **Toggle Alerts**: Enable/disable each type
- **Delivery Method**: Push, email, SMS
- **Thresholds**: Customize trigger points
- **Quiet Hours**: Do not disturb schedule

## UI Components

### AlertBadge
- Notification bell icon with count
- Dropdown preview of recent alerts
- Quick actions (dismiss, view)

### AlertCard
- Icon based on type
- Message and timestamp
- Severity color coding
- Action buttons (View Details, Dismiss)

### Alert Types

**Fuel Price Spike**
- Red exclamation icon
- "Fuel prices jumped $0.35 in your area"
- Suggestion: "Consider filling up today"

**Budget Threshold**
- Yellow warning icon
- "You've used 75% of your monthly budget"
- Remaining amount display

**MPG Decline**
- Orange alert icon
- "Your MPG has dropped 12% this month"
- Possible causes and actions

**Milestone Achievement**
- Green celebration icon
- "Congratulations! You've driven 100,000 miles"
- Share button

## Push Notifications

### Priority Levels
- Critical: Low fuel, budget exceeded
- High: Price spike, MPG decline
- Medium: Budget threshold, maintenance due
- Low: Milestones, weekly reports

### Rich Notifications
- Images for celebrations
- Action buttons
- Deep links to relevant pages
