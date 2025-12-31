export interface RefinanceScenario {
  refinanceScenarioId: string;
  mortgageId: string;
  name: string;
  newInterestRate: number;
  newLoanTermYears: number;
  refinancingCosts: number;
  newMonthlyPayment: number;
  monthlySavings: number;
  breakEvenMonths: number;
  totalSavings: number;
  createdAt: string;
}

export interface CreateRefinanceScenario {
  mortgageId: string;
  name: string;
  newInterestRate: number;
  newLoanTermYears: number;
  refinancingCosts: number;
  newMonthlyPayment: number;
  monthlySavings: number;
  breakEvenMonths: number;
  totalSavings: number;
}

export interface UpdateRefinanceScenario {
  refinanceScenarioId: string;
  mortgageId: string;
  name: string;
  newInterestRate: number;
  newLoanTermYears: number;
  refinancingCosts: number;
  newMonthlyPayment: number;
  monthlySavings: number;
  breakEvenMonths: number;
  totalSavings: number;
}
