# Environment Configuration - Backend Requirements

## Overview
Backend services for managing focus environment settings including environment preferences, focus mode activation, and background noise configurations.

---

## API Endpoints

### POST /api/environment/configure
**Description**: Configure user's focus environment preferences

**Request Body**:
```json
{
  "theme": "dark | light | auto",
  "notifications": {
    "enabled": true,
    "duringSession": false,
    "breakReminders": true
  },
  "display": {
    "fullscreen": false,
    "hideDistractions": true,
    "dimming": 0.3
  },
  "integrations": {
    "slackStatus": true,
    "teamsStatus": false,
    "calendar": true
  }
}
```

**Response**: `200 OK`
```json
{
  "configurationId": "uuid",
  "userId": "uuid",
  "theme": "dark",
  "notifications": { ... },
  "display": { ... },
  "integrations": { ... },
  "updatedAt": "ISO8601"
}
```

**Domain Event**: `FocusEnvironmentConfigured`

---

### POST /api/environment/focus-mode
**Description**: Activate focus mode for a session

**Request Body**:
```json
{
  "sessionId": "uuid",
  "mode": "deep | moderate | light",
  "duration": 25,
  "blockWebsites": ["facebook.com", "twitter.com"],
  "blockApplications": ["Slack", "Discord"],
  "autoReply": {
    "enabled": true,
    "message": "In focus mode. Will respond after {endTime}"
  }
}
```

**Response**: `201 Created`
```json
{
  "focusModeId": "uuid",
  "sessionId": "uuid",
  "mode": "deep",
  "activatedAt": "ISO8601",
  "estimatedEndTime": "ISO8601",
  "status": "active"
}
```

**Domain Event**: `FocusModeActivated`

---

### PUT /api/environment/focus-mode/{focusModeId}/deactivate
**Description**: Deactivate focus mode

**Response**: `200 OK`

**Domain Event**: `FocusModeDeactivated`

---

### POST /api/environment/background-noise
**Description**: Set background noise preferences

**Request Body**:
```json
{
  "enabled": true,
  "type": "white-noise | brown-noise | rain | cafe | nature | custom",
  "volume": 0.6,
  "customUrl": "https://example.com/audio.mp3",
  "autoStart": true
}
```

**Response**: `200 OK`
```json
{
  "preferenceId": "uuid",
  "userId": "uuid",
  "enabled": true,
  "type": "rain",
  "volume": 0.6,
  "autoStart": true,
  "updatedAt": "ISO8601"
}
```

**Domain Event**: `BackgroundNoisePreferenceSet`

---

### GET /api/environment/configuration
**Description**: Get user's environment configuration

**Response**: `200 OK` with configuration object

---

### GET /api/environment/focus-mode/active
**Description**: Get active focus mode details

**Response**: `200 OK` with focus mode object or `204 No Content`

---

### GET /api/environment/background-noise
**Description**: Get background noise preferences

**Response**: `200 OK` with background noise preferences

---

## Database Schema

### EnvironmentConfigurations Table
```sql
CREATE TABLE EnvironmentConfigurations (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL UNIQUE,
    Theme VARCHAR(20) NOT NULL DEFAULT 'auto',
    NotificationsEnabled BIT DEFAULT 1,
    NotificationsDuringSession BIT DEFAULT 0,
    BreakRemindersEnabled BIT DEFAULT 1,
    FullscreenMode BIT DEFAULT 0,
    HideDistractions BIT DEFAULT 1,
    DimmingLevel DECIMAL(3,2) DEFAULT 0.0,
    SlackIntegration BIT DEFAULT 0,
    TeamsIntegration BIT DEFAULT 0,
    CalendarIntegration BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2
);
```

### FocusModes Table
```sql
CREATE TABLE FocusModes (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    SessionId UNIQUEIDENTIFIER,
    Mode VARCHAR(20) NOT NULL,
    Duration INT NOT NULL,
    ActivatedAt DATETIME2 NOT NULL,
    DeactivatedAt DATETIME2,
    EstimatedEndTime DATETIME2,
    Status VARCHAR(20) NOT NULL,
    AutoReplyEnabled BIT DEFAULT 0,
    AutoReplyMessage NVARCHAR(500),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (SessionId) REFERENCES Sessions(Id)
);
```

