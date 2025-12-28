# Domain Events - Fuel Economy Tracker

## Overview
This application tracks domain events related to fuel consumption, mileage tracking, refueling history, driving efficiency, and fuel cost management. These events support fuel economy optimization, expense tracking, and driving habit improvement.

## Events

### FuelPurchaseEvents

#### FuelPurchased
- **Description**: Vehicle has been refueled
- **Triggered When**: User fills up gas tank
- **Key Data**: Fill-up ID, vehicle ID, date, gallons purchased, cost per gallon, total cost, odometer reading, fuel grade, station location, payment method
- **Consumers**: Fuel log, expense tracker, economy calculator, cost analyzer, station preference tracker

#### PartialFillUpRecorded
- **Description**: Incomplete tank refill has been logged
- **Triggered When**: User adds fuel without filling tank
- **Key Data**: Partial fill ID, vehicle ID, date, gallons added, cost, odometer reading, reason for partial fill, tank level before/after
- **Consumers**: Fuel tracker, expense log, calculation adjuster, filling pattern analyzer, tank management

#### FuelStationRated
- **Description**: Gas station has been evaluated
- **Triggered When**: User rates refueling location
- **Key Data**: Rating ID, station name, location, rating date, price competitiveness, cleanliness, amenities, service, would return flag
- **Consumers**: Station database, station selector, rating aggregator, route planner, preference tracker

#### FuelPriceTracked
- **Description**: Local fuel price has been recorded
- **Triggered When**: User notes current fuel prices
- **Key Data**: Price record ID, date, location, regular price, premium price, diesel price, price trend, historical comparison, station
- **Consumers**: Price tracker, savings opportunity identifier, budgeter, price alert system, historical analyzer

### EconomyCalculationEvents

#### FuelEconomyCalculated
- **Description**: Miles per gallon has been computed
- **Triggered When**: System calculates MPG from fill-up data
- **Key Data**: Calculation ID, vehicle ID, MPG result, miles driven, gallons used, calculation date, trip type, weather conditions, driving conditions
- **Consumers**: Economy dashboard, trend analyzer, efficiency tracker, driving feedback, goal monitor

#### RunningAverageUpdated
- **Description**: Average fuel economy over time has been refreshed
- **Triggered When**: New fill-up updates rolling average
- **Key Data**: Average ID, vehicle ID, update date, current average MPG, previous average, time period, trend direction, EPA comparison
- **Consumers**: Performance tracker, trend visualizer, efficiency monitor, baseline setter, improvement measurer

#### PersonalBestMPGAchieved
- **Description**: Highest fuel economy record has been set
- **Triggered When**: Fill-up calculation exceeds previous best
- **Key Data**: Record ID, vehicle ID, MPG achieved, date achieved, trip details, driving conditions, techniques used, beat previous by
- **Consumers**: Achievement tracker, record keeper, motivation booster, technique validator, celebration trigger

#### FuelEconomyDeclined
- **Description**: Fuel efficiency has dropped significantly
- **Triggered When**: MPG falls below threshold or trend
- **Key Data**: Decline ID, vehicle ID, current MPG, expected MPG, decline percentage, potential causes, investigation needed flag, alert date
- **Consumers**: Alert system, problem detector, maintenance trigger, driving habit reviewer, diagnostic scheduler

### TripTrackingEvents

#### TripStarted
- **Description**: Driving trip has begun
- **Triggered When**: User starts tracking a trip
- **Key Data**: Trip ID, vehicle ID, start time, starting odometer, starting fuel level, trip purpose, route planned, weather conditions
- **Consumers**: Trip tracker, real-time monitor, route logger, purpose categorizer, condition documenter

#### TripCompleted
- **Description**: Driving trip has concluded
- **Triggered When**: User ends trip tracking
- **Key Data**: Trip ID, end time, ending odometer, miles driven, trip duration, average speed, trip MPG estimate, trip purpose, driving conditions
- **Consumers**: Trip log, mileage aggregator, economy estimator, trip categorizer, pattern analyzer

#### LongDistanceTripLogged
- **Description**: Extended highway trip has been recorded
- **Triggered When**: User completes trip over distance threshold
- **Key Data**: Trip ID, vehicle ID, distance, highway percentage, trip MPG, fuel cost, date, rest stops, highway vs city economy comparison
- **Consumers**: Highway economy tracker, trip analyzer, long-distance efficiency, route optimizer, cost calculator

