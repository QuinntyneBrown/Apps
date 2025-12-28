# Domain Events - Men's Group Discussion Tracker

## Overview
This application supports men's groups in organizing meetings, tracking discussions, sharing resources, and fostering accountability. Domain events capture group gatherings, discussion topics, personal growth commitments, and the development of meaningful male friendships.

## Events

### MeetingEvents

#### MeetingScheduled
- **Description**: A new group meeting has been scheduled
- **Triggered When**: Group organizer or member sets date for gathering
- **Key Data**: Meeting ID, date/time, location/virtual link, topic preview, facilitator ID, expected attendance, timestamp
- **Consumers**: Calendar integration, member notifications, attendance tracking, reminder scheduler

#### MeetingStarted
- **Description**: A group meeting has commenced
- **Triggered When**: Facilitator marks meeting as in progress
- **Key Data**: Meeting ID, actual start time, attendees present, opening topic, timestamp
- **Consumers**: Attendance logger, duration tracker, discussion recorder

#### MeetingCompleted
- **Description**: A group meeting has concluded
- **Triggered When**: Meeting ends and is marked complete
- **Key Data**: Meeting ID, end time, duration, topics covered, attendance count, next meeting scheduled, timestamp
- **Consumers**: Meeting history, topic archive, follow-up task generator, analytics

#### AttendanceRecorded
- **Description**: Member attendance at meeting has been logged
- **Triggered When**: Member checks in or attendance is taken
- **Key Data**: Attendance ID, meeting ID, member ID, arrival time, participation level, timestamp
- **Consumers**: Attendance tracker, commitment metrics, member engagement analysis

#### MeetingCancelled
- **Description**: A scheduled meeting has been cancelled
- **Triggered When**: Organizer cancels due to low attendance or other reasons
- **Key Data**: Meeting ID, cancellation reason, notice period, rescheduled date (if any), timestamp
- **Consumers**: Member notifications, calendar updates, attendance pattern analyzer

### DiscussionEvents

#### DiscussionTopicProposed
- **Description**: A topic has been suggested for group discussion
- **Triggered When**: Member submits topic idea for consideration
- **Key Data**: Topic ID, proposer ID, topic title, description, relevance, urgency, suggested resources, timestamp
- **Consumers**: Topic queue, voting system, facilitator planning, topic curation

#### TopicDiscussed
- **Description**: A topic has been covered in group meeting
- **Triggered When**: Group engages with a discussion topic
- **Key Data**: Discussion ID, meeting ID, topic ID, duration, key points raised, participants active, depth rating, timestamp
- **Consumers**: Discussion archive, topic completion tracker, insight aggregator

#### InsightShared
- **Description**: A member has shared a significant insight or perspective
- **Triggered When**: Valuable contribution is made during discussion
- **Key Data**: Insight ID, discussion ID, member ID, insight content, impact rating, related scripture/quote, timestamp
- **Consumers**: Wisdom collection, member contribution tracker, inspiration archive

#### QuestionRaised
- **Description**: An important question has been posed to the group
- **Triggered When**: Member asks question for group consideration
- **Key Data**: Question ID, discussion ID, asker ID, question content, question type, answers received count, timestamp
- **Consumers**: Q&A tracker, facilitator notes, unresolved questions list

### AccountabilityEvents

#### AccountabilityGoalSet
- **Description**: A member has committed to a personal goal with group accountability
- **Triggered When**: Member shares goal and requests group support
- **Key Data**: Goal ID, member ID, goal description, timeframe, check-in frequency, accountability partner IDs, timestamp
- **Consumers**: Accountability tracker, check-in scheduler, progress monitor, support system

#### ProgressCheckInCompleted
- **Description**: Member has reported progress on accountability goal
- **Triggered When**: Scheduled check-in occurs or member provides update
- **Key Data**: Check-in ID, goal ID, member ID, progress report, challenges faced, victories, next steps, timestamp
- **Consumers**: Progress dashboard, encouragement trigger, accountability partner notification

#### AccountabilityPartnerAssigned
- **Description**: Members have been paired for mutual accountability
- **Triggered When**: Partnership is established for specific support
- **Key Data**: Partnership ID, both member IDs, focus area, check-in agreement, duration, timestamp
- **Consumers**: Partner matching system, check-in reminder, relationship strength tracker

