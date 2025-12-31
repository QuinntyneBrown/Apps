export interface Contribution {
  contributionId: string;
  planId: string;
  amount: number;
  contributionDate: string;
  contributor?: string;
  notes?: string;
  isRecurring: boolean;
}

export interface CreateContribution {
  planId: string;
  amount: number;
  contributionDate: string;
  contributor?: string;
  notes?: string;
  isRecurring: boolean;
}

export interface UpdateContribution {
  amount: number;
  contributionDate: string;
  contributor?: string;
  notes?: string;
  isRecurring: boolean;
}
