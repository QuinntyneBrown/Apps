# Nutrition Management - Backend Requirements

## Overview
The Nutrition Management feature enables pet owners to track feeding schedules, monitor dietary intake, manage food brands, and report dietary issues for their pets with automated feeding reminders and nutrition monitoring.

## Domain Events

### 1. FoodBrandChanged
Triggered when a pet's food brand is switched to a new brand.

**Event Properties:**
- `FoodBrandChangeId` (Guid): Unique identifier for the food brand change
- `PetId` (Guid): Pet whose food is being changed
- `ChangedDate` (DateTime): When the food brand was changed
- `PreviousFoodBrand` (string): Name of the previous food brand
- `NewFoodBrand` (string): Name of the new food brand
- `PreviousFoodType` (string): Type of previous food (e.g., "Dry", "Wet", "Raw", "Homemade")
- `NewFoodType` (string): Type of new food
- `ReasonForChange` (string): Reason for changing food brand
- `ReasonCategory` (string): "VeterinaryRecommendation", "AllergicReaction", "PreferenceChange", "Availability", "Cost", "Other"
- `TransitionPlan` (string): Plan for transitioning between foods
- `TransitionDurationDays` (int): Number of days for gradual transition
- `VeterinarianApproved` (bool): Whether change was approved by veterinarian
- `Notes` (string): Additional notes about the change
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- ChangedDate must be <= current date
- NewFoodBrand must be different from PreviousFoodBrand
- TransitionDurationDays should be >= 7 for gradual transition
- Should track food brand history
- Must validate food brand compatibility with pet type

**Triggered By:**
- Owner switching pet's food brand
- Veterinarian recommending food change
- Allergic reaction or dietary issue
- Food availability changes

**Side Effects:**
- Creates transition schedule if gradual change
- Sends reminders for transition milestones
- Updates current food brand in pet profile
- Archives previous food brand record
- May trigger DietaryIssueReported if reaction occurs

### 2. FeedingScheduleSet
Triggered when feeding times and portions are established for a pet.

**Event Properties:**
- `FeedingScheduleId` (Guid): Unique identifier for the schedule
- `PetId` (Guid): Pet for whom schedule is set
- `ScheduleDate` (DateTime): When the schedule was created
- `FeedingTimes` (List<FeedingTime>): Collection of feeding times
  - `Time` (TimeSpan): Time of feeding
  - `PortionSize` (decimal): Amount in cups/grams
  - `PortionUnit` (string): "Cups", "Grams", "Ounces"
  - `FoodType` (string): Type of food for this meal
- `DailyTotalPortion` (decimal): Total daily food amount
- `FoodBrand` (string): Current food brand
- `CaloriesPerPortion` (decimal): Estimated calories per portion
- `DailyCalorieTarget` (decimal): Target daily calories
- `ScheduleType` (string): "Regular", "Weight Management", "Puppy/Kitten", "Senior", "Medical"
- `SetBy` (Guid): User who set the schedule (owner or vet)
- `VeterinarianRecommended` (bool): Whether vet recommended this schedule
- `SpecialInstructions` (string): Additional feeding instructions
- `TreatAllowancePerDay` (decimal): Maximum treats allowed per day
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- Must have at least one feeding time
- Total portions must align with pet's weight and activity level
- FeedingTimes must be sorted chronologically
- Cannot have duplicate feeding times
- Portion sizes must be positive
- Daily calorie target should match pet's requirements

**Triggered By:**
- Owner creating new feeding schedule
- Veterinarian prescribing feeding plan
- Pet age/weight change requiring schedule adjustment
- Dietary plan modification

**Side Effects:**
- Generates feeding reminders for each scheduled time
- Updates pet's current feeding plan
- Cancels previous feeding schedule
- Calculates and displays nutrition statistics
- Sends confirmation notification

### 3. TreatGiven
Triggered when a treat is given to a pet.

**Event Properties:**
- `TreatRecordId` (Guid): Unique identifier for treat record
- `PetId` (Guid): Pet who received the treat
- `GivenAt` (DateTime): When the treat was given
- `TreatType` (string): Type of treat (e.g., "Dental Chew", "Training Treat", "Biscuit", "Fruit", "Other")
- `TreatName` (string): Name or description of treat
- `Quantity` (int): Number of treats given
- `EstimatedCalories` (decimal): Estimated calories per treat
- `TotalCalories` (decimal): Total calories from treats
- `GivenBy` (string): Person who gave the treat
- `Reason` (string): Reason for treat (e.g., "Training", "Reward", "Recreation")
- `Notes` (string): Optional notes
- `WithinDailyAllowance` (bool): Whether treat is within daily allowance
- `DailyTreatCount` (int): Total treats given today
- `DailyTreatCalories` (decimal): Total treat calories today
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- Quantity must be positive
- EstimatedCalories should be realistic for treat type
- Should track against daily treat allowance
- Must validate treat safety for pet type
- Should warn if exceeding daily allowance

