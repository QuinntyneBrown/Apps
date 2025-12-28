# Domain Events - Sports Team Following Tracker

## Overview
This application tracks domain events related to sports team following, game attendance, team performance, player tracking, and fan engagement. These events support fan experience enhancement, statistics tracking, and sports fandom management.

## Events

### TeamFollowingEvents

#### TeamAdded
- **Description**: A sports team has been added to followed teams list
- **Triggered When**: User begins following a team
- **Key Data**: Team ID, team name, sport, league, division, follow date, fan level (casual/dedicated/superfan), reason for following, home stadium
- **Consumers**: Team tracker, game scheduler, news aggregator, notification preferences, fan profile builder

#### FanLoyaltyLevelSet
- **Description**: Dedication level to team has been established
- **Triggered When**: User defines their fandom intensity
- **Key Data**: Loyalty ID, team ID, loyalty level, notification preferences, game attendance intent, merchandise ownership, emotional investment
- **Consumers**: Notification customizer, engagement predictor, merchandise recommender, community matcher, experience personalizer

#### RivalryIdentified
- **Description**: Rival team or matchup has been designated
- **Triggered When**: User marks specific team as rival
- **Key Data**: Rivalry ID, primary team, rival team, rivalry intensity, historical context, notable games, trash talk permission level
- **Consumers**: Rivalry tracker, game importance highlighter, notification intensifier, matchup anticipation, engagement booster

#### TeamUnfollowed
- **Description**: User has stopped following a team
- **Triggered When**: User removes team from followed list
- **Key Data**: Unfollow ID, team ID, unfollow date, reason (performance/relocation/lost interest), following duration, might return flag
- **Consumers**: Team list updater, preference analyzer, fan journey tracker, reengagement opportunity

### GameTrackingEvents

#### GameScheduled
- **Description**: Upcoming game has been added to calendar
- **Triggered When**: Team schedule is released or game is announced
- **Key Data**: Game ID, teams playing, scheduled date/time, venue, game type (regular/playoff/exhibition), broadcast info, importance level
- **Consumers**: Game calendar, reminder service, attendance planner, broadcast finder, anticipation tracker

#### GameStarted
- **Description**: A game has begun
- **Triggered When**: Game commences
- **Key Data**: Game ID, actual start time, starting lineups, weather conditions (outdoor sports), initial odds, viewing method
- **Consumers**: Live tracker, real-time updates, notification service, viewing party coordinator, in-game engagement

#### ScoreUpdated
- **Description**: Game score has changed
- **Triggered When**: Team scores points
- **Key Data**: Update ID, game ID, scoring team, new score, scoring player/play, game time, score differential, scoring type
- **Consumers**: Live score tracker, notification service, game excitement meter, momentum analyzer, fan emotion tracker

#### GameCompleted
- **Description**: Game has concluded
- **Triggered When**: Final whistle/buzzer
- **Key Data**: Game ID, final score, winner, game duration, attendance, key players, game highlights, post-game sentiment, rivalry game flag
- **Consumers**: Results database, season tracker, statistics aggregator, win/loss record, fan mood analyzer

#### GameAttended
- **Description**: User has attended game in person
- **Triggered When**: User goes to live game
- **Key Data**: Attendance ID, game ID, attendance date, seat location, ticket cost, companions, pre-game activities, in-stadium experience rating
- **Consumers**: Attendance history, spending tracker, experience logger, stadium familiarity, loyalty points

### PlayerTrackingEvents

#### FavoritePlayerDesignated
- **Description**: Specific player has been marked as favorite
- **Triggered When**: User identifies preferred player
- **Key Data**: Player ID, team, position, favorite designation date, reason for favoriting, jersey owned flag, performance interest level
- **Consumers**: Player tracker, statistics monitor, news filter, merchandise suggester, fantasy sports integration

#### PlayerPerformanceTracked
- **Description**: Individual player statistics have been logged
- **Triggered When**: Player stats are updated after game
- **Key Data**: Performance ID, player ID, game ID, statistics (sport-specific), performance rating, milestone achievements, fantasy points
- **Consumers**: Player statistics database, fantasy sports, performance trends, milestone tracker, fan engagement

