# Domain Events - Woodworking Project Manager

## Overview
This application tracks domain events related to woodworking projects, material management, tool usage, build progress, and skill development. These events support project planning, quality tracking, and craftsmanship improvement.

## Events

### ProjectEvents

#### ProjectConceived
- **Description**: A new woodworking project idea has been created
- **Triggered When**: User documents a project concept or design
- **Key Data**: Project ID, name, project type (furniture/cabinet/decorative), difficulty level, inspiration source, concept date, sketches, intended recipient
- **Consumers**: Project queue, design library, feasibility analyzer, material estimator, skill requirement checker

#### ProjectPlanned
- **Description**: Detailed project plan has been developed
- **Triggered When**: User creates comprehensive project plan
- **Key Data**: Plan ID, project ID, design finalized, dimensions, material list, cut list, tool requirements, estimated cost, timeline, skill level needed
- **Consumers**: Material calculator, budget estimator, timeline planner, tool checker, preparation coordinator

#### ProjectStarted
- **Description**: Physical work on project has begun
- **Triggered When**: User commences construction or building
- **Key Data**: Start ID, project ID, start date, workspace setup, initial materials prepared, safety checks completed, build approach
- **Consumers**: Project tracker, time logger, progress monitor, workspace manager, active project list

#### ProjectMilestoneReached
- **Description**: Significant project phase has been completed
- **Triggered When**: Major component or build stage finishes
- **Key Data**: Milestone ID, project ID, milestone name, completion date, quality check result, photos, issues encountered, time spent
- **Consumers**: Progress tracker, timeline validator, quality monitor, documentation system, motivation tracker

#### ProjectCompleted
- **Description**: Woodworking project has been finished
- **Triggered When**: Final finishing and assembly are complete
- **Key Data**: Completion ID, project ID, completion date, total time spent, total cost, quality rating, photos, client/recipient satisfaction, lessons learned
- **Consumers**: Project archive, portfolio builder, time analytics, cost analyzer, skill development tracker

#### ProjectAbandoned
- **Description**: Project has been discontinued without completion
- **Triggered When**: User decides to stop working on project
- **Key Data**: Abandonment ID, project ID, abandon date, progress at abandonment, abandon reason, salvageable materials, lessons learned, retry potential
- **Consumers**: Project history, material recovery, learning tracker, failure analyzer, decision optimizer

### MaterialEvents

#### MaterialPurchased
- **Description**: Wood or materials have been acquired for projects
- **Triggered When**: User buys lumber, hardware, or finishing supplies
- **Key Data**: Purchase ID, material type, species (if wood), quantity, dimensions, grade, supplier, cost, purchase date, intended project, storage location
- **Consumers**: Material inventory, budget tracker, supplier history, project material allocator, stock level monitor

#### WoodMilled
- **Description**: Rough lumber has been dimensioned and prepared
- **Triggered When**: User processes wood to final dimensions
- **Key Data**: Milling ID, wood ID, original dimensions, final dimensions, waste generated, milling date, quality assessment, moisture content
- **Consumers**: Material efficiency tracker, waste monitor, dimension validator, yield calculator, wood preparation log

#### MaterialAllocatedToProject
- **Description**: Materials have been assigned to specific project
- **Triggered When**: User designates inventory for a build
- **Key Data**: Allocation ID, project ID, material IDs, quantity allocated, allocation date, cost allocation, material preparation needed
- **Consumers**: Project material tracker, inventory updater, budget allocator, availability checker, cut list generator

#### MaterialWasted
- **Description**: Material has been damaged or wasted
- **Triggered When**: Cutting error, damage, or unusable material occurs
- **Key Data**: Waste ID, project ID, material type, quantity wasted, waste reason, cost impact, lesson learned, preventable flag
- **Consumers**: Waste tracker, cost impact analyzer, learning system, efficiency monitor, process improvement

#### ScrapMaterialReclaimed
- **Description**: Leftover material has been saved for future use
- **Triggered When**: User stores usable scrap pieces
- **Key Data**: Scrap ID, material type, dimensions, quantity, storage date, potential uses, condition, storage location
- **Consumers**: Scrap inventory, small project planner, waste reduction tracker, material optimizer

### ToolAndEquipmentEvents

#### ToolPurchased
- **Description**: New woodworking tool or equipment has been acquired
- **Triggered When**: User buys or receives a tool
- **Key Data**: Tool ID, tool type, brand/model, purchase date, cost, capabilities, safety features, warranty info, storage location
- **Consumers**: Tool inventory, budget tracker, capability expander, maintenance scheduler, insurance records

#### ToolUsed
- **Description**: A tool has been utilized in project work
- **Triggered When**: User employs a tool during building
- **Key Data**: Usage ID, tool ID, project ID, usage date, operation type, usage duration, performance notes, safety incidents
- **Consumers**: Tool usage tracker, maintenance predictor, skill development, safety monitor, efficiency analyzer

#### ToolMaintenancePerformed
- **Description**: Tool servicing or maintenance has been completed
- **Triggered When**: User cleans, sharpens, calibrates, or repairs tool
- **Key Data**: Maintenance ID, tool ID, maintenance type, date performed, tasks completed, parts replaced, performance improvement, next service due
- **Consumers**: Maintenance scheduler, tool condition tracker, cost tracker, performance optimizer, longevity monitor

#### BladeOrBitSharpened
- **Description**: Cutting tool has been sharpened
- **Triggered When**: User sharpens saw blade, chisel, plane iron, or router bit
- **Key Data**: Sharpening ID, tool ID, sharpen date, sharpening method, edge quality before/after, time spent, sharpness test result
- **Consumers**: Sharpening tracker, edge quality monitor, maintenance scheduler, cut quality optimizer, tool performance

