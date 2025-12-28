# Domain Events - Car Modification & Parts Database

## Overview
This application tracks domain events related to automotive modifications, aftermarket parts installation, performance upgrades, customization projects, and mod documentation. These events support build planning, parts tracking, and modification history management.

## Events

### ModificationPlanningEvents

#### ModificationProjectCreated
- **Description**: A vehicle modification project has been initiated
- **Triggered When**: User plans to modify or upgrade their vehicle
- **Key Data**: Project ID, vehicle ID, project name, modification type (performance/aesthetic/functional), goal, budget, timeline, inspiration source
- **Consumers**: Project planner, budget allocator, timeline scheduler, parts identifier, goal tracker

#### BuildVisionDefined
- **Description**: Overall modification vision and theme has been established
- **Triggered When**: User defines the complete modification direction
- **Key Data**: Vision ID, project ID, theme description, target look/performance, inspiration vehicles, priority modifications, budget allocation, completion timeline
- **Consumers**: Vision keeper, coherence checker, priority setter, budget planner, progress measurer

#### ModificationPrioritized
- **Description**: Modification tasks have been ranked by importance
- **Triggered When**: User orders modifications by priority
- **Key Data**: Priority ID, project ID, modification list, priority order, dependency mapping, quick wins identified, budget sequence, timing considerations
- **Consumers**: Installation sequencer, budget phaser, dependency manager, timeline optimizer, progress tracker

### PartsEvents

#### PartResearched
- **Description**: Potential modification part has been investigated
- **Triggered When**: User researches aftermarket component
- **Key Data**: Research ID, part name, manufacturer, specifications, compatibility, reviews, price range, research date, alternatives considered
- **Consumers**: Parts database, decision support, comparison tool, knowledge base, vendor identifier

#### PartAdded
- **Description**: Aftermarket part has been added to modification database
- **Triggered When**: User catalogs a part for current or future installation
- **Key Data**: Part ID, part name, manufacturer, part number, category, vehicle compatibility, specifications, purchase info, install status
- **Consumers**: Parts inventory, compatibility checker, installation planner, budget tracker, project organizer

#### PartPurchased
- **Description**: Modification part has been acquired
- **Triggered When**: User buys aftermarket component
- **Key Data**: Purchase ID, part ID, purchase date, vendor, cost, shipping cost, expected delivery, warranty info, install priority
- **Consumers**: Parts tracker, expense logger, delivery monitor, installation scheduler, budget updater

#### PartReceived
- **Description**: Ordered part has arrived
- **Triggered When**: Part delivery is confirmed
- **Key Data**: Receipt ID, part ID, receipt date, condition on arrival, quality check, fitment verification, damage claim needed, ready to install
- **Consumers**: Inventory updater, quality validator, installation enabler, vendor rating, project progress

#### PartReturned
- **Description**: Part has been sent back to vendor
- **Triggered When**: Wrong, defective, or unwanted part is returned
- **Key Data**: Return ID, part ID, return date, return reason, refund/exchange status, restocking fee, replacement ordered, vendor response
- **Consumers**: Returns tracker, budget adjuster, project delay tracker, vendor evaluator, learning system

### InstallationEvents

#### PartInstalled
- **Description**: Modification part has been fitted to vehicle
- **Triggered When**: Installation of aftermarket component is completed
- **Key Data**: Install ID, part ID, vehicle ID, install date, installer (DIY/shop), install time, difficulty level, issues encountered, quality result
- **Consumers**: Installation log, project progress, time estimator, skill tracker, modification history

#### ProfessionalInstallationCompleted
- **Description**: Shop has installed modification
- **Triggered When**: Professional installation service is finished
- **Key Data**: Install ID, part ID, shop name, install date, labor cost, labor hours, quality rating, warranty, certification
- **Consumers**: Installation tracker, expense logger, shop evaluator, warranty manager, professional work database

#### DIYInstallationCompleted
- **Description**: User has installed part themselves
- **Triggered When**: Self-installation is finished
- **Key Data**: Install ID, part ID, install date, time spent, tools used, difficulty rating, mistakes made, would DIY again, tutorial followed
- **Consumers**: DIY tracker, skill development, time investment, learning database, capability assessment

#### InstallationChallengeEncountered
- **Description**: Unexpected difficulty during installation has occurred
- **Triggered When**: Problem arises during part installation
- **Key Data**: Challenge ID, install ID, problem description, solution found, time impact, additional parts needed, lesson learned, resolution
- **Consumers**: Problem database, learning system, time adjuster, future planning, tutorial improvement

