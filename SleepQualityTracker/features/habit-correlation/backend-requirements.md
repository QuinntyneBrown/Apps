# Backend Requirements - Habit Correlation

## API Endpoints

### POST /api/habits
Log a habit
- **Request Body**: habitType, timing, intensity, quantity, date, userId
- **Events**: `HabitLogged`

### GET /api/habits/{userId}
Get user's habits
- **Query Parameters**: startDate, endDate, habitType

### POST /api/habits/correlations/analyze
Analyze habit correlations
- **Request Body**: userId, analysisType (caffeine, exercise, etc.)
- **Events**: `SleepHabitCorrelationIdentified`, `CaffeineImpactAnalyzed`, `ExerciseTimingOptimized`

### GET /api/habits/correlations/{userId}
Get identified correlations

## Domain Models
### Habit: Id, UserId, HabitType, Timing, Intensity, Quantity, LogDate
### HabitCorrelation: Id, UserId, HabitType, CorrelationStrength, ImpactOnSleep, ConfidenceLevel

## Business Logic
- Statistical correlation analysis (Pearson coefficient)
- Minimum 14 days of data required
- Significance threshold: p < 0.05
- Analyze timing impact for caffeine (cutoff time), exercise (optimal window)
- Track habits: caffeine, alcohol, exercise, screen time, meals, stress

## Events
HabitLogged, SleepHabitCorrelationIdentified, CaffeineImpactAnalyzed, ExerciseTimingOptimized

## Database Schema
Habits table, HabitCorrelations table, both indexed by UserId and date
