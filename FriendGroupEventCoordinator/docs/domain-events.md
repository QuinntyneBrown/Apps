# Domain Events - Friend Group Event Coordinator

## Overview
This application facilitates planning and coordinating events among friend groups. Domain events capture event creation, RSVP management, communication, schedule coordination, and the execution of group activities.

## Events

### EventEvents

#### EventProposed
- **Description**: A new event idea has been proposed to the friend group
- **Triggered When**: Member suggests an activity or gathering
- **Key Data**: Event ID, proposer ID, group ID, event title, proposed date/time options, activity type, estimated cost, location suggestions, timestamp
- **Consumers**: Group notification service, polling system, discussion thread initiator

#### EventDetailsFinalized
- **Description**: Event details have been confirmed and finalized
- **Triggered When**: Group consensus reached on date, time, location, and activity
- **Key Data**: Event ID, final date/time, confirmed location, confirmed activity, budget, host/organizer, finalization timestamp
- **Consumers**: Calendar service, RSVP system, reminder scheduler, preparation checklist

#### EventCancelled
- **Description**: A planned event has been cancelled
- **Triggered When**: Organizer or group decides to cancel the event
- **Key Data**: Event ID, cancellation reason, cancelled by user ID, notice period, timestamp
- **Consumers**: Notification service, calendar update, refund coordinator (if applicable)

#### EventRescheduled
- **Description**: Event date or time has been changed
- **Triggered When**: Organizer updates event timing
- **Key Data**: Event ID, original date/time, new date/time, reschedule reason, affected attendees, timestamp
- **Consumers**: Calendar update service, attendee notification, RSVP re-confirmation system

#### EventCompleted
- **Description**: An event has concluded successfully
- **Triggered When**: Event date passes and marked as complete
- **Key Data**: Event ID, actual attendance, cost breakdown, highlights, photos, satisfaction ratings, timestamp
- **Consumers**: Event history, cost settlement system, photo album, analytics service

### RSVPEvents

#### RSVPSubmitted
- **Description**: A friend has responded to an event invitation
- **Triggered When**: Member submits their attendance response
- **Key Data**: RSVP ID, event ID, user ID, response (yes/no/maybe), plus-ones count, dietary restrictions, notes, timestamp
- **Consumers**: Attendance tracker, headcount calculator, organizer notifications, planning adjustments

#### RSVPChanged
- **Description**: A member has updated their RSVP response
- **Triggered When**: Member changes their attendance status
- **Key Data**: RSVP ID, event ID, user ID, previous response, new response, change reason, timestamp
- **Consumers**: Attendance recalculation, organizer notification, planning re-evaluation

#### RSVPDeadlinePassed
- **Description**: The deadline for RSVPs has expired
- **Triggered When**: RSVP cutoff date is reached
- **Key Data**: Event ID, final headcount, response rate, non-responders list, timestamp
- **Consumers**: Final planning service, non-responder follow-up, budget finalization

#### LastMinuteCancellation
- **Description**: An attendee has cancelled within the notice period
- **Triggered When**: Member cancels close to event date
- **Key Data**: RSVP ID, event ID, user ID, cancellation time, notice period violation, impact assessment, timestamp
- **Consumers**: Organizer urgent notification, backup plan trigger, attendance impact analysis

### SchedulingEvents

#### AvailabilitySubmitted
- **Description**: Member has shared their availability for event scheduling
- **Triggered When**: User indicates when they can attend
- **Key Data**: Availability ID, user ID, event ID, available time slots, unavailable dates, flexibility score, timestamp
- **Consumers**: Schedule optimizer, best time calculator, consensus finder

#### OptimalTimeIdentified
- **Description**: System has identified the best time based on availabilities
- **Triggered When**: Analysis of all member availabilities completes
- **Key Data**: Event ID, recommended time slots, attendee coverage percentage, conflict details, timestamp
- **Consumers**: Event scheduling assistant, organizer decision support, polling system

#### PollCreated
- **Description**: A poll has been created for group decision-making
- **Triggered When**: Organizer needs group input on event details
- **Key Data**: Poll ID, event ID, poll question, options, voting deadline, creator ID, timestamp
- **Consumers**: Voting system, member notifications, decision tracking

#### PollCompleted
- **Description**: A poll has closed and results are available
- **Triggered When**: Poll deadline reached or decision made
- **Key Data**: Poll ID, event ID, winning option, vote distribution, participation rate, timestamp
- **Consumers**: Event planning updates, decision implementation, result notification

### CostSharingEvents

#### ExpenseAdded
- **Description**: An event-related expense has been logged
- **Triggered When**: Member pays for something related to the event
- **Key Data**: Expense ID, event ID, payer ID, amount, description, category, receipt, timestamp
- **Consumers**: Cost tracking system, expense splitter, settlement calculator

#### CostSplitCalculated
- **Description**: Event costs have been divided among attendees
- **Triggered When**: All expenses logged and split calculation requested
- **Key Data**: Event ID, total cost, per-person amounts, split method, who owes whom, timestamp
- **Consumers**: Payment request system, settlement dashboard, debt tracking

#### PaymentRecorded
- **Description**: A member has settled their share of event costs
- **Triggered When**: Payment between members is completed
- **Key Data**: Payment ID, event ID, payer ID, payee ID, amount, payment method, timestamp
- **Consumers**: Debt settlement tracker, balance updater, payment confirmation service

#### SettlementCompleted
- **Description**: All event costs have been fully settled
- **Triggered When**: All members have paid their shares
- **Key Data**: Event ID, final settlement date, total amount settled, timestamp
- **Consumers**: Event closure system, financial analytics, group trust score

### CommunicationEvents

#### EventDiscussionStarted
- **Description**: A discussion thread about the event has begun
- **Triggered When**: Member posts to event discussion
- **Key Data**: Discussion ID, event ID, initiator ID, topic, initial message, timestamp
- **Consumers**: Notification service, discussion tracker, engagement monitor

#### GroupMessageSent
- **Description**: A message has been sent to event attendees
- **Triggered When**: Organizer or member sends update to group
- **Key Data**: Message ID, event ID, sender ID, message content, recipient count, urgency level, timestamp
- **Consumers**: Notification delivery, read receipt tracker, message archive

#### ReminderSent
- **Description**: Automated reminder about event has been sent
- **Triggered When**: Scheduled reminder time reached
- **Key Data**: Reminder ID, event ID, reminder type, recipients, delivery channel, timestamp
- **Consumers**: Notification service, reminder effectiveness tracker, engagement monitor

### GroupEvents

#### NewMemberAdded
- **Description**: A new person has been added to the friend group
- **Triggered When**: Existing member invites new friend
- **Key Data**: Member ID, group ID, inviter ID, member name, contact info, timestamp
- **Consumers**: Group roster, future event invitations, onboarding flow

#### MemberRemovedFromEvent
- **Description**: A member has been removed from event invitation
- **Triggered When**: Organizer uninvites someone or member leaves group
- **Key Data**: Event ID, member ID, removal reason, removed by user ID, timestamp
- **Consumers**: Invitation list updater, notification service, attendance recalculation

#### SubgroupFormed
- **Description**: A subset of the group has been created for an event
- **Triggered When**: Event is for specific members only
- **Key Data**: Subgroup ID, parent group ID, event ID, member IDs included, subgroup purpose, timestamp
- **Consumers**: Invitation targeting, privacy management, group dynamics tracker