#### ModificationReversed
- **Description**: Previously installed part has been removed
- **Triggered When**: User returns vehicle to stock or removes mod
- **Key Data**: Removal ID, part ID, removal date, removal reason, stock part reinstalled, mod part disposition, reversal difficulty, reversibility rating
- **Consumers**: Modification history, reversibility tracker, stock parts inventory, resale value consideration, preference learner

### PerformanceEvents

#### PerformanceGainMeasured
- **Description**: Modification's performance impact has been quantified
- **Triggered When**: User tests and measures performance improvement
- **Key Data**: Measurement ID, modification ID, metric type (HP/torque/0-60/quarter mile), before value, after value, gain, test date, test conditions
- **Consumers**: Performance tracker, mod effectiveness validator, dyno database, bench racing facts, ROI calculator

#### DynoTestCompleted
- **Description**: Dynamometer testing has been performed
- **Triggered When**: Vehicle is dyno tested for power measurement
- **Key Data**: Dyno ID, vehicle ID, test date, horsepower, torque, dyno type, test conditions, baseline comparison, dyno sheet, facility
- **Consumers**: Performance database, power tracker, modification validator, bragging rights, tuning reference

#### TrackDayPerformanceRecorded
- **Description**: Vehicle performance at track has been logged
- **Triggered When**: User tracks vehicle at racing circuit
- **Key Data**: Track session ID, vehicle ID, track name, session date, lap times, modifications at time, conditions, driver feedback, video
- **Consumers**: Track performance database, lap time progression, modification effectiveness, track history, driver development

#### FuelEconomyImpactAssessed
- **Description**: Modification's effect on fuel economy has been evaluated
- **Triggered When**: MPG change from modification is measured
- **Key Data**: Assessment ID, modification ID, MPG before, MPG after, change percentage, acceptable trade-off flag, date assessed
- **Consumers**: Economy impact tracker, trade-off evaluator, daily drivability assessor, cost consideration, modification selection

### AestheticEvents

#### VisualModificationCompleted
- **Description**: Appearance modification has been finished
- **Triggered When**: Aesthetic upgrade is installed
- **Key Data**: Mod ID, modification type (wheels/body kit/wrap/lights), completion date, cost, before/after photos, public reaction, satisfaction level
- **Consumers**: Visual build tracker, photo gallery, show preparation, crowd pleaser identifier, aesthetic evolution

#### VehicleWrapApplied
- **Description**: Vinyl wrap or paint protection film has been installed
- **Triggered When**: Wrap installation is completed
- **Key Data**: Wrap ID, vehicle ID, wrap type, color/design, installation date, installer, cost, warranty, expected lifespan, compliments received
- **Consumers**: Appearance tracker, expense log, warranty manager, uniqueness factor, resale consideration

#### CustomPaintCompleted
- **Description**: Custom paint job has been finished
- **Triggered When**: Vehicle painting is completed
- **Key Data**: Paint ID, vehicle ID, paint type, color code, painter, completion date, cost, curing time, paint quality, show-worthy flag
- **Consumers**: Aesthetic tracker, expense log, value impact, visual identity, show car qualifier

### TuningEvents

#### ECUTunePerformed
- **Description**: Engine computer has been tuned or flashed
- **Triggered When**: ECU programming is modified
- **Key Data**: Tune ID, vehicle ID, tuner, tune date, tune type (flash/piggyback), gains achieved, drivability impact, fuel requirements, cost
- **Consumers**: Tune database, performance tracker, warranty consideration, power delivery optimizer, tuner evaluator

#### CustomTuneDeveloped
- **Description**: Personalized ECU calibration has been created
- **Triggered When**: Dyno tuning session creates custom map
- **Key Data**: Custom tune ID, vehicle ID, tuner, dyno session date, maps created, power goals achieved, safety margins, revision count, final result
- **Consumers**: Custom tune archive, performance maximizer, engine protection, tuner expertise validator, ultimate optimization

#### TuneRevised
- **Description**: ECU tune has been updated or refined
- **Triggered When**: Tune is modified to fix issues or optimize further
- **Key Data**: Revision ID, original tune ID, revision date, revision reason, changes made, improvement measured, revision cost, tuner
- **Consumers**: Tune version control, optimization progress, problem resolution, continuous improvement, tuner relationship

