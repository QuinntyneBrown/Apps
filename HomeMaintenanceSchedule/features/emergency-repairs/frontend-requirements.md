# Emergency Repairs - Frontend Requirements

## Overview
Quick access emergency reporting interface with photo/video capture, emergency provider contact, and real-time status updates.

## UI Components

### 1. Emergency Dashboard
- **Alert Banner**: Active emergencies count with red indicator
- **Quick Report Button**: Large, prominent "Report Emergency" CTA
- **Active Emergencies List**: Real-time status, provider info, time elapsed
- **Emergency Contacts Card**: Quick access to 24/7 providers
- **Status Timeline**: Visual tracking of emergency progress

### 2. Report Emergency Form (Optimized for Speed)
- Emergency type selector (large icons)
- Severity selector (color-coded: Red=Critical, Orange=Urgent, Yellow=Moderate)
- Quick description (voice-to-text option)
- Photo/video capture (camera integration)
- Location within property dropdown
- One-click "Submit Emergency"

### 3. Emergency Detail View
- Severity badge (color-coded, pulsing if active)
- Status timeline with timestamps
- Assigned provider info with click-to-call
- Photo/video gallery
- Temporary solution notes
- Insurance claim tracking
- Real-time updates

### 4. Emergency Provider Quick Contact
- Emergency providers list (filtered by type)
- One-tap call buttons
- SMS quick send
- Provider availability status
- Last response time

## State Management
```javascript
{
  emergencies: {
    active: [],
    history: [],
    current: null,
    loading: false,
    providers: []
  }
}
```

## Features
- Real-time status updates (WebSocket)
- Push notifications for updates
- Offline support for emergency reporting
- GPS location tagging
- Voice notes recording
- Photo/video compression
- Emergency provider rating

## Mobile-First Design
- Large touch targets for emergency actions
- Voice input for descriptions
- Native camera integration
- Emergency mode (simplified UI)
- Offline queue for poor connectivity

---

**Version**: 1.0
**Last Updated**: 2025-12-29
