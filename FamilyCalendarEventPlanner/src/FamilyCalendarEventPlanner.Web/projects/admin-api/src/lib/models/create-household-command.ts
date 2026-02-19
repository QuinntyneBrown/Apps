import { CanadianProvince } from './household-dto';

export interface CreateHouseholdCommand {
  name: string;
  street: string;
  city: string;
  province: CanadianProvince;
  postalCode: string;
}
