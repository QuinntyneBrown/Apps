export enum MortgageType {
  Fixed = 0,
  ARM = 1,
  FHA = 2,
  VA = 3,
  USDA = 4
}

export const MortgageTypeLabels = {
  [MortgageType.Fixed]: 'Fixed-Rate',
  [MortgageType.ARM]: 'Adjustable-Rate (ARM)',
  [MortgageType.FHA]: 'FHA Loan',
  [MortgageType.VA]: 'VA Loan',
  [MortgageType.USDA]: 'USDA Loan'
};
