# Domain Events - Personal Mission Statement Builder

## Overview
This application guides users through creating, refining, and living by their personal mission statement. Domain events capture the development of values, mission articulation, alignment tracking, and the journey of purposeful living.

## Events

### MissionDevelopmentEvents

#### MissionDraftCreated
- **Description**: User has created an initial draft of their mission statement
- **Triggered When**: User completes first version of mission statement
- **Key Data**: Draft ID, user ID, mission text, word count, creation context, timestamp
- **Consumers**: Draft management, version control, development tracker

#### MissionRevised
- **Description**: Mission statement has been updated or refined
- **Triggered When**: User edits their mission statement
- **Key Data**: Revision ID, mission ID, previous version, new version, revision reason, timestamp
- **Consumers**: Version history, evolution tracker, refinement analytics

#### MissionFinalized
- **Description**: User has committed to their mission statement
- **Triggered When**: User marks mission as complete and active
- **Key Data**: Mission ID, finalized text, commitment date, intended review frequency, timestamp
- **Consumers**: Active mission tracker, reminder scheduler, alignment monitoring

#### MissionReaffirmed
- **Description**: User has reviewed and recommitted to their mission
- **Triggered When**: Periodic review confirms continued alignment
- **Key Data**: Reaffirmation ID, mission ID, review date, any adjustments, recommitment notes, timestamp
- **Consumers**: Commitment tracker, mission strength monitor, renewal celebration

### ValueEvents

#### CoreValueIdentified
- **Description**: A core personal value has been defined
- **Triggered When**: User names a fundamental guiding value
- **Key Data**: Value ID, value name, description, importance ranking, real-life examples, timestamp
- **Consumers**: Values library, mission foundation, decision-making framework

#### ValuePrioritized
- **Description**: Values have been ranked in order of importance
- **Triggered When**: User establishes priority order among values
- **Key Data**: Prioritization ID, value IDs in order, ranking methodology, timestamp
- **Consumers**: Decision framework, conflict resolution guide, clarity enhancer

#### ValueConflictIdentified
- **Description**: A conflict between values or value/action has been recognized
- **Triggered When**: User identifies misalignment between stated values
- **Key Data**: Conflict ID, conflicting value IDs or value/action pair, context, resolution attempt, timestamp
- **Consumers**: Values refinement system, integrity tracker, growth opportunity identifier

#### ValueLivedOut
- **Description**: User has acted in alignment with a core value
- **Triggered When**: User logs instance of living their values
- **Key Data**: Instance ID, value ID, action taken, situation, outcome, timestamp
- **Consumers**: Integrity tracker, alignment metrics, encouragement system

### PurposeEvents

#### LifePurposeArticulated
- **Description**: User has defined their overarching life purpose
- **Triggered When**: User completes life purpose statement
- **Key Data**: Purpose ID, purpose statement, supporting reasoning, connection to values, timestamp
- **Consumers**: Purpose tracker, mission alignment checker, meaning-making system

#### PurposeQuestionAnswered
- **Description**: User has responded to a guided purpose-finding question
- **Triggered When**: User works through purpose discovery exercises
- **Key Data**: Answer ID, question ID, response content, insights gained, timestamp
- **Consumers**: Purpose development pathway, insight aggregator, self-discovery tracker

#### LegacyVisionDefined
- **Description**: User has articulated their desired legacy
- **Triggered When**: User describes how they want to be remembered
- **Key Data**: Vision ID, legacy description, timeframe, impact areas, stakeholders, timestamp
- **Consumers**: Legacy planning, long-term alignment, impact tracking

### AlignmentEvents

#### DecisionAlignmentChecked
- **Description**: User has evaluated a decision against their mission
- **Triggered When**: User uses mission to guide decision-making
- **Key Data**: Check ID, decision description, alignment score, mission elements referenced, timestamp
- **Consumers**: Decision quality tracker, mission utility metrics, practical application monitor

#### MisalignmentDetected
- **Description**: An action or commitment conflicts with stated mission
- **Triggered When**: User or system identifies conflict between behavior and mission
- **Key Data**: Misalignment ID, mission element, conflicting action, severity, timestamp
- **Consumers**: Integrity alert, course correction prompt, values clarification opportunity

