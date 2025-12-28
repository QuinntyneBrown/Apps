# Domain Events - Video Game Collection Manager

## Overview
This application tracks domain events related to video game collections, gameplay tracking, backlog management, completion tracking, and gaming achievements. These events support collection organization, playing habits analysis, and gaming goal management.

## Events

### CollectionEvents

#### GameAddedToCollection
- **Description**: A new game has been added to the collection
- **Triggered When**: User acquires a physical or digital game
- **Key Data**: Game ID, title, platform, genre, developer, publisher, acquisition date, format (physical/digital), purchase price, acquisition source
- **Consumers**: Collection database, value tracker, platform organizer, genre analyzer, wishlist updater

#### GameCategorized
- **Description**: Game has been organized into categories or tagged
- **Triggered When**: User assigns genres, tags, or custom categories
- **Key Data**: Game ID, primary genre, sub-genres, tags, series, franchise, play status, ownership format, storage location
- **Consumers**: Collection organizer, search filter, recommendation engine, backlog categorizer, library browser

#### GameRemovedFromCollection
- **Description**: Game has been removed from the collection
- **Triggered When**: User sells, trades, or removes a game
- **Key Data**: Removal ID, game ID, removal date, removal reason (sold/traded/donated), sale price, recipient, regret flag
- **Consumers**: Collection updater, value tracker, trade history, decision analyzer, reacquisition tracker

#### CollectionValueAssessed
- **Description**: Collection's total value has been calculated
- **Triggered When**: System or user computes collection worth
- **Key Data**: Valuation ID, assessment date, total games, current market value, acquisition cost, appreciation/depreciation, rare items value
- **Consumers**: Net worth tracker, insurance documentation, investment analyzer, collection statistics, selling decision support

### BacklogEvents

#### GameAddedToBacklog
- **Description**: Game has been added to the to-play list
- **Triggered When**: User marks unplayed game for future playing
- **Key Data**: Backlog ID, game ID, added date, priority level, estimated playtime, interest level, acquisition source, added reason
- **Consumers**: Backlog manager, play queue, time estimator, priority sorter, recommendation engine

#### BacklogPrioritized
- **Description**: Backlog games have been reorganized by priority
- **Triggered When**: User reorders or updates backlog priorities
- **Key Data**: Prioritization ID, reorder date, new rankings, sorting criteria (genre/release date/hype/length), next to play selection
- **Consumers**: Play queue manager, next game suggester, backlog optimizer, playing schedule planner

#### GameRemovedFromBacklog
- **Description**: Game has been taken off the backlog list
- **Triggered When**: User removes game from to-play list
- **Key Data**: Removal ID, game ID, removal date, removal reason (completed/lost interest/sold), time on backlog, played flag
- **Consumers**: Backlog tracker, interest analyzer, completion tracker, backlog statistics, decision monitor

#### BacklogGoalSet
- **Description**: Goal to reduce backlog has been established
- **Triggered When**: User sets target for games to complete
- **Key Data**: Goal ID, target games count, timeframe, start date, end date, specific games targeted, motivation reason
- **Consumers**: Goal tracker, progress monitor, motivation system, achievement tracker, backlog reducer

### GameplayEvents

#### GameplaySessionStarted
- **Description**: User has begun playing a game
- **Triggered When**: User starts a gaming session
- **Key Data**: Session ID, game ID, start time, platform, session type (story/multiplayer/casual), save file, initial playtime total
- **Consumers**: Session tracker, playtime calculator, activity logger, platform usage tracker, gaming habit analyzer

#### GameplaySessionEnded
- **Description**: Gaming session has concluded
- **Triggered When**: User stops playing
- **Key Data**: Session ID, end time, session duration, progress made, achievements earned, deaths/fails, enjoyment level, continue playing intent
- **Consumers**: Playtime aggregator, progress tracker, session analytics, habit tracker, engagement monitor

#### GameProgressUpdated
- **Description**: Progress through game has been recorded
- **Triggered When**: User updates completion percentage or story progress
- **Key Data**: Progress ID, game ID, completion percentage, story chapter/level, side quests completed, collectibles found, hours played, update date
- **Consumers**: Progress tracker, completion estimator, achievement monitor, goal tracker, statistics calculator

#### GameStarted
- **Description**: User has begun playing a game for the first time
- **Triggered When**: First gameplay session of a new game
- **Key Data**: Start ID, game ID, start date, first impressions, initial difficulty, tutorial completed, backlog removal, excitement level
- **Consumers**: Backlog updater, now playing list, first impression logger, game start tracker, interest validator

### CompletionEvents

#### GameCompleted
- **Description**: User has finished the main story or campaign
- **Triggered When**: Game's primary content is completed
- **Key Data**: Completion ID, game ID, completion date, total playtime, completion percentage, ending achieved, satisfaction rating, worth it flag
- **Consumers**: Completed games library, statistics tracker, backlog reducer, achievement system, review prompter

#### GameFullyCompleted
- **Description**: Game has been 100% completed including all extras
- **Triggered When**: All achievements, collectibles, and content finished
- **Key Data**: Perfect completion ID, game ID, completion date, total playtime, all achievements unlocked, platinum/100% earned, difficulty completed
- **Consumers**: Perfectionist tracker, achievement showcase, completion statistics, elite status, dedication metric

