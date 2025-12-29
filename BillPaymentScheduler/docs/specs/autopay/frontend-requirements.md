# Autopay - Frontend Requirements

## Overview
User interface for managing automatic bill payment configurations.

## User Stories
1. Enable autopay for recurring bills
2. Configure autopay settings (payment method, max amount, approval threshold)
3. View all autopay-enabled bills
4. Temporarily pause or disable autopay
5. Approve pending autopay executions
6. View autopay execution history
7. Set safety limits and controls

## Pages/Views

### 1. Autopay Dashboard (`/autopay`)
- List of autopay-enabled bills
- Summary statistics (Total enabled, Next execution, Savings estimate)
- Quick enable/disable toggles
- Pending approvals section

### 2. Enable Autopay Form
- Select bill
- Choose payment method
- Set maximum amount (optional)
- Configure approval threshold
- Set execution timing (days before due date)
- Safety settings
- Confirmation summary

### 3. Autopay Configuration Details
- Current settings
- Execution history
- Edit configuration
- Pause/Resume autopay
- Disable autopay

### 4. Pending Approvals View
- Bills requiring approval due to amount changes
- Old vs new amount comparison
- Approve/Deny actions
- Bulk approval option

## UI Components

### AutopayCard
- Bill name and amount
- Payment method
- Next execution date
- Enable/disable toggle
- Quick settings access

### AutopayStatusBadge
- Enabled (green)
- Paused (yellow)
- Disabled (gray)
- Pending Approval (orange)

### SafetyLimitsPanel
- Maximum amount slider
- Approval threshold slider
- Failure handling options

## State Management

```typescript
interface AutopayState {
  configurations: AutopayConfiguration[];
  pendingApprovals: AutopayApproval[];
  loading: boolean;
  error: string | null;
}

interface AutopayConfiguration {
  autopayConfigId: string;
  billId: string;
  billName: string;
  paymentMethodId: string;
  isEnabled: boolean;
  maxAmount?: number;
  requireApproval: boolean;
  approvalThresholdPercentage: number;
  lastExecutionDate?: Date;
  nextExecutionDate?: Date;
  executionDaysBefore: number;
  failureCount: number;
}
```

## Notifications

### Success
- "Autopay enabled successfully"
- "Autopay configuration updated"
- "Autopay disabled"
- "Payment approved"

### Warnings
- "Autopay execution pending your approval"
- "Bill amount changed by 15% - approval required"
- "Autopay failed 2 times. One more failure will disable it."
