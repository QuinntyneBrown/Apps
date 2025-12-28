# Domain Events - Meal Prep Planner

## Overview
This document defines the domain events tracked in the Meal Prep Planner application. These events capture significant business occurrences related to meal planning, recipe management, grocery list generation, and macronutrient tracking.

## Events

### MealPlanEvents

#### MealPlanCreated
- **Description**: A new meal plan has been designed
- **Triggered When**: User creates weekly or custom meal plan
- **Key Data**: Plan ID, plan name, start date, duration days, target calories, dietary preferences, user ID, timestamp
- **Consumers**: Plan manager, calendar integrator, shopping list generator, dashboard UI

#### MealPlanActivated
- **Description**: User has committed to following a meal plan
- **Triggered When**: User starts actively following the plan
- **Key Data**: Plan ID, activation date, end date, adherence tracking enabled, user ID
- **Consumers**: Meal tracker, reminder service, grocery list scheduler

#### MealPlanCompleted
- **Description**: Meal plan duration finished
- **Triggered When**: Plan end date reached or user marks as complete
- **Key Data**: Plan ID, completion date, adherence percentage, favorite meals, completion notes, user ID
- **Consumers**: Achievement service, historical analyzer, next plan recommender

#### MealAssigned
- **Description**: Recipe assigned to specific meal slot
- **Triggered When**: User adds recipe to breakfast, lunch, dinner, or snack slot
- **Key Data**: Assignment ID, plan ID, recipe ID, meal type, date, servings, user ID, timestamp
- **Consumers**: Nutrition calculator, shopping list generator, meal calendar

### RecipeEvents

#### RecipeAdded
- **Description**: New recipe added to recipe library
- **Triggered When**: User creates or imports recipe
- **Key Data**: Recipe ID, recipe name, ingredients, instructions, prep time, cook time, servings, user ID, timestamp
- **Consumers**: Recipe library, search index, nutrition calculator

#### RecipeNutritionCalculated
- **Description**: Macros and calories for recipe computed
- **Triggered When**: Recipe created or ingredients updated
- **Key Data**: Recipe ID, calories per serving, protein, carbs, fat, fiber, micronutrients, calculation timestamp
- **Consumers**: Nutrition tracker, meal plan optimizer, macro goal checker

#### RecipeFavorited
- **Description**: Recipe marked as favorite
- **Triggered When**: User favorites recipe for quick access
- **Key Data**: Recipe ID, favorite date, user ID
- **Consumers**: Recipe recommender, quick add service, favorites collection

#### RecipePrepared
- **Description**: Recipe has been cooked
- **Triggered When**: User marks recipe as prepared/cooked
- **Key Data**: Prep ID, recipe ID, prep date, servings made, prep notes, user ID
- **Consumers**: Meal tracker, recipe popularity tracker, batch prep logger

### GroceryListEvents

#### GroceryListGenerated
- **Description**: Shopping list created from meal plan
- **Triggered When**: User generates grocery list for upcoming week
- **Key Data**: List ID, plan ID, generation date, items count, estimated cost, user ID, timestamp
- **Consumers**: Shopping list manager, budget estimator, store organizer

#### GroceryItemAdded
- **Description**: Item manually added to grocery list
- **Triggered When**: User adds custom item not from meal plan
- **Key Data**: Item ID, list ID, item name, quantity, unit, category, user ID
- **Consumers**: Shopping list updater, category organizer, purchase tracker

#### GroceryItemPurchased
- **Description**: Item marked as bought during shopping
- **Triggered When**: User checks off item while shopping
- **Key Data**: Item ID, purchase date, actual price, store, user ID, timestamp
- **Consumers**: Purchase tracker, budget analyzer, inventory updater

#### GroceryListCompleted
- **Description**: All items on list purchased
- **Triggered When**: Final item checked off
- **Key Data**: List ID, completion date, total cost, items purchased, user ID
- **Consumers**: Budget tracker, shopping session closer, inventory updater

### MacroTrackingEvents

#### DailyMacrosCalculated
- **Description**: Daily macronutrient totals computed from meal plan
- **Triggered When**: Meals assigned or day's nutrition requested
- **Key Data**: Date, total calories, protein grams, carb grams, fat grams, fiber, user ID, timestamp
- **Consumers**: Nutrition dashboard, macro goal checker, diet adherence tracker

#### MacroGoalSet
- **Description**: Daily macro targets established
- **Triggered When**: User sets nutrition goals
- **Key Data**: Goal ID, target calories, target protein, target carbs, target fat, goal type (deficit/maintenance/surplus), user ID
- **Consumers**: Macro tracker, meal plan optimizer, adherence monitor

#### MacroGoalMet
- **Description**: Daily macro targets achieved
- **Triggered When**: Day's meals hit macro goals within tolerance
- **Key Data**: Date, target macros, actual macros, variance, achievement timestamp, user ID
- **Consumers**: Achievement service, streak tracker, nutrition compliance

#### MacroDeficitDetected
- **Description**: Significant macro shortfall identified
- **Triggered When**: Daily totals significantly below targets
- **Key Data**: Date, deficit type (protein/carbs/fat), deficit amount, deficit percentage, user ID
- **Consumers**: Alert service, meal suggestion engine, supplementation recommender

### MealPrepEvents