#### RealignmentActionTaken
- **Description**: User has taken steps to better align with mission
- **Triggered When**: Corrective action is taken to restore alignment
- **Key Data**: Action ID, previous misalignment ID, corrective steps, outcome, timestamp
- **Consumers**: Integrity restoration tracker, commitment monitor, growth documentation

#### CommitmentEvaluated
- **Description**: Existing commitment has been assessed against mission
- **Triggered When**: User reviews obligations for mission alignment
- **Key Data**: Evaluation ID, commitment description, alignment rating, keep/modify/release decision, timestamp
- **Consumers**: Life simplification, priority clarification, boundary setting support

### GoalEvents

#### MissionAlignedGoalSet
- **Description**: A goal derived from mission statement has been established
- **Triggered When**: User creates goal that serves their mission
- **Key Data**: Goal ID, mission element addressed, goal description, timeframe, success metrics, timestamp
- **Consumers**: Goal tracking, mission execution, progress monitoring

#### GoalProgressTracked
- **Description**: Progress toward mission-aligned goal has been logged
- **Triggered When**: User updates goal status
- **Key Data**: Progress ID, goal ID, progress percentage, milestones reached, obstacles, timestamp
- **Consumers**: Achievement tracker, mission effectiveness metrics, motivation system

#### MissionImpactRecorded
- **Description**: Tangible impact from living mission has been documented
- **Triggered When**: User records specific outcome from mission alignment
- **Key Data**: Impact ID, impact description, people affected, satisfaction level, timestamp
- **Consumers**: Impact aggregator, mission validation, encouragement feed

### ReviewEvents

#### PeriodicReviewScheduled
- **Description**: Mission review session has been scheduled
- **Triggered When**: User sets date for mission evaluation
- **Key Data**: Review ID, scheduled date, review scope, preparation items, timestamp
- **Consumers**: Calendar integration, reminder service, review preparation

#### MissionReviewCompleted
- **Description**: Formal mission review has been conducted
- **Triggered When**: User completes comprehensive mission evaluation
- **Key Data**: Review ID, review date, findings, adjustments needed, recommitment level, timestamp
- **Consumers**: Mission evolution tracker, health assessment, adjustment trigger

#### LifeChangeProcessed
- **Description**: Significant life change has been evaluated for mission impact
- **Triggered When**: Major life event prompts mission reconsideration
- **Key Data**: Change ID, change description, mission implications, required adjustments, timestamp
- **Consumers**: Mission adaptation system, resilience tracker, context awareness

### SharingEvents

#### MissionSharedWithOthers
- **Description**: User has shared their mission statement
- **Triggered When**: User discloses mission to specific people or publicly
- **Key Data**: Share ID, mission ID, recipient(s), share context, vulnerability level, timestamp
- **Consumers**: Accountability network, social commitment, influence tracker

#### AccountabilityPartnerAssigned
- **Description**: Someone has been invited to help hold user accountable to mission
- **Triggered When**: User designates accountability relationship
- **Key Data**: Partnership ID, partner ID, accountability scope, check-in frequency, timestamp
- **Consumers**: Accountability system, relationship tracker, support network

#### FeedbackReceived
- **Description**: Others have provided input on user's mission or alignment
- **Triggered When**: Accountability partner or trusted person gives feedback
- **Key Data**: Feedback ID, provider ID, feedback content, mission area addressed, timestamp
- **Consumers**: External perspective integration, blind spot identification, mission refinement

### InspirationEvents

#### InspirationCaptured
- **Description**: Content that inspires or informs mission has been saved
- **Triggered When**: User collects quote, article, or example relevant to mission
- **Key Data**: Inspiration ID, content type, source, relevance to mission, personal notes, timestamp
- **Consumers**: Inspiration library, motivation system, mission reinforcement

#### RoleModelIdentified
- **Description**: Person exemplifying user's mission has been noted
- **Triggered When**: User identifies someone living similarly to their mission
- **Key Data**: Role model ID, person name, qualities admired, lessons to apply, timestamp
- **Consumers**: Mentorship identification, example library, aspiration tracker
