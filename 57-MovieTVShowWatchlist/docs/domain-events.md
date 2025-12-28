# Domain Events - Movie & TV Show Watchlist

## Overview
This application tracks domain events related to movie and TV show watching activities, watchlists, ratings, and viewing history. These events support entertainment tracking, discovery, and personal viewing preference management.

## Events

### WatchlistEvents

#### MovieAddedToWatchlist
- **Description**: A movie has been added to the user's watchlist
- **Triggered When**: User adds a movie they want to watch
- **Key Data**: Movie ID, title, release year, genre(s), director, runtime, added date, priority level, source of recommendation, availability
- **Consumers**: Watchlist manager, availability notifier, recommendation engine, streaming service integration, reminder scheduler

#### TVShowAddedToWatchlist
- **Description**: A TV show has been added to the user's watchlist
- **Triggered When**: User adds a TV series they want to watch
- **Key Data**: Show ID, title, premiere year, genre(s), number of seasons, status (ongoing/ended), added date, priority, streaming platform
- **Consumers**: Watchlist manager, new episode notifier, streaming integration, recommendation engine, binge planner

#### WatchlistItemRemoved
- **Description**: An item has been removed from the watchlist
- **Triggered When**: User removes a movie or show from their watchlist
- **Key Data**: Item ID, item type, removal date, removal reason (watched/lost interest/unavailable), time on watchlist, alternative added
- **Consumers**: Watchlist analytics, interest tracker, recommendation refinement, cleanup service

#### WatchlistPrioritized
- **Description**: Watchlist items have been reorganized by priority
- **Triggered When**: User reorders or reprioritizes their watchlist
- **Key Data**: Reorder timestamp, item rankings, priority changes, sorting criteria, mood-based categories, watch order preferences
- **Consumers**: Watch-next recommender, viewing planner, priority queue, mood-based suggestion engine

### ViewingActivityEvents

#### MovieWatched
- **Description**: User has watched a movie
- **Triggered When**: User marks a movie as watched or completes viewing
- **Key Data**: Movie ID, watch date, viewing location, viewing platform, watched with whom, viewing context (theater/home/streaming), rewatch flag
- **Consumers**: Viewing history, statistics calculator, recommendation engine, spending tracker, social features

#### TVEpisodeWatched
- **Description**: User has watched a TV show episode
- **Triggered When**: User completes watching an episode
- **Key Data**: Episode ID, show ID, season number, episode number, watch date, platform, binge session ID, viewing duration
- **Consumers**: Progress tracker, next episode queue, binge analytics, show completion calculator, recommendation engine

#### TVSeasonCompleted
- **Description**: User has finished watching an entire TV season
- **Triggered When**: Last episode of a season is marked as watched
- **Key Data**: Season ID, show ID, season number, completion date, binge duration, episodes watched, season rating, next season intent
- **Consumers**: Achievement tracker, show progress manager, recommendation engine, viewing statistics, continuation reminder

#### TVShowCompleted
- **Description**: User has finished watching an entire TV series
- **Triggered When**: Final episode of the series is watched
- **Key Data**: Show ID, completion date, total episodes, total viewing time, overall rating, rewatch interest, series finale reaction
- **Consumers**: Completion tracker, statistics dashboard, recommendation engine, achievement system, series archive

#### ViewingAbandoned
- **Description**: User has stopped watching without completing
- **Triggered When**: User marks a movie or show as abandoned or DNF
- **Key Data**: Item ID, item type, abandon date, progress when abandoned, abandon reason, quality rating, would retry flag
- **Consumers**: Viewing pattern analyzer, recommendation refinement, interest tracker, DNF statistics

### RatingAndReviewEvents

#### MovieRated
- **Description**: User has assigned a rating to a movie
- **Triggered When**: User provides a star rating or score
- **Key Data**: Rating ID, movie ID, rating value, rating scale, rating date, viewing date, rewatch rating flag, mood during rating
- **Consumers**: Personal ratings database, recommendation engine, statistics calculator, best movies list, taste profile builder

#### TVShowRated
- **Description**: User has assigned a rating to a TV show
- **Triggered When**: User provides an overall show rating
- **Key Data**: Rating ID, show ID, rating value, rating date, seasons watched, completion status, rating evolution over seasons
- **Consumers**: Show ratings tracker, recommendation engine, viewing history, taste analyzer, comparison tools

#### ReviewWritten
- **Description**: User has written a detailed review
- **Triggered When**: User creates written feedback about a movie or show
- **Key Data**: Review ID, content ID, review text, spoiler flag, review date, themes discussed, recommendation flag, target audience
- **Consumers**: Review library, sharing platform, personal archive, social features, search indexer