**Triggered By:**
- Owner recording treat given to pet
- Training session with treats
- Recreational treat time
- Special occasion treats

**Side Effects:**
- Updates daily treat count and calories
- May send warning if treat allowance exceeded
- Updates nutrition tracking statistics
- Adjusts daily calorie intake calculation
- May trigger notification if treats excessive

### 4. DietaryIssueReported
Triggered when a food-related problem is logged for a pet.

**Event Properties:**
- `IssueId` (Guid): Unique identifier for the issue
- `PetId` (Guid): Pet experiencing the issue
- `ReportedDate` (DateTime): When the issue was reported
- `IssueType` (string): Type of issue
- `IssueCategory` (string): "AllergicReaction", "Vomiting", "Diarrhea", "Constipation", "LossOfAppetite", "WeightChange", "FoodRefusal", "Other"
- `Severity` (string): "Mild", "Moderate", "Severe", "Critical"
- `Symptoms` (List<string>): List of observed symptoms
- `SuspectedTrigger` (string): Suspected food or ingredient causing issue
- `OnsetDate` (DateTime): When symptoms started
- `Duration` (string): How long symptoms have persisted
- `FoodBrandAtOnset` (string): Food brand being fed when issue occurred
- `RecentFoodChange` (bool): Whether food was recently changed
- `TreatFrequency` (string): How often pet gets treats
- `VeterinarianNotified` (bool): Whether vet has been informed
- `VeterinarianNotes` (string): Vet's notes or recommendations
- `ActionsTaken` (List<string>): Actions taken to address issue
- `ResolutionStatus` (string): "Ongoing", "Resolved", "Monitoring", "VetVisitScheduled"
- `AttachmentUrls` (List<string>): Photos or documents related to issue
- `OccurredAt` (DateTime): Event timestamp

**Business Rules:**
- Severity must be specified
- OnsetDate must be <= ReportedDate
- If Severity is "Critical", must trigger urgent notification
- Should track correlation with recent food changes
- Must preserve issue history for pattern analysis
- Veterinarian should be notified for Severe/Critical issues

**Triggered By:**
- Owner observing adverse food reaction
- Digestive issues or symptoms
- Changes in eating behavior
- Weight changes outside normal range
- Veterinarian diagnosing food-related issue

**Side Effects:**
- Sends notification to pet owner
- Creates alert if severity is high
- May suggest food brand change
- Notifies veterinarian if configured
- Updates pet health history
- Triggers analysis for food allergy patterns
- May recommend vet visit

## Aggregates

### Nutrition Aggregate
**Root Entity:** NutritionPlan

**Entities:**
- NutritionPlan (root)
- FeedingSchedule
- FeedingRecord
- TreatHistory
- FoodBrandHistory
- DietaryIssueHistory

**Value Objects:**
- FeedingTime (Time, PortionSize, PortionUnit, FoodType)
- Portion (Amount, Unit, Calories)
- FoodBrand (BrandName, FoodType, CaloriesPerCup, Ingredients)
- DailyNutrition (TotalCalories, ProteinGrams, FatGrams, CarbsGrams)
- TreatAllowance (MaxTreatsPerDay, MaxCaloriesPerDay, CurrentCount, CurrentCalories)

**Invariants:**
- Active nutrition plan must have valid feeding schedule
- Total daily calories must align with pet's requirements
- Treat allowance cannot be exceeded without warning
- Food brand changes must maintain history
- Dietary issues must be tracked and monitored

### Pet Aggregate Extension
Nutrition management extends the Pet aggregate with:
- CurrentNutritionPlan
- FeedingHistory (past meals)
- TreatHistory (past treats)
- FoodBrandHistory (food changes)
- DietaryIssues (health issues)
- NutritionStatistics

## API Endpoints

### Commands

#### POST /api/nutrition/food-brand/change
Change pet's food brand.

**Request:**
```json
{
  "petId": "guid",
  "changedDate": "datetime",
  "previousFoodBrand": "string",
  "newFoodBrand": "string",
  "previousFoodType": "Dry|Wet|Raw|Homemade",
  "newFoodType": "Dry|Wet|Raw|Homemade",
  "reasonForChange": "string",
  "reasonCategory": "VeterinaryRecommendation|AllergicReaction|PreferenceChange|Availability|Cost|Other",
  "transitionPlan": "string",
  "transitionDurationDays": "number",
  "veterinarianApproved": "boolean",
  "notes": "string"
}
```

