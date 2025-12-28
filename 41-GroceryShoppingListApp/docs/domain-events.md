# Domain Events - Grocery Shopping List App

## Overview
This document defines the domain events tracked in the Grocery Shopping List App. These events capture significant business occurrences related to shopping list creation, item management, price tracking, store organization, and collaborative family shopping.

## Events

### ListEvents

#### ShoppingListCreated
- **Description**: New grocery shopping list has been started
- **Triggered When**: User creates fresh shopping list
- **Key Data**: List ID, list name, created by, creation date, target store, target shop date, household members shared with
- **Consumers**: List manager, sharing service, reminder scheduler, budget estimator

#### ListShared
- **Description**: Shopping list has been shared with household members
- **Triggered When**: User grants access to list to other family members
- **Key Data**: List ID, shared by, shared with, share date, permission level (view/edit), notification sent
- **Consumers**: Collaboration manager, notification service, permission controller, real-time sync enabler

#### ListCompleted
- **Description**: Shopping trip has been finished and list marked complete
- **Triggered When**: User indicates shopping is done
- **Key Data**: List ID, completion date, items purchased count, items not found, total spent, store visited, shopper
- **Consumers**: Shopping history, budget tracker, analytics, list archiver, repurchase pattern analyzer

#### ListArchived
- **Description**: Shopping list has been moved to historical records
- **Triggered When**: Completed list archived or user manually archives
- **Key Data**: List ID, archive date, final status, items purchased, total cost, archive reason
- **Consumers**: Archive storage, history viewer, template library, analytics database

### ItemEvents

#### ItemAddedToList
- **Description**: Grocery item has been added to shopping list
- **Triggered When**: User adds item manually or from suggestions
- **Key Data**: Item ID, list ID, item name, quantity, unit, category, priority, brand preference, added by, estimated price
- **Consumers**: List updater, category organizer, budget estimator, duplicate detector, store aisle mapper

#### ItemQuantityUpdated
- **Description**: Quantity needed for item has been changed
- **Triggered When**: User adjusts how much of item to buy
- **Key Data**: Item ID, previous quantity, new quantity, update timestamp, updated by, reason for change
- **Consumers**: List updater, budget recalculator, recipe adjuster if linked, sharing notifier

#### ItemMarkedPurchased
- **Description**: Item has been checked off as bought
- **Triggered When**: User marks item as purchased during shopping
- **Key Data**: Item ID, purchase timestamp, actual quantity, actual price, purchaser, store, variation from estimate
- **Consumers**: List progress tracker, budget tracker, price history, shopping completion calculator

#### ItemMarkedUnavailable
- **Description**: Item could not be found at store
- **Triggered When**: User marks item as out of stock or not carried
- **Key Data**: Item ID, store, unavailability date, reason, substitution made, alternative store needed
- **Consumers**: Alternative suggester, store inventory tracker, substitute recommender, alternate store finder

#### ItemRemoved
- **Description**: Item has been deleted from shopping list
- **Triggered When**: User removes item before purchasing
- **Key Data**: Item ID, list ID, removal date, removed by, removal reason, was purchased elsewhere flag
- **Consumers**: List updater, budget adjuster, item popularity tracker, deletion history

### CategoryEvents

#### CategoryAssigned
- **Description**: Item has been categorized for organization
- **Triggered When**: User manually categorizes or system auto-categorizes item
- **Key Data**: Item ID, category, assignment method, assignment date, previous category
- **Consumers**: List organizer, store layout optimizer, aisle grouper, category filter

#### StoreLayoutCustomized
- **Description**: User has defined custom store aisle organization
- **Triggered When**: User sets up preferred store layout and category order
- **Key Data**: Store ID, category sequence, aisle mapping, custom categories, layout date, user ID
- **Consumers**: List sorter, shopping route optimizer, in-store navigation, efficiency improver

### PriceTrackingEvents

#### ItemPriceRecorded
- **Description**: Purchase price for item has been logged
- **Triggered When**: User enters actual price paid during or after shopping
- **Key Data**: Item ID, price, store, purchase date, unit size, price per unit, sale/regular price flag
- **Consumers**: Price history, price trend analyzer, budget tracker, best price finder, savings calculator

#### PriceIncreaseDetected
- **Description**: Item price has risen significantly from last purchase
- **Triggered When**: System compares current price to historical average
- **Key Data**: Item ID, previous price, current price, increase percentage, store, detection date
- **Consumers**: Price alert, budget adjuster, alternative brand suggester, deal finder

#### DealIdentified
- **Description**: Item is on sale or special promotion
- **Triggered When**: User marks item on sale or system detects from store API
- **Key Data**: Item ID, regular price, sale price, discount percentage, store, deal start date, deal end date
- **Consumers**: Deal alert, buy now recommender, stock up suggester, savings tracker

