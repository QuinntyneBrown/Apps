# Domain Events - Professional Network CRM

## Overview
This document defines the domain events tracked in the Professional Network CRM application. These events capture significant business occurrences related to professional relationships, networking activities, contact management, follow-ups, and relationship building.

## Events

### ContactEvents

#### ContactAdded
- **Description**: New professional contact has been added to network
- **Triggered When**: User adds person to professional network
- **Key Data**: Contact ID, name, title, company, industry, email, phone, LinkedIn URL, met date, met location, connection source, relationship type
- **Consumers**: Contact directory, relationship manager, search indexer, follow-up scheduler, network visualizer

#### ContactUpdated
- **Description**: Contact information has been modified
- **Triggered When**: User updates contact details or job change detected
- **Key Data**: Contact ID, updated fields, previous values, new values, update date, update source (manual/LinkedIn/automated)
- **Consumers**: Contact record, change log, relationship tracker, outreach opportunity detector

#### ContactTagged
- **Description**: Descriptive label has been applied to contact
- **Triggered When**: User categorizes contact with tags
- **Key Data**: Contact ID, tags applied, tag categories, tag date, auto-tagged flag, tag purpose
- **Consumers**: Contact organizer, search filter, segmentation, targeted outreach, relationship grouping

#### ContactRelationshipDefined
- **Description**: Type of professional relationship has been specified
- **Triggered When**: User defines relationship nature (mentor, colleague, client, prospect, etc.)
- **Key Data**: Contact ID, relationship type, relationship strength, connection context, mutual contacts, relationship start date
- **Consumers**: Relationship manager, networking strategy, prioritization, outreach customization

#### ContactDeactivated
- **Description**: Contact has been marked as inactive
- **Triggered When**: User archives contact no longer actively networking with
- **Key Data**: Contact ID, deactivation date, deactivation reason, last interaction date, reactivation conditions
- **Consumers**: Active network filter, relationship pruner, re-engagement detector, archive service

### InteractionEvents

#### InteractionLogged
- **Description**: Communication or meeting with contact has been recorded
- **Triggered When**: User documents interaction with professional contact
- **Key Data**: Interaction ID, contact ID, interaction type, date, duration, location/medium, topics discussed, notes, sentiment, follow-up needed
- **Consumers**: Interaction history, relationship strength calculator, follow-up scheduler, engagement tracker, talking points library

#### MeetingScheduled
- **Description**: Upcoming meeting with contact has been planned
- **Triggered When**: User books coffee, lunch, or meeting with contact
- **Key Data**: Meeting ID, contact ID, scheduled date/time, location, meeting purpose, agenda items, preparation notes, calendar synced
- **Consumers**: Calendar integration, meeting reminder, preparation prompt, interaction anticipator

#### MeetingCompleted
- **Description**: Scheduled meeting has taken place
- **Triggered When**: Meeting occurs and user logs outcome
- **Key Data**: Meeting ID, actual date, attendees, duration, topics covered, outcomes, action items, next steps, relationship impact
- **Consumers**: Interaction history, action item tracker, relationship scorer, follow-up generator, meeting effectiveness

#### EmailExchangeTracked
- **Description**: Email communication with contact has been logged
- **Triggered When**: Significant email correspondence documented or auto-tracked
- **Key Data**: Exchange ID, contact ID, email date, subject, email direction, response time, sentiment, topics, follow-up required
- **Consumers**: Communication history, responsiveness tracker, conversation context, email analytics

### FollowUpEvents

#### FollowUpScheduled
- **Description**: Reminder to follow up with contact has been set
- **Triggered When**: User or system schedules future outreach
- **Key Data**: Follow-up ID, contact ID, scheduled date, follow-up type, follow-up reason, context notes, priority level
- **Consumers**: Reminder scheduler, task manager, relationship maintainer, engagement prompter

#### FollowUpCompleted
- **Description**: Scheduled follow-up has been executed
- **Triggered When**: User completes planned outreach
- **Key Data**: Follow-up ID, completion date, method used, outcome, next follow-up scheduled, relationship impact
- **Consumers**: Follow-up tracker, completion logger, relationship scorer, next action generator

