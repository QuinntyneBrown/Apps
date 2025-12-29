# Offers - Frontend Requirements

## Pages/Views

### 1. Offers Dashboard (`/offers`)
- List of all offers with status badges
- Filter by status: Pending, Accepted, Rejected, Expired
- Quick comparison view
- Deadline countdown timers

### 2. Add Offer Form (`/offers/new`)
- Application selection
- Salary details (base, bonus, equity)
- Benefits checklist
- Start date and deadline
- Upload offer letter

### 3. Offer Detail View (`/offers/:id`)
- Complete offer details
- Comparison calculator
- Negotiation history
- Accept/reject/counter actions
- Download offer letter

### 4. Compare Offers (`/offers/compare`)
- Side-by-side comparison of multiple offers
- Total compensation calculator
- Benefits comparison matrix
- Recommendations based on priorities

### 5. Negotiation Dialog
- Counter offer form
- Requested salary
- Additional benefits requests
- Negotiation notes

## Components

### OfferCard
- Company and role
- Total compensation (with breakdown tooltip)
- Status badge
- Deadline indicator
- Quick actions

### CompensationBreakdown
- Base salary
- Bonus (annual/sign-on)
- Equity value estimate
- Benefits value
- Total compensation

### OfferComparison
- Multi-column comparison table
- Highlight differences
- Score/rank offers
- Custom criteria weighting

### DeadlineTimer
- Countdown to deadline
- Color-coded urgency
- Expiration warnings

## State Management

**offersSlice:**
```typescript
{
  offers: Offer[],
  selectedOffer: Offer | null,
  compareMode: boolean,
  selectedForComparison: string[],
  loading: boolean
}
```

## UI/UX Features

- Offer comparison calculator
- Benefits valuation estimator
- Deadline reminders
- Visual compensation breakdown
- Negotiation tracker
- Mobile-responsive design
