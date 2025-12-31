export interface TrustedContact {
  trustedContactId: string;
  userId: string;
  fullName: string;
  relationship: string;
  email: string;
  phoneNumber?: string;
  role?: string;
  isPrimaryContact: boolean;
  isNotified: boolean;
  notes?: string;
  createdAt: Date;
}

export interface CreateTrustedContactCommand {
  userId: string;
  fullName: string;
  relationship: string;
  email: string;
  phoneNumber?: string;
  role?: string;
  isPrimaryContact: boolean;
  notes?: string;
}

export interface UpdateTrustedContactCommand {
  trustedContactId: string;
  fullName: string;
  relationship: string;
  email: string;
  phoneNumber?: string;
  role?: string;
  isPrimaryContact: boolean;
  notes?: string;
}