#### FollowUpMissed
- **Description**: Scheduled follow-up was not completed on time
- **Triggered When**: Follow-up due date passes without completion
- **Key Data**: Follow-up ID, missed date, days overdue, contact importance, relationship risk, rescheduled date
- **Consumers**: Overdue alert, relationship risk monitor, priority escalator, follow-up rescheduler

#### AutomaticFollowUpSuggested
- **Description**: System has recommended reaching out to contact
- **Triggered When**: Time-based or event-based trigger suggests contact
- **Key Data**: Suggestion ID, contact ID, suggestion reason, suggested timing, suggested approach, talking points
- **Consumers**: Proactive reminder, relationship maintenance, networking optimization, outreach quality

### RelationshipEvents

#### RelationshipStrengthCalculated
- **Description**: Connection strength score has been computed
- **Triggered When**: System evaluates relationship based on interactions
- **Key Data**: Contact ID, strength score, calculation date, interaction frequency, recency, quality indicators, trend direction
- **Consumers**: Relationship dashboard, prioritization, weak tie identifier, network health monitor

#### StrongTieIdentified
- **Description**: Contact has been recognized as close professional relationship
- **Triggered When**: Relationship strength exceeds threshold
- **Key Data**: Contact ID, strong tie date, relationship factors, mutual benefit level, collaboration history
- **Consumers**: Key relationship highlighter, VIP treatment, prioritization, testimonial requester

#### WeakTieDetected
- **Description**: Relationship has weakened or become dormant
- **Triggered When**: Lack of interaction causes relationship degradation
- **Key Data**: Contact ID, last interaction date, days since contact, previous strength, degradation rate, re-engagement priority
- **Consumers**: Re-engagement alert, relationship rescue, dormant contact awakener, networking opportunity

#### RelationshipMilestoneReached
- **Description**: Significant point in professional relationship achieved
- **Triggered When**: Anniversary, milestone interaction count, or major collaboration
- **Key Data**: Contact ID, milestone type, milestone date, relationship duration, interactions count, milestone significance
- **Consumers**: Celebration prompt, relationship acknowledger, deepening opportunity, gratitude reminder

### NetworkingEventEvents

#### EventAttended
- **Description**: Professional networking event has been attended
- **Triggered When**: User logs attendance at conference, meetup, or industry event
- **Key Data**: Event ID, event name, event type, date, location, attendees met, connections made, follow-up plan
- **Consumers**: Event tracker, new contact source, follow-up scheduler, networking ROI calculator

#### ContactMetAtEvent
- **Description**: New professional connection made at event
- **Triggered When**: User adds contact with event context
- **Key Data**: Contact ID, event ID, meeting context, conversation topics, mutual interests, follow-up commitment
- **Consumers**: Contact creator, event-based follow-up, conversation context, relationship initializer

#### EventFollowUpGenerated
- **Description**: Post-event follow-up tasks have been created
- **Triggered When**: After event, system generates follow-up list
- **Key Data**: Event ID, contacts to follow-up, follow-up timeline, personalized talking points, event takeaways to share
- **Consumers**: Batch follow-up tool, personalized outreach, relationship starter, networking effectiveness

### OpportunityEvents

#### OpportunityIdentified
- **Description**: Potential business or career opportunity discovered through contact
- **Triggered When**: User identifies opportunity via network connection
- **Key Data**: Opportunity ID, contact ID, opportunity type, description, potential value, pursuit date, status
- **Consumers**: Opportunity tracker, relationship ROI, value attribution, networking effectiveness validator

#### IntroductionRequested
- **Description**: Request for introduction to mutual connection has been made
- **Triggered When**: User asks contact to introduce them to third party
- **Key Data**: Request ID, requesting contact, target contact, requested from, request date, introduction purpose, status
- **Consumers**: Introduction tracker, relationship favor bank, networking expansion, introduction facilitator

