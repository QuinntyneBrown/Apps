export enum CanadianProvince {
  Alberta = 0,
  BritishColumbia = 1,
  Manitoba = 2,
  NewBrunswick = 3,
  NewfoundlandAndLabrador = 4,
  NorthwestTerritories = 5,
  NovaScotia = 6,
  Nunavut = 7,
  Ontario = 8,
  PrinceEdwardIsland = 9,
  Quebec = 10,
  Saskatchewan = 11,
  Yukon = 12
}

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
  { value: CanadianProvince.Alberta, label: 'Alberta' },
  { value: CanadianProvince.BritishColumbia, label: 'British Columbia' },
  { value: CanadianProvince.Manitoba, label: 'Manitoba' },
  { value: CanadianProvince.NewBrunswick, label: 'New Brunswick' },
  { value: CanadianProvince.NewfoundlandAndLabrador, label: 'Newfoundland and Labrador' },
  { value: CanadianProvince.NorthwestTerritories, label: 'Northwest Territories' },
  { value: CanadianProvince.NovaScotia, label: 'Nova Scotia' },
  { value: CanadianProvince.Nunavut, label: 'Nunavut' },
  { value: CanadianProvince.Ontario, label: 'Ontario' },
  { value: CanadianProvince.PrinceEdwardIsland, label: 'Prince Edward Island' },
  { value: CanadianProvince.Quebec, label: 'Quebec' },
  { value: CanadianProvince.Saskatchewan, label: 'Saskatchewan' },
  { value: CanadianProvince.Yukon, label: 'Yukon' }
];

export function getProvinceLabel(province: CanadianProvince): string {
  const found = CANADIAN_PROVINCES.find(p => p.value === province);
  return found?.label || CanadianProvince[province];
}
