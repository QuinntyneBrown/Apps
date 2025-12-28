# Domain Events - Fishing Log & Spot Tracker

## Overview
This application tracks domain events related to fishing trips, catch logging, fishing spot management, and angling success tracking. These events support pattern recognition, spot optimization, and fishing skill development.

## Events

### FishingTripEvents

#### TripPlanned
- **Description**: A fishing trip has been scheduled
- **Triggered When**: User plans an upcoming fishing outing
- **Key Data**: Trip ID, planned date, target location, expected duration, fishing method, target species, weather forecast, tide info, participants
- **Consumers**: Trip calendar, gear preparation list, weather monitor, license checker, spot availability tracker

#### TripStarted
- **Description**: Fishing trip has commenced
- **Triggered When**: User begins fishing session
- **Key Data**: Trip ID, actual start time, location, weather conditions, water temperature, water clarity, tide stage, moon phase, companions
- **Consumers**: Trip logger, time tracker, location recorder, condition logger, catch expectation calculator

#### TripCompleted
- **Description**: Fishing trip has ended
- **Triggered When**: User concludes fishing session
- **Key Data**: Trip ID, end time, total duration, location(s) visited, total catches, keeper count, trip rating, conditions summary, fuel cost, overall success
- **Consumers**: Trip history, statistics calculator, location success tracker, cost analyzer, trip rating aggregator

#### TripCancelled
- **Description**: Planned fishing trip was cancelled
- **Triggered When**: User cancels scheduled trip
- **Key Data**: Trip ID, cancellation date, cancellation reason (weather/personal/equipment), rescheduled flag, deposits lost
- **Consumers**: Calendar updater, pattern analyzer, weather impact tracker, planning optimizer

### CatchEvents

#### FishCaught
- **Description**: A fish has been caught
- **Triggered When**: User hooks and lands a fish
- **Key Data**: Catch ID, trip ID, species, catch time, location coordinates, length, weight, catch method, lure/bait used, water depth, photo
- **Consumers**: Catch log, species tracker, location analyzer, success pattern identifier, personal best tracker

#### PersonalBestRecorded
- **Description**: A personal record fish has been caught
- **Triggered When**: Catch exceeds previous best for species or overall
- **Key Data**: Record ID, catch ID, species, measurement, previous record, beat by margin, catch date, location, conditions, verification photo
- **Consumers**: Achievement system, personal records, bragging rights tracker, milestone celebrator, leaderboard

#### FishReleased
- **Description**: A caught fish has been returned to water
- **Triggered When**: User practices catch and release
- **Key Data**: Release ID, catch ID, release condition, handling time, revival time, release reason, photo before release, conservation note
- **Consumers**: Conservation tracker, ethical fishing log, species health monitor, release statistics, environmental impact

#### FishKept
- **Description**: A caught fish has been retained
- **Triggered When**: User keeps fish within legal limits
- **Key Data**: Keeper ID, catch ID, species, size, weight, legal verification, intended use (eat/mount), limit remaining, harvest date
- **Consumers**: Harvest tracker, legal limit monitor, cooler inventory, meal planner, regulation compliance checker

#### CatchPhotoTaken
- **Description**: Photo of caught fish has been captured
- **Triggered When**: User photographs their catch
- **Key Data**: Photo ID, catch ID, photo timestamp, fish position, measurement visible, background, photo quality, sharing permission
- **Consumers**: Photo gallery, catch verification, social sharing, species identification aid, memory keeper

### FishingSpotEvents

#### SpotDiscovered
- **Description**: A new fishing location has been found
- **Triggered When**: User identifies and logs a new fishing spot
- **Key Data**: Spot ID, location coordinates, spot name, access type (public/private), discovery date, water type, structure present, initial assessment
- **Consumers**: Spot database, map plotter, access tracker, exploration logger, potential spot ranker

#### SpotDetailsRecorded
- **Description**: Detailed information about a spot has been documented
- **Triggered When**: User adds comprehensive spot information
- **Key Data**: Spot ID, depth map, bottom structure, vegetation, current flow, seasonal patterns, best times, parking location, boat ramp details
- **Consumers**: Spot knowledge base, fishing planner, seasonal optimizer, access guide, success predictor

#### SpotRated
- **Description**: A fishing spot has been evaluated for quality
- **Triggered When**: User assigns rating after fishing at location
- **Key Data**: Rating ID, spot ID, overall score, fish quantity, fish quality, accessibility, scenery, crowding level, rating date, season
- **Consumers**: Spot ranking system, trip planning, seasonal comparison, location recommender

#### SpotConditionsUpdated
- **Description**: Current conditions at a spot have been updated
- **Triggered When**: User reports recent conditions at location
- **Key Data**: Update ID, spot ID, update date, water level, clarity, temperature, algae/vegetation status, recent catches, crowding, access status
- **Consumers**: Condition tracker, trip planning, real-time spot selector, pattern analyzer, community sharing

