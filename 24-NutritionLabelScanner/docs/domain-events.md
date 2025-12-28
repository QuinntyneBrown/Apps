# Domain Events - Nutrition Label Scanner

## Overview
This document defines the domain events tracked in the Nutrition Label Scanner application. These events capture significant business occurrences related to food label scanning, nutrition data extraction, dietary tracking, and nutritional goal management.

## Events

### ScanEvents

#### NutritionLabelScanned
- **Description**: Food product label has been scanned
- **Triggered When**: User captures nutrition label photo with camera
- **Key Data**: Scan ID, image data, scan timestamp, scan quality, user ID
- **Consumers**: OCR processor, image storage, scan history tracker

#### LabelOCRProcessed
- **Description**: Text extraction from label image completed
- **Triggered When**: OCR analysis finishes processing label
- **Key Data**: Scan ID, extracted text, confidence scores, processing timestamp, user ID
- **Consumers**: Nutrition data parser, quality validator, manual review queue

#### NutritionDataExtracted
- **Description**: Nutritional information parsed from scanned label
- **Triggered When**: OCR text successfully converted to structured nutrition data
- **Key Data**: Product ID, calories, protein, carbs, fat, fiber, sodium, vitamins/minerals, serving size, user ID
- **Consumers**: Nutrition database, food tracker, dietary analyzer

#### ScanFailed
- **Description**: Label scan unsuccessful
- **Triggered When**: Image quality insufficient or OCR unable to extract data
- **Key Data**: Scan ID, failure reason, image quality score, retry suggestion, user ID
- **Consumers**: Error handler, user guidance, quality improvement analyzer

### ProductEvents

#### ProductIdentified
- **Description**: Food product recognized from label or barcode
- **Triggered When**: Product matched to existing database entry
- **Key Data**: Product ID, product name, brand, UPC/barcode, match confidence, user ID
- **Consumers**: Product database, quick lookup service, user favorites

#### NewProductAdded
- **Description**: Previously unknown product added to database
- **Triggered When**: Scanned product not in database and user confirms details
- **Key Data**: Product ID, product name, brand, nutrition data, added by user ID, timestamp
- **Consumers**: Product database, crowd-sourced data aggregator, product search index

#### ProductFavorited
- **Description**: Product marked as frequently consumed
- **Triggered When**: User saves product to favorites
- **Key Data**: Product ID, favorite date, consumption frequency, user ID
- **Consumers**: Quick access list, product recommender, shopping list suggester

#### ProductCompared
- **Description**: Multiple products compared side-by-side
- **Triggered When**: User compares nutrition of similar products
- **Key Data**: Product IDs, comparison metrics, healthier choice identified, comparison timestamp, user ID
- **Consumers**: Comparison visualizer, healthier alternative suggester, purchase decision support

### ConsumptionEvents

#### FoodItemLogged
- **Description**: Consumed food product recorded
- **Triggered When**: User logs eating scanned product
- **Key Data**: Log ID, product ID, serving size consumed, meal type, consumption timestamp, user ID
- **Consumers**: Daily nutrition aggregator, meal tracker, dietary goal checker

#### ServingSizeAdjusted
- **Description**: Portion size modified from standard serving
- **Triggered When**: User specifies different quantity than label serving
- **Key Data**: Log ID, product ID, label serving size, actual serving consumed, multiplier, user ID
- **Consumers**: Nutrition calculator, portion tracker, calorie counter

#### MealCompleted
- **Description**: All items in meal logged
- **Triggered When**: User finishes logging all foods for a meal
- **Key Data**: Meal ID, meal type, items logged, total nutrition, meal timestamp, user ID
- **Consumers**: Meal analyzer, nutrition summary generator, meal pattern tracker

### NutritionTrackingEvents

#### DailyNutritionCalculated
- **Description**: Total daily nutrition values computed
- **Triggered When**: User logs food or requests daily summary
- **Key Data**: Date, total calories, macros (protein/carbs/fat), micronutrients, fiber, sodium, user ID
- **Consumers**: Nutrition dashboard, goal checker, dietary analysis

