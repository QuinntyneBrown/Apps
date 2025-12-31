import { BillingCycle } from './billing-cycle.enum';

export interface CreateSubscriptionRequest {
  serviceName: string;
  cost: number;
  billingCycle: BillingCycle;
  nextBillingDate: string;
  startDate: string;
  categoryId?: string;
  notes?: string;
}