#### FavoriteMarked
- **Description**: User has marked a movie or show as a favorite
- **Triggered When**: User designates content as personally significant or beloved
- **Key Data**: Favorite ID, content ID, content type, added to favorites date, favorite category, rewatch count, emotional significance
- **Consumers**: Favorites collection, recommendation engine, rewatch suggestions, taste profile, sharing features

### DiscoveryAndRecommendationEvents

#### RecommendationReceived
- **Description**: User has received a movie or show recommendation
- **Triggered When**: System, friend, or critic suggests content
- **Key Data**: Recommendation ID, content ID, recommender, recommendation source, reason, reception date, interest level, added to watchlist
- **Consumers**: Recommendation tracker, watchlist integration, social features, discovery engine, influence analyzer

#### RecommendationGiven
- **Description**: User has recommended content to someone
- **Triggered When**: User shares a movie or show suggestion
- **Key Data**: Recommendation ID, content ID, recipient, recommendation reason, sharing method, share date, recipient feedback
- **Consumers**: Social features, influence tracker, viewing community, relationship builder, recommendation history

#### SimilarContentDiscovered
- **Description**: System has identified similar movies or shows based on viewing history
- **Triggered When**: Recommendation algorithm finds relevant content
- **Key Data**: Discovery ID, source content ID, similar content IDs, similarity score, match reasons, discovery date, algorithm version
- **Consumers**: Discovery queue, recommendation feed, watchlist suggestions, personalization engine

#### GenrePreferenceIdentified
- **Description**: System has detected a genre preference pattern
- **Triggered When**: Analysis reveals strong viewing patterns in specific genres
- **Key Data**: Preference ID, genre, preference strength, evidence (ratings/viewing frequency), detection date, trend direction
- **Consumers**: Recommendation engine, content filter, discovery tool, taste profile, personalization system

### TrackingAndAnalyticsEvents

#### ViewingStreakUpdated
- **Description**: User's consecutive viewing days streak has changed
- **Triggered When**: User watches content on consecutive days or breaks streak
- **Key Data**: Streak ID, current streak, longest streak, last watched date, streak status, content types in streak
- **Consumers**: Habit tracker, achievement system, reminder service, engagement analytics, gamification

#### ViewingMilestoneAchieved
- **Description**: User has reached a significant viewing milestone
- **Triggered When**: User completes notable achievement (100 movies, 50 series, 1000 episodes, etc.)
- **Key Data**: Milestone ID, milestone type, achievement date, metric achieved, content breakdown, historical context, celebration tier
- **Consumers**: Achievement system, notification service, statistics dashboard, social sharing, badges

#### ViewingTimeCalculated
- **Description**: Total viewing time for a period has been calculated
- **Triggered When**: System aggregates viewing duration for analysis
- **Key Data**: Calculation ID, time period, total hours, content breakdown, platform distribution, genre distribution, comparison to previous period
- **Consumers**: Statistics dashboard, viewing habits analyzer, time management insights, year-in-review generator

#### YearInReviewGenerated
- **Description**: Annual viewing summary has been created
- **Triggered When**: System compiles yearly viewing statistics and highlights
- **Key Data**: Report ID, year, total movies/shows watched, total hours, favorite genres, top-rated content, viewing trends, memorable moments
- **Consumers**: User dashboard, sharing features, personal archive, trend analyzer, retrospective tool

### StreamingAndAvailabilityEvents

#### ContentAvailabilityChanged
- **Description**: A watchlisted item's streaming availability has changed
- **Triggered When**: System detects content added to or removed from streaming platforms
- **Key Data**: Content ID, platform, availability status, change date, subscription requirement, availability window, regional restrictions
- **Consumers**: Notification service, watchlist prioritizer, platform recommendations, availability tracker

#### StreamingSubscriptionUpdated
- **Description**: User's streaming service subscriptions have changed
- **Triggered When**: User adds or removes streaming platform access
- **Key Data**: Subscription ID, platform name, subscription status, start/end date, subscription tier, monthly cost, content access count
- **Consumers**: Availability calculator, budget tracker, content filter, platform analytics, ROI evaluator

#### WatchPartyScheduled
- **Description**: A group viewing session has been scheduled
- **Triggered When**: User plans to watch content with others
- **Key Data**: Party ID, content ID, scheduled date/time, participants, platform, host, viewing context, discussion plan
- **Consumers**: Calendar integration, participant notification, reminder service, social features, viewing history

#### NewEpisodeNotified
- **Description**: User has been notified of new episode release for followed show
- **Triggered When**: New episode becomes available for a show in user's watchlist or currently watching
- **Key Data**: Notification ID, show ID, season/episode number, release date, platform, notification delivery time, user response
- **Consumers**: Notification service, viewing queue, binge planner, viewing schedule, engagement tracker
