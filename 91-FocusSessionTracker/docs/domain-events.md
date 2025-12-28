# Domain Events - Focus Session Tracker

## Overview
This application helps users maintain deep focus through Pomodoro-style sessions, track distractions, and improve concentration over time. Domain events capture focus sessions, interruption tracking, productivity patterns, and the development of sustained attention skills.

## Events

### SessionEvents

#### FocusSessionStarted
- **Description**: A focused work session has begun
- **Triggered When**: User initiates a focus timer
- **Key Data**: Session ID, user ID, task/project, planned duration, session type (pomodoro/custom), start time, timestamp
- **Consumers**: Timer service, session tracker, distraction monitor, analytics engine

#### SessionCompleted
- **Description**: A focus session has been successfully finished
- **Triggered When**: Timer expires and user confirms completion
- **Key Data**: Session ID, actual duration, completion quality rating, task progress, focus score, timestamp
- **Consumers**: Achievement tracker, productivity analytics, session history, streak calculator

#### SessionAbandoned
- **Description**: A focus session was terminated early
- **Triggered When**: User stops session before completion
- **Key Data**: Session ID, planned duration, actual duration, abandonment reason, progress made, timestamp
- **Consumers**: Abandonment analyzer, session difficulty assessor, support system

#### SessionPaused
- **Description**: A focus session has been temporarily paused
- **Triggered When**: User needs to briefly stop the session
- **Key Data**: Session ID, pause time, pause reason, pause duration, timestamp
- **Consumers**: Session continuity tracker, pause pattern analyzer, flexibility metrics

#### SessionResumed
- **Description**: A paused focus session has been restarted
- **Triggered When**: User continues after a pause
- **Key Data**: Session ID, resume time, pause duration, continuation timestamp
- **Consumers**: Session flow tracker, interruption impact analyzer

### DistractionEvents

#### DistractionLogged
- **Description**: An interruption or distraction has been recorded
- **Triggered When**: User notes something that broke their focus
- **Key Data**: Distraction ID, session ID, distraction type, source, duration, impact level, timestamp
- **Consumers**: Distraction analyzer, pattern detector, mitigation suggester

#### InternalDistractionRecorded
- **Description**: User has noted a self-generated distraction
- **Triggered When**: User logs mind wandering or internal interruption
- **Key Data**: Distraction ID, session ID, distraction nature (mind wandering/urge/thought), trigger, timestamp
- **Consumers**: Self-discipline tracker, attention training, mindfulness insights

#### ExternalDistractionRecorded
- **Description**: An external interruption has been documented
- **Triggered When**: Outside force breaks user's focus
- **Key Data**: Distraction ID, session ID, source (person/notification/noise), preventability, timestamp
- **Consumers**: Environment optimizer, boundary setting suggestions, notification management

#### DistractionResisted
- **Description**: User has successfully resisted a potential distraction
- **Triggered When**: User notes and dismisses distraction without losing focus
- **Key Data**: Resistance ID, session ID, distraction type resisted, resistance strategy, timestamp
- **Consumers**: Willpower tracker, success pattern identifier, skill development

### BreakEvents

#### BreakStarted
- **Description**: A scheduled break between focus sessions has begun
- **Triggered When**: User starts rest period after session
- **Key Data**: Break ID, session ID, break type (short/long), planned duration, timestamp
- **Consumers**: Break timer, recovery tracker, energy management

#### BreakActivityLogged
- **Description**: User has recorded what they did during break
- **Triggered When**: User logs break activities
- **Key Data**: Break ID, activities performed, restorative quality, energy level after, timestamp
- **Consumers**: Break effectiveness analyzer, activity recommendation, recovery optimization

#### BreakExtended
- **Description**: Break duration has exceeded planned time
- **Triggered When**: User takes longer break than scheduled
- **Key Data**: Break ID, planned duration, actual duration, extension reason, timestamp
- **Consumers**: Break discipline tracker, schedule adjustment, procrastination detector

#### BreakSkipped
- **Description**: User has skipped a recommended break
- **Triggered When**: User continues working instead of taking break
- **Key Data**: Skip ID, session ID, skip reason, consecutive sessions count, timestamp
- **Consumers**: Burnout risk monitor, rest advocacy, sustainability tracker

### ProductivityEvents

#### HighFocusSessionAchieved
- **Description**: Exceptionally productive focus session has been completed
- **Triggered When**: Session metrics indicate superior focus quality
- **Key Data**: Session ID, focus score, productivity metrics, contributing factors, timestamp
- **Consumers**: Success pattern analyzer, best practice identifier, motivation system

#### FocusStreakStarted
- **Description**: User has begun a streak of completed focus sessions
- **Triggered When**: Consecutive successful sessions begin
- **Key Data**: Streak ID, start date, session type, timestamp
- **Consumers**: Streak tracker, gamification system, motivation engine

