# Backend Requirements - Alert System

## Overview
The Alert System monitors blood pressure readings and triggers notifications for abnormal values, dangerous conditions, and irregular heartbeats.

## API Endpoints

### GET /api/alerts
Retrieve user's alerts with filtering.

**Query Parameters:**
- `severity` (optional): low, medium, high, critical
- `type` (optional): high_bp, low_bp, crisis, irregular_heartbeat
- `startDate` (optional)
- `endDate` (optional)
- `acknowledged` (optional): true/false
- `limit` (default: 50)
- `offset` (default: 0)

**Response:** 200 OK
```json
{
  "alerts": [
    {
      "alertId": "uuid",
      "type": "high_bp",
      "severity": "medium",
      "readingId": "uuid",
      "title": "High Blood Pressure Detected",
      "message": "Your blood pressure reading of 145/92 mmHg indicates Stage 1 Hypertension",
      "actionRecommended": "Monitor closely and consult doctor if readings remain elevated",
      "createdAt": "2025-12-29T08:30:15Z",
      "acknowledged": false
    }
  ],
  "total": 15
}
```

### GET /api/alerts/{alertId}
Retrieve specific alert details.

**Response:** 200 OK

### PUT /api/alerts/{alertId}/acknowledge
Mark an alert as acknowledged.

**Response:** 200 OK

**Domain Events Triggered:**
- AlertAcknowledged

### GET /api/alerts/settings
Get user's alert preferences.

**Response:** 200 OK
```json
{
  "enableAlerts": true,
  "highBPThreshold": {
    "systolic": 140,
    "diastolic": 90
  },
  "lowBPThreshold": {
    "systolic": 90,
    "diastolic": 60
  },
  "notificationChannels": ["push", "email"],
  "quietHours": {
    "enabled": true,
    "start": "22:00",
    "end": "07:00"
  }
}
```

### PUT /api/alerts/settings
Update alert preferences.

**Request Body:** Same as GET response

**Response:** 200 OK

## Domain Models

### Alert Aggregate
```csharp
public class Alert : AggregateRoot
{
    public Guid AlertId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ReadingId { get; private set; }
    public AlertType Type { get; private set; }
    public AlertSeverity Severity { get; private set; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public string ActionRecommended { get; private set; }
    public bool Acknowledged { get; private set; }
    public DateTime AcknowledgedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void Acknowledge();
}

public enum AlertType
{
    HighBloodPressure,
    LowBloodPressure,
    HypertensiveCrisis,
    IrregularHeartbeat
}

public enum AlertSeverity
{
    Low,
    Medium,
    High,
    Critical
}
```

### AlertSettings Value Object
```csharp
public class AlertSettings : ValueObject
{
    public bool EnableAlerts { get; private set; }
    public BPThreshold HighBPThreshold { get; private set; }
    public BPThreshold LowBPThreshold { get; private set; }
    public List<string> NotificationChannels { get; private set; }
    public QuietHours QuietHours { get; private set; }
}
```

## Business Rules

### BR-AS-001: High BP Detection
- Stage 1 Hypertension: Systolic 130-139 OR Diastolic 80-89 (Medium severity)
- Stage 2 Hypertension: Systolic ≥140 OR Diastolic ≥90 (High severity)
- Use user-customizable thresholds if set

### BR-AS-002: Low BP Detection
- Hypotension: Systolic <90 AND Diastolic <60 (Medium severity)
- Severe Hypotension: Systolic <70 OR Diastolic <40 (High severity)

### BR-AS-003: Hypertensive Crisis
- Crisis Urgency: Systolic >180 OR Diastolic >120 without symptoms (Critical severity)
- Crisis Emergency: Systolic >180 OR Diastolic >120 with symptoms (Critical severity)
- Immediate notification required

### BR-AS-004: Irregular Heartbeat
- Triggered when device or user indicates irregular rhythm
- Severity based on frequency (Medium if occasional, High if frequent)