#### IntroductionMade
- **Description**: Connection has been facilitated between two contacts
- **Triggered When**: User introduces two people in their network
- **Key Data**: Introduction ID, introducer, party 1, party 2, introduction date, introduction context, outcome, relationship strengthener
- **Consumers**: Value provision tracker, relationship builder, network weaver, reciprocity bank

#### ReferralReceived
- **Description**: Contact has provided business or job referral
- **Triggered When**: Connection refers opportunity to user
- **Key Data**: Referral ID, referrer contact ID, referral type, referral details, referral date, outcome, gratitude expressed
- **Consumers**: Referral tracker, relationship value calculator, gratitude manager, reciprocity prompter

### ValueExchangeEvents

#### ValueProvided
- **Description**: User has helped or provided value to contact
- **Triggered When**: User assists contact with introduction, advice, or resource
- **Key Data**: Value ID, contact ID, value type, date provided, description, contact appreciation, relationship deposit
- **Consumers**: Giving tracker, relationship investment, reciprocity balance, relationship strengthener

#### ValueReceived
- **Description**: Contact has provided help or value to user
- **Triggered When**: User receives benefit from professional relationship
- **Key Data**: Value ID, contact ID, value type, date received, description, gratitude expressed, reciprocity owed
- **Consumers**: Value tracker, reciprocity calculator, thank you reminder, relationship debt monitor

#### ReciprocityBalanceCalculated
- **Description**: Give-take balance in relationship has been evaluated
- **Triggered When**: System analyzes value exchange in relationship
- **Key Data**: Contact ID, value given, value received, balance status, relationship health, reciprocity recommendations
- **Consumers**: Relationship health monitor, giving opportunity suggester, relationship sustainability checker

### GoalEvents

#### NetworkingGoalSet
- **Description**: Professional networking objective has been established
- **Triggered When**: User defines networking target
- **Key Data**: Goal ID, goal type, target metric, target date, related contacts, progress tracking, motivation
- **Consumers**: Goal tracker, progress monitor, networking strategy, achievement motivator

#### NetworkingQuotaSet
- **Description**: Target number of interactions or new contacts has been defined
- **Triggered When**: User commits to networking frequency
- **Key Data**: Quota ID, quota type, target number, time period, current progress, quota rationale
- **Consumers**: Activity tracker, quota monitor, motivation system, consistency enforcer

#### NetworkingGoalAchieved
- **Description**: Networking objective has been met
- **Triggered When**: User reaches networking goal
- **Key Data**: Goal ID, achievement date, target, actual, success factors, new goal recommendation
- **Consumers**: Achievement celebration, success analysis, new goal setter, momentum maintainer

### SegmentationEvents

#### ContactSegmentCreated
- **Description**: Group of contacts has been organized by criteria
- **Triggered When**: User creates segment for targeted outreach
- **Key Data**: Segment ID, segment name, criteria, contacts included, purpose, creation date
- **Consumers**: Segment manager, targeted campaigns, relationship strategy, outreach personalization

#### BulkOutreachPlanned
- **Description**: Coordinated communication to contact segment has been prepared
- **Triggered When**: User plans outreach campaign to multiple contacts
- **Key Data**: Campaign ID, target segment, message template, outreach timing, personalization fields, campaign goal
- **Consumers**: Campaign manager, message personalizer, outreach scheduler, effectiveness tracker

### NoteEvents

#### ContactNoteAdded
- **Description**: Important information about contact has been documented
- **Triggered When**: User adds note to contact record
- **Key Data**: Note ID, contact ID, note content, note type, note date, created by, privacy level, searchable
- **Consumers**: Contact context, conversation preparation, relationship insights, searchable knowledge base

#### ConversationTopicLogged
- **Description**: Subject of interest to contact has been recorded
- **Triggered When**: User documents what contact cares about
- **Key Data**: Topic ID, contact ID, topic description, interest level, last discussed, talking point flag
- **Consumers**: Conversation starter, personalization, relationship depth, engagement quality