#### SecretSpotShared
- **Description**: A private fishing spot has been shared with trusted individuals
- **Triggered When**: User shares location with select people
- **Key Data**: Share ID, spot ID, shared with, share date, confidentiality level, conditions of share, reciprocal sharing expectation
- **Consumers**: Trust network, spot access manager, relationship tracker, ethical sharing monitor

### EquipmentAndTechniqueEvents

#### LureSuccessRecorded
- **Description**: A lure or bait has proven effective
- **Triggered When**: User logs successful catch with specific tackle
- **Key Data**: Success ID, lure/bait ID, species caught, location, conditions, presentation method, time of day, number of strikes, hookup ratio
- **Consumers**: Lure effectiveness tracker, pattern analyzer, tackle box optimizer, shopping recommender, technique library

#### TechniqueAttempted
- **Description**: A new fishing technique has been tried
- **Triggered When**: User experiments with unfamiliar fishing method
- **Key Data**: Technique ID, technique name, trip ID, attempt date, success level, catches resulting, learning notes, continue using flag
- **Consumers**: Skill development tracker, technique library, learning progress, success analyzer, expertise builder

#### GearPurchased
- **Description**: New fishing equipment has been acquired
- **Triggered When**: User adds gear to their collection
- **Key Data**: Purchase ID, equipment type, brand/model, purchase date, cost, intended use, first use date, performance expectation
- **Consumers**: Equipment inventory, budget tracker, gear effectiveness monitor, ROI calculator, tackle box manager

#### EquipmentLostOrBroken
- **Description**: Fishing gear has been lost or damaged
- **Triggered When**: User reports equipment loss or failure
- **Key Data**: Loss ID, equipment ID, incident date, loss type (snag/break/lost), location, replacement needed, cost impact, lesson learned
- **Consumers**: Equipment inventory, replacement planner, budget impact, reliability tracker, technique adjustment

### EnvironmentalEvents

#### WeatherPatternObserved
- **Description**: Weather correlation with fishing success has been noted
- **Triggered When**: User identifies weather impact on fishing
- **Key Data**: Observation ID, trip ID, weather type, barometric pressure, temperature, wind, success correlation, pattern identified
- **Consumers**: Weather pattern analyzer, trip planning optimizer, success predictor, best conditions identifier

#### SeasonalPatternIdentified
- **Description**: Seasonal fishing pattern has been recognized
- **Triggered When**: System or user detects seasonal success trends
- **Key Data**: Pattern ID, season, species affected, location, success indicators, timing windows, water temperature range, moon phase correlation
- **Consumers**: Seasonal planner, trip timing optimizer, species targeter, pattern library, predictive analytics

#### WaterConditionLogged
- **Description**: Water conditions during trip have been documented
- **Triggered When**: User records water quality and characteristics
- **Key Data**: Log ID, trip ID, temperature, clarity, current speed, water level vs normal, pH if tested, oxygen level, pollution indicators
- **Consumers**: Condition tracker, success correlator, spot health monitor, environmental database, pattern analyzer

### ComplianceEvents

#### FishingLicenseRenewed
- **Description**: Fishing license has been updated or renewed
- **Triggered When**: User obtains or renews fishing license
- **Key Data**: License ID, license type, issue date, expiration date, cost, jurisdiction, endorsements, renewal reminder date
- **Consumers**: Compliance tracker, reminder service, budget tracker, legal checker, expiration alerter

#### RegulationChecked
- **Description**: Current fishing regulations have been verified
- **Triggered When**: User reviews rules for location and species
- **Key Data**: Check ID, jurisdiction, check date, species limits, size limits, season dates, method restrictions, special rules
- **Consumers**: Compliance system, legal limit enforcer, harvest tracker, planning tool, regulation updater

#### LimitReached
- **Description**: Legal catch limit has been reached
- **Triggered When**: User reaches maximum allowed harvest
- **Key Data**: Limit ID, trip ID, species, limit type (daily/possession), count at limit, time limit reached, continued fishing flag
- **Consumers**: Harvest monitor, compliance enforcer, trip decision maker, release encourager, legal protector

### SocialAndCompetitionEvents

#### TripSharedWithGroup
- **Description**: Fishing trip has been shared with fishing community
- **Triggered When**: User posts trip report to social platform or forum
- **Key Data**: Share ID, trip ID, platform, share date, report content, photos shared, location specificity, community response
- **Consumers**: Social platform, community engagement, spot popularity tracker, influence measure, fishing network

#### TournamentEntered
- **Description**: User has registered for fishing tournament
- **Triggered When**: User signs up for competitive fishing event
- **Key Data**: Tournament ID, event name, entry date, entry fee, tournament rules, target species, weigh-in location, payout structure
- **Consumers**: Competition tracker, trip scheduler, strategy planner, preparation checklist, result anticipator

#### TournamentResultRecorded
- **Description**: Tournament performance has been logged
- **Triggered When**: Competition concludes and results are final
- **Key Data**: Result ID, tournament ID, placement, total weight, biggest fish, prize won, competitors count, conditions during event, lessons learned
- **Consumers**: Achievement tracker, competitive history, skill validator, prize tracker, improvement identifier
