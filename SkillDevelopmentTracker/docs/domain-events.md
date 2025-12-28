# Domain Events - Skill Development Tracker

## Overview
This document defines the domain events tracked in the Skill Development Tracker application. These events capture significant business occurrences related to learning goals, course completion, skill progression, certifications, and continuous professional development.

## Events

### SkillEvents

#### SkillTargetSet
- **Description**: Learning objective for new skill has been established
- **Triggered When**: User identifies skill they want to develop
- **Key Data**: Skill ID, skill name, skill category, target proficiency level, current proficiency, target date, motivation, career relevance
- **Consumers**: Learning planner, goal tracker, resource recommender, progress monitor, motivation system

#### SkillProgressUpdated
- **Description**: Advancement in skill competency has been recorded
- **Triggered When**: User logs improvement in skill level
- **Key Data**: Skill ID, previous proficiency, new proficiency, update date, evidence of progress, learning hours invested, milestone reached
- **Consumers**: Progress tracker, proficiency dashboard, learning effectiveness analyzer, motivation system

#### SkillMasteryAchieved
- **Description**: Target proficiency level in skill has been reached
- **Triggered When**: User completes skill development goal
- **Key Data**: Skill ID, mastery date, final proficiency level, total learning time, resources used, validation method, next level goal
- **Consumers**: Achievement system, skill portfolio, resume updater, new goal suggester, celebration trigger

#### SkillValidated
- **Description**: Skill competency has been verified
- **Triggered When**: External validation through test, project, or endorsement
- **Key Data**: Skill ID, validation type, validation date, validator, validation score, certification earned, credential
- **Consumers**: Credential vault, skill credibility, resume enhancer, portfolio builder, achievement tracker

### CourseEvents

#### CourseEnrolled
- **Description**: User has registered for learning course or program
- **Triggered When**: User enrolls in online course, bootcamp, or training
- **Key Data**: Course ID, course name, platform, instructor, start date, end date, cost, skills targeted, estimated hours, enrollment date
- **Consumers**: Course tracker, calendar integration, budget tracker, learning plan, progress monitor

#### LessonCompleted
- **Description**: Individual course lesson or module has been finished
- **Triggered When**: User completes course section
- **Key Data**: Lesson ID, course ID, completion date, time spent, quiz score, comprehension rating, notes taken, next lesson
- **Consumers**: Progress tracker, course completion calculator, learning analytics, knowledge retention monitor

#### CourseCompleted
- **Description**: Entire course or program has been finished
- **Triggered When**: User completes all course requirements
- **Key Data**: Course ID, completion date, final grade, certificate earned, total time invested, skills acquired, would recommend rating
- **Consumers**: Achievement system, certificate vault, skill proficiency updater, learning history, new course recommender

#### CourseAbandoned
- **Description**: Enrolled course has been discontinued before completion
- **Triggered When**: User stops progressing through course
- **Key Data**: Course ID, abandonment date, completion percentage, abandonment reason, lessons completed, refund eligibility
- **Consumers**: Learning analytics, course effectiveness evaluator, goal adjuster, re-enrollment consideration

#### QuizPassed
- **Description**: Course assessment has been successfully completed
- **Triggered When**: User achieves passing score on quiz or exam
- **Key Data**: Quiz ID, course ID, pass date, score, passing threshold, attempts taken, topics mastered, weak areas
- **Consumers**: Progress validator, comprehension tracker, skill verification, achievement recorder

#### QuizFailed
- **Description**: Course assessment has not been passed
- **Triggered When**: User scores below passing threshold
- **Key Data**: Quiz ID, score, passing threshold, fail date, attempt number, topics struggled with, remediation needed
- **Consumers**: Learning gap identifier, study focus recommender, retry scheduler, comprehension monitor

### CertificationEvents

#### CertificationPursued
- **Description**: Professional certification goal has been established
- **Triggered When**: User commits to earning specific certification
- **Key Data**: Certification ID, certification name, issuing body, target exam date, preparation plan, estimated study hours, cost, career value
- **Consumers**: Certification tracker, study planner, exam scheduler, resource allocator, goal monitor

