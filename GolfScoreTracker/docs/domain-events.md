# Domain Events - Golf Score Tracker

## Overview
This document defines the domain events tracked in the Golf Score Tracker application. These events capture significant business occurrences related to round logging, score tracking, handicap calculation, and performance analysis.

## Events

### RoundEvents

#### RoundStarted
- **Description**: A new golf round has begun
- **Triggered When**: User starts playing a round and begins scoring
- **Key Data**: Round ID, course ID, tee box, playing partners, start time, weather conditions, user ID, timestamp
- **Consumers**: Active round tracker, GPS scorer, statistics calculator

#### RoundCompleted
- **Description**: Golf round has finished
- **Triggered When**: User completes 9 or 18 holes and finalizes scorecard
- **Key Data**: Round ID, total score, score to par, completion time, course played, user ID
- **Consumers**: Score history, handicap calculator, statistics aggregator, performance analyzer

#### RoundAbandoned
- **Description**: Round started but not finished
- **Triggered When**: User quits round before completing all holes
- **Key Data**: Round ID, holes completed, abandonment reason, abandonment time, user ID
- **Consumers**: Round tracker cleanup, partial statistics recorder

#### PersonalBestRound
- **Description**: Lowest score ever achieved
- **Triggered When**: Round score beats all previous rounds
- **Key Data**: Round ID, new best score, previous best, improvement amount, achievement date, user ID
- **Consumers**: Achievement service, celebration trigger, milestone tracker, social sharing

### HoleEvents

#### HoleScoreRecorded
- **Description**: Score for individual hole logged
- **Triggered When**: User completes hole and enters strokes
- **Key Data**: Hole ID, round ID, hole number, strokes, par, score to par (birdie/par/bogey), user ID
- **Consumers**: Round score aggregator, hole statistics, performance analyzer

#### BirdieMade
- **Description**: Hole completed one stroke under par
- **Triggered When**: Hole score is par minus one
- **Key Data**: Hole ID, round ID, hole number, par, birdies this round, user ID, timestamp
- **Consumers**: Achievement tracker, birdie counter, performance highlighter

#### EagleMade
- **Description**: Hole completed two strokes under par
- **Triggered When**: Hole score is par minus two
- **Key Data**: Hole ID, round ID, hole number, par, eagle type (eagle/albatross), user ID, timestamp
- **Consumers**: Achievement service, rare feat tracker, celebration trigger

#### ParMade
- **Description**: Hole completed at par
- **Triggered When**: Hole score equals par
- **Key Data**: Hole ID, round ID, hole number, par count this round, user ID
- **Consumers**: Consistency tracker, GIR correlator, scoring pattern analyzer

#### DoubleBogeyOrWorse
- **Description**: Hole completed two or more over par
- **Triggered When**: Hole score is par plus two or more
- **Key Data**: Hole ID, round ID, hole number, strokes over par, user ID
- **Consumers**: Trouble spot identifier, improvement area highlighter, handicap adjuster

### HandicapEvents

#### HandicapCalculated
- **Description**: Golf handicap index computed
- **Triggered When**: Sufficient recent rounds for handicap calculation
- **Key Data**: Handicap index, calculation date, rounds used, trending direction, user ID
- **Consumers**: Handicap tracker, competition readiness, performance evaluator

#### HandicapImproved
- **Description**: Handicap index decreased (better)
- **Triggered When**: New handicap lower than previous
- **Key Data**: Previous handicap, new handicap, improvement amount, calculation date, user ID
- **Consumers**: Achievement service, progress tracker, motivation system

#### HandicapIncreased
- **Description**: Handicap index increased (worse)
- **Triggered When**: New handicap higher than previous
- **Key Data**: Previous handicap, new handicap, increase amount, possible causes, user ID
- **Consumers**: Performance alert, practice recommender, trend analyzer

#### SingleDigitHandicapAchieved
- **Description**: Handicap dropped below 10
- **Triggered When**: Handicap index calculated as 9.9 or lower
- **Key Data**: Handicap index, achievement date, rounds to achievement, user ID
- **Consumers**: Achievement service, elite golfer milestone, social sharing

### CourseEvents

#### CourseAdded
- **Description**: Golf course added to database
- **Triggered When**: User plays or adds new course
- **Key Data**: Course ID, course name, location, par, slope rating, course rating, tee boxes, user ID, timestamp
- **Consumers**: Course directory, slope/rating calculator, course selector

#### CourseRatingRecorded
- **Description**: User's rating/review of course logged
- **Triggered When**: User rates course difficulty and enjoyment
- **Key Data**: Course ID, difficulty rating, enjoyment rating, review text, rating date, user ID
- **Consumers**: Course recommender, personal course ranking, favorite course identifier

#### FavoriteCourseSet
- **Description**: Course marked as favorite
- **Triggered When**: User designates preferred course
- **Key Data**: Course ID, favorite date, times played, user ID
- **Consumers**: Quick course selector, home course identifier, statistics segmentation

### StatisticsEvents

#### FairwaysHitTracked
- **Description**: Driving accuracy for par 4s and 5s recorded
- **Triggered When**: User tracks fairways hit during round
- **Key Data**: Round ID, fairways hit, fairways attempted, accuracy percentage, user ID
- **Consumers**: Driving stats, accuracy trend analyzer, improvement area identifier

#### GreensInRegulationTracked
- **Description**: GIR statistics recorded
- **Triggered When**: User tracks greens reached in regulation
- **Key Data**: Round ID, GIRs made, GIRs attempted, GIR percentage, user ID
- **Consumers**: Ball striking stats, scoring opportunity analyzer, iron play evaluator

