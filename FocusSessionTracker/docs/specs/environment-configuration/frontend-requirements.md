# Environment Configuration - Frontend Requirements

## Overview
User interface for configuring focus environment settings, managing focus modes, and controlling background noise preferences.

---

## Pages

### Environment Settings Page (`/settings/environment`)
**Purpose**: Configure all environment-related preferences

**Components**:
- Theme Selector
- Notification Settings Panel
- Display Settings Panel
- Integration Toggles
- Background Noise Controls
- Save/Reset Buttons

**Layout**: Tabbed interface with sections

---

### Focus Mode Control Panel (`/focus-mode`)
**Purpose**: Activate and manage focus mode

**Components**:
- Focus Mode Activation Card
- Mode Level Selector (Deep/Moderate/Light)
- Website/App Blocker Configuration
- Auto-Reply Settings
- Active Mode Status Display
- Deactivate Button

**States**:
- Inactive (configuration view)
- Active (status display + deactivate option)

---

## Components

### ThemeSelector
```typescript
interface ThemeSelectorProps {
  currentTheme: 'dark' | 'light' | 'auto';
  onChange: (theme: string) => void;
}
```

**Visual Requirements**:
- Three option cards with preview
- Live preview of selected theme
- Icon representation for each theme
- Active state highlighting

---

### NotificationSettingsPanel
```typescript
interface NotificationSettingsPanelProps {
  settings: {
    enabled: boolean;
    duringSession: boolean;
    breakReminders: boolean;
  };
  onChange: (settings: NotificationSettings) => void;
}
```

**Fields**:
- Master notifications toggle
- During session toggle (disabled if master is off)
- Break reminders toggle (disabled if master is off)
- Test notification button

---

### DisplaySettingsPanel
```typescript
interface DisplaySettingsPanelProps {
  settings: {
    fullscreen: boolean;
    hideDistractions: boolean;
    dimming: number;
  };
  onChange: (settings: DisplaySettings) => void;
}
```

**Fields**:
- Fullscreen mode toggle
- Hide distractions toggle
- Dimming level slider (0-80%)
- Preview dimming effect

---

### IntegrationToggles
```typescript
interface IntegrationTogglesProps {
  integrations: {
    slackStatus: boolean;
    teamsStatus: boolean;
    calendar: boolean;
  };
  onToggle: (service: string, enabled: boolean) => void;
  onConnect: (service: string) => void;
}
```

**Display**:
- Service cards (Slack, Teams, Calendar)
- Connection status indicator
- Connect/Disconnect buttons
- Authorization flow handling

---

### BackgroundNoiseControls
```typescript
interface BackgroundNoiseControlsProps {
  preferences: {
    enabled: boolean;
    type: string;
    volume: number;
    customUrl?: string;
    autoStart: boolean;
  };
  onChange: (preferences: BackgroundNoisePreferences) => void;
  onPreview: (type: string) => void;
}
```

**Fields**:
- Enable background noise toggle
- Noise type selector (grid of options)
- Volume slider with live preview
- Custom URL input (shown when "custom" selected)
- Auto-start with session toggle
- Preview button for each noise type

**Noise Types**:
- White Noise (icon: waves)
- Brown Noise (icon: deep waves)
- Rain (icon: rain cloud)
- Cafe (icon: coffee cup)
- Nature (icon: tree)
- Custom (icon: upload)

---

### FocusModeActivator
```typescript
interface FocusModeActivatorProps {
  onActivate: (config: FocusModeConfig) => void;
  currentSession?: Session;
}
```

**Fields**:
- Mode level selector (Deep/Moderate/Light)
- Mode description display
- Duration selector (links to session if active)
- Website blocker configuration
- App blocker configuration
- Auto-reply toggle and message
- Activate button

**Mode Descriptions**:
- **Deep**: Maximum focus - blocks all distractions, auto-replies enabled
- **Moderate**: Balanced focus - selective blocking, urgent notifications only
- **Light**: Gentle focus - minimal blocking, notifications delayed

---

### WebsiteBlockerConfig
```typescript
interface WebsiteBlockerConfigProps {
  blockedSites: string[];
  onAdd: (site: string) => void;
  onRemove: (site: string) => void;
  presets: string[][];
}
```

**Features**:
- Input field for manual entry
- Tag-style display of blocked sites
- Preset lists (Social Media, News, Shopping, Entertainment)
- Import/Export blocked list

---

### ApplicationBlockerConfig
```typescript
interface ApplicationBlockerConfigProps {
  blockedApps: string[];
  onAdd: (app: string) => void;
  onRemove: (app: string) => void;
  installedApps: string[];
}
```

**Features**:
- Dropdown of installed applications
- Tag-style display of blocked apps
- Quick-add common apps (Slack, Discord, Email clients)

---

### FocusModeStatusDisplay
```typescript
interface FocusModeStatusDisplayProps {
  focusMode: {
    mode: string;
    activatedAt: Date;
    estimatedEndTime: Date;
    blockedWebsites: string[];
    blockedApplications: string[];
  };
  onDeactivate: () => void;
}
```

**Display**:
- Active mode badge
- Time remaining countdown
- List of active blocks
- Deactivate button (with confirmation)
- Quick pause option (temporary disable)

---

## User Flows

### Configure Environment Flow
1. User navigates to Environment Settings
2. User selects theme preference
3. User toggles notification settings
4. User adjusts display settings (dimming slider)
5. User connects integrations (OAuth flows)
6. User clicks "Save Settings"
7. Success message displayed
8. Settings applied immediately

