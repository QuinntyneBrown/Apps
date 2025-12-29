# Requirements - Men's Group Discussion Tracker

## Overview
The Men's Group Discussion Tracker helps men's groups organize meetings, track discussions, share resources, and foster accountability. The application supports group coordination, personal growth tracking, and the development of meaningful male friendships.

## Target Users
- Men's group organizers and facilitators
- Group members
- Accountability partners
- Guest speakers and mentors

## Core Features

### 1. Meeting Management
**Functional Requirements**:
- FR-MM-001: Users shall be able to schedule meetings with date, time, location/virtual link, and topic
- FR-MM-002: Facilitators shall be able to start and end meetings
- FR-MM-003: Members shall be able to RSVP to meetings
- FR-MM-004: System shall track meeting attendance automatically
- FR-MM-005: Users shall be able to cancel or reschedule meetings
- FR-MM-006: System shall send meeting reminders 24 hours and 1 hour before start time

### 2. Discussion Tracking
**Functional Requirements**:
- FR-DT-001: Members shall be able to propose discussion topics
- FR-DT-002: Group shall be able to vote on proposed topics
- FR-DT-003: Facilitators shall be able to record key discussion points during meetings
- FR-DT-004: Members shall be able to share insights during discussions
- FR-DT-005: System shall archive all discussions for future reference
- FR-DT-006: Users shall be able to search discussion history

### 3. Accountability System
**Functional Requirements**:
- FR-AS-001: Members shall be able to set accountability goals with timeframes
- FR-AS-002: Users shall be able to assign accountability partners
- FR-AS-003: System shall send check-in reminders based on agreed frequency
- FR-AS-004: Members shall be able to report progress on goals
- FR-AS-005: Users shall be able to share struggles and request support
- FR-AS-006: System shall track goal completion and celebrate achievements

### 4. Resource Sharing
**Functional Requirements**:
- FR-RS-001: Members shall be able to share resources (books, articles, videos)
- FR-RS-002: Facilitators shall be able to assign reading for upcoming meetings
- FR-RS-003: Users shall be able to create study guides with discussion questions
- FR-RS-004: System shall organize resources by category and topic
- FR-RS-005: Members shall be able to mark resources as completed

### 5. Prayer Support
**Functional Requirements**:
- FR-PS-001: Members shall be able to submit prayer requests with urgency and confidentiality levels
- FR-PS-002: Users shall be able to view active prayer requests
- FR-PS-003: Members shall be able to report answered prayers
- FR-PS-004: System shall track group prayer sessions
- FR-PS-005: Users shall be able to set prayer reminders for specific requests

### 6. Member Management
**Functional Requirements**:
- FR-MG-001: Organizers shall be able to invite new members
- FR-MG-002: New members shall be able to agree to group covenant
- FR-MG-003: System shall track member engagement and attendance
- FR-MG-004: Organizers shall be able to mark members as inactive
- FR-MG-005: System shall maintain member directory with contact info

## Technical Requirements
- Frontend: React web application with mobile responsiveness
- Backend: ASP.NET Core RESTful API with domain events
- Database: SQL Server
- Real-time: SignalR for live updates
- Authentication: OAuth 2.0 with secure login

## Success Metrics
- Meeting attendance: Average 80% of committed members per meeting
- Engagement: 60% of members participate in discussions
- Accountability: 70% of goals tracked show progress
- Retention: 75% of members remain active after 6 months