#### MacroBalanceAssessed
- **Description**: Macronutrient distribution evaluated
- **Triggered When**: Daily or meal macros analyzed
- **Key Data**: Protein percentage, carb percentage, fat percentage, target ratios, variance, user ID
- **Consumers**: Macro dashboard, diet quality scorer, recommendation engine

#### MicronutrientDeficiencyDetected
- **Description**: Insufficient vitamin or mineral intake identified
- **Triggered When**: Tracked nutrients consistently below RDA
- **Key Data**: Nutrient type, current intake, recommended intake, deficiency severity, detection period, user ID
- **Consumers**: Alert service, supplementation recommender, dietary diversification advisor

### GoalEvents

#### NutritionGoalSet
- **Description**: Dietary target established
- **Triggered When**: User sets calorie, macro, or nutrient goal
- **Key Data**: Goal ID, goal type, target value, time period, goal start date, user ID, timestamp
- **Consumers**: Goal tracker, progress monitor, achievement checker

#### CalorieGoalReached
- **Description**: Daily calorie target achieved
- **Triggered When**: Logged calories within goal range
- **Key Data**: Date, goal calories, actual calories, variance, achievement timestamp, user ID
- **Consumers**: Achievement service, streak tracker, diet adherence monitor

#### ProteinGoalMet
- **Description**: Daily protein target achieved
- **Triggered When**: Protein intake meets or exceeds goal
- **Key Data**: Date, protein goal, actual protein, achievement timestamp, user ID
- **Consumers**: Achievement service, muscle nutrition tracker, athletic goal supporter

#### GoalExceeded
- **Description**: Nutrition target significantly surpassed
- **Triggered When**: Intake exceeds goal by substantial amount
- **Key Data**: Goal type, target amount, actual amount, excess percentage, user ID
- **Consumers**: Alert service, portion control advisor, goal adjustment suggester

### HealthAlertEvents

#### HighSodiumWarning
- **Description**: Excessive sodium intake detected
- **Triggered When**: Sodium exceeds daily recommended limit
- **Key Data**: Product ID or meal ID, sodium amount, daily total, warning level, user ID
- **Consumers**: Health alert service, product alternative suggester, cardiovascular health monitor

#### HighSugarWarning
- **Description**: Excessive sugar content identified
- **Triggered When**: Product or meal has high added sugar
- **Key Data**: Product ID, sugar amount, daily total, percentage of calories from sugar, user ID
- **Consumers**: Alert service, healthier alternative finder, diabetes prevention advisor

#### TransFatDetected
- **Description**: Trans fat found in product
- **Triggered When**: Label shows trans fat content
- **Key Data**: Product ID, trans fat amount, health impact, user ID
- **Consumers**: Health warning service, product avoidance recommender, heart health protector

#### AllergenIdentified
- **Description**: Allergen found in product ingredients
- **Triggered When**: Scanned label contains user's allergen
- **Key Data**: Product ID, allergen type, severity, user ID, timestamp
- **Consumers**: Allergen alert service, product rejection, safe alternative finder

### IngredientEvents

#### IngredientsExtracted
- **Description**: Ingredient list parsed from label
- **Triggered When**: OCR successfully extracts ingredients section
- **Key Data**: Product ID, ingredients list, ingredient order, additives identified, user ID
- **Consumers**: Ingredient database, allergen checker, additive analyzer

#### UndesirableIngredientDetected
- **Description**: Ingredient user wants to avoid found in product
- **Triggered When**: Product contains ingredient on user's avoid list
- **Key Data**: Product ID, ingredient name, avoidance reason, user ID
- **Consumers**: Product warning service, dietary preference enforcer, alternative suggester