#### ToolBreakdownOccurred
- **Description**: Tool has failed or broken during use
- **Triggered When**: Equipment malfunction or damage happens
- **Key Data**: Breakdown ID, tool ID, failure date, failure type, project impact, repair cost, downtime, replacement needed, cause analysis
- **Consumers**: Reliability tracker, repair scheduler, project delay manager, budget impact, tool replacement planner

### BuildProcessEvents

#### CutsMade
- **Description**: Wood cutting operations have been performed
- **Triggered When**: User executes cuts according to cut list
- **Key Data**: Cut session ID, project ID, pieces cut, cut accuracy, tool used, cut type, waste generated, safety incidents, rework needed
- **Consumers**: Progress tracker, accuracy monitor, cut list validator, waste analyzer, skill assessment

#### JoineryExecuted
- **Description**: Joinery methods have been applied
- **Triggered When**: User creates joints to connect wood pieces
- **Key Data**: Joinery ID, project ID, joint type, pieces joined, fit quality, glue used, clamping method, dry fit result, final assembly result
- **Consumers**: Quality tracker, technique library, skill development, structural integrity monitor, time estimator

#### AssemblyCompleted
- **Description**: Project components have been assembled
- **Triggered When**: Major assembly or glue-up session finishes
- **Key Data**: Assembly ID, project ID, assembly date, components assembled, adhesives used, clamp time, square/alignment check, issues resolved
- **Consumers**: Progress tracker, quality validator, timeline updater, photo documentation, process logger

#### FinishingApplied
- **Description**: Finish or protective coating has been applied
- **Triggered When**: User applies stain, paint, oil, or other finish
- **Key Data**: Finishing ID, project ID, finish type, application method, coats applied, drying time, finish quality, environmental conditions
- **Consumers**: Finish tracker, quality monitor, timeline manager, material usage, environmental condition logger

### QualityEvents

#### QualityInspectionPerformed
- **Description**: Project quality has been assessed
- **Triggered When**: User evaluates workmanship and standards
- **Key Data**: Inspection ID, project ID, inspection date, quality score, defects found, measurements accuracy, finish quality, structural soundness
- **Consumers**: Quality management, defect tracker, skill assessment, client readiness, improvement identifier

#### DefectIdentified
- **Description**: A flaw or issue has been discovered
- **Triggered When**: User finds mistake, gap, tear-out, or other defect
- **Key Data**: Defect ID, project ID, defect type, severity, location, discovery date, cause identified, repair plan, preventable flag
- **Consumers**: Quality tracker, repair planner, learning system, cost impact, process improvement

#### ReworkRequired
- **Description**: Work must be redone or corrected
- **Triggered When**: Defect or error necessitates redoing steps
- **Key Data**: Rework ID, project ID, reason for rework, scope of rework, time impact, material impact, lesson learned, prevention strategy
- **Consumers**: Time tracker, cost impact analyzer, learning system, quality improvement, process refinement

#### QualityStandardMet
- **Description**: Project has achieved quality benchmarks
- **Triggered When**: Inspection confirms meeting quality criteria
- **Key Data**: Achievement ID, project ID, standards met, quality level achieved, client satisfaction, pride level, portfolio worthy flag
- **Consumers**: Portfolio selector, quality tracker, skill validation, client communication, reputation builder

### SkillDevelopmentEvents

#### NewTechniqueAttempted
- **Description**: Unfamiliar woodworking method has been tried
- **Triggered When**: User experiments with new technique
- **Key Data**: Technique ID, technique name, project ID, attempt date, success level, difficulty encountered, resources used, continue using flag
- **Consumers**: Skill tracker, technique library, learning progress, confidence builder, project capability expander

#### SkillMasteryAchieved
- **Description**: Proficiency in a technique has been attained
- **Triggered When**: User demonstrates consistent success with skill
- **Key Data**: Mastery ID, skill name, achievement date, evidence projects, practice time, quality level, teaching ability, complexity handled
- **Consumers**: Skill inventory, project capability, teaching opportunities, confidence tracker, expertise recognition

#### MistakeLearned
- **Description**: An error has been analyzed for learning
- **Triggered When**: User documents mistake and prevention strategy
- **Key Data**: Lesson ID, project ID, mistake description, impact, root cause, prevention method, lesson learned, application to future projects
- **Consumers**: Knowledge base, error prevention, skill improvement, teaching material, wisdom accumulator

### ClientAndDeliveryEvents

#### CustomOrderReceived
- **Description**: A client has requested a custom project
- **Triggered When**: User accepts commission or custom build request
- **Key Data**: Order ID, client name, project description, specifications, budget, deadline, deposit amount, special requirements, contract terms
- **Consumers**: Project queue, timeline planner, contract manager, budget planner, client communication

#### ClientFeedbackReceived
- **Description**: Customer has provided feedback on delivered project
- **Triggered When**: Client reviews completed work
- **Key Data**: Feedback ID, project ID, client rating, comments, satisfaction level, would recommend flag, referral potential, testimonial permission
- **Consumers**: Reputation tracker, portfolio builder, improvement identifier, testimonial collection, client relationship

#### ProjectDelivered
- **Description**: Completed project has been delivered to client or recipient
- **Triggered When**: User hands over finished work
- **Key Data**: Delivery ID, project ID, delivery date, recipient, condition on delivery, payment received, care instructions provided, warranty offered
- **Consumers**: Delivery tracker, payment processor, project closure, warranty tracker, client satisfaction monitor
