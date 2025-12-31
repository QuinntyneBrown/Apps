import { AlertStatus } from './alert-status.enum';
import { BreachSeverity } from './breach-severity.enum';

export interface BreachAlert {
  breachAlertId: string;
  accountId: string;
  severity: BreachSeverity;
  status: AlertStatus;
  detectedDate: string;
  breachDate?: string;
  source?: string;
  description: string;
  dataCompromised?: string;
  recommendedActions?: string;
  acknowledgedAt?: string;
  resolvedAt?: string;
  notes?: string;
}

export interface CreateBreachAlert {
  accountId: string;
  severity: BreachSeverity;
  breachDate?: string;
  source?: string;
  description: string;
  dataCompromised?: string;
  recommendedActions?: string;
  notes?: string;
}

export interface UpdateBreachAlert {
  breachAlertId: string;
  severity: BreachSeverity;
  status: AlertStatus;
  breachDate?: string;
  source?: string;
  description: string;
  dataCompromised?: string;
  recommendedActions?: string;
  notes?: string;
}
