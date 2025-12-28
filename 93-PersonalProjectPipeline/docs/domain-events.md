# Domain Events - Personal Project Pipeline

## Overview
This application helps users manage their personal projects through a Kanban-style pipeline, tracking ideas through execution to completion. Domain events capture project creation, status transitions, priority management, and the journey from conception to achievement.

## Events

### ProjectEvents

#### ProjectIdeaCaptured
- **Description**: A new project idea has been documented
- **Triggered When**: User records a potential project
- **Key Data**: Project ID, user ID, title, initial description, inspiration source, category, capture timestamp
- **Consumers**: Ideas backlog, categorization system, idea evaluation queue

#### ProjectDefined
- **Description**: An idea has been fleshed out into a defined project
- **Triggered When**: User completes project definition with goals and scope
- **Key Data**: Project ID, detailed description, goals, success criteria, estimated effort, resources needed, timestamp
- **Consumers**: Project planning system, feasibility analyzer, portfolio manager

#### ProjectStarted
- **Description**: Work on a project has officially begun
- **Triggered When**: Project moves from planning to active execution
- **Key Data**: Project ID, start date, initial tasks, allocated time/resources, target completion, timestamp
- **Consumers**: Active projects tracker, time allocation system, progress monitoring

#### ProjectCompleted
- **Description**: A project has been finished successfully
- **Triggered When**: Project reaches completion criteria
- **Key Data**: Project ID, completion date, actual vs. estimated effort, outcomes achieved, lessons learned, timestamp
- **Consumers**: Project archive, success analytics, portfolio stats, celebration system

#### ProjectAbandoned
- **Description**: A project has been discontinued before completion
- **Triggered When**: User decides to stop pursuing a project
- **Key Data**: Project ID, abandonment date, progress at abandonment, abandonment reason, salvageable elements, timestamp
- **Consumers**: Project graveyard, lesson extraction, resource recovery

#### ProjectPaused
- **Description**: An active project has been temporarily halted
- **Triggered When**: User needs to pause work on a project
- **Key Data**: Project ID, pause date, pause reason, intended resume date, preserved context, timestamp
- **Consumers**: Paused projects tracker, resume reminder, context preservation

### StatusEvents

#### ProjectMovedToBacklog
- **Description**: A project has been moved to the ideas backlog
- **Triggered When**: Project is captured but not yet ready for planning
- **Key Data**: Project ID, backlog position, review timeline, timestamp
- **Consumers**: Backlog manager, periodic review scheduler, idea incubation

#### ProjectMovedToPlanning
- **Description**: A project has advanced to the planning stage
- **Triggered When**: Project selected for detailed planning
- **Key Data**: Project ID, planning start date, planning tasks, estimated planning duration, timestamp
- **Consumers**: Planning workflow, resource assessment, feasibility analysis

#### ProjectMovedToActive
- **Description**: A project has transitioned to active work
- **Triggered When**: Project execution begins
- **Key Data**: Project ID, activation date, assigned priority, allocated resources, timestamp
- **Consumers**: Active portfolio, capacity management, progress tracking initialization

#### ProjectMovedToReview
- **Description**: A project has entered the review/testing phase
- **Triggered When**: Main work is complete and project needs evaluation
- **Key Data**: Project ID, review start date, review criteria, reviewers, timestamp
- **Consumers**: Quality assurance, completion verification, refinement identification

#### ProjectMovedToDone
- **Description**: A project has been marked as complete and archived
- **Triggered When**: Project successfully finishes and is archived
- **Key Data**: Project ID, completion date, archive category, final notes, timestamp
- **Consumers**: Completed projects archive, success metrics, portfolio history

### PriorityEvents

#### ProjectPrioritized
- **Description**: A priority level has been assigned to a project
- **Triggered When**: User sets project priority
- **Key Data**: Project ID, priority level, prioritization factors, comparison to other projects, timestamp
- **Consumers**: Priority queue, resource allocation, scheduling system

#### PriorityChanged
- **Description**: A project's priority has been updated
- **Triggered When**: User adjusts project importance
- **Key Data**: Project ID, old priority, new priority, change reason, impact on other projects, timestamp
- **Consumers**: Portfolio rebalancing, schedule adjustment, focus redirection

#### ProjectBecameUrgent
- **Description**: A project has been escalated to urgent status
- **Triggered When**: Time sensitivity or importance dramatically increases
- **Key Data**: Project ID, urgency trigger, deadline, priority boost, timestamp
- **Consumers**: Urgent project handler, calendar adjustment, capacity reallocation

### ProgressEvents

#### MilestoneCreated
- **Description**: A project milestone has been defined
- **Triggered When**: User breaks project into milestones
- **Key Data**: Milestone ID, project ID, milestone name, target date, success criteria, timestamp
- **Consumers**: Milestone tracker, progress visualization, deadline monitoring

#### MilestoneAchieved
- **Description**: A project milestone has been reached
- **Triggered When**: Milestone criteria are met
- **Key Data**: Milestone ID, project ID, achievement date, actual vs. planned, celebration notes, timestamp
- **Consumers**: Progress dashboard, motivation system, project health indicator

