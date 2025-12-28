# Domain Events - Family Vacation Planner

## Overview
This document defines the domain events tracked in the Family Vacation Planner application. These events capture significant business occurrences related to trip planning, itinerary management, booking coordination, budget tracking, and family travel logistics.

## Events

### TripEvents

#### TripCreated
- **Description**: New vacation or trip has been initiated
- **Triggered When**: User starts planning a family vacation
- **Key Data**: Trip ID, destination, start date, end date, travelers, trip type, created by, creation date, estimated budget
- **Consumers**: Trip dashboard, budget tracker, booking coordinator, packing list generator, countdown timer

#### TripDatesModified
- **Description**: Travel dates have been changed
- **Triggered When**: User adjusts trip start or end date
- **Key Data**: Trip ID, previous start date, new start date, previous end date, new end date, change date, change reason
- **Consumers**: Booking updater, calendar sync, price re-checker, availability re-validator, notification service

#### TripConfirmed
- **Description**: All major bookings finalized and trip locked in
- **Triggered When**: User marks trip as confirmed with key arrangements made
- **Key Data**: Trip ID, confirmation date, major bookings confirmed, total cost locked, final traveler count, preparation phase start
- **Consumers**: Confirmation notifier, preparation checklist generator, countdown activator, final payment tracker

#### TripCancelled
- **Description**: Planned vacation has been called off
- **Triggered When**: User cancels trip before travel dates
- **Key Data**: Trip ID, cancellation date, cancellation reason, bookings to cancel, refunds expected, cancellation costs
- **Consumers**: Booking canceller, refund tracker, insurance claim helper, budget releaser, calendar clearer

#### TripCompleted
- **Description**: Vacation has concluded and family has returned
- **Triggered When**: Trip end date passes or user marks trip complete
- **Key Data**: Trip ID, completion date, final cost, memories created, highlights, would revisit rating, expense summary
- **Consumers**: Trip archiver, expense finalizer, memory creator, review prompts, future trip suggester

### BookingEvents

#### AccommodationBooked
- **Description**: Lodging has been reserved for trip
- **Triggered When**: User books hotel, rental, resort, or other accommodation
- **Key Data**: Booking ID, trip ID, property name, location, check-in date, check-out date, cost, confirmation number, cancellation policy
- **Consumers**: Itinerary builder, budget tracker, confirmation organizer, calendar integration, check-in reminder

#### FlightBooked
- **Description**: Air travel has been reserved
- **Triggered When**: User purchases flight tickets
- **Key Data**: Booking ID, trip ID, airline, flight numbers, departure/arrival times, travelers, cost, confirmation code, seat assignments
- **Consumers**: Itinerary, travel day planner, check-in reminder, flight tracker, budget updater, airport info provider

#### ActivityReserved
- **Description**: Tour, attraction, or activity has been booked
- **Triggered When**: User reserves tickets or spots for vacation activity
- **Key Data**: Reservation ID, trip ID, activity name, date, time, participants, cost, meeting location, cancellation deadline
- **Consumers**: Daily itinerary, budget tracker, day planner, activity reminder, confirmation vault

#### RentalCarBooked
- **Description**: Vehicle rental has been reserved
- **Triggered When**: User books rental car for trip transportation
- **Key Data**: Booking ID, trip ID, rental company, pickup location, pickup date/time, return date/time, vehicle type, cost, insurance
- **Consumers**: Transportation planner, budget tracker, pickup reminder, driver assignment, itinerary integration

#### BookingModified
- **Description**: Existing reservation has been changed
- **Triggered When**: User updates booking details
- **Key Data**: Booking ID, booking type, previous details, new details, modification date, change fee, confirmation updated
- **Consumers**: Itinerary updater, cost adjuster, confirmation refresher, notification service

#### BookingCancelled
- **Description**: Reservation has been cancelled
- **Triggered When**: User cancels booking before travel
- **Key Data**: Booking ID, cancellation date, refund amount, cancellation fee, refund timeline, cancellation reason
- **Consumers**: Budget adjuster, itinerary updater, refund tracker, alternative booking suggester

### ItineraryEvents

#### DailyItineraryCreated
- **Description**: Day-by-day schedule has been planned
- **Triggered When**: User organizes activities for each day of trip
- **Key Data**: Itinerary ID, trip ID, date, activities planned, meal plans, transportation, free time, estimated costs
- **Consumers**: Daily planner, activity sequencer, time optimizer, family schedule sharer

#### ActivityAddedToItinerary
- **Description**: Event or activity has been scheduled for specific day
- **Triggered When**: User places activity into daily schedule
- **Key Data**: Activity ID, itinerary date, time slot, activity name, duration, location, participants, cost
- **Consumers**: Daily schedule, time conflict checker, map planner, reminder scheduler

#### ItineraryOptimized
- **Description**: Daily schedule has been reorganized for efficiency
- **Triggered When**: User or system reorders activities for better flow
- **Key Data**: Itinerary ID, date, previous sequence, optimized sequence, optimization criteria, time saved, travel reduced
- **Consumers**: Route planner, daily schedule, map updater, efficiency maximizer

#### FreeTimeBlocked
- **Description**: Unscheduled relaxation time has been preserved
- **Triggered When**: User designates downtime in schedule
- **Key Data**: Trip ID, date, time block, duration, location, purpose, protected from scheduling flag
- **Consumers**: Schedule protector, family balance maintainer, over-scheduling preventer

### BudgetEvents

#### TripBudgetSet
- **Description**: Overall spending limit for vacation has been established
- **Triggered When**: User defines total budget and category allocations
- **Key Data**: Budget ID, trip ID, total budget, category breakdown, per person budget, buffer amount, funding source
- **Consumers**: Budget tracker, expense validator, booking affordability checker, spending monitor

