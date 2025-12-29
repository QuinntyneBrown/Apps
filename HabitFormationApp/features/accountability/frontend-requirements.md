# Accountability - Frontend Requirements

## Overview
UI for managing accountability partners, sharing progress, sending encouragement, and completing check-ins.

## Key Components

### 1. Add Partner Interface
- Search for users by email/username
- Select habits to share
- Set permission levels
- Send invitation message
- View pending invitations

### 2. Partners Dashboard
- List of active partners
- Partner progress cards
- Check-in status
- Send encouragement button
- Quick message feature

### 3. Check-In Form
- Message input
- Mood selector (Great, Good, Okay, Struggling)
- Challenges description
- Share with partners button

### 4. Partner Progress View
- Partner's habits (based on permissions)
- Streak information
- Recent completions
- Encouragement history

### 5. Encouragement Interface
- Quick encouragement templates
- Custom message
- GIF/emoji support
- Send to specific partner

## State Management
```typescript
interface AccountabilityState {
  partnerships: Partnership[];
  checkIns: CheckIn[];
  encouragements: Encouragement[];
  pendingInvitations: Invitation[];
}
```

## Notifications
- "Sarah sent you encouragement!"
- "Time for your weekly check-in with Mike"
- "John accepted your partnership invitation"

## Privacy Controls
- Toggle habit visibility
- Remove partner
- Block user
- Report inappropriate content
