# Domain Events - Kids Activity & Sports Tracker

## Overview
This document defines the domain events tracked in the Kids Activity & Sports Tracker application. These events capture significant business occurrences related to children's activities, sports schedules, practice attendance, game results, carpool coordination, and performance tracking.

## Events

### ActivityRegistrationEvents

#### ActivityEnrollmentCreated
- **Description**: Child has been registered for activity or sport
- **Triggered When**: Parent enrolls child in program, team, or class
- **Key Data**: Enrollment ID, child ID, activity type, organization, start date, end date, fee amount, commitment level, location
- **Consumers**: Schedule generator, payment tracker, contact list, season planner, commitment calendar

#### EnrollmentStatusChanged
- **Description**: Child's enrollment status has been updated
- **Triggered When**: Status changes (active, waitlist, dropped, completed)
- **Key Data**: Enrollment ID, previous status, new status, change date, change reason, refund eligibility
- **Consumers**: Schedule updater, payment processor, notification service, availability tracker

#### SeasonCompleted
- **Description**: Activity season or program session has ended
- **Triggered When**: End date reached or final event completed
- **Key Data**: Enrollment ID, completion date, attendance summary, achievements, final stats, next season info
- **Consumers**: Archive service, achievement recorder, re-enrollment reminder, season recap generator

### ScheduleEvents

#### PracticeScheduled
- **Description**: Practice or training session has been added to calendar
- **Triggered When**: Coach publishes practice schedule or updates times
- **Key Data**: Practice ID, enrollment ID, date/time, duration, location, coach, focus areas, attendance required
- **Consumers**: Family calendar integration, reminder scheduler, carpool coordinator, conflict detector

#### GameScheduled
- **Description**: Competitive game or performance has been scheduled
- **Triggered When**: League publishes game schedule or event date set
- **Key Data**: Game ID, enrollment ID, date/time, opponent, location, home/away, arrival time, uniform requirements
- **Consumers**: Family calendar, notification service, carpool planner, preparation checklist, opponent tracker

#### ScheduleChanged
- **Description**: Activity schedule has been modified
- **Triggered When**: Practice/game time, date, or location changes
- **Key Data**: Event ID, change type, previous details, new details, change notification date, reason for change
- **Consumers**: Calendar updater, participant notifier, carpool re-coordinator, conflict re-checker

#### EventCancelled
- **Description**: Scheduled practice, game, or event has been called off
- **Triggered When**: Organization cancels due to weather, facility, or other reasons
- **Key Data**: Event ID, cancellation date/time, cancellation reason, rescheduled date, makeup plan, notification sent
- **Consumers**: Calendar cleaner, notification service, carpool canceller, make-up scheduler

### AttendanceEvents

#### AttendanceMarked
- **Description**: Child's presence at practice or game has been recorded
- **Triggered When**: Parent or coach logs attendance
- **Key Data**: Event ID, child ID, attendance status, check-in time, check-out time, recorded by, participation level
- **Consumers**: Attendance tracker, playing time eligibility, commitment monitor, coach visibility

#### AttendanceStreakAchieved
- **Description**: Child has attended consecutive sessions
- **Triggered When**: Attendance reaches milestone (5, 10, 20 consecutive)
- **Key Data**: Child ID, enrollment ID, streak length, start date, achievement date, missed practices count
- **Consumers**: Achievement system, motivation reward, coach notification, dedication tracker

#### ExcusedAbsenceLogged
- **Description**: Pre-approved absence has been recorded
- **Triggered When**: Parent notifies coach of planned absence
- **Key Data**: Event ID, child ID, absence reason, notification date, advance notice given, makeup available
- **Consumers**: Attendance recorder, coach notification, playing time adjuster, commitment tracker

#### UnexcusedAbsenceRecorded
- **Description**: Child missed session without advance notice
- **Triggered When**: No-show detected for scheduled event
- **Key Data**: Event ID, child ID, absence date, follow-up required, consecutive unexcused count, policy implications
- **Consumers**: Alert service, coach notification, attendance policy checker, parent follow-up trigger

### PerformanceEvents

#### GameResultRecorded
- **Description**: Outcome of competitive event has been logged
- **Triggered When**: Game completes and score/result entered
- **Key Data**: Game ID, final score, win/loss/tie, opponent, child participation, team performance, highlights
- **Consumers**: Win/loss record, season statistics, performance trends, child engagement tracker

#### ChildStatisticsUpdated
- **Description**: Individual performance metrics have been recorded
- **Triggered When**: Coach or parent logs child's game/practice stats
- **Key Data**: Child ID, event ID, statistics (sport-specific), playing time, position played, notable achievements
- **Consumers**: Performance dashboard, progress tracker, skills development monitor, season stats aggregator

#### MilestoneAchieved
- **Description**: Child has reached significant performance milestone
- **Triggered When**: Achievement detected (first goal, belt promotion, skill mastery)
- **Key Data**: Child ID, milestone type, achievement date, milestone description, skill level, celebration worthy
- **Consumers**: Achievement tracker, parent notification, motivation system, memory recorder, social sharing

