# Domain Events - Job Search Organizer

## Overview
This document defines the domain events tracked in the Job Search Organizer application. These events capture significant business occurrences related to job applications, interview scheduling, offer negotiations, job search strategy, and career transition management.

## Events

### JobListingEvents

#### JobListingDiscovered
- **Description**: Potential job opportunity has been identified
- **Triggered When**: User finds job posting of interest
- **Key Data**: Listing ID, job title, company, location, posting URL, discovery date, discovery source, salary range, job type, match score
- **Consumers**: Job pipeline, opportunity tracker, application planner, job board integrator, interest scorer

#### JobListingSaved
- **Description**: Job posting has been bookmarked for consideration
- **Triggered When**: User saves job listing for future action
- **Key Data**: Listing ID, saved date, interest level, application priority, notes, application deadline, referral possibility
- **Consumers**: Saved jobs library, application queue, deadline tracker, priority sorter

#### JobListingArchived
- **Description**: Job opportunity has been dismissed or is no longer relevant
- **Triggered When**: User removes job from active consideration
- **Key Data**: Listing ID, archive date, archive reason (position filled, not interested, too late), decision notes
- **Consumers**: Pipeline cleaner, decision tracker, interest pattern analyzer, archived jobs library

### ApplicationEvents

#### ApplicationStarted
- **Description**: Job application process has been initiated
- **Triggered When**: User begins applying for position
- **Key Data**: Application ID, listing ID, start date, application method, resume version used, cover letter tailored, referral contact
- **Consumers**: Application tracker, status pipeline, resume version tracker, progress monitor

#### ApplicationSubmitted
- **Description**: Job application has been completed and sent
- **Triggered When**: User submits application materials
- **Key Data**: Application ID, submission date, submission method, materials submitted, confirmation received, application deadline met
- **Consumers**: Application pipeline, confirmation tracker, follow-up scheduler, submission log, timeline tracker

#### ApplicationStatusUpdated
- **Description**: Application status has changed
- **Triggered When**: Company updates application status or user receives communication
- **Key Data**: Application ID, previous status, new status, update date, update source, company contact, next expected step
- **Consumers**: Status tracker, pipeline visualizer, notification service, next action prompter

#### ApplicationRejected
- **Description**: Job application has been declined
- **Triggered When**: Company sends rejection notification
- **Key Data**: Application ID, rejection date, rejection stage, rejection reason (if provided), feedback received, time from application to rejection
- **Consumers**: Rejection tracker, feedback analyzer, learning opportunity, application strategy adjuster, emotional support

#### ApplicationWithdrawn
- **Description**: User has pulled application from consideration
- **Triggered When**: User decides not to continue with opportunity
- **Key Data**: Application ID, withdrawal date, withdrawal reason, withdrawal stage, company notified
- **Consumers**: Active pipeline remover, decision tracker, opportunity cost analyzer, pattern identifier

### InterviewEvents

#### InterviewScheduled
- **Description**: Interview has been arranged with company
- **Triggered When**: Company schedules interview with candidate
- **Key Data**: Interview ID, application ID, interview type, date/time, location/platform, interviewers, duration, preparation materials
- **Consumers**: Calendar integration, interview preparation trigger, reminder scheduler, logistics coordinator

#### InterviewPrepared
- **Description**: Candidate has completed interview preparation
- **Triggered When**: User finishes research and practice for interview
- **Key Data**: Interview ID, preparation date, company research done, questions prepared, answers practiced, mock interviews completed
- **Consumers**: Preparation tracker, confidence builder, readiness validator, talking points organizer

#### InterviewCompleted
- **Description**: Interview has been conducted
- **Triggered When**: Interview finishes and user logs details
- **Key Data**: Interview ID, completion date, interviewers met, duration, questions asked, performance self-rating, follow-up actions, thank you sent
- **Consumers**: Interview log, follow-up reminder, performance tracker, interview stage advancer, thank you monitor

#### InterviewRescheduled
- **Description**: Interview date/time has been changed
- **Triggered When**: Either party requests interview reschedule
- **Key Data**: Interview ID, original date, new date, reschedule reason, rescheduled by, reschedule count
- **Consumers**: Calendar updater, preparation re-planner, logistics adjuster, engagement signal tracker

