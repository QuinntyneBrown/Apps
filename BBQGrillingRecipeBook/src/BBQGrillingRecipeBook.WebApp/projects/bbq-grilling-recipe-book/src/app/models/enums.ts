export enum MeatType {
  Beef = 0,
  Pork = 1,
  Chicken = 2,
  Turkey = 3,
  Lamb = 4,
  Seafood = 5,
  Vegetables = 6,
  Mixed = 7,
  Other = 8
}

export const MeatTypeLabels: Record<MeatType, string> = {
  [MeatType.Beef]: 'Beef',
  [MeatType.Pork]: 'Pork',
  [MeatType.Chicken]: 'Chicken',
  [MeatType.Turkey]: 'Turkey',
  [MeatType.Lamb]: 'Lamb',
  [MeatType.Seafood]: 'Seafood',
  [MeatType.Vegetables]: 'Vegetables',
  [MeatType.Mixed]: 'Mixed',
  [MeatType.Other]: 'Other'
};

export enum CookingMethod {
  DirectGrilling = 0,
  IndirectGrilling = 1,
  Smoking = 2,
  Rotisserie = 3,
  Searing = 4,
  SlowAndLow = 5,
  Combination = 6
}

export const CookingMethodLabels: Record<CookingMethod, string> = {
  [CookingMethod.DirectGrilling]: 'Direct Grilling',
  [CookingMethod.IndirectGrilling]: 'Indirect Grilling',
  [CookingMethod.Smoking]: 'Smoking',
  [CookingMethod.Rotisserie]: 'Rotisserie',
  [CookingMethod.Searing]: 'Searing',
  [CookingMethod.SlowAndLow]: 'Slow and Low',
  [CookingMethod.Combination]: 'Combination'
};
