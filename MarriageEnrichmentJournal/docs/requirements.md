# Requirements - Marriage Enrichment Journal

## Overview
The Marriage Enrichment Journal is a web application designed to help couples strengthen their relationship through gratitude practices, appreciation sharing, reflective journaling, and intentional communication. The application provides tools for both individual and joint reflections, tracks relationship health trends, and celebrates growth milestones.

## Target Users
- Married couples seeking to deepen their relationship
- Couples in committed long-term relationships
- Pre-marital couples preparing for marriage
- Marriage counselors and therapists (observer role)

## Core Features

### 1. Gratitude Journaling
**Description**: Enable partners to record and share what they're grateful for about each other.

**Functional Requirements**:
- FR-GJ-001: Users shall be able to create gratitude journal entries with content, category (action/quality/moment), and privacy level
- FR-GJ-002: Users shall be able to categorize gratitude by type (actions, qualities, moments)
- FR-GJ-003: Users shall be able to choose whether to share gratitude with their spouse immediately or keep it private
- FR-GJ-004: Users shall be able to view a feed of all their gratitude entries
- FR-GJ-005: Users shall be able to share previously private gratitude entries with their spouse
- FR-GJ-006: Receiving partner shall be notified when gratitude is shared with them
- FR-GJ-007: Users shall be able to acknowledge and respond to gratitude entries from their spouse
- FR-GJ-008: System shall track gratitude entry streaks and award milestones

**Non-Functional Requirements**:
- NFR-GJ-001: Gratitude entries shall be encrypted at rest
- NFR-GJ-002: Private entries shall only be accessible to the author until shared
- NFR-GJ-003: Notification delivery shall occur within 30 seconds of sharing

### 2. Appreciation Expression
**Description**: Allow partners to express specific appreciation for each other using love language frameworks.

**Functional Requirements**:
- FR-AE-001: Users shall be able to express appreciation with type (words/actions/qualities), specific instance, and intensity level
- FR-AE-002: System shall analyze appreciation patterns to identify love languages
- FR-AE-003: Users shall be able to view their partner's identified love language preferences
- FR-AE-004: Users shall receive personalized suggestions for expressing appreciation based on partner's love language
- FR-AE-005: System shall track appreciation reciprocity and balance
- FR-AE-006: Users shall be able to respond to appreciation with their own expressions

**Non-Functional Requirements**:
- NFR-AE-001: Love language identification shall achieve 80% confidence before displaying
- NFR-AE-002: Pattern analysis shall process within 2 seconds

### 3. Reflection Journaling
**Description**: Provide guided reflection prompts for individual and joint journaling.

**Functional Requirements**:
- FR-RJ-001: Users shall be able to complete guided reflection exercises from a prompt library
- FR-RJ-002: Users shall be able to record conflict reflections with self-awareness insights
- FR-RJ-003: Users shall be able to document growth moments and breakthroughs
- FR-RJ-004: Couples shall be able to complete weekly relationship reviews together
- FR-RJ-005: Users shall be able to set privacy level for reflections (private, spouse-visible, joint)
- FR-RJ-006: System shall identify themes and patterns in reflections
- FR-RJ-007: Users shall be able to view progress timeline of growth moments

**Non-Functional Requirements**:
- NFR-RJ-001: Reflection content shall support rich text formatting
- NFR-RJ-002: Theme identification shall update daily during off-peak hours

### 4. Communication Tools
**Description**: Facilitate deeper conversations through prompts and collaborative journaling.

**Functional Requirements**:
- FR-CT-001: System shall provide daily/weekly conversation starter prompts
- FR-CT-002: Users shall be able to answer prompts individually or together
- FR-CT-003: Couples shall be able to create joint journal entries collaboratively
- FR-CT-004: Users shall be able to set communication goals with success criteria
- FR-CT-005: Users shall be able to record communication wins and successful moments
- FR-CT-006: System shall track depth of sharing and conversation quality

**Non-Functional Requirements**:
- NFR-CT-001: Collaborative editing shall support real-time synchronization with <500ms latency
- NFR-CT-002: Prompt library shall contain minimum 500 curated prompts

### 5. Relationship Health Monitoring
**Description**: Track relationship satisfaction trends and provide insights.

