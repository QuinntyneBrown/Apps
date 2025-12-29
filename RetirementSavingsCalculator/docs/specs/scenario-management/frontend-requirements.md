# Frontend Requirements - Scenario Management

## UI Components

### 1. Scenario Creation Wizard
**Steps:**
1. Basic Info (name, ages)
2. Current Savings & Contributions
3. Retirement Income Sources
4. Expected Expenses
5. Review & Calculate

**Form Fields:**
- Scenario name
- Current age (number input)
- Desired retirement age (slider: 55-75)
- Life expectancy (slider: 75-100, default: 90)
- Current savings (currency input)
- Monthly contribution (currency input)

### 2. Scenario List Page
**Display:**
- Card view of all scenarios
- Each card shows: name, retirement age, savings gap, status
- Quick actions: View, Edit, Duplicate, Delete
- "+ New Scenario" button

### 3. Scenario Comparison View
**Features:**
- Select up to 4 scenarios
- Side-by-side comparison table
- Key metrics: total savings, monthly income, gap
- Highlight best scenario

## User Flows

### Create Scenario
1. Click "New Scenario"
2. Wizard opens
3. User fills step 1
4. Click "Next"
5. Repeat for steps 2-4
6. Step 5 shows summary
7. Click "Calculate"
8. Results display

### Duplicate Scenario
1. Click "Duplicate" on scenario card
2. Modal asks for new name
3. User enters name
4. Click "Duplicate"
5. New scenario created with same parameters
6. Redirect to edit new scenario

## Analytics Events
- `scenario_created`
- `scenario_duplicated`
- `retirement_age_changed`
- `scenario_deleted`
