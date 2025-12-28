# Domain Events - Classic Car Restoration Log

## Overview
This application tracks domain events related to classic car restoration projects, parts sourcing, work progress, expense tracking, and vehicle documentation. These events support project management, budget control, and restoration quality assurance.

## Events

### ProjectEvents

#### RestorationProjectStarted
- **Description**: Classic car restoration has been initiated
- **Triggered When**: User begins restoration of a vehicle
- **Key Data**: Project ID, vehicle make/model, year, VIN, acquisition date, purchase price, condition at start, restoration scope, target completion
- **Consumers**: Project tracker, budget planner, timeline estimator, documentation system, garage space allocator

#### ProjectScopesDefined
- **Description**: Restoration level and objectives have been established
- **Triggered When**: User determines extent of restoration work
- **Key Data**: Scope ID, project ID, restoration type (concours/restomod/driver), systems to restore, originality level, budget ceiling, quality targets
- **Consumers**: Work planner, budget allocator, parts strategy, timeline estimator, expectation setter

#### ProjectMilestoneReached
- **Description**: Major restoration phase has been completed
- **Triggered When**: Significant project stage finishes
- **Key Data**: Milestone ID, project ID, milestone name, completion date, quality assessment, photos, time spent, costs to date, remaining work
- **Consumers**: Progress tracker, timeline validator, motivation booster, documentation system, completion predictor

#### RestorationCompleted
- **Description**: Vehicle restoration has been finished
- **Triggered When**: All restoration work is complete
- **Key Data**: Completion ID, project ID, completion date, total time, total cost, final condition, restoration quality, before/after comparison, reveal event
- **Consumers**: Project archive, value assessment, showcase preparation, insurance updater, achievement tracker

#### ProjectAbandoned
- **Description**: Restoration project has been discontinued
- **Triggered When**: User stops work without completion
- **Key Data**: Abandonment ID, project ID, abandon date, progress at stop, reason, invested costs, salvageable parts, disposition plan
- **Consumers**: Project archive, loss calculator, parts recovery, learning tracker, future consideration

### WorkSessionEvents

#### WorkSessionStarted
- **Description**: Restoration work session has begun
- **Triggered When**: User starts working on vehicle
- **Key Data**: Session ID, project ID, start time, planned tasks, tools needed, parts to install, workspace setup, helper availability
- **Consumers**: Time tracker, task scheduler, tool organizer, progress monitor, work logger

#### TaskCompleted
- **Description**: Specific restoration task has been finished
- **Triggered When**: Work item is completed
- **Key Data**: Task ID, session ID, task description, completion time, quality result, challenges encountered, rework needed, documentation photos
- **Consumers**: Task tracker, progress monitor, quality assessor, time estimator, knowledge base

#### WorkSessionCompleted
- **Description**: Work session has concluded
- **Triggered When**: User finishes working for the day
- **Key Data**: Session ID, end time, duration, tasks completed, progress made, issues discovered, next session plans, fatigue level
- **Consumers**: Time aggregator, progress tracker, session analytics, planning assistant, work patterns analyzer

#### ProblemDiscovered
- **Description**: Unexpected issue or damage has been found
- **Triggered When**: Hidden problems are uncovered during work
- **Key Data**: Problem ID, project ID, discovery date, issue description, severity, cost impact, time impact, solution plan, common problem flag
- **Consumers**: Problem tracker, scope adjuster, budget impact, timeline reviser, knowledge base

#### ReworkRequired
- **Description**: Previously completed work must be redone
- **Triggered When**: Quality issues or errors necessitate redo
- **Key Data**: Rework ID, original task, rework reason, time impact, cost impact, lesson learned, prevention strategy, frustration level
- **Consumers**: Quality tracker, time impact analyzer, learning system, process improvement, patience tester

### PartsEvents

#### PartSourced
- **Description**: Needed restoration part has been located
- **Triggered When**: User finds source for required part
- **Key Data**: Part ID, part name/number, supplier, availability, price, condition (NOS/used/reproduction), shipping cost, lead time
- **Consumers**: Parts tracker, sourcing database, budget planner, timeline validator, supplier relationship

