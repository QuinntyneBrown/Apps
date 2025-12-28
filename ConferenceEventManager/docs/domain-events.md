# Domain Events - Conference & Event Manager

## Overview
This application tracks domain events related to professional conferences, networking events, sessions attended, and professional connections made. These events support career development, knowledge sharing, and professional network building.

## Events

### ConferenceEvents

#### ConferenceRegistered
- **Description**: User has registered for a conference or professional event
- **Triggered When**: User completes conference registration
- **Key Data**: Conference ID, name, location, dates, registration type, cost, payment status, registration date, confirmation number
- **Consumers**: Calendar integration, expense tracker, reminder service, travel planning tool, attendance tracker

#### ConferenceAttended
- **Description**: User has attended a conference
- **Triggered When**: Conference dates pass or user manually confirms attendance
- **Key Data**: Conference ID, attendance dates, location, check-in times, sessions attended count, networking events participated, overall rating
- **Consumers**: Professional development tracker, ROI calculator, networking analyzer, career history builder

#### ConferenceCancelled
- **Description**: A registered conference has been cancelled
- **Triggered When**: Organizer cancels event or user withdraws registration
- **Key Data**: Conference ID, cancellation date, cancellation reason, refund status, cancelled by (organizer/user), rescheduled flag
- **Consumers**: Calendar updater, expense reconciliation, notification service, alternative event recommender

#### ConferenceSchedulePlanned
- **Description**: User has planned their conference session schedule
- **Triggered When**: User selects and organizes sessions they plan to attend
- **Key Data**: Plan ID, conference ID, selected sessions, time blocks, priorities, conflicts identified, alternative sessions, planning date
- **Consumers**: Session reminder service, schedule optimizer, conflict resolver, personal calendar sync

### SessionEvents

#### SessionAttended
- **Description**: User has attended a conference session or workshop
- **Triggered When**: User checks in to or marks session as attended
- **Key Data**: Session ID, conference ID, title, speaker(s), start/end time, track/category, attendance timestamp, location/room
- **Consumers**: Learning tracker, session analytics, speaker follow-up, certificate generator, knowledge base

#### SessionRated
- **Description**: User has rated or reviewed a conference session
- **Triggered When**: User provides feedback on attended session
- **Key Data**: Rating ID, session ID, rating score, review text, speaker rating, content quality, practical value, submission timestamp
- **Consumers**: Conference feedback system, speaker evaluation, future session recommender, quality analyzer

#### SessionNotesCreated
- **Description**: User has created notes from a conference session
- **Triggered When**: User saves notes during or after attending a session
- **Key Data**: Notes ID, session ID, content, key takeaways, action items, resources mentioned, speaker quotes, creation timestamp
- **Consumers**: Knowledge management, action item tracker, search indexer, sharing service, learning repository

#### SpeakerFollowUpScheduled
- **Description**: User has scheduled follow-up with a session speaker
- **Triggered When**: User marks a speaker for follow-up contact
- **Key Data**: Follow-up ID, speaker name, contact info, session ID, follow-up reason, scheduled date, follow-up type, conversation topics
- **Consumers**: CRM integration, reminder service, networking tracker, relationship manager

### NetworkingEvents

#### ContactMet
- **Description**: User has met a new professional contact at event
- **Triggered When**: User records a new connection made at conference
- **Key Data**: Contact ID, name, company, title, contact method, meeting context, conference ID, meeting date, notes, follow-up intent
- **Consumers**: CRM system, LinkedIn integration, follow-up reminder, relationship strength tracker, networking analytics

#### BusinessCardExchanged
- **Description**: User has exchanged business cards or contact information
- **Triggered When**: User scans or manually enters business card information
- **Key Data**: Card ID, contact details, company info, exchange location, conference ID, exchange timestamp, card image, digital contact flag
- **Consumers**: Contact manager, CRM sync, OCR processor, duplicate detector, follow-up scheduler

#### NetworkingEventAttended
- **Description**: User has attended a networking event or social function
- **Triggered When**: User marks attendance at networking session
- **Key Data**: Event ID, event name, conference ID, date/time, venue, attendee count estimate, connections made, event type, rating
- **Consumers**: Networking analytics, ROI calculator, contact relationship tracker, social activity monitor

#### FollowUpCompleted
- **Description**: User has completed follow-up with a conference contact
- **Triggered When**: User marks follow-up action as done
- **Key Data**: Follow-up ID, contact ID, completion date, communication method, outcome, next steps, relationship status update
- **Consumers**: CRM system, relationship tracker, networking effectiveness analyzer, reminder service

### LearningAndDevelopmentEvents

#### LearningObjectiveSet
- **Description**: User has set learning objectives for conference attendance
- **Triggered When**: User defines what they want to learn or achieve at conference
- **Key Data**: Objective ID, conference ID, objective description, related sessions, success criteria, priority, set date
- **Consumers**: Session recommender, goal tracker, ROI evaluator, learning path planner

#### CertificateEarned
- **Description**: User has earned a certificate or continuing education credits
- **Triggered When**: Conference completion or specific session attendance qualifies for certification
- **Key Data**: Certificate ID, conference ID, certificate type, issuing organization, credit hours, issue date, expiration date, certificate number
- **Consumers**: Professional development tracker, credential manager, resume builder, compliance tracker

#### KnowledgeAreaExpanded
- **Description**: User has gained knowledge in a specific professional area
- **Triggered When**: User or system identifies skill development from conference attendance
- **Key Data**: Knowledge ID, topic area, related sessions, proficiency gain, evidence sources, assessment date, practical applications
- **Consumers**: Skill development tracker, career planning, learning path generator, expertise profiler

### ExpenseAndROIEvents

#### ConferenceExpenseRecorded
- **Description**: An expense related to conference attendance has been recorded
- **Triggered When**: User logs travel, accommodation, registration, or other conference costs
- **Key Data**: Expense ID, conference ID, expense type, amount, date, receipt, reimbursement status, business justification, category
- **Consumers**: Expense management, reimbursement tracker, budget monitor, tax documentation, ROI calculator

#### ConferenceROIAssessed
- **Description**: Return on investment for conference attendance has been evaluated
- **Triggered When**: User or system calculates value derived from conference
- **Key Data**: Assessment ID, conference ID, total costs, tangible benefits, intangible benefits, ROI score, assessment date, key outcomes
- **Consumers**: Budget planning, attendance decision support, manager reporting, career investment tracker

#### TravelItineraryCreated
- **Description**: Travel arrangements for conference have been planned
- **Triggered When**: User creates or books travel and accommodation
- **Key Data**: Itinerary ID, conference ID, transportation details, accommodation, arrival/departure times, travel cost, booking confirmations
- **Consumers**: Travel coordination, expense tracker, calendar integration, reminder service, itinerary sharing
