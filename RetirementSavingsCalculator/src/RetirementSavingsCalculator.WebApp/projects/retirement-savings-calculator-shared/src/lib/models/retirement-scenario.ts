export interface RetirementScenario {
  retirementScenarioId: string;
  name: string;
  currentAge: number;
  retirementAge: number;
  lifeExpectancyAge: number;
  currentSavings: number;
  annualContribution: number;
  expectedReturnRate: number;
  inflationRate: number;
  projectedAnnualIncome: number;
  projectedAnnualExpenses: number;
  notes?: string;
  createdAt: string;
  lastUpdated: string;
  projectedSavings: number;
  annualWithdrawalNeeded: number;
  yearsToRetirement: number;
  yearsInRetirement: number;
}

export interface CreateRetirementScenario {
  name: string;
  currentAge: number;
  retirementAge: number;
  lifeExpectancyAge: number;
  currentSavings: number;
  annualContribution: number;
  expectedReturnRate: number;
  inflationRate: number;
  projectedAnnualIncome: number;
  projectedAnnualExpenses: number;
  notes?: string;
}

export interface UpdateRetirementScenario {
  retirementScenarioId: string;
  name: string;
  currentAge: number;
  retirementAge: number;
  lifeExpectancyAge: number;
  currentSavings: number;
  annualContribution: number;
  expectedReturnRate: number;
  inflationRate: number;
  projectedAnnualIncome: number;
  projectedAnnualExpenses: number;
  notes?: string;
}
