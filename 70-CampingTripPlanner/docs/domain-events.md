# Domain Events - Camping Trip Planner

## Overview
This application tracks domain events related to camping trip planning, campsite selection, gear management, outdoor experiences, and trip memories. These events support trip organization, equipment tracking, and outdoor adventure optimization.

## Events

### TripPlanningEvents

#### CampingTripPlanned
- **Description**: A camping trip has been scheduled
- **Triggered When**: User creates plans for camping adventure
- **Key Data**: Trip ID, destination, planned dates, duration, trip type (car/backpacking/RV), group size, weather forecast, difficulty level
- **Consumers**: Trip calendar, gear checklist generator, weather monitor, reservation system, group coordinator

#### CampsiteResearched
- **Description**: Potential campsite has been investigated
- **Triggered When**: User gathers information about camping location
- **Key Data**: Research ID, campsite name, location, amenities, reviews, reservation requirements, fees, best season, crowd level
- **Consumers**: Campsite database, decision support, reservation planner, expectation setter, comparison tool

#### CampsiteReserved
- **Description**: Campsite booking has been confirmed
- **Triggered When**: User secures campsite reservation
- **Key Data**: Reservation ID, campsite name, site number, reservation dates, confirmation number, cost, cancellation policy, check-in time
- **Consumers**: Trip confirmer, expense tracker, reservation manager, itinerary builder, reminder service

#### TripItineraryCreated
- **Description**: Detailed trip plan has been developed
- **Triggered When**: User plans activities and daily schedule
- **Key Data**: Itinerary ID, trip ID, daily activities, hiking plans, meal plans, travel route, rest periods, flexibility buffer, group preferences
- **Consumers**: Activity scheduler, meal planner, navigation aid, group communication, time optimizer

#### TripCancelled
- **Description**: Camping trip has been called off
- **Triggered When**: User cancels planned camping trip
- **Key Data**: Cancellation ID, trip ID, cancel date, cancellation reason (weather/emergency/personal), deposits lost, reschedule intent
- **Consumers**: Calendar updater, refund processor, gear unpacking, disappointment tracker, future planning

### CampsiteEvents

#### CampsiteArrival
- **Description**: Campers have arrived at campsite
- **Triggered When**: User reaches camping destination
- **Key Data**: Arrival ID, trip ID, arrival time, site condition, first impressions, setup challenges, weather conditions, site suitability
- **Consumers**: Trip tracker, experience logger, campsite validator, setup scheduler, real-time updates

#### CampSetupCompleted
- **Description**: Camp has been established and ready
- **Triggered When**: Tent, gear, and camp area are set up
- **Key Data**: Setup ID, trip ID, setup time, setup challenges, layout efficiency, comfort level, group coordination, setup photos
- **Consumers**: Setup time tracker, efficiency analyzer, group dynamics, learning database, camp organization

#### CampsiteRated
- **Description**: Campsite has been evaluated
- **Triggered When**: User reviews campsite quality and experience
- **Key Data**: Rating ID, campsite name, overall score, amenities rating, privacy, scenery, noise level, would return flag, visit date
- **Consumers**: Campsite database, future planning, recommendation engine, rating aggregator, site selector

#### CampDeparture
- **Description**: Campers have left campsite
- **Triggered When**: User packs up and departs
- **Key Data**: Departure ID, trip ID, departure time, breakdown time, site condition left, forgotten items check, leave-no-trace compliance
- **Consumers**: Trip completion, breakdown efficiency, environmental responsibility, forgotten items tracker, trip closer

### GearManagementEvents

#### GearPackingListCreated
- **Description**: Equipment checklist for trip has been generated
- **Triggered When**: User compiles gear needed for trip
- **Key Data**: List ID, trip ID, gear items, quantities, assignment to persons, optional items, procurement needs, weight considerations
- **Consumers**: Packing organizer, gear inventory checker, shopping list, weight planner, group distribution

#### GearPacked
- **Description**: Equipment has been prepared and loaded
- **Triggered When**: User completes packing gear for trip
- **Key Data**: Packing ID, trip ID, items packed, items missing, packing date, vehicle loaded, weight total, forgotten items risk
- **Consumers**: Readiness tracker, completeness validator, departure enabler, weight monitor, organization assessor