### FocusModeBlockRules Table
```sql
CREATE TABLE FocusModeBlockRules (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    FocusModeId UNIQUEIDENTIFIER NOT NULL,
    RuleType VARCHAR(20) NOT NULL, -- 'website' or 'application'
    Target NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (FocusModeId) REFERENCES FocusModes(Id)
);
```

### BackgroundNoisePreferences Table
```sql
CREATE TABLE BackgroundNoisePreferences (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL UNIQUE,
    Enabled BIT DEFAULT 0,
    NoiseType VARCHAR(50) NOT NULL,
    Volume DECIMAL(3,2) NOT NULL DEFAULT 0.5,
    CustomUrl NVARCHAR(500),
    AutoStart BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2
);
```

---

## Domain Events

### FocusEnvironmentConfigured
```csharp
public record FocusEnvironmentConfigured(
    Guid ConfigurationId,
    Guid UserId,
    string Theme,
    bool NotificationsEnabled,
    bool NotificationsDuringSession,
    bool FullscreenMode,
    bool HideDistractions,
    decimal DimmingLevel,
    bool SlackIntegration,
    bool TeamsIntegration,
    bool CalendarIntegration,
    DateTime Timestamp
);
```

### FocusModeActivated
```csharp
public record FocusModeActivated(
    Guid FocusModeId,
    Guid UserId,
    Guid? SessionId,
    string Mode,
    int Duration,
    DateTime ActivatedAt,
    DateTime EstimatedEndTime,
    List<string> BlockedWebsites,
    List<string> BlockedApplications,
    bool AutoReplyEnabled,
    string? AutoReplyMessage,
    DateTime Timestamp
);
```

### FocusModeDeactivated
```csharp
public record FocusModeDeactivated(
    Guid FocusModeId,
    DateTime DeactivatedAt,
    int ActualDuration,
    DateTime Timestamp
);
```

### BackgroundNoisePreferenceSet
```csharp
public record BackgroundNoisePreferenceSet(
    Guid PreferenceId,
    Guid UserId,
    bool Enabled,
    string NoiseType,
    decimal Volume,
    string? CustomUrl,
    bool AutoStart,
    DateTime Timestamp
);
```

---

## Business Rules

1. **Focus Mode Levels**:
   - **Deep**: Blocks all websites/apps, no notifications, auto-reply enabled
   - **Moderate**: Blocks selected websites/apps, emergency notifications only
   - **Light**: Minimal blocking, notifications delayed until break

2. **Volume Range**: Background noise volume must be 0.0-1.0
3. **Dimming Level**: Display dimming must be 0.0-0.8 (0% to 80%)
4. **Concurrent Focus Modes**: Only one active focus mode per user
5. **Focus Mode Duration**: Must match session duration if sessionId provided
6. **Auto-Deactivation**: Focus mode auto-deactivates when session ends
7. **Theme Options**: "dark", "light", "auto" (follows system preference)
8. **Background Noise Types**: white-noise, brown-noise, rain, cafe, nature, custom
9. **Custom Audio**: Must be valid HTTPS URL, supported formats: MP3, OGG, WAV

---

## Integration Points

- **Notification Service**: Control notification delivery based on focus mode
- **Session Service**: Link focus mode to active sessions
- **Slack/Teams API**: Update user status when focus mode activates
- **Calendar Service**: Block calendar time during focus sessions
- **Browser Extension**: Enforce website blocking rules
- **Desktop App**: Enforce application blocking rules
- **Audio Service**: Stream background noise audio

---

## Security Considerations

1. **API Access**: All endpoints require authenticated user token
2. **Data Privacy**: Environment preferences are user-specific and private
3. **URL Validation**: Custom audio URLs must be validated and sanitized
4. **Rate Limiting**: Focus mode activation limited to 100 requests/hour per user
5. **Integration Tokens**: Slack/Teams tokens stored encrypted at rest
