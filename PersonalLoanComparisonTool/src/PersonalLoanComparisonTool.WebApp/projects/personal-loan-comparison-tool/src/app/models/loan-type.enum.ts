export enum LoanType {
  Personal = 0,
  Auto = 1,
  Home = 2,
  Student = 3,
  Business = 4
}

export const LoanTypeLabels: Record<LoanType, string> = {
  [LoanType.Personal]: 'Personal',
  [LoanType.Auto]: 'Auto',
  [LoanType.Home]: 'Home',
  [LoanType.Student]: 'Student',
  [LoanType.Business]: 'Business'
};
