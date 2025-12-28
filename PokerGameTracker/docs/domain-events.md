# Domain Events - Poker Game Tracker

## Overview
This application tracks domain events related to poker game sessions, bankroll management, hand history, performance analytics, and skill development. These events support profit optimization, game improvement, and responsible gambling tracking.

## Events

### SessionEvents

#### PokerSessionStarted
- **Description**: A poker playing session has begun
- **Triggered When**: User sits down to play poker
- **Key Data**: Session ID, start time, game type (cash/tournament), stakes/buy-in, location (home/casino/online), platform, starting bankroll, table dynamics
- **Consumers**: Session tracker, time monitor, location analytics, game selection analyzer, bankroll allocator

#### SessionCompleted
- **Description**: Poker session has concluded
- **Triggered When**: User finishes playing
- **Key Data**: Session ID, end time, session duration, profit/loss, final chip count, hands played, notable hands, session notes, fatigue level
- **Consumers**: Bankroll manager, profit/loss tracker, session analytics, time-profit correlation, performance evaluator

#### SessionBankrollRecorded
- **Description**: Starting and ending bankroll for session has been logged
- **Triggered When**: User documents chips bought in and cashed out
- **Key Data**: Bankroll record ID, session ID, buy-in amount, rebuy count, cash-out amount, net profit/loss, session ROI
- **Consumers**: Bankroll tracker, profit calculator, session performance, variance analyzer, financial records

#### TableChanged
- **Description**: Player has moved to different table or game
- **Triggered When**: User switches tables during session
- **Key Data**: Table change ID, session ID, from table, to table, reason for change, time of change, table conditions comparison
- **Consumers**: Table selection tracker, game optimization, mobility patterns, profit by table analyzer

### BankrollManagementEvents

#### BankrollDeposit
- **Description**: Funds have been added to poker bankroll
- **Triggered When**: User adds money to playing funds
- **Key Data**: Deposit ID, amount, deposit date, source of funds, bankroll total after deposit, deposit reason, replenishment flag
- **Consumers**: Bankroll manager, fund tracking, deposit frequency monitor, bankroll sustainability, financial discipline

#### BankrollWithdrawal
- **Description**: Funds have been removed from poker bankroll
- **Triggered When**: User takes money out of playing funds
- **Key Data**: Withdrawal ID, amount, withdrawal date, withdrawal purpose, remaining bankroll, profit withdrawal flag, bankroll management discipline
- **Consumers**: Bankroll manager, profit realization, withdrawal patterns, bankroll stability, financial responsibility

#### BankrollMilestoneReached
- **Description**: Significant bankroll level has been achieved
- **Triggered When**: Bankroll reaches notable threshold (doubling, 10k, etc.)
- **Key Data**: Milestone ID, milestone type, achievement date, bankroll amount, time to achieve, growth rate, celebration level
- **Consumers**: Achievement tracker, progress monitor, goal validator, confidence booster, stake-up consideration

#### BankrollLowThresholdReached
- **Description**: Bankroll has fallen to concerning level
- **Triggered When**: Bankroll drops below warning threshold
- **Key Data**: Alert ID, current bankroll, threshold level, alert date, downswing duration, stake-down recommendation, risk level
- **Consumers**: Risk management, stake adjustment advisor, break recommendation, financial protection, self-awareness trigger

### HandHistoryEvents

#### NotableHandPlayed
- **Description**: Significant poker hand has been recorded
- **Triggered When**: User logs memorable or educational hand
- **Key Data**: Hand ID, session ID, hand description, cards held, community cards, actions taken, pot size, outcome, learning points
- **Consumers**: Hand history database, learning library, strategy review, mistake analyzer, success pattern identifier

#### BigPotWon
- **Description**: Large pot has been won
- **Triggered When**: User wins pot above significance threshold
- **Key Data**: Pot win ID, session ID, pot size, winning hand, opponents, hand action, emotional impact, session momentum shift
- **Consumers**: Big win tracker, confidence monitor, variance tracker, memorable moments, session impact analyzer

#### BadBeatSuffered
- **Description**: Statistically unlikely loss has occurred
- **Triggered When**: Strong hand loses to unlikely outdraw
- **Key Data**: Bad beat ID, session ID, hand held, opponent hand, pot size, bad beat severity, emotional impact, recovery response
- **Consumers**: Variance tracker, emotional resilience monitor, tilt risk assessor, bad beat jackpot qualifier, perspective keeper