#### CityDrivingSessionTracked
- **Description**: Urban driving period has been logged
- **Triggered When**: User records city driving
- **Key Data**: Session ID, vehicle ID, date, miles driven, traffic level, stop-and-go frequency, city MPG, comparison to highway, frustration level
- **Consumers**: City economy tracker, driving condition analyzer, economy comparison, traffic impact assessor

### CostAnalysisEvents

#### MonthlyFuelCostCalculated
- **Description**: Monthly fuel expenses have been totaled
- **Triggered When**: Month ends or user requests calculation
- **Key Data**: Calculation ID, vehicle ID, month/year, total spent, gallons purchased, average price paid, miles driven, cost per mile
- **Consumers**: Budget tracker, expense analyzer, monthly report, trend identifier, financial planning

#### AnnualFuelCostProjected
- **Description**: Yearly fuel expense estimate has been generated
- **Triggered When**: User projects annual costs
- **Key Data**: Projection ID, vehicle ID, projection date, estimated annual cost, based on current driving, price assumptions, mileage assumptions
- **Consumers**: Budget planner, financial forecaster, vehicle cost evaluator, affordability assessor, alternative fuel consideration

#### FuelBudgetSet
- **Description**: Monthly or annual fuel budget has been established
- **Triggered When**: User sets spending limit for fuel
- **Key Data**: Budget ID, vehicle ID, budget amount, budget period, budget start date, allowance per fill-up, overspend threshold
- **Consumers**: Budget manager, expense monitor, alert system, spending discipline, financial planning

#### BudgetThresholdReached
- **Description**: Fuel spending has approached budget limit
- **Triggered When**: Cumulative costs near or exceed budget
- **Key Data**: Alert ID, vehicle ID, budget amount, current spending, remaining budget, threshold percentage, period, alert date
- **Consumers**: Alert system, spending awareness, budget adjuster, driving reduction consideration, cost control

### EfficiencyOptimizationEvents

#### DrivingHabitAnalyzed
- **Description**: Driving behavior impact on economy has been evaluated
- **Triggered When**: System analyzes driving patterns
- **Key Data**: Analysis ID, vehicle ID, analysis date, aggressive driving indicators, speed patterns, acceleration habits, efficiency impact, improvement suggestions
- **Consumers**: Behavior analyzer, coaching system, efficiency improver, feedback provider, habit modifier

#### EcoDrivingGoalSet
- **Description**: Fuel economy improvement target has been established
- **Triggered When**: User sets MPG improvement goal
- **Key Data**: Goal ID, vehicle ID, target MPG, current baseline, goal timeframe, motivation, tracking method, reward plan
- **Consumers**: Goal tracker, progress monitor, motivation system, achievement anticipator, behavior modifier

#### EcoDrivingTipApplied
- **Description**: Fuel-saving technique has been implemented
- **Triggered When**: User tries efficiency-improving method
- **Key Data**: Tip ID, vehicle ID, technique description, application date, observed impact, continued use decision, effectiveness rating
- **Consumers**: Technique library, effectiveness tracker, tip recommender, learning system, efficiency optimizer

#### RouteOptimizationPerformed
- **Description**: More fuel-efficient route has been identified
- **Triggered When**: User finds better route for regular trip
- **Key Data**: Optimization ID, route description, old route MPG, new route MPG, fuel savings, time difference, optimization date, route details
- **Consumers**: Route library, efficiency improver, commute optimizer, savings calculator, smart routing

### MaintenanceImpactEvents

#### MaintenanceImpactedEconomy
- **Description**: Vehicle service has affected fuel efficiency
- **Triggered When**: Maintenance correlates with MPG change
- **Key Data**: Impact ID, vehicle ID, service type, service date, MPG before, MPG after, improvement percentage, maintenance cost, ROI
- **Consumers**: Maintenance effectiveness tracker, service validator, economy optimizer, maintenance prioritizer, cost-benefit analyzer

#### TireInflationCorrected
- **Description**: Tire pressure has been adjusted to proper level
- **Triggered When**: User corrects tire pressure
- **Key Data**: Inflation ID, vehicle ID, correction date, pressure before, pressure after, temperature, expected economy improvement, actual improvement
- **Consumers**: Tire pressure tracker, economy improver, maintenance reminder, simple fix validator, pressure monitoring