**Functional Requirements**:
- FR-RH-001: Users shall be able to log relationship mood/satisfaction ratings
- FR-RH-002: Users shall be able to add contributing factors and notes to mood logs
- FR-RH-003: System shall detect and display mood trends over time
- FR-RH-004: System shall identify positive patterns in relationship behaviors
- FR-RH-005: System shall provide early warnings for negative trend detection
- FR-RH-006: Users shall be able to view relationship health dashboard with metrics
- FR-RH-007: System shall suggest interventions or resources based on trends

**Non-Functional Requirements**:
- NFR-RH-001: Trend analysis shall process data from last 90 days minimum
- NFR-RH-002: Dashboard shall load within 3 seconds

### 6. Milestones and Celebrations
**Description**: Help couples commemorate special moments and achievements.

**Functional Requirements**:
- FR-MC-001: Users shall be able to mark relationship milestones with type, date, and significance
- FR-MC-002: Users shall be able to attach photos and celebration notes to milestones
- FR-MC-003: System shall send anniversary and milestone reminders
- FR-MC-004: Users shall be able to view relationship timeline of all milestones
- FR-MC-005: System shall celebrate achievement of app usage milestones (streaks, entries, etc.)

**Non-Functional Requirements**:
- NFR-MC-001: Photo uploads shall support JPEG, PNG up to 10MB per image
- NFR-MC-002: Reminder notifications shall send 1 week and 1 day before milestone dates

### 7. Privacy and Security
**Description**: Ensure intimate journal content is protected and access-controlled.

**Functional Requirements**:
- FR-PS-001: Users shall be able to create private entries visible only to themselves
- FR-PS-002: Users shall be able to convert private entries to shared after delay
- FR-PS-003: Users shall be able to set default privacy levels for different content types
- FR-PS-004: System shall provide audit log of who viewed shared content
- FR-PS-005: Users shall be able to export their personal data

**Non-Functional Requirements**:
- NFR-PS-001: All journal content shall be encrypted using AES-256
- NFR-PS-002: Authentication shall use OAuth 2.0 with multi-factor authentication option
- NFR-PS-003: Sessions shall timeout after 30 minutes of inactivity
- NFR-PS-004: Data export shall complete within 5 minutes for average user data

## User Roles

### Individual Partner
- Create personal and shared journal entries
- View own entries and spouse-shared entries
- Set privacy levels
- Track personal streaks and achievements

### Couple (Joint Account)
- Access all features as a paired unit
- View combined insights and analytics
- Complete joint exercises together
- Share milestone celebrations

### Therapist/Observer (Optional)
- View entries shared by couple with permission
- Provide guided exercises and prompts
- Monitor progress (read-only access)

## Technical Requirements

### Frontend
- Modern responsive web application
- Support for desktop and mobile browsers
- Real-time collaborative editing for joint entries
- Offline-capable with sync when connection restored
- Rich text editor for journal entries

### Backend
- RESTful API architecture
- Real-time WebSocket support for notifications and collaboration
  - **AC1**: Notifications are delivered within the specified timeframe
  - **AC2**: Users can configure notification preferences
  - **AC3**: Notification content is clear and actionable
- Domain event sourcing for all significant actions
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- SQL Server database
- CQRS pattern for read/write separation

### Integration
- Email notification service
- SMS notification service (optional)
- Calendar integration for milestone reminders
- Photo storage service

### Performance
- Page load time: <2 seconds
- API response time: <500ms for 95th percentile
- Support for 1000 concurrent users
- 99.9% uptime SLA

### Security
- HTTPS encryption for all communications
- Data encryption at rest
- Regular security audits
- GDPR compliance for data privacy
- Regular automated backups


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


## Success Metrics
- User engagement: 70% of couples log entries at least 3x per week
- Retention: 60% of couples still active after 3 months
- Satisfaction: Average relationship mood trend shows improvement over 6 weeks
- Streak achievement: 40% of users achieve 30-day gratitude streak
- Feature adoption: 80% of couples complete at least one weekly review

## Future Enhancements
- Mobile native apps (iOS/Android)
- Integration with marriage education content
- Couples challenges and guided programs
- Community features (anonymous sharing)
- Therapist collaboration portal
- Voice journal entries
- AI-powered insight recommendations