#### MistakeIdentified
- **Description**: Playing error has been recognized and logged
- **Triggered When**: User identifies strategic or tactical mistake
- **Key Data**: Mistake ID, session ID, hand context, error type, correct play, cost of mistake, lesson learned, prevention strategy
- **Consumers**: Learning system, mistake tracker, strategy improvement, leak identification, skill development

### PerformanceMetricsEvents

#### WinRateCalculated
- **Description**: Performance win rate has been computed
- **Triggered When**: System calculates bb/100 or tournament ROI
- **Key Data**: Calculation ID, time period, hands/tournaments analyzed, win rate, standard deviation, confidence interval, trend direction
- **Consumers**: Performance dashboard, skill assessment, game selection, stake appropriateness, progress tracker

#### HourlyRateUpdated
- **Description**: Profit per hour metric has been refreshed
- **Triggered When**: New session data updates hourly earning rate
- **Key Data**: Rate ID, calculation date, time period, total profit, total hours, hourly rate, by game type breakdown, comparison to goals
- **Consumers**: Profitability tracker, game selection optimizer, opportunity cost evaluator, professional viability assessor

#### VarianceAnalyzed
- **Description**: Bankroll variance and swings have been evaluated
- **Triggered When**: User or system analyzes volatility
- **Key Data**: Analysis ID, time period, upswings, downswings, largest swing, standard deviation, variance level, bankroll adequacy
- **Consumers**: Risk assessment, bankroll requirement calculator, emotional preparedness, game selection, reality check

#### LeakIdentified
- **Description**: Systematic playing weakness has been detected
- **Triggered When**: Analysis reveals consistent pattern of mistakes
- **Key Data**: Leak ID, leak type (overplaying hands/tilt/position errors), evidence, frequency, cost impact, improvement plan
- **Consumers**: Strategy improvement, coaching focus, leak plugging, profit optimization, skill development priority

### TournamentEvents

#### TournamentEntered
- **Description**: User has registered for poker tournament
- **Triggered When**: Tournament entry is confirmed
- **Key Data**: Tournament ID, tournament name, buy-in, entry date, starting chips, blind structure, field size, expected duration
- **Consumers**: Tournament scheduler, bankroll allocator, ROI tracker, tournament selection, time planner

#### TournamentProgressUpdated
- **Description**: Position in ongoing tournament has changed
- **Triggered When**: Key tournament milestones reached
- **Key Data**: Progress ID, tournament ID, current chips, current position, players remaining, blinds level, money bubble distance, notable plays
- **Consumers**: Tournament tracker, in-the-money probability, strategy adjuster, deep run recognizer

#### TournamentCompleted
- **Description**: Tournament has finished for the player
- **Triggered When**: Player busts out or wins tournament
- **Key Data**: Result ID, tournament ID, finish position, prize won, profit/loss, tournament duration, key hands, performance assessment
- **Consumers**: Tournament results tracker, ROI calculator, bankroll updater, performance analyzer, results archive

#### InTheMoneyAchieved
- **Description**: Player has reached paid positions in tournament
- **Triggered When**: Tournament enters money positions
- **Key Data**: ITM ID, tournament ID, finish position, min cash amount, bubble factor, field size, ITM percentage, ladder consideration
- **Consumers**: ITM tracker, success rate monitor, min-cash vs deep-run balance, tournament skill validator

#### FinalTableReached
- **Description**: Player has advanced to tournament final table
- **Triggered When**: Down to final table participants
- **Key Data**: Final table ID, tournament ID, chip position, pay jumps, opponents, strategy notes, pressure level, prize pool distribution
- **Consumers**: Achievement tracker, final table experience, ICM considerations, deep run analyzer, high-pressure performance

### GameSelectionEvents

#### GameEvaluated
- **Description**: Poker game or table has been assessed for profitability
- **Triggered When**: User evaluates game conditions
- **Key Data**: Evaluation ID, game type, stakes, player types, table dynamics, profitability assessment, stay/leave decision, reasoning
- **Consumers**: Game selection optimizer, table change advisor, profit maximizer, seat selection, environment assessor

