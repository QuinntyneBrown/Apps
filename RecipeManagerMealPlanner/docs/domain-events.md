# Domain Events - Recipe Manager & Meal Planner

## Overview
This document defines the domain events tracked in the Recipe Manager & Meal Planner application. These events capture significant business occurrences related to recipe management, meal planning, grocery shopping, nutritional tracking, and family meal coordination.

## Events

### RecipeEvents

#### RecipeAdded
- **Description**: New recipe has been added to the recipe collection
- **Triggered When**: User creates or imports recipe with ingredients and instructions
- **Key Data**: Recipe ID, recipe name, cuisine type, ingredients, instructions, prep time, cook time, servings, difficulty, source, photos
- **Consumers**: Recipe library, search indexer, ingredient database, meal planning pool, nutrition calculator

#### RecipeModified
- **Description**: Existing recipe has been updated
- **Triggered When**: User edits recipe details, ingredients, or instructions
- **Key Data**: Recipe ID, modified fields, previous version, new version, modification date, modification notes
- **Consumers**: Recipe database, version history, meal plan updater, nutrition recalculator

#### RecipeFavorited
- **Description**: User has marked recipe as favorite
- **Triggered When**: User adds recipe to favorites collection
- **Key Data**: Recipe ID, user ID, favorited date, favorite category, personal rating
- **Consumers**: Favorites collection, recommendation engine, meal suggestion prioritizer

#### RecipeRated
- **Description**: User has reviewed and rated recipe
- **Triggered When**: User submits rating and optional review after making recipe
- **Key Data**: Recipe ID, user ID, rating score, review text, cooking date, would make again flag, difficulty experienced
- **Consumers**: Recipe ranking, recommendation system, family preferences, search relevance

### MealPlanningEvents

#### MealPlanned
- **Description**: Recipe has been scheduled for specific meal
- **Triggered When**: User assigns recipe to calendar date and meal type
- **Key Data**: Plan ID, recipe ID, planned date, meal type, servings, household members attending, dietary modifications
- **Consumers**: Meal calendar, grocery list generator, prep schedule, notification service

#### MealPlanWeekGenerated
- **Description**: Complete week of meals has been planned
- **Triggered When**: User or system creates full weekly meal plan
- **Key Data**: Week start date, meals planned, recipes used, household size, dietary preferences honored, plan method
- **Consumers**: Weekly calendar, consolidated grocery list, prep timeline, budget estimator

#### MealPlanned Modified
- **Description**: Scheduled meal has been changed or swapped
- **Triggered When**: User updates planned meal to different recipe
- **Key Data**: Plan ID, previous recipe, new recipe, modification date, reason for change
- **Consumers**: Calendar updater, grocery list adjuster, prep schedule updater, notification service

#### MealCompleted
- **Description**: Planned meal has been prepared and served
- **Triggered When**: User marks meal as cooked
- **Key Data**: Plan ID, completion date, actual servings made, feedback, leftovers amount, success rating
- **Consumers**: Cooking history, recipe popularity, leftovers tracker, meal rotation analyzer

### GroceryEvents

#### GroceryListGenerated
- **Description**: Shopping list has been created from meal plan
- **Triggered When**: System compiles ingredients from planned meals
- **Key Data**: List ID, date range covered, recipes included, ingredient count, estimated cost, store organization
- **Consumers**: Shopping list view, pantry checker, cost estimator, store organizer

#### GroceryItemAdded
- **Description**: Item has been manually added to shopping list
- **Triggered When**: User adds non-recipe ingredient or household item
- **Key Data**: Item ID, list ID, item name, quantity, category, priority, added by
- **Consumers**: Shopping list, category organizer, cost estimator

#### GroceryItemPurchased
- **Description**: Shopping list item has been bought
- **Triggered When**: User checks off item during shopping trip
- **Key Data**: Item ID, purchase date, actual quantity, actual price, store, purchaser
- **Consumers**: Pantry inventory, budget tracker, price history, shopping completion monitor

#### ShoppingTripCompleted
- **Description**: Grocery shopping session has been finished
- **Triggered When**: User completes shopping with all or most items purchased
- **Key Data**: Trip ID, completion date, total cost, items purchased, items not found, store(s) visited
- **Consumers**: Budget tracker, pantry updater, shopping analytics, cost trends

### PantryEvents

#### PantryItemAdded
- **Description**: Ingredient has been added to pantry inventory
- **Triggered When**: User adds item from shopping or existing stock
- **Key Data**: Item ID, ingredient name, quantity, unit, purchase date, expiration date, location, cost
- **Consumers**: Pantry inventory, recipe availability checker, expiration monitor, cost tracker

#### PantryItemUsed
- **Description**: Ingredient has been consumed or used in cooking
- **Triggered When**: User logs ingredient usage or recipe marks ingredients used
- **Key Data**: Item ID, quantity used, usage date, recipe ID, remaining quantity
- **Consumers**: Pantry inventory updater, restock trigger, usage analytics, cost per meal calculator

