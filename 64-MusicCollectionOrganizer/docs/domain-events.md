# Domain Events - Music Collection Organizer

## Overview
This application tracks domain events related to music collection management, listening history, playlist creation, artist discovery, and music appreciation. These events support collection organization, listening habit analysis, and musical taste development.

## Events

### CollectionEvents

#### AlbumAddedToCollection
- **Description**: A new album has been added to the music collection
- **Triggered When**: User acquires album in any format
- **Key Data**: Album ID, title, artist, release year, genre, format (vinyl/CD/digital), acquisition date, purchase price, label, catalog number
- **Consumers**: Collection database, format tracker, artist library, genre organizer, value tracker

#### VinylRecordAcquired
- **Description**: Physical vinyl record has been added
- **Triggered When**: User purchases or receives vinyl
- **Key Data**: Record ID, album details, pressing variant, condition, speed (33/45/78 RPM), acquisition source, price, pressing year, special edition flag
- **Consumers**: Vinyl collection, format organizer, condition tracker, value assessor, discography manager

#### DigitalAlbumPurchased
- **Description**: Digital music has been bought
- **Triggered When**: User purchases digital album or tracks
- **Key Data**: Purchase ID, album/tracks, platform (iTunes/Bandcamp/etc), purchase date, price, format quality (MP3/FLAC/etc), DRM status
- **Consumers**: Digital library, purchase history, platform tracker, budget monitor, quality manager

#### CollectionCatalogued
- **Description**: Music collection has been organized or re-catalogued
- **Triggered When**: User categorizes or reorganizes collection
- **Key Data**: Catalog ID, organization scheme, total items, categories created, cataloguing date, metadata completeness, organization method
- **Consumers**: Collection organizer, search optimizer, metadata manager, library browser, discovery facilitator

### ListeningEvents

#### AlbumPlayed
- **Description**: User has listened to an album
- **Triggered When**: Album playback occurs
- **Key Data**: Play ID, album ID, play date/time, listening platform, listening context (focus/background), playback quality, complete/partial listen
- **Consumers**: Listening history, play count tracker, recommendation engine, habit analyzer, artist statistics

#### TrackPlayed
- **Description**: Individual song has been played
- **Triggered When**: Track playback occurs
- **Key Data**: Track ID, play timestamp, album ID, artist, duration, platform, skip status, listening context, repeat flag
- **Consumers**: Track statistics, favorite identifier, playlist suggester, skip analyzer, listening pattern tracker

#### ListeningSessionStarted
- **Description**: User has begun a music listening session
- **Triggered When**: User starts playing music
- **Key Data**: Session ID, start time, initial album/playlist, platform, listening mode (active/background), audio setup, mood/context
- **Consumers**: Session tracker, listening time calculator, context analyzer, habit monitor, engagement tracker

#### ListeningSessionEnded
- **Description**: Music listening session has concluded
- **Triggered When**: User stops listening or session times out
- **Key Data**: Session ID, end time, session duration, tracks played, albums completed, skips count, listening quality, satisfaction level
- **Consumers**: Time analytics, listening statistics, session aggregator, habit tracker, engagement analyzer

#### SongSkipped
- **Description**: Track was skipped before completion
- **Triggered When**: User skips to next track
- **Key Data**: Skip ID, track ID, skip time, time listened before skip, skip reason, skip count for track, context
- **Consumers**: Skip analyzer, track quality assessor, playlist optimizer, preference learner, engagement monitor

### DiscoveryEvents

#### NewArtistDiscovered
- **Description**: User has found and listened to a new artist
- **Triggered When**: First play of previously unknown artist
- **Key Data**: Discovery ID, artist name, discovery date, discovery source, first track/album played, initial impression, follow-up interest
- **Consumers**: Discovery log, artist library expander, recommendation engine, taste evolution tracker, exploration metric

#### GenreExplored
- **Description**: User has ventured into new musical genre
- **Triggered When**: First significant listening in unfamiliar genre
- **Key Data**: Exploration ID, genre name, exploration date, entry album/artist, initial reaction, continued exploration flag, comfort zone expansion
- **Consumers**: Genre diversity tracker, taste broadening monitor, recommendation expander, musical growth tracker

