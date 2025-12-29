# WorkoutPlanBuilder - System Requirements

## Overview

WorkoutPlanBuilder is a comprehensive workout routine and fitness tracking system designed to help users create, manage, and track their fitness journey. The system enables users to build custom workout plans, log workout sessions, maintain an exercise library, track progress over time, and celebrate personal records.

## System Architecture

### Technology Stack
- **Backend**: .NET 8+ with Clean Architecture
- **Database**: SQL Server
- **Frontend**: Modern SPA framework (React/Angular/Vue)
- **Event-Driven**: Domain Events for decoupled communication
- **API**: RESTful API with CQRS pattern

### Architectural Patterns
- Clean Architecture / Onion Architecture
- Domain-Driven Design (DDD)
- CQRS (Command Query Responsibility Segregation)
- Event-Driven Architecture with Domain Events
- Repository Pattern
- Mediator Pattern (MediatR)

## Core Features

### 1. Workout Plans
Create and manage structured workout routines with scheduling and progression logic.

**Key Capabilities:**
- Create custom workout plans with multiple workouts
- Define workout structure (exercises, sets, reps, rest periods)
- Schedule workouts across weeks/months
- Clone and modify existing plans
- Share plans with other users
- Track plan completion status

**Domain Events:**
- `WorkoutPlanCreated`: Triggered when a new workout plan is created
- `WorkoutPlanStarted`: Triggered when a user begins a workout plan
- `WorkoutPlanCompleted`: Triggered when all workouts in a plan are finished
- `WorkoutPlanModified`: Triggered when a plan is updated

### 2. Workout Sessions
Log and track individual workout sessions with real-time progress updates.

**Key Capabilities:**
- Start/stop workout sessions with timer
- Log sets, reps, and weights for each exercise
- Add notes and comments to sessions
- Mark sessions as complete or skipped
- View session history and statistics
- Track workout duration and rest times

**Domain Events:**
- `WorkoutSessionStarted`: Triggered when a workout session begins
- `WorkoutSessionCompleted`: Triggered when a session is finished
- `WorkoutSessionSkipped`: Triggered when a scheduled session is skipped

### 3. Exercise Library
Maintain a comprehensive database of exercises with detailed information.

**Key Capabilities:**
- Browse exercises by category (strength, cardio, flexibility)
- Search and filter exercises by muscle group, equipment, difficulty
- View exercise details (instructions, form tips, video links)
- Add custom exercises to personal library
- Track exercise performance history
- Substitute exercises in workout plans

**Domain Events:**
- `ExerciseAddedToLibrary`: Triggered when a new exercise is added
- `ExercisePerformed`: Triggered when an exercise is logged in a session
- `ExerciseSubstituted`: Triggered when an exercise is replaced in a plan

### 4. Progress Tracking
Visualize and analyze fitness progress over time with charts and metrics.

**Key Capabilities:**
- Track body measurements (weight, body fat %, measurements)
- View strength progression charts by exercise
- Monitor workout frequency and consistency
- Calculate volume (sets x reps x weight) trends
- Set and track fitness goals
- Generate progress reports

**Key Metrics:**
- Total workouts completed
- Total volume lifted
- Average workout duration
- Consistency streaks
- Exercise-specific PRs
- Body composition changes

### 5. Personal Records
Celebrate and track personal bests across all exercises and metrics.

**Key Capabilities:**
- Automatically detect new personal records
- Track one-rep max (1RM) estimates
- Record best sets (max weight, max reps, max volume)
- View PR history timeline
- Compare current performance to PRs
- Share PRs with community

**PR Types:**
- Heaviest weight for given reps
- Most reps at given weight
- Highest volume in single set
- Fastest time (for cardio/timed exercises)
- Best calculated 1RM

## Domain Model

### Core Entities

#### WorkoutPlan
- Id (Guid)
- Name (string)
- Description (string)
- CreatedBy (UserId)
- CreatedDate (DateTime)
- Status (enum: Draft, Active, Completed, Archived)
- DurationWeeks (int)
- Workouts (Collection<Workout>)
- IsPublic (bool)

#### Workout
- Id (Guid)
- WorkoutPlanId (Guid)
- Name (string)
- DayOfWeek (enum)
- OrderIndex (int)
- Exercises (Collection<WorkoutExercise>)
- EstimatedDuration (TimeSpan)

#### WorkoutSession
- Id (Guid)
- WorkoutPlanId (Guid?)
- WorkoutId (Guid?)
- UserId (Guid)
- StartTime (DateTime)
- EndTime (DateTime?)
- Status (enum: InProgress, Completed, Skipped)
- Notes (string)
- ExerciseSets (Collection<ExerciseSet>)
- ActualDuration (TimeSpan)

#### Exercise
- Id (Guid)
- Name (string)
- Description (string)
- Category (enum: Strength, Cardio, Flexibility, Other)
- MuscleGroup (enum: Chest, Back, Legs, Shoulders, Arms, Core, FullBody)
- Equipment (enum: Barbell, Dumbbell, Machine, Bodyweight, Cable, Other)
- Difficulty (enum: Beginner, Intermediate, Advanced)
- Instructions (string)
- VideoUrl (string?)
- IsCustom (bool)
- CreatedBy (UserId?)