#### StakesMoved
- **Description**: Playing stakes have been adjusted up or down
- **Triggered When**: User changes regular stake level
- **Key Data**: Stakes change ID, old stakes, new stakes, change date, reason (bankroll/skill/comfort), success at new level
- **Consumers**: Stake progression tracker, bankroll management, skill confidence, shot-taking monitor, comfort zone expander

#### OptimalPlayingTimeIdentified
- **Description**: Most profitable playing times have been determined
- **Triggered When**: Analysis reveals time-based profit patterns
- **Key Data**: Time pattern ID, profitable day/time, game type, opponent characteristics, profit differential, schedule optimization
- **Consumers**: Schedule optimizer, game selection, opponent analysis, profit maximization, lifestyle balance

### PsychologicalEvents

#### TiltDetected
- **Description**: Emotional control has been compromised
- **Triggered When**: User recognizes tilted state
- **Key Data**: Tilt ID, session ID, tilt trigger, severity, decisions affected, financial impact, recognition timing, response action
- **Consumers**: Tilt tracker, session termination advisor, emotional awareness, loss prevention, mental game improvement

#### TiltSessionExited
- **Description**: User has quit session due to tilt
- **Triggered When**: User stops playing to prevent tilt losses
- **Key Data**: Exit ID, session ID, loss at exit, tilt severity, exit discipline, cool-down needed, loss prevention amount
- **Consumers**: Discipline tracker, loss limitation, emotional intelligence, long-term profit protection, mental game strength

#### ConfidenceUpdated
- **Description**: Player confidence level has changed
- **Triggered When**: User assesses current confidence state
- **Key Data**: Confidence ID, confidence level, date, contributing factors, performance correlation, appropriate stake level, mental state
- **Consumers**: Mental game tracker, stake selection, performance predictor, self-awareness, optimal play facilitator

#### StudySessionCompleted
- **Description**: Poker strategy study has been undertaken
- **Triggered When**: User completes training or analysis session
- **Key Data**: Study ID, study topic, duration, resources used, insights gained, application plan, study date, skill area improved
- **Consumers**: Learning tracker, skill development, strategic improvement, dedication monitor, edge maintainer

### SocialAndLocationEvents

#### HomeGameHosted
- **Description**: User has organized private poker game
- **Triggered When**: User hosts poker game at home
- **Key Data**: Game ID, host date, participants, stakes, game format, social experience, profit/loss, hosting frequency
- **Consumers**: Social poker tracker, home game organizer, friend engagement, recreational balance, hosting patterns

#### CasinoSessionLogged
- **Description**: Live casino poker session has been recorded
- **Triggered When**: User plays at brick-and-mortar casino
- **Key Data**: Session ID, casino name, location, game type, stakes, session result, casino atmosphere, casino rating, travel cost
- **Consumers**: Casino comparison, location profitability, travel cost consideration, live play preference, venue selection

#### OnlinePlatformEvaluated
- **Description**: Online poker site has been assessed
- **Triggered When**: User reviews online playing platform
- **Key Data**: Platform ID, platform name, game variety, traffic, software quality, player pool, rakeback, platform rating, profitability
- **Consumers**: Platform selection, site comparison, game availability, rakeback optimizer, playing environment

### GoalsAndAchievementsEvents

#### PokerGoalSet
- **Description**: Performance or financial goal has been established
- **Triggered When**: User defines poker objective
- **Key Data**: Goal ID, goal type (profit/win rate/volume/skill), target metric, timeframe, current baseline, motivation, tracking method
- **Consumers**: Goal tracker, progress monitor, motivation system, achievement anticipator, performance driver

#### MonthlyProfitTargetMet
- **Description**: Monthly profit goal has been achieved
- **Triggered When**: Month-end profit meets or exceeds target
- **Key Data**: Achievement ID, month, target profit, actual profit, exceeded by, consistency, celebration level, next month target
- **Consumers**: Goal achievement tracker, financial success validator, motivation booster, bankroll builder, confidence enhancer

#### SkillMilestoneAchieved
- **Description**: Poker skill development milestone has been reached
- **Triggered When**: Notable skill improvement is demonstrated
- **Key Data**: Milestone ID, skill area, achievement date, evidence, proficiency level, teaching capability, edge quantification
- **Consumers**: Skill development tracker, competence validator, edge maintainer, progression monitor, expertise recognition