### BR-AS-005: Alert Deduplication
- Don't create duplicate alert if similar alert exists within 1 hour
- Don't send crisis alert if one already sent within 15 minutes

### BR-AS-006: Quiet Hours
- Suppress non-critical alerts during quiet hours
- Crisis alerts always sent immediately
- Queue suppressed alerts for delivery after quiet hours

### BR-AS-007: Notification Channels
- Push notifications for critical alerts (always)
- Email for high severity (if enabled)
- SMS for critical alerts (if configured and enabled)

## Event Handlers

### On BloodPressureRecorded
1. Evaluate reading against high BP thresholds
2. Evaluate reading against low BP thresholds
3. Check for hypertensive crisis
4. Create appropriate alerts
5. Send notifications based on severity and settings

### On PulseRecorded
1. Check if rhythm irregularity noted
2. Create irregular heartbeat alert if detected
3. Send notification

### On IrregularHeartbeatDetected
1. Create alert
2. Send notification
3. Track frequency for pattern detection

## Event Publishing

### HighBloodPressureDetected
```json
{
  "eventId": "uuid",
  "eventType": "HighBloodPressureDetected",
  "timestamp": "2025-12-29T08:30:15Z",
  "data": {
    "readingId": "uuid",
    "userId": "uuid",
    "systolic": 145,
    "diastolic": 92,
    "severityLevel": "Stage 1",
    "alertTimestamp": "2025-12-29T08:30:15Z"
  }
}
```

### HypertensiveCrisisDetected
```json
{
  "eventId": "uuid",
  "eventType": "HypertensiveCrisisDetected",
  "timestamp": "2025-12-29T08:30:15Z",
  "data": {
    "readingId": "uuid",
    "userId": "uuid",
    "systolic": 185,
    "diastolic": 125,
    "crisisType": "emergency",
    "detectionTime": "2025-12-29T08:30:15Z"
  }
}
```

## Database Schema

### Alerts Table
```sql
CREATE TABLE Alerts (
    AlertId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    ReadingId UNIQUEIDENTIFIER NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Severity NVARCHAR(20) NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Message NVARCHAR(1000) NOT NULL,
    ActionRecommended NVARCHAR(500),
    Acknowledged BIT DEFAULT 0,
    AcknowledgedAt DATETIME2 NULL,
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (ReadingId) REFERENCES Readings(ReadingId),
    INDEX IX_Alerts_UserId_CreatedAt (UserId, CreatedAt DESC),
    INDEX IX_Alerts_Severity (Severity)
);
```

### AlertSettings Table
```sql
CREATE TABLE AlertSettings (
    UserId UNIQUEIDENTIFIER PRIMARY KEY,
    EnableAlerts BIT DEFAULT 1,
    HighBPSystolicThreshold INT DEFAULT 140,
    HighBPDiastolicThreshold INT DEFAULT 90,
    LowBPSystolicThreshold INT DEFAULT 90,
    LowBPDiastolicThreshold INT DEFAULT 60,
    NotificationChannels NVARCHAR(200) DEFAULT 'push,email',
    QuietHoursEnabled BIT DEFAULT 0,
    QuietHoursStart TIME NULL,
    QuietHoursEnd TIME NULL,
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```

## Notification Service Integration

### Push Notifications
- Use Firebase Cloud Messaging (FCM) or similar
- Store device tokens in UserDevices table
- Send to all user's registered devices

### Email Notifications
- Use SendGrid or AWS SES
- HTML email templates for each alert type
- Include reading details and recommended actions

### SMS Notifications (Optional)
- Use Twilio or similar
- Only for critical alerts
- Requires verified phone number

## Error Handling

### Error Codes
- `AS-001`: Alert not found
- `AS-002`: Unauthorized access to alert
- `AS-003`: Alert already acknowledged
- `AS-004`: Invalid threshold values
- `AS-005`: Notification delivery failed

## Performance Considerations

- Alert evaluation must be fast (<500ms)
- Use message queue for notification delivery (don't block API response)
- Index alerts by severity for priority queries
- Archive acknowledged alerts older than 1 year