### DocumentationEvents

#### ModificationPhotoTaken
- **Description**: Photo of modification has been captured
- **Triggered When**: User photographs installed part or modified vehicle
- **Key Data**: Photo ID, modification ID, photo date, photo angle, quality, before/after flag, sharing intent, portfolio worthy
- **Consumers**: Photo gallery, documentation archive, progress visualization, social sharing, build thread content

#### BuildThreadCreated
- **Description**: Online build documentation has been started
- **Triggered When**: User creates forum thread or blog for modifications
- **Key Data**: Thread ID, vehicle ID, platform, thread start date, initial post content, goals shared, following count, community engagement
- **Consumers**: Community engagement, documentation platform, feedback collection, fame tracker, knowledge sharing

#### ModificationVideoRecorded
- **Description**: Video of modification or vehicle has been created
- **Triggered When**: User records video content
- **Key Data**: Video ID, vehicle ID, video type (install/review/sound clip/drive), recording date, platform, views, engagement, production quality
- **Consumers**: Video library, social sharing, tutorial creation, sound documentation, view counter

### CommunityEvents

#### CarMeetAttended
- **Description**: User has participated in car gathering
- **Triggered When**: User takes modified vehicle to meet or show
- **Key Data**: Meet ID, vehicle ID, event name, meet date, location, attendance, awards won, networking, photos taken, feedback received
- **Consumers**: Meet history, social engagement, award collection, community participation, networking tracker

#### ModificationShowcased
- **Description**: Vehicle modification has been displayed
- **Triggered When**: User exhibits modified vehicle
- **Key Data**: Showcase ID, vehicle ID, event, showcase date, awards won, crowd reaction, feature requests, validation level, pride factor
- **Consumers**: Achievement tracker, award collection, validation, social proof, build confidence

#### ModificationRecommendationGiven
- **Description**: User has recommended specific modification to others
- **Triggered When**: User suggests mod to fellow enthusiasts
- **Key Data**: Recommendation ID, modification type, recommended to, recommendation reason, recipient response, installation by recipient
- **Consumers**: Influence tracker, community contribution, knowledge sharing, mod validation, helping others

### FinancialEvents

#### ModificationBudgetSet
- **Description**: Modification spending limit has been established
- **Triggered When**: User defines budget for modifications
- **Key Data**: Budget ID, vehicle ID, total budget, category allocations, timeframe, funding source, discipline commitment
- **Consumers**: Budget manager, expense controller, financial planning, overspend preventer, priority enforcer

#### TotalInvestmentCalculated
- **Description**: Cumulative modification costs have been totaled
- **Triggered When**: User calculates total invested in modifications
- **Key Data**: Calculation ID, vehicle ID, calculation date, total spent, parts cost, labor cost, cost breakdown, value impact, ownership total cost
- **Consumers**: Financial tracker, value impact assessor, insurance consideration, resale reality check, investment acknowledgment

#### ResaleValueImpactAssessed
- **Description**: Modifications' effect on vehicle value has been evaluated
- **Triggered When**: User considers how mods affect resale value
- **Key Data**: Assessment ID, vehicle ID, stock value, modified value estimate, value increase/decrease, marketability impact, buyer pool effect
- **Consumers**: Value assessor, selling strategy, mod selection influence, financial reality, personal vs financial value

### ComplianceEvents

#### EmissionsComplianceVerified
- **Description**: Modification legality for emissions has been confirmed
- **Triggered When**: User verifies modification meets emissions standards
- **Key Data**: Verification ID, modification ID, compliant status, jurisdiction, verification date, CARB status, inspection implications
- **Consumers**: Legal compliance, inspection readiness, legal risk manager, modification selection, street legality

#### InsuranceNotified
- **Description**: Insurance company has been informed of modifications
- **Triggered When**: User declares modifications to insurer
- **Key Data**: Notification ID, vehicle ID, modifications declared, notification date, premium impact, coverage adjustment, insurer response
- **Consumers**: Insurance compliance, coverage adequacy, premium tracker, legal protection, asset protection

#### WarrantyImpactAssessed
- **Description**: Modification's effect on warranty has been evaluated
- **Triggered When**: User considers warranty implications
- **Key Data**: Assessment ID, modification ID, warranty impact, manufacturer position, dealer relationship, risk acceptance, documentation
- **Consumers**: Warranty risk manager, modification decision influence, dealer relationship, legal preparedness, risk acceptance