#### FocusStreakMilestone
- **Description**: A focus streak milestone has been reached
- **Triggered When**: Streak reaches significant length
- **Key Data**: Streak ID, milestone number, sessions completed, consistency score, timestamp
- **Consumers**: Achievement system, celebration trigger, habit reinforcement

#### DeepWorkThresholdReached
- **Description**: User has accumulated significant deep work time
- **Triggered When**: Daily or weekly deep work hours hit target
- **Key Data**: Threshold ID, time period, hours accumulated, quality score, timestamp
- **Consumers**: Goal achievement, productivity insights, time management success

### PatternEvents

#### OptimalSessionLengthIdentified
- **Description**: User's ideal focus session duration has been determined
- **Triggered When**: Analysis reveals most successful session length
- **Key Data**: Optimal duration, success rate correlation, sample size, timestamp
- **Consumers**: Session configuration, personalization engine, recommendation system

#### PeakFocusTimeDetected
- **Description**: User's most focused time of day has been identified
- **Triggered When**: Pattern analysis reveals productivity peak
- **Key Data**: Peak time window, focus quality metrics, consistency, timestamp
- **Consumers**: Schedule optimizer, session timing recommendations, energy management

#### DistractionPatternIdentified
- **Description**: Recurring distraction pattern has been detected
- **Triggered When**: Analysis reveals consistent interruption source
- **Key Data**: Pattern ID, distraction type, frequency, timing, impact, timestamp
- **Consumers**: Mitigation suggester, environment optimization, awareness alerts

#### ProductivityTrendDetected
- **Description**: Upward or downward trend in focus quality has been identified
- **Triggered When**: Session quality shows consistent direction over time
- **Key Data**: Trend ID, direction, duration, magnitude, contributing factors, timestamp
- **Consumers**: Intervention trigger, celebration system, course correction

### GoalEvents

#### DailyFocusGoalSet
- **Description**: User has established a daily focus target
- **Triggered When**: User commits to amount of focused work per day
- **Key Data**: Goal ID, target sessions or hours, goal period, timestamp
- **Consumers**: Goal tracker, progress monitor, daily planning

#### WeeklyFocusTargetSet
- **Description**: User has set a weekly deep work objective
- **Triggered When**: User defines weekly productivity goal
- **Key Data**: Goal ID, target metrics, baseline, deadline, timestamp
- **Consumers**: Weekly planning, progress tracking, achievement system

#### GoalAchieved
- **Description**: A focus goal has been successfully met
- **Triggered When**: User hits their focus target
- **Key Data**: Goal ID, achievement date, actual vs. target, timestamp
- **Consumers**: Celebration system, success tracker, goal escalation

### EnvironmentEvents

#### FocusEnvironmentConfigured
- **Description**: User has set up their focus environment preferences
- **Triggered When**: User configures focus-supporting settings
- **Key Data**: Config ID, do-not-disturb settings, app blockers, environment notes, timestamp
- **Consumers**: Environment optimizer, distraction prevention, setup reminder

#### FocusModeActivated
- **Description**: Focus-supporting mode has been enabled
- **Triggered When**: User activates focus environment
- **Key Data**: Activation ID, mode type, restrictions enabled, timestamp
- **Consumers**: App blocking, notification silencing, environment enforcement

#### BackgroundNoisePreferenceSet
- **Description**: User has configured audio environment preference
- **Triggered When**: User selects focus music/sounds/silence
- **Key Data**: Preference ID, audio type, volume, effectiveness rating, timestamp
- **Consumers**: Audio player integration, preference learning, environment optimization

### TaskEvents

#### TaskAssignedToSession
- **Description**: A specific task has been designated for a focus session
- **Triggered When**: User defines what they'll work on
- **Key Data**: Session ID, task ID, task description, estimated effort, timestamp
- **Consumers**: Task tracking, session planning, completion correlation

#### TaskProgressUpdated
- **Description**: Progress on task during session has been logged
- **Triggered When**: Session ends and task status updated
- **Key Data**: Task ID, session ID, progress made, completion percentage, timestamp
- **Consumers**: Task management, productivity measurement, estimation improvement

#### MultipleSessionTaskCompleted
- **Description**: A task requiring multiple focus sessions has been finished
- **Triggered When**: Multi-session task reaches completion
- **Key Data**: Task ID, total sessions used, total focus time, quality rating, timestamp
- **Consumers**: Task analytics, effort tracking, planning insights

### AnalyticsEvents

#### WeeklyReportGenerated
- **Description**: Summary of week's focus sessions has been created
- **Triggered When**: Week ends or user requests report
- **Key Data**: Report ID, sessions completed, total focus time, key insights, improvement areas, timestamp
- **Consumers**: User dashboard, trend analysis, goal setting

#### MonthlyInsightCreated
- **Description**: Monthly focus and productivity analysis has been generated
- **Triggered When**: Month ends and comprehensive analysis runs
- **Key Data**: Insight ID, month covered, productivity trends, achievements, recommendations, timestamp
- **Consumers**: Long-term planning, pattern awareness, strategic improvement