#### ExamScheduled
- **Description**: Certification exam date has been booked
- **Triggered When**: User registers for certification test
- **Key Data**: Exam ID, certification ID, exam date, exam location/format, registration fee, preparation deadline, exam topics
- **Consumers**: Exam countdown, study intensifier, preparation checklist, calendar blocker, anxiety manager

#### ExamPassed
- **Description**: Certification exam has been successfully completed
- **Triggered When**: User achieves passing score on certification test
- **Key Data**: Exam ID, certification ID, pass date, score, passing threshold, preparation hours, certification earned, credential number
- **Consumers**: Certification issuer, achievement celebration, resume updater, credential vault, LinkedIn sync, skill validator

#### ExamFailed
- **Description**: Certification exam has not been passed
- **Triggered When**: User scores below passing threshold on certification test
- **Key Data**: Exam ID, score, passing threshold, fail date, weak domains, re-test eligibility, study adjustment needed
- **Consumers**: Retry planner, study focus adjuster, weakness identifier, motivation support, learning strategy reviser

#### CertificationEarned
- **Description**: Professional certification has been officially awarded
- **Triggered When**: Certification body issues credential
- **Key Data**: Certification ID, issue date, certification number, expiration date, issuing organization, verification URL, skill areas covered
- **Consumers**: Credential manager, resume updater, professional profile sync, expiration reminder, achievement tracker

#### CertificationRenewed
- **Description**: Expiring certification has been renewed
- **Triggered When**: User completes renewal requirements
- **Key Data**: Certification ID, renewal date, new expiration date, continuing education completed, renewal cost, updated credential
- **Consumers**: Certification manager, credential updater, resume refresher, professional development tracker

#### CertificationExpiring
- **Description**: Professional certification approaching expiration
- **Triggered When**: Expiration date within warning window (90, 60, 30 days)
- **Key Data**: Certification ID, expiration date, days remaining, renewal requirements, continuing education needed, renewal cost
- **Consumers**: Renewal reminder, continuing education planner, credential maintenance, urgency alerter

### LearningResourceEvents

#### ResourceBookmarked
- **Description**: Learning resource has been saved for future reference
- **Triggered When**: User bookmarks article, video, course, or tutorial
- **Key Data**: Resource ID, resource type, URL, title, topics covered, bookmarked date, skill relevance, priority, completion status
- **Consumers**: Resource library, reading list, learning queue, topic organizer

#### ResourceCompleted
- **Description**: Bookmarked learning resource has been finished
- **Triggered When**: User completes reading or viewing bookmarked content
- **Key Data**: Resource ID, completion date, time spent, key takeaways, rating, practical application, recommend to others
- **Consumers**: Completion tracker, knowledge base, resource rating, learning time calculator

#### BookFinished
- **Description**: Technical or professional book has been read
- **Triggered When**: User completes reading book
- **Key Data**: Book ID, title, author, finish date, start date, rating, key lessons, skills improved, would recommend
- **Consumers**: Reading log, knowledge tracker, book recommender, learning breadth monitor

### PracticeEvents

#### ProjectStarted
- **Description**: Hands-on practice project has been initiated
- **Triggered When**: User begins project to apply learned skills
- **Key Data**: Project ID, project name, skills practiced, start date, project goals, estimated duration, learning objectives
- **Consumers**: Project tracker, skill application monitor, portfolio builder, hands-on learning tracker

#### ProjectCompleted
- **Description**: Practice project has been finished
- **Triggered When**: User completes hands-on learning project
- **Key Data**: Project ID, completion date, skills applied, challenges overcome, outcomes achieved, portfolio worthy, GitHub link
- **Consumers**: Portfolio adder, skill validator, achievement tracker, project showcase, practical experience recorder

#### SkillApplied
- **Description**: Learned skill has been used in real-world scenario
- **Triggered When**: User applies skill in work or project
- **Key Data**: Skill ID, application date, application context, effectiveness, challenges faced, reinforcement value
- **Consumers**: Skill reinforcement tracker, practical experience logger, competency validator, retention enhancer

### GoalEvents