#### PartOrdered
- **Description**: Restoration part has been purchased
- **Triggered When**: User places order for part
- **Key Data**: Order ID, part ID, order date, supplier, cost, quantity, expected delivery, tracking number, order notes
- **Consumers**: Parts inventory, expense tracker, delivery monitor, budget updater, installation scheduler

#### PartReceived
- **Description**: Ordered part has arrived
- **Triggered When**: Part delivery is confirmed
- **Key Data**: Receipt ID, order ID, receipt date, condition on arrival, quality check result, correct part verification, damage claim needed
- **Consumers**: Inventory updater, quality validator, installation enabler, delivery tracker, supplier rating

#### PartInstalled
- **Description**: Part has been fitted to vehicle
- **Triggered When**: Component installation is completed
- **Key Data**: Installation ID, part ID, install date, installation difficulty, fit quality, functionality test, installation time, helper needed
- **Consumers**: Installation tracker, progress monitor, part validation, time estimator, assembly documentation

#### PartReturned
- **Description**: Part has been sent back to supplier
- **Triggered When**: Wrong or defective part is returned
- **Key Data**: Return ID, part ID, return date, return reason, refund/exchange status, restocking fee, timeline impact, supplier response
- **Consumers**: Returns tracker, budget adjuster, timeline impact, supplier evaluation, problem resolver

### ExpenseEvents

#### RestrationExpenseRecorded
- **Description**: Project cost has been logged
- **Triggered When**: Money is spent on restoration
- **Key Data**: Expense ID, project ID, amount, category (parts/labor/tools/supplies), date, vendor, receipt, budget category, necessity level
- **Consumers**: Budget tracker, expense categorizer, cost analyzer, financial reporting, overspend alerter

#### BudgetExceeded
- **Description**: Project spending has surpassed planned budget
- **Triggered When**: Cumulative costs exceed budget threshold
- **Key Data**: Alert ID, project ID, budgeted amount, actual spent, overage amount, overage percentage, continuation decision, funding plan
- **Consumers**: Budget alerter, financial decision support, scope reviser, funding planner, reality checker

#### ToolPurchased
- **Description**: New tool or equipment has been acquired
- **Triggered When**: User buys tool for restoration work
- **Key Data**: Tool ID, tool description, purchase date, cost, intended use, multi-project value, tool quality, storage location
- **Consumers**: Tool inventory, budget tracker, capability expander, investment justification, workshop organizer

#### LaborCostIncurred
- **Description**: Professional service has been paid for
- **Triggered When**: Outside labor or specialist work is hired
- **Key Data**: Labor ID, project ID, service type, provider, cost, labor date, quality result, necessity reason, DIY vs paid decision
- **Consumers**: Labor expense tracker, budget impact, skills gap identifier, quality assurance, cost-benefit analyzer

### DocumentationEvents

#### ProgressPhotoTaken
- **Description**: Photo documenting restoration progress has been captured
- **Triggered When**: User photographs work state
- **Key Data**: Photo ID, project ID, photo date, work stage, component shown, angle/view, quality, documentation purpose, sharing intent
- **Consumers**: Photo archive, progress visualizer, before/after compiler, documentation system, sharing platform

#### BeforeAfterPhotoSet
- **Description**: Comparison photos showing transformation have been created
- **Triggered When**: User captures pre and post restoration images
- **Key Data**: Photo set ID, project ID, before photo, after photo, component/area, transformation magnitude, emotional impact, showcase worthy
- **Consumers**: Visual progress tracker, showcase material, value demonstration, marketing content, satisfaction validator

#### VehicleHistoryDocumented
- **Description**: Car's provenance and history have been recorded
- **Triggered When**: User compiles vehicle background information
- **Key Data**: History ID, project ID, previous owners, production numbers, original specs, modifications history, documentation found, rarity factors
- **Consumers**: Authenticity verifier, value assessor, restoration guide, marketing story, historical archive

#### RestorationJournalEntryWritten
- **Description**: Written account of restoration experience has been created
- **Triggered When**: User documents thoughts and progress narratively
- **Key Data**: Entry ID, project ID, entry date, content, challenges discussed, victories celebrated, lessons learned, emotional journey
- **Consumers**: Journal archive, story builder, reflection tool, knowledge sharing, emotional processing