#### BatchPrepScheduled
- **Description**: Batch cooking session planned
- **Triggered When**: User schedules meal prep time
- **Key Data**: Prep ID, scheduled date, scheduled time, recipes to prep, estimated duration, user ID
- **Consumers**: Reminder service, calendar integrator, prep checklist generator

#### BatchPrepCompleted
- **Description**: Meal prep session finished
- **Triggered When**: User completes batch cooking
- **Key Data**: Prep ID, completion date, recipes prepared, servings made, actual duration, user ID
- **Consumers**: Meal inventory updater, efficiency tracker, prep history

#### PrepContainerPacked
- **Description**: Individual meal container prepared
- **Triggered When**: User portions meal into container
- **Key Data**: Container ID, recipe ID, servings, pack date, consumption date, storage method, user ID
- **Consumers**: Meal inventory, consumption scheduler, freshness tracker

### InventoryEvents

#### IngredientInventoryUpdated
- **Description**: Pantry/fridge ingredient quantities updated
- **Triggered When**: User updates what ingredients they have on hand
- **Key Data**: Ingredient ID, quantity, unit, location, purchase date, expiration date, user ID
- **Consumers**: Inventory tracker, recipe suggester, shopping list optimizer

#### IngredientExpiringAlert
- **Description**: Ingredient approaching expiration detected
- **Triggered When**: Expiration date within warning threshold
- **Key Data**: Ingredient ID, expiration date, days remaining, quantity, suggested recipes, user ID
- **Consumers**: Alert service, recipe recommender, waste prevention

#### PreparedMealConsumed
- **Description**: Pre-made meal eaten
- **Triggered When**: User logs consumption of prepared meal
- **Key Data**: Container ID, consumption date, recipe ID, servings consumed, user ID
- **Consumers**: Meal tracker, inventory updater, adherence tracker

### DietaryPreferenceEvents

#### DietaryRestrictionSet
- **Description**: Dietary restriction or preference configured
- **Triggered When**: User sets allergies, intolerances, or dietary choices
- **Key Data**: Restriction ID, restriction type (allergy/intolerance/preference), restricted items, severity, user ID
- **Consumers**: Recipe filter, ingredient validator, meal plan customizer

#### AllergenDetected
- **Description**: Allergen found in planned meal
- **Triggered When**: Recipe contains ingredient user is allergic to
- **Key Data**: Recipe ID, allergen ingredient, severity, detection timestamp, user ID
- **Consumers**: Alert service, recipe remover, substitute suggester

### CalorieEvents

#### DailyCalorieTargetSet
- **Description**: Daily calorie goal established
- **Triggered When**: User sets calorie target based on goals
- **Key Data**: Target ID, daily calories, goal type (deficit/maintenance/surplus), start date, user ID, timestamp
- **Consumers**: Calorie tracker, meal plan generator, portion adjuster

#### CalorieGoalReached
- **Description**: Daily calorie target achieved
- **Triggered When**: Logged meals hit calorie goal within tolerance
- **Key Data**: Date, target calories, actual calories, variance, achievement timestamp, user ID
- **Consumers**: Achievement service, diet adherence tracker, streak counter

#### SignificantCalorieDeviation
- **Description**: Daily calories significantly over or under target
- **Triggered When**: Actual intake deviates beyond threshold
- **Key Data**: Date, target calories, actual calories, deviation amount, deviation percentage, user ID
- **Consumers**: Alert service, diet adherence monitor, adjustment recommender

### VarietyEvents

#### RecipeRotationAnalyzed
- **Description**: Meal variety and repetition analyzed
- **Triggered When**: User requests variety analysis or periodic check runs
- **Key Data**: Analysis period, unique recipes, most frequent meals, variety score, timestamp, user ID
- **Consumers**: Variety dashboard, recipe suggester, diet quality assessor

#### LowVarietyDetected
- **Description**: Insufficient meal diversity identified
- **Triggered When**: Same meals repeated too frequently
- **Key Data**: Detection period, repetitive meals, variety score, recommendation threshold, user ID
- **Consumers**: Recipe recommender, meal plan diversifier, nutrition quality advisor

### BudgetEvents

#### WeeklyFoodBudgetSet
- **Description**: Grocery spending budget established
- **Triggered When**: User sets weekly or monthly food budget
- **Key Data**: Budget ID, budget amount, budget period, start date, user ID, timestamp
- **Consumers**: Budget tracker, shopping cost estimator, deal finder

#### GroceryCostTracked
- **Description**: Actual grocery spending recorded
- **Triggered When**: Shopping trip completed and costs logged
- **Key Data**: Trip ID, total cost, cost per item, shop date, budget variance, user ID
- **Consumers**: Budget analyzer, cost trend tracker, savings calculator

#### BudgetThresholdExceeded
- **Description**: Grocery spending exceeded budget
- **Triggered When**: Spending surpasses budget limit
- **Key Data**: Budget period, budget limit, actual spending, overage amount, overage date, user ID
- **Consumers**: Alert service, cost reduction advisor, meal plan optimizer

### SubstitutionEvents

#### IngredientSubstituted
- **Description**: Recipe ingredient replaced with alternative
- **Triggered When**: User swaps ingredient due to preference or availability
- **Key Data**: Recipe ID, original ingredient, substitute ingredient, substitution reason, nutrition impact, user ID
- **Consumers**: Recipe adjuster, nutrition recalculator, preference learner
