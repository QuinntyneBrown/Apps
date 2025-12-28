# Domain Events - Meeting Notes & Action Item Tracker

## Overview
This application tracks significant events related to meetings, note-taking, action items, and follow-up activities. These domain events capture critical business occurrences that enable tracking accountability, productivity, and organizational communication.

## Events

### MeetingEvents

#### MeetingScheduled
- **Description**: A new meeting has been scheduled in the system
- **Triggered When**: A user creates a new meeting entry with date, time, and participants
- **Key Data**: Meeting ID, title, scheduled date/time, organizer, participant list, meeting type, agenda topics
- **Consumers**: Calendar integration service, notification service, participant availability checker, reminder scheduler

#### MeetingStarted
- **Description**: A scheduled meeting has commenced
- **Triggered When**: The meeting start time is reached or manually marked as started
- **Key Data**: Meeting ID, actual start time, attendees present, no-shows list, meeting location/link
- **Consumers**: Time tracking service, attendance recorder, real-time collaboration tools, analytics engine

#### MeetingCompleted
- **Description**: A meeting has been concluded
- **Triggered When**: Meeting is marked as complete or end time is reached
- **Key Data**: Meeting ID, end time, duration, attendee list, completion status, notes availability
- **Consumers**: Meeting analytics service, follow-up generator, participant feedback system, time tracking

#### MeetingCancelled
- **Description**: A scheduled meeting has been cancelled
- **Triggered When**: Organizer or authorized user cancels the meeting
- **Key Data**: Meeting ID, cancellation reason, cancelled by, cancellation timestamp, affected participants
- **Consumers**: Notification service, calendar sync, participant availability updater, reporting system

#### MeetingRescheduled
- **Description**: A meeting's date or time has been changed
- **Triggered When**: Meeting organizer updates the scheduled time
- **Key Data**: Meeting ID, old date/time, new date/time, reason for change, updated participant list
- **Consumers**: Calendar integration, notification service, conflict checker, reminder scheduler

### NotesEvents

#### MeetingNotesCreated
- **Description**: Meeting notes have been created and saved
- **Triggered When**: User saves initial notes during or after a meeting
- **Key Data**: Notes ID, meeting ID, author, creation timestamp, note content, formatting, attachments
- **Consumers**: Search indexer, collaboration service, backup system, version control

#### MeetingNotesUpdated
- **Description**: Existing meeting notes have been modified
- **Triggered When**: User edits and saves changes to existing notes
- **Key Data**: Notes ID, editor, update timestamp, changes made, version number, edit reason
- **Consumers**: Version control system, collaboration tracker, search re-indexer, audit log

#### MeetingNotesShared
- **Description**: Meeting notes have been shared with team members or stakeholders
- **Triggered When**: User explicitly shares notes with specific individuals or groups
- **Key Data**: Notes ID, shared by, recipient list, share timestamp, access permissions, share method
- **Consumers**: Access control service, notification service, collaboration platform, analytics

### ActionItemEvents

#### ActionItemCreated
- **Description**: A new action item has been created from a meeting
- **Triggered When**: User creates a task or action item during or after a meeting
- **Key Data**: Action item ID, meeting ID, title, description, assignee, due date, priority, status
- **Consumers**: Task management system, notification service, assignee dashboard, reminder scheduler

#### ActionItemAssigned
- **Description**: An action item has been assigned to a team member
- **Triggered When**: Action item is assigned or reassigned to a specific person
- **Key Data**: Action item ID, assignee, assigned by, assignment timestamp, due date, priority level
- **Consumers**: Notification service, assignee task list, workload balancer, escalation tracker

#### ActionItemStatusChanged
- **Description**: The status of an action item has been updated
- **Triggered When**: Assignee or authorized user updates the action item status (in progress, blocked, completed, etc.)
- **Key Data**: Action item ID, old status, new status, changed by, timestamp, status reason, completion percentage
- **Consumers**: Progress tracker, reporting dashboard, notification service, analytics engine

#### ActionItemCompleted
- **Description**: An action item has been marked as complete
- **Triggered When**: Assignee marks the action item as done and provides completion evidence
- **Key Data**: Action item ID, completed by, completion timestamp, outcome notes, verification status
- **Consumers**: Meeting follow-up tracker, team productivity analytics, notification service, reporting system

#### ActionItemOverdue
- **Description**: An action item has passed its due date without completion
- **Triggered When**: System detects that current date exceeds the due date for an incomplete action item
- **Key Data**: Action item ID, assignee, due date, days overdue, priority, escalation level
- **Consumers**: Escalation service, manager notification system, team dashboard, accountability tracker

### FollowUpEvents

#### FollowUpMeetingScheduled
- **Description**: A follow-up meeting has been scheduled based on action items or previous meeting outcomes
- **Triggered When**: User creates a follow-up meeting linked to a previous meeting
- **Key Data**: New meeting ID, original meeting ID, scheduled date/time, participants, follow-up topics, related action items
- **Consumers**: Calendar service, notification system, action item tracker, meeting chain analyzer

#### FollowUpReminderSent
- **Description**: A reminder has been sent regarding pending action items or upcoming follow-ups
- **Triggered When**: Scheduled reminder time is reached or manually triggered
- **Key Data**: Reminder ID, recipient, action item IDs, reminder type, sent timestamp, delivery channel
- **Consumers**: Notification delivery service, reminder scheduler, user preference manager, analytics

#### DecisionRecorded
- **Description**: An important decision has been documented in meeting notes
- **Triggered When**: User explicitly tags or marks a decision in the meeting notes
- **Key Data**: Decision ID, meeting ID, decision text, decision maker(s), timestamp, impact level, related action items
- **Consumers**: Decision registry, compliance tracker, audit system, organizational knowledge base