#### GearItemPurchased
- **Description**: New camping equipment has been acquired
- **Triggered When**: User buys camping gear
- **Key Data**: Purchase ID, item name, category, purchase date, cost, intended use, quality rating, first use planned
- **Consumers**: Gear inventory, budget tracker, capability expander, equipment collection, investment tracker

#### GearItemDamaged
- **Description**: Camping equipment has been broken or damaged
- **Triggered When**: Gear fails or gets damaged during use
- **Key Data**: Damage ID, item name, trip ID, damage type, repair possibility, replacement cost, backup available, lesson learned
- **Consumers**: Gear condition tracker, replacement scheduler, budget impact, reliability assessor, backup strategy

#### GearMaintenancePerformed
- **Description**: Equipment care and servicing has been completed
- **Triggered When**: User cleans, repairs, or maintains gear
- **Key Data**: Maintenance ID, item name, maintenance type, date performed, cost, performance improvement, next maintenance due, longevity impact
- **Consumers**: Maintenance scheduler, gear condition, performance optimizer, longevity tracker, readiness assurer

### ActivityEvents

#### HikeCompleted
- **Description**: Hiking activity has been finished
- **Triggered When**: User completes a hike from camp
- **Key Data**: Hike ID, trip ID, trail name, distance, elevation gain, duration, difficulty, scenic highlights, photos taken, group all completed
- **Consumers**: Activity log, fitness tracker, trail database, experience recorder, achievement tracker

#### WildlifeEncountered
- **Description**: Wildlife sighting has occurred
- **Triggered When**: User observes animals during trip
- **Key Data**: Encounter ID, trip ID, species, encounter date/time, location, encounter type, distance, behavior observed, photos, safety considerations
- **Consumers**: Wildlife log, safety tracker, nature experience, photo collection, location annotation

#### CampfireEnjoyed
- **Description**: Campfire gathering has taken place
- **Triggered When**: User has campfire experience
- **Key Data**: Campfire ID, trip ID, fire date, duration, attendees, activities (cooking/stories/songs), fire safety, memorable moments
- **Consumers**: Experience logger, safety tracker, social bonding, tradition keeper, memory maker

#### OutdoorSkillPracticed
- **Description**: Camping or wilderness skill has been exercised
- **Triggered When**: User practices outdoor competency
- **Key Data**: Skill ID, trip ID, skill type (fire-starting/navigation/knots/shelter), practice date, success level, improvement noted, teaching opportunity
- **Consumers**: Skill development tracker, competency builder, confidence enhancer, teaching database, expertise growth

### MealEvents

#### MealPrepared
- **Description**: Camp meal has been cooked
- **Triggered When**: User prepares food at campsite
- **Key Data**: Meal ID, trip ID, meal type, recipe, cooking method, prep time, success rating, group satisfaction, cleanup time
- **Consumers**: Meal log, recipe keeper, cooking time estimator, success tracker, meal planner

#### FoodSuppliesManaged
- **Description**: Food inventory has been tracked
- **Triggered When**: User monitors food consumption and needs
- **Key Data**: Inventory ID, trip ID, inventory date, supplies remaining, shortages identified, surplus items, resupply needed, waste level
- **Consumers**: Food planner, resupply scheduler, waste tracker, budget monitor, packing optimizer

#### CampCookingRecipeShared
- **Description**: Successful camp recipe has been documented
- **Triggered When**: User records recipe for future use
- **Key Data**: Recipe ID, recipe name, ingredients, instructions, cooking equipment needed, serving size, success rating, trip tested
- **Consumers**: Recipe database, meal planner, knowledge sharing, cooking reference, trip preparation

### WeatherAndSafetyEvents

#### WeatherConditionLogged
- **Description**: Weather during trip has been recorded
- **Triggered When**: User documents weather conditions
- **Key Data**: Weather ID, trip ID, date, temperature, precipitation, wind, weather impact on plans, gear performance, comfort level
- **Consumers**: Weather tracker, trip planning, gear evaluation, comfort analyzer, seasonal patterns