#### WorkoutExercise
- Id (Guid)
- WorkoutId (Guid)
- ExerciseId (Guid)
- OrderIndex (int)
- TargetSets (int)
- TargetReps (string) // e.g., "8-12", "15", "AMRAP"
- TargetWeight (decimal?)
- RestSeconds (int)
- Notes (string)

#### ExerciseSet
- Id (Guid)
- WorkoutSessionId (Guid)
- ExerciseId (Guid)
- SetNumber (int)
- Reps (int)
- Weight (decimal)
- WeightUnit (enum: Pounds, Kilograms)
- IsWarmup (bool)
- CompletedAt (DateTime)
- Notes (string)

#### PersonalRecord
- Id (Guid)
- UserId (Guid)
- ExerciseId (Guid)
- RecordType (enum: MaxWeight, MaxReps, MaxVolume, Best1RM, FastestTime)
- Value (decimal)
- Reps (int?)
- Weight (decimal?)
- AchievedDate (DateTime)
- WorkoutSessionId (Guid)
- Notes (string)

#### ProgressMeasurement
- Id (Guid)
- UserId (Guid)
- MeasurementDate (DateTime)
- Weight (decimal?)
- BodyFatPercentage (decimal?)
- Measurements (JSON) // chest, waist, arms, legs, etc.
- Notes (string)
- PhotoUrl (string?)

## Domain Events

### Workout Plan Events
```csharp
public class WorkoutPlanCreatedEvent : DomainEvent
{
    public Guid WorkoutPlanId { get; set; }
    public Guid UserId { get; set; }
    public string PlanName { get; set; }
    public int DurationWeeks { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class WorkoutPlanStartedEvent : DomainEvent
{
    public Guid WorkoutPlanId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
}

public class WorkoutPlanCompletedEvent : DomainEvent
{
    public Guid WorkoutPlanId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CompletedDate { get; set; }
    public int TotalWorkoutsCompleted { get; set; }
    public int TotalWorkoutsSkipped { get; set; }
}

public class WorkoutPlanModifiedEvent : DomainEvent
{
    public Guid WorkoutPlanId { get; set; }
    public Guid UserId { get; set; }
    public string ModificationType { get; set; } // Added, Removed, Updated
    public DateTime ModifiedDate { get; set; }
}
```

### Workout Session Events
```csharp
public class WorkoutSessionStartedEvent : DomainEvent
{
    public Guid WorkoutSessionId { get; set; }
    public Guid UserId { get; set; }
    public Guid? WorkoutPlanId { get; set; }
    public DateTime StartTime { get; set; }
}

public class WorkoutSessionCompletedEvent : DomainEvent
{
    public Guid WorkoutSessionId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CompletedTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int ExercisesCompleted { get; set; }
    public int TotalSets { get; set; }
    public decimal TotalVolume { get; set; }
}

public class WorkoutSessionSkippedEvent : DomainEvent
{
    public Guid WorkoutSessionId { get; set; }
    public Guid UserId { get; set; }
    public DateTime SkippedDate { get; set; }
    public string Reason { get; set; }
}
```

### Exercise Events
```csharp
public class ExerciseAddedToLibraryEvent : DomainEvent
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public string Category { get; set; }
    public bool IsCustom { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class ExercisePerformedEvent : DomainEvent
{
    public Guid ExerciseId { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutSessionId { get; set; }
    public int TotalSets { get; set; }
    public decimal MaxWeight { get; set; }
    public decimal TotalVolume { get; set; }
    public DateTime PerformedDate { get; set; }
}

public class ExerciseSubstitutedEvent : DomainEvent
{
    public Guid WorkoutPlanId { get; set; }
    public Guid OriginalExerciseId { get; set; }
    public Guid SubstituteExerciseId { get; set; }
    public Guid UserId { get; set; }
    public string Reason { get; set; }
    public DateTime SubstitutedDate { get; set; }
}
```

## Event Handlers

### Analytics and Reporting
- Track user engagement metrics from session events
- Generate workout completion statistics
- Calculate aggregate volume and intensity metrics

### Notifications
- Send notifications when workout session is completed
- Alert users about new personal records
- Remind users about scheduled workouts

### Progress Calculation
- Update progress charts when measurements are recorded
- Recalculate personal records when new sets are logged
- Update workout plan progress percentages

### Gamification
- Award badges for consistency streaks
- Track milestone achievements
- Calculate leaderboard rankings

## User Roles

### Standard User
- Create and manage personal workout plans
- Log workout sessions
- Track personal progress and PRs
- Access exercise library
- View own statistics

### Premium User
- All Standard User features
- Advanced analytics and insights
- Custom exercise creation
- Workout plan sharing
- Export data and reports

