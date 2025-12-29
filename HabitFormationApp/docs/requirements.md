# HabitFormationApp - Consolidated Requirements

## Overview
The HabitFormationApp is a comprehensive habit tracking application that helps users build and maintain positive habits through streaks, accountability, analytics, and intelligent reminders.

## Table of Contents
1. [Habit Management](#habit-management)
2. [Completion Tracking](#completion-tracking)
3. [Streak Management](#streak-management)
4. [Accountability](#accountability)
5. [Motivation](#motivation)
6. [Reminders](#reminders)
7. [Analytics](#analytics)

---

## 1. Habit Management

### Purpose
Enable users to create, modify, archive, and reactivate habits with flexible scheduling and categorization.

### Domain Events
- **HabitCreated**: When a new habit is created
- **HabitModified**: When habit details are updated
- **HabitArchived**: When a habit is archived
- **HabitReactivated**: When an archived habit is reactivated

### Key Features
- Create habits with name, description, category, frequency
- Daily, weekly, or custom frequency patterns
- Start date configuration (can backdate up to 30 days)
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- Reminder settings integration
- Archive inactive habits (data retained)
- Reactivate archived habits
- Search and filter habits by category
- Sort by name, created date, streak, category

### API Endpoints
- `POST /api/habits` - Create new habit
- `PUT /api/habits/{id}` - Modify habit
- `POST /api/habits/{id}/archive` - Archive habit
- `POST /api/habits/{id}/reactivate` - Reactivate habit
- `GET /api/habits/{id}` - Get habit details
- `GET /api/habits/user/{userId}` - Get active habits
- `GET /api/habits/user/{userId}/archived` - Get archived habits

### Business Rules
- Habit names must be unique per user
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs the specified function correctly
  - **AC3**: Feature handles error conditions gracefully
- Users can have unlimited habits
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- Archived habits hidden but data retained
- Reactivating resets streak
- StartDate can be backdated up to 30 days
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- Habits only deletable if no completion history

---

## 2. Completion Tracking

### Purpose
Log habit completions, track completion history, and manage completion records with support for late completions and undo functionality.

### Domain Events
- **HabitCompletionLogged**: When user marks habit complete
- **HabitCompletionUndone**: When completion record removed
- **LateCompletionLogged**: When completion logged after scheduled time

### Key Features
- Quick one-tap completion
- Detailed completion with notes, duration, location
- Late completion detection and flagging
- Undo completions (within 7 days)
- Completion calendar view
- Today's progress dashboard
- Completion history with filtering
- Streak calculation integration

### API Endpoints
- `POST /api/completions` - Log completion
- `DELETE /api/completions/{id}` - Undo completion
- `GET /api/completions/{id}` - Get completion details
- `GET /api/completions/habit/{habitId}` - Get habit completions
- `GET /api/completions/user/{userId}/today` - Today's completions
- `GET /api/completions/habit/{habitId}/streak` - Get current streak
- `GET /api/completions/habit/{habitId}/stats` - Get completion stats

### Business Rules
- One completion per calendar day for daily habits
- Cannot log completions for future dates
- Cannot log completions before habit start date
- Late completions flagged after midnight (daily) or scheduled time
- Completions undoable within 7 days only
- Cannot complete archived habits
- Duplicate prevention for same habit/day

---

## 3. Streak Management

### Purpose
Track consecutive habit completions, celebrate milestones, protect streaks with freeze tokens, and support recovery options.

### Domain Events
- **StreakStarted**: When user begins new streak
- **StreakMilestoneReached**: When streak hits milestone (7, 30, 100 days)
- **StreakBroken**: When streak is interrupted
- **StreakRecovered**: When user recovers broken streak

### Key Features
- Automatic streak calculation
- Milestone tracking (3, 7, 14, 30, 100, 365 days)
- Streak freeze tokens
- Visual streak indicators with color coding
- Longest streak history
- Streak calendar visualization
- Leaderboards (friends/global)
- Grace period for late completions

### API Endpoints
- `GET /api/streaks/habit/{habitId}` - Get current streak
- `GET /api/streaks/habit/{habitId}/history` - Get streak history
- `POST /api/streaks/{id}/freeze` - Use freeze token
- `POST /api/streaks/{id}/recover` - Recover broken streak
- `GET /api/streaks/leaderboard` - Get streak rankings
- `GET /api/streaks/milestones/{habitId}` - Get milestone progress

### Business Rules
- Daily habits: one completion per calendar day maintains streak
- Weekly habits: meeting weekly target maintains streak
- Missing scheduled day breaks streak
- Freeze tokens earned through achievements
- Max X freezes per month
- Freeze must be used within 48 hours of missed day
- Premium users get more freeze tokens
- 24-hour grace period for late completions

---

## 4. Accountability

### Purpose
Enable users to add accountability partners for progress monitoring, encouragement, and support when struggling.

### Domain Events
- **AccountabilityPartnerAdded**: When partner invitation sent
- **AccountabilityCheckInCompleted**: When check-in completed
- **PartnerEncouragementSent**: When encouragement sent
- **PartnerAlertTriggered**: When partner notified of issues

### Key Features
- Add/invite accountability partners
- Set permission levels (view, comment, encourage)
- Share specific habits or all habits
- Weekly check-ins with mood tracking
- Send encouragement messages
- Partner progress visibility
- Automatic alerts for struggling users
- Remove partners anytime

### API Endpoints
- `POST /api/accountability/partners` - Add partner
- `GET /api/accountability/partners` - Get partnerships
- `POST /api/accountability/checkins` - Complete check-in
- `POST /api/accountability/encouragement` - Send encouragement
- `GET /api/accountability/partners/{id}/progress` - View partner progress

### Business Rules
- Max 5 active accountability partners
- Partners must accept invitation
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs the specified function correctly
  - **AC3**: Feature handles error conditions gracefully
- Visibility based on permission level
- Auto-alert after 3+ consecutive misses
- Weekly check-in reminders
- Privacy controls on shared data
- Can report/block inappropriate behavior

---

## 5. Motivation

### Purpose
Track achievements, detect motivation dips, celebrate personal bests, and provide personalized encouragement through gamification.

### Domain Events
- **MilestoneAchieved**: When significant achievement reached
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- **MotivationDipDetected**: When declining engagement detected
- **PersonalBestSet**: When new personal record set

### Key Features
- Achievement badges and points system
- Motivation score (0-100) with trend tracking
- Personal best tracking
- Motivation dip detection with interventions
- Success celebration animations
- Achievement sharing
- Level progression
- Daily challenges

### Achievement Types
1. **Streak Achievements**: 3, 7, 14, 30, 100, 365 days
2. **Completion Achievements**: 1, 10, 50, 100, 500 completions
3. **Consistency Achievements**: Perfect week, perfect month
4. **Category Achievements**: Health champion, learning leader
5. **Social Achievements**: Team player, encourager

### API Endpoints
- `GET /api/motivation/achievements` - Get achievements
- `GET /api/motivation/score` - Get motivation score
- `GET /api/motivation/personal-bests` - Get personal records
- `GET /api/motivation/available` - Get locked achievements

### Business Rules
- Motivation score calculated daily
- Dip triggers when score <40 for 2+ weeks
- Interventions include easier habits, partner notification
- Points redeemable for features
- Achievements cannot be lost once earned
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback

---

## 6. Reminders

### Purpose
Send timely notifications to help users complete habits with intelligent scheduling and response tracking.

### Domain Events
- **ReminderScheduled**: When reminder created
- **ReminderActedUpon**: When user completes after reminder
- **ReminderIgnored**: When reminder expires without action

### Key Features
- Time-based reminders (scheduled times)
- Advance reminders (15, 30, 60 mins before)
- Multiple notification channels (push, email, SMS)
- Smart scheduling based on past behavior
- Snooze functionality
- Quiet hours configuration
- Reminder effectiveness tracking
- Adaptive timing optimization

### API Endpoints
- `GET /api/reminders/habit/{habitId}/schedule` - Get schedule
- `PUT /api/reminders/habit/{habitId}/schedule` - Update schedule
- `GET /api/reminders/upcoming` - Get upcoming reminders
- `POST /api/reminders/{id}/snooze` - Snooze reminder
- `GET /api/reminders/effectiveness` - Get effectiveness metrics

### Business Rules
- Max 10 reminders per day per user
- Quiet hours respected (default 10 PM - 7 AM)
- Max 3 snoozes per reminder
- Auto-expire after 24 hours
- Don't remind for completed habits
- Response time tracking for optimization

---

## 7. Analytics

### Purpose
Provide insights into habit patterns, success factors, and trends to help users optimize their habit formation.

### Domain Events
- **WeeklyProgressReportGenerated**: When weekly summary created
- **HabitPatternIdentified**: When behavioral pattern discovered
- **SuccessFactorAnalyzed**: When correlation found

### Key Features
- Weekly progress reports
- Trend visualization (completion rates, streaks)
- Pattern identification (time, day, behavior)
- Success factor analysis
- Personalized insights and recommendations
- Comparison views (week-to-week, month-to-month)
- PDF/CSV export
- AI-generated coaching messages

### Pattern Types
1. **Time-based**: Preferred completion times, day-of-week patterns
2. **Behavior**: Completion streaks, habit groupings, failure patterns
3. **Context**: Location correlations, weather impact, social influence

### Success Factors Analyzed
- Time of day effectiveness
- Day of week performance
- Frequency optimization
- Accountability partner impact
- Environmental triggers

### API Endpoints
- `GET /api/analytics/weekly/{weekStartDate}` - Get weekly report
- `GET /api/analytics/trends` - Get trends
- `GET /api/analytics/patterns` - Get identified patterns
- `GET /api/analytics/success-factors` - Get success factors
- `GET /api/analytics/insights` - Get personalized insights
- `POST /api/analytics/reports/generate` - Generate report

### Business Rules
- Weekly reports generated every Sunday night
- Pattern confidence threshold: 70%
- Minimum sample size: 7 data points
- Privacy controls on data analysis
- Opt-out available for ML analysis

---

## Cross-Feature Integration

### Event Flow
1. **Habit Created** → Initialize Streak, Schedule Reminders
2. **Completion Logged** → Update Streak, Mark Reminder Acted Upon, Update Analytics
3. **Streak Milestone** → Award Achievement, Celebrate
4. **Motivation Dip** → Alert Partners, Suggest Interventions
5. **Weekly Report Generated** → Email Summary, Update Insights

### Shared Services
- **Event Publisher**: Distributes domain events
- **Notification Service**: Sends all notifications
- **User Service**: Manages user data and authentication
- **Analytics Service**: Aggregates data across features

### Data Dependencies
- Completions → Streaks → Achievements
- Completions → Analytics → Insights
- Reminders → Completions → Effectiveness
- Habits → All Features

---

## Technical Requirements

### Backend Stack
- .NET 8 / ASP.NET Core Web API
- SQL Server database
- Domain-Driven Design (DDD) architecture
- CQRS pattern with MediatR
- Event sourcing for domain events
- Repository pattern for data access
- Background jobs with Hangfire
- JWT authentication
- SignalR for real-time updates

### Frontend Stack
- React 18+ or Next.js
- TypeScript
- State management: Redux Toolkit or Zustand
- UI framework: Tailwind CSS or Material-UI
- Charts: Recharts or Chart.js
- Forms: React Hook Form + Zod validation
- API client: Axios or React Query
- PWA support for offline functionality
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs the specified function correctly
  - **AC3**: Feature handles error conditions gracefully

### Infrastructure
- Docker containers
- Azure App Service or AWS deployment
- Azure SQL Database or AWS RDS
- Redis cache for performance
- Application Insights for monitoring
- CDN for static assets

### Security
- JWT token authentication
- Role-based access control
- Input validation and sanitization
- SQL injection prevention (parameterized queries)
- XSS protection
- CORS configuration
- Rate limiting
- HTTPS enforcement
- Data encryption at rest

### Performance
- Database indexing strategy
- Caching frequently accessed data
- Lazy loading for large datasets
- Virtual scrolling for long lists
- Image optimization
- Code splitting
- Background job processing
- Query optimization

### Testing
- Unit tests (xUnit, NUnit)
- Integration tests
- E2E tests (Playwright, Cypress)
- API tests (Postman/Newman)
- Performance tests
- Accessibility tests
- Target: >80% code coverage

---

## Non-Functional Requirements

### Availability
- 99.9% uptime SLA
- Graceful degradation
- Offline support for core features
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs the specified function correctly
  - **AC3**: Feature handles error conditions gracefully
- Automatic failover

### Scalability
- Support 100,000+ active users
- Horizontal scaling capability
- Database sharding for growth
- Caching strategy

### Performance
- API response time <200ms (p95)
- Page load time <2 seconds
- Real-time updates <500ms latency
- Background job processing within 5 minutes

### Accessibility
- WCAG 2.1 AA compliance
- Screen reader support
- Keyboard navigation
- High contrast mode
- Adjustable font sizes

### Compliance
- GDPR compliance
- CCPA compliance
- Data retention policies
- Privacy policy
- Terms of service
- Cookie consent

---

## Deployment Strategy

### Environments
- Development
- Staging
- Production

### CI/CD Pipeline
- Automated builds
- Unit test execution
- Integration test execution
- Security scanning
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- Code quality checks
- Automated deployment

### Monitoring
- Application performance monitoring
- Error tracking and logging
- User analytics
- Uptime monitoring
- Database performance
- API usage metrics

---

## Success Metrics

### User Engagement
- Daily active users (DAU)
- Monthly active users (MAU)
- Average habits per user
- Average completion rate
- Retention rate (D1, D7, D30)

### Feature Adoption
- Habits created
- Completions logged
- Streaks maintained
- Accountability partnerships
- Reminder effectiveness
- Report views

### Business Metrics
- User acquisition cost
- Lifetime value
- Conversion rate (free to premium)
- Churn rate
- Net Promoter Score (NPS)

---

## Future Enhancements

### Phase 2
- Social features (public challenges, community)
- Integration with wearables (Fitbit, Apple Watch)
- Voice commands (Alexa, Google Assistant)
- Habit templates marketplace
- Team/group habit tracking
- Coach/therapist dashboard

### Phase 3
- AI-powered habit recommendations
- Predictive analytics
- Behavioral science insights
- Mental health integration
- Corporate wellness programs
- White-label solutions

---

## Documentation

### User Documentation
- Getting started guide
- Feature tutorials
- FAQs
- Video walkthroughs
- Best practices

### Developer Documentation
- API documentation (Swagger/OpenAPI)
- Architecture overview
- Database schema
- Deployment guide
- Contributing guidelines

---

## Support & Maintenance

### Support Channels
- In-app chat support
- Email support
- Knowledge base
- Community forum
- Social media

### Maintenance Windows
- Weekly: Sunday 2-4 AM UTC
- Emergency: As needed
- Notification: 48 hours advance

---

*Last Updated: December 28, 2025*
*Version: 1.0*
