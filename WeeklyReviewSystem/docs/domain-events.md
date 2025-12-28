# Domain Events - Weekly Review System

## Overview
This application guides users through structured weekly reviews to reflect on accomplishments, learn from challenges, and plan ahead. Domain events capture review completion, reflection insights, priority setting, and the continuous improvement cycle.

## Events

### ReviewSessionEvents

#### WeeklyReviewStarted
- **Description**: A weekly review session has been initiated
- **Triggered When**: User begins their weekly reflection process
- **Key Data**: Review ID, user ID, week ending date, start time, review template used, timestamp
- **Consumers**: Review session manager, analytics tracker, completion monitor

#### ReviewSectionCompleted
- **Description**: A section of the review has been finished
- **Triggered When**: User completes a review component (accomplishments, challenges, etc.)
- **Key Data**: Section ID, review ID, section type, completion time, depth score, timestamp
- **Consumers**: Progress tracker, review flow manager, thoroughness monitor

#### WeeklyReviewCompleted
- **Description**: A complete weekly review has been finished
- **Triggered When**: User completes all review sections
- **Key Data**: Review ID, completion date, total duration, completeness score, key insights count, timestamp
- **Consumers**: Review history, streak tracker, analytics engine, next week planner

#### ReviewSkipped
- **Description**: A week has passed without completing a review
- **Triggered When**: Week ends without review completion
- **Key Data**: Week skipped, skip reason (if provided), streak impact, timestamp
- **Consumers**: Re-engagement system, habit tracker, accountability alerts

### AccomplishmentEvents

#### AccomplishmentRecorded
- **Description**: A weekly accomplishment has been documented
- **Triggered When**: User logs something they achieved
- **Key Data**: Accomplishment ID, review ID, description, category, significance level, related goals, timestamp
- **Consumers**: Win tracking, success pattern analyzer, motivation system

#### WinCelebrated
- **Description**: A significant achievement has been highlighted for celebration
- **Triggered When**: User marks accomplishment as particularly meaningful
- **Key Data**: Win ID, accomplishment ID, celebration type, impact description, gratitude notes, timestamp
- **Consumers**: Celebration system, positivity reinforcement, success archive

#### ProgressOnGoalNoted
- **Description**: Movement toward a goal has been acknowledged
- **Triggered When**: User connects accomplishment to larger objective
- **Key Data**: Progress note ID, goal ID, accomplishment ID, progress percentage, momentum assessment, timestamp
- **Consumers**: Goal tracking integration, progress visualization, motivation enhancement

### ChallengeEvents

#### ChallengeIdentified
- **Description**: A difficulty or obstacle from the week has been noted
- **Triggered When**: User documents what didn't go well
- **Key Data**: Challenge ID, review ID, description, impact level, category, timestamp
- **Consumers**: Challenge tracker, learning opportunity identifier, support system

#### LessonLearned
- **Description**: A learning has been extracted from a challenge
- **Triggered When**: User derives insight from difficulty
- **Key Data**: Lesson ID, challenge ID, lesson content, application plan, timestamp
- **Consumers**: Lesson library integration, growth tracker, wisdom accumulation

#### ImprovementActionDefined
- **Description**: Specific action to address challenge has been planned
- **Triggered When**: User creates actionable response to obstacle
- **Key Data**: Action ID, challenge ID, action description, implementation timeline, success criteria, timestamp
- **Consumers**: Action tracker, next week planning, continuous improvement

#### PatternRecognized
- **Description**: Recurring challenge pattern has been identified
- **Triggered When**: User notices repeating obstacle across weeks
- **Key Data**: Pattern ID, related challenges, frequency, root cause hypothesis, timestamp
- **Consumers**: Pattern analyzer, root cause investigation, strategic problem-solving

### ReflectionEvents

#### GratitudeExpressed
- **Description**: Things user is grateful for have been documented
- **Triggered When**: User records gratitude during review
- **Key Data**: Gratitude ID, review ID, gratitude items, category, emotional impact, timestamp
- **Consumers**: Gratitude tracker, mood analytics, perspective enhancement

#### MindsetShiftNoted
- **Description**: A change in thinking or perspective has been recorded
- **Triggered When**: User recognizes changed viewpoint
- **Key Data**: Shift ID, previous mindset, new mindset, what prompted change, timestamp
- **Consumers**: Growth documentation, mental model evolution, self-awareness tracker

#### EnergyLevelReviewed
- **Description**: Weekly energy and wellbeing has been assessed
- **Triggered When**: User evaluates their energy state
- **Key Data**: Assessment ID, review ID, energy rating, contributing factors, sustainability, timestamp
- **Consumers**: Energy management, burnout prevention, life balance monitor

#### StressFactorsAnalyzed
- **Description**: Sources of stress from the week have been examined
- **Triggered When**: User identifies and reflects on stressors
- **Key Data**: Analysis ID, stress factors, severity, controllability, mitigation strategies, timestamp
- **Consumers**: Stress management, wellbeing monitoring, life adjustment

