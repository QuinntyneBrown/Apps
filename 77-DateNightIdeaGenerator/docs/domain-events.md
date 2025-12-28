# Domain Events - Date Night Idea Generator

## Overview
This application helps couples discover, plan, and track date night experiences. Domain events capture the creation and curation of date ideas, user ratings and feedback, experience completion, and the evolution of preferences over time.

## Events

### DateIdeaEvents

#### DateIdeaGenerated
- **Description**: A new date idea has been generated or suggested to the user
- **Triggered When**: User requests a new date idea based on preferences, budget, or filters
- **Key Data**: Idea ID, category, activity type, estimated cost, duration, location requirements, difficulty level, generation timestamp
- **Consumers**: Recommendation engine, idea history tracker, analytics service

#### DateIdeaSaved
- **Description**: User has saved a date idea to their favorites or planning list
- **Triggered When**: User bookmarks or saves an idea for future reference
- **Key Data**: Idea ID, user ID, save timestamp, planned date (if scheduled), list category
- **Consumers**: User profile service, recommendation engine (to improve suggestions), planning dashboard

#### DateIdeaRejected
- **Description**: User has rejected or dismissed a suggested date idea
- **Triggered When**: User indicates they don't like or want to skip a suggestion
- **Key Data**: Idea ID, rejection reason, user preferences, timestamp
- **Consumers**: Recommendation engine (to avoid similar suggestions), preference learning system

#### CustomIdeaCreated
- **Description**: User has created their own custom date idea
- **Triggered When**: User adds a personal date idea to the system
- **Key Data**: Idea ID, title, description, category, estimated cost, location, user notes, creation timestamp
- **Consumers**: Personal idea library, community sharing service (if enabled), recommendation engine

### PlanningEvents

#### DateNightScheduled
- **Description**: A date idea has been scheduled for a specific date and time
- **Triggered When**: User commits to a date idea and sets a date
- **Key Data**: Planning ID, idea ID, scheduled date/time, reservations made, budget allocated, participants, timestamp
- **Consumers**: Calendar integration service, reminder system, preparation checklist generator

#### DateNightReservationMade
- **Description**: Reservations or bookings have been confirmed for the date
- **Triggered When**: User marks that reservations are complete (restaurant, tickets, etc.)
- **Key Data**: Planning ID, reservation type, confirmation number, vendor/venue, reservation timestamp
- **Consumers**: Reminder service, date night checklist, budget tracker

#### DateNightCancelled
- **Description**: A scheduled date night has been cancelled
- **Triggered When**: User cancels or postpones their planned date
- **Key Data**: Planning ID, idea ID, cancellation reason, original scheduled date, timestamp
- **Consumers**: Calendar service, analytics (to understand cancellation patterns), idea re-suggestion service

### ExperienceEvents

#### DateNightCompleted
- **Description**: A date night has been completed and marked as done
- **Triggered When**: Date occurs and user marks it as completed
- **Key Data**: Planning ID, idea ID, completion date, actual cost, duration, notes, timestamp
- **Consumers**: Experience history, analytics service, rating prompt system

#### DateExperienceRated
- **Description**: User has rated their date night experience
- **Triggered When**: User provides a rating after completing a date
- **Key Data**: Experience ID, idea ID, overall rating, cost rating, enjoyment rating, detailed review, photos, timestamp
- **Consumers**: Recommendation engine, idea ranking system, public reviews (if shared), analytics

#### DatePhotoAdded
- **Description**: User has added photos from their date night
- **Triggered When**: User uploads photos to document the experience
- **Key Data**: Photo ID, experience ID, photo URLs, captions, upload timestamp
- **Consumers**: Memory gallery, social sharing service, anniversary reminder system

### CategoryEvents

#### CategoryPreferenceUpdated
- **Description**: User's preferences for date idea categories have changed
- **Triggered When**: User explicitly updates preferences or system learns from behavior
- **Key Data**: User ID, category preferences, liked categories, disliked categories, preference weights, timestamp
- **Consumers**: Recommendation engine, idea filtering service, personalization system

#### NewCategoryAdded
- **Description**: A new category of date ideas has been added to the system
- **Triggered When**: Administrator or user adds a new activity category
- **Key Data**: Category ID, category name, description, parent category, icon, timestamp
- **Consumers**: Idea generation service, filtering system, UI category selector

### BudgetEvents

#### BudgetLimitSet
- **Description**: User has set or updated their date night budget preferences
- **Triggered When**: User configures budget constraints or spending limits
- **Key Data**: User ID, budget tier (free/low/medium/high), specific amount ranges, frequency limits, timestamp
- **Consumers**: Idea filtering service, financial tracking, spending analytics

#### ActualSpendingRecorded
- **Description**: Actual spending for a date night has been recorded
- **Triggered When**: User logs what they actually spent on a date
- **Key Data**: Experience ID, budgeted amount, actual amount, variance, spending breakdown, timestamp
- **Consumers**: Budget analytics, future budgeting suggestions, spending trend tracker

### SocialEvents

#### IdeaSharedWithPartner
- **Description**: A date idea has been shared with a partner for approval
- **Triggered When**: User sends an idea to their partner for feedback
- **Key Data**: Idea ID, sender ID, recipient ID, message, share timestamp
- **Consumers**: Partner notification service, collaborative planning dashboard, response tracking

#### PartnerResponseReceived
- **Description**: Partner has responded to a shared date idea
- **Triggered When**: Partner approves, rejects, or suggests modifications
- **Key Data**: Idea ID, response type, feedback comments, timestamp
- **Consumers**: Planning workflow, notification service, idea finalization process

#### IdeaSharedToCommunity
- **Description**: User has shared their date idea or experience with the community
- **Triggered When**: User publishes their idea or review publicly
- **Key Data**: Idea ID, experience ID, public visibility settings, rating, review content, timestamp
- **Consumers**: Community idea feed, discovery service, social engagement tracker