#### SafetyIncidentReported
- **Description**: Safety concern or incident has occurred
- **Triggered When**: Injury, near-miss, or safety issue happens
- **Key Data**: Incident ID, trip ID, incident type, severity, date/time, persons involved, response taken, medical attention needed, prevention lessons
- **Consumers**: Safety tracker, incident log, learning system, preparedness evaluation, risk management

#### EmergencyContactNotified
- **Description**: Emergency contact has been informed of trip status
- **Triggered When**: User communicates with emergency contact
- **Key Data**: Notification ID, trip ID, contact person, notification date, message content, check-in status, expected next contact
- **Consumers**: Safety protocol, emergency system, peace of mind, accountability, trip monitoring

### ExperienceEvents

#### MemorableMomentRecorded
- **Description**: Special trip experience has been captured
- **Triggered When**: User documents significant moment
- **Key Data**: Moment ID, trip ID, description, date/time, location, people involved, photos/videos, emotional impact, sharing worthy
- **Consumers**: Memory keeper, story collection, photo album, sharing platform, experience highlighter

#### NaturePhotoTaken
- **Description**: Photo of natural scenery has been captured
- **Triggered When**: User photographs outdoor beauty
- **Key Data**: Photo ID, trip ID, photo date, location, subject, photo quality, sharing intent, memory significance, portfolio worthy
- **Consumers**: Photo gallery, memory archive, sharing platform, location marker, beauty appreciator

#### GroupBondingExperienced
- **Description**: Social connection has been strengthened
- **Triggered When**: Group activities create bonding moments
- **Key Data**: Bonding ID, trip ID, activity context, participants, bonding strength, memorable interactions, relationship impact, tradition potential
- **Consumers**: Social tracker, relationship strengthener, tradition builder, group dynamics, experience enricher

#### PersonalReflectionRecorded
- **Description**: Trip reflection or insight has been documented
- **Triggered When**: User writes thoughts about camping experience
- **Key Data**: Reflection ID, trip ID, reflection date, content, insights gained, nature connection, personal growth, gratitude expressions
- **Consumers**: Journal archive, personal growth tracker, mindfulness record, experience deepener, wisdom keeper

### PostTripEvents

#### GearCleaningCompleted
- **Description**: Equipment has been cleaned after trip
- **Triggered When**: User cleans and stores gear post-trip
- **Key Data**: Cleaning ID, trip ID, cleaning date, items cleaned, time spent, repairs identified, storage preparation, next trip readiness
- **Consumers**: Gear maintenance, readiness tracker, post-trip discipline, equipment longevity, organization system

#### TripPhotosOrganized
- **Description**: Trip photos have been sorted and catalogued
- **Triggered When**: User organizes trip photography
- **Key Data**: Organization ID, trip ID, photo count, organization date, albums created, best photos selected, sharing prepared, memories preserved
- **Consumers**: Photo library, memory keeper, sharing platform, trip archive, visual storytelling

#### TripReviewWritten
- **Description**: Comprehensive trip feedback has been created
- **Triggered When**: User writes detailed trip review
- **Key Data**: Review ID, trip ID, overall rating, highlights, challenges, lessons learned, recommendations, would revisit flag, sharing intent
- **Consumers**: Trip archive, planning reference, knowledge sharing, improvement guide, memory documentation

#### NextTripPlanned
- **Description**: Future camping trip has been inspired
- **Triggered When**: User begins planning next adventure
- **Key Data**: Planning ID, inspiration source, target location, target timeframe, lessons to apply, improvements intended, excitement level
- **Consumers**: Trip pipeline, continuous adventure, learning application, improvement implementation, outdoor lifestyle

### LocationDiscoveryEvents

#### SecretSpotDiscovered
- **Description**: Hidden or exceptional campsite has been found
- **Triggered When**: User finds outstanding undiscovered location
- **Key Data**: Discovery ID, location details, discovery date, special features, access information, share decision, return priority
- **Consumers**: Location database, personal favorites, discovery log, selective sharing, treasure collection

#### CampsiteRecommendationGiven
- **Description**: Campsite has been recommended to others
- **Triggered When**: User suggests location to fellow campers
- **Key Data**: Recommendation ID, campsite name, recommended to, recommendation reason, best time to visit, tips provided, recipient feedback
- **Consumers**: Community contribution, influence tracker, knowledge sharing, camping network, helpful resource