### PlanningEvents

#### NextWeekPrioritiesSet
- **Description**: Top priorities for upcoming week have been defined
- **Triggered When**: User determines focus areas for next week
- **Key Data**: Priority set ID, review ID, priority items, importance ranking, time allocation, timestamp
- **Consumers**: Weekly planning, calendar integration, priority tracking

#### WeeklyGoalsEstablished
- **Description**: Specific goals for the upcoming week have been set
- **Triggered When**: User commits to weekly objectives
- **Key Data**: Goal set ID, goal items, success criteria, related larger goals, timestamp
- **Consumers**: Goal tracking, accountability system, week-end review preparation

#### TasksBraindumped
- **Description**: All potential tasks for next week have been captured
- **Triggered When**: User lists everything on their mind for the week
- **Key Data**: Braindump ID, review ID, task count, categories, prioritization needed, timestamp
- **Consumers**: Task management, mental clarity, planning foundation

#### CalendarReviewed
- **Description**: Upcoming week's commitments have been examined
- **Triggered When**: User reviews scheduled events and appointments
- **Key Data**: Calendar review ID, upcoming events, conflicts identified, time available, timestamp
- **Consumers**: Schedule optimization, time blocking, realistic planning

### MetricsEvents

#### WeekRated
- **Description**: Overall quality of the week has been scored
- **Triggered When**: User provides holistic week rating
- **Key Data**: Rating ID, review ID, overall score, satisfaction level, key factors, timestamp
- **Consumers**: Week quality tracker, trend analysis, life satisfaction monitoring

#### ProductivityAssessed
- **Description**: Week's productivity has been evaluated
- **Triggered When**: User rates their productive output
- **Key Data**: Assessment ID, productivity score, output quality, efficiency rating, timestamp
- **Consumers**: Productivity trends, effectiveness analysis, improvement identification

#### BalanceEvaluated
- **Description**: Work-life balance for the week has been assessed
- **Triggered When**: User reflects on time allocation across life areas
- **Key Data**: Balance ID, review ID, life area breakdown, satisfaction per area, imbalance notes, timestamp
- **Consumers**: Balance monitoring, life area adjustment, sustainability assessment

### InsightEvents

#### WeeklyInsightCaptured
- **Description**: A significant realization from the week has been documented
- **Triggered When**: User records an important insight
- **Key Data**: Insight ID, review ID, insight content, significance, application potential, timestamp
- **Consumers**: Insight library, wisdom accumulation, self-knowledge growth

#### TrendIdentified
- **Description**: A pattern across multiple weeks has been recognized
- **Triggered When**: User notices trend in reviews
- **Key Data**: Trend ID, trend description, weeks involved, trend direction, implications, timestamp
- **Consumers**: Long-term pattern analysis, strategic planning, behavioral awareness

#### BreakthroughMoment
- **Description**: A transformative insight or decision has occurred during review
- **Triggered When**: User has significant realization during reflection
- **Key Data**: Breakthrough ID, description, what changed, action implications, timestamp
- **Consumers**: Growth milestones, transformation tracker, pivotal moments archive

### HabitEvents

#### ReviewStreakMaintained
- **Description**: Consecutive weeks of review completion continues
- **Triggered When**: Another week added to review streak
- **Key Data**: Streak ID, current length, streak start date, consistency score, timestamp
- **Consumers**: Habit reinforcement, streak tracking, achievement system

#### ReviewTemplateCustomized
- **Description**: User has personalized their review structure
- **Triggered When**: User modifies review questions or sections
- **Key Data**: Template ID, customizations made, personalization reason, timestamp
- **Consumers**: Template management, personalization tracker, user preference learning

#### OptimalReviewTimeIdentified
- **Description**: Best time for conducting weekly review has been determined
- **Triggered When**: Pattern analysis reveals most effective review timing
- **Key Data**: Optimal time window, completion correlation, quality metrics, timestamp
- **Consumers**: Review scheduling, reminder optimization, habit support

### IntegrationEvents

#### GoalsAlignmentChecked
- **Description**: Weekly activities have been evaluated against long-term goals
- **Triggered When**: User assesses goal alignment during review
- **Key Data**: Alignment check ID, goals reviewed, alignment score, course corrections, timestamp
- **Consumers**: Goal management integration, strategic alignment, priority validation

#### ProjectStatusReviewed
- **Description**: Status of ongoing projects has been assessed
- **Triggered When**: User evaluates project progress
- **Key Data**: Project review ID, projects assessed, status updates, blockers identified, timestamp
- **Consumers**: Project management integration, portfolio health, resource allocation

#### ActionItemsArchived
- **Description**: Previous week's action items have been processed
- **Triggered When**: User reviews completion of last week's commitments
- **Key Data**: Archive ID, items completed, items rolled over, completion rate, timestamp
- **Consumers**: Accountability tracking, commitment follow-through, planning accuracy
