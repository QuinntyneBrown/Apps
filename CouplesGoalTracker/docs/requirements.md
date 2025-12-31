# Requirements - Couples Goal Tracker

## Executive Summary
Help couples set, track, and achieve shared goals together with milestones, progress tracking, and celebration features.

## Core Features

### Functional Requirements

#### 1. Goal Management

- **FR-1.0**: The system shall provide goal management capabilities to create shared goals, update details, complete or abandon
  - **AC1**: Users can create new items with all required fields
  - **AC2**: Validation prevents invalid data entry
  - **AC3**: Existing items can be edited and changes are saved
  - **AC4**: Items can be deleted with confirmation

#### 2. Milestone Tracking

- **FR-2.0**: The system shall provide milestone tracking capabilities to break goals into milestones, track achievements
  - **AC1**: Progress is tracked and historical data is preserved
  - **AC2**: Visual indicators show current status
  - **AC3**: Notifications are delivered at appropriate times

#### 3. Progress Tracking

- **FR-3.0**: The system shall provide progress tracking capabilities to log progress, visualize trends, celebrate wins
  - **AC1**: Progress is tracked and historical data is preserved
  - **AC2**: Visual indicators show current status
  - **AC3**: Data is displayed in a clear, understandable format

#### 4. Collaboration

- **FR-4.0**: The system shall provide collaboration capabilities to assign tasks, log contributions, balance workload
  - **AC1**: Progress is tracked and historical data is preserved
  - **AC2**: Visual indicators show current status
  - **AC3**: Shared items are visible to all authorized users
  - **AC4**: Changes by one user are reflected for all users

#### 5. Check-ins

- **FR-5.0**: The system shall provide check-ins capabilities to weekly reviews, goal discussions, alignment tracking
  - **AC1**: Progress is tracked and historical data is preserved
  - **AC2**: Visual indicators show current status
  - **AC3**: Data is displayed in a clear, understandable format

#### 6. Motivation

- **FR-6.0**: The system shall provide motivation capabilities to celebrations, streaks, encouragement, achievements
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs its intended function correctly
  - **AC3**: Appropriate feedback is provided to users


## Goal Categories
- Financial (savings, debt payoff, investments)
- Travel (trips, experiences, destinations)
- Health & Fitness (workout goals, healthy habits)
- Relationship (quality time, communication, intimacy)
- Home (buying, renovating, organizing)
- Career (professional development together)
- Personal Growth (learning, hobbies, skills)

## Technical Stack
- Backend: .NET 8, C#, CQRS, SQL Server
- Frontend: React, TypeScript, Chart.js for visualizations
- Features: Calendar sync, notifications, photo storage


## Key Screens
1. Dashboard - Active goals, recent progress, upcoming milestones
2. Goal Detail - Full goal info, milestones, progress chart, tasks
3. Create Goal - Set shared goal with category, timeline, success criteria
4. Progress Log - Record progress updates with notes
5. Check-in - Weekly review interface for all goals
6. Achievements - Completed goals, streaks, celebrations


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