#### PlayerMilestoneAchieved
- **Description**: Player has reached career or season milestone
- **Triggered When**: Significant achievement is recorded
- **Key Data**: Milestone ID, player ID, milestone type, achievement date, statistics involved, historical significance, celebration level
- **Consumers**: Milestone tracker, notification service, historical archive, fan celebration trigger, memorabilia opportunity

#### PlayerTradedOrTransferred
- **Description**: Player has moved to different team
- **Triggered When**: Trade or transfer is completed
- **Key Data**: Transaction ID, player ID, from team, to team, transaction date, trade details, fan reaction, continue following flag
- **Consumers**: Roster updater, fan sentiment tracker, multi-team interest, jersey obsolescence, loyalty tester

### SeasonTrackingEvents

#### SeasonStarted
- **Description**: New season has begun for followed team
- **Triggered When**: First game of season or season opener
- **Key Data**: Season ID, team ID, season start date, roster, expectations, championship odds, season goals, fan optimism level
- **Consumers**: Season tracker, expectation setter, performance monitor, playoff probability, year-over-year comparison

#### StandingsUpdated
- **Description**: League standings have changed
- **Triggered When**: Games affect team position
- **Key Data**: Update ID, team ID, current position, division rank, conference rank, wins/losses, games behind leader, playoff position
- **Consumers**: Standings tracker, playoff calculator, team comparison, performance context, hope/despair meter

#### PlayoffQualified
- **Description**: Team has clinched playoff berth
- **Triggered When**: Team secures postseason position
- **Key Data**: Qualification ID, team ID, clinch date, playoff seed, regular season record, celebration level, championship odds
- **Consumers**: Achievement tracker, playoff scheduler, ticket planner, expectation adjuster, fan excitement amplifier

#### SeasonCompleted
- **Description**: Team's season has ended
- **Triggered When**: Final game or playoff elimination
- **Key Data**: Season end ID, team ID, end date, final record, final position, playoff outcome, season assessment, offseason hope
- **Consumers**: Season archive, performance analysis, historical comparison, offseason tracker, next season anticipation

#### ChampionshipWon
- **Description**: Team has won league championship
- **Triggered When**: Team wins title
- **Key Data**: Championship ID, team ID, championship date, opponent defeated, finals record, celebration magnitude, historical context, memorabilia acquired
- **Consumers**: Achievement archive, ultimate celebration, historical record, loyalty validation, memorabilia tracker

### FanEngagementEvents

#### GameWatchedOnTV
- **Description**: User has watched game on television or stream
- **Triggered When**: User tunes in to broadcast
- **Key Data**: Viewing ID, game ID, viewing date, broadcast network/platform, watching location, watching companions, viewing experience rating
- **Consumers**: Viewing habits tracker, engagement monitor, broadcast preference, watch party opportunities, cord-cutting decisions

#### SocialMediaEngagement
- **Description**: User has interacted with team content online
- **Triggered When**: User posts, comments, or reacts to team content
- **Key Data**: Engagement ID, team ID, platform, engagement type, content engaged with, sentiment, engagement date, viral potential
- **Consumers**: Fan engagement tracker, sentiment analyzer, community connector, influence measure, social presence

#### Merchandisepurchased
- **Description**: Team merchandise or memorabilia has been bought
- **Triggered When**: User acquires team-branded items
- **Key Data**: Purchase ID, team ID, item type, purchase date, cost, item description, limited edition flag, proudly displayed flag
- **Consumers**: Merchandise collection, spending tracker, loyalty indicator, jersey retirement, memorabilia valuation

#### WatchPartyHosted
- **Description**: User has organized group viewing event
- **Triggered When**: User hosts game watching gathering
- **Key Data**: Party ID, game ID, host date, attendees, food/drinks, atmosphere, outcome impact on party mood, would host again
- **Consumers**: Social engagement, fan community builder, hosting history, tradition tracker, friendship strengthener

### PredictionAndFantasyEvents