#### SkillAssessmentCompleted
- **Description**: Formal or informal evaluation of child's abilities has been done
- **Triggered When**: Coach assessment, belt test, skill evaluation conducted
- **Key Data**: Assessment ID, child ID, assessment date, evaluator, skills assessed, ratings, improvement areas, strengths
- **Consumers**: Progress tracker, development planner, goal setter, parent communication, next level readiness

### CarpoolEvents

#### CarpoolCreated
- **Description**: Carpool arrangement has been established
- **Triggered When**: Parents organize shared transportation for activity
- **Key Data**: Carpool ID, activity ID, participants, schedule, rotation pattern, driver assignments, contact info
- **Consumers**: Carpool scheduler, driver reminder, participant notification, route planner

#### CarpoolDriverAssigned
- **Description**: Driver has been designated for specific date/event
- **Triggered When**: Carpool rotation assigns or parent volunteers to drive
- **Key Data**: Assignment ID, carpool ID, driver ID, event date, passengers, pickup times, pickup locations, drop-off plan
- **Consumers**: Driver reminder, passenger parent notification, schedule confirmation, backup coordinator

#### CarpoolReminderSent
- **Description**: Upcoming carpool responsibility notification delivered
- **Triggered When**: Advance reminder time reached (24hr, 2hr before)
- **Key Data**: Assignment ID, driver ID, event details, passengers, timing, notification channel, confirmation required
- **Consumers**: Driver notification service, confirmation tracker, backup trigger if no response

#### CarpoolSubstituteRequested
- **Description**: Assigned driver needs replacement
- **Triggered When**: Driver cannot fulfill commitment and requests substitute
- **Key Data**: Assignment ID, original driver, request date, event date, urgency, reason, passengers affected
- **Consumers**: Substitute finder, carpool member notifier, backup coordinator, emergency contact

### CommunicationEvents

#### CoachMessageReceived
- **Description**: Communication from coach has been delivered
- **Triggered When**: Coach sends announcement, update, or individual message
- **Key Data**: Message ID, sender, recipients, message content, send date, priority, related event, read status
- **Consumers**: Notification service, message inbox, parent alert, action item extractor

#### ParentMessageSent
- **Description**: Parent has communicated with coach or organization
- **Triggered When**: Parent sends question, concern, or update
- **Key Data**: Message ID, sender, recipient, subject, content, send date, response expected, related child
- **Consumers**: Communication log, response tracker, coach inbox, relationship manager

#### TeamAnnouncementPosted
- **Description**: Important team-wide announcement has been published
- **Triggered When**: Coach or organization posts general information
- **Key Data**: Announcement ID, author, post date, content, urgency, expiration, acknowledgment required, recipients
- **Consumers**: Notification blaster, acknowledgment tracker, announcement board, archive

### PaymentEvents

#### ActivityFeePaid
- **Description**: Payment has been made for enrollment or participation
- **Triggered When**: Parent completes payment transaction
- **Key Data**: Payment ID, enrollment ID, amount, payment date, payment method, installment number, receipt, balance remaining
- **Consumers**: Payment tracker, receipt generator, balance updater, financial records, tax documentation

#### PaymentReminder Sent
- **Description**: Upcoming or overdue payment notification delivered
- **Triggered When**: Payment due date approaching or past
- **Key Data**: Reminder ID, enrollment ID, amount due, due date, days overdue, payment methods, late fee warning
- **Consumers**: Payment notification, parent alert, late fee calculator, payment processor

#### FundraiserContributionLogged
- **Description**: Donation or fundraising amount has been recorded
- **Triggered When**: Parent contributes to team fundraiser
- **Key Data**: Contribution ID, child ID, amount, contribution date, fundraiser campaign, contribution method, tax deductible
- **Consumers**: Fundraising tracker, goal progress, receipt generator, thank you automation, campaign analytics

### EquipmentEvents

#### EquipmentIssued
- **Description**: Team equipment has been distributed to child
- **Triggered When**: Uniform, gear, or equipment checked out
- **Key Data**: Issue ID, child ID, equipment items, issue date, return date expected, condition, deposit collected
- **Consumers**: Equipment tracker, return reminder, inventory manager, deposit tracker

#### EquipmentReturned
- **Description**: Issued equipment has been returned to organization
- **Triggered When**: Child returns uniform or gear
- **Key Data**: Issue ID, return date, items returned, condition assessment, deposit refund, late return flag
- **Consumers**: Inventory updater, deposit processor, condition tracker, season closer

#### UniformSizeUpdated
- **Description**: Child's equipment size requirements have changed
- **Triggered When**: Parent updates sizing information for growing child
- **Key Data**: Child ID, garment/equipment type, previous size, new size, update date, reissue needed
- **Consumers**: Uniform coordinator, reissue workflow, sizing database, ordering system
