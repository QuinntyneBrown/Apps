export interface Projection {
  projectionId: string;
  planId: string;
  name: string;
  currentSavings: number;
  monthlyContribution: number;
  expectedReturnRate: number;
  yearsUntilCollege: number;
  targetGoal: number;
  projectedBalance: number;
  createdAt: string;
  goalDifference: number;
  requiredMonthlyContribution: number;
}

export interface CreateProjection {
  planId: string;
  name: string;
  currentSavings: number;
  monthlyContribution: number;
  expectedReturnRate: number;
  yearsUntilCollege: number;
  targetGoal: number;
}

export interface UpdateProjection {
  name: string;
  currentSavings: number;
  monthlyContribution: number;
  expectedReturnRate: number;
  yearsUntilCollege: number;
  targetGoal: number;
}
