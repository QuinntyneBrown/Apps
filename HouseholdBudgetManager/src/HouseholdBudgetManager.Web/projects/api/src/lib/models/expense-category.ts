export enum ExpenseCategory {
  Housing = 0,
  Transportation = 1,
  Food = 2,
  Healthcare = 3,
  Entertainment = 4,
  PersonalCare = 5,
  Education = 6,
  DebtPayment = 7,
  Savings = 8,
  Other = 9
}

export const EXPENSE_CATEGORY_LABELS: Record<ExpenseCategory, string> = {
  [ExpenseCategory.Housing]: 'Housing',
  [ExpenseCategory.Transportation]: 'Transportation',
  [ExpenseCategory.Food]: 'Food',
  [ExpenseCategory.Healthcare]: 'Healthcare',
  [ExpenseCategory.Entertainment]: 'Entertainment',
  [ExpenseCategory.PersonalCare]: 'Personal Care',
  [ExpenseCategory.Education]: 'Education',
  [ExpenseCategory.DebtPayment]: 'Debt Payment',
  [ExpenseCategory.Savings]: 'Savings',
  [ExpenseCategory.Other]: 'Other'
};