**Response:** 201 Created with FoodBrandChangeId

#### POST /api/nutrition/feeding-schedule/set
Set feeding schedule for a pet.

**Request:**
```json
{
  "petId": "guid",
  "feedingTimes": [
    {
      "time": "HH:mm",
      "portionSize": "decimal",
      "portionUnit": "Cups|Grams|Ounces",
      "foodType": "string"
    }
  ],
  "dailyTotalPortion": "decimal",
  "foodBrand": "string",
  "caloriesPerPortion": "decimal",
  "dailyCalorieTarget": "decimal",
  "scheduleType": "Regular|WeightManagement|PuppyKitten|Senior|Medical",
  "veterinarianRecommended": "boolean",
  "specialInstructions": "string",
  "treatAllowancePerDay": "decimal"
}
```

**Response:** 201 Created with FeedingScheduleId

#### POST /api/nutrition/feeding/record
Record a feeding.

**Request:**
```json
{
  "petId": "guid",
  "scheduledTime": "datetime",
  "actualTime": "datetime",
  "portionGiven": "decimal",
  "portionUnit": "string",
  "fedBy": "string",
  "notes": "string",
  "foodLeftover": "decimal"
}
```

**Response:** 200 OK

#### POST /api/nutrition/treat/give
Record treat given to pet.

**Request:**
```json
{
  "petId": "guid",
  "givenAt": "datetime",
  "treatType": "string",
  "treatName": "string",
  "quantity": "number",
  "estimatedCalories": "decimal",
  "givenBy": "string",
  "reason": "Training|Reward|Recreation",
  "notes": "string"
}
```

**Response:** 200 OK

#### POST /api/nutrition/issue/report
Report dietary issue.

**Request:**
```json
{
  "petId": "guid",
  "reportedDate": "datetime",
  "issueType": "string",
  "issueCategory": "AllergicReaction|Vomiting|Diarrhea|Constipation|LossOfAppetite|WeightChange|FoodRefusal|Other",
  "severity": "Mild|Moderate|Severe|Critical",
  "symptoms": ["string"],
  "suspectedTrigger": "string",
  "onsetDate": "datetime",
  "duration": "string",
  "actionsTaken": ["string"],
  "veterinarianNotified": "boolean",
  "attachmentUrls": ["string"]
}
```

**Response:** 201 Created with IssueId

#### PUT /api/nutrition/issue/{issueId}/update
Update dietary issue status.

**Request:**
```json
{
  "resolutionStatus": "Ongoing|Resolved|Monitoring|VetVisitScheduled",
  "veterinarianNotes": "string",
  "actionsTaken": ["string"]
}
```

**Response:** 200 OK

### Queries

#### GET /api/pets/{petId}/nutrition/current-plan
Get current nutrition plan for a pet.

**Response:**
```json
{
  "nutritionPlanId": "guid",
  "petId": "guid",
  "feedingSchedule": {
    "feedingTimes": [
      {
        "time": "HH:mm",
        "portionSize": "decimal",
        "portionUnit": "string",
        "foodType": "string",
        "nextFeedingTime": "datetime"
      }
    ],
    "dailyTotalPortion": "decimal",
    "dailyCalorieTarget": "decimal"
  },
  "currentFoodBrand": {
    "brandName": "string",
    "foodType": "string",
    "inTransition": "boolean",
    "transitionDaysRemaining": "number"
  },
  "treatAllowance": {
    "maxPerDay": "decimal",
    "maxCalories": "decimal",
    "usedToday": "number",
    "caloriesUsedToday": "decimal",
    "remaining": "number"
  }
}
```

#### GET /api/pets/{petId}/nutrition/feeding-schedule
Get feeding schedule for a specific date range.

**Query Parameters:**
- startDate (required)
- endDate (required)

**Response:**
```json
{
  "schedule": [
    {
      "scheduledTime": "datetime",
      "portionSize": "decimal",
      "portionUnit": "string",
      "foodType": "string",
      "status": "Pending|Fed|Missed|Skipped",
      "actualTime": "datetime",
      "fedBy": "string"
    }
  ]
}
```

#### GET /api/pets/{petId}/nutrition/history
Get feeding history.

**Query Parameters:**
- page (default: 1)
- pageSize (default: 20)
- startDate (optional)
- endDate (optional)

