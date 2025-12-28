# Domain Events - Gift Idea Tracker

## Overview
This document defines the domain events tracked in the Gift Idea Tracker application. These events capture significant business occurrences related to gift ideas, occasions, purchases, recipient preferences, and gift-giving management.

## Events

### RecipientEvents

#### RecipientAdded
- **Description**: New gift recipient has been added to tracking system
- **Triggered When**: User adds person to gift recipient list
- **Key Data**: Recipient ID, name, relationship, birth date, important dates, preferences, interests, sizes, contact info
- **Consumers**: Recipient directory, occasion generator, gift suggester, reminder scheduler, preference tracker

#### RecipientPreferencesUpdated
- **Description**: Gift recipient's interests or preferences have been modified
- **Triggered When**: User updates what recipient likes or wants
- **Key Data**: Recipient ID, preference category, previous preferences, new preferences, update date, source of info
- **Consumers**: Gift recommender, idea filter, shopping guide, preference history

#### RecipientSizesRecorded
- **Description**: Clothing, shoe, or other size information has been logged
- **Triggered When**: User enters or updates size details
- **Key Data**: Recipient ID, size category, size value, update date, measurement notes, kids growth tracking
- **Consumers**: Clothing gift helper, size reference, shopping assistant, growth tracker for children

### OccasionEvents

#### OccasionCreated
- **Description**: Gift-giving occasion has been added to calendar
- **Triggered When**: User creates occasion requiring gift
- **Key Data**: Occasion ID, recipient ID, occasion type, date, importance level, budget, created by, recurring flag
- **Consumers**: Occasion calendar, reminder scheduler, budget allocator, gift brainstorming trigger

#### RecurringOccasionGenerated
- **Description**: Annual occasion has been automatically created
- **Triggered When**: System generates birthday, anniversary, or holiday
- **Key Data**: Occasion ID, recipient ID, occasion type, date, year, auto-generated flag, last year gift reference
- **Consumers**: Occasion calendar, reminder service, historical gift viewer, planning prompt

#### OccasionReminderSent
- **Description**: Upcoming occasion notification has been delivered
- **Triggered When**: Advance reminder time reached (30, 14, 7 days before)
- **Key Data**: Reminder ID, occasion ID, recipient, days until occasion, reminder level, gift ideas available, purchased status
- **Consumers**: Notification service, shopping prompt, idea review trigger, urgency indicator

#### OccasionCompleted
- **Description**: Gift has been given for occasion
- **Triggered When**: Occasion date passes and gift marked as given
- **Key Data**: Occasion ID, completion date, gift given, cost, recipient reaction, success rating, would buy again
- **Consumers**: Occasion archiver, gift history, preference learner, success tracker, planning for next year

### GiftIdeaEvents

#### GiftIdeaAdded
- **Description**: Gift suggestion has been captured
- **Triggered When**: User logs gift idea for recipient
- **Key Data**: Idea ID, recipient ID, gift description, category, price range, where to buy, priority, idea source, added date
- **Consumers**: Idea bank, gift suggester, shopping list, price tracker, idea organizer

#### GiftIdeaShared
- **Description**: Gift suggestion has been shared with others
- **Triggered When**: User shares idea with family for coordination
- **Key Data**: Idea ID, shared by, shared with, share date, purpose (claim/coordinate/suggest), permissions
- **Consumers**: Collaboration tool, duplicate preventer, family coordination, claim tracker

#### GiftIdeaClaimed
- **Description**: Someone has committed to purchasing specific gift idea
- **Triggered When**: User or family member claims idea to prevent duplicates
- **Key Data**: Idea ID, claimed by, claim date, occasion ID, purchase commitment, claim expiration
- **Consumers**: Availability marker, duplicate preventer, accountability tracker, coordination system

#### GiftIdeaRejected
- **Description**: Gift suggestion has been deemed unsuitable
- **Triggered When**: User decides against gift idea
- **Key Data**: Idea ID, rejection date, rejection reason, alternative sought, learned preference
- **Consumers**: Idea archive, preference refinement, suggestion filter improvement, learning system

### PurchaseEvents

#### GiftPurchased
- **Description**: Gift has been bought
- **Triggered When**: User marks gift as purchased
- **Key Data**: Purchase ID, gift idea ID, occasion ID, recipient, purchase date, actual cost, store, confirmation number, delivery expected
- **Consumers**: Budget tracker, occasion checklist, delivery tracker, wrapping reminder, occasion readiness

#### GiftDelivered
- **Description**: Purchased gift has arrived
- **Triggered When**: User confirms delivery of online order or pickup
- **Key Data**: Purchase ID, delivery date, delivery location, condition check, ready for wrapping, storage location
- **Consumers**: Wrapping reminder, gift inventory, occasion preparation, delivery tracker

#### GiftWrapped
- **Description**: Gift has been prepared for giving
- **Triggered When**: User marks gift as wrapped and ready
- **Key Data**: Purchase ID, wrap date, wrapping style, card included, gift tag, ready to give flag
- **Consumers**: Occasion readiness, gift preparation tracker, last-minute checker

#### GiftGiven
- **Description**: Gift has been presented to recipient
- **Triggered When**: User confirms gift delivery to recipient
- **Key Data**: Purchase ID, occasion ID, given date, recipient reaction, success rating, would gift again, notes
- **Consumers**: Gift history, recipient preference learner, success tracker, future idea generator