#### PantryItemExpiring
- **Description**: Pantry item is approaching expiration date
- **Triggered When**: System detects item expiration within warning window
- **Key Data**: Item ID, item name, expiration date, days until expiration, quantity remaining, recipe suggestions
- **Consumers**: Alert service, recipe suggester (using soon-to-expire ingredients), waste prevention

#### PantryStockLow
- **Description**: Staple ingredient has fallen below minimum stock level
- **Triggered When**: Item quantity reaches restock threshold
- **Key Data**: Item ID, current quantity, minimum threshold, typical usage rate, recommended restock amount
- **Consumers**: Shopping list auto-add, restock reminder, inventory analytics

### NutritionEvents

#### MealNutritionCalculated
- **Description**: Nutritional information has been computed for planned meal
- **Triggered When**: Recipe nutritional data calculated or updated
- **Key Data**: Recipe ID, serving size, calories, macronutrients, vitamins/minerals, allergens, dietary labels
- **Consumers**: Nutrition tracker, dietary goal monitor, meal balance analyzer, allergen alert

#### DailyNutritionSummarized
- **Description**: Day's total nutritional intake has been compiled
- **Triggered When**: End of day or user requests daily summary
- **Key Data**: Date, total calories, macro breakdown, meals included, nutritional goals met, deficiencies identified
- **Consumers**: Nutrition dashboard, goal tracker, health insights, meal planning recommendations

#### DietaryGoalSet
- **Description**: User has established nutritional target or dietary restriction
- **Triggered When**: User configures dietary preferences or health goals
- **Key Data**: Goal ID, goal type, target values, dietary restrictions, start date, household members affected
- **Consumers**: Meal planner, recipe filter, nutrition validator, shopping list optimizer

#### DietaryViolationDetected
- **Description**: Planned meal conflicts with dietary restriction
- **Triggered When**: System identifies restricted ingredient in planned recipe
- **Key Data**: Plan ID, recipe ID, restriction type, conflicting ingredient, affected household member, severity
- **Consumers**: Alert service, substitution suggester, meal plan validator, dietary compliance monitor

### PrepScheduleEvents

#### PrepTaskGenerated
- **Description**: Cooking preparation task has been created
- **Triggered When**: System breaks down recipe into prep steps with timing
- **Key Data**: Task ID, recipe ID, task description, duration, must complete by time, dependencies, assigned person
- **Consumers**: Prep timeline, task notification, cooking workflow, delegation system

#### MakeAheadIdentified
- **Description**: Recipe component can be prepared in advance
- **Triggered When**: System or user identifies make-ahead opportunity
- **Key Data**: Recipe ID, component description, advance prep time possible, storage instructions, reheating instructions
- **Consumers**: Prep scheduler, time optimizer, meal efficiency improver, busy day helper

#### CookingTimelineOptimized
- **Description**: Multi-recipe preparation schedule has been optimized
- **Triggered When**: System analyzes meal prep for efficient timing
- **Key Data**: Meal IDs, optimized sequence, total prep time, parallel tasks, bottlenecks identified, time saved
- **Consumers**: Prep instruction generator, kitchen workflow, time management, cooking assistant

### FamilyPreferenceEvents

#### DietaryPreferenceUpdated
- **Description**: Family member's food preferences have changed
- **Triggered When**: User updates likes, dislikes, or restrictions for household member
- **Key Data**: Member ID, preference type, ingredients affected, severity, effective date, reason
- **Consumers**: Recipe filter, meal planner, grocery adjuster, substitution engine

#### FamilyFavoriteIdentified
- **Description**: Recipe has been identified as family favorite
- **Triggered When**: Recipe consistently receives high ratings from family
- **Key Data**: Recipe ID, average rating, family member ratings, frequency made, last made date
- **Consumers**: Meal rotation suggester, comfort food identifier, reliable choice highlighter

### LeftoversEvents

#### LeftoversLogged
- **Description**: Remaining food from meal has been stored
- **Triggered When**: User logs leftover amount after meal
- **Key Data**: Leftover ID, original recipe, quantity, storage date, storage location, best by date
- **Consumers**: Leftover inventory, meal planner (use leftover reminders), food waste tracker

#### LeftoversConsumed
- **Description**: Previously stored leftovers have been eaten
- **Triggered When**: User marks leftovers as consumed
- **Key Data**: Leftover ID, consumption date, consumed by, days stored, meal planned or improvised
- **Consumers**: Leftover inventory updater, food waste reducer, meal count adjuster

#### LeftoversDiscarded
- **Description**: Leftovers have been thrown away
- **Triggered When**: User disposes of uneaten leftovers
- **Key Data**: Leftover ID, discard date, discard reason, quantity wasted, original meal cost estimate
- **Consumers**: Food waste tracker, cost waste analyzer, portion size adjuster, environmental impact calculator
