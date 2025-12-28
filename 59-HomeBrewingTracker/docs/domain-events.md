# Domain Events - Home Brewing Tracker

## Overview
This application tracks domain events related to home beer brewing, recipe development, batch production, fermentation monitoring, and tasting evaluations. These events support brewing mastery, quality control, and recipe refinement.

## Events

### RecipeEvents

#### RecipeCreated
- **Description**: A new brewing recipe has been developed
- **Triggered When**: User creates an original or adapted beer recipe
- **Key Data**: Recipe ID, beer name, style, target ABV, target IBU, grain bill, hop schedule, yeast strain, batch size, creation date, inspiration source
- **Consumers**: Recipe library, brew scheduler, ingredient calculator, cost estimator, recipe sharer

#### RecipeCloned
- **Description**: An existing recipe has been duplicated for modification
- **Triggered When**: User creates variant based on previous recipe
- **Key Data**: Clone ID, original recipe ID, new recipe ID, clone date, intended modifications, experimentation goal, variation type
- **Consumers**: Recipe version control, experimentation tracker, recipe evolution, comparison analyzer

#### RecipeScaled
- **Description**: Recipe has been adjusted for different batch size
- **Triggered When**: User scales recipe up or down
- **Key Data**: Scaling ID, original recipe ID, original batch size, new batch size, scaling factor, adjusted ingredients, efficiency adjustments
- **Consumers**: Ingredient calculator, equipment capacity checker, scaling validator, batch planner

### BrewDayEvents

#### BrewDayScheduled
- **Description**: A brewing session has been planned
- **Triggered When**: User schedules a brew day
- **Key Data**: Brew day ID, recipe ID, scheduled date, estimated duration, equipment checklist, ingredient checklist, preparation tasks
- **Consumers**: Calendar integration, reminder service, ingredient preparation, equipment readiness checker, time blocker

#### BrewSessionStarted
- **Description**: Brewing process has commenced
- **Triggered When**: User begins brew day activities
- **Key Data**: Session ID, recipe ID, start time, actual batch size, water chemistry, equipment used, brewing partners, weather conditions
- **Consumers**: Timer system, process tracker, temperature monitor, note taker, duration calculator

#### MashTemperatureRecorded
- **Description**: Mash temperature reading has been logged
- **Triggered When**: User records temperature during mashing
- **Key Data**: Reading ID, session ID, temperature, target temperature, time in mash, pH reading, temperature stability, adjustment needed
- **Consumers**: Temperature tracker, mash efficiency calculator, process optimizer, quality control, troubleshooter

#### GravityReadingTaken
- **Description**: Specific gravity measurement has been recorded
- **Triggered When**: User measures wort or beer gravity
- **Key Data**: Reading ID, session ID, gravity type (original/final), measured value, target value, temperature correction, sample date, ABV calculation
- **Consumers**: ABV calculator, fermentation tracker, efficiency monitor, completion estimator, quality validator

#### BrewSessionCompleted
- **Description**: Brew day has finished successfully
- **Triggered When**: Wort is transferred to fermenter and brewing is done
- **Key Data**: Session ID, completion time, total duration, actual OG, target OG, efficiency achieved, volume produced, notes, issues encountered
- **Consumers**: Brew log, efficiency tracker, recipe refinement, batch history, time analytics

### FermentationEvents

#### FermentationStarted
- **Description**: Beer has been pitched with yeast and fermentation initiated
- **Triggered When**: Wort is inoculated with yeast
- **Key Data**: Fermentation ID, session ID, yeast strain, pitch rate, pitch temperature, fermentation vessel, expected duration, fermentation type
- **Consumers**: Fermentation tracker, temperature monitor, timeline calculator, gravity check scheduler

#### FermentationTemperatureLogged
- **Description**: Fermentation temperature has been recorded
- **Triggered When**: User logs temperature during fermentation
- **Key Data**: Reading ID, fermentation ID, temperature, target range, ambient temperature, days into fermentation, temperature control method
- **Consumers**: Temperature profile tracker, fermentation health monitor, quality control, yeast performance analyzer

#### FermentationActivityObserved
- **Description**: Fermentation signs and activity have been documented
- **Triggered When**: User notes airlock activity, krausen, or other fermentation indicators
- **Key Data**: Observation ID, fermentation ID, observation date, activity level, visual appearance, airlock frequency, aroma notes, health indicators
- **Consumers**: Fermentation health tracker, timeline validator, problem detector, completion estimator

#### SecondaryFermentationInitiated
- **Description**: Beer has been transferred to secondary fermenter
- **Triggered When**: User racks beer to secondary vessel
- **Key Data**: Transfer ID, fermentation ID, transfer date, gravity at transfer, volume transferred, clarification status, conditioning plan
- **Consumers**: Process tracker, aging scheduler, gravity monitor, clarification tracker, bottling planner

#### FermentationCompleted
- **Description**: Fermentation has finished and beer is ready for packaging
- **Triggered When**: Final gravity is stable and fermentation is complete
- **Key Data**: Completion ID, fermentation ID, completion date, final gravity, fermentation duration, attenuation achieved, clarity, ready for packaging
- **Consumers**: Packaging scheduler, final ABV calculator, success validator, aging decision maker, recipe analyzer

### PackagingEvents

