export interface Contribution {
  contributionId: string;
  retirementScenarioId: string;
  amount: number;
  contributionDate: string;
  accountName: string;
  isEmployerMatch: boolean;
  notes?: string;
}

export interface CreateContribution {
  retirementScenarioId: string;
  amount: number;
  contributionDate: string;
  accountName: string;
  isEmployerMatch: boolean;
  notes?: string;
}

export interface UpdateContribution {
  contributionId: string;
  amount: number;
  contributionDate: string;
  accountName: string;
  isEmployerMatch: boolean;
  notes?: string;
}
