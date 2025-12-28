# Domain Events - Travel Destination Wishlist

## Overview
This application tracks domain events related to travel planning, destination wishlisting, trip experiences, memory documentation, and travel goal tracking. These events support trip planning, budget management, and travel life enrichment.

## Events

### WishlistEvents

#### DestinationAddedToWishlist
- **Description**: A new travel destination has been added to wishlist
- **Triggered When**: User identifies a place they want to visit
- **Key Data**: Destination ID, location name, country, region, wishlist date, priority level, inspiration source, target season, estimated budget
- **Consumers**: Wishlist manager, trip planner, budget estimator, inspiration tracker, destination organizer

#### DestinationPrioritized
- **Description**: Wishlist destinations have been ranked
- **Triggered When**: User organizes wishlist by priority or preference
- **Key Data**: Prioritization ID, reorder date, new rankings, criteria (bucket list/ease/cost/season), next trip target
- **Consumers**: Trip planning, priority queue, budget allocation, timing optimizer, goal setter

#### DestinationResearched
- **Description**: User has gathered information about a destination
- **Triggered When**: User investigates travel details for wishlist location
- **Key Data**: Research ID, destination ID, research date, information gathered, attractions identified, costs researched, logistics notes, feasibility assessment
- **Consumers**: Knowledge base, trip planner, budget refiner, itinerary builder, decision support

#### DestinationRemovedFromWishlist
- **Description**: Location has been removed from travel wishlist
- **Triggered When**: User deletes destination or marks as no longer interested
- **Key Data**: Removal ID, destination ID, removal date, removal reason (visited/interest lost/impractical), time on wishlist
- **Consumers**: Wishlist updater, interest tracker, visited places marker, goal adjuster

### TripPlanningEvents

#### TripPlanned
- **Description**: A specific trip has been planned
- **Triggered When**: User creates concrete travel plans
- **Key Data**: Trip ID, destination(s), planned dates, duration, travel companions, budget estimate, accommodation type, transportation method
- **Consumers**: Trip calendar, budget planner, booking coordinator, packing list generator, countdown timer

#### TripBooked
- **Description**: Travel arrangements have been confirmed
- **Triggered When**: User finalizes bookings for trip
- **Key Data**: Booking ID, trip ID, confirmation numbers, flights, accommodations, rental cars, tours, total cost, booking date, cancellation policies
- **Consumers**: Trip organizer, budget tracker, confirmation vault, itinerary builder, expense manager

#### ItineraryCreated
- **Description**: Detailed day-by-day trip plan has been developed
- **Triggered When**: User plans daily activities and schedule
- **Key Data**: Itinerary ID, trip ID, daily activities, time allocations, reservations, attraction bookings, meal plans, flexibility buffer
- **Consumers**: Trip scheduler, activity tracker, reservation reminder, time optimizer, travel guide

#### TripRescheduled
- **Description**: Travel dates or plans have been changed
- **Triggered When**: User modifies existing trip arrangements
- **Key Data**: Reschedule ID, trip ID, original dates, new dates, reason for change, rebooking costs, cancellation impacts, revised itinerary
- **Consumers**: Trip calendar updater, budget adjuster, rebooking manager, notification service, impact analyzer

#### TripCancelled
- **Description**: Planned trip has been cancelled
- **Triggered When**: User abandons travel plans
- **Key Data**: Cancellation ID, trip ID, cancellation date, cancellation reason, deposits lost, refunds received, trip insurance claim, emotional impact
- **Consumers**: Calendar clearer, budget reconciler, insurance processor, destination re-prioritizer, learning tracker

### TripExperienceEvents

#### TripStarted
- **Description**: User has begun their journey
- **Triggered When**: Travel commences
- **Key Data**: Trip start ID, trip ID, actual start date, departure location, initial impressions, travel conditions, excitement level
- **Consumers**: Trip tracker, real-time logger, safety check-in, journey documenter, experience capturer

#### AttractionVisited
- **Description**: Tourist attraction or landmark has been visited
- **Triggered When**: User visits planned or spontaneous attraction
- **Key Data**: Visit ID, trip ID, attraction name, visit date, time spent, entrance cost, experience rating, photos taken, highlights
- **Consumers**: Experience log, expense tracker, attraction reviewer, itinerary validator, memory keeper

#### LocalExperienceEnjoyed
- **Description**: Authentic local activity has been experienced
- **Triggered When**: User participates in cultural or local experience
- **Key Data**: Experience ID, trip ID, activity description, location, date, cost, cultural significance, authenticity rating, would recommend flag
- **Consumers**: Cultural experience tracker, authentic travel monitor, recommendation generator, memory highlights, cultural engagement

#### AccommodationReviewed
- **Description**: Stay at hotel or lodging has been evaluated
- **Triggered When**: User rates accommodation after stay
- **Key Data**: Review ID, trip ID, accommodation name, location, dates stayed, rating, amenities, service quality, value for money, would return
- **Consumers**: Accommodation database, future reference, recommendation engine, budget evaluator, quality tracker

#### TransportationExperienceLogged
- **Description**: Travel transportation experience has been documented
- **Triggered When**: User records flight, train, bus, or other transit experience
- **Key Data**: Transport ID, trip ID, transport type, route, provider, date, class/comfort level, cost, on-time performance, experience rating
- **Consumers**: Transportation reference, future planning, cost analyzer, reliability tracker, comfort assessor

### MemoryDocumentationEvents

#### TripPhotoUploaded
- **Description**: Travel photo has been added to trip album
- **Triggered When**: User captures and saves trip photo
- **Key Data**: Photo ID, trip ID, location, photo date, caption, tags, photo type (landscape/people/food), favorite flag, sharing permission
- **Consumers**: Photo gallery, memory keeper, location tagger, album organizer, sharing platform

