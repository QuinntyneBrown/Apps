# Backend Requirements - Time Logging

## Domain Events
- TimeEntryLogged
- ManualTimeAdjusted
- AutomaticTimeDetected
- BulkTimeLogged

## API Endpoints

### POST /api/time-entries
Create a new time entry
- Request: { activityType, startTime, endTime, category, location, energyLevel }
- Response: { entryId, duration, timestamp }
- Validation: Check for overlaps, validate time range

### PUT /api/time-entries/{id}
Update existing time entry
- Request: { originalValues, adjustedValues, adjustmentReason }
- Response: { entryId, updatedFields, timestamp }
- Event: ManualTimeAdjusted

### POST /api/time-entries/bulk
Log multiple time entries at once
- Request: { entries[], timePeriod, loggingMethod }
- Response: { batchId, entryCount, createdEntries[] }
- Event: BulkTimeLogged

### GET /api/time-entries
Retrieve time entries with filtering
- Query params: startDate, endDate, category, limit, offset
- Response: { entries[], totalCount, hasMore }

### POST /api/time-entries/automatic
Record automatically detected time entry
- Request: { detectionMethod, confidenceLevel, autoCategorization }
- Response: { entryId, requiresConfirmation }
- Event: AutomaticTimeDetected

## Data Models

### TimeEntry
```typescript
{
  id: UUID,
  userId: UUID,
  activityType: string,
  startTime: DateTime,
  endTime: DateTime,
  duration: number, // in minutes
  category: string,
  location?: string,
  energyLevel: number, // 1-10
  createdAt: DateTime,
  updatedAt: DateTime,
  isAutomatic: boolean,
  confidence?: number
}
```

### TimeEntryAdjustment
```typescript
{
  id: UUID,
  entryId: UUID,
  originalValues: object,
  adjustedValues: object,
  adjustmentReason: string,
  adjustedAt: DateTime,
  adjustedBy: UUID
}
```

## Business Logic
- Calculate duration automatically from start and end times
- Detect overlapping time entries and warn users
- Validate energy level is between 1-10
- Support timezone-aware time tracking
- Maintain audit trail of all adjustments
- Auto-categorize entries based on patterns when confidence > 0.8

## Database Schema
```sql
CREATE TABLE time_entries (
    id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    activity_type VARCHAR(255) NOT NULL,
    start_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NOT NULL,
    duration INTEGER NOT NULL,
    category VARCHAR(100),
    location VARCHAR(255),
    energy_level INTEGER CHECK (energy_level BETWEEN 1 AND 10),
    is_automatic BOOLEAN DEFAULT FALSE,
    confidence DECIMAL(3,2),
    created_at TIMESTAMP DEFAULT NOW(),
    updated_at TIMESTAMP DEFAULT NOW(),
    FOREIGN KEY (user_id) REFERENCES users(id)
);

CREATE TABLE time_entry_adjustments (
    id UUID PRIMARY KEY,
    entry_id UUID NOT NULL,
    original_values JSONB,
    adjusted_values JSONB,
    adjustment_reason TEXT,
    adjusted_at TIMESTAMP DEFAULT NOW(),
    adjusted_by UUID,
    FOREIGN KEY (entry_id) REFERENCES time_entries(id),
    FOREIGN KEY (adjusted_by) REFERENCES users(id)
);

CREATE INDEX idx_time_entries_user_date ON time_entries(user_id, start_time);
CREATE INDEX idx_time_entries_category ON time_entries(category);
```

## Integration Points
- Calendar sync service for importing scheduled events
- App usage tracking service for automatic detection
- Notification service for logging reminders
