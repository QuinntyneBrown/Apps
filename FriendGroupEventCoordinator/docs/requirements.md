# Requirements - Friend Group Event Coordinator

## Executive Summary
Coordinate social events with friends through proposal, scheduling, RSVP tracking, cost splitting, and group communication.

## Core Features

### Functional Requirements

#### 1. Event Management

- **FR-1.0**: The system shall provide event management capabilities to propose, finalize, cancel, reschedule events
  - **AC1**: Users can create new items with all required fields
  - **AC2**: Validation prevents invalid data entry
  - **AC3**: Scheduled items appear on the calendar correctly

#### 2. RSVP Management

- **FR-2.0**: The system shall provide rsvp management capabilities to track responses, headcount, dietary restrictions
  - **AC1**: Users can create new items with all required fields
  - **AC2**: Validation prevents invalid data entry
  - **AC3**: Progress is tracked and historical data is preserved
  - **AC4**: Visual indicators show current status

#### 3. Scheduling

- **FR-3.0**: The system shall provide scheduling capabilities to share availability, find optimal times, create polls
  - **AC1**: Users can create new items with all required fields
  - **AC2**: Validation prevents invalid data entry
  - **AC3**: Shared items are visible to all authorized users
  - **AC4**: Changes by one user are reflected for all users

#### 4. Cost Sharing

- **FR-4.0**: The system shall provide cost sharing capabilities to track expenses, split costs, settle payments
  - **AC1**: Users can create new items with all required fields
  - **AC2**: Validation prevents invalid data entry
  - **AC3**: Progress is tracked and historical data is preserved
  - **AC4**: Visual indicators show current status

#### 5. Communication

- **FR-5.0**: The system shall provide communication capabilities to discuss events, send reminders, group messaging
  - **AC1**: Feature is accessible to authorized users
  - **AC2**: Feature performs its intended function correctly
  - **AC3**: Appropriate feedback is provided to users

#### 6. Group Management

- **FR-6.0**: The system shall provide group management capabilities to manage friend groups and subgroups
  - **AC1**: Users can create new items with all required fields
  - **AC2**: Validation prevents invalid data entry


## Technical Stack
- Backend: .NET 8, C#, CQRS, SQL Server
- Frontend: React, TypeScript, Material-UI
- Real-time: SignalR for live updates
- Payments: Integration with Venmo/PayPal APIs


## Key Screens
1. Dashboard - Upcoming events, pending RSVPs, owed amounts
2. Event Detail - Full event info, attendees, discussion, costs
3. Create Event - Propose new event with details
4. Schedule Finder - Collect availability, find best times
5. Cost Splitter - Add expenses, calculate splits, track payments
6. Group Chat - Event-specific discussions and updates
