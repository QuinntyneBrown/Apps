export interface Contribution {
  contributionId: string;
  goalId: string;
  amount: number;
  contributionDate: string;
  notes?: string;
}

export interface CreateContributionCommand {
  goalId: string;
  amount: number;
  contributionDate: string;
  notes?: string;
}

export interface UpdateContributionCommand {
  contributionId: string;
  goalId: string;
  amount: number;
  contributionDate: string;
  notes?: string;
}
