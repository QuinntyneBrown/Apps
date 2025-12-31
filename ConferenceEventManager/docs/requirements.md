# Requirements - Conference & Event Manager

## Executive Summary
A professional conference and event management platform for tracking conference attendance, sessions, networking contacts, learning outcomes, and ROI. Helps professionals maximize value from conference participation and build stronger professional networks.

## Business Objectives
- Track all professional development events and conferences
- Manage session schedules and prevent conflicts
- Build and maintain professional network contacts
- Measure ROI on conference investments
- Store learning outcomes and certifications
- Facilitate post-conference follow-up

## Core Features

### 1. Conference Management
- Register for conferences with full details tracking
- Store registration confirmations and payment info
- Manage conference calendar with reminders
- Plan session schedules and identify conflicts
- Track attendance and check-ins
- Handle cancellations and refunds
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- Rate overall conference experience

### 2. Session Tracking
- Browse and search conference session catalog
- Plan personalized session schedule
- Check-in to sessions attended
- Take detailed notes with action items
- Rate sessions and speakers
- Store presentation materials and resources
- Schedule speaker follow-ups
- Track continuing education credits

### 3. Networking
- Record new contacts met at events
- Scan and store business cards (OCR)
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- Tag contacts by conference and context
- Track networking events attended
- Schedule and track follow-ups
- Measure connection strength over time
- Export contacts to CRM/LinkedIn
- Note conversation topics and next steps

### 4. Learning & Development
- Set learning objectives before conferences
- Track knowledge gained by topic area
- Store earned certificates and credentials
- Monitor professional development hours
- Build skill development timeline
- Link sessions to career goals
- Generate learning reports

### 5. Expense & ROI Management
- Record all conference-related expenses
- Track reimbursement status
- Categorize costs (registration, travel, meals, etc.)
- Store receipts and documentation
- Calculate conference ROI
- Assess tangible and intangible benefits
- Create travel itineraries
- Budget planning for future events

## Technical Requirements

### Backend
- .NET 8 with C# CQRS architecture
- SQL Server database
- Domain events for all key actions
- RESTful API with JWT auth
- OCR integration for business card scanning
  - **AC1**: Given a valid user session, the feature is accessible from the appropriate UI location
  - **AC2**: When the user performs the action, the system responds within acceptable performance limits
  - **AC3**: Then the action completes successfully and the user receives appropriate feedback
- Calendar sync (Google Calendar, Outlook)
- File storage for certificates and receipts

### Frontend
- React 18 with TypeScript
- Material-UI or Tailwind CSS
- Calendar component for scheduling
- File upload for receipts/cards
- Notes editor with rich text
- Contact management interface
- Charts for ROI and analytics

### Key Screens
1. Dashboard - Upcoming conferences, recent sessions, pending follow-ups
2. Conferences - List and manage registered conferences
3. Conference Detail - Schedule, sessions, notes, contacts
4. Session Library - Browse and search all sessions
5. Networking - Contact list, follow-ups, relationship tracker
6. Learning - Objectives, certificates, skill development
7. Expenses - Expense tracking, receipts, ROI analysis


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
- Conference attendance tracking accuracy
- Follow-up completion rate
- Professional network growth
- Learning objectives achieved
- Positive conference ROI percentage
- User engagement and retention