**Response:**
```json
{
  "feedings": [
    {
      "feedingId": "guid",
      "scheduledTime": "datetime",
      "actualTime": "datetime",
      "portionGiven": "decimal",
      "portionUnit": "string",
      "fedBy": "string",
      "notes": "string",
      "foodLeftover": "decimal"
    }
  ],
  "totalCount": "number",
  "feedingComplianceRate": "decimal"
}
```

#### GET /api/pets/{petId}/nutrition/treats/history
Get treat history.

**Query Parameters:**
- startDate (optional)
- endDate (optional)
- page (default: 1)
- pageSize (default: 20)

**Response:**
```json
{
  "treats": [
    {
      "treatRecordId": "guid",
      "givenAt": "datetime",
      "treatType": "string",
      "treatName": "string",
      "quantity": "number",
      "totalCalories": "decimal",
      "givenBy": "string"
    }
  ],
  "totalCount": "number",
  "totalCaloriesToday": "decimal",
  "averageTreatsPerDay": "decimal"
}
```

#### GET /api/pets/{petId}/nutrition/food-brand/history
Get food brand change history.

**Response:**
```json
{
  "foodBrandHistory": [
    {
      "foodBrandChangeId": "guid",
      "changedDate": "datetime",
      "previousBrand": "string",
      "newBrand": "string",
      "reasonForChange": "string",
      "inTransition": "boolean",
      "transitionProgress": "decimal"
    }
  ],
  "currentBrand": {
    "brandName": "string",
    "foodType": "string",
    "startedDate": "datetime",
    "daysOnCurrentBrand": "number"
  }
}
```

#### GET /api/pets/{petId}/nutrition/issues
Get dietary issues.

**Query Parameters:**
- status (optional: "Ongoing", "Resolved", "All")
- severity (optional)

**Response:**
```json
{
  "issues": [
    {
      "issueId": "guid",
      "reportedDate": "datetime",
      "issueCategory": "string",
      "severity": "string",
      "symptoms": ["string"],
      "suspectedTrigger": "string",
      "resolutionStatus": "string",
      "durationDays": "number"
    }
  ],
  "activeIssuesCount": "number"
}
```

#### GET /api/pets/{petId}/nutrition/statistics
Get nutrition statistics.

**Query Parameters:**
- periodDays (default: 30)

**Response:**
```json
{
  "period": {
    "startDate": "datetime",
    "endDate": "datetime"
  },
  "feedingCompliance": {
    "scheduledFeedings": "number",
    "completedFeedings": "number",
    "missedFeedings": "number",
    "complianceRate": "decimal"
  },
  "treatStatistics": {
    "totalTreats": "number",
    "averageTreatsPerDay": "decimal",
    "totalTreatCalories": "decimal",
    "daysOverAllowance": "number"
  },
  "averageDailyCalories": "decimal",
  "calorieTargetCompliance": "decimal",
  "weightTrend": {
    "startWeight": "decimal",
    "currentWeight": "decimal",
    "changePercentage": "decimal"
  }
}
```

## Background Services

### FeedingReminderService
- Sends notifications before scheduled feeding times
- Configurable reminder window (e.g., 15 min before)
- Supports multiple notification channels (push, email)
- Handles recurring feeding schedules

### MissedFeedingDetectionService
- Runs hourly to check for missed feedings
- Grace period before marking as missed
- Triggers alerts for missed feedings
- Tracks feeding compliance

### FoodTransitionMonitorService
- Monitors ongoing food brand transitions
- Sends reminders for transition milestones
- Tracks transition progress
- Alerts on transition completion

### TreatAllowanceMonitorService
- Tracks daily treat consumption
- Sends warnings when approaching daily limit
- Alerts when treat allowance exceeded
- Resets daily counters at midnight

### DietaryIssueAnalysisService
- Analyzes patterns in dietary issues
- Correlates issues with food changes
- Identifies potential food allergies
- Generates health insights and recommendations

## Data Models

### NutritionPlan Entity
```csharp
public class NutritionPlan
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public FeedingSchedule Schedule { get; set; }
    public FoodBrand CurrentFoodBrand { get; set; }
    public TreatAllowance TreatAllowance { get; set; }
    public decimal DailyCalorieTarget { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public bool IsActive { get; set; }
}
```