#### BottlingSessionStarted
- **Description**: Beer bottling process has begun
- **Triggered When**: User begins transferring beer to bottles
- **Key Data**: Bottling ID, fermentation ID, bottling date, bottle count, priming sugar amount, final gravity, expected carbonation, bottle types used
- **Consumers**: Inventory tracker, carbonation calculator, label printer, aging scheduler, bottle cap tracker

#### KeggingCompleted
- **Description**: Beer has been transferred to keg
- **Triggered When**: User fills and seals a keg
- **Key Data**: Kegging ID, fermentation ID, keg date, keg size, carbonation method, serving pressure, expected ready date, storage location
- **Consumers**: Keg inventory, carbonation scheduler, serving readiness tracker, tap list manager, gas supply monitor

#### CarbonationAchieved
- **Description**: Beer has reached target carbonation level
- **Triggered When**: Conditioning period completes or forced carbonation finishes
- **Key Data**: Carbonation ID, batch ID, target volumes CO2, achieved carbonation, method used, time to carbonate, carbonation quality
- **Consumers**: Ready-to-drink notifier, serving scheduler, quality validator, batch readiness tracker

#### LabelDesigned
- **Description**: Custom label has been created for batch
- **Triggered When**: User designs and applies labels to bottles or keg
- **Key Data**: Label ID, batch ID, design file, beer name, style, ABV, bottling date, special notes, artwork credits
- **Consumers**: Label printer, batch identifier, presentation enhancer, gift packaging, competition entry

### TastingAndEvaluationEvents

#### TastingSessionScheduled
- **Description**: Beer tasting evaluation has been planned
- **Triggered When**: User schedules formal or informal tasting
- **Key Data**: Tasting ID, batch ID, scheduled date, tasters invited, evaluation criteria, serving temperature, glassware, tasting context
- **Consumers**: Calendar integration, participant notification, evaluation form generator, serving preparation

#### BeerTasted
- **Description**: A beer has been sampled and evaluated
- **Triggered When**: User or tasters consume and assess the beer
- **Key Data**: Tasting ID, batch ID, tasting date, taster, scores (appearance/aroma/flavor/mouthfeel/overall), notes, food pairings, serving conditions
- **Consumers**: Tasting log, quality tracker, recipe feedback, score aggregator, improvement identifier

#### TastingNotesRecorded
- **Description**: Detailed sensory evaluation has been documented
- **Triggered When**: User writes comprehensive tasting notes
- **Key Data**: Notes ID, batch ID, appearance description, aroma profile, flavor notes, mouthfeel, finish, off-flavors detected, overall impression
- **Consumers**: Flavor profile database, recipe refinement, troubleshooting guide, comparison analyzer, sharing platform

#### BatchRated
- **Description**: Overall rating has been assigned to a batch
- **Triggered When**: User provides final assessment of batch quality
- **Key Data**: Rating ID, batch ID, overall score, success level, met expectations flag, would brew again, improvement notes, highlight aspects
- **Consumers**: Recipe success tracker, brew-again prioritizer, recipe archive decision, recommendation engine

### InventoryEvents

#### IngredientPurchased
- **Description**: Brewing ingredients have been acquired
- **Triggered When**: User buys grain, hops, yeast, or other ingredients
- **Key Data**: Purchase ID, ingredient type, quantity, supplier, purchase date, cost, freshness date, storage location, lot number
- **Consumers**: Inventory manager, cost tracker, freshness monitor, supplier history, recipe availability checker

#### IngredientInventoryUpdated
- **Description**: Ingredient stock levels have been adjusted
- **Triggered When**: Ingredients are used in brewing or inventory is counted
- **Key Data**: Update ID, ingredient ID, quantity change, new balance, update reason, update date, expiration check
- **Consumers**: Inventory system, reorder alert, recipe feasibility checker, cost calculator, waste tracker

#### IngredientExpired
- **Description**: An ingredient has passed its optimal use date
- **Triggered When**: System detects expired or stale ingredients
- **Key Data**: Expiration ID, ingredient ID, expiration date, quantity affected, disposal decision, replacement needed flag
- **Consumers**: Inventory cleanup, reorder system, quality control, cost tracker, freshness manager

### CollaborationAndSharingEvents

#### RecipeShared
- **Description**: A brewing recipe has been shared with others
- **Triggered When**: User shares recipe with homebrewing community or friends
- **Key Data**: Share ID, recipe ID, shared with, sharing platform, share date, share permissions, feedback received, clone count
- **Consumers**: Community platform, recipe distribution, popularity tracker, collaboration facilitator, influence measure

#### BrewingSessionCollaborative
- **Description**: Multiple brewers have participated in a brew session
- **Triggered When**: User brews with partners or brewing club
- **Key Data**: Collaboration ID, session ID, participants, role assignments, shared costs, shared output, collaboration notes
- **Consumers**: Collaboration tracker, cost splitter, batch ownership, social brewing analytics, partnership history

#### CompetitionEntrySubmitted
- **Description**: Beer has been entered into competition
- **Triggered When**: User submits batch for judging
- **Key Data**: Entry ID, batch ID, competition name, category, entry date, entry fee, judging date, bottle count submitted
- **Consumers**: Competition tracker, result awaiter, feedback collector, achievement system, recipe validator

#### CompetitionResultReceived
- **Description**: Judging results and feedback have been received
- **Triggered When**: Competition scores and judge comments are available
- **Key Data**: Result ID, entry ID, score, placement, medal/award, judge feedback, strengths noted, areas to improve, score sheets
- **Consumers**: Achievement tracker, recipe validation, improvement guide, celebration system, credibility builder
