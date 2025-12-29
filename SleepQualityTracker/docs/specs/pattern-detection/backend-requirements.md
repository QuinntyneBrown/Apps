# Backend Requirements - Pattern Detection

## API Endpoints

### POST /api/patterns/detect/{userId}
Run pattern detection analysis
- **Events**: `SleepPatternDetected`, `WeekdayWeekendDiscrepancyIdentified`, `InsomniaPatternDetected`

### GET /api/patterns/{userId}
Get detected patterns

## Domain Models
### SleepPattern: Id, UserId, PatternType, Frequency, Characteristics, ConfidenceLevel, DetectedAt

## Business Logic
- ML/statistical analysis for pattern detection
- Weekday vs weekend comparison (2+ hour difference = social jetlag)
- Insomnia detection: difficulty falling asleep 3+ nights/week for 2+ weeks
- Minimum 30 days data for pattern detection
- Confidence threshold 70% for alerts

## Events
SleepPatternDetected, WeekdayWeekendDiscrepancyIdentified, InsomniaPatternDetected

## Database Schema
SleepPatterns table with userId and detectionDate indexing