#### AirFilterReplaced
- **Description**: Engine air filter has been changed
- **Triggered When**: Air filter replacement is completed
- **Key Data**: Replacement ID, vehicle ID, replacement date, odometer reading, filter condition, cost, expected MPG improvement, actual improvement measured
- **Consumers**: Maintenance tracker, economy impact assessor, service scheduler, improvement validator, maintenance value

### SeasonalAndEnvironmentalEvents

#### SeasonalEconomyPatternIdentified
- **Description**: Seasonal fuel efficiency trend has been detected
- **Triggered When**: Analysis reveals seasonal MPG patterns
- **Key Data**: Pattern ID, vehicle ID, season, average MPG, variance from annual, contributing factors (weather/traffic/trips), pattern strength
- **Consumers**: Seasonal analyzer, expectation setter, pattern library, budget adjuster, condition correlator

#### WeatherImpactRecorded
- **Description**: Weather conditions' effect on economy has been logged
- **Triggered When**: User notes weather impact on MPG
- **Key Data**: Impact ID, vehicle ID, date, weather conditions, temperature, MPG impact, driving adjustments made, severity
- **Consumers**: Weather impact tracker, seasonal analyzer, expectation manager, condition database, climate correlator

#### EthanolBlendImpactAssessed
- **Description**: Different fuel blend's effect on economy has been measured
- **Triggered When**: User compares MPG with different ethanol content
- **Key Data**: Assessment ID, vehicle ID, fuel type, ethanol percentage, MPG result, cost difference, date, preference decision
- **Consumers**: Fuel type analyzer, blend preference, economy comparison, cost-benefit calculator, fuel selector

### VehicleComparisonEvents

#### VehicleEconomyCompared
- **Description**: Multiple vehicles' fuel efficiency has been compared
- **Triggered When**: User compares MPG across vehicles
- **Key Data**: Comparison ID, vehicle IDs, comparison date, MPG values, cost comparison, usage patterns, efficiency winner, practical considerations
- **Consumers**: Fleet analyzer, vehicle selector, upgrade consideration, cost evaluator, decision support

#### EPARatingComparison
- **Description**: Actual MPG has been compared to EPA estimates
- **Triggered When**: User evaluates real-world vs rated economy
- **Key Data**: Comparison ID, vehicle ID, EPA city/highway/combined, actual achieved, variance, driving conditions, expectations met flag
- **Consumers**: Reality checker, vehicle evaluator, manufacturer accountability, realistic expectation setter, future purchase consideration

### AlertsAndNotificationsEvents

#### FuelPriceSpikeDetected
- **Description**: Significant fuel price increase has been identified
- **Triggered When**: Price jump exceeds threshold
- **Key Data**: Alert ID, location, price increase, spike date, new price, historical comparison, fill-up timing suggestion, alert severity
- **Consumers**: Price alert system, timing advisor, budget impact notifier, fill-up scheduler, cost awareness

#### LowFuelWarning
- **Description**: Fuel level has reached low threshold
- **Triggered When**: Tank level drops below warning point
- **Key Data**: Warning ID, vehicle ID, current fuel level, estimated range, warning date, nearby stations, price comparison, urgency level
- **Consumers**: Refueling reminder, station finder, range anxiety preventer, planning assistant, safety alert

#### MileageGoalReached
- **Description**: Driving distance milestone has been achieved
- **Triggered When**: Odometer reaches significant mileage
- **Key Data**: Milestone ID, vehicle ID, mileage achieved, date, total fuel consumed, average MPG over period, cost to reach milestone, celebration tier
- **Consumers**: Achievement tracker, milestone celebrator, vehicle history marker, statistics generator, documentation system

### ReportingEvents

#### WeeklyEconomyReportGenerated
- **Description**: Weekly fuel efficiency summary has been created
- **Triggered When**: Week ends or user requests report
- **Key Data**: Report ID, vehicle ID, week dates, average MPG, total fuel cost, miles driven, best/worst fill-up, trends, week-over-week comparison
- **Consumers**: Performance dashboard, trend tracker, user feedback, behavior reinforcement, progress visibility

#### AnnualFuelReportCompiled
- **Description**: Yearly fuel consumption and cost summary has been produced
- **Triggered When**: Year ends or user requests annual review
- **Key Data**: Report ID, vehicle ID, year, total spent, total gallons, total miles, average MPG, trends, achievements, cost per mile, year-over-year
- **Consumers**: Annual review, tax documentation, vehicle cost assessment, achievement showcase, historical archive