### FeedingSchedule Entity
```csharp
public class FeedingSchedule
{
    public Guid Id { get; set; }
    public Guid NutritionPlanId { get; set; }
    public List<FeedingTime> FeedingTimes { get; set; }
    public decimal DailyTotalPortion { get; set; }
    public string ScheduleType { get; set; }
    public string SpecialInstructions { get; set; }
    public bool VeterinarianRecommended { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### FeedingRecord Entity
```csharp
public class FeedingRecord
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime? ActualTime { get; set; }
    public decimal PortionGiven { get; set; }
    public string PortionUnit { get; set; }
    public string FedBy { get; set; }
    public string Notes { get; set; }
    public decimal? FoodLeftover { get; set; }
    public FeedingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### TreatRecord Entity
```csharp
public class TreatRecord
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime GivenAt { get; set; }
    public string TreatType { get; set; }
    public string TreatName { get; set; }
    public int Quantity { get; set; }
    public decimal EstimatedCalories { get; set; }
    public decimal TotalCalories { get; set; }
    public string GivenBy { get; set; }
    public string Reason { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### FoodBrandChange Entity
```csharp
public class FoodBrandChange
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime ChangedDate { get; set; }
    public string PreviousFoodBrand { get; set; }
    public string NewFoodBrand { get; set; }
    public string PreviousFoodType { get; set; }
    public string NewFoodType { get; set; }
    public string ReasonForChange { get; set; }
    public string ReasonCategory { get; set; }
    public string TransitionPlan { get; set; }
    public int TransitionDurationDays { get; set; }
    public bool VeterinarianApproved { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### DietaryIssue Entity
```csharp
public class DietaryIssue
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public DateTime ReportedDate { get; set; }
    public string IssueType { get; set; }
    public string IssueCategory { get; set; }
    public string Severity { get; set; }
    public List<string> Symptoms { get; set; }
    public string SuspectedTrigger { get; set; }
    public DateTime OnsetDate { get; set; }
    public string Duration { get; set; }
    public string FoodBrandAtOnset { get; set; }
    public bool RecentFoodChange { get; set; }
    public bool VeterinarianNotified { get; set; }
    public string VeterinarianNotes { get; set; }
    public List<string> ActionsTaken { get; set; }
    public string ResolutionStatus { get; set; }
    public List<string> AttachmentUrls { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
}
```

## Validation Rules

### Food Brand Change
- New food brand required (max 200 chars)
- Reason for change required
- Transition duration >= 7 days recommended
- Changed date cannot be in future

### Feeding Schedule
- At least one feeding time required
- Feeding times must be unique
- Portion sizes must be positive
- Daily total must equal sum of portions
- Calorie target must be appropriate for pet size/age

### Treat Recording
- Treat type required
- Quantity must be positive (max 20 per entry)
- Estimated calories should be reasonable
- Must validate against daily allowance

### Dietary Issue Reporting
- Issue category required
- Severity required
- Onset date must be <= reported date
- If severity is Critical, veterinarian notification recommended
- Symptoms list cannot be empty

## Security & Authorization

### Permissions
- **PetOwner**: Can manage nutrition for their own pets
- **Veterinarian**: Can view nutrition plans and set recommended schedules
- **Admin**: Full access to all nutrition records

### Access Rules
- Users can only view/modify nutrition plans for pets they own
- Veterinarians can view nutrition for pets under their care
- Veterinarians can set recommended feeding schedules
- Nutrition history is read-only after 48 hours
- Audit trail for all nutrition operations
- Dietary issues shared with assigned veterinarian

## Integration Points

### Veterinary System Integration
- Import feeding recommendations from vet systems
- Export nutrition history for vet appointments
- Share dietary issues with veterinarian
- Receive prescription diet plans

### Pet Food Database
- Lookup food brands and nutritional information
- Import calorie data for foods
- Ingredient information for allergy tracking
- Product recall notifications

### Weight Tracking Integration
- Correlate feeding with weight changes
- Adjust calorie targets based on weight trends
- Nutrition effectiveness analysis

### Notification System
- Push notifications for feeding reminders
- Email summaries for missed feedings
- SMS alerts for dietary issues
- Daily/weekly nutrition reports

## Performance Requirements

- Feeding schedule generation: < 1 second for 30-day period
- Nutrition plan query: < 200ms
- Feeding record: < 300ms
- Support 10,000+ active nutrition plans
- Handle 100,000+ feeding records
- Real-time notification delivery < 5 seconds

## Error Handling

### Business Rule Violations
- Return 400 Bad Request with detailed error message
- Include validation errors in response
- Provide corrective action guidance

### Not Found
- Return 404 for invalid nutrition plan/pet IDs
- Clear error messages

### Concurrency
- Optimistic concurrency control using row versions
- Return 409 Conflict for concurrent modifications
- Client must refresh and retry

### System Errors
- Log detailed error information
- Return 500 with generic message to client
- Alert monitoring system for critical failures