#### ProgressUpdated
- **Description**: Project progress percentage has been updated
- **Triggered When**: User logs advancement on project
- **Key Data**: Progress update ID, project ID, new percentage, work completed, remaining work, timestamp
- **Consumers**: Progress visualization, completion estimation, velocity tracking

#### BlockerIdentified
- **Description**: An obstacle preventing project progress has been noted
- **Triggered When**: User identifies something blocking advancement
- **Key Data**: Blocker ID, project ID, blocker description, impact severity, resolution approaches, timestamp
- **Consumers**: Blocker management, resolution tracking, risk management

#### BlockerResolved
- **Description**: A project blocker has been overcome
- **Triggered When**: Obstacle is removed and progress can continue
- **Key Data**: Blocker ID, project ID, resolution method, resolution date, time blocked, timestamp
- **Consumers**: Progress resumption, blocker analytics, problem-solving patterns

### TaskEvents

#### TaskAddedToProject
- **Description**: A specific task has been created for a project
- **Triggered When**: User breaks project into actionable tasks
- **Key Data**: Task ID, project ID, task description, estimated effort, dependencies, timestamp
- **Consumers**: Task management, work breakdown, effort estimation

#### TaskCompleted
- **Description**: A project task has been finished
- **Triggered When**: Task is marked as done
- **Key Data**: Task ID, project ID, completion date, actual effort, quality notes, timestamp
- **Consumers**: Project progress calculation, task velocity, completion tracking

#### TaskRolledOver
- **Description**: An incomplete task has been moved to next work session
- **Triggered When**: Task not completed as planned
- **Key Data**: Task ID, project ID, times rolled over, new target date, timestamp
- **Consumers**: Task persistence tracker, estimation improvement, procrastination detection

### ResourceEvents

#### ResourceAllocated
- **Description**: Time or resources have been committed to a project
- **Triggered When**: User assigns capacity to project work
- **Key Data**: Allocation ID, project ID, resource type, amount, time period, timestamp
- **Consumers**: Resource management, capacity planning, availability tracking

#### TimeSpentLogged
- **Description**: Work time on a project has been recorded
- **Triggered When**: User logs hours worked on project
- **Key Data**: Time log ID, project ID, duration, date, activity type, productivity level, timestamp
- **Consumers**: Time tracking, effort analysis, project costing

#### BudgetSet
- **Description**: A financial or time budget has been established for a project
- **Triggered When**: User defines resource limits
- **Key Data**: Budget ID, project ID, budget type, amount, justification, timestamp
- **Consumers**: Budget tracking, resource constraint management, overage prevention

#### BudgetExceeded
- **Description**: Project resource usage has surpassed budget
- **Triggered When**: Allocated resources are consumed
- **Key Data**: Budget ID, project ID, budgeted amount, actual amount, overage, timestamp
- **Consumers**: Budget alert system, scope reassessment, resource reallocation

### CollaborationEvents

#### CollaboratorAdded
- **Description**: Another person has been added to a project
- **Triggered When**: User includes someone else in project work
- **Key Data**: Collaborator ID, project ID, person details, role, responsibilities, timestamp
- **Consumers**: Collaboration management, access control, communication routing

#### ProjectShared
- **Description**: Project details have been shared with others
- **Triggered When**: User shows project to someone
- **Key Data**: Share ID, project ID, recipient(s), share context, feedback requested, timestamp
- **Consumers**: Social features, feedback collection, accountability

#### FeedbackReceived
- **Description**: Input or comments on a project have been collected
- **Triggered When**: Others provide feedback on project
- **Key Data**: Feedback ID, project ID, provider, feedback content, action items, timestamp
- **Consumers**: Improvement integration, quality enhancement, external perspective

### ReviewEvents

#### ProjectReviewScheduled
- **Description**: A project retrospective has been planned
- **Triggered When**: User sets time to review project
- **Key Data**: Review ID, project ID, scheduled date, review scope, participants, timestamp
- **Consumers**: Review calendar, preparation reminders, retrospective system

#### ProjectReviewCompleted
- **Description**: A project retrospective has been conducted
- **Triggered When**: User completes project reflection
- **Key Data**: Review ID, project ID, what worked, what didn't, lessons learned, process improvements, timestamp
- **Consumers**: Lessons library, process improvement, future project planning

#### PortfolioReviewConducted
- **Description**: Overall project portfolio has been evaluated
- **Triggered When**: User reviews all active and planned projects
- **Key Data**: Portfolio review ID, date, active projects analyzed, capacity assessment, rebalancing decisions, timestamp
- **Consumers**: Strategic planning, capacity management, priority realignment

### MetricsEvents

#### CompletionRateCalculated
- **Description**: Project completion statistics have been computed
- **Triggered When**: Periodic analysis of project success rate
- **Key Data**: Metric ID, time period, projects started, projects completed, completion rate, timestamp
- **Consumers**: Performance analytics, success tracking, habit assessment

#### ProjectVelocityMeasured
- **Description**: Rate of project progress has been quantified
- **Triggered When**: Analysis of how quickly projects advance
- **Key Data**: Velocity ID, project ID or overall, progress rate, trend, timestamp
- **Consumers**: Estimation improvement, planning accuracy, capacity understanding