### Activate Focus Mode Flow
1. User clicks "Activate Focus Mode"
2. Focus Mode panel opens
3. User selects mode level (Deep/Moderate/Light)
4. Default blocks pre-populated based on level
5. User adds/removes specific websites/apps
6. User enables auto-reply and sets message
7. User clicks "Activate"
8. Focus mode activates
9. System status updates (Slack, Teams, etc.)
10. Blocking rules enforced

### Configure Background Noise Flow
1. User enables background noise toggle
2. Noise type grid appears
3. User selects noise type (e.g., Rain)
4. Preview plays automatically
5. User adjusts volume slider
6. User enables auto-start option
7. Settings save automatically
8. Audio continues playing if enabled

### Deactivate Focus Mode Flow
1. User clicks "Deactivate Focus Mode"
2. Confirmation dialog appears
3. User confirms deactivation
4. Focus mode deactivates
5. All blocks removed
6. System status restored
7. Summary displayed (duration, effectiveness)

---

## State Management

### Environment Configuration Store
```typescript
interface EnvironmentConfigState {
  theme: 'dark' | 'light' | 'auto';
  notifications: {
    enabled: boolean;
    duringSession: boolean;
    breakReminders: boolean;
  };
  display: {
    fullscreen: boolean;
    hideDistractions: boolean;
    dimming: number;
  };
  integrations: {
    slackStatus: boolean;
    teamsStatus: boolean;
    calendar: boolean;
  };
  backgroundNoise: {
    enabled: boolean;
    type: string;
    volume: number;
    customUrl?: string;
    autoStart: boolean;
  };
  isLoading: boolean;
  lastSaved: Date | null;
}
```

### Focus Mode Store
```typescript
interface FocusModeState {
  activeFocusMode: FocusMode | null;
  isActivating: boolean;
  blockedWebsites: string[];
  blockedApplications: string[];
  autoReply: {
    enabled: boolean;
    message: string;
  };
}
```

### Actions
- `updateTheme(theme: string)`
- `updateNotifications(settings: NotificationSettings)`
- `updateDisplay(settings: DisplaySettings)`
- `toggleIntegration(service: string, enabled: boolean)`
- `updateBackgroundNoise(preferences: BackgroundNoisePreferences)`
- `activateFocusMode(config: FocusModeConfig)`
- `deactivateFocusMode()`
- `addBlockedWebsite(site: string)`
- `removeBlockedWebsite(site: string)`
- `addBlockedApplication(app: string)`
- `removeBlockedApplication(app: string)`
- `previewBackgroundNoise(type: string)`

---

## Validation Rules

| Field | Rule |
|-------|------|
| Theme | Must be: dark, light, or auto |
| Dimming Level | 0.0 to 0.8 (0% to 80%) |
| Volume | 0.0 to 1.0 (0% to 100%) |
| Custom Audio URL | Valid HTTPS URL, max 500 chars |
| Auto-Reply Message | Max 500 characters |
| Website URL | Valid domain format (e.g., example.com) |
| Application Name | Non-empty, max 100 chars |
| Focus Mode Duration | 5-120 minutes |

---

## Accessibility Requirements

- All toggles have clear on/off states with labels
- Sliders show current value and update aria-valuenow
- Color is not the only indicator (use icons/text)
- All modals support keyboard navigation (Tab, Escape)
- Focus mode status announced to screen readers
- Background noise controls accessible via keyboard
- High contrast support for all UI elements
- Focus indicators on all interactive elements

---

## Responsive Design

| Breakpoint | Layout | Notable Changes |
|------------|--------|-----------------|
| Mobile (<768px) | Single column | Stacked settings panels, collapsed integrations |
| Tablet (768-1024px) | Two column | Side-by-side panels where appropriate |
| Desktop (>1024px) | Multi-column | Full tabbed interface, side preview panels |

---

## Visual Design Guidelines

### Color Scheme
- **Deep Focus**: Red accent (#EF4444)
- **Moderate Focus**: Yellow accent (#F59E0B)
- **Light Focus**: Green accent (#10B981)
- **Active State**: Blue (#3B82F6)
- **Disabled State**: Gray (#9CA3AF)

### Animations
- Theme switch: 300ms fade transition
- Focus mode activation: Slide-in panel with 400ms ease
- Dimming effect: Gradual fade over 500ms
- Toggle switches: 200ms smooth transition

### Icons
- Use outline style for inactive states
- Use solid style for active states
- Consistent 24px size
- Include text labels for clarity

---

## Performance Considerations

1. **Audio Streaming**: Use progressive loading for background noise
2. **Theme Switching**: Preload theme assets to prevent flash
3. **Settings Sync**: Debounce slider changes (300ms delay)
4. **Auto-Save**: Save settings automatically 1 second after last change
5. **Preview Mode**: Limit audio preview to 10 seconds
6. **Integration Status**: Poll every 30 seconds when focus mode active

---

## Error Handling

| Scenario | User Feedback |
|----------|--------------|
| Save fails | Toast error: "Failed to save settings. Please try again." |
| Integration connection fails | Modal with retry option and help link |
| Custom audio URL invalid | Inline error: "Please enter a valid HTTPS URL" |
| Focus mode activation fails | Toast error with reason and support contact |
| Audio playback fails | Notification: "Unable to play audio. Check your connection." |
