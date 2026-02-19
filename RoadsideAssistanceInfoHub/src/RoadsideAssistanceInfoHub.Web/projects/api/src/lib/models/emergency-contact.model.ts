export interface EmergencyContact {
  emergencyContactId: string;
  name: string;
  relationship?: string;
  phoneNumber: string;
  alternatePhone?: string;
  email?: string;
  address?: string;
  isPrimaryContact: boolean;
  contactType?: string;
  serviceArea?: string;
  notes?: string;
  isActive: boolean;
}