#### RecommendationReceived
- **Description**: Music recommendation has been received
- **Triggered When**: Friend, service, or algorithm suggests music
- **Key Data**: Recommendation ID, music recommended, recommender, recommendation reason, reception date, listening intent, source credibility
- **Consumers**: Recommendation tracker, queue manager, social features, discovery facilitator, influence monitor

#### AlbumWishlisted
- **Description**: Album has been added to wishlist for future acquisition
- **Triggered When**: User marks album for future purchase
- **Key Data**: Wishlist ID, album ID, added date, priority level, reason for interest, preferred format, budget allocated, availability tracking
- **Consumers**: Wishlist manager, purchase planner, budget allocator, availability notifier, collection gap identifier

### PlaylistEvents

#### PlaylistCreated
- **Description**: New playlist has been created
- **Triggered When**: User creates a music playlist
- **Key Data**: Playlist ID, name, description, creation date, playlist type (mood/genre/activity), public/private flag, initial tracks, cover art
- **Consumers**: Playlist manager, organization system, sharing platform, listening queue, mood mapper

#### TrackAddedToPlaylist
- **Description**: Song has been added to an existing playlist
- **Triggered When**: User adds track to playlist
- **Key Data**: Addition ID, playlist ID, track ID, add date, position in playlist, add reason, manual/suggested flag
- **Consumers**: Playlist manager, track organizer, listening queue, recommendation validator, curation tracker

#### PlaylistShared
- **Description**: Playlist has been shared with others
- **Triggered When**: User shares playlist publicly or with specific people
- **Key Data**: Share ID, playlist ID, shared with, share date, sharing platform, share permissions, feedback received, follower count
- **Consumers**: Social features, influence tracker, community platform, sharing analytics, relationship builder

#### PlaylistCollaborativeCreated
- **Description**: Shared editable playlist has been initiated
- **Triggered When**: User creates playlist allowing others to add tracks
- **Key Data**: Collab playlist ID, creator, collaborators, creation date, contribution rules, track count, activity level, purpose
- **Consumers**: Collaboration manager, social features, shared experience tracker, community builder

### RatingAndReviewEvents

#### AlbumRated
- **Description**: Album has been assigned a rating
- **Triggered When**: User provides numerical or star rating
- **Key Data**: Rating ID, album ID, rating value, rating date, listen count before rating, rating justification, would recommend flag
- **Consumers**: Personal ratings database, recommendation engine, best albums list, taste profiler, collection highlighter

#### AlbumReviewWritten
- **Description**: Detailed album review has been created
- **Triggered When**: User writes comprehensive album feedback
- **Key Data**: Review ID, album ID, review text, favorite tracks, production quality notes, lyrical analysis, review date, public/private flag
- **Consumers**: Review library, sharing platform, personal reference, critical analysis archive, community contribution

#### FavoriteAlbumMarked
- **Description**: Album has been designated as a personal favorite
- **Triggered When**: User marks album as beloved or essential
- **Key Data**: Favorite ID, album ID, favorite date, play count, emotional significance, favorite category (all-time/genre), first listen date
- **Consumers**: Favorites collection, recommendation engine, top albums showcase, taste core identifier, essential listening

#### TrackRated
- **Description**: Individual song has been rated
- **Triggered When**: User assigns rating to specific track
- **Key Data**: Track rating ID, track ID, rating value, rating date, play count, rating reason, standalone/album context
- **Consumers**: Track ratings database, best songs identifier, playlist curator, skip predictor, preference analyzer

### ConcertAndLiveEvents

#### ConcertAttended
- **Description**: User has attended a live music performance
- **Triggered When**: User logs concert or show attendance
- **Key Data**: Concert ID, artist(s), venue, date, ticket price, setlist highlights, concert rating, companions, memorable moments
- **Consumers**: Concert history, artist connection strengthener, music memory bank, spending tracker, live music engagement

