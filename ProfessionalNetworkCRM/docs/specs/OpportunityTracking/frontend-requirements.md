# Frontend Requirements - Opportunity Tracking

## User Interface Components

### Opportunities Dashboard
**Route**: `/opportunities`

**Widgets**:
- Opportunities by status
- Opportunities by type
- Total value tracked
- Conversion rate

### Log Opportunity Form
**Fields**:
- Contact (who provided)
- Opportunity type
- Description
- Estimated value
- Status
- Notes

### Introduction Manager
**Features**:
- Request introduction form
- Track introduction status
- Log introductions made
- Reciprocity tracking

### Referral Tracker
**Display**:
- Referrals received list
- Referral source contact
- Outcome tracking
- Thank you note prompt

## State Management

```typescript
interface OpportunitiesState {
  opportunities: Opportunity[];
  introductions: Introduction[];
  referrals: Referral[];
  stats: {
    totalValue: number;
    conversionRate: number;
  };
}
```
