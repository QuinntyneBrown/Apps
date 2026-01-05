export interface Referral {
  referralId: string;
  sourceContactId: string;
  description: string;
  outcome?: string;
  notes?: string;
  thankYouSent: boolean;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateReferralRequest {
  sourceContactId: string;
  description: string;
  outcome?: string;
  notes?: string;
}

export interface UpdateReferralRequest {
  referralId: string;
  outcome?: string;
  notes?: string;
  thankYouSent?: boolean;
}