#### ConcertScheduled
- **Description**: Upcoming concert has been planned
- **Triggered When**: User purchases tickets or plans to attend show
- **Key Data**: Event ID, artist, venue, date, ticket cost, seating section, purchase date, attendance group, excitement level
- **Consumers**: Concert calendar, budget tracker, reminder service, pre-show playlist generator, anticipation monitor

#### LiveRecordingAcquired
- **Description**: Live album or bootleg has been obtained
- **Triggered When**: User adds live recording to collection
- **Key Data**: Recording ID, artist, venue, performance date, recording quality, acquisition source, official/bootleg flag, setlist
- **Consumers**: Live music collection, concert memory supplement, artist discography, comparative listening, authenticity tracker

### ArtistEngagementEvents

#### ArtistFollowed
- **Description**: User has started following an artist
- **Triggered When**: User subscribes to artist updates
- **Key Data**: Follow ID, artist name, follow date, notification preferences, discovery source, existing collection count, engagement level intent
- **Consumers**: Artist tracker, new release notifier, tour alert system, recommendation engine, fan engagement monitor

#### NewReleaseNotified
- **Description**: Followed artist has released new music
- **Triggered When**: System detects new album/single from followed artist
- **Key Data**: Notification ID, artist, release details, release date, notification delivery time, user response, listening intent
- **Consumers**: Notification service, purchase consideration, listening queue, collection updater, fan engagement

#### ArtistDiscographyCompleted
- **Description**: User has collected all releases from an artist
- **Triggered When**: User acquires final missing album from artist
- **Key Data**: Completion ID, artist name, completion date, total albums, format completeness, rare items included, collection pride level
- **Consumers**: Achievement tracker, completionist recognition, artist dedication measure, collection milestone, superfan identifier

### SocialAndSharingEvents

#### MusicRecommendationGiven
- **Description**: User has recommended music to others
- **Triggered When**: User shares album/track suggestion
- **Key Data**: Recommendation ID, music ID, recipient, recommendation reason, share method, recipient feedback, conversion flag
- **Consumers**: Influence tracker, recommendation history, social features, taste sharing, music evangelist metric

#### ListeningActivityShared
- **Description**: User has shared what they're currently listening to
- **Triggered When**: User posts current music to social platform
- **Key Data**: Share ID, music being played, sharing platform, share timestamp, context/comment, reactions received, engagement level
- **Consumers**: Social integration, community engagement, music identity expression, influence tracker, real-time sharing

#### MusicTasteSimilarityIdentified
- **Description**: User with similar music taste has been found
- **Triggered When**: System identifies compatible music preferences
- **Key Data**: Match ID, matched user, similarity score, common artists/genres, match date, connection potential, recommendation exchange
- **Consumers**: Social discovery, recommendation exchange, community building, music friendship, collaboration opportunity

### AnalyticsAndInsightsEvents

#### ListeningHabitAnalyzed
- **Description**: Listening patterns have been evaluated
- **Triggered When**: System performs behavioral analysis
- **Key Data**: Analysis ID, analysis period, top artists, top genres, listening time distribution, discovery rate, variety score, trend changes
- **Consumers**: Habit insights, recommendation refinement, year-in-review, personal statistics, listening optimization

#### YearInMusicGenerated
- **Description**: Annual listening summary has been created
- **Triggered When**: Year-end music statistics are compiled
- **Key Data**: Report ID, year, total listening time, top artists, top albums, top tracks, discovery count, genre breakdown, listening evolution
- **Consumers**: User dashboard, social sharing, personal archive, trend analysis, music journey retrospective

#### ListeningMilestoneReached
- **Description**: Significant listening achievement attained
- **Triggered When**: User hits notable milestone (10,000 songs played, 500 hours, etc.)
- **Key Data**: Milestone ID, milestone type, achievement date, metric achieved, historical context, celebration tier, sharing preference
- **Consumers**: Achievement system, notification service, statistics dashboard, social sharing, gamification tracker
