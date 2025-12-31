export interface InsuranceInfo {
  insuranceInfoId: string;
  vehicleId: string;
  insuranceCompany: string;
  policyNumber: string;
  policyHolder: string;
  policyStartDate: string;
  policyEndDate: string;
  agentName?: string;
  agentPhone?: string;
  companyPhone?: string;
  claimsPhone?: string;
  coverageType?: string;
  deductible?: number;
  premium?: number;
  includesRoadsideAssistance: boolean;
  notes?: string;
}