#### PuttsPerRoundRecorded
- **Description**: Total putts for round logged
- **Triggered When**: User completes round with putt tracking
- **Key Data**: Round ID, total putts, putts per hole average, putts per GIR, user ID
- **Consumers**: Putting stats, short game analyzer, stroke saver identifier

#### PenaltyStrokesRecorded
- **Description**: Penalty strokes incurred during round
- **Triggered When**: User logs OB, water, unplayable, etc.
- **Key Data**: Round ID, total penalties, penalty types, holes with penalties, user ID
- **Consumers**: Course management analyzer, trouble area identifier, risk assessment

### PerformanceEvents

#### ScoringAverageCalculated
- **Description**: Average score over recent rounds computed
- **Triggered When**: Multiple rounds used to calculate scoring average
- **Key Data**: Rounds included, average score, scoring trend, calculation date, user ID
- **Consumers**: Performance dashboard, handicap validator, improvement tracker

#### ConsistencyScoreGenerated
- **Description**: Scoring consistency metric calculated
- **Triggered When**: Round variance analyzed
- **Key Data**: Analysis period, score standard deviation, consistency rating, user ID
- **Consumers**: Performance evaluator, strength/weakness identifier, training focus suggester

#### StrengthAreaIdentified
- **Description**: Aspect of game performing well
- **Triggered When**: Statistical analysis identifies strong skill
- **Key Data**: Strength type (driving/iron play/short game/putting), performance metric, user ID
- **Consumers**: Confidence booster, game plan optimizer, competitive advantage highlighter

#### WeaknessAreaIdentified
- **Description**: Aspect of game needing improvement
- **Triggered When**: Statistical analysis identifies weak skill
- **Key Data**: Weakness type, performance metric, improvement potential, user ID
- **Consumers**: Practice recommender, improvement focus, lesson booking suggester

### GoalEvents

#### ScoringGoalSet
- **Description**: Target score or handicap established
- **Triggered When**: User sets performance goal
- **Key Data**: Goal ID, goal type (break 90/80/70, reach handicap), target value, deadline, user ID, timestamp
- **Consumers**: Goal tracker, motivation system, progress monitor

#### ScoringGoalAchieved
- **Description**: Target score reached
- **Triggered When**: Round score meets goal criteria
- **Key Data**: Goal ID, round ID, goal score, actual score, achievement date, user ID
- **Consumers**: Achievement service, celebration trigger, goal history

#### BreakingScoreAchieved
- **Description**: Scored below milestone threshold for first time
- **Triggered When**: User breaks 100, 90, 80, or 70
- **Key Data**: Milestone score, actual score, achievement date, rounds to achievement, user ID
- **Consumers**: Achievement service, milestone tracker, social sharing, motivation boost

### EquipmentEvents

#### ClubUsageTracked
- **Description**: Club selection and performance logged
- **Triggered When**: User tracks which club used on each shot
- **Key Data**: Shot ID, hole ID, club type, distance, accuracy, user ID
- **Consumers**: Club performance analyzer, gapping identifier, equipment recommender

#### EquipmentChanged
- **Description**: Golf equipment updated
- **Triggered When**: User changes clubs, ball, or equipment
- **Key Data**: Equipment ID, equipment type, change date, previous equipment, user ID
- **Consumers**: Equipment tracker, performance comparison pre/post change

### CompetitionEvents

#### TournamentRoundEntered
- **Description**: Round played in tournament or competition
- **Triggered When**: User designates round as competitive
- **Key Data**: Round ID, tournament name, competition type, finishing position, user ID
- **Consumers**: Competition history, performance under pressure analyzer, tournament tracker

#### BestBallRoundPlayed
- **Description**: Team/scramble format round completed
- **Triggered When**: User plays non-stroke play format
- **Key Data**: Round ID, format type, team score, contribution, user ID
- **Consumers**: Format-specific stats, team performance, social golf tracker

### StreakEvents

#### ParOrBetterStreak
- **Description**: Consecutive holes at par or better
- **Triggered When**: User strings together par+ holes
- **Key Data**: Streak length, holes included, round ID, achievement date, user ID
- **Consumers**: Achievement service, momentum tracker, hot streak identifier

#### RoundsPlayedStreak
- **Description**: Consecutive days/weeks playing golf
- **Triggered When**: User maintains regular play schedule
- **Key Data**: Streak length, streak type (daily/weekly), start date, user ID
- **Consumers**: Consistency tracker, dedication badge, habit reinforcer

### WeatherEvents

#### WeatherConditionsLogged
- **Description**: Playing conditions recorded for round
- **Triggered When**: User logs weather during round
- **Key Data**: Round ID, temperature, wind speed, precipitation, conditions rating, user ID
- **Consumers**: Conditions-adjusted scoring, toughness factor, weather correlation analyzer

#### AdverseConditionsRound
- **Description**: Round played in difficult weather
- **Triggered When**: Weather conditions significantly challenging
- **Key Data**: Round ID, score, conditions, score adjustment, user ID
- **Consumers**: Mental toughness tracker, adjusted scoring, conditions handicap

### AnalysisEvents

#### TrendAnalysisCompleted
- **Description**: Performance trend over time calculated
- **Triggered When**: Multiple rounds analyzed for patterns
- **Key Data**: Analysis period, trend direction, improvement rate, statistical significance, user ID
- **Consumers**: Trend visualizer, progress report, prediction engine

#### PersonalizedInsightGenerated
- **Description**: AI-generated improvement recommendation created
- **Triggered When**: Analysis identifies actionable insight
- **Key Data**: Insight type, recommendation, expected impact, priority, user ID
- **Consumers**: Insight notification, practice suggestion, lesson focus recommender