#### GameAbandoned
- **Description**: User has stopped playing without completing
- **Triggered When**: User marks game as abandoned or quit
- **Key Data**: Abandonment ID, game ID, abandon date, playtime before quitting, progress percentage, abandon reason, would retry flag
- **Consumers**: Abandon tracker, interest analyzer, recommendation refiner, DNF statistics, retry consideration

#### SecretOrEasterEggFound
- **Description**: Hidden content or secret has been discovered
- **Triggered When**: User finds secret area, easter egg, or hidden content
- **Key Data**: Secret ID, game ID, discovery date, secret type, difficulty to find, discovery method, shared with community flag
- **Consumers**: Discovery log, completionist tracker, secret database, community sharing, exploration metric

### AchievementEvents

#### AchievementUnlocked
- **Description**: Game achievement or trophy has been earned
- **Triggered When**: User completes achievement requirements
- **Key Data**: Achievement ID, game ID, achievement name, unlock date, rarity, difficulty, unlock method, Gamerscore/Trophy points
- **Consumers**: Achievement tracker, gamerscore calculator, trophy collection, rare achievement highlighter, progression monitor

#### MilestoneAchieved
- **Description**: Significant gaming milestone has been reached
- **Triggered When**: User hits notable achievement (100 games completed, 10000 gamerscore, etc.)
- **Key Data**: Milestone ID, milestone type, achievement date, metric achieved, historical context, celebration tier, sharing preference
- **Consumers**: Achievement system, notification service, statistics dashboard, social sharing, gamification tracker

#### PlatinumTrophyEarned
- **Description**: All trophies in a game have been unlocked (PlayStation)
- **Triggered When**: User earns platinum trophy
- **Key Data**: Platinum ID, game ID, earn date, total playtime, difficulty level, platinum rarity, effort level, trophy count
- **Consumers**: Trophy showcase, elite achievement tracker, dedication metric, collection statistics, bragging rights

#### RareAchievementUnlocked
- **Description**: Uncommonly earned achievement has been obtained
- **Triggered When**: User unlocks achievement with low completion percentage
- **Key Data**: Rare achievement ID, game ID, achievement name, rarity percentage, unlock date, difficulty, special recognition flag
- **Consumers**: Rare achievement showcase, dedication tracker, skill validator, community recognition, pride indicator

### RatingAndReviewEvents

#### GameRated
- **Description**: User has assigned a rating to a game
- **Triggered When**: User provides numerical or star rating
- **Key Data**: Rating ID, game ID, rating value, rating date, playtime at rating, completion status, rating justification, would recommend
- **Consumers**: Personal ratings database, recommendation engine, collection highlighter, best games ranker, taste profiler

#### GameReviewWritten
- **Description**: Detailed review of game has been created
- **Triggered When**: User writes comprehensive game feedback
- **Key Data**: Review ID, game ID, review text, pros/cons, story rating, gameplay rating, graphics rating, value rating, target audience
- **Consumers**: Review library, sharing platform, personal reference, community contribution, critical analysis archive

#### FavoriteGameMarked
- **Description**: Game has been designated as a personal favorite
- **Triggered When**: User marks game as beloved or top-tier
- **Key Data**: Favorite ID, game ID, favorite date, favorite category (all-time/genre/platform), replay count, emotional significance
- **Consumers**: Favorites collection, recommendation engine, replay suggester, taste profile, collection highlights

### SocialAndMultiplayerEvents

#### MultiplayerSessionJoined
- **Description**: User has participated in online/local multiplayer
- **Triggered When**: User plays multiplayer mode
- **Key Data**: MP session ID, game ID, session date, players involved, session duration, game mode, wins/losses, fun rating
- **Consumers**: Multiplayer tracker, social gaming log, win/loss record, friend activity, gaming social network

#### OnlineEventParticipated
- **Description**: User has joined special in-game event
- **Triggered When**: User participates in limited-time event or tournament
- **Key Data**: Event ID, game ID, event name, participation date, rewards earned, placement/rank, event duration, participation level
- **Consumers**: Event tracker, rewards monitor, competitive history, limited content tracker, engagement metric

#### GameRecommendationGiven
- **Description**: User has recommended game to others
- **Triggered When**: User suggests specific game to friends
- **Key Data**: Recommendation ID, game ID, recommended to, recommendation reason, recipient response, purchase by recipient flag
- **Consumers**: Influence tracker, recommendation history, social features, taste sharing, gaming network

### PlatformAndHardwareEvents

#### PlatformAddedToCollection
- **Description**: New gaming platform or console has been acquired
- **Triggered When**: User obtains new gaming system
- **Key Data**: Platform ID, platform name, acquisition date, purchase price, condition (new/used), purpose, compatible games count
- **Consumers**: Platform inventory, collection capabilities, game compatibility checker, value tracker, playing options

#### DigitalStorefrontLinked
- **Description**: Digital game library has been connected
- **Triggered When**: User links Steam, PlayStation Network, Xbox, etc.
- **Key Data**: Storefront ID, platform name, link date, games imported count, auto-sync enabled, library size, account username
- **Consumers**: Collection auto-updater, cross-platform tracker, digital library manager, achievement sync, unified collection

#### CloudSaveUtilized
- **Description**: Game progress has been saved to cloud
- **Triggered When**: User enables or uses cloud save feature
- **Key Data**: Cloud save ID, game ID, save date, platform, save slot, save file size, backup frequency, cross-platform flag
- **Consumers**: Save manager, backup system, cross-platform play enabler, progress protector, sync monitor
