export enum WithdrawalStrategyType {
  FixedAmount = 0,
  PercentageBased = 1,
  Dynamic = 2,
  RequiredMinimumDistribution = 3
}

export const WithdrawalStrategyTypeLabels: Record<WithdrawalStrategyType, string> = {
  [WithdrawalStrategyType.FixedAmount]: 'Fixed Amount',
  [WithdrawalStrategyType.PercentageBased]: 'Percentage Based',
  [WithdrawalStrategyType.Dynamic]: 'Dynamic',
  [WithdrawalStrategyType.RequiredMinimumDistribution]: 'Required Minimum Distribution'
};

export interface WithdrawalStrategy {
  withdrawalStrategyId: string;
  retirementScenarioId: string;
  name: string;
  withdrawalRate: number;
  annualWithdrawalAmount: number;
  adjustForInflation: boolean;
  minimumBalance?: number;
  strategyType: WithdrawalStrategyType;
  notes?: string;
}

export interface CreateWithdrawalStrategy {
  retirementScenarioId: string;
  name: string;
  withdrawalRate: number;
  annualWithdrawalAmount: number;
  adjustForInflation: boolean;
  minimumBalance?: number;
  strategyType: WithdrawalStrategyType;
  notes?: string;
}

export interface UpdateWithdrawalStrategy {
  withdrawalStrategyId: string;
  name: string;
  withdrawalRate: number;
  annualWithdrawalAmount: number;
  adjustForInflation: boolean;
  minimumBalance?: number;
  strategyType: WithdrawalStrategyType;
  notes?: string;
}