#### BestPriceStoreIdentified
- **Description**: System has determined which store offers best price
- **Triggered When**: Price comparison across stores completed
- **Key Data**: Item ID, stores compared, best price store, price difference, savings potential, distance factor
- **Consumers**: Store recommendation, shopping optimizer, multi-store trip planner, savings maximizer

### RecurringItemEvents

#### RecurringItemCreated
- **Description**: Item has been set to auto-add to shopping lists
- **Triggered When**: User designates frequently purchased item for auto-replenishment
- **Key Data**: Recurring item ID, item name, frequency, next add date, preferred quantity, household consumption rate
- **Consumers**: Auto-add scheduler, list generator, consumption tracker, household staples manager

#### RecurringItemTriggered
- **Description**: Recurring item has been automatically added to list
- **Triggered When**: Scheduled time arrives or consumption threshold met
- **Key Data**: Item ID, list ID, trigger reason, trigger date, auto-added quantity, user notification sent
- **Consumers**: List populator, notification service, recurring item updater, pattern learner

#### ConsumptionRateUpdated
- **Description**: Household usage rate for item has been recalculated
- **Triggered When**: System analyzes purchase frequency to adjust recurring schedule
- **Key Data**: Item ID, previous rate, new rate, data basis, adjustment date, confidence level
- **Consumers**: Recurring scheduler, next purchase predictor, quantity recommender, inventory optimizer

### BudgetEvents

#### ShoppingBudgetSet
- **Description**: Spending limit has been established for shopping trip
- **Triggered When**: User sets budget for list or shopping period
- **Key Data**: Budget ID, list ID, budget amount, period, category allocations, buffer amount
- **Consumers**: Budget tracker, spending monitor, overage alerter, purchase decision support

#### BudgetThresholdReached
- **Description**: Shopping list total has reached budget warning level
- **Triggered When**: Estimated or actual total hits percentage threshold (75%, 90%, 100%)
- **Key Data**: List ID, budget amount, current total, threshold percentage, items contributing, overage risk
- **Consumers**: Alert service, item prioritizer, removal suggester, substitute recommender

#### ActualSpendingRecorded
- **Description**: Final shopping trip cost has been logged
- **Triggered When**: Shopping completed with all prices entered
- **Key Data**: List ID, total spent, budget amount, variance, savings achieved, store, shopping date
- **Consumers**: Budget analyzer, spending trends, savings tracker, budget adjuster, financial integration

### CollaborationEvents

#### ItemSuggestionMade
- **Description**: Household member has suggested item for list
- **Triggered When**: Shared list member recommends adding item
- **Key Data**: Suggestion ID, list ID, suggested item, suggested by, suggestion date, reason, pending approval
- **Consumers**: Suggestion queue, primary shopper notification, approval workflow, collaboration tracker

#### ItemSuggestionApproved
- **Description**: Suggested item has been accepted and added to list
- **Triggered When**: List owner approves suggested item
- **Key Data**: Suggestion ID, approval date, approved by, added to list ID, quantity finalized
- **Consumers**: List updater, suggester notification, collaboration success tracker, family engagement

#### RealTimeUpdateSynced
- **Description**: List change has been synchronized across shared users
- **Triggered When**: Any edit made to shared list
- **Key Data**: List ID, change type, changed by, sync timestamp, recipients notified, sync conflicts
- **Consumers**: Real-time sync engine, conflict resolver, notification service, version control

### TemplateEvents

#### ListTemplateCreated
- **Description**: Reusable shopping list template has been saved
- **Triggered When**: User saves frequently used list as template
- **Key Data**: Template ID, template name, items included, category, created by, creation date, usage count
- **Consumers**: Template library, quick list generator, template recommender, household favorites

#### ListFromTemplateGenerated
- **Description**: New shopping list has been created from template
- **Triggered When**: User selects template to start new list
- **Key Data**: List ID, template ID, generation date, items copied, modifications made, generated by
- **Consumers**: List creator, template usage tracker, template effectiveness analyzer, time saver

### StoreEvents

#### PreferredStoreSet
- **Description**: User has designated default shopping location
- **Triggered When**: User configures preferred grocery store
- **Key Data**: Store ID, store name, location, set date, set by, price level, layout customization
- **Consumers**: Store selector, list organizer, price estimator, location-based features

#### MultiStoreShoppingPlanned
- **Description**: Shopping list has been split across multiple stores
- **Triggered When**: User plans to shop at different stores for different items
- **Key Data**: List ID, stores included, items per store, shopping route, estimated savings, total trip time
- **Consumers**: Multi-store coordinator, route optimizer, savings calculator, time estimator

#### StoreInventoryUpdated
- **Description**: Store product availability information has been refreshed
- **Triggered When**: Integration with store API updates stock status
- **Key Data**: Store ID, update timestamp, items updated, out of stock items, price changes
- **Consumers**: Availability checker, alternative suggester, price updater, shopping readiness validator