### QualityEvents

#### QualityInspectionPerformed
- **Description**: Restoration work quality has been assessed
- **Triggered When**: User evaluates workmanship standards
- **Key Data**: Inspection ID, project ID, inspection date, area inspected, quality rating, defects found, corrections needed, standards reference
- **Consumers**: Quality assurance, defect tracker, rework scheduler, pride level, show-readiness

#### ProfessionalAppraisalCompleted
- **Description**: Expert has evaluated restoration work
- **Triggered When**: Professional appraiser reviews vehicle
- **Key Data**: Appraisal ID, project ID, appraiser, appraisal date, condition rating, value estimate, strengths noted, improvements suggested
- **Consumers**: Value tracker, insurance documentation, quality validation, selling preparation, expert feedback

#### ShowJudgingReceived
- **Description**: Vehicle has been judged at car show
- **Triggered When**: Car is entered in judged event
- **Key Data**: Judging ID, project ID, show name, judging date, score received, award won, judge comments, category, peer comparison
- **Consumers**: Achievement tracker, quality benchmark, award collection, validation system, improvement insights

### ValueEvents

#### VehicleValueAssessed
- **Description**: Current market value has been estimated
- **Triggered When**: User researches or calculates vehicle worth
- **Key Data**: Valuation ID, project ID, assessment date, estimated value, comparable sales, value factors, investment recovery, appreciation potential
- **Consumers**: Value tracker, investment analyzer, insurance updater, selling consideration, restoration justification

#### InvestmentCalculated
- **Description**: Total financial investment has been computed
- **Triggered When**: User totals all restoration costs
- **Key Data**: Calculation ID, project ID, total invested, current value, ROI, profit/loss, time invested, investment decision validation
- **Consumers**: Financial analyzer, investment tracker, decision validator, passion vs profit, lesson learner

#### InsuranceUpdated
- **Description**: Vehicle insurance has been adjusted for restoration
- **Triggered When**: Insurance coverage is modified
- **Key Data**: Insurance ID, project ID, policy update date, coverage amount, premium, agreed value, policy type, coverage adequacy
- **Consumers**: Insurance manager, value protection, premium tracker, coverage validator, asset protection

### ShowAndSocialEvents

#### CarShowAttended
- **Description**: Restored vehicle has been displayed at show
- **Triggered When**: User exhibits car at event
- **Key Data**: Show ID, project ID, show name, show date, attendance, interest level, awards won, networking, feedback received
- **Consumers**: Show history, award tracker, social engagement, marketing opportunity, community participation

#### ClubMembershipJoined
- **Description**: User has joined classic car club
- **Triggered When**: User becomes member of enthusiast organization
- **Key Data**: Membership ID, club name, join date, membership fee, club benefits, networking opportunities, event access, expertise access
- **Consumers**: Community connector, resource access, event notifier, networking tracker, support system

#### RestorationStoryShared
- **Description**: Project experience has been shared with community
- **Triggered When**: User publishes restoration journey
- **Key Data**: Share ID, project ID, platform, share date, content shared, audience, engagement received, influence level, inspiration provided
- **Consumers**: Community contribution, influence tracker, knowledge sharing, brand building, inspiration spreader

### MaintenanceEvents

#### PostRestorationMaintenancePerformed
- **Description**: Upkeep work on restored vehicle has been completed
- **Triggered When**: User maintains finished restoration
- **Key Data**: Maintenance ID, project ID, maintenance type, date performed, cost, parts used, next maintenance due, condition preservation
- **Consumers**: Maintenance scheduler, preservation tracker, ongoing cost monitor, condition maintainer, value protector

#### DriveTestCompleted
- **Description**: Restored vehicle has been road tested
- **Triggered When**: User tests vehicle operation
- **Key Data**: Test ID, project ID, test date, distance driven, performance notes, issues found, adjustments needed, driving experience
- **Consumers**: Functionality validator, adjustment identifier, enjoyment measurer, safety checker, completion validator