#### TravelJournalEntryWritten
- **Description**: Written reflection about travel experience has been created
- **Triggered When**: User documents thoughts and experiences
- **Key Data**: Entry ID, trip ID, entry date, content, location context, emotions, highlights, challenges, people met, lessons learned
- **Consumers**: Journal archive, memory keeper, reflection library, experience enricher, storytelling source

#### MemorableInteractionRecorded
- **Description**: Significant personal encounter has been logged
- **Triggered When**: User documents meaningful interaction with locals or travelers
- **Key Data**: Interaction ID, trip ID, encounter date, people involved, context, cultural exchange, impact, lasting impression, contact maintained
- **Consumers**: Human connection tracker, cultural exchange log, relationship builder, travel enrichment, story collection

#### TripHighlightMarked
- **Description**: Exceptional trip moment has been flagged
- **Triggered When**: User identifies standout experience
- **Key Data**: Highlight ID, trip ID, moment description, location, date, emotional impact, photos/videos, why significant, sharing priority
- **Consumers**: Memory highlights, story selection, sharing content, trip essence capturer, nostalgia trigger

### BudgetAndExpenseEvents

#### TripBudgetSet
- **Description**: Financial budget for trip has been established
- **Triggered When**: User defines spending limits for trip
- **Key Data**: Budget ID, trip ID, total budget, category allocations (transport/accommodation/food/activities), buffer amount, funding sources
- **Consumers**: Budget manager, expense tracker, overspending alerter, category monitor, financial planner

#### TravelExpenseRecorded
- **Description**: Trip expense has been logged
- **Triggered When**: User documents spending during travel
- **Key Data**: Expense ID, trip ID, amount, category, date, vendor, payment method, currency, receipt, notes
- **Consumers**: Expense aggregator, budget monitor, category tracker, currency converter, reimbursement collector

#### BudgetThresholdReached
- **Description**: Spending has approached or exceeded budget limit
- **Triggered When**: Expenses reach warning level or budget cap
- **Key Data**: Alert ID, trip ID, budget category, threshold percentage, amount over/under, alert date, spending adjustment needed
- **Consumers**: Alert system, budget manager, spending moderator, financial awareness, trip adjustor

#### TripCostReconciled
- **Description**: Final trip costs have been calculated and reviewed
- **Triggered When**: User totals all expenses after trip
- **Key Data**: Reconciliation ID, trip ID, total spent, budget comparison, over/under amount, spending breakdown, cost per day, value assessment
- **Consumers**: Financial tracker, budget analyzer, future trip budgeting, cost learning, value evaluator

### GoalAndAchievementEvents

#### TravelGoalSet
- **Description**: Specific travel objective has been established
- **Triggered When**: User defines travel target (countries visited, continents, experiences)
- **Key Data**: Goal ID, goal type, target metric, timeframe, progress tracking, motivation, priority level, milestone markers
- **Consumers**: Goal tracker, progress monitor, motivation system, achievement anticipator, bucket list manager

#### CountryVisited
- **Description**: User has traveled to a new country
- **Triggered When**: Trip includes previously unvisited country
- **Key Data**: Country visit ID, country name, visit date, trip ID, first impressions, would return flag, country count update
- **Consumers**: Countries visited tracker, world map progress, travel diversity monitor, achievement system, exploration metric

#### ContinentCompleted
- **Description**: User has visited all countries in a continent (or significant portion)
- **Triggered When**: Continental travel milestone is reached
- **Key Data**: Continent ID, completion date, countries visited, remaining countries, completion percentage, travel duration, highlight countries
- **Consumers**: Achievement tracker, continental explorer badge, goal progression, travel accomplishment, bucket list milestone

#### TravelMilestoneAchieved
- **Description**: Significant travel achievement has been reached
- **Triggered When**: User hits notable milestone (50 countries, 7 continents, 100 flights, etc.)
- **Key Data**: Milestone ID, milestone type, achievement date, metric value, historical context, celebration level, sharing preference
- **Consumers**: Achievement system, notification service, statistics dashboard, social sharing, gamification tracker

### SocialAndSharingEvents

#### TripRecommendationReceived
- **Description**: Someone has suggested a destination or experience
- **Triggered When**: Friend or contact recommends travel idea
- **Triggered When**: User receives travel suggestion
- **Key Data**: Recommendation ID, destination/experience, recommender, recommendation reason, reception date, interest level, added to wishlist flag
- **Consumers**: Recommendation tracker, wishlist consideration, social influence, discovery facilitator, trust-based planning

#### TravelStoryShared
- **Description**: Trip experience has been shared with others
- **Triggered When**: User posts trip story or photos publicly
- **Key Data**: Share ID, trip ID, content shared, sharing platform, share date, audience, engagement received, sharing purpose
- **Consumers**: Social platform, community engagement, influence tracker, storytelling archive, inspiration provider

#### TravelBuddyInvited
- **Description**: Companion has been invited to join trip
- **Triggered When**: User invites someone to travel together
- **Key Data**: Invitation ID, trip ID, invitee, invitation date, trip details shared, response, planning collaboration level
- **Consumers**: Travel companion manager, trip collaboration, shared planning, relationship builder, group coordinator

#### TripCompletionCelebrated
- **Description**: Completed trip has been celebrated or commemorated
- **Triggered When**: User marks trip completion with celebration
- **Key Data**: Celebration ID, trip ID, completion date, celebration type, memories shared, thank yous sent, trip rating, would revisit flag
- **Consumers**: Memory closer, gratitude tracker, trip archive, experience appreciator, future trip motivator
