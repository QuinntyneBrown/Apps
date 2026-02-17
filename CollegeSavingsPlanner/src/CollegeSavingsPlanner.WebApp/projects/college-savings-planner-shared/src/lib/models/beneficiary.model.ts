export interface Beneficiary {
  beneficiaryId: string;
  planId: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  relationship?: string;
  expectedCollegeStartYear?: number;
  isPrimary: boolean;
  age: number;
  yearsUntilCollege?: number;
}

export interface CreateBeneficiary {
  planId: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  relationship?: string;
  expectedCollegeStartYear?: number;
  isPrimary: boolean;
}

export interface UpdateBeneficiary {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  relationship?: string;
  expectedCollegeStartYear?: number;
  isPrimary: boolean;
}