#### ThankYouNoteSent
- **Description**: Post-interview thank you message has been delivered
- **Triggered When**: User sends follow-up thank you email
- **Key Data**: Interview ID, send date, recipients, personalization quality, key points reinforced, timeliness
- **Consumers**: Follow-up tracker, interview protocol completer, impression reinforcer, professional courtesy logger

### InterviewRoundEvents

#### PhoneScreenScheduled
- **Description**: Initial phone interview has been arranged
- **Triggered When**: Recruiter schedules phone screening
- **Key Data**: Application ID, screen date/time, screener name, expected duration, topics to cover, preparation checklist
- **Consumers**: Interview scheduler, preparation planner, first impression readiness, pipeline advancer

#### TechnicalInterviewScheduled
- **Description**: Skills assessment interview has been arranged
- **Triggered When**: Company schedules technical/skills evaluation
- **Key Data**: Interview ID, interview date, assessment type, topics covered, preparation resources, tools needed
- **Consumers**: Technical prep trigger, study planner, skills refresher, assessment readiness

#### OnSiteScheduled
- **Description**: In-person or full-day interview has been arranged
- **Triggered When**: Company invites for final round interview
- **Key Data**: Interview ID, date, location, itinerary, interviewers, travel arrangements, expenses covered, duration
- **Consumers**: Travel planner, schedule blocker, preparation intensifier, logistics coordinator, expense tracker

#### FinalInterviewCompleted
- **Description**: Last interview round has been finished
- **Triggered When**: User completes final interview stage
- **Key Data**: Application ID, completion date, overall impression, decision timeline communicated, next steps, offer likelihood
- **Consumers**: Decision anticipator, offer preparation, negotiation prep trigger, timeline tracker

### OfferEvents

#### OfferReceived
- **Description**: Job offer has been extended
- **Triggered When**: Company makes employment offer
- **Key Data**: Offer ID, application ID, offer date, salary, benefits, start date, offer expiration, decision deadline, offer terms
- **Consumers**: Offer evaluator, negotiation planner, decision timeline, offer comparison tool, excitement tracker

#### OfferNegotiated
- **Description**: Offer terms discussion has occurred
- **Triggered When**: User negotiates salary, benefits, or terms
- **Key Data**: Offer ID, negotiation date, items negotiated, requests made, company responses, negotiation outcome, revised offer
- **Consumers**: Negotiation tracker, offer updater, negotiation effectiveness analyzer, compensation optimizer

#### OfferAccepted
- **Description**: Job offer has been accepted
- **Triggered When**: User commits to employment offer
- **Key Data**: Offer ID, acceptance date, final terms, start date, resignation needed, onboarding start, contract signed
- **Consumers**: Job search closer, resignation prompter, onboarding coordinator, other offer withdrawer, celebration trigger

#### OfferRejected
- **Description**: Job offer has been declined
- **Triggered When**: User turns down employment offer
- **Key Data**: Offer ID, rejection date, rejection reason, counteroffer made, company relationship preservation, alternative accepted
- **Consumers**: Offer decliner, company relationship manager, decision tracker, search continuer

#### OfferExpired
- **Description**: Offer acceptance deadline has passed
- **Triggered When**: Decision deadline reached without acceptance
- **Key Data**: Offer ID, expiration date, decision delay reason, extension requested, opportunity lost
- **Consumers**: Opportunity loss tracker, decision analyzer, timeline review, learning opportunity

### NetworkingEvents

#### ReferralRequested
- **Description**: User has asked for job referral
- **Triggered When**: User requests internal referral from contact
- **Key Data**: Referral request ID, listing ID, contact ID, request date, request message, response received, referral status
- **Consumers**: Referral tracker, contact relationship manager, application advantage tracker, gratitude reminder

#### ReferralReceived
- **Description**: Contact has provided job referral
- **Triggered When**: Network contact submits internal referral
- **Key Data**: Referral ID, application ID, referrer contact, referral date, referral strength, hiring manager notification, referral bonus
- **Consumers**: Application pipeline, referral advantage tracker, thank you prompter, relationship value recorder