#### GoalAchieved
- **Description**: A member has successfully completed their accountability goal
- **Triggered When**: Goal completion is confirmed and celebrated
- **Key Data**: Goal ID, member ID, completion date, success story, lessons learned, celebration notes, timestamp
- **Consumers**: Success tracker, group encouragement system, testimony archive

#### StrugglesShared
- **Description**: Member has vulnerably shared struggles or challenges
- **Triggered When**: Member opens up about difficulties
- **Key Data**: Share ID, member ID, meeting ID, struggle type, vulnerability level, support requested, timestamp
- **Consumers**: Support mobilization, prayer request system, care follow-up, trust building metrics

### ResourceEvents

#### ResourceShared
- **Description**: A helpful resource has been shared with the group
- **Triggered When**: Member posts book, article, video, or tool recommendation
- **Key Data**: Resource ID, sharer ID, resource type, title, URL/reference, topic tags, relevance, timestamp
- **Consumers**: Resource library, categorization system, recommendation engine

#### ReadingAssigned
- **Description**: Material has been assigned for group to read
- **Triggered When**: Facilitator sets reading for next meeting
- **Key Data**: Assignment ID, resource ID, due date, discussion questions, assigned by ID, timestamp
- **Consumers**: Assignment tracker, completion monitor, discussion prep system

#### StudyGuideCreated
- **Description**: Discussion guide or study material has been developed
- **Triggered When**: Facilitator or member creates structured content
- **Key Data**: Guide ID, creator ID, topic, questions/exercises, recommended duration, timestamp
- **Consumers**: Content library, facilitator resources, study plan generator

### MemberEvents

#### NewMemberJoined
- **Description**: A new man has joined the group
- **Triggered When**: New member is added after vetting/invitation
- **Key Data**: Member ID, name, join date, invited by ID, background, interests, timestamp
- **Consumers**: Welcome workflow, member directory, relationship building, group dynamics

#### MembershipCommitmentSigned
- **Description**: Member has agreed to group covenant or commitment
- **Triggered When**: Member accepts group values and participation expectations
- **Key Data**: Commitment ID, member ID, covenant version, agreement date, timestamp
- **Consumers**: Member status tracker, accountability baseline, group culture enforcement

#### MemberInactive
- **Description**: A member has become inactive or left the group
- **Triggered When**: Extended absence or formal departure
- **Key Data**: Member ID, inactivity start date, reason (if known), exit interview notes, timestamp
- **Consumers**: Roster updates, outreach consideration, alumni tracking

### PrayerEvents

#### PrayerRequestSubmitted
- **Description**: A member has requested prayer from the group
- **Triggered When**: Member shares prayer need
- **Key Data**: Request ID, requester ID, request content, urgency, confidentiality level, timestamp
- **Consumers**: Prayer list, group notification, intercessor assignment, follow-up system

#### PrayerAnswered
- **Description**: A prayer request has been answered or resolved
- **Triggered When**: Member reports answered prayer
- **Key Data**: Request ID, answer description, testimony, time to answer, timestamp
- **Consumers**: Praise report system, faith building archive, testimony collection

#### GroupPrayerSession
- **Description**: Group has engaged in dedicated prayer time
- **Triggered When**: Prayer time is held during or outside meeting
- **Key Data**: Session ID, meeting ID, duration, prayer focuses, participants, timestamp
- **Consumers**: Spiritual health tracker, prayer culture metrics, session history

### LeadershipEvents

#### FacilitatorRotated
- **Description**: Meeting facilitation has been assigned to different member
- **Triggered When**: Leadership rotation occurs
- **Key Data**: Assignment ID, meeting ID, facilitator ID, previous facilitator, preparation status, timestamp
- **Consumers**: Facilitator schedule, preparation reminders, leadership development tracker

#### GroupDecisionMade
- **Description**: Important decision has been made by the group
- **Triggered When**: Consensus reached on group matter
- **Key Data**: Decision ID, decision topic, outcome, voting results (if applicable), implementation plan, timestamp
- **Consumers**: Decision log, implementation tracker, accountability system
