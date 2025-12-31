import { BillingCycle } from './billing-cycle.enum';
import { SubscriptionStatus } from './subscription-status.enum';

export interface Subscription {
  subscriptionId: string;
  serviceName: string;
  cost: number;
  billingCycle: BillingCycle;
  nextBillingDate: string;
  status: SubscriptionStatus;
  startDate: string;
  cancellationDate?: string;
  categoryId?: string;
  notes?: string;
  annualCost: number;
  categoryName?: string;
}
