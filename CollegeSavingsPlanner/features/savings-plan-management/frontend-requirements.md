# Savings Plan Management - Frontend Requirements

## Overview
User interface for creating and managing college savings plans, tracking beneficiaries, and monitoring savings progress.

## Key Pages

### 1. Plans Dashboard
**Route**: `/plans`

**Features**:
- List of all savings plans grouped by beneficiary
- Plan summary cards (balance, beneficiary, progress)
- Add New Plan button
- Quick stats (total saved, total beneficiaries, avg progress)
- Timeline view of beneficiaries' college start dates

### 2. Create Plan Wizard
**Route**: `/plans/create`

**Steps**:
1. **Plan Type**: Select 529, ESA, UTMA, etc.
2. **Beneficiary**: Select existing or add new child
3. **Plan Details**: State plan, institution, account number
4. **Initial Balance**: Starting amount

### 3. Plan Details View
**Route**: `/plans/{planId}`

**Sections**:
- **Plan Overview**: Type, state, institution, account number
- **Beneficiary Info**: Name, age, years until college
- **Balance**: Current amount, growth over time
- **Contributions**: History and recurring schedules
- **Projections**: Projected balance at enrollment
- **Investment**: Current allocation and performance
- **Actions**: Add contribution, change beneficiary, close plan

### 4. Beneficiary Management
**Route**: `/beneficiaries`

**Features**:
- List of all children/beneficiaries
- Plans associated with each beneficiary
- Total saved per beneficiary
- Progress toward goal
- Add/edit beneficiaries
- View timeline (years until college)

## UI Components

### PlanCard
- Plan type badge
- Beneficiary name and photo
- Current balance (large)
- Progress gauge toward goal
- Quick action buttons

### BeneficiaryTimeline
- Visual timeline showing years until college
- Color-coded by urgency (green: 10+ years, yellow: 5-10, red: <5)
- Milestones (kindergarten, middle school, high school, college)

### BalanceGauge
- Circular progress showing saved vs goal
- Percentage complete
- Years remaining countdown

### ContributionChart
- Line graph showing balance growth over time
- Projection line to enrollment
- Contribution markers

## User Flows

### Create Plan Flow
1. User clicks "Create New Plan"
2. Wizard opens, selects plan type
3. User selects or creates beneficiary
4. User enters plan details (state, institution)
5. User enters initial balance (if any)
6. System calculates years until enrollment
7. User clicks "Create Plan"
8. Plan created and redirects to plan details
9. Prompt to set up recurring contributions

### Change Beneficiary Flow
1. User views plan details
2. User clicks "Change Beneficiary"
3. System shows eligible family members
4. User selects new beneficiary
5. System shows tax impact warning
6. User confirms change
7. Beneficiary updated
8. Projections recalculated

## State Management

```typescript
interface PlansState {
  plans: SavingsPlan[];
  beneficiaries: Beneficiary[];
  selectedPlan: SavingsPlan | null;
  loading: boolean;
  error: string | null;
}

interface SavingsPlan {
  planId: string;
  userId: string;
  planType: '529' | 'ESA' | 'UTMA' | 'UGMA' | 'Other';
  statePlan: string;
  currentBalance: number;
  beneficiary: Beneficiary;
  openDate: Date;
  status: 'Active' | 'Closed' | 'Transferred';
}

interface Beneficiary {
  beneficiaryId: string;
  name: string;
  birthDate: Date;
  relationship: string;
  expectedEnrollmentYear: number;
  currentAge: number;
  yearsUntilCollege: number;
}
```

## Responsive Design

### Mobile
- Stacked plan cards
- Simplified timeline view
- Bottom sheet for plan details

### Tablet
- Two-column plan grid
- Expanded charts

### Desktop
- Multi-column layout
- Side-by-side comparison
- Full timeline visualization

## Testing
- E2E: Complete plan creation flow
- E2E: Change beneficiary flow
- Unit: Age and enrollment calculations
- Integration: Plan balance updates
