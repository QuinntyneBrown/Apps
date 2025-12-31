export enum LiabilityType {
  Mortgage = 0,
  AutoLoan = 1,
  StudentLoan = 2,
  CreditCard = 3,
  PersonalLoan = 4,
  MedicalDebt = 5,
  BusinessLoan = 6,
  Other = 7
}

export const LiabilityTypeLabels: Record<LiabilityType, string> = {
  [LiabilityType.Mortgage]: 'Mortgage',
  [LiabilityType.AutoLoan]: 'Auto Loan',
  [LiabilityType.StudentLoan]: 'Student Loan',
  [LiabilityType.CreditCard]: 'Credit Card',
  [LiabilityType.PersonalLoan]: 'Personal Loan',
  [LiabilityType.MedicalDebt]: 'Medical Debt',
  [LiabilityType.BusinessLoan]: 'Business Loan',
  [LiabilityType.Other]: 'Other'
};
