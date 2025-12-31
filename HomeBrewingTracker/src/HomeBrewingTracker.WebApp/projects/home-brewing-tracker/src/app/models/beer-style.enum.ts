export enum BeerStyle {
  PaleAle = 0,
  IPA = 1,
  Stout = 2,
  Porter = 3,
  Lager = 4,
  Pilsner = 5,
  Wheat = 6,
  Belgian = 7,
  Sour = 8,
  Amber = 9,
  BrownAle = 10,
  Other = 11
}

export const BeerStyleLabels: Record<BeerStyle, string> = {
  [BeerStyle.PaleAle]: 'Pale Ale',
  [BeerStyle.IPA]: 'IPA',
  [BeerStyle.Stout]: 'Stout',
  [BeerStyle.Porter]: 'Porter',
  [BeerStyle.Lager]: 'Lager',
  [BeerStyle.Pilsner]: 'Pilsner',
  [BeerStyle.Wheat]: 'Wheat',
  [BeerStyle.Belgian]: 'Belgian',
  [BeerStyle.Sour]: 'Sour',
  [BeerStyle.Amber]: 'Amber',
  [BeerStyle.BrownAle]: 'Brown Ale',
  [BeerStyle.Other]: 'Other'
};
