# LetterToFutureSelf - System Requirements

## Executive Summary

LetterToFutureSelf is a personal growth and self-reflection platform that enables users to write letters to their future selves, schedule delivery dates, track goals and predictions, create time capsules, and reflect on personal growth over time.

## Business Goals

- Facilitate meaningful self-reflection and personal growth
- Create anticipation and excitement for future milestones
- Track personal development over time
- Preserve thoughts, emotions, and life circumstances
- Encourage goal-setting and accountability
- Provide insights into personal change and growth patterns

## System Purpose
- Compose and schedule letters to future self
- Create time capsules with multiple letters and media
- Track goals, predictions, and personal aspirations
- Capture emotional states and life circumstances
- Deliver letters at scheduled future dates
- Facilitate reflection on past thoughts and growth
- Visualize personal journey over time
- Generate writing prompts for inspiration

## Core Features

### 1. Letter Writing & Management
- Compose letters with rich text editor
- Save drafts and edit before scheduling
- Set delivery dates (months or years in future)
- Add photos, voice memos, and attachments
- Tag letters by theme or life area
- Delete or reschedule undelivered letters

### 2. Letter Delivery & Reading
- Automatic delivery on scheduled date
- Email and push notifications
- Secure letter unlocking
- Reading experience with original context
- Time elapsed since writing displayed
- Add reflections after reading

### 3. Time Capsule Creation
- Group multiple letters into capsules
- Add photos, videos, documents
- Set capsule opening date
- Seal capsule to prevent editing
- Batch delivery of capsule contents
- Special opening ceremony experience

### 4. Goal & Prediction Tracking
- Declare goals in letters
- Track goal progress over time
- Make predictions about future
- Evaluate prediction accuracy
- Visualize goal achievement rates
- Identify patterns in aspirations

### 5. Emotional & Mood Tracking
- Log mood when writing
- Capture life circumstances
- Compare emotional states over time
- Detect significant emotional contrasts
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- Visualize emotional timeline
- Gain insights into resilience

### 6. Writing Prompts & Inspiration
- Curated writing prompts library
- Categorized prompts (gratitude, goals, fears, hopes)
- Prompt suggestions based on user history
- Track prompt effectiveness
- Custom prompt creation

### 7. Archive & Timeline
- Browse all delivered letters
- Search letters by content, date, mood
- Visualize letter timeline
- Organize with tags and categories
- Export letters for backup
- Create personal growth reports

## Domain Events

### Letter Events
- **LetterCreated**: New letter written
- **LetterScheduled**: Letter delivery date set
- **LetterDelivered**: Letter reached delivery date
- **LetterRead**: User opened delivered letter
- **LetterReflectionAdded**: Reflection notes added
- **LetterRescheduled**: Delivery date changed
- **LetterDeleted**: Letter removed before delivery

### Time Capsule Events
- **TimeCapsuleCreated**: New capsule created
- **CapsuleItemAdded**: Content added to capsule
- **CapsuleSealed**: Capsule locked for delivery
- **CapsuleOpened**: Capsule delivered and opened

### Goal Tracking Events
- **FutureGoalDeclared**: Goal stated in letter
- **GoalProgressEvaluated**: Goal progress assessed
- **PredictionMade**: Prediction about future
- **PredictionVerified**: Prediction checked against reality

### Prompt Events
- **WritingPromptGenerated**: Prompt suggested
- **PromptUsed**: Letter written from prompt

### Emotional Events
- **MoodCaptured**: Emotional state recorded
- **EmotionalContrastDetected**: Mood change identified

## Technical Architecture

### Backend
- .NET 8.0 Web API
- SQL Server database
- Domain-driven design with domain events
- CQRS pattern
- Scheduled background jobs for delivery
- Encryption for letter content

### Frontend
- Modern SPA (Single Page Application)
- Rich text editor for letter writing
- Timeline visualization
- Responsive design
- Offline draft support
- Push notifications

## User Roles
- **Individual User**: Personal letter writing
- **Premium User**: Advanced features (unlimited letters, time capsules)


## Multi-Tenancy Support

### Tenant Isolation
- **FR-MT-1**: Support for multi-tenant architecture with complete data isolation
  - **AC1**: Each tenant's data is completely isolated from other tenants
  - **AC2**: All queries are automatically filtered by TenantId
  - **AC3**: Cross-tenant data access is prevented at the database level
- **FR-MT-2**: TenantId property on all aggregate entities
  - **AC1**: Every aggregate root has a TenantId property
  - **AC2**: TenantId is set during entity creation
  - **AC3**: TenantId cannot be modified after creation
- **FR-MT-3**: Automatic tenant context resolution
  - **AC1**: TenantId is extracted from JWT claims or HTTP headers
  - **AC2**: Invalid or missing tenant context is handled gracefully
  - **AC3**: Tenant context is available throughout the request pipeline


## Security Requirements
- End-to-end encryption for letter content
- Secure delivery mechanisms
- Privacy protection
- Data backup and recovery
- Account authentication

## Success Metrics
- Letters written per user > 5/year
- Delivery rate > 95%
- Reading rate within 7 days > 70%
- Reflection addition rate > 40%
- User retention > 60% after 1 year
- User satisfaction > 4.5/5

## Future Enhancements
- Voice-recorded letters
- Video letters to future self
- AI-powered insights and patterns
- Group time capsules
- Physical letter printing service
- Integration with journaling apps
