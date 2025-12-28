# Domain Events - Home Energy Usage Tracker

## Overview
This document defines the domain events tracked in the Home Energy Usage Tracker application. These events capture significant business occurrences related to utility monitoring, energy consumption, cost tracking, efficiency improvements, and environmental impact.

## Events

### UtilityBillEvents

#### BillRecorded
- **Description**: Utility bill data has been logged
- **Triggered When**: User enters monthly bill information
- **Key Data**: Bill ID, utility type (electric/gas/water), billing period start, billing period end, usage amount, cost, due date, bill document
- **Consumers**: Cost tracker, usage analyzer, trend calculator, payment reminder, budget monitor

#### BillPaid
- **Description**: Utility payment has been completed
- **Triggered When**: User marks bill as paid or confirms payment
- **Key Data**: Bill ID, payment date, amount paid, payment method, confirmation number, autopay flag
- **Consumers**: Payment history, budget tracker, upcoming payment calculator, payment method analyzer

#### BillIncreaseDetected
- **Description**: Utility cost has risen significantly from previous period
- **Triggered When**: System detects bill amount increase above threshold
- **Key Data**: Bill ID, current amount, previous amount, increase percentage, usage change, rate change, seasonal factor
- **Consumers**: Alert service, usage investigation trigger, cost analysis, budget adjuster, efficiency review

#### UnusualUsageDetected
- **Description**: Energy consumption anomaly has been identified
- **Triggered When**: Usage significantly deviates from expected pattern
- **Key Data**: Bill ID, expected usage, actual usage, variance percentage, time period, potential causes, investigation needed
- **Consumers**: Alert system, leak detector, efficiency audit trigger, appliance inspection recommender

### UsageTrackingEvents

#### DailyUsageRecorded
- **Description**: Daily energy consumption has been logged
- **Triggered When**: Smart meter data imported or user manually enters daily reading
- **Key Data**: Usage ID, date, utility type, usage amount, cost estimate, weather conditions, occupancy level
- **Consumers**: Daily tracker, trend analyzer, goal monitor, real-time feedback, pattern detector

#### PeakUsageIdentified
- **Description**: High consumption period has been detected
- **Triggered When**: Usage spikes during specific time of day or week
- **Key Data**: Usage ID, peak time, usage amount, cost impact, contributing appliances, avoidability assessment
- **Consumers**: Peak reduction recommendations, time-of-use rate optimizer, behavior change suggestions, cost savings opportunities

#### BaselineUsageEstablished
- **Description**: Normal consumption baseline has been calculated
- **Triggered When**: System analyzes historical data to determine typical usage
- **Key Data**: Utility type, baseline amount, calculation period, seasonal adjustments, occupancy factors, baseline date
- **Consumers**: Comparison engine, anomaly detector, goal setter, efficiency measure evaluator

### GoalEvents

#### EnergyGoalSet
- **Description**: Consumption or cost reduction target has been established
- **Triggered When**: User defines energy saving goal
- **Key Data**: Goal ID, goal type, target reduction percentage or amount, target period, baseline, start date, motivation
- **Consumers**: Goal tracker, progress monitor, achievement detector, motivation system, recommendation engine

#### GoalMilestoneReached
- **Description**: Progress checkpoint toward energy goal has been achieved
- **Triggered When**: Cumulative savings reach percentage of goal (25%, 50%, 75%)
- **Key Data**: Goal ID, milestone percentage, achievement date, total savings so far, projected goal completion, actions working
- **Consumers**: Celebration trigger, motivation boost, progress report, community sharing, continued effort encouragement

#### GoalAchieved
- **Description**: Energy reduction target has been met
- **Triggered When**: Actual performance meets or exceeds goal
- **Key Data**: Goal ID, achievement date, target amount, actual amount, total savings, achievement method, duration to achieve
- **Consumers**: Achievement system, reward generator, success analysis, new goal suggester, celebration service

#### GoalMissed
- **Description**: Energy goal was not achieved by target date
- **Triggered When**: Goal period ends without reaching target
- **Key Data**: Goal ID, target amount, actual achievement, shortfall, end date, barriers identified, revised goal option
- **Consumers**: Analysis service, barrier identifier, goal adjuster, learning opportunity, motivation recalibration

### EfficiencyEvents

#### EfficiencyImprovementImplemented
- **Description**: Energy-saving measure has been put in place
- **Triggered When**: User completes efficiency upgrade or behavior change
- **Key Data**: Improvement ID, improvement type, implementation date, estimated savings, cost of improvement, payback period
- **Consumers**: Improvement tracker, savings calculator, ROI monitor, recommendation validator, upgrade history

#### SavingsMeasured
- **Description**: Impact of efficiency improvement has been quantified
- **Triggered When**: Sufficient time passed to measure improvement results
- **Key Data**: Improvement ID, measurement period, usage before, usage after, actual savings, cost savings, ROI achieved
- **Consumers**: Effectiveness validator, ROI calculator, recommendation refiner, success library, future improvement prioritizer

#### UpgradeRecommendationGenerated
- **Description**: System has suggested efficiency improvement opportunity
- **Triggered When**: Analysis identifies potential for energy savings
- **Key Data**: Recommendation ID, upgrade type, estimated savings, implementation cost, payback period, priority level, effort required
- **Consumers**: Recommendation queue, decision support, cost-benefit analyzer, implementation planner

#### WeatherNormalizedUsageCalculated
- **Description**: Energy use has been adjusted for weather variations
- **Triggered When**: System factors out weather impact to show true efficiency
- **Key Data**: Usage period, actual usage, weather-normalized usage, temperature data, heating/cooling degree days, efficiency score
- **Consumers**: Fair comparison, trend analysis, weather impact isolator, true efficiency measure