#### CleanLabelVerified
- **Description**: Product confirmed to have minimal processing/additives
- **Triggered When**: Analysis shows product meets clean label criteria
- **Key Data**: Product ID, clean score, minimal ingredients, no artificial additives, user ID
- **Consumers**: Clean eating tracker, product recommender, quality badge assigner

### DietaryPreferenceEvents

#### DietaryRestrictionSet
- **Description**: Dietary limitation configured
- **Triggered When**: User sets vegan, gluten-free, kosher, etc. preference
- **Key Data**: Restriction ID, restriction type, strictness level, start date, user ID
- **Consumers**: Product filter, allergen checker, compliance monitor

#### ProductMatchesDiet
- **Description**: Scanned product compatible with dietary restrictions
- **Triggered When**: Product verified against user's dietary requirements
- **Key Data**: Product ID, diet type, compliance confirmed, user ID
- **Consumers**: Safe product list, dietary adherence tracker, product recommender

#### ProductViolatesDiet
- **Description**: Scanned product incompatible with dietary restrictions
- **Triggered When**: Product contains prohibited ingredients or nutrients
- **Key Data**: Product ID, diet type, violation reason, severity, user ID
- **Consumers**: Product rejection alert, alternative suggester, compliance tracker

### AnalysisEvents

#### NutrientDensityCalculated
- **Description**: Nutrient quality per calorie assessed
- **Triggered When**: Product nutrition analyzed for nutrient density
- **Key Data**: Product ID, nutrient density score, key nutrients, calories, user ID
- **Consumers**: Product quality ranker, healthier choice identifier, nutrition education

#### HealthScoreGenerated
- **Description**: Overall product healthiness rating calculated
- **Triggered When**: Product evaluated against health criteria
- **Key Data**: Product ID, health score (0-100), score factors, strengths/weaknesses, user ID
- **Consumers**: Product ranking, purchase decision aid, health-conscious shopping guide

#### ProcessingLevelDetermined
- **Description**: Food processing degree classified
- **Triggered When**: Product categorized (unprocessed, processed, ultra-processed)
- **Key Data**: Product ID, NOVA classification, processing indicators, user ID
- **Consumers**: Processing awareness tracker, whole food recommender, diet quality assessor

### HistoryEvents

#### ScanHistoryRecorded
- **Description**: Scanned product added to history
- **Triggered When**: Product successfully scanned and saved
- **Key Data**: Product ID, scan date, subsequent action (logged/compared/rejected), user ID
- **Consumers**: History tracker, pattern analyzer, frequently scanned identifier

#### ProductRescanned
- **Description**: Previously scanned product scanned again
- **Triggered When**: User scans product already in their history
- **Key Data**: Product ID, previous scan date, frequency count, user ID
- **Consumers**: Frequency tracker, staple food identifier, quick log suggester

### RecommendationEvents

#### HealthierAlternativeSuggested
- **Description**: Better nutritional option recommended
- **Triggered When**: System finds healthier version of scanned product
- **Key Data**: Original product ID, alternative product ID, improvement areas, nutritional comparison, user ID
- **Consumers**: Recommendation engine, smart shopping guide, health improvement facilitator

#### SimilarProductsFound
- **Description**: Related products identified
- **Triggered When**: User scans product and similar items available
- **Key Data**: Product ID, similar product IDs, similarity basis, user ID
- **Consumers**: Product discovery, comparison facilitator, choice expander

### BarcodeEvents

#### BarcodeScanned
- **Description**: Product barcode captured and decoded
- **Triggered When**: User scans product barcode/UPC
- **Key Data**: Barcode ID, barcode number, product lookup initiated, scan timestamp, user ID
- **Consumers**: Product database lookup, quick product identification, inventory tracker

#### BarcodeNotRecognized
- **Description**: Barcode not found in database
- **Triggered When**: Scanned barcode doesn't match any known products
- **Key Data**: Barcode number, scan timestamp, manual entry suggested, user ID
- **Consumers**: New product creation flow, database expansion, manual entry prompt
