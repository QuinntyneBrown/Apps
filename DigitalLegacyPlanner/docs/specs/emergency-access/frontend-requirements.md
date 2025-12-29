# Emergency Access - Frontend Requirements

## Pages & Components

### Emergency Protocol Dashboard
**Route**: `/emergency`

**Components**:
- Dead man's switch status card
- Activity confirmation button (prominent)
- Last activity timestamp
- Next check date countdown
- Emergency access requests (for executors)

### Configure Emergency Access
**Component**: `<ConfigureEmergencyModal />`

**Form Fields**:
- Activation trigger (dropdown: Inactivity, Manual request)
- Inactivity threshold (number input, days: 30-90)
- Waiting period before release (number input, days)
- Notification recipients (multi-select from contacts)
- Access package contents (checkboxes: All accounts, Instructions only, Specific accounts)

**Features**:
- Visual timeline showing activation flow
- Warning period configuration
- Test notification option
- Protocol preview

### Activity Confirmation
**Component**: `<ConfirmActivityButton />`

**Features**:
- One-click confirmation
- "I'm still here" button (prominent, colorful)
- Confirmation success animation
- Next check date display
- Streak counter (gamification)

### Dead Man's Switch Monitor
**Component**: `<DeadManSwitchStatus />`

**Visual Elements**:
- Status indicator (Active, Warning, Triggered)
- Days until next check (countdown)
- Activity history timeline
- Warning notifications display

### Emergency Access Request (Executor View)
**Route**: `/executor/request-access`

**Form Fields**:
- Request reason (textarea, required)
- Urgency level (radio: Emergency, Standard)
- Supporting documentation upload
- Verification method selection

**Process Flow**:
1. Submit request
2. Verification pending status
3. Waiting period countdown
4. Access granted notification
5. Access materials delivery

## UI/UX Requirements

### Activity Confirmation UX
- Friendly, non-morbid design
- Easy one-click confirmation
- Email/SMS reminders
- Mobile-optimized confirmation
- Gamification (streaks, badges)

### Warning States
- Gentle warnings (7 days before)
- Escalating urgency (3 days, 1 day)
- Multiple notification channels
- Clear reset instructions

### Executor Experience
- Clear access request process
- Transparent verification status
- Estimated wait time display
- Support contact information
- Compassionate design language

## Component Structure
```
/components/emergency/
  ├── EmergencyDashboard.tsx
  ├── ConfigureEmergencyModal.tsx
  ├── ConfirmActivityButton.tsx
  ├── DeadManSwitchStatus.tsx
  ├── AccessRequestForm.tsx (executor)
  └── VerificationStatus.tsx
```
