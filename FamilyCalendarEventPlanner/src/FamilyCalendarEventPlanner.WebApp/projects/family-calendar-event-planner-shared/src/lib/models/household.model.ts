import { CanadianProvince } from './enums';

export interface Household {
  householdId: string;
  name: string;
  street: string;
  city: string;
  province: CanadianProvince;
  postalCode: string;
  formattedPostalCode: string;
  fullAddress: string;
}

export interface CreateHouseholdRequest {
  name: string;
  street: string;
  city: string;
  province: CanadianProvince;
  postalCode: string;
}

export interface UpdateHouseholdRequest {
  householdId: string;
  name?: string;
  street?: string;
  city?: string;
  province?: CanadianProvince;
  postalCode?: string;
}