#### InformationalInterviewConducted
- **Description**: Informational interview with industry contact has occurred
- **Triggered When**: User conducts career/company research interview
- **Key Data**: Interview ID, contact ID, date, company discussed, insights gained, job leads discovered, follow-up actions
- **Consumers**: Company intelligence, job lead generator, networking tracker, industry knowledge base

### CompanyResearchEvents

#### CompanyResearched
- **Description**: Target company information has been gathered
- **Triggered When**: User researches potential employer
- **Key Data**: Company ID, research date, company culture, products/services, recent news, growth trajectory, employee reviews, notes
- **Consumers**: Company intelligence, interview prep, application targeting, culture fit evaluator

#### CompanyContactIdentified
- **Description**: Relevant person at target company has been found
- **Triggered When**: User identifies hiring manager, recruiter, or employee contact
- **Key Data**: Contact ID, company ID, contact name, role, LinkedIn profile, connection path, outreach plan
- **Consumers**: Networking opportunity, outreach planner, informational interview requester, referral path finder

### SearchStrategyEvents

#### JobSearchGoalSet
- **Description**: Job search objective has been defined
- **Triggered When**: User establishes search parameters and goals
- **Key Data**: Goal ID, target role, target companies, target salary, target location, applications per week goal, timeline, priorities
- **Consumers**: Goal tracker, search focus, activity monitor, success criteria, motivation system

#### WeeklyActivityLogged
- **Description**: Job search activities for week have been recorded
- **Triggered When**: Week ends and activities summarized
- **Key Data**: Week start date, applications submitted, interviews completed, networking events, offers received, time invested
- **Consumers**: Activity tracker, goal progress, search momentum monitor, weekly report, pattern analyzer

#### JobSearchMilestoneReached
- **Description**: Significant search achievement has been attained
- **Triggered When**: Milestone hit (10 applications, first interview, first offer, etc.)
- **Key Data**: Milestone type, achievement date, milestone significance, time to milestone, next milestone, celebration
- **Consumers**: Progress tracker, motivation booster, milestone celebrator, momentum indicator

### DocumentEvents

#### ResumeVersionCreated
- **Description**: Resume has been tailored for specific application
- **Triggered When**: User creates customized resume version
- **Key Data**: Resume ID, application ID, version number, creation date, tailoring approach, keywords emphasized, sections modified
- **Consumers**: Resume library, application tracker, version manager, effectiveness analyzer

#### CoverLetterWritten
- **Description**: Cover letter has been drafted for application
- **Triggered When**: User writes customized cover letter
- **Key Data**: Cover letter ID, application ID, writing date, customization quality, key points emphasized, template used
- **Consumers**: Cover letter library, application materials, personalization tracker, effectiveness monitor

### TimelineEvents

#### ApplicationDeadlineApproaching
- **Description**: Job application deadline is near
- **Triggered When**: Application deadline within warning window (3, 2, 1 days)
- **Key Data**: Listing ID, deadline date, days remaining, application status, materials ready, urgency level
- **Consumers**: Deadline alert, application prioritizer, urgency prompt, submission reminder

#### DecisionDeadlineSet
- **Description**: Timeline for job offer decision has been established
- **Triggered When**: Offer received with decision deadline
- **Key Data**: Offer ID, deadline date, days to decide, factors to consider, other offers pending, extension possible
- **Consumers**: Decision timeline, offer comparison facilitator, urgency manager, deliberation prompter

### FeedbackEvents

#### RejectionFeedbackRequested
- **Description**: User has asked for interview/application feedback
- **Triggered When**: User requests constructive feedback after rejection
- **Key Data**: Application ID, request date, feedback received, improvement areas identified, response quality
- **Consumers**: Learning opportunity, skill gap identifier, application improvement, growth mindset

#### PerformanceSelfAssessed
- **Description**: User has evaluated interview performance
- **Triggered When**: User reflects on interview after completion
- **Key Data**: Interview ID, self-rating, strengths demonstrated, areas for improvement, lessons learned, preparation effectiveness
- **Consumers**: Performance tracker, improvement identifier, interview skill development, preparation refinement
