export enum AccountCategory {
  SocialMedia = 0,
  Email = 1,
  Banking = 2,
  Shopping = 3,
  Work = 4,
  Entertainment = 5,
  Healthcare = 6,
  Other = 7
}

export const AccountCategoryLabels: Record<AccountCategory, string> = {
  [AccountCategory.SocialMedia]: 'Social Media',
  [AccountCategory.Email]: 'Email',
  [AccountCategory.Banking]: 'Banking',
  [AccountCategory.Shopping]: 'Shopping',
  [AccountCategory.Work]: 'Work',
  [AccountCategory.Entertainment]: 'Entertainment',
  [AccountCategory.Healthcare]: 'Healthcare',
  [AccountCategory.Other]: 'Other'
};
