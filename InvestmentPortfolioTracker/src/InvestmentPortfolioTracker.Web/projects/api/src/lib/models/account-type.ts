export enum AccountType {
  Taxable = 0,
  TraditionalIRA = 1,
  RothIRA = 2,
  FourZeroOneK = 3,
  FourZeroThreeB = 4,
  HSA = 5,
  FiveTwoNine = 6
}

export const ACCOUNT_TYPE_LABELS: Record<AccountType, string> = {
  [AccountType.Taxable]: 'Taxable',
  [AccountType.TraditionalIRA]: 'Traditional IRA',
  [AccountType.RothIRA]: 'Roth IRA',
  [AccountType.FourZeroOneK]: '401(k)',
  [AccountType.FourZeroThreeB]: '403(b)',
  [AccountType.HSA]: 'HSA',
  [AccountType.FiveTwoNine]: '529'
};
