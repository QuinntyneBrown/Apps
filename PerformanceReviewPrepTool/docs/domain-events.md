# Domain Events - Performance Review Prep Tool

## Overview
This application tracks domain events related to performance reviews, achievement documentation, goal setting, and career development. These events enable employees to effectively prepare for reviews and demonstrate their contributions.

## Events

### AchievementEvents

#### AchievementRecorded
- **Description**: A professional achievement or accomplishment has been documented
- **Triggered When**: User logs a significant accomplishment, win, or contribution
- **Key Data**: Achievement ID, title, description, date achieved, impact metrics, team members involved, project/initiative, visibility level
- **Consumers**: Performance review compiler, achievement portfolio, resume builder, self-evaluation generator, impact tracker

#### ProjectMilestoneCompleted
- **Description**: A significant project milestone has been reached
- **Triggered When**: User marks completion of a major project deliverable or phase
- **Key Data**: Milestone ID, project name, milestone description, completion date, success metrics, challenges overcome, stakeholders, business value
- **Consumers**: Project tracker, performance documentation, achievement aggregator, impact calculator

#### RecognitionReceived
- **Description**: User has received recognition or praise from colleagues or management
- **Triggered When**: User documents formal or informal recognition
- **Key Data**: Recognition ID, type (award/praise/thank you), from whom, date, reason, visibility (public/private/team), recognition context, related achievement
- **Consumers**: Morale tracker, achievement portfolio, peer feedback analyzer, performance review compiler

#### ImpactMetricAchieved
- **Description**: A quantifiable business impact metric has been achieved
- **Triggered When**: User records a measurable contribution to business goals
- **Key Data**: Metric ID, metric type (revenue/efficiency/quality/customer satisfaction), value achieved, baseline, improvement percentage, date, supporting data
- **Consumers**: Impact calculator, performance dashboard, business value tracker, data-driven review preparation

### GoalEvents

#### GoalSet
- **Description**: A professional goal has been established
- **Triggered When**: User creates a new performance or development goal
- **Key Data**: Goal ID, goal description, category (performance/development/project), target date, success criteria, priority, alignment with company objectives
- **Consumers**: Goal tracker, progress monitor, reminder service, performance review preparation, OKR system

#### GoalProgressUpdated
- **Description**: Progress toward a goal has been updated
- **Triggered When**: User updates the status or completion percentage of a goal
- **Key Data**: Goal ID, progress percentage, update notes, blockers encountered, achievements toward goal, update date, revised timeline
- **Consumers**: Progress dashboard, manager updates, performance tracking, goal achievement predictor

#### GoalCompleted
- **Description**: A professional goal has been successfully achieved
- **Triggered When**: User marks a goal as completed
- **Key Data**: Goal ID, completion date, final outcome, success metrics, lessons learned, exceeded/met/partially met status, next steps
- **Consumers**: Achievement tracker, performance review compiler, success analytics, career milestone recorder

#### GoalRevisedOrAbandoned
- **Description**: A goal has been modified or discontinued
- **Triggered When**: User changes goal parameters or marks as no longer relevant
- **Key Data**: Goal ID, revision type, original goal, revised goal, reason for change, change date, approval status, impact assessment
- **Consumers**: Goal management system, change tracker, performance documentation, planning adjustment

### FeedbackEvents

#### PeerFeedbackRequested
- **Description**: User has requested feedback from a colleague
- **Triggered When**: User sends a feedback request to one or more peers
- **Key Data**: Request ID, recipient(s), feedback topic, specific questions, due date, anonymity preference, project context, request date
- **Consumers**: Feedback collection system, reminder service, response tracker, 360-review compiler

#### PeerFeedbackReceived
- **Description**: Feedback from a peer has been received
- **Triggered When**: A colleague submits requested feedback
- **Key Data**: Feedback ID, provider, feedback content, rating/scores, strengths identified, areas for improvement, examples provided, submission date
- **Consumers**: Feedback analyzer, performance review preparation, development planning, sentiment analysis

#### ManagerFeedbackDocumented
- **Description**: Feedback from manager has been recorded
- **Triggered When**: User logs feedback received from their manager
- **Key Data**: Feedback ID, date received, feedback type (formal/informal), content, action items, positive highlights, improvement areas, meeting context
- **Consumers**: Performance documentation, development tracker, one-on-one notes, career progression analyzer

#### SelfReflectionCompleted
- **Description**: User has completed a self-reflection or self-assessment
- **Triggered When**: User documents their own performance evaluation
- **Key Data**: Reflection ID, review period, strengths, accomplishments, areas for growth, challenges faced, support needed, career aspirations
- **Consumers**: Self-evaluation generator, development planning, performance review preparation, career counseling

### ReviewPreparationEvents

#### PerformanceReviewScheduled
- **Description**: A performance review meeting has been scheduled
- **Triggered When**: User receives notification of upcoming review or sets reminder
- **Key Data**: Review ID, scheduled date, review type (annual/quarterly/mid-year), manager, preparation deadline, review period covered, format
- **Consumers**: Reminder service, preparation checklist generator, document compiler, calendar integration

#### ReviewDocumentationCompiled
- **Description**: Supporting documentation for review has been compiled
- **Triggered When**: User generates comprehensive review preparation package
- **Key Data**: Package ID, review ID, achievements included, goals summary, feedback compilation, metrics/KPIs, time period, generation date
- **Consumers**: Document formatter, review meeting prep, manager sharing, archive system

#### SelfEvaluationSubmitted
- **Description**: User has submitted their self-evaluation for review
- **Triggered When**: User completes and submits formal self-assessment
- **Key Data**: Evaluation ID, review ID, submission date, ratings provided, narrative responses, goals achieved, development requests, compensation expectations
- **Consumers**: Review workflow, manager review system, HR system, performance records

#### ReviewCompleted
- **Description**: Performance review meeting has been completed
- **Triggered When**: Review meeting concludes and outcomes are documented
- **Key Data**: Review ID, completion date, overall rating, manager assessment, development plan, salary/promotion outcome, next review date, action items
- **Consumers**: Performance history, compensation tracker, development planning, career progression tracker

### DevelopmentEvents

#### SkillDevelopmentGoalSet
- **Description**: A specific skill development objective has been established
- **Triggered When**: User identifies a skill to develop and creates a plan
- **Key Data**: Development goal ID, skill name, current proficiency, target proficiency, learning resources, timeline, business justification, success metrics
- **Consumers**: Learning management, skill tracker, career development, training recommender

#### TrainingCompleted
- **Description**: User has completed a training program or course
- **Triggered When**: User finishes professional development training
- **Key Data**: Training ID, course name, provider, completion date, hours invested, certificate earned, skills gained, application opportunities
- **Consumers**: Professional development tracker, skill inventory, achievement log, training ROI analyzer

#### MentorshipEngagementRecorded
- **Description**: User has engaged with a mentor or mentee
- **Triggered When**: User logs mentorship session or interaction
- **Key Data**: Engagement ID, mentor/mentee name, session date, topics discussed, advice received/given, action items, relationship duration, value rating
- **Consumers**: Mentorship tracker, development planning, relationship manager, leadership development

#### CareerDevelopmentPlanUpdated
- **Description**: User's career development plan has been revised
- **Triggered When**: User updates their career trajectory or development roadmap
- **Key Data**: Plan ID, career goals, target roles, skill gaps, development activities, timeline, milestones, stakeholder support needed, update date
- **Consumers**: Career planning tool, development tracker, goal alignment, succession planning
