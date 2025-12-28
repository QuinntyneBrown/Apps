# Domain Events - BBQ & Grilling Recipe Book

## Overview
This application tracks domain events related to BBQ and grilling recipes, cooking sessions, recipe modifications, and culinary achievements. These events support skill development, recipe refinement, and grilling mastery.

## Events

### RecipeEvents

#### RecipeAdded
- **Description**: A new BBQ or grilling recipe has been added to the collection
- **Triggered When**: User creates or imports a new recipe
- **Key Data**: Recipe ID, name, protein type, cuisine style, difficulty level, cook time, prep time, servings, heat level, creation date, source
- **Consumers**: Recipe catalog, search indexer, meal planner, shopping list generator, recommendation engine

#### RecipeModified
- **Description**: An existing recipe has been updated or customized
- **Triggered When**: User edits recipe details, ingredients, or instructions
- **Key Data**: Recipe ID, modification date, changes made, version number, modification reason, modified by, original vs modified comparison
- **Consumers**: Version control, recipe optimizer, personalization tracker, experimentation log, recipe evolution analyzer

#### RecipeCategorized
- **Description**: Recipe has been organized into categories or tagged
- **Triggered When**: User assigns categories, tags, or classifications
- **Key Data**: Recipe ID, categories (meat type/cooking method/cuisine), tags, difficulty rating, occasion tags, seasonal flag, diet restrictions
- **Consumers**: Recipe organizer, search filter, meal planning, dietary preference manager, recommendation engine

#### RecipeFavorited
- **Description**: Recipe has been marked as a favorite or go-to recipe
- **Triggered When**: User designates recipe as particularly successful or preferred
- **Key Data**: Favorite ID, recipe ID, favorited date, reason, success rate, family approval rating, frequency cooked
- **Consumers**: Favorites collection, meal rotation planner, guest entertainment planner, recipe recommender

### CookingSessionEvents

#### CookingSessionStarted
- **Description**: A grilling or BBQ cooking session has begun
- **Triggered When**: User starts cooking a recipe
- **Key Data**: Session ID, recipe ID, start time, grill type used, fuel type, weather conditions, number of guests, occasion type
- **Consumers**: Timer system, session tracker, cooking log, temperature monitor, time analytics

#### TemperatureRecorded
- **Description**: A temperature reading has been logged during cooking
- **Triggered When**: User records grill or meat temperature
- **Key Data**: Reading ID, session ID, temperature value, temperature type (grill/meat/ambient), timestamp, target temperature, probe location
- **Consumers**: Temperature tracker, cooking analytics, doneness calculator, temperature history, safety monitor

#### RecipeCooked
- **Description**: A recipe has been prepared and cooked
- **Triggered When**: User completes cooking a recipe
- **Key Data**: Cooking ID, recipe ID, cook date, actual cook time, grill temperature, modifications made, portion size, success rating
- **Consumers**: Cooking history, recipe frequency tracker, skill development, modification analyzer, success rate calculator

#### CookingSessionCompleted
- **Description**: Grilling session has concluded
- **Triggered When**: User finishes cooking and cleanup
- **Key Data**: Session ID, end time, total duration, recipes cooked, fuel consumed, weather impact, cleanup time, overall satisfaction
- **Consumers**: Session analytics, cost tracker, efficiency analyzer, experience log, planning optimizer

#### RecipeFailureRecorded
- **Description**: A cooking attempt did not meet expectations
- **Triggered When**: User documents an unsuccessful cooking experience
- **Key Data**: Failure ID, recipe ID, failure date, issues encountered, suspected causes, temperature problems, timing issues, lessons learned
- **Consumers**: Learning tracker, recipe troubleshooter, technique improvement, problem pattern analyzer, retry planner

### RatingAndReviewEvents

#### RecipeRated
- **Description**: A cooked recipe has been rated
- **Triggered When**: User or guests provide rating after eating
- **Key Data**: Rating ID, recipe ID, overall score, taste rating, tenderness, smoke level, presentation, rater, rating date, would make again
- **Consumers**: Recipe ranking, favorites identifier, recommendation engine, success tracker, improvement analyzer

#### RecipeReviewWritten
- **Description**: Detailed feedback about a recipe has been documented
- **Triggered When**: User writes notes about cooking experience and results
- **Key Data**: Review ID, recipe ID, review text, modifications made, tips discovered, what worked well, what to improve, serving suggestions
- **Consumers**: Recipe knowledge base, cooking tips library, modification tracker, community sharing, personal reference

#### GuestFeedbackRecorded
- **Description**: Feedback from guests or family has been captured
- **Triggered When**: User logs reactions and comments from those who ate the food
- **Key Data**: Feedback ID, recipe ID, session ID, guest comments, requests for recipe, favorite aspects, suggested improvements, crowd pleaser flag
- **Consumers**: Recipe popularity tracker, entertaining planner, crowd favorites list, social cooking analytics

