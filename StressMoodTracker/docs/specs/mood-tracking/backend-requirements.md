# Backend Requirements - Mood Tracking

## Domain Events
- MoodEntryLogged
- MoodSwingDetected
- PositiveMoodStreakAchieved

## API Endpoints
- POST /api/mood/entries - Log mood entry
- GET /api/mood/entries - Get mood history
- GET /api/mood/swings - Get detected mood swings
- GET /api/mood/streaks - Get mood streaks

## Data Models
```typescript
MoodEntry {
  id: UUID,
  userId: UUID,
  moodRating: number, // 1-10
  emotionTags: string[],
  intensityLevel: number,
  context: string,
  timestamp: DateTime
}
MoodSwing {
  id: UUID,
  userId: UUID,
  previousMood: number,
  currentMood: number,
  changeMagnitude: number,
  timeBetween: number,
  potentialTriggers: string[]
}
```

## Business Logic
- Detect mood swings when change > 3 points within 24 hours
- Track positive streaks (mood ≥ 7 for consecutive days)
- Calculate average mood per day/week/month
- Identify mood patterns by time of day
