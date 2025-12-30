export type CanadianProvince =
  | 'Alberta'
  | 'BritishColumbia'
  | 'Manitoba'
  | 'NewBrunswick'
  | 'NewfoundlandAndLabrador'
  | 'NorthwestTerritories'
  | 'NovaScotia'
  | 'Nunavut'
  | 'Ontario'
  | 'PrinceEdwardIsland'
  | 'Quebec'
  | 'Saskatchewan'
  | 'Yukon';

export interface HouseholdDto {
  householdId: string;
  name: string;
  street: string;
  city: string;
  province: CanadianProvince;
  postalCode: string;
  formattedPostalCode: string;
  fullAddress: string;
}

export const CANADIAN_PROVINCES: { value: CanadianProvince; label: string }[] = [
  { value: 'Alberta', label: 'Alberta' },
  { value: 'BritishColumbia', label: 'British Columbia' },
  { value: 'Manitoba', label: 'Manitoba' },
  { value: 'NewBrunswick', label: 'New Brunswick' },
  { value: 'NewfoundlandAndLabrador', label: 'Newfoundland and Labrador' },
  { value: 'NorthwestTerritories', label: 'Northwest Territories' },
  { value: 'NovaScotia', label: 'Nova Scotia' },
  { value: 'Nunavut', label: 'Nunavut' },
  { value: 'Ontario', label: 'Ontario' },
  { value: 'PrinceEdwardIsland', label: 'Prince Edward Island' },
  { value: 'Quebec', label: 'Quebec' },
  { value: 'Saskatchewan', label: 'Saskatchewan' },
  { value: 'Yukon', label: 'Yukon' }
];

export function getProvinceLabel(province: CanadianProvince): string {
  const found = CANADIAN_PROVINCES.find(p => p.value === province);
  return found?.label || province;
}