### PhotoDocumentationEvents

#### RecipePhotoAdded
- **Description**: A photo of prepared recipe has been added
- **Triggered When**: User uploads or captures photo of finished dish
- **Key Data**: Photo ID, recipe ID, session ID, photo timestamp, photo type (raw/cooking/plated), photo quality, photo tags, sharing permission
- **Consumers**: Recipe gallery, visual progress tracker, social sharing, recipe presentation, before/after comparison

#### ProgressPhotoTaken
- **Description**: Photo documenting cooking progress has been captured
- **Triggered When**: User photographs cooking stages or techniques
- **Key Data**: Photo ID, session ID, cooking stage, timestamp, temperature at time, technique demonstrated, learning purpose
- **Consumers**: Technique documentation, cooking timeline, educational content, process improvement, visual guide

#### BeforeAfterPhotoSet
- **Description**: Comparison photos showing cooking transformation have been created
- **Triggered When**: User captures both pre-cook and finished result photos
- **Key Data**: Photo set ID, recipe ID, session ID, before photo, after photo, transformation notes, time elapsed, technique showcased
- **Consumers**: Visual progress library, technique demonstration, social sharing, cooking achievement showcase

### IngredientAndSupplyEvents

#### ShoppingListGenerated
- **Description**: Ingredient shopping list has been created from recipe(s)
- **Triggered When**: User generates list for upcoming cooking session
- **Key Data**: List ID, recipe IDs, ingredients needed, quantities, estimated cost, store suggestions, priority items, substitution options
- **Consumers**: Shopping planner, budget estimator, inventory checker, store locator, meal prep coordinator

#### IngredientSubstitutionMade
- **Description**: An ingredient was substituted in a recipe
- **Triggered When**: User uses alternative ingredient during cooking
- **Key Data**: Substitution ID, recipe ID, session ID, original ingredient, substitute used, substitution reason, success rating, taste impact
- **Consumers**: Substitution database, recipe flexibility tracker, dietary accommodation, cost savings tracker, experimentation log

#### RubOrMarinadeCreated
- **Description**: A custom rub or marinade recipe has been developed
- **Triggered When**: User creates signature seasoning blend
- **Key Data**: Blend ID, name, ingredients, proportions, flavor profile, best used for, creation date, batch size, success rating
- **Consumers**: Rub library, recipe component, flavor profile builder, signature collection, sharing database

### SkillDevelopmentEvents

#### TechniqueAttempted
- **Description**: A new grilling technique has been tried
- **Triggered When**: User attempts unfamiliar cooking method
- **Key Data**: Technique ID, technique name, attempt date, recipe used, difficulty encountered, success level, tips learned, retry intention
- **Consumers**: Skill tracker, learning progress, technique library, difficulty assessor, improvement planner

#### SkillMilestoneAchieved
- **Description**: A grilling skill milestone has been reached
- **Triggered When**: User masters technique or achieves cooking milestone
- **Key Data**: Milestone ID, skill type, achievement date, evidence (photos/ratings), difficulty level, practice sessions required, mastery level
- **Consumers**: Achievement system, skill progression tracker, confidence builder, teaching potential identifier

#### CompetitionParticipated
- **Description**: User has participated in BBQ competition or cookoff
- **Triggered When**: User enters competitive grilling event
- **Key Data**: Competition ID, event name, date, category entered, recipe used, placement/awards, judges feedback, lessons learned
- **Consumers**: Competition history, achievement tracker, recipe validation, skill benchmark, motivation system

### EquipmentAndMaintenanceEvents

#### GrillMaintenancePerformed
- **Description**: Maintenance has been performed on grilling equipment
- **Triggered When**: User cleans, services, or maintains grill
- **Key Data**: Maintenance ID, grill ID, maintenance type, date performed, tasks completed, parts replaced, next maintenance due, condition notes
- **Consumers**: Maintenance scheduler, equipment tracker, performance optimizer, cost tracker, longevity monitor

#### EquipmentUpgradeAcquired
- **Description**: New grilling equipment or accessory has been obtained
- **Triggered When**: User adds new tool, grill, or accessory to collection
- **Key Data**: Equipment ID, item name, purchase date, cost, intended use, first use date, performance rating, value assessment
- **Consumers**: Equipment inventory, budget tracker, capability expander, recipe enabler, investment analyzer

#### FuelConsumptionTracked
- **Description**: Fuel usage for grilling session has been recorded
- **Triggered When**: User logs charcoal, wood, or gas consumption
- **Key Data**: Consumption ID, session ID, fuel type, amount used, cost, burn time, efficiency rating, weather impact
- **Consumers**: Cost calculator, efficiency tracker, fuel inventory, budget planner, session analytics
