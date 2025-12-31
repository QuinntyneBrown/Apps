import { DonationType } from './donation-type.enum';

export interface Donation {
  donationId: string;
  organizationId: string;
  amount: number;
  donationDate: string;
  donationType: DonationType;
  receiptNumber?: string;
  isTaxDeductible: boolean;
  notes?: string;
  organizationName?: string;
}

export interface CreateDonationCommand {
  organizationId: string;
  amount: number;
  donationDate: string;
  donationType: DonationType;
  receiptNumber?: string;
  isTaxDeductible: boolean;
  notes?: string;
}

export interface UpdateDonationCommand {
  donationId: string;
  organizationId: string;
  amount: number;
  donationDate: string;
  donationType: DonationType;
  receiptNumber?: string;
  isTaxDeductible: boolean;
  notes?: string;
}