#### LearningGoalSet
- **Description**: Professional development objective has been established
- **Triggered When**: User defines learning target
- **Key Data**: Goal ID, goal description, target skills, target date, motivation, success criteria, resources needed, time commitment
- **Consumers**: Goal tracker, learning planner, resource allocator, progress monitor, accountability system

#### MilestoneReached
- **Description**: Significant checkpoint in learning journey achieved
- **Triggered When**: User completes major step toward learning goal
- **Key Data**: Goal ID, milestone description, achievement date, progress percentage, skills gained, next milestone, celebration
- **Consumers**: Progress tracker, motivation system, achievement logger, momentum maintainer

#### LearningGoalAchieved
- **Description**: Professional development objective has been completed
- **Triggered When**: User meets all learning goal criteria
- **Key Data**: Goal ID, achievement date, skills mastered, time invested, outcomes achieved, success factors, next goal ideas
- **Consumers**: Achievement celebration, goal archiver, success analyzer, new goal recommender, portfolio updater

#### GoalDeadlineExtended
- **Description**: Learning goal timeline has been adjusted
- **Triggered When**: User extends target completion date
- **Key Data**: Goal ID, original deadline, new deadline, extension reason, adjustment date, revised plan
- **Consumers**: Goal tracker, deadline monitor, learning plan adjuster, expectation manager

### TimeTrackingEvents

#### LearningSessionLogged
- **Description**: Study or practice session has been recorded
- **Triggered When**: User logs time spent learning
- **Key Data**: Session ID, date, duration, skill/course studied, activities performed, focus level, productivity rating, interruptions
- **Consumers**: Time tracker, learning analytics, productivity monitor, weekly summary, commitment validator

#### WeeklyLearningHoursCalculated
- **Description**: Total learning time for week has been computed
- **Triggered When**: Week ends and learning hours tallied
- **Key Data**: Week start date, total hours, hours by skill, hours by resource type, goal hours, variance, consistency
- **Consumers**: Weekly report, goal progress, commitment monitor, time investment analyzer, habit tracker

#### LearningStreakAchieved
- **Description**: Consecutive days of learning has reached milestone
- **Triggered When**: Daily learning streak hits milestone (7, 14, 30, 100 days)
- **Key Data**: Streak length, start date, achievement date, average daily time, skills focused on, streak type
- **Consumers**: Achievement system, habit reinforcement, motivation booster, consistency validator

#### LearningStreakBroken
- **Description**: Consecutive days learning streak has ended
- **Triggered When**: Day passes without learning session
- **Key Data**: Broken streak length, break date, longest streak, days since last session, recovery plan
- **Consumers**: Streak tracker, re-engagement prompt, motivation support, habit rebuilder

### AssessmentEvents

#### SelfAssessmentCompleted
- **Description**: User has evaluated their own skill proficiency
- **Triggered When**: User performs self-assessment of skill level
- **Key Data**: Assessment ID, skill ID, assessed proficiency, assessment date, confidence level, evidence cited, growth areas identified
- **Consumers**: Skill proficiency tracker, learning gap identifier, goal setter, progress baseline

#### PeerFeedbackReceived
- **Description**: Colleague has provided skill feedback
- **Triggered When**: Peer evaluates user's skill competency
- **Key Data**: Feedback ID, skill ID, reviewer, feedback date, proficiency rating, strengths noted, improvement suggestions
- **Consumers**: External validation, blind spot identifier, skill credibility, development focus

### PathEvents

#### LearningPathCreated
- **Description**: Structured curriculum for skill development has been designed
- **Triggered When**: User creates sequential learning plan
- **Key Data**: Path ID, path name, target skills, courses included, estimated duration, path difficulty, career goal alignment
- **Consumers**: Learning roadmap, curriculum organizer, progress tracker, resource sequencer

#### PathProgressTracked
- **Description**: Advancement through learning path has been updated
- **Triggered When**: User completes step in learning path
- **Key Data**: Path ID, current step, completion percentage, steps completed, estimated completion date, pace
- **Consumers**: Progress monitor, completion forecaster, pacing analyzer, motivation system