#### ExpenseLogged
- **Description**: Trip-related cost has been recorded
- **Triggered When**: User logs booking, purchase, or expense
- **Key Data**: Expense ID, trip ID, amount, category, date, description, paid by, split among travelers
- **Consumers**: Budget tracker, traveler balance calculator, spending analyzer, category monitor

#### BudgetAlertTriggered
- **Description**: Spending has reached budget warning threshold
- **Triggered When**: Total or category spending hits percentage limit
- **Key Data**: Trip ID, alert type, budget amount, amount spent, threshold percentage, overage risk, categories affected
- **Consumers**: Alert service, booking pause recommendation, spending review trigger, budget reallocation suggester

#### ExpenseSplitCalculated
- **Description**: Cost sharing among travelers has been computed
- **Triggered When**: User calculates who owes what for shared expenses
- **Key Data**: Trip ID, total shared expenses, per person amount, individual balances, settlement plan
- **Consumers**: Payment coordinator, balance tracker, settlement reminder, fairness monitor

### DocumentEvents

#### TravelDocumentUploaded
- **Description**: Important travel document has been stored
- **Triggered When**: User uploads passport, visa, ticket, or confirmation
- **Key Data**: Document ID, trip ID, document type, traveler, upload date, expiration date, document file
- **Consumers**: Document vault, expiration monitor, travel readiness checker, easy access organizer

#### DocumentExpirationWarning
- **Description**: Travel document approaching expiration
- **Triggered When**: Passport or visa expiration detected before/during trip
- **Key Data**: Document ID, traveler, document type, expiration date, days until expiration, trip dates, renewal urgency
- **Consumers**: Alert service, renewal reminder, travel risk assessor, urgent action prompter

#### ConfirmationNumberStored
- **Description**: Booking confirmation has been saved
- **Triggered When**: User stores confirmation code for reservation
- **Key Data**: Confirmation ID, booking ID, confirmation number, booking type, vendor, retrieval access
- **Consumers**: Confirmation organizer, check-in facilitator, quick reference, booking validator

### PackingEvents

#### PackingListGenerated
- **Description**: Packing checklist has been created for trip
- **Triggered When**: System or user creates list based on trip details
- **Key Data**: List ID, trip ID, destination, duration, travelers, activities, weather-based items, standard items
- **Consumers**: Packing checklist, traveler assignment, preparation timeline, packing reminder

#### ItemAddedToPackingList
- **Description**: Item has been added to packing checklist
- **Triggered When**: User adds specific item to pack
- **Key Data**: Item ID, list ID, item name, quantity, assigned to traveler, category, priority, packed status
- **Consumers**: Packing list, traveler checklist, completion tracker

#### ItemMarkedPacked
- **Description**: Item has been checked off as packed
- **Triggered When**: User confirms item is in luggage
- **Key Data**: Item ID, packed date, packed by, which bag, packing completion percentage
- **Consumers**: Packing progress, completion calculator, departure readiness, missing item alerter

#### PackingCompleted
- **Description**: All packing has been finished
- **Triggered When**: All items checked off as packed
- **Key Data**: List ID, trip ID, completion date, completed by, total items, bags count, travel ready flag
- **Consumers**: Departure readiness, completion celebration, travel day preparation

### WeatherEvents

#### WeatherForecastRetrieved
- **Description**: Destination weather forecast has been fetched
- **Triggered When**: System retrieves weather prediction for travel dates
- **Key Data**: Trip ID, destination, forecast dates, temperature range, precipitation, conditions, update date
- **Consumers**: Packing adjuster, activity planner, clothing recommender, weather alert system

#### WeatherAlertIssued
- **Description**: Severe weather warning for destination during trip
- **Triggered When**: Weather service issues alert for travel location/dates
- **Key Data**: Alert ID, trip ID, alert type, severity, affected dates, safety recommendations, activity impact
- **Consumers**: Alert notification, activity re-planner, safety advisor, trip modification suggester

### CollaborationEvents

#### TripSharedWithFamily
- **Description**: Trip planning has been shared with other travelers
- **Triggered When**: User grants access to family members
- **Key Data**: Trip ID, shared with, share date, permission level, collaboration enabled, notification sent
- **Consumers**: Collaboration manager, real-time sync, family input collector, shared planning

#### CollaborativeSuggestionMade
- **Description**: Family member has suggested trip element
- **Triggered When**: Shared traveler proposes activity, restaurant, or change
- **Key Data**: Suggestion ID, trip ID, suggested by, suggestion type, details, date suggested, votes/approvals
- **Consumers**: Suggestion queue, family voting, decision maker, collaboration tracker

#### FamilyVoteCompleted
- **Description**: Travelers have voted on trip decision
- **Triggered When**: Voting period ends or consensus reached
- **Key Data**: Vote ID, trip ID, decision topic, voters, results, winning option, implementation date
- **Consumers**: Decision finalizer, itinerary updater, booking initiator, family engagement tracker

### ReminderEvents

#### PreTripReminderSent
- **Description**: Preparation reminder has been delivered
- **Triggered When**: Scheduled time before trip (1 week, 3 days, 1 day)
- **Key Data**: Reminder ID, trip ID, reminder type, tasks to complete, sent date, recipients
- **Consumers**: Notification service, task list, preparation tracker, countdown manager

#### CheckInReminderTriggered
- **Description**: Flight or hotel check-in reminder has been sent
- **Triggered When**: Check-in window opens (24 hours for flight)
- **Key Data**: Reminder ID, booking ID, check-in type, check-in time window, confirmation number, action link
- **Consumers**: Check-in facilitator, notification service, boarding pass retriever