### ApplianceMonitoringEvents

#### ApplianceUsageTracked
- **Description**: Individual appliance energy consumption has been monitored
- **Triggered When**: Smart plug or meter tracks specific appliance
- **Key Data**: Appliance ID, tracking period, energy consumed, cost, usage hours, efficiency rating, contribution to total
- **Consumers**: Appliance cost calculator, efficiency ranker, upgrade candidate identifier, usage pattern analyzer

#### EnergyHogIdentified
- **Description**: High-consumption appliance has been flagged
- **Triggered When**: Appliance uses disproportionate amount of energy
- **Key Data**: Appliance ID, consumption amount, percentage of total usage, cost impact, age, upgrade savings potential
- **Consumers**: Replacement recommender, upgrade prioritizer, behavior change suggester, cost impact highlighter

#### ApplianceReplacementPlanned
- **Description**: Inefficient appliance scheduled for upgrade
- **Triggered When**: User plans to replace energy-wasting appliance
- **Key Data**: Appliance ID, planned replacement date, current consumption, expected savings, replacement cost, payback calculation
- **Consumers**: Purchase planner, savings forecaster, budget allocator, rebate finder, efficiency tracker

### CostAnalysisEvents

#### MonthlyComparisonGenerated
- **Description**: Current month usage compared to previous periods
- **Triggered When**: New bill entered or month ends
- **Key Data**: Current month, comparison months, usage variance, cost variance, weather adjustment, trend direction
- **Consumers**: Trend report, insight generator, alert trigger, budget forecaster

#### AnnualCostProjected
- **Description**: Yearly energy expense has been forecasted
- **Triggered When**: System projects full year cost based on trends
- **Key Data**: Projection date, projected annual cost, current pace, seasonal adjustments, efficiency factors, confidence level
- **Consumers**: Budget planner, financial forecaster, goal setter, improvement ROI calculator

#### CostSavingsRealized
- **Description**: Actual cost reduction has been achieved
- **Triggered When**: Bills show sustained cost decrease
- **Key Data**: Savings period, baseline cost, actual cost, total savings, savings percentage, contributing factors
- **Consumers**: Savings report, success validator, motivation system, ROI calculator, improvement effectiveness

### RateEvents

#### UtilityRateChanged
- **Description**: Energy price per unit has been updated
- **Triggered When**: Utility company changes rates or user updates rate info
- **Key Data**: Utility type, previous rate, new rate, effective date, rate structure (flat/tiered/time-of-use), rate change percentage
- **Consumers**: Cost calculator, bill projector, rate impact analyzer, usage time optimizer

#### TimeOfUseRateActivated
- **Description**: Time-based pricing structure has been enabled
- **Triggered When**: User switches to or updates TOU rate plan
- **Key Data**: Rate plan ID, peak hours, peak rate, off-peak hours, off-peak rate, effective date, potential savings
- **Consumers**: Usage time optimizer, shifting recommendations, savings calculator, behavior change prompter

#### TierThresholdApproached
- **Description**: Usage nearing higher price tier
- **Triggered When**: Tiered rate plan and usage approaching tier boundary
- **Key Data**: Current tier, usage so far, threshold amount, days remaining in period, overage cost, reduction needed
- **Consumers**: Alert service, conservation prompt, usage limiter suggestions, cost avoidance calculator

### EnvironmentalEvents

#### CarbonFootprintCalculated
- **Description**: Environmental impact of energy use has been computed
- **Triggered When**: Usage data converted to carbon emissions
- **Key Data**: Calculation period, energy consumed, carbon emissions, equivalent metrics (trees, cars), grid carbon intensity
- **Consumers**: Environmental dashboard, impact report, sustainability goals, carbon reduction tracker

#### RenewableEnergyContributionTracked
- **Description**: Solar or renewable generation has been measured
- **Triggered When**: Home solar or renewable system production recorded
- **Key Data**: Generation date, energy produced, energy consumed, net usage, grid export, offset value, environmental benefit
- **Consumers**: Solar dashboard, net metering tracker, savings calculator, environmental impact, ROI monitor

#### GreenEnergyMilestoneReached
- **Description**: Renewable energy achievement has been attained
- **Triggered When**: Solar generation or carbon reduction reaches milestone
- **Key Data**: Milestone type, achievement date, total impact, equivalent environmental benefits, celebration content
- **Consumers**: Achievement system, environmental impact reporter, motivation service, social sharing

### AlertEvents

#### HighUsageAlert
- **Description**: Real-time energy consumption exceeds threshold
- **Triggered When**: Current usage rate indicates bill will be high
- **Key Data**: Alert date, current usage rate, projected bill, threshold exceeded, recommended actions
- **Consumers**: Immediate notification, conservation suggestions, appliance check prompts, cost warning

#### BudgetExceededAlert
- **Description**: Energy spending has surpassed budget limit
- **Triggered When**: Cumulative costs exceed monthly budget
- **Key Data**: Budget period, budget amount, actual spending, overage amount, days remaining, reduction needed
- **Consumers**: Budget alert, spending review, conservation urgency, behavior change recommendations

#### LeakSuspected
- **Description**: Continuous usage pattern suggests possible leak
- **Triggered When**: Water or gas usage never drops to zero, indicating potential leak
- **Key Data**: Utility type, baseline usage, minimum usage detected, duration, estimated waste, investigation needed
- **Consumers**: Urgent alert, leak investigation prompt, professional inspection recommendation, potential damage warning
