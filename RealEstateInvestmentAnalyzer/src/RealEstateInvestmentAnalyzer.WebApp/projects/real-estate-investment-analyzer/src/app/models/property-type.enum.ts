export enum PropertyType {
  SingleFamily = 0,
  MultiFamily = 1,
  Condo = 2,
  Townhouse = 3,
  Commercial = 4,
  Land = 5
}

export const PropertyTypeLabels: Record<PropertyType, string> = {
  [PropertyType.SingleFamily]: 'Single Family',
  [PropertyType.MultiFamily]: 'Multi Family',
  [PropertyType.Condo]: 'Condo',
  [PropertyType.Townhouse]: 'Townhouse',
  [PropertyType.Commercial]: 'Commercial',
  [PropertyType.Land]: 'Land'
};