#### GiftReturned
- **Description**: Gift has been taken back to store
- **Triggered When**: Gift not suitable and returned or exchanged
- **Key Data**: Purchase ID, return date, return reason, refund amount, exchanged for, recipient feedback
- **Consumers**: Budget updater, preference adjuster, learning system, purchase history

### BudgetEvents

#### GiftBudgetSet
- **Description**: Spending limit has been established for occasion or recipient
- **Triggered When**: User sets budget for gift-giving
- **Key Data**: Budget ID, occasion ID or recipient ID, budget amount, period, flexibility, budget rationale
- **Consumers**: Budget tracker, gift idea filter, spending monitor, purchase validator

#### BudgetThresholdReached
- **Description**: Gift spending has hit warning level
- **Triggered When**: Actual or planned spending reaches percentage of budget
- **Key Data**: Budget ID, threshold percentage, budgeted amount, spent/planned amount, overage risk
- **Consumers**: Alert service, spending review, idea re-evaluation, budget increase decision

#### AnnualGiftSpendingCalculated
- **Description**: Total yearly gift expenditure has been computed
- **Triggered When**: End of year or user requests spending summary
- **Key Data**: Year, total spent, spending by recipient, spending by occasion, budget adherence, comparison to previous year
- **Consumers**: Financial planning, budget setting for next year, spending insights, category analysis

### WishlistEvents

#### WishlistItemDiscovered
- **Description**: Recipient's desired item has been identified
- **Triggered When**: User finds item on recipient's wishlist or registry
- **Key Data**: Wishlist item ID, recipient ID, item description, source (Amazon, registry, hint), price, priority, discovery date
- **Consumers**: Gift idea auto-add, perfect gift identifier, occasion matcher, price tracker

#### WishlistMonitored
- **Description**: Recipient's public wishlist is being tracked
- **Triggered When**: User connects to recipient's Amazon/registry wishlist
- **Key Data**: Wishlist ID, recipient ID, platform, wishlist URL, monitoring start date, update frequency, new item alerts
- **Consumers**: Wishlist sync, new item notifier, price change tracker, gift idea importer

#### WishlistItemPriceDropped
- **Description**: Desired item now available at lower price
- **Triggered When**: Price monitoring detects significant price decrease
- **Key Data**: Wishlist item ID, previous price, new price, discount percentage, deal end date, buy now recommendation
- **Consumers**: Deal alert, purchase prompt, budget optimizer, savings opportunity

### CoordinationEvents

#### GiftCoordinationGroupCreated
- **Description**: Family members organized for joint gift or coordination
- **Triggered When**: User creates group for collaborative gift-giving
- **Key Data**: Group ID, recipient, participants, coordination type, budget pooling, communication channel
- **Consumers**: Group coordination, contribution tracker, idea collaboration, duplicate prevention

#### ContributionReceived
- **Description**: Family member has contributed to group gift
- **Triggered When**: Participant pays share for joint gift
- **Key Data**: Contribution ID, group ID, contributor, amount, payment date, payment method, total collected
- **Consumers**: Contribution tracker, budget accumulator, purchase enabler, participant thank you

#### GroupGiftPurchased
- **Description**: Collaborative gift has been bought
- **Triggered When**: Organizer purchases gift with pooled funds
- **Key Data**: Purchase ID, group ID, gift purchased, total cost, contributors, purchase date, from card wording
- **Consumers**: Contribution settler, gift tracker, participant notifier, occasion completer

### ReminderEvents

#### ShoppingReminderScheduled
- **Description**: Reminder to shop for gift has been set
- **Triggered When**: User schedules shopping reminder or system auto-generates
- **Key Data**: Reminder ID, occasion ID, reminder date, shopping window, ideas to review, budget allocated
- **Consumers**: Reminder scheduler, notification service, shopping list generator

#### LastMinuteAlert
- **Description**: Urgent reminder for approaching occasion
- **Triggered When**: Occasion very close and gift not purchased (3, 2, 1 day warnings)
- **Key Data**: Alert ID, occasion ID, days remaining, gift status, emergency gift ideas, express shipping options
- **Consumers**: Urgent notification, express gift suggester, last-minute options, stress indicator

### HistoryEvents

#### GiftHistoryReviewed
- **Description**: Past gifts for recipient have been analyzed
- **Triggered When**: User reviews what was previously gifted
- **Key Data**: Recipient ID, gifts given, date range, success ratings, patterns identified, repetition check
- **Consumers**: Idea suggester, repetition avoider, success pattern identifier, relationship gift tracker

#### GiftSuccessAnalyzed
- **Description**: Gift effectiveness has been evaluated
- **Triggered When**: User rates how well gift was received
- **Key Data**: Gift ID, success rating, recipient feedback, use frequency, regret/success flag, lessons learned
- **Consumers**: Preference learner, future idea improver, recipient insight, gift strategy optimizer

#### FavoriteGiftIdentified
- **Description**: Highly successful gift has been marked for reference
- **Triggered When**: Gift receives excellent rating or recipient loved it
- **Key Data**: Gift ID, recipient ID, gift details, why it worked, repeatability, similar ideas
- **Consumers**: Success library, idea template, relationship insight, gift excellence tracker
