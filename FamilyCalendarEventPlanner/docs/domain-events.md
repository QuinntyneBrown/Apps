# Domain Events - Family Calendar & Event Planner

## Overview
This document defines the domain events tracked in the Family Calendar & Event Planner application. These events capture significant business occurrences related to shared family scheduling, event planning, coordination, conflicts, and family member availability.

## Events

### CalendarEvents

#### EventCreated
- **Description**: A new event has been added to the family calendar
- **Triggered When**: Family member creates calendar entry
- **Key Data**: Event ID, creator ID, title, description, start time, end time, location, attendees, event type, recurrence pattern, priority
- **Consumers**: Calendar sync, notification service, conflict detector, availability checker, reminder scheduler

#### EventModified
- **Description**: Existing calendar event has been updated
- **Triggered When**: Family member edits event details
- **Key Data**: Event ID, modifier ID, changed fields, previous values, new values, modification timestamp, notification required
- **Consumers**: Calendar sync, attendee notifier, conflict re-checker, reminder updater, change log

#### EventCancelled
- **Description**: Scheduled event has been cancelled
- **Triggered When**: Family member cancels planned event
- **Key Data**: Event ID, cancellation date, cancelled by, cancellation reason, attendees affected, was recurring
- **Consumers**: Notification service, calendar updater, attendee alerts, cleanup service, analytics

#### EventRSVPReceived
- **Description**: Family member has responded to event invitation
- **Triggered When**: Invitee accepts, declines, or marks tentative
- **Key Data**: Event ID, family member ID, RSVP status, response timestamp, notes, dietary restrictions, plus ones
- **Consumers**: Attendance tracker, headcount calculator, planning adjuster, host notifier

### ConflictEvents

#### ScheduleConflictDetected
- **Description**: Overlapping events have been identified for family member(s)
- **Triggered When**: New or modified event conflicts with existing calendar entry
- **Key Data**: Conflict ID, conflicting event IDs, affected family members, overlap duration, conflict severity, resolution suggestions
- **Consumers**: Conflict alert, resolution workflow, priority analyzer, scheduling assistant

#### ConflictResolved
- **Description**: Previously identified scheduling conflict has been addressed
- **Triggered When**: Events rescheduled or conflict marked as resolved
- **Key Data**: Conflict ID, resolution method, events modified, resolution timestamp, resolved by
- **Consumers**: Conflict tracker, notification service, calendar updater, analytics

#### DoubleBookingWarning
- **Description**: Family member has been scheduled for multiple simultaneous activities
- **Triggered When**: Person assigned to overlapping events
- **Key Data**: Family member ID, overlapping events, time period, booking priority, warning level
- **Consumers**: Alert system, family member notification, resolution recommender, calendar optimization

### AvailabilityEvents

#### AvailabilityBlocked
- **Description**: Family member has marked time as unavailable
- **Triggered When**: User blocks calendar time or marks busy period
- **Key Data**: Block ID, family member ID, start time, end time, reason, recurrence, block type (busy/out-of-office/personal)
- **Consumers**: Availability checker, scheduling assistant, conflict preventer, family visibility

#### FamilyAvailabilityChecked
- **Description**: System has checked availability of multiple family members
- **Triggered When**: User searches for common free time slots
- **Key Data**: Check ID, family members checked, date range, available time slots found, constraints applied
- **Consumers**: Scheduling assistant, meeting planner, event suggestion engine

#### AvailabilityOpened
- **Description**: Previously blocked time has become available
- **Triggered When**: User removes time block or event is cancelled
- **Key Data**: Family member ID, freed time period, previous blocking reason, opening timestamp
- **Consumers**: Scheduling opportunity notifier, conflict resolver, rescheduling suggester

### ReminderEvents

#### EventReminderScheduled
- **Description**: Notification reminder has been set for upcoming event
- **Triggered When**: Event created or reminder preferences configured
- **Key Data**: Reminder ID, event ID, recipients, reminder times (1 day, 1 hour, 15 min), delivery channels
- **Consumers**: Notification scheduler, reminder delivery service, preference manager

#### EventReminderSent
- **Description**: Event reminder notification has been delivered
- **Triggered When**: Scheduled reminder time arrives
- **Key Data**: Reminder ID, event ID, recipients, sent timestamp, delivery status, event details included
- **Consumers**: Delivery tracker, engagement monitor, notification analytics