#### GamePredictionMade
- **Description**: User has predicted game outcome
- **Triggered When**: User forecasts winner and/or score
- **Key Data**: Prediction ID, game ID, predicted winner, predicted score, confidence level, prediction date, rationale
- **Consumers**: Prediction tracker, accuracy calculator, bragging rights manager, betting consideration, homer bias detector

#### PredictionResultRecorded
- **Description**: Game prediction accuracy has been determined
- **Triggered When**: Game completes and prediction is evaluated
- **Key Data**: Result ID, prediction ID, actual outcome, prediction accuracy, points earned, accuracy streak, overall record
- **Consumers**: Accuracy statistics, leaderboard, prediction credibility, learning from misses, forecasting improvement

#### FantasyTeamImpacted
- **Description**: Followed player performance has affected fantasy team
- **Triggered When**: Player stats contribute to fantasy results
- **Key Data**: Impact ID, player ID, game ID, fantasy points, performance impact, fantasy position change, emotional conflict (team vs fantasy)
- **Consumers**: Fantasy tracker, dual loyalty manager, player value assessor, fantasy strategy, team-fantasy tension

### NewsAndInformationEvents

#### TeamNewsReceived
- **Description**: News or update about followed team has been delivered
- **Triggered When**: Significant team news is published
- **Key Data**: News ID, team ID, news type (injury/trade/coaching/scandal), news date, source, importance level, sentiment, reaction
- **Consumers**: News aggregator, notification service, sentiment tracker, discussion trigger, information currency

#### InjuryReportUpdated
- **Description**: Player injury status has been reported
- **Triggered When**: Injury news is announced or status changes
- **Key Data**: Injury ID, player ID, injury type, severity, expected absence, impact on team, injury date, return timeline
- **Consumers**: Roster availability tracker, lineup predictor, performance impact estimator, concern level, fantasy implications

#### TradeRumorTracked
- **Description**: Potential player movement speculation has been noted
- **Triggered When**: Trade rumors emerge
- **Key Data**: Rumor ID, player(s) involved, potential teams, rumor date, source credibility, likelihood, fan desire level, anxiety level
- **Consumers**: Rumor mill tracker, anxiety generator, offseason entertainment, hope/dread balance, roster speculation

### StatisticsEvents

#### TeamStatisticsUpdated
- **Description**: Team performance metrics have been refreshed
- **Triggered When**: Game results update season statistics
- **Key Data**: Stats ID, team ID, update date, offensive stats, defensive stats, special teams, rankings, trends, performance areas
- **Consumers**: Performance analyzer, strength/weakness identifier, comparison tool, coaching evaluation, championship probability

#### HistoricalComparisonMade
- **Description**: Current team has been compared to past seasons
- **Triggered When**: User or system analyzes historical context
- **Key Data**: Comparison ID, current season, comparison season(s), metrics compared, historical standing, better/worse assessment, context notes
- **Consumers**: Historical perspective, expectation calibration, franchise history, glory days nostalgia, hope measurement

#### FanLoyaltyMilestoneReached
- **Description**: Fan dedication milestone has been achieved
- **Triggered When**: Years following, games attended, or engagement threshold is reached
- **Key Data**: Milestone ID, team ID, milestone type, achievement date, years followed, games attended, loyalty level, recognition deserved
- **Consumers**: Loyalty recognition, fan achievement, dedication validation, community status, team appreciation opportunity

### EmotionalJourneyEvents

#### EmotionalHighRecorded
- **Description**: Peak positive fan experience has been captured
- **Triggered When**: Exceptional victory or moment occurs
- **Key Data**: High ID, game/event ID, emotion type (euphoria/vindication/relief), intensity, context, memory significance, sharing compulsion
- **Consumers**: Emotional journey tracker, peak experience archive, loyalty reinforcement, storytelling material, fandom validation

#### EmotionalLowRecorded
- **Description**: Difficult fan moment has been documented
- **Triggered When**: Devastating loss or disappointment occurs
- **Key Data**: Low ID, game/event ID, emotion type (heartbreak/frustration/anger), intensity, context, recovery time, loyalty test level
- **Consumers**: Emotional journey tracker, resilience measure, therapy needs, loyalty depth validator, suffering documentation
