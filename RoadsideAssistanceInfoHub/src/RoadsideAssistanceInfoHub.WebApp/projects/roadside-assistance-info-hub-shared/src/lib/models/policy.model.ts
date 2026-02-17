export interface Policy {
  policyId: string;
  vehicleId: string;
  provider: string;
  policyNumber: string;
  startDate: string;
  endDate: string;
  emergencyPhone: string;
  maxTowingDistance?: number;
  serviceCallsPerYear?: number;
  coveredServices: string[];
  annualPremium?: number;
  coversBatteryService: boolean;
  coversFlatTire: boolean;
  coversFuelDelivery: boolean;
  coversLockout: boolean;
  notes?: string;
}