#### PreparationReminderTriggered
- **Description**: Reminder for event preparation has been sent
- **Triggered When**: Sufficient advance time before event requiring preparation
- **Key Data**: Event ID, preparation tasks, deadline, responsible family members, checklist items
- **Consumers**: Task notification, preparation tracker, assignment alerts, deadline monitor

### RecurringEventEvents

#### RecurringEventSeriesCreated
- **Description**: New recurring event pattern has been established
- **Triggered When**: User creates event with recurrence rule
- **Key Data**: Series ID, recurrence pattern, start date, end date/occurrence count, exceptions, attendees, event template
- **Consumers**: Occurrence generator, calendar populator, long-term conflict checker, capacity planner

#### RecurrenceExceptionAdded
- **Description**: Single occurrence of recurring event has been modified or skipped
- **Triggered When**: User changes one instance without affecting series
- **Key Data**: Series ID, exception date, modification type, changed details, reason for exception
- **Consumers**: Calendar updater, attendee notifier, recurrence manager, exception tracker

#### RecurringEventEnded
- **Description**: Recurring event series has reached completion
- **Triggered When**: Final occurrence completed or series cancelled
- **Key Data**: Series ID, total occurrences, actual attendance stats, completion date, series summary
- **Consumers**: Analytics, archive service, pattern analyzer, template library

### FamilyCoordinationEvents

#### CarpoolOrganized
- **Description**: Transportation coordination has been arranged for event
- **Triggered When**: User sets up carpool for family activity
- **Key Data**: Carpool ID, event ID, driver, passengers, pickup times, pickup locations, return arrangement
- **Consumers**: Participant notifier, location sharer, timing coordinator, driver reminder

#### ResponsibilityAssigned
- **Description**: Event-related task has been assigned to family member
- **Triggered When**: User delegates event preparation or execution task
- **Key Data**: Assignment ID, event ID, task description, assigned to, due date, status, priority
- **Consumers**: Task tracker, assignee notification, completion monitor, accountability system

#### FamilyMeetingScheduled
- **Description**: Family gathering or meeting has been coordinated
- **Triggered When**: User schedules event requiring all or multiple family members
- **Key Data**: Meeting ID, required attendees, topic, duration, location, agenda items, decision items
- **Consumers**: Attendance tracker, agenda manager, decision log, conflict minimizer

### SyncEvents

#### CalendarSyncCompleted
- **Description**: Family calendar has synchronized with external calendar system
- **Triggered When**: Integration syncs data with Google Calendar, Outlook, Apple Calendar, etc.
- **Key Data**: Sync ID, sync timestamp, calendar source, events updated, conflicts found, sync direction
- **Consumers**: Sync status monitor, conflict resolver, data integrity checker, audit log

#### SyncConflictDetected
- **Description**: Discrepancy found between family calendar and external calendar
- **Triggered When**: Sync process identifies conflicting event data
- **Key Data**: Conflict ID, event ID, family calendar version, external calendar version, field differences, sync direction
- **Consumers**: Conflict resolution UI, manual review queue, sync error handler, version reconciler

### NotificationEvents

#### DigestScheduled
- **Description**: Daily or weekly family calendar summary has been prepared
- **Triggered When**: Scheduled digest time arrives with events to summarize
- **Key Data**: Digest ID, period covered, recipients, events included, highlights, action items, conflicts
- **Consumers**: Email service, push notification, digest delivery, engagement tracker

#### EventChangeNotificationSent
- **Description**: Attendees have been notified of event modification
- **Triggered When**: Event details change after invitations sent
- **Key Data**: Event ID, change type, notified attendees, notification timestamp, what changed summary
- **Consumers**: Communication tracker, re-RSVP requester, change log, attendee engagement

### ColorCodingEvents

#### EventCategoryColorized
- **Description**: Event has been assigned color code for visual organization
- **Triggered When**: User applies category color to event
- **Key Data**: Event ID, category, color code, previous color, categorization rules applied
- **Consumers**: Visual calendar renderer, category analytics, pattern recognition, filtering system

#### FamilyMemberColorAssigned
- **Description**: Family member has been assigned unique calendar color
- **Triggered When**: User configures color-coding scheme for family members
- **Key Data**: Family member ID, color code, applied to events, visibility rules
- **Consumers**: Calendar renderer, visual filter, multi-calendar view, conflict visualization