### Coach/Trainer
- All Premium features
- Create plans for clients
- Monitor client progress
- Bulk exercise management
- Client management tools

### Administrator
- System configuration
- User management
- Exercise library curation
- Analytics dashboard
- Content moderation

## Non-Functional Requirements

### Performance
- API response time < 200ms for 95% of requests
- Support 1000+ concurrent users
- Real-time session updates with minimal latency
- Optimized database queries with proper indexing

### Security
- Authentication and authorization (JWT tokens)
- Role-based access control (RBAC)
- Data encryption at rest and in transit
- SQL injection prevention
- XSS protection
- CORS configuration

### Scalability
- Horizontal scaling capability
- Database connection pooling
- Caching strategy (Redis/In-Memory)
- Async processing for heavy operations
- Event queue for domain events

### Reliability
- 99.9% uptime SLA
- Automated backups (daily)
- Error logging and monitoring
- Graceful degradation
- Transaction management for data consistency

### Usability
- Responsive design (mobile, tablet, desktop)
- Intuitive navigation
- Fast data entry during workouts
- Offline capability for session logging
- Accessibility compliance (WCAG 2.1)

## API Endpoints

### Workout Plans
- `GET /api/workout-plans` - List user's workout plans
- `GET /api/workout-plans/{id}` - Get plan details
- `POST /api/workout-plans` - Create new plan
- `PUT /api/workout-plans/{id}` - Update plan
- `DELETE /api/workout-plans/{id}` - Delete plan
- `POST /api/workout-plans/{id}/start` - Start a plan
- `POST /api/workout-plans/{id}/complete` - Mark plan complete

### Workout Sessions
- `GET /api/workout-sessions` - List user's sessions
- `GET /api/workout-sessions/{id}` - Get session details
- `POST /api/workout-sessions` - Start new session
- `PUT /api/workout-sessions/{id}` - Update session
- `POST /api/workout-sessions/{id}/complete` - Complete session
- `POST /api/workout-sessions/{id}/skip` - Skip session
- `POST /api/workout-sessions/{id}/sets` - Log exercise set

### Exercise Library
- `GET /api/exercises` - List exercises (with filters)
- `GET /api/exercises/{id}` - Get exercise details
- `POST /api/exercises` - Create custom exercise
- `PUT /api/exercises/{id}` - Update exercise
- `DELETE /api/exercises/{id}` - Delete custom exercise
- `GET /api/exercises/{id}/history` - Get performance history

### Progress Tracking
- `GET /api/progress/measurements` - Get body measurements
- `POST /api/progress/measurements` - Log new measurement
- `GET /api/progress/charts` - Get progress chart data
- `GET /api/progress/statistics` - Get workout statistics
- `GET /api/progress/goals` - Get user goals
- `POST /api/progress/goals` - Create new goal

### Personal Records
- `GET /api/personal-records` - List user's PRs
- `GET /api/personal-records/exercise/{exerciseId}` - PRs for exercise
- `GET /api/personal-records/recent` - Recent PRs
- `POST /api/personal-records` - Manually add PR
- `GET /api/personal-records/history/{exerciseId}` - PR history

## Database Schema Considerations

### Indexes
- UserId on all user-specific tables
- WorkoutPlanId, WorkoutSessionId for relationships
- ExerciseId for exercise lookups
- CompletedAt, AchievedDate for temporal queries
- Composite indexes for common query patterns

### Relationships
- One-to-Many: User -> WorkoutPlans, WorkoutSessions, PersonalRecords
- One-to-Many: WorkoutPlan -> Workouts -> WorkoutExercises
- One-to-Many: WorkoutSession -> ExerciseSets
- Many-to-One: Exercise references from multiple tables

### Data Retention
- Keep workout session data indefinitely
- Archive completed plans after 2 years
- Retain progress measurements permanently
- Clean up draft plans after 90 days of inactivity

## Integration Points

### External Services
- Email service for notifications
- Cloud storage for exercise videos/photos
- Analytics service (Google Analytics, Mixpanel)
- Payment processing (for premium features)
- Social sharing APIs

### Webhooks
- New PR achieved
- Workout plan completed
- Milestone reached
- Weekly summary available

## Testing Strategy

### Unit Tests
- Domain logic validation
- Business rule enforcement
- Event handler logic
- Calculation accuracy (volume, 1RM, etc.)

### Integration Tests
- API endpoint functionality
- Database operations
- Event publishing/handling
- Authentication/authorization

### End-to-End Tests
- Complete workout session flow
- Plan creation to completion
- PR detection and recording
- Progress tracking updates

## Deployment

### Environments
- Development
- Staging
- Production

### CI/CD Pipeline
- Automated testing
- Code quality checks
- Database migrations
- Docker containerization
- Blue-green deployment

## Future Enhancements

- Mobile applications (iOS/Android)
- Wearable device integration
- AI-powered workout recommendations
- Social features and community
- Nutrition tracking integration
- Video form analysis
- Live coaching sessions
- Marketplace for workout plans
